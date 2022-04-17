using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PT3.ViewModel
{
    public class FileExplorer : ViewModelBase
    {
        public ICommand OpenRootFolderCommand { get; private set; }
        public ICommand SortRootFolderCommand { get; private set; }
        public ICommand ExitCommand { get; private set; }
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


        private DirectoryInfoViewModel root;
        public DirectoryInfoViewModel Root { get => root;
            set {
                if (value != null) root = value;
            }
        }

        public FileExplorer()
        {
            NotifyPropertyChanged(nameof(Lang));
            root = new DirectoryInfoViewModel();
            OpenRootFolderCommand = new RelayCommand(OpenRootFolderExecute);
            SortRootFolderCommand = new RelayCommand(SortRootFolderExecute, CanExecuteSort);
            ExitCommand = new RelayCommand(ExitExecute);
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
        }

        private void OpenRoot(string path)
        {
            Root.Open(path);
        }

        private void SortRootFolderExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        private bool CanExecuteSort(object parameter)
        {
            return root.IsInitlized;
        }
    }
}
