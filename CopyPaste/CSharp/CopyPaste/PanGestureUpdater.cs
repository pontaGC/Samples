namespace CopyPaste
{
    public class PanGestureUpdater
    {
        private (double X, double Y) panStart = (0.0, 0.0);

        public void Execute(
            VisualElement element,
            PanUpdatedEventArgs e,
            double panAreaWidth,
            double panAreaHeight)
        {
            switch (e.StatusType)
            {
                case GestureStatus.Started:
                case GestureStatus.Completed:
                    // Store intial position or the translation applied during the pan
                    this.panStart.X = element.TranslationX;
                    this.panStart.Y = element.TranslationY;
                    break;

                case GestureStatus.Running:
                    var newX = this.panStart.X + e.TotalX;
                    var newY = this.panStart.Y + e.TotalY;

                    // Calculate the maximum X and Y values ​​not to go out the area
                    var maxX = panAreaWidth - element.Width;
                    var maxY = panAreaHeight - element.Height;

                    const double MinPosisition = 0.0;
                    element.TranslationX = Math.Clamp(newX, MinPosisition, maxX);
                    element.TranslationY = Math.Clamp(newY, MinPosisition, maxY);
                    break;
            }
        }
    }
}
