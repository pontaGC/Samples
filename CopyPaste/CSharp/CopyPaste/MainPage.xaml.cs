namespace CopyPaste
{
    public partial class MainPage
    {
        private double xOffset = 0;
        private double yOffset = 0;

        public MainPage()
        {
            InitializeComponent();
        }

        public void OnPanUpdated(object sender, PanUpdatedEventArgs e)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                    // 現在のオフセットを記憶
                    xOffset = DraggableCircle.TranslationX;
                    yOffset = DraggableCircle.TranslationY;
                    break;

                case GestureStatus.Running:
                    // 新しい位置を計算
                    double newX = xOffset + e.TotalX;
                    double newY = yOffset + e.TotalY;

                    // Grid のサイズ取得
                    double maxX = MainLayout.Width - DraggableCircle.WidthRequest;
                    double maxY = MainLayout.Height - DraggableCircle.HeightRequest;

                    // はみ出さないよう制限
                    newX = Math.Clamp(newX, 0, maxX);
                    newY = Math.Clamp(newY, 0, maxY);

                    DraggableCircle.TranslationX = newX;
                    DraggableCircle.TranslationY = newY;
                    break;
            }
        }
    }

}
