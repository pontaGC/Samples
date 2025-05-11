using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        private double positionX = 100;
        private double positionY = 100;

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
        
        public double PositionX
        {
            get => this.positionX;
            set => this.SetProperty(ref this.positionX, value);
        }

        public double PositionY
        {
            get => this.positionY;
            set => this.SetProperty(ref this.positionY, value);
        }
    }
}
