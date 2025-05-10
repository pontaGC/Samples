using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CopyPaste.CircleView
{
    internal class CircleViewModel : ObservableObject
    {
        private Brush fillColor = Brush.Red;
        private Brush strokeColor = Brush.Black;
        private double positionX = 100;
        private double positionY = 100;

        public Brush FillColor 
        { 
            get => this.fillColor;
            set => this.SetProperty(ref this.fillColor, value);
        }

        public Brush StrokeColor 
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
