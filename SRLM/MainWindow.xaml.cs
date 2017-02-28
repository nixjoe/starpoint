using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SRLM {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {

        public SRL library { get; set; }

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
        }
        public void Update() {
            lv_resourceList.ItemsSource = library.resourceList;

        }
        private void mi_libraryView_new_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_libraryView_copy_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_libraryView_delete_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {
            library = new SRL();
        }

        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {
            library.Save();
        }

        private void mi_loadLibrary_Click(object sender, RoutedEventArgs e) {

        }
    }
}
