using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CopyPaste.CircleView;
using Services.Core.Circles;
using Services.Core.Extensions;
using Services.Core.Serialization;

using Color = System.Drawing.Color;

namespace CopyPaste
{
    internal class MainPageViewModel : ObservableObject
    {
        #region Fields

        private readonly IXmlSerializer xmlSerializer;
        private readonly IClipboard clipboard;

        private readonly AsyncRelayCommand<Page> copyCircleCommand;
        private readonly AsyncRelayCommand<Page> pasteCircleCommand;

        private string copiedCirclesData;
        private CircleViewModel selectedCircle;

        #endregion

        #region Constructors

        public MainPageViewModel(IXmlSerializer xmlSerializer, IClipboard clipboard)
        {
            this.xmlSerializer = xmlSerializer;
            this.clipboard = clipboard;

            this.Circles.Add(new CircleViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Circle 1",
            });
            this.Circles.Add(new CircleViewModel()
            {
                Id = Guid.NewGuid().ToString(),
                Name = "Circle 2",
                PositionX = 150,
                PositionY = 150,
                FillColor = Color.Blue,
            });

            this.copyCircleCommand = new AsyncRelayCommand<Page>(this.CopyCircle, this.CanCopyCircle);
            this.pasteCircleCommand = new AsyncRelayCommand<Page>(this.PasteCircle, this.CanPasteCircle);
        }

        #endregion

        #region Properties

        public CircleViewModel SelectedCircle
        {
            get => this.selectedCircle;
            set
            {
                if (this.SetProperty(ref this.selectedCircle, value))
                {
                    this.copyCircleCommand.NotifyCanExecuteChanged();
                }
            }
        }

        public string CopiedCirclesData
        {
            get => this.copiedCirclesData;
            set => this.SetProperty(ref this.copiedCirclesData, value);
        }

        public ICommand CopyCircleCommand => this.copyCircleCommand;

        public ICommand PasteCircleCommand => this.pasteCircleCommand;

        public ObservableCollection<CircleViewModel> Circles { get; } = new ObservableCollection<CircleViewModel>();

        #endregion

        #region Methods

        private bool CanCopyCircle(Page? page)
        {
            return this.SelectedCircle != null;
        }

        private async Task CopyCircle(Page? page)
        {
            var circleData = ConvertCircleToSerializableModel(this.SelectedCircle);
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    this.xmlSerializer.Serialize(circleData, memoryStream, null, Encoding.UTF8);

                    memoryStream.Seek(0, SeekOrigin.Begin);
                    using (var streamReader = new StreamReader(memoryStream))
                    {
                        this.CopiedCirclesData = streamReader.ReadToEnd();
                    }
                }

                // This project does not use the clipboard, but demonstrates what you can copy to the clipboard.
                await this.clipboard.SetTextAsync(this.CopiedCirclesData);
            }
            catch (InvalidOperationException invalidOperationException)
            {
                await page.DisplayAlert("Copy Error", invalidOperationException.Message, "OK");
            }
            finally
            {
                this.pasteCircleCommand.NotifyCanExecuteChanged();
            }
        }


        private bool CanPasteCircle(Page? page)
        {
            return !string.IsNullOrEmpty(this.CopiedCirclesData);
        }

        private async Task PasteCircle(Page? page)
        {
            try
            {
                using (var reader = new StringReader(this.CopiedCirclesData))
                {
                    var deserializedCirclesData = this.xmlSerializer.Deserialize<CirclesData>(reader);
                    var pastingCircleData = deserializedCirclesData.Circles.FirstOrDefault();
                    if (pastingCircleData is not null)
                    {
                        var pastingCircle = ConvertBackSerializableModel(pastingCircleData);
                        this.Circles.Add(pastingCircle);
                    }
                }
            }
            catch (InvalidOperationException invalidOperationException)
            {
                await page.DisplayAlert("Paste Error", invalidOperationException.Message, "OK");
            }
            finally
            {
                this.ResetCopyData();
            }
        }

        private static CirclesData ConvertCircleToSerializableModel(CircleViewModel source)
        {
            var circleData = new CircleData
            {
                Id = source.Id,
                Name = source.Name,
                Position = new CirclePositionData
                {
                    X = source.PositionX,
                    Y = source.PositionY,
                },
                Radius = source.Radius,
                StrokeColor = source.StrokeColor.GetHtmlColorCode(),
                FillColor = source.FillColor.GetHtmlColorCode(),
            };
            return new CirclesData { Circles = new List<CircleData> { circleData } };
        }

        private static CircleViewModel ConvertBackSerializableModel(CircleData source)
        {
            return new CircleViewModel
            {
                Id = Guid.NewGuid().ToString(), // to avoid ID conflict
                Name = source.Name + "_copy",
                PositionX = source.Position.X + source.Radius, // to avoid overlap
                PositionY = source.Position.Y + source.Radius, // to avoid overlap
                Radius = source.Radius,
                StrokeColor = ColorTranslator.FromHtml(source.StrokeColor),
                FillColor = ColorTranslator.FromHtml(source.FillColor),
            };
        }

        private void ResetCopyData()
        {
            this.copiedCirclesData = string.Empty;
        }

        #endregion
    }
}
