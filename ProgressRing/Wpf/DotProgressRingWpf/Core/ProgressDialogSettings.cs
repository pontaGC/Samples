namespace Core.Dialogs
{
    /// <summary>
    /// The progress dialog option settings.
    /// </summary>
    public class ProgressDialogSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the percent is visible in the dialog.
        /// </summary>
        public bool IsPercentVisible { get; set; } = true;

        //ダイアログのスタイル系の設定追加が必要な場合はここにプロパティを追加する
    }
}
