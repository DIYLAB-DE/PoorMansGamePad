using HidLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;

namespace PoorMansGamePad {
    public class TeensyWatcher : IDisposable {

        #region [ declarations ]

        private static Dictionary<PJRC_Board, BoardDefinition> BoardDefinitions = new Dictionary<PJRC_Board, BoardDefinition>()
        {
            { PJRC_Board.Teensy_40, new BoardDefinition
            {
                MCU =       "IMXRT1062",
                FlashSize = 2048 * 1024,
                BlockSize = 1024,
                DataOffset= 64,
                AddrCopy = (rep,addr) => {rep[0]=addr[0]; rep[1]=addr[1]; rep[2]=addr[2];}
            }},

            { PJRC_Board.Teensy_36, new BoardDefinition
            {
                MCU =       "MK66FX1M0",
                FlashSize = 1024 * 1024,
                BlockSize = 1024,
                DataOffset= 64,
                AddrCopy = (rep,addr) => {rep[0]=addr[0]; rep[1]=addr[1]; rep[2]=addr[2];}
            }},

            { PJRC_Board.Teensy_35, new BoardDefinition
            {
                MCU=       "MK64FX512",
                FlashSize = 512 * 1024,
                BlockSize = 1024,
                DataOffset= 64,
                AddrCopy = (rep,addr) => {rep[0]=addr[0]; rep[1]=addr[1]; rep[2]=addr[2];}
            }},

            {PJRC_Board.Teensy_31_2, new BoardDefinition
            {
                MCU=       "MK20DX256",
                FlashSize = 256 * 1024,
                BlockSize = 1024,
                DataOffset= 64,
                AddrCopy = (rep,addr) => {rep[0]=addr[0]; rep[1]=addr[1]; rep[2]=addr[2];}
            }},

            {PJRC_Board.Teensy_30, new BoardDefinition
            {
                MCU=       "MK20DX128",
                FlashSize = 128 * 1024,
                BlockSize = 1024,
                DataOffset= 64,
                AddrCopy = (rep,addr)=>{rep[0]=addr[0]; rep[1]=addr[1]; rep[2]=addr[2];}
            }},

            {PJRC_Board.Teensy_LC, new BoardDefinition
            {
                MCU =      "MK126Z64",
                FlashSize = 62 * 1024,
                BlockSize = 512,
                DataOffset= 64,
                AddrCopy = (rep,addr)=>{rep[0]=addr[0]; rep[1]=addr[1]; rep[2]=addr[2];}
            }},

            {PJRC_Board.Teensy_2pp, new BoardDefinition
            {
                MCU =      "AT90USB1286",
                FlashSize = 12 * 1024,
                BlockSize = 256,
                DataOffset= 2,
                AddrCopy = (rep,addr)=>{rep[0]=addr[1]; rep[1]=addr[2];}
            }},

            {PJRC_Board.Teensy_2, new BoardDefinition
            {
                MCU =      "ATMEGA32U4",
                FlashSize = 31 * 1024,
                BlockSize = 128,
                DataOffset= 2,
                AddrCopy = (rep,addr)=>{rep[0]=addr[0]; rep[1]=addr[1];}
            }}
        };

        private class BoardDefinition {
            public int FlashSize;
            public int BlockSize;
            public int DataOffset;
            public string MCU;
            public Action<byte[], byte[]> AddrCopy;
        }

        public enum PJRC_Board {
            Teensy_40,
            Teensy_36,
            Teensy_35,
            Teensy_31_2,
            Teensy_30,
            Teensy_LC,
            Teensy_2pp,
            Teensy_2,
            unknown,
        }

        const uint vid = 0x16C0;
        const uint serPid = 0x483;
        const uint halfKayPid = 0x478;
        string vidStr = "'%USB_VID[_]" + vid.ToString("X") + "%'";

        #endregion

        #region [ properties and events ]

        public List<USB_Device> ConnectedDevices { get; } = new List<USB_Device>();
        public event EventHandler<ConnectionChangedEventArgs> ConnectionChanged;

