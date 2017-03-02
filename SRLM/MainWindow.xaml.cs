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

        }
        private void mi_libraryView_new_Click(object sender, RoutedEventArgs e) {
            string name = "Resource " + (library.resourceList.Count + 1);
            library.AddResource(new StarpointResource(name, 0.0f));
            Update();
        }

        private void mi_libraryView_copy_Click(object sender, RoutedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                StarpointResource selected = lv_resourceList.SelectedItem as StarpointResource;
                library.AddResource(new StarpointResource(selected.name, selected.weight));
            }
            Update();
        }

        private void mi_libraryView_delete_Click(object sender, RoutedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                library.resourceList.Remove(lv_resourceList.SelectedItem as StarpointResource);
            }
            Update();
        }

        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {
            library = new SRL();
            Update();
        }

        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {
            if ((from StarpointResource s in library.resourceList select s.name).Count() != (from StarpointResource s in library.resourceList select s.name).Distinct().Count()) {
                MessageBox.Show("All Resources must have unique names!");
            } else {
                library.Save();
                Update();
            }
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

        private void lv_resourceList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                tb_resourceName.IsEnabled = true;
                tb_resourceWeight.IsEnabled = true;
                tb_resourceName.Text = (lv_resourceList.SelectedItem as StarpointResource).name;
                tb_resourceWeight.Text = (lv_resourceList.SelectedItem as StarpointResource).weight.ToString();
            } else {
                tb_resourceName.IsEnabled = false;
                tb_resourceWeight.IsEnabled = false;
                tb_resourceName.Text ="";
                tb_resourceWeight.Text = "";
            }
        }

        private void tb_resourceName_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                (lv_resourceList.SelectedItem as StarpointResource).name = tb_resourceName.Text;
                Update();
            }
        }

        private void tb_resourceWeight_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                float parsedVal;
                if (float.TryParse(tb_resourceWeight.Text, out parsedVal)) {
                    (lv_resourceList.SelectedItem as StarpointResource).weight = parsedVal;
                    Update();
                } else {
                    tb_resourceWeight.Text = (lv_resourceList.SelectedItem as StarpointResource).weight.ToString();
                    tb_resourceWeight.CaretIndex = tb_resourceWeight.Text.Length;
                }
            }
        }

        private void mi_libraryView_sort_Click(object sender, RoutedEventArgs e) {
            library.resourceList.Sort(delegate(StarpointResource x, StarpointResource y) {
                if (x.name == null && y.name == null)
                    return 0;
                else if (x.name == null)
                    return -1;
                else if (y.name == null)
                    return 1;
                else
                    return x.name.CompareTo(y.name);
            });
            Update();
        }
    }
}
