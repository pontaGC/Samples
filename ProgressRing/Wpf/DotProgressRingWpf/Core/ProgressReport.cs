namespace Core.Dialogs
{
    /// <summary>
    /// The progress report.
    /// </summary>
    public class ProgressReport
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressReport" /> class.
        /// </summary>
        /// <param name="percent">The perent indicating progress state.</param>
        /// <param name="message">The progress message.</param>
        /// <param name="type">The progress report type.</param>
        private ProgressReport(int percent, string message, ProgressReportType type)
        {
            if (percent <= 0)
            {
                this.Percent = 0;
            }
            else if (100 <= percent)
            {
                this.Percent = 100;
            }
            else
            {
                this.Percent = percent;
            }

            this.Message = message ?? string.Empty;
            this.Type = type;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets a percent indicating the current progress state.
        /// </summary>
        public int Percent { get; }

        /// <summary>
        /// Gets a message indicating the current progress state.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets a progress report type.
        /// </summary>
        public ProgressReportType Type { get; }

        #endregion

        #region Methods

        /// <summary>
        /// Updates a progress percent and message.
        /// </summary>
        /// <param name="percent">The percent to update.</param>
        /// <param name="message">The progress message to update.</param>
        /// <returns>An instance of the progress report.</returns>
        public static ProgressReport Update(int percent, string message)
        {
            return new ProgressReport(percent, message, ProgressReportType.Updated);
        }

        /// <summary>
        /// Updates a progress.
        /// </summary>
        /// <param name="message">The percent to update.</param>
        /// <returns>An instance of the progress report.</returns>
        public static ProgressReport UpdatePercent(int percent)
        {
            return new ProgressReport(percent, string.Empty, ProgressReportType.UpdatedPercent);
        }

        /// <summary>
        /// Updates a progress message.
        /// </summary>
        /// <param name="message">The percent to update.</param>
        /// <returns>An instance of the progress report.</returns>
        public static ProgressReport UpdateMessage(string message)
        {
            return new ProgressReport(50, message, ProgressReportType.UpdatedMessage);
        }

        #endregion
    }
}
