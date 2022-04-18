using PT3.Enum;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PT3.ViewModel
{
    public class DirectoryInfoViewModel : FileSystemInfoViewModel
    {
        private FileSystemWatcher? watcher;
        private string? path;
        private string imageSource = "Resources/FolderClose.png";
        private long count;

        public new FileSystemInfo? Model
        {
            get { return fileSystemInfo; }
            set
            {
                if (fileSystemInfo != value && value != null)
                {
                    SetProperties(value);
                    NotifyPropertyChanged();
                }
            }
        }

        public bool IsInitlized { 
            get 
            { 
                if(path == null) return false;

                if(path.Length == 0) return false; 

                return true;
            }  
        }

        public new string ImageSource { get => imageSource; private set { } }

        public ObservableCollection<FileSystemInfoViewModel> Items { get; private set; } = new ObservableCollection<FileSystemInfoViewModel>();
        public Exception? Exception { get; private set; }
        public new long Count
        {
            get { return count; }
            set {

                if(count != value)
                {
                    count = value; 
                    NotifyPropertyChanged();
                }
            }
        }

        public void OnFileSystemChanged(object sender, FileSystemEventArgs e)
        {
            //Handling for multithread execution
            Application.Current.Dispatcher.Invoke(() => OnFileSystemChanged(e));
        }

        public bool Open(string path)
        {
            this.path = path;
            bool result = false;
            try
            {
                Items.Clear();
                ReadCatalogs();
                ReadFiles();
                result = true;
            } catch (Exception ex)
            {
                Exception = ex;
            }

            if (result) InitlizeWatcher();

            return result;
        }

        public void Sort(SortingViewModel sortingViewModel)
        {
            bool isEmpty = IsInitlized;
            
            if (isEmpty) return;

            Sort(sortingViewModel.SortBy, sortingViewModel.Direction);
        }

        private void Sort(SortBy sortBy,Direction direction)
        {
            foreach (var item in this.Items)
            {
                
            }
        }

        private void ReadCatalogs()
        {
            foreach (var dirName in Directory.GetDirectories(path))
            {
                var dirInfo = new DirectoryInfo(dirName);
                DirectoryInfoViewModel itemViewModel = new DirectoryInfoViewModel();
                itemViewModel.Model = dirInfo;
                Items.Add(itemViewModel);

                //recurrecny load
                itemViewModel.Open(dirName);
            }
        }

        private void ReadFiles()
        {
            foreach (var fileName in Directory.GetFiles(path))
            {
                var fileInfo = new FileInfo(fileName);
                FileInfoViewModel itemViewModel = new FileInfoViewModel();
                itemViewModel.Model = fileInfo;
                Items.Add(itemViewModel);
            }
        }

        private void InitlizeWatcher()
        {
            watcher = new FileSystemWatcher(path);
            watcher.Created += OnFileSystemChanged;
            watcher.Renamed += OnFileSystemChanged;
            watcher.Deleted += OnFileSystemChanged;
            watcher.Changed += OnFileSystemChanged;

            watcher.Error += OnWatcherError;
            watcher.EnableRaisingEvents = true;

        }

        private void OnWatcherError(object sender, ErrorEventArgs e)
        {
            MessageBox.Show(e.ToString());
        }

        private void OnFileSystemChanged(FileSystemEventArgs e)
        {
            if (e.ChangeType == WatcherChangeTypes.Changed) return;
            if(path != null) Open(path);
        }

        
    }
}
