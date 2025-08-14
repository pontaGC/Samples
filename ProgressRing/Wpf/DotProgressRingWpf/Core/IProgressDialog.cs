using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Core.Dialogs
{
    /// <summary>
    /// Responsible for showing a progress dialog to the user.
    /// </summary>
    public interface IProgressDialog
    {
        #region Show

        /// <summary>
        /// Shows a progress dialogs while running the given function in background thread.
        /// </summary>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="action">The action to run.</param>
        /// <param name="isCancellable">The value indicating whether the dialog can be cancelled.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        ProgressDialogResult Show(
            Window ownerWindow,
            string title,
            string initialMessage,
            Action<IProgress<ProgressReport>, CancellationToken> action,
            bool isCancellable = true,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel while running the given function in background thread.
        /// </summary>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="action">The action to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        ProgressDialogResult Show(
            Window ownerWindow,
            string title,
            string initialMessage,
            Action<IProgress<ProgressReport>> action,
            ProgressDialogSettings dialogSettings = null);

        /// Shows a progress dialogs without cancel and percent display while running the given function in background thread.
        /// </summary>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="action">The action to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        ProgressDialogResult Show(
            Window ownerWindow,
            string title,
            string initialMessage,
            Action action,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs while running the given function in background thread.
        /// </summary>
        /// <typeparam name="T">The type of a function result.</typeparam>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="function">The function to run.</param>
        /// <param name="isCancellable">The value indicating whether the dialog can be cancelled.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        ProgressDialogResult<T> Show<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, CancellationToken, T> function,
            bool isCancellable = true,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel while running the given function in background thread.
        /// </summary>
        /// <typeparam name="T">The type of a function result.</typeparam>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="function">The function to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        ProgressDialogResult<T> Show<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, T> function,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel and percent display while running the given function in background thread.
        /// </summary>
        /// <typeparam name="T">The type of a function result.</typeparam>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="function">The function to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        ProgressDialogResult<T> Show<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<T> function,
            ProgressDialogSettings dialogSettings = null);

        #endregion

        #region ShowAsync

        /// <summary>
        /// Shows a progress dialogs while running the given function in background thread.
        /// </summary>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="task">The task to run.</param>
        /// <param name="isCancellable">The value indicating whether the dialog can be cancelled.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        Task<ProgressDialogResult> ShowAsync(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, CancellationToken, Task> task,
            bool isCancellable = true,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel while running the given function in background thread.
        /// </summary>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="task">The task to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        Task<ProgressDialogResult> ShowAsync(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, Task> task,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel and percent display while running the given function in background thread.
        /// </summary>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="task">The task to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        Task<ProgressDialogResult> ShowAsync(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<Task> task,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs while running the given function in background thread.
        /// </summary>
        /// <typeparam name="T">The type of a function result.</typeparam>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="task">The task to run.</param>
        /// <param name="isCancellable">The value indicating whether the dialog can be cancelled.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        Task<ProgressDialogResult<T>> ShowAsync<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, CancellationToken, Task<T>> task,
            bool isCancellable = true,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel while running the given function in background thread.
        /// </summary>
        /// <typeparam name="T">The type of a function result.</typeparam>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="task">The task to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        Task<ProgressDialogResult<T>> ShowAsync<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, Task<T>> task,
            ProgressDialogSettings dialogSettings = null);

        /// <summary>
        /// Shows a progress dialogs without cancel and percent display while running the given function in background thread.
        /// </summary>
        /// <typeparam name="T">The type of a function result.</typeparam>
        /// <param name="ownerWindow">The owner window in dialog.</param>
        /// <param name="title">The dialog title.</param>
        /// <param name="initialMessage">The initial progress message.</param>
        /// <param name="task">The task to run.</param>
        /// <param name="dialogSettings">The progress dialog option settings.</param>
        /// <returns>The function result.</returns>
        /// <remarks>Throws exceptions in function.</remarks>
        Task<ProgressDialogResult<T>> ShowAsync<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<Task<T>> task,
            ProgressDialogSettings dialogSettings = null);

        #endregion
    }
}
