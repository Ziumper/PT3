using PT3.DialogWindow;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PT3.ViewModel
{
    public class FileExplorer : ViewModelBase
    {
        private DirectoryInfoViewModel root;
        private SortingViewModel sorting;

        public static readonly string[] TextFilesExtensions = new string[] { ".txt", ".ini", ".log" };
        public event EventHandler<FileInfoViewModel> OnOpenFileRequest;

        public ICommand OpenRootFolderCommand { get; private set; }
        public ICommand SortRootFolderCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
        public ICommand OpenFileCommand { get; private set; }

        public DirectoryInfoViewModel Root
        {
            get => root;
            set
            {
                if (value != null) root = value;
                NotifyPropertyChanged();
            }
        }

        public SortingViewModel Sorting
        {
            get { return sorting; }
            set { if (value != null) sorting = value; NotifyPropertyChanged(); }
        }

        public string Lang
        {
            get { return CultureInfo.CurrentUICulture.TwoLetterISOLanguageName; }
            set
            {
                if (value != null)
                    if (CultureInfo.CurrentUICulture.TwoLetterISOLanguageName != value)
                    {
                        CultureInfo.CurrentUICulture = new CultureInfo(value);
                        NotifyPropertyChanged();
                    }
            }
        }


        public FileExplorer()
        {
            NotifyPropertyChanged(nameof(Lang));

            root = new DirectoryInfoViewModel(this);
            NotifyPropertyChanged(nameof(Root));

            sorting = new SortingViewModel();
            sorting.SortBy = Enum.SortBy.Alphabetical;
            sorting.Direction = Enum.Direction.Ascending;
            NotifyPropertyChanged(nameof(Sorting));

            Sorting.PropertyChanged += OnSortingPropertyChanged;

            OpenRootFolderCommand = new RelayCommand(OpenRootFolderExecute);
            SortRootFolderCommand = new RelayCommand(SortRootFolderExecute, CanExecuteSort);
            ExitCommand = new RelayCommand(ExitExecute);

            OpenFileCommand = new RelayCommand(OnOpenFileCommand, OpenFileCanExecute);
        }

        public string GetFileContent(FileInfoViewModel viewModel)
        {
            var extension = viewModel.Extension?.ToLower();
            if (TextFilesExtensions.Contains(extension))
            {
                return GetTextFileContent(viewModel);
            }
            return "";
        }

        private string GetTextFileContent(FileInfoViewModel viewModel)
        {
            string result = "";
            
            using(var textReader = File.OpenText(viewModel.Model.FullName)) {
                result = textReader.ReadToEnd();
            }

            return result;
        }

        private void OnOpenFileCommand(object obj)
        {
            if (obj is not FileInfoViewModel) return;
            FileInfoViewModel viewModel = (FileInfoViewModel)obj;
            OnOpenFileRequest.Invoke(this, viewModel);
        }

        private bool OpenFileCanExecute(object parameter)
        {
            if (parameter is FileInfoViewModel viewModel)
            {
                var extension = viewModel.Extension?.ToLower();
                return TextFilesExtensions.Contains(extension);
            }
            return false;
        }

        private void OnSortingPropertyChanged(object? sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            Root.Sort(Sorting);
        }

        private void ExitExecute(object parameter)
        {
            if(parameter == null) return;

            if (parameter is not Window)
            {
                throw new ArgumentException("Not valid parameter passed into exit command");
            };

            Window window = (Window)parameter;
            window.Close();
        }

        private void OpenRootFolderExecute(object parameter)
        {
         
            var dlg = new System.Windows.Forms.FolderBrowserDialog() { Description = Strings.Directory_Description };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = dlg.SelectedPath;
                OpenRoot(path);
            }

            Root.Sort(Sorting);
        }

        private void OpenRoot(string path)
        {
            Root.Open(path);
        }

        private void SortRootFolderExecute(object parameter)
        {
            Window window = new SortingDialog(Sorting);
            window.ShowDialog();
        }

        private bool CanExecuteSort(object parameter)
        {
            return root.IsInitlized;
        }
    }
}
