using System.Windows;

namespace Behaviors
{
    /// <summary>
    /// Closing a window behavior.
    /// </summary>
    public sealed class CloseWindowBehavior
    {
        /// <summary>
        /// The window close dependency property.
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty
            = DependencyProperty.RegisterAttached("DialogResult",
                                                  typeof(bool?),
                                                  typeof(CloseWindowBehavior),
                                                  new UIPropertyMetadata(null, OnClose));

        /// <summary>
        /// Gets a value indicating whether closes the <see cref="Window"/>.
        /// </summary>
        /// <param name="target">The target <see cref="DependencyObject"/>.</param>
        /// <returns>The value value indicating whether closes the <see cref="Window"/>.</returns>
        public static bool? GetDialogResult(DependencyObject target)
        {
            return (bool?)target.GetValue(DialogResultProperty);
        }

        /// <summary>
        /// Sets a value indicating whether closes the <see cref="Window"/>.
        /// </summary>
        /// <param name="target">The target <see cref="DependencyObject"/>.</param>
        /// <param name="value">The setting value.</param>
        public static void SetDialogResult(DependencyObject target, bool? value)
        {
            target.SetValue(DialogResultProperty, value);
        }

        private static void OnClose(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            var newValue = (bool?)e.NewValue;
            if (sender is Window window && newValue.HasValue)
            {
                window.DialogResult = newValue;
                window.Close();
            }
        }
    }
}