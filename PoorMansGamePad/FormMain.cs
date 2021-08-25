using System;
using System.IO.Ports;
using System.Linq;
using System.Windows.Forms;

namespace PoorMansGamePad {
    public partial class FormMain : Form {

        #region [ declarations ]

        private SerialPort port;
        private TeensyWatcher watcher;

        #endregion

        #region [ constructor ]

        public FormMain() {
            InitializeComponent();

            // eventhandler to get information about changes
            watcher = new TeensyWatcher();
            watcher.ConnectionChanged += ConnectedTeensiesChanged;
        }

        #endregion

        #region [ form events ]

        private void FormMain_Shown(object sender, EventArgs e) {
            var teensy = watcher.ConnectedDevices.FirstOrDefault();
            if (teensy != null && teensy.UsbType == USB_Device.USBtype.UsbSerial) {
                port = new SerialPort(teensy.Port);
                OpenPort();
                LabelUSB.Image = Properties.Resources.usb_connected_64;
                LabelUSB.Text = String.Format("{0}", teensy.Port);
            } else {
                LabelUSB.Image = Properties.Resources.usb_disconnected_64;
                LabelUSB.Text = "";
                ChangeEnabled(false);
            }
        }

        #endregion

        #region [ buttons & keys ]

        private void ButtonSelect_Click(object sender, EventArgs e) {
            Button btn = (Button)sender;

            switch (btn.Name) {
                case "ButtonSelect":
                    WriteToTeensy(101, 1);
                    break;
                case "ButtonStart":
                    WriteToTeensy(102, 2);
                    break;
                case "ButtonA":
                    WriteToTeensy(103, 3);
                    break;
                case "ButtonB":
                    WriteToTeensy(104, 4);
                    break;
                case "ButtonUp":
                    WriteToTeensy(105, 5);
                    break;
                case "ButtonDown":
                    WriteToTeensy(106, 6);
                    break;
                case "ButtonLeft":
                    WriteToTeensy(107, 7);
                    break;
                case "ButtonRight":
                    WriteToTeensy(108, 8);
                    break;
            }
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData) {
            switch (keyData) {
                case Keys.A:
                    ButtonA.PerformClick();
                    return true;
                case Keys.B:
                    ButtonB.PerformClick();
                    return true;
                case Keys.S:
                    ButtonSelect.PerformClick();
                    return true;
                case Keys.P:
                    ButtonStart.PerformClick();
                    return true;
                case Keys.Left:
                    ButtonLeft.PerformClick();
                    return true;
                case Keys.Up:
                    ButtonUp.PerformClick();
                    return true;
                case Keys.Right:
                    ButtonRight.PerformClick();
                    return true;
                case Keys.Down:
                    ButtonDown.PerformClick();
                    return true;
            }

            return base.ProcessCmdKey(ref msg, keyData);
        }

        #endregion

        #region [ teensy ]

        private void ConnectedTeensiesChanged(object sender, ConnectionChangedEventArgs e) {
            USB_Device teensy = e.changedDevice;

            if (teensy.UsbType == USB_Device.USBtype.UsbSerial) {
                if (e.changeType == TeensyWatcher.ChangeType.add) {
                    ClosePort();
                    port = new SerialPort(teensy.Port);
                    OpenPort();

                    this.UIThreadAsync(delegate {
                        LabelUSB.Image = Properties.Resources.usb_connected_64;
                        LabelUSB.Text = String.Format("{0}", teensy.Port);
                        ChangeEnabled(true);
                    });
                } else {
                    ClosePort();

                    this.UIThreadAsync(delegate {
                        ChangeEnabled(false);
                        LabelUSB.Image = Properties.Resources.usb_disconnected_64;
                        LabelUSB.Text = "";
                    });
                }
            }
        }

        private bool OpenPort() {
            bool res = false;
            try {
                port.Open();
                res = true;
            } catch {
                res = false;
            }
            return res;
        }

        private bool ClosePort() {
            bool res = false;
            try {
                if (port != null) {
                    if (port.IsOpen) {
                        port.Close();
                        port.Dispose();
                        GC.SuppressFinalize(this);
                        res = true;
                    }
                }
            } catch {
                res = false;
            }
            return res;
        }

        private void WriteToTeensy(int address, int value) {
            if (port != null) {
                try {
                    if (port.IsOpen) {
                        port.WriteLine(string.Format("{0} {1}\n", address, value));
                    } else {
                        MessageBox.Show("Error writing to Teensy, port is not open!", "PORT ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                } catch (Exception ex) {
                    MessageBox.Show(ex.Message, "PORT ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        #endregion

        #region [ private methodes ]

        void ChangeEnabled(bool enabled) {
            foreach (Control c in this.Controls) {
                c.Enabled = enabled;
            }
        }

        #endregion
    }
}