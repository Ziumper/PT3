using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT3.ViewModel
{
    public class FileExplorer : ViewModelBase
    {
        public RelayCommand OpenRootFolderCommand { get; private set; }
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
        }

        public void OpenRoot(string path)
        {
            Root.Open(path);
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
    }
}