        #endregion

        #region [ constructor / destructor ]

        public TeensyWatcher() {
            using (var searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_PnPEntity WHERE DeviceID LIKE " + vidStr)) {
                foreach (var mgmtObject in searcher.Get()) {
                    var device = MakeDevice(mgmtObject);
                    if (device != null) {
                        ConnectedDevices.Add(device);
                    }
                }
            }
            StartWatching();
        }

        public void Dispose() {
            StopWatching();
        }

        #endregion

        #region [ port watching ]

        protected ManagementEventWatcher CreateWatcher = null;
        protected ManagementEventWatcher DeleteWatcher = null;

        protected void StartWatching() {
            StopWatching(); // Just to make sure 

            DeleteWatcher = new ManagementEventWatcher {
                Query = new WqlEventQuery {
                    EventClassName = "__InstanceDeletionEvent",
                    Condition = "TargetInstance ISA 'Win32_PnPEntity'",
                    WithinInterval = new TimeSpan(0, 0, 1), //Todo: make the interval settable
                },
            };
            DeleteWatcher.EventArrived += PortsChanged;
            DeleteWatcher.Start();

            CreateWatcher = new ManagementEventWatcher {
                Query = new WqlEventQuery {
                    EventClassName = "__InstanceCreationEvent",
                    Condition = "TargetInstance ISA 'Win32_PnPEntity'",
                    WithinInterval = new TimeSpan(0, 0, 1), //Todo: make the interval settable
                },
            };
            CreateWatcher.EventArrived += PortsChanged;
            CreateWatcher.Start();
        }

        protected void StopWatching() {
            if (CreateWatcher != null) {
                CreateWatcher.Stop();
                CreateWatcher.Dispose();
            }
            if (DeleteWatcher != null) {
                DeleteWatcher.Stop();
                DeleteWatcher.Dispose();
            }
        }

        public enum ChangeType {
            add,
            remove
        }

        void PortsChanged(object sender, EventArrivedEventArgs e) {
            var device = MakeDevice((ManagementBaseObject)e.NewEvent["TargetInstance"]);
            if (device != null) {
                ChangeType type = e.NewEvent.ClassPath.ClassName == "__InstanceCreationEvent" ? ChangeType.add : ChangeType.remove;

                if (type == ChangeType.add) {
                    ConnectedDevices.Add(device);
                    OnConnectionChanged(type, device);
                } else {
                    var rd = ConnectedDevices.Find(d => d.Serialnumber == device.Serialnumber);
                    ConnectedDevices.Remove(rd);
                    OnConnectionChanged(type, rd);
                }
            }
        }

        #endregion

        #region [ helpers ]

