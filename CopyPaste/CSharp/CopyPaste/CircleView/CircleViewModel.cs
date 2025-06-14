using CommunityToolkit.Mvvm.ComponentModel;
using Color = System.Drawing.Color;

namespace CopyPaste.CircleView
{
    internal class CircleViewModel : ObservableObject
    {
        private string id = string.Empty;
        private string name = string.Empty;
        private Color fillColor = Color.Red;
        private Color strokeColor = Color.Black;
        private float positionX = 100;
        private float positionY = 100;
        private float radius = 20;

        public string Id
        {
            get => this.id;
            set => this.SetProperty(ref this.id, value);
        }

        public string Name
        {
            get => this.name;
            set => this.SetProperty(ref this.name, value);
        }

        public Color FillColor 
        { 
            get => this.fillColor;
            set => this.SetProperty(ref this.fillColor, value);
        }

        public Color StrokeColor 
        {
            get => this.strokeColor;
            set => this.SetProperty(ref this.strokeColor, value);
        }
        
        public float PositionX
        {
            get => this.positionX;
            set => this.SetProperty(ref this.positionX, value);
        }

        public float PositionY
        {
            get => this.positionY;
            set => this.SetProperty(ref this.positionY, value);
        }

        public float Radius
        {
            get => this.radius;
            set => this.SetProperty(ref this.radius, value);
        }
    }
}
