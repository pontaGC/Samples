using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

using Core.Dialogs;
using Mvvm;

namespace Dialogs
{
    /// <summary>
    /// Responsible for showing a progress dialog to the user.
    /// </summary>
    internal class ProgressDialog : IProgressDialog
    {
        #region Public Methods

        #region Show

        /// <inheritdoc />
        public ProgressDialogResult Show(string title, string initialMessage, Action action, ProgressDialogSettings dialogSettings)
        {
            return this.Show(Application.Current?.MainWindow, title, initialMessage, action, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult Show(string title, string initialMessage, Action<IProgress<ProgressReport>> action, ProgressDialogSettings dialogSettings)
        {
            return this.Show(Application.Current?.MainWindow, title, initialMessage, action, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult Show(string title, string initialMessage, Action<IProgress<ProgressReport>, CancellationToken> action, bool isCancellable, ProgressDialogSettings dialogSettings)
        {
            return this.Show(Application.Current?.MainWindow, title, initialMessage, action, isCancellable, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult Show(Window ownerWindow, string title, string initialMessage, Action action, ProgressDialogSettings dialogSettings)
        {
            var wrappedAction = new Action<IProgress<ProgressReport>, CancellationToken>(
                (p, c) => action.Invoke());

            var settings = dialogSettings ?? new ProgressDialogSettings();
            settings.IsPercentVisible = false;
            return this.Show(ownerWindow, title, initialMessage, wrappedAction, false, settings);
        }

        /// <inheritdoc />
        public ProgressDialogResult Show(Window ownerWindow, string title, string initialMessage, Action<IProgress<ProgressReport>> action, ProgressDialogSettings dialogSettings)
        {
            var wrappedAction = new Action<IProgress<ProgressReport>, CancellationToken>(
                (progress, _) => action.Invoke(progress));
            return this.Show(ownerWindow, title, initialMessage, wrappedAction, false, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult Show(
            Window ownerWindow,
            string title,
            string initialMessage,
            Action<IProgress<ProgressReport>, CancellationToken> action,
            bool isCancellable,
            ProgressDialogSettings dialogSettings)
        {
            using (var cancellationTokenSource = isCancellable ? new CancellationTokenSource() : null)
            {
                // Prepares progress report and dialog
                var progressReporter = new ProgressReporter();
                var createdVVM = CreateProgressDialogViewViewModel(ownerWindow, title, initialMessage, cancellationTokenSource, progressReporter, dialogSettings);
                var dialog = createdVVM.View;

                // Runs a task in background thread and shows progress dialog
                // This redundant description is to catch exceptions
                Exception caughtException = null;
                var cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
                var runTask = Task.Run(() =>
                {
                    try
                    {
                        action(progressReporter, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        caughtException = ex;
                    }
                    finally
                    {
                        progressReporter.OnProgressCompleted();
                    }
                }, cancellationToken);

                dialog.ShowDialog();
                runTask.GetAwaiter().GetResult(); // To get result or catch exceptions

                if (caughtException is OperationCanceledException)
                {
                    return new ProgressDialogResult(true);
                }
                else if (caughtException != null)
                {
                    throw caughtException;
                }

                return new ProgressDialogResult(false);
            }
        }

        #endregion

        #region Show<T>

        /// <inheritdoc />
        public ProgressDialogResult<T> Show<T>(string title, string initialMessage, Func<T> function, ProgressDialogSettings dialogSettings)
        {
            return this.Show(Application.Current?.MainWindow, title, initialMessage, function, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult<T> Show<T>(string title, string initialMessage, Func<IProgress<ProgressReport>, T> function, ProgressDialogSettings dialogSettings)
        {
            return this.Show(Application.Current?.MainWindow, title, initialMessage, function, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult<T> Show<T>(string title, string initialMessage, Func<IProgress<ProgressReport>, CancellationToken, T> function, bool isCancellable, ProgressDialogSettings dialogSettings)
        {
            return this.Show(Application.Current?.MainWindow, title, initialMessage, function, isCancellable, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult<T> Show<T>(Window ownerWindow, string title, string initialMessage, Func<T> function, ProgressDialogSettings dialogSettings)
        {
            var wrappedFunction = new Func<IProgress<ProgressReport>, CancellationToken, T>(
                (p, c) => function.Invoke());

            var settings = dialogSettings ?? new ProgressDialogSettings();
            settings.IsPercentVisible = false;
            return this.Show(ownerWindow, title, initialMessage, wrappedFunction, false, settings);
        }

        /// <inheritdoc />
        public ProgressDialogResult<T> Show<T>(Window ownerWindow, string title, string initialMessage, Func<IProgress<ProgressReport>, T> function, ProgressDialogSettings dialogSettings)
        {
            var wrappedFunction = new Func<IProgress<ProgressReport>, CancellationToken, T>(
                (progress, _) => function.Invoke(progress));
            return this.Show(ownerWindow, title, initialMessage, wrappedFunction, false, dialogSettings);
        }

        /// <inheritdoc />
        public ProgressDialogResult<T> Show<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, CancellationToken, T> function,
            bool isCancellable,
            ProgressDialogSettings dialogSettings)
        {
            using (var cancellationTokenSource = isCancellable ? new CancellationTokenSource() : null)
            {
                // Prepares progress report and dialog
                var progressReporter = new ProgressReporter();
                var createdVVM = CreateProgressDialogViewViewModel(ownerWindow, title, initialMessage, cancellationTokenSource, progressReporter, dialogSettings);
                var dialog = createdVVM.View;

                // Runs a task in background thread and shows progress dialog
                // This redundant description is to catch exceptions
                T result = default;
                Exception caughtException = null;
                var cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
                var runTask = Task.Run(() =>
                {
                    try
                    {
                        result = function(progressReporter, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        caughtException = ex;
                    }
                    finally
                    {
                        progressReporter.OnProgressCompleted();
                    }
                }, cancellationToken);

                dialog.ShowDialog();
                runTask.GetAwaiter().GetResult(); // To get result or catch exceptions

                if (caughtException is OperationCanceledException)
                {
                    return new ProgressDialogResult<T>(true, default);
                }
                else if (caughtException != null)
                {
                    throw caughtException;
                }

                return new ProgressDialogResult<T>(false, result);
            }
        }

        #endregion

        #region ShowAsync

        /// <inheritdoc />
        public async Task<ProgressDialogResult> ShowAsync(string title, string initialMessage, Func<Task> task, ProgressDialogSettings dialogSettings)
        {
            return await this.ShowAsync(Application.Current?.MainWindow, title, initialMessage, task, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult> ShowAsync(string title, string initialMessage, Func<IProgress<ProgressReport>, Task> task, ProgressDialogSettings dialogSettings)
        {
            return await this.ShowAsync(Application.Current?.MainWindow, title, initialMessage, task, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult> ShowAsync(string title, string initialMessage, Func<IProgress<ProgressReport>, CancellationToken, Task> task, bool isCancellable, ProgressDialogSettings dialogSettings)
        {
            return await this.ShowAsync(Application.Current?.MainWindow, title, initialMessage, task, isCancellable, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult> ShowAsync(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<Task> task,
            ProgressDialogSettings dialogSettings)
        {
            var wrappedAction = new Func<IProgress<ProgressReport>, CancellationToken, Task>(
                (p, c) => task());
            var settings = dialogSettings ?? new ProgressDialogSettings();
            settings.IsPercentVisible = false;
            return await this.ShowAsync(ownerWindow, title, initialMessage, wrappedAction, false, settings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult> ShowAsync(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, Task> task,
            ProgressDialogSettings dialogSettings)
        {
            var wrappedAction = new Func<IProgress<ProgressReport>, CancellationToken, Task>(
                (progress, _) => task(progress));
            return await this.ShowAsync(ownerWindow, title, initialMessage, wrappedAction, false, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult> ShowAsync(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, CancellationToken, Task> task,
            bool isCancellable,
            ProgressDialogSettings dialogSettings)
        {
            using (var cancellationTokenSource = isCancellable ? new CancellationTokenSource() : null)
            {
                // Prepares dialog and progress reporter
                var progressReporter = new ProgressReporter();
                var createdVVM = CreateProgressDialogViewViewModel(ownerWindow, title, initialMessage, cancellationTokenSource, progressReporter, dialogSettings);
                var dialog = createdVVM.View;

                // Runs a task in background thread and shows progress dialog
                // This redundant description is to catch exceptions
                Exception caughtException = null;
                var cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
                var runTask = Task.Run(async () =>
                {
                    try
                    {
                        await task(progressReporter, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        caughtException = ex;
                    }
                    finally
                    {
                        progressReporter.OnProgressCompleted();
                    }
                }, cancellationToken);

                dialog.ShowDialog();
                await runTask; // To get result or catch exceptions

                if (caughtException is OperationCanceledException)
                {
                    return new ProgressDialogResult(true);
                }
                else if (caughtException != null)
                {
                    throw caughtException;
                }

                return new ProgressDialogResult(false);
            }
        }

        #endregion

        #region ShowAsync<T>

        /// <inheritdoc />
        public async Task<ProgressDialogResult<T>> ShowAsync<T>(string title, string initialMessage, Func<Task<T>> task, ProgressDialogSettings dialogSettings)
        {
            return await this.ShowAsync(Application.Current?.MainWindow, title, initialMessage, task, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult<T>> ShowAsync<T>(string title, string initialMessage, Func<IProgress<ProgressReport>, Task<T>> task, ProgressDialogSettings dialogSettings)
        {
            return await this.ShowAsync(Application.Current?.MainWindow, title, initialMessage, task, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult<T>> ShowAsync<T>(string title, string initialMessage, Func<IProgress<ProgressReport>, CancellationToken, Task<T>> task, bool isCancellable, ProgressDialogSettings dialogSettings)
        {
            return await this.ShowAsync(Application.Current?.MainWindow, title, initialMessage, task, isCancellable, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult<T>> ShowAsync<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<Task<T>> task,
            ProgressDialogSettings dialogSettings)
        {
            var wrappedFunction = new Func<IProgress<ProgressReport>, CancellationToken, Task<T>>(
                (p, c) => task());
            var settings = dialogSettings ?? new ProgressDialogSettings();
            settings.IsPercentVisible = false;
            return await this.ShowAsync(ownerWindow, title, initialMessage, wrappedFunction, false, settings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult<T>> ShowAsync<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, Task<T>> task,
            ProgressDialogSettings dialogSettings)
        {
            var wrappedFunction = new Func<IProgress<ProgressReport>, CancellationToken, Task<T>>(
                (progress, _) => task(progress));
            return await this.ShowAsync(ownerWindow, title, initialMessage, wrappedFunction, false, dialogSettings);
        }

        /// <inheritdoc />
        public async Task<ProgressDialogResult<T>> ShowAsync<T>(
            Window ownerWindow,
            string title,
            string initialMessage,
            Func<IProgress<ProgressReport>, CancellationToken, Task<T>> task,
            bool isCancellable,
            ProgressDialogSettings dialogSettings)
        {
            using (var cancellationTokenSource = isCancellable ? new CancellationTokenSource() : null)
            {
                // Prepares dialog and progress reporter
                var progressReporter = new ProgressReporter();
                var createdVVM = CreateProgressDialogViewViewModel(ownerWindow, title, initialMessage, cancellationTokenSource, progressReporter, dialogSettings);
                var dialog = createdVVM.View;

                // Runs a task in background thread and shows progress dialog
                // This redundant description is to catch exceptions
                T result = default;
                Exception caughtException = null;
                var cancellationToken = cancellationTokenSource?.Token ?? CancellationToken.None;
                var runTask = Task.Run(async () =>
                {
                    try
                    {
                       result = await task(progressReporter, cancellationToken);
                    }
                    catch (Exception ex)
                    {
                        caughtException = ex;
                    }
                    finally
                    {
                        progressReporter.OnProgressCompleted();
                    }
                }, cancellationToken);

                dialog.ShowDialog();
                await runTask; // To get result or catch exceptions

                if (caughtException is OperationCanceledException)
                {
                    return new ProgressDialogResult<T>(true, default);
                }
                else if (caughtException != null)
                {
                    throw caughtException;
                }

                return new ProgressDialogResult<T>(false, result);
            }
        }

        #endregion

        #endregion

        #region Private Methods

        private static ViewViewModel<ProgressDialogView, ProgressDialogViewModel> CreateProgressDialogViewViewModel(
            Window ownerWindow,
            string title,
            string initialMessage,
            CancellationTokenSource tokenSource,
            ProgressReporter progressReporter,
            ProgressDialogSettings dialogSettings)
        {
            var dialogOwner = ownerWindow ?? Application.Current?.MainWindow;
            var viewModel = new ProgressDialogViewModel(progressReporter, tokenSource)
            {
                Title = title,
                ProgressMessage = initialMessage,
                IsPercentVisible = dialogSettings?.IsPercentVisible ?? true,
            };
            var view = new ProgressDialogView
            {
                Owner = dialogOwner,
                WindowStartupLocation = dialogOwner is null ? WindowStartupLocation.CenterScreen : WindowStartupLocation.CenterOwner,
                DataContext = viewModel,
            };
            return new ViewViewModel<ProgressDialogView, ProgressDialogViewModel>(view, viewModel);
        }

        #endregion
    }
}
