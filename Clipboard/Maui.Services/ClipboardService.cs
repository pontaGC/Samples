namespace Maui.Services
{
    /// <inheritdoc />
    public class ClipboardService : IClipboardService
    {
        #region Fields

        // Milliseconds intervals
        private static readonly ushort[] RetrySpans = { 500, 400, 200 };

        #endregion

        #region Events


        /// <inheritdoc />
        public event EventHandler<EventArgs> ContentChanged
        {
            add { Clipboard.ClipboardContentChanged += value; }
            remove { Clipboard.ClipboardContentChanged -= value; }
        }

        #endregion

        #region Public Methods

        /// <inheritdoc />
        public bool HasText()
        {
            return Clipboard.HasText;
        }

        /// <inheritdoc />
        public async Task<string?> GetTextAsync()
        {
            var text = await InvokeAsyncWithRetry(Clipboard.GetTextAsync);
            return text;
        }

        /// <inheritdoc />
        public async Task<bool> SetTextAsync(string? text)
        {
            return await InvokeAsyncWithRetry(() => Clipboard.SetTextAsync(text));
        }

        #endregion

        #region Private Methods

        private static async Task<bool> InvokeAsyncWithRetry(Func<Task> taskFactory)
        {
            foreach (var interval in RetrySpans)
            {
                try
                {
                    await taskFactory.Invoke();
                    return true;
                }
                catch
                {
                    await Task.Delay(interval);
                }
            }

            return false;
        }

        private static async Task<TResult?> InvokeAsyncWithRetry<TResult>(Func<Task<TResult>> taskFactory)
        {
            foreach (var interval in RetrySpans)
            {
                try
                {
                    return await taskFactory.Invoke();
                }
                catch
                {
                    await Task.Delay(interval);
                }
            }

            return default;
        }

        #endregion
    }
}
