using System;
using Core.Dialogs;

namespace Dialogs
{
    /// <summary>
    /// The progress reporter.
    /// </summary>
    internal class ProgressReporter : Progress<ProgressReport>
    {
        /// <summary>
        /// Occurs when the progress operation is completed.
        /// </summary>
        internal event EventHandler ProgressCompleted;

        /// <summary>
        /// Notifies the progress has been completed.
        /// </summary>
        internal void OnProgressCompleted()
        {
            this.ProgressCompleted?.Invoke(this, EventArgs.Empty);
        }
    }
}
