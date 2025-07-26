using System.Configuration;
using System.Data;
using System.Windows;

namespace FileDragAndDrop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow()
            {
                DataContext = new MainWindowViewModel(),
            };
            mainWindow.ShowDialog();
        }
    }
}
