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
    private readonly PanGestureUpdater panGestureUpdater = new PanGestureUpdater();

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

    private void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        this.panGestureUpdater.Execute(
            this.Content,
            e,
            this.PanAreaWidth,
            this.PanAreaHeight);
    }
}