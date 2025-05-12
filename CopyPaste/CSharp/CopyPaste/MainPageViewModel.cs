using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CopyPaste.CircleView;

using Services.Core.Serialization;

using Color = System.Drawing.Color;

namespace CopyPaste
{
    internal class MainPageViewModel : ObservableObject
    {
        private CircleViewModel selectedCircle;

        public MainPageViewModel(IXmlSerializer xmlSerializer)
        {
            this.Circles.Add(new CircleViewModel()
            {
                Id = "1",
                Name = "Circle 1",
            });
            this.Circles.Add(new CircleViewModel()
            {
                Id = "2",
                Name = "Circle 2",
                PositionX = 150,
                PositionY = 150,
                FillColor = Color.Blue,
            });
        }

        public CircleViewModel SelectedCircle 
        {
            get => this.selectedCircle;
            set => this.SetProperty(ref this.selectedCircle, value);
        }

        public ObservableCollection<CircleViewModel> Circles { get; } = new ObservableCollection<CircleViewModel>();
    }
}
