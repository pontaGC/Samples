using System;
using System.Runtime.InteropServices;
using System.Windows.Interop;

namespace Dialogs
{
    /// <summary>
    /// Interaction logic for ProgressDialogView.xaml.
    /// </summary>
    internal partial class ProgressDialogView
    {
        [DllImport("user32.dll")]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public ProgressDialogView()
        {
            InitializeComponent();
        }

        /// <inheritdoc />
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            this.HideWindowCloseButton();
        }

        private void HideWindowCloseButton()
        {
            const int GWL_STYLE = -16;
            const int WS_SYSMENU = 0x80000;

            IntPtr handle = new WindowInteropHelper(this).Handle;
            int style = GetWindowLong(handle, GWL_STYLE);
            style = style & (~WS_SYSMENU);
            SetWindowLong(handle, GWL_STYLE, style);
        }
    }
}
