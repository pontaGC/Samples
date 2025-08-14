using System;
using System.Threading;
using System.Windows.Input;
using Core.Dialogs;
using Mvvm;
using Prism.Commands;

namespace Dialogs
{
    /// <summary>
    /// The progress dialog view-model.
    /// </summary>
    internal class ProgressDialogViewModel : ViewModelBase
    {
        #region Fields

        private readonly ProgressReporter progressReporter;
        private readonly CancellationTokenSource cancellationTokenSource;
        private readonly DelegateCommand cancelCommand;

        private int percent;
        private bool ispercentVisible;
        private bool? dialogResult;
        private string title;
        private string progressMessage;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressDialogViewModel" /> class.
        /// </summary>
        /// <param name="progressReporter">The progress reporter.</param>
        /// <param name="cancellationTokenSource">The cancellation token source. Can be <c>null</c>.</param>
        public ProgressDialogViewModel(ProgressReporter progressReporter, CancellationTokenSource cancellationTokenSource = null)
        {
            this.progressReporter = progressReporter;          
            this.cancellationTokenSource = cancellationTokenSource;

            this.progressReporter.ProgressChanged += this.OnProgressChanged;
            this.progressReporter.ProgressCompleted += this.OnProgressCompleted;
            this.IsCancellable = cancellationTokenSource != null;
            if (this.IsCancellable)
            {
                this.cancelCommand = new DelegateCommand(this.Cancel);
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a value indicating whether the dialog can be cancelled.
        /// </summary>
        public bool IsCancellable { get; }

        /// <summary>
        /// Gets or sets a dialog result. The view will close when this property is set to <c>true</c> or <c>false</c>.
        /// </summary>
        public bool? DialogResult 
        { 
            get => this.dialogResult;
            set => this.SetProperty(ref this.dialogResult, value);
        }

        /// <summary>
        /// Gets or sets a progress title.
        /// </summary>
        public string Title
        {
            get => this.title;
            set => this.SetProperty(ref this.title, value);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the percent is visible in the dialog.
        /// </summary>
        public bool IsPercentVisible
        {
            get => this.ispercentVisible;
            set => this.SetProperty(ref this.ispercentVisible, value);
        }

        /// <summary>
        /// Gets or sets a percent indicating the current progress state.
        /// </summary>
        public int Percent
        {
            get => this.percent;
            set => this.SetProperty(ref this.percent, value);
        }

        /// <summary>
        /// Gets or sets a message indicating the current progress state.
        /// </summary>
        public string ProgressMessage 
        {
            get => this.progressMessage;
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                   this.SetProperty(ref this.progressMessage, value);
                }
            }
        }

        /// <summary>
        /// Gets a cancel commmand.
        /// </summary>
        public ICommand CancelCommand => this.cancelCommand;

        #endregion

        #region Private Methods

        private void Cancel()
        {
            this.cancellationTokenSource.Cancel();
            this.DialogResult = false;
            this.UnsubsribeEvents();
        }

        private void OnProgressChanged(object sender, ProgressReport report)
        {
            if (!ReferenceEquals(sender, this.progressReporter))
            {
                return;
            }

            switch (report.Type)
            {
                case ProgressReportType.Updated:
                    this.Percent = report.Percent;
                    this.ProgressMessage = report.Message;
                    break;

                case ProgressReportType.UpdatedPercent:
                    this.Percent = report.Percent;
                    break;

                case ProgressReportType.UpdatedMessage:
                    this.ProgressMessage = report.Message;
                    break;
            }
        }

        private void OnProgressCompleted(object sender, EventArgs e)
        {
            this.DialogResult = true;
            this.UnsubsribeEvents();
        }

        private void UnsubsribeEvents()
        {
            this.progressReporter.ProgressChanged -= this.OnProgressChanged;
            this.progressReporter.ProgressCompleted -= this.OnProgressCompleted;
        }

        #endregion
    }
}
