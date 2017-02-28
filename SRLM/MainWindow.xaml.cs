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
            Update();
        }
        public void Update() {
            tb_libraryName.IsEnabled = library != null;
            tb_bundleName.IsEnabled = library != null;
            lv_resourceList.IsEnabled = library != null;
            mi_libraryView_delete.IsEnabled = library != null;
            mi_libraryView_new.IsEnabled = library != null;
            mi_libraryView_copy.IsEnabled = library != null;
            mi_saveLibrary.IsEnabled = library != null;
            tb_resourceName.IsEnabled = lv_resourceList.SelectedItem != null;
            tb_resourceWeight.IsEnabled = lv_resourceList.SelectedItem != null;
            if (library != null) {
                lv_resourceList.ItemsSource = library.resourceList;
                lv_resourceList.Items.Refresh();
            }
            if (lv_resourceList.SelectedItem != null) {
                tb_resourceName.Text = (lv_resourceList.SelectedItem as StarpointResource).name;
                tb_resourceWeight.Text = (lv_resourceList.SelectedItem as StarpointResource).weight.ToString();
            }

        }
        private void mi_libraryView_new_Click(object sender, RoutedEventArgs e) {
            string name = "Resource " + (library.resourceList.Count + 1);
            library.AddResource(new StarpointResource(name, 0.0f));
            Update();
        }

        private void mi_libraryView_copy_Click(object sender, RoutedEventArgs e) {
            Update();
        }

        private void mi_libraryView_delete_Click(object sender, RoutedEventArgs e) {
            Update();
        }

        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {
            library = new SRL();
            Update();
        }

        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {
            library.Save();
            Update();
        }

        private void mi_loadLibrary_Click(object sender, RoutedEventArgs e) {
            Update();
        }

        private void tb_libraryName_TextChanged(object sender, TextChangedEventArgs e) {
            if (library != null) {
                library.name = tb_libraryName.Text;
            }
            Update();
        }

        private void tb_bundleName_TextChanged(object sender, TextChangedEventArgs e) {
            if (library != null) {
                library.bundleName = tb_bundleName.Text;
            }
            Update();
        }
    }
}
