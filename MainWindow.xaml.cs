using PT3.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PT3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private FileExplorer fileExplorer;

        public MainWindow()
        {
            InitializeComponent();
            fileExplorer = new FileExplorer();
            DataContext = fileExplorer;
            fileExplorer.PropertyChanged += OnFileExplorerPropertyChanged;
        }

        private void OnOpenItemMenuClick(object sender, RoutedEventArgs args)
        {
            var dlg = new FolderBrowserDialog() { Description = "Select directory to open" };
            if (dlg.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                var path = dlg.SelectedPath;
                fileExplorer.OpenRoot(path);
                DataContext = fileExplorer;
            }
        }

        private void OnExitItemMenuClick(object sender, RoutedEventArgs args)
        {
            Close();
        }

        private void OnFileExplorerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(FileExplorer.Lang))
                CultureResources.ChangeCulture(CultureInfo.CurrentUICulture);
        }
    }
}
