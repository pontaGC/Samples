using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Core.Dialogs;
using Mvvm;
using Prism.Commands;

namespace DotProgressRingWpf
{
    internal class MainWindowViewModel : ViewModelBase
    {
        private readonly IProgressDialog progressDialog;
        private readonly DelegateCommand<Window> showProgresssCommand;

        public MainWindowViewModel(IProgressDialog progressDialog)
        {
            this.progressDialog = progressDialog;
            this.showProgresssCommand = new DelegateCommand<Window>(async w => await this.ShowProgresss(w));
        }

        public ICommand ShowProgressRingCommand => this.showProgresssCommand;

        private async Task ShowProgresss(Window ownerWindow)
        {
            try
            {
                var dialogResult = await this.progressDialog.ShowAsync(
                    ownerWindow,
                    "Sample",
                    "Executing...",
                    async (progress, token) =>
                    {
                        int percent = 0;
                        while (!token.IsCancellationRequested && percent < 100)
                        {
                            progress.Report(ProgressReport.UpdatePercent(percent));
                            await Task.Delay(100);
                            ++percent;
                        }

                        return 0;
                    });

                if (dialogResult.IsCanceled)
                {
                    return;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ownerWindow, ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
