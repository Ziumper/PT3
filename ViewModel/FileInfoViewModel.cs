using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PT3.ViewModel
{
    public class FileInfoViewModel: FileSystemInfoViewModel
    {
        private Dictionary<string, string> imageSource = new Dictionary<string, string>();

        public FileInfoViewModel(ViewModelBase owner) : base(owner)
        {
            imageSource.Add(".txt", "Resources/icon-txt.png");
            imageSource.Add(".pdf", "Resources/icon-pdf.png");

            OpenFileCommand = new RelayCommand(OnOpenFileCommand, CanExecuteOnOpenFileCommand);
        }

        public ICommand OpenFileCommand { get; private set; }
      
        public new string ImageSource { 
            get 
            {
                string extension =  base.Model.Extension;
                string source;

                imageSource.TryGetValue(extension, out source);
                if (source == null) return "Resources/icon-file.png";

                return source;

            } private set { }  }

        //public FileInfoViewModel()
        //{

        //}

        private bool CanExecuteOnOpenFileCommand(object obj)
        {
            return true;
        }

        private void OnOpenFileCommand(object obj)
        {
           throw new NotImplementedException();
        }
    }
}
