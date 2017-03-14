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

namespace SOLM {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
            cb_operation_actionType.ItemsSource = new List<string>(new string[] { "Discrete", "Continuous" });
            cb_operation_actionType.Items.Refresh();
            cb_operation_trigger.ItemsSource = new List<string>(new string[] { "Auto", "Semiauto","Passive" });
            cb_operation_trigger.Items.Refresh();
            cb_property_type.ItemsSource = new List<string>(new string[] {"Resource","Physical","Audio","Visual","Object"});
            cb_property_type.Items.Refresh();
        }

        private void lv_operationsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void tb_objectName_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void tb_objectDryWeight_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_loadLibrary_Click(object sender, RoutedEventArgs e) {

        }

        private void tb_bundleName_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void tb_libraryName_TextChanged(object sender, TextChangedEventArgs e) {

        }

        private void mi_libraryView_new_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_libraryView_copy_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_libraryView_delete_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_libraryView_sort_Click(object sender, RoutedEventArgs e) {

        }

        private void lv_objectList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void mi_operations_new_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_operations_copy_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_operations_delete_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_operations_sort_Click(object sender, RoutedEventArgs e) {

        }

        private void lv_propertiesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void lv_effectList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void tc_opPropTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_objectTabControl.SelectedIndex = tc_opPropTabControl.SelectedIndex;
        }

        private void tc_objectTabControl_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_opPropTabControl.SelectedIndex = tc_objectTabControl.SelectedIndex;
        }

        private void mi_effects_new_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_effects_copy_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_effects_delete_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_effects_sort_Click(object sender, RoutedEventArgs e) {

        }
    }
}
