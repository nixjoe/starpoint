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
            cb_operation_actionType.ItemsSource = new string[] { "Discrete", "Continuous" };
            cb_operation_trigger.ItemsSource = new string[] { "Auto", "Semiauto","Passive" };
            cb_effect_type.ItemsSource = new string[] { "Property", "Resource", "Physical", "Audio", "Visual", "Object" };
            cb_effect_assignment.ItemsSource = new string[] { "Equals","Additive","Subractive","Multiplicative" };
            cb_property_type.ItemsSource = new string[] { "Container", "Integer", "Real", "Enum" };
            cb_requirement_comparison.ItemsSource = new string[] { "=", "<", ">", "<=", ">=", "!=" };
            cb_requirement_type.ItemsSource = new string[] { "Resource", "Property" };
            cb_effect_audioMode.ItemsSource = new string[] {"Play Once","Loop"};
            cb_effect_physicalType.ItemsSource = new string[] { "AbsoluteForce", "RelativeForce", "Torque" };
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

        private void tc_opProp_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_object.SelectedIndex = tc_opProp.SelectedIndex;
        }

        private void tc_object_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_opProp.SelectedIndex = tc_object.SelectedIndex;
        }

        private void mi_effects_new_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_effects_copy_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_effects_delete_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_effects_sort_Click(object sender, RoutedEventArgs e) {

        }

        private void cb_operation_actionType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if ((string)cb_operation_actionType.SelectedItem == "Continuous") {
                cb_operation_trigger.ItemsSource = new string[] { "Auto", "Passive" };
                cb_operation_trigger.Items.Refresh();
                tb_operationCooldown.Visibility = Visibility.Collapsed;
                tb_operationCooldownLabel.Visibility = Visibility.Collapsed;
            } else {
                cb_operation_trigger.ItemsSource = new string[] { "Auto", "Semiauto", "Passive" };
                cb_operation_trigger.Items.Refresh();
                tb_operationCooldown.Visibility = Visibility.Visible;
                tb_operationCooldownLabel.Visibility = Visibility.Visible;
            }
        }

        private void tc_operation_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_reqEff.SelectedIndex = tc_operation.SelectedIndex;
        }

        private void tc_reqEff_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_operation.SelectedIndex = tc_reqEff.SelectedIndex;
        }

        private void mi_requirements_new_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_requirements_copy_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_requirements_delete_Click(object sender, RoutedEventArgs e) {

        }

        private void mi_requirements_sort_Click(object sender, RoutedEventArgs e) {

        }

        private void lv_requirementList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void cb_requirement_type_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if ((string)cb_requirement_type.SelectedItem == "Resource") {
                tb_requirement_resource.Visibility = Visibility.Visible;
                cb_requirement_property.Visibility = Visibility.Collapsed;
                tb_requirement_resourceLabel.Visibility = Visibility.Visible;
                tb_requirement_propertyLabel.Visibility = Visibility.Collapsed;
            } else {
                tb_requirement_resource.Visibility = Visibility.Collapsed;
                cb_requirement_property.Visibility = Visibility.Visible;
                tb_requirement_resourceLabel.Visibility = Visibility.Collapsed;
                tb_requirement_propertyLabel.Visibility = Visibility.Visible;
            }
        }

        private void cb_effect_type_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tb_effect_propertyLabel.Visibility = Visibility.Collapsed;
            tb_effect_assignmentLabel.Visibility = Visibility.Collapsed;
            tb_effect_resourceLabel.Visibility = Visibility.Collapsed;
            tb_effect_physicalTypeLabel.Visibility = Visibility.Collapsed;
            tb_effect_valueLabel.Visibility = Visibility.Collapsed;
            tb_effect_xValueLabel.Visibility = Visibility.Collapsed;
            tb_effect_yValueLabel.Visibility = Visibility.Collapsed;
            tb_effect_zValueLabel.Visibility = Visibility.Collapsed;
            tb_effect_audioClipLabel.Visibility = Visibility.Collapsed;
            tb_effect_audioModeLabel.Visibility = Visibility.Collapsed;
            tb_effect_visualLabel.Visibility = Visibility.Collapsed;
            tb_effect_objectLabel.Visibility = Visibility.Collapsed;
            cb_effect_property.Visibility = Visibility.Collapsed;
            cb_effect_assignment.Visibility = Visibility.Collapsed;
            tb_effect_resource.Visibility = Visibility.Collapsed;
            cb_effect_physicalType.Visibility = Visibility.Collapsed;
            tb_effect_value.Visibility = Visibility.Collapsed;
            tb_effect_xValue.Visibility = Visibility.Collapsed;
            tb_effect_yValue.Visibility = Visibility.Collapsed;
            tb_effect_zValue.Visibility = Visibility.Collapsed;
            tb_effect_audioClip.Visibility = Visibility.Collapsed;
            cb_effect_audioMode.Visibility = Visibility.Collapsed;
            tb_effect_visual.Visibility = Visibility.Collapsed;
            tb_effect_object.Visibility = Visibility.Collapsed;
            tb_effect_xPosLabel.Visibility = Visibility.Collapsed;
            tb_effect_yPosLabel.Visibility = Visibility.Collapsed;
            tb_effect_zPosLabel.Visibility = Visibility.Collapsed;
            tb_effect_xPos.Visibility = Visibility.Collapsed;
            tb_effect_yPos.Visibility = Visibility.Collapsed;
            tb_effect_zPos.Visibility = Visibility.Collapsed;
            tb_effect_xRotLabel.Visibility = Visibility.Collapsed;
            tb_effect_yRotLabel.Visibility = Visibility.Collapsed;
            tb_effect_zRotLabel.Visibility = Visibility.Collapsed;
            tb_effect_xRot.Visibility = Visibility.Collapsed;
            tb_effect_yRot.Visibility = Visibility.Collapsed;
            tb_effect_zRot.Visibility = Visibility.Collapsed;
            tb_effect_xVelocityLabel.Visibility = Visibility.Collapsed;
            tb_effect_yVelocityLabel.Visibility = Visibility.Collapsed;
            tb_effect_zVelocityLabel.Visibility = Visibility.Collapsed;
            tb_effect_xVelocity.Visibility = Visibility.Collapsed;
            tb_effect_yVelocity.Visibility = Visibility.Collapsed;
            tb_effect_zVelocity.Visibility = Visibility.Collapsed;
            tb_effect_xAngVelocityLabel.Visibility = Visibility.Collapsed;
            tb_effect_yAngVelocityLabel.Visibility = Visibility.Collapsed;
            tb_effect_zAngVelocityLabel.Visibility = Visibility.Collapsed;
            tb_effect_xAngVelocity.Visibility = Visibility.Collapsed;
            tb_effect_yAngVelocity.Visibility = Visibility.Collapsed;
            tb_effect_zAngVelocity.Visibility = Visibility.Collapsed;
            switch ((string)cb_effect_type.SelectedItem) {
                case "Property":
                    tb_effect_propertyLabel.Visibility = Visibility.Visible;
                    tb_effect_assignmentLabel.Visibility = Visibility.Visible;
                    cb_effect_property.Visibility = Visibility.Visible;
                    cb_effect_assignment.Visibility = Visibility.Visible;
                    tb_effect_valueLabel.Visibility = Visibility.Visible;
                    tb_effect_value.Visibility = Visibility.Visible;
                    break;
                case "Resource":
                    tb_effect_resourceLabel.Visibility = Visibility.Visible;
                    tb_effect_resource.Visibility = Visibility.Visible;
                    tb_effect_assignmentLabel.Visibility = Visibility.Visible;
                    cb_effect_assignment.Visibility = Visibility.Visible;
                    tb_effect_valueLabel.Visibility = Visibility.Visible;
                    tb_effect_value.Visibility = Visibility.Visible;
                    break;
                case "Physical":
                    tb_effect_physicalTypeLabel.Visibility = Visibility.Visible;
                    cb_effect_physicalType.Visibility = Visibility.Visible;
                    tb_effect_xValueLabel.Visibility = Visibility.Visible;
                    tb_effect_yValueLabel.Visibility = Visibility.Visible;
                    tb_effect_zValueLabel.Visibility = Visibility.Visible;
                    tb_effect_xValue.Visibility = Visibility.Visible;
                    tb_effect_yValue.Visibility = Visibility.Visible;
                    tb_effect_zValue.Visibility = Visibility.Visible;
                    break;
                case "Audio":
                    tb_effect_audioClipLabel.Visibility = Visibility.Visible;
                    tb_effect_audioModeLabel.Visibility = Visibility.Visible;
                    tb_effect_audioClip.Visibility = Visibility.Visible;
                    cb_effect_audioMode.Visibility = Visibility.Visible;
                    break;
                case "Visual":
                    tb_effect_visualLabel.Visibility = Visibility.Visible;
                    tb_effect_visual.Visibility = Visibility.Visible;
                    tb_effect_xPosLabel.Visibility = Visibility.Visible;
                    tb_effect_yPosLabel.Visibility = Visibility.Visible;
                    tb_effect_zPosLabel.Visibility = Visibility.Visible;
                    tb_effect_xPos.Visibility = Visibility.Visible;
                    tb_effect_yPos.Visibility = Visibility.Visible;
                    tb_effect_zPos.Visibility = Visibility.Visible;
                    break;
                case "Object":
                    tb_effect_object.Visibility = Visibility.Visible;
                    tb_effect_objectLabel.Visibility = Visibility.Visible;
                    tb_effect_xRotLabel.Visibility = Visibility.Visible;
                    tb_effect_yRotLabel.Visibility = Visibility.Visible;
                    tb_effect_zRotLabel.Visibility = Visibility.Visible;
                    tb_effect_xRot.Visibility = Visibility.Visible;
                    tb_effect_yRot.Visibility = Visibility.Visible;
                    tb_effect_zRot.Visibility = Visibility.Visible;
                    tb_effect_xVelocityLabel.Visibility = Visibility.Visible;
                    tb_effect_yVelocityLabel.Visibility = Visibility.Visible;
                    tb_effect_zVelocityLabel.Visibility = Visibility.Visible;
                    tb_effect_xVelocity.Visibility = Visibility.Visible;
                    tb_effect_yVelocity.Visibility = Visibility.Visible;
                    tb_effect_zVelocity.Visibility = Visibility.Visible;
                    tb_effect_xAngVelocityLabel.Visibility = Visibility.Visible;
                    tb_effect_yAngVelocityLabel.Visibility = Visibility.Visible;
                    tb_effect_zAngVelocityLabel.Visibility = Visibility.Visible;
                    tb_effect_xAngVelocity.Visibility = Visibility.Visible;
                    tb_effect_yAngVelocity.Visibility = Visibility.Visible;
                    tb_effect_zAngVelocity.Visibility = Visibility.Visible;
                    break;
            }
        }
    }

}
