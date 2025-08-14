namespace Core.Dialogs
{
    /// <summary>
    /// The progress report type.
    /// </summary>
    public enum ProgressReportType
    {
        /// <summary>
        /// Indicates that the task progress has been updated.
        /// </summary>
        Updated,

        /// <summary>
        /// Updated a progress percent.
        /// </summary>
        UpdatedPercent,

        /// <summary>
        /// Updated a progress message.
        /// </summary>
        UpdatedMessage,
    }
}
