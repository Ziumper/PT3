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
        protected FileSystemInfo? fileSystemInfo;
        private DateTime lastWriteTime;
        private string? caption;
        private string? imageSource;
        private string? extension;
        private long count;

        public FileSystemInfo? Model
        {
            get { return fileSystemInfo; }
            set { 
                if (fileSystemInfo != value && value != null)
                {
                    SetProperties(value);
                    
                    FileInfo fileInfo = new FileInfo(value.FullName);
                    Count = fileInfo.Length;

                    NotifyPropertyChanged();
                }
            }
        }

        public string? Extension {  get { return extension; } set
            {
                if(extension != value)
                {
                    extension = value;
                    NotifyPropertyChanged();
                }
            }
        }

        public string? ImageSource { get { return imageSource; } 
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

        public string? Caption
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

        public long Count
        {
            get { return count; }
            set
            {
                if(count != value)
                {
                    count = value;
                    NotifyPropertyChanged();
                }
            }
        }


        
        protected void SetProperties(FileSystemInfo model)
        {
            fileSystemInfo = model;
            LastWriteTime = model.LastWriteTime;
            Caption = model.Name;
            Extension = model.Extension;
        }
        
    }
}
