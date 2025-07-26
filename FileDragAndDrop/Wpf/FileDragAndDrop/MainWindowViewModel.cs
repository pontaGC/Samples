using System.Windows;
using System.Windows.Input;
using FileDragAndDrop.Mvvm;

namespace FileDragAndDrop
{
    internal class MainWindowViewModel : ViewModelBase
    {
        #region Backing stores

        private string filesAndFolders;
        private string files;
        private string folders;
        private IEnumerable<string> droppedFilesAndFolders;

        private readonly DelegateCommand dropFilesAndFolders2Command;
        private readonly DelegateCommand<IEnumerable<string>> dropFilesAndFoldersCommand;
        private readonly DelegateCommand<IEnumerable<string>> dropFilesCommand;
        private readonly DelegateCommand<IEnumerable<string>> dropFoldersCommand;

        #endregion

        #region Constructors

        public MainWindowViewModel()
        {
            this.dropFilesAndFoldersCommand = new DelegateCommand<IEnumerable<string>>(this.DropFilesAndFolders);
            this.dropFilesAndFolders2Command = new DelegateCommand(this.DropFilesAndFolders2);
            this.dropFilesCommand = new DelegateCommand<IEnumerable<string>>(this.DropFiles);
            this.dropFoldersCommand = new DelegateCommand<IEnumerable<string>>(this.DropFolders);
        }


        #endregion

        #region Properties

        public string FilesAndFolders
        {
            get => this.filesAndFolders;
            set => this.SetProperty(ref this.filesAndFolders, value);
        }

        public string Files
        {
            get => this.files;
            set => this.SetProperty(ref this.files, value);
        }

        public string Folders
        {
            get => this.folders;
            set => this.SetProperty(ref this.folders, value);
        }

        public IEnumerable<string> DroppedFilesAndFolders 
        {
            get => this.droppedFilesAndFolders;
            set => this.SetProperty(ref this.droppedFilesAndFolders, value);
        }

        public ICommand DropFilesAndFoldersCommand => this.dropFilesAndFoldersCommand;

        public ICommand DropFilesAndFolders2Command => this.dropFilesAndFolders2Command;

        public ICommand DropFilesCommand => this.dropFilesCommand;

        public ICommand DropFoldersCommand => this.dropFoldersCommand;

        #endregion

        #region Methods

        private void DropFilesAndFolders(IEnumerable<string> fileFolderPaths)
        {
            this.FilesAndFolders = WriteLine(fileFolderPaths);

            foreach(var paths in fileFolderPaths)
            {
                if (!this.FilesAndFolders.Contains(paths))
                {
                    MessageBox.Show("Files and folders Binding Error.");
                }
            }
        }

        private void DropFilesAndFolders2()
        {
            // Operation confirmation: OK
            this.FilesAndFolders = WriteLine(this.DroppedFilesAndFolders);
        }

        private void DropFiles(IEnumerable<string> filePaths)
        {
            this.Files = WriteLine(filePaths);
        }

        private void DropFolders(IEnumerable<string> folderPaths)
        {
            this.Folders = WriteLine(folderPaths);
        }

        private static string WriteLine(IEnumerable<string> source)
        {
            return string.Join(Environment.NewLine, source);
        }

        #endregion
    }
}
