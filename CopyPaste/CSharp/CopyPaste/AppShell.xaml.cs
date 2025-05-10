namespace CopyPaste
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            this.Items.Add(CreateMainPageShellContent());
        }

        private static ShellContent CreateMainPageShellContent()
        {
            var mainPage = new MainPage()
            {
                BindingContext = new MainPageViewModel(),
            };

            return new ShellContent()
            {
                Content = mainPage,
                Route = nameof(MainPage),
            };
        }
    }
}