        protected USB_Device MakeDevice(ManagementBaseObject mgmtObj) {
            var DeviceIdParts = ((string)mgmtObj["PNPDeviceID"]).Split("\\".ToArray());
            if (DeviceIdParts[0] != "USB") return null;

            int start = DeviceIdParts[1].IndexOf("PID_") + 4;
            uint pid = Convert.ToUInt32(DeviceIdParts[1].Substring(start, 4), 16);
            if (pid == serPid) {

                string port = null;
                uint serNum = 0;

                if ((string)mgmtObj["PNPClass"] == "Ports") {
                    port = (((string)mgmtObj["Caption"]).Split("()".ToArray()))[1];
                } else return null;

                return new USB_Device {
#pragma warning disable 0618 // obsolete warning
                    Type = USB_Device.USBtype.UsbSerial,
                    UsbType = USB_Device.USBtype.UsbSerial,
                    Port = port,
                    Serialnumber = serNum,
                    Board = PJRC_Board.unknown,
                    BoardType = PJRC_Board.unknown
#pragma warning restore 0618
                };
            } else if (pid == halfKayPid) {
                // getting the hid device like done in the following block is not very efficient
                // need to find a way to extract the device path from the WMI and construct
                // the devcie using HidDevices.GetDevice
                var hwid = ((string[])mgmtObj["HardwareID"])[0];
                uint serNum = Convert.ToUInt32(DeviceIdParts[2], 16);
                if (serNum != 0xFFFFFFFF) // diy boards without serial number
                {
                    serNum *= 10;
                }

                var devices = HidDevices.Enumerate((int)vid, (int)halfKayPid); // Get all boards with running HalfKay
                var device = devices.FirstOrDefault(x => GetSerialNumber(x) == serNum);

                PJRC_Board board = PJRC_Board.unknown;

                switch (device?.Capabilities.Usage) {
                    case 0x1A: board = PJRC_Board.unknown; break;
                    case 0x1B: board = PJRC_Board.Teensy_2; break;
                    case 0x1C: board = PJRC_Board.Teensy_2pp; break;
                    case 0x1D: board = PJRC_Board.Teensy_30; break;
                    case 0x1E: board = PJRC_Board.Teensy_31_2; break;
                    case 0x20: board = PJRC_Board.Teensy_LC; break;
                    case 0x21: board = PJRC_Board.Teensy_31_2; break;
                    case 0x1F: board = PJRC_Board.Teensy_35; break;
                    case 0x22: board = PJRC_Board.Teensy_36; break;
                    case 0x24: board = PJRC_Board.Teensy_40; break;
                    default: board = PJRC_Board.unknown; break;
                }

                return new USB_Device {
#pragma warning disable 0618
                    Type = USB_Device.USBtype.HalfKay,
                    UsbType = USB_Device.USBtype.HalfKay,
                    Port = "",
                    Serialnumber = serNum,
                    Board = board,
                    BoardType = board
#pragma warning restore 0618
                };
            } else return null;
        }

        static uint GetSerialNumber(HidDevice device) {
            byte[] sn;
            device.ReadSerialNumber(out sn);

            string snString = System.Text.Encoding.Unicode.GetString(sn).TrimEnd("\0".ToArray());

            var serialNumber = Convert.ToUInt32(snString, 16);
            if (serialNumber != 0xFFFFFFFF) {
                serialNumber *= 10;
            }
            return serialNumber;
        }

        #endregion

        #region [ eventHandler ]

        protected void OnConnectionChanged(ChangeType type, USB_Device changedDevice) {
            if (ConnectionChanged != null) ConnectionChanged(this, new ConnectionChangedEventArgs(type, changedDevice));
        }

        #endregion
    }

    #region [ public classes ]

    public class USB_Device {
        public enum USBtype {
            UsbSerial,
            HalfKay,
            HID,
            //...
        }

        [Obsolete("Please use UsbType instead.", false)]
        public USBtype Type;
        public USBtype UsbType;
        public uint Serialnumber { get; set; }
        public string Port { get; set; }
        [Obsolete("Please use BoardType instead.", false)]
        public TeensyWatcher.PJRC_Board Board { get; set; }
        public TeensyWatcher.PJRC_Board BoardType;
        public string BoardId {
            get {
                switch (BoardType) {
                    case TeensyWatcher.PJRC_Board.Teensy_2: return $"Teensy 2 ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_2pp: return $"Teensy 2++ ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_LC: return $"Teensy LC ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_30: return $"Teensy 3.0 ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_31_2: return $"Teensy 3.2 ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_35: return $"Teensy 3.5 ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_36: return $"Teensy 3.6 ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.Teensy_40: return $"Teensy 4.0 ({Serialnumber})";
                    case TeensyWatcher.PJRC_Board.unknown: return $"Unknown Board ({Serialnumber})";
                    default: return null;
                }
            }
        }
    }

    public class ConnectionChangedEventArgs : EventArgs {
        public readonly TeensyWatcher.ChangeType changeType;
        public readonly USB_Device changedDevice;

        public ConnectionChangedEventArgs(TeensyWatcher.ChangeType type, USB_Device changedDevice) {
            this.changeType = type;
            this.changedDevice = changedDevice;
        }
    }

    #endregion
}