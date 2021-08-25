using System;
using System.Windows.Forms;

namespace PoorMansGamePad {
    public static class Extensions {
        public static void UIThreadAsync(this Control control, Action code) {
            if (control.InvokeRequired) {
                control.BeginInvoke(code);
                return;
            }
            code.Invoke();
        }

        public static void UIThreadSync(this Control control, Action code) {
            if (control.InvokeRequired) {
                control.Invoke(code);
                return;
            }
            code.Invoke();
        }
    }
}