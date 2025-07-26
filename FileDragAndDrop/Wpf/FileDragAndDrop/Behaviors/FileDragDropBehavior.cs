using System.IO;
using System.Windows;
using System.Windows.Input;

using Microsoft.Xaml.Behaviors;

namespace FileDragAndDrop.Behaviors
{
    /// <summary>
    /// File drag and drop behavior.
    /// </summary>
    public class FileDragDropBehavior : Behavior<UIElement>
    {
        #region Properties

        public static readonly DependencyProperty DropFileAndFoldersProperty
            = DependencyProperty.Register(
                nameof(DropFileAndFolders),
                typeof(IEnumerable<string>),
                typeof(FileDragDropBehavior),
                new PropertyMetadata(Enumerable.Empty<string>()));

        public static readonly DependencyProperty DropFilesAndFoldersCommandProperty
            = DependencyProperty.Register(
                nameof(DropFilesAndFoldersCommand),
                typeof(ICommand),
                typeof(FileDragDropBehavior),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DropFilesCommandProperty
            = DependencyProperty.Register(
                nameof(DropFilesCommand),
                typeof(ICommand),
                typeof(FileDragDropBehavior),
                new PropertyMetadata(null));

        public static readonly DependencyProperty DropFoldersCommandProperty
            = DependencyProperty.Register(
                nameof(DropFoldersCommand),
                typeof(ICommand),
                typeof(FileDragDropBehavior),
                new PropertyMetadata(null));

        /// <summary>
        /// Gets or sets a collection of the paths of the dropped files and folders.
        /// </summary>
        public IEnumerable<string> DropFileAndFolders
        {
            get => (IEnumerable<string>)GetValue(DropFileAndFoldersProperty);
            set => SetValue(DropFileAndFoldersProperty, value);
        }

        /// <summary>
        /// Gets or sets the dropped files and folders command.
        /// </summary>
        public ICommand DropFilesAndFoldersCommand
        {
            get => (ICommand)GetValue(DropFilesAndFoldersCommandProperty);
            set => SetValue(DropFilesAndFoldersCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the dropped files command.
        /// </summary>
        public ICommand DropFilesCommand
        {
            get => (ICommand)GetValue(DropFilesCommandProperty);
            set => SetValue(DropFilesCommandProperty, value);
        }

        /// <summary>
        /// Gets or sets the dropped folders command.
        /// </summary>
        public ICommand DropFoldersCommand
        {
            get => (ICommand)GetValue(DropFoldersCommandProperty);
            set => SetValue(DropFoldersCommandProperty, value);
        }

        #endregion

        #region Protected Methods

        /// <inheritdoc />
        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.AllowDrop = true;
            AssociatedObject.PreviewDragOver += OnPreviewDragOver;
            AssociatedObject.Drop += OnDrop;
        }

        /// <inheritdoc />
        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.PreviewDragOver -= OnPreviewDragOver;
            AssociatedObject.Drop -= OnDrop;
        }

        #endregion

        #region Private Methods

        private void OnPreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
            }
            else
            {
                e.Effects = DragDropEffects.None;
            }

            e.Handled = true;
        }

        private void OnDrop(object sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                return;
            }

            var droppedAllPaths = e.Data.GetData(DataFormats.FileDrop) as string[];
            this.DropFileAndFolders = droppedAllPaths ?? Enumerable.Empty<string>();

            if (this.DropFilesAndFoldersCommand != null)
            {
                this.ExecuteDropFilesAndFoldersCommand(this.DropFileAndFolders);
            }

            if (this.DropFilesCommand != null)
            {
                var droppeedFiles = this.DropFileAndFolders.Where(File.Exists).ToArray();
                this.ExecuteDropFilesCommand(droppeedFiles);
            }

            if (this.DropFoldersCommand != null)
            {
                var droppedFolders = this.DropFileAndFolders.Where(path => !File.Exists(path)).ToArray();
                this.ExecuteDropFoldersCommand(droppedFolders);
            }
        }

        private void ExecuteDropFilesAndFoldersCommand(IEnumerable<string> droppedFiles)
        {
            if (this.DropFilesAndFoldersCommand.CanExecute(droppedFiles))
            {
                this.DropFilesAndFoldersCommand.Execute(droppedFiles);
            }
        }

        private void ExecuteDropFilesCommand(IEnumerable<string> droppedFiles)
        {
            if (this.DropFilesCommand.CanExecute(droppedFiles))
            {
                this.DropFilesCommand.Execute(droppedFiles);
            }
        }

        private void ExecuteDropFoldersCommand(IEnumerable<string> droppedFiles)
        {
            if (this.DropFoldersCommand.CanExecute(droppedFiles))
            {
                this.DropFoldersCommand.Execute(droppedFiles);
            }
        }

        #endregion
    }
}
