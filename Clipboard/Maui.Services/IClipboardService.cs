namespace Maui.Services
{
    /// <summary>
    /// Responsible for facilitating transferring data to and from the system Clipboard.
    /// </summary>
    public interface IClipboardService
    {
        /// <summary>
        /// Occurs when the clipboard content changes.
        /// </summary>
        event EventHandler<EventArgs> ContentChanged;

        /// <summary>
        /// Gets a value indicating whether there is any text on the clipboard.
        /// </summary>
        /// <returns><c>true</c> if the Clipboard contains text data; otherwise, <c>false</c>.</returns>
        bool HasText();

        /// <summary>
        /// Returns any text that is on the clipboard.
        /// </summary>
        /// <returns>The text content that is on the clipboard, or <c>null</c> if there is none.</returns>
        Task<string?> GetTextAsync();

        /// <summary>
        /// Sets the contents of the clipboard to be the specified text.
        /// </summary>
        /// <param name="text">The text data to store on the Clipboard.</param>
        /// <returns><c>true</c> if setting the given text to clipboard is successful; otherwise, <c>false</c>.</returns>
        Task<bool> SetTextAsync(string? text);        
    }
}
