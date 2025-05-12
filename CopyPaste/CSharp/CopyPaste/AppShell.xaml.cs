using Services.Core.Serialization;

namespace CopyPaste
{
    public partial class AppShell : Shell
    {
        public AppShell(IServiceProvider serivceProvider)
        {
            InitializeComponent();

            this.Items.Add(CreateMainPageShellContent(serivceProvider));
        }

        private static ShellContent CreateMainPageShellContent(IServiceProvider serviceProvider)
        {
            var mainPage = new MainPage()
            {
                BindingContext = new MainPageViewModel(
                    serviceProvider.GetRequiredService<IXmlSerializer>()),
            };

            return new ShellContent()
            {
                Content = mainPage,
                Route = nameof(MainPage),
            };
        }
    }
}
