using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT3.ViewModel
{
    public class FileSystemInfoViewModel : ViewModelBase
    {
        private FileSystemInfo fileSystemInfo;
        private DateTime lastWriteTime;
        private string caption;
        private string imageSource;

        public FileSystemInfo Model
        {
            get { return fileSystemInfo; }
            set { 
                if (fileSystemInfo != value)
                {
                    fileSystemInfo = value;
                    LastWriteTime = value.LastWriteTime;
                    Caption = value.Name;
                    NotifyPropertyChanged();
                }
            }
        }

        public string ImageSource { get { return imageSource; } 
            set 
            {
                if(imageSource != value)
                {
                    imageSource = value;
                    NotifyPropertyChanged();
                }
            } 
        }

        public DateTime LastWriteTime
        {
            get { return lastWriteTime; }
            set
            {
                if (lastWriteTime != value)
                {
                    lastWriteTime = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string Caption
        {
            get { return caption; }
            set 
            { 
                if(caption != value)
                {
                    caption = value; 
                    NotifyPropertyChanged();
                }
            }
        }
        
        
    }
}
