using Microsoft.Maui.Controls.Shapes;
using Microsoft.Maui.Layouts;

namespace DotProgressRing;

public partial class DotProgressRingView : ContentView
{
    #region Fields

    private const int CirclesCount = 12;
    private const double CircleWidth = 10;
    private const double CircleHeight = 10;
    private const double RadiusRatio = 0.35; // not length

    private CancellationTokenSource? progressCts;


    #endregion

    #region Constructors

    public DotProgressRingView()
	{
		InitializeComponent();

        this.InitializeCirlces();

        this.Loaded += this.OnLoaded;
        this.Unloaded += this.OnUnloaded;
    }

    #endregion

    #region Private Megthods

    private void InitializeCirlces()
    {
        var eclipseStyle = this.Resources["DotStyle"] as Style;
        for (int i = 0; i < CirclesCount; ++i)
        {
            var circle = new Ellipse() 
            { 
                WidthRequest = CircleWidth,
                HeightRequest = CircleHeight,
                Style = eclipseStyle,
            };

            double angle = (2 * Math.PI) * i / CirclesCount;
            double posX = 0.5 + RadiusRatio * Math.Sin(angle);
            double posY = 0.5 - RadiusRatio * Math.Cos(angle);

            AbsoluteLayout.SetLayoutBounds(circle, new Rect(posX, posY, AbsoluteLayout.AutoSize, AbsoluteLayout.AutoSize));
            AbsoluteLayout.SetLayoutFlags(circle, AbsoluteLayoutFlags.PositionProportional);

            this.CirclesAbsoluteLayout.Add(circle);
        }
    }

    private async Task BeginProgressAnimation(CancellationToken cancellationToken)
    {
        double[] opacitySteps = { 1.0, 0.8, 0.6, 0.4, 0.2, 0.0 };

        var circles = this.CirclesAbsoluteLayout.Children.OfType<Ellipse>().ToArray();
        var circlesCount = circles.Length;
        while (!cancellationToken.IsCancellationRequested)
        {
            for (int frame = 0; frame < circlesCount; ++frame)
            {
                for (int i = 0; i < circlesCount; ++i)
                {
                    int relativeStep = (frame - i + circlesCount) % circlesCount;
                    if (relativeStep < opacitySteps.Length)
                    {
                        circles[i].Opacity = opacitySteps[relativeStep];
                    }
                    else
                    {
                        circles[i].Opacity = 0;
                    }
                }

                // 1 frame is 0.1 seconds
                await Task.Delay(100, cancellationToken);
            }
        }
    }

    private void OnLoaded(object? sender, EventArgs e)
    {
        this.progressCts = new CancellationTokenSource();
        _ = this.BeginProgressAnimation(this.progressCts.Token); // Executes in background
    }

    private void OnUnloaded(object? sender, EventArgs e)
    {
        this.ExitProgressAnimation();
    }

    private void ExitProgressAnimation()
    {
        this.progressCts?.Cancel();
        this.progressCts = null;
    }

    #endregion
}