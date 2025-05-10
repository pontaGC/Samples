using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CopyPaste.CircleView;

namespace CopyPaste
{
    internal class MainPageViewModel : ObservableObject
    {
        public ObservableCollection<CircleViewModel> CircleViewModels { get; } = new ObservableCollection<CircleViewModel>();

        public MainPageViewModel()
        {
            this.CircleViewModels.Add(new CircleViewModel());
        }
    }
}
