using System.Windows.Input;

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Maui.Services;

namespace MauiClipboardSample
{
    internal class MainPageViewModel : ObservableObject
    {
        private readonly IClipboardService clipboardService;

        private readonly AsyncRelayCommand copyTextCommand;
        private readonly RelayCommand hasTextCommand;
        private readonly AsyncRelayCommand pasteTextCommand;

        private string inputText;
        private string operationStatus;
        private string pasteResult;

        public MainPageViewModel(IClipboardService clipboardService)
        {
            this.clipboardService = clipboardService;

            this.copyTextCommand = new AsyncRelayCommand(this.CopyTextAsync, this.CanCopyText);
            this.hasTextCommand = new RelayCommand(this.HasText);
            this.pasteTextCommand = new AsyncRelayCommand(this.PasteTextAsync);
        }

        #region Properties

        public string InputText
        {
            get => this.inputText;
            set 
            { 
                if (this.SetProperty(ref this.inputText, value))
                {
                    this.copyTextCommand.NotifyCanExecuteChanged();
                }
            } 
        }

        public string OperationStatus
        {
            get => this.operationStatus;
            set => this.SetProperty(ref this.operationStatus, value);
        }

        public string PasteResult
        {
            get => this.pasteResult;
            set => this.SetProperty(ref this.pasteResult, value);
        }

        public ICommand HasTextCommand => this.hasTextCommand;

        public ICommand CopyTextCommand => this.copyTextCommand;

        public ICommand PasteTextCommand => this.pasteTextCommand;


        #endregion

        #region Private Methods

        private bool CanCopyText() => !string.IsNullOrEmpty(this.InputText);

        private async Task CopyTextAsync()
        {
            bool success = await this.clipboardService.SetTextAsync(this.InputText);
            this.OperationStatus = success
                ? $"[SetText][Success] Copy successful! ({this.InputText.Length} characters)"
                : "[SetText][Failure] Copy failed. Writing to the clipboard failed after all retries.";
        }

        private async Task PasteTextAsync()
        {
            var pastedText = await this.clipboardService.GetTextAsync();

            if (string.IsNullOrEmpty(pastedText))
            {
                this.OperationStatus = "[GetText][Failure] Paste failed: No text data in the clipboard, or retrieval failed after retries.";
                this.PasteResult = "Null or an empty string";
            }
            else
            {
                this.OperationStatus = $"[GetText][Success] Paste successful! ({pastedText.Length} characters)";
                this.PasteResult = pastedText;
            }
        }

        private void HasText()
        {
            if (this.clipboardService.HasText())
            {
                this.OperationStatus = "[HasText] Clipboard contains text data.";
            }
            else
            {
                this.OperationStatus = "[HasText] Clipboard does not contain text data.";
            }
        }

        #endregion
    }
}
