using System.Windows;
using Core.Dialogs;
using Dialogs;

namespace DotProgressRingWpf
{
    /// <summary>
    /// Interaction logic for App.xaml.
    /// </summary>
    public partial class App
    {
        /// <inheritdoc />
        protected override void OnStartup(StartupEventArgs e)
        {
            IProgressDialog progressDialog = new ProgressDialog();

            var mainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(progressDialog)
            };
            mainWindow.ShowDialog();
        }
    }
}
