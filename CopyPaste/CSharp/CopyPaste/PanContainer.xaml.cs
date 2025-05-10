namespace CopyPaste;

public partial class PanContainer : ContentView
{
    public static readonly BindableProperty PanAreaWidthProperty = BindableProperty.Create(
        nameof(PanAreaWidth),
        typeof(double),
        typeof(PanContainer),
        defaultValue: 0.0,
        defaultBindingMode: BindingMode.OneWay);

    public static readonly BindableProperty PanAreaHeightProperty = BindableProperty.Create(
        nameof(PanAreaHeight),
        typeof(double),
        typeof(PanContainer),
        defaultValue: 0.0,
        defaultBindingMode: BindingMode.OneWay);

    private readonly PanGestureRecognizer panGesture = new PanGestureRecognizer();

    private double panX = 0;
    private double panY = 0;

	public PanContainer()
	{
		InitializeComponent();

        this.panGesture.PanUpdated += this.OnPanUpdated;
        GestureRecognizers.Add(this.panGesture);
    }

    public double PanAreaWidth
    {
        get => (double)this.GetValue(PanAreaWidthProperty);
        set => this.SetValue(PanAreaWidthProperty, value);
    }

    public double PanAreaHeight
    {
        get => (double)this.GetValue(PanAreaHeightProperty);
        set => this.SetValue(PanAreaHeightProperty, value);
    }

    private void OnPanUpdated(object sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Started:
            case GestureStatus.Completed:
                // Store intial position or the translation applied during the pan
                this.panX = this.Content.TranslationX;
                this.panY = this.Content.TranslationY;
                break;

            case GestureStatus.Running:

                var newX = this.panX + e.TotalX;
                var newY = this.panY + e.TotalY;

                // Calculate the maximum X and Y values ​​not to go out the area
                var maxX = this.PanAreaWidth - this.Content.Width;
                var maxY = this.PanAreaHeight - this.Content.Height;

                this.Content.TranslationX = Math.Clamp(newX, 0, maxX);
                this.Content.TranslationY = Math.Clamp(newY, 0, maxY);
                break;
        }
    }
}