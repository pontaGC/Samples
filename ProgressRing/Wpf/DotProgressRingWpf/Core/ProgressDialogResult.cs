namespace Core.Dialogs
{
    /// <summary>
    /// The progress dialog result.
    /// </summary>
    public class ProgressDialogResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressDialogResult" /> class.
        /// </summary>
        /// <param name="isCanceled">The value indicating whether the function execution was canceled or not.</param>
        public ProgressDialogResult(bool isCanceled)
        {
            this.IsCanceled = isCanceled;
        }

        /// <summary>
        /// Gets a value indicating whether the function execution was canceled or not.
        /// </summary>
        public bool IsCanceled { get; }
    }

    /// <summary>
    /// The progress dialog result.
    /// </summary>
    /// <typeparam name="T">The type of </typeparam>
    public class ProgressDialogResult<T> : ProgressDialogResult
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProgressDialogResult{T}" /> class.
        /// </summary>
        /// <param name="isCanceled">The value indicating whether the function execution was canceled or not.</param>
        /// <param name="value">The function result.</param>
        public ProgressDialogResult(bool isCanceled, T value)
            : base(isCanceled)
        {
            this.Value = value;
        }

        /// <summary>
        /// Gets a function result.
        /// </summary>
        public T Value { get; }
    }
}
