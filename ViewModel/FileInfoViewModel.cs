using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT3.ViewModel
{
    public class FileInfoViewModel: FileSystemInfoViewModel
    {
        private Dictionary<string, string> imageSource = new Dictionary<string, string>();
        public string ImageSource { 
            get 
            {
                string extension =  base.Model.Extension;
                string source;

                imageSource.TryGetValue(extension, out source);
                if (source == null) return "Resources/icon-file.png";

                return source;

            } private set { }  }

        public FileInfoViewModel()
        {
            imageSource.Add(".txt", "Resources/icon-txt.png");
            imageSource.Add(".pdf", "Resources/icon-pdf.png");
        }
    }
}
