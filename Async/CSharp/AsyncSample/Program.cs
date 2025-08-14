using System;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSample
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //
            // Normal execution
            //
            Console.WriteLine("Starting normal execution...");

            using (var cts = new CancellationTokenSource())
            {
                var result = ExecuteWithProgress(SampleFunction, cts.Token);
                Console.WriteLine(result); // Output: Completed Successfully
            }

            Console.WriteLine("\n");

            //
            // Throws exception
            //

            Console.WriteLine("Starting execution with exception...");

            try
            {
                using (var cts2 = new CancellationTokenSource())
                {
                    var resultWithException = ExecuteWithProgress((progress, token) =>
                    {
                        throw new InvalidOperationException("An error occurred during execution.");
                        return 0;
                    }, cts2.Token);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: {ex.Message}"); // Output: Caught exception: An error occurred during execution.
            }
        }

        private static string SampleFunction(IProgress<int> progress, CancellationToken token)
        {
            int percent = 0;
            while (percent < 100)
            {
                token.ThrowIfCancellationRequested();
                Thread.Sleep(100); // Simulate work
                percent += 10;
                progress.Report(percent);
            }

            return "Completed Successfully";
        }

        private static T ExecuteWithProgress<T>(Func<IProgress<int>, CancellationToken, T> function, CancellationToken cancellationToken)
        {
            // Runs a task in background thread and shows progress dialog
            // This redundant description is to catch exceptions
            T result = default;
            Exception caughtException = null;
            var runTask = Task.Run(() =>
            {
                try
                {
                    var sampleProgress = new Progress<int>(progress => Console.WriteLine($"Progress: {progress}%"));
                    result = function(sampleProgress, cancellationToken);
                }
                catch (Exception ex)
                {
                    caughtException = ex;
                }
            }, cancellationToken);

            Console.WriteLine("Do something else while waiting for the task to complete...");
            runTask.GetAwaiter().GetResult(); // To get result or catch exceptions

            if (caughtException is OperationCanceledException)
            {
                Console.WriteLine("Operation was cancelled.");
            }
            else if (caughtException != null)
            {
                Console.WriteLine($"An error occurred: {caughtException.Message}");
            }

            return result;
        }
    }
}
