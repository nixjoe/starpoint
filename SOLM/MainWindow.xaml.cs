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
        SOL sol;
        public MainWindow() {

            InitializeComponent();
            cb_operation_actionType.ItemsSource = new string[] { "Discrete", "Continuous" };
            cb_operation_trigger.ItemsSource = new string[] { "Auto", "Semiauto", "Passive" };
            cb_effect_type.ItemsSource = new string[] { "Property", "Resource", "Physical", "Audio", "Visual", "Object" };
            cb_effect_assignment.ItemsSource = new string[] { "Equals", "Additive", "Subractive", "Multiplicative" };
            cb_property_type.ItemsSource = new string[] { "Container", "Integer", "Real", "Enum" };
            cb_requirement_comparison.ItemsSource = new string[] { "=", "<", ">", "<=", ">=", "!=" };
            cb_requirement_type.ItemsSource = new string[] { "Resource", "Property" };
            cb_effect_audioMode.ItemsSource = new string[] { "Play Once", "Loop" };
            cb_effect_physicalType.ItemsSource = new string[] { "AbsoluteForce", "RelativeForce", "Torque" };
            cb_collider_shape.ItemsSource = new string[] { "Cube", "Sphere", "Capsule", "Cylinder" };

            sol = new SOL();
            lv_objectList.ItemsSource = sol.objects;
            Refresh();
        }
        private void Refresh() {
            lv_objectList.Items.Refresh();
            lv_operationsList.Items.Refresh();
            lv_propertiesList.Items.Refresh();
            lv_requirementList.Items.Refresh();
            lv_effectList.Items.Refresh();
            lv_collidersList.Items.Refresh();
        }
        private void lv_operationsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_loadLibrary_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_objects_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_operations_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void lv_propertiesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void lv_effectList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void mi_effects_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_requirements_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void lv_requirementList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void tb_objectModel_TextChanged(object sender, TextChangedEventArgs e) {

        }
        private void lv_collidersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void mi_properties_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_colliders_sort_Click(object sender, RoutedEventArgs e) {

        }
        #region completed and stable
        private void NewItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.ItemsSource != null) {
                (lv_objectList.ItemsSource as List<StarpointObject>).Add(new StarpointObject());
                lv_objectList.Items.Refresh();
            } else if (listView == lv_operationsList && lv_operationsList.ItemsSource != null) {
                (lv_operationsList.ItemsSource as List<Operation>).Add(new Operation());
                lv_operationsList.Items.Refresh();
            } else if (listView == lv_propertiesList && lv_propertiesList.ItemsSource != null) {
                (lv_propertiesList.ItemsSource as List<Property>).Add(new ContainerProperty());
                lv_propertiesList.Items.Refresh();
            } else if (listView == lv_requirementList && lv_requirementList.ItemsSource != null) {
                (lv_requirementList.ItemsSource as List<Requirement>).Add(new Requirement());
                lv_requirementList.Items.Refresh();
            } else if (listView == lv_effectList && lv_effectList.ItemsSource != null) {
                (lv_effectList.ItemsSource as List<Effect>).Add(new Effect());
                lv_effectList.Items.Refresh();
            } else if (listView == lv_collidersList && lv_collidersList.ItemsSource != null) {
                (lv_collidersList.ItemsSource as List<StarpointCollider>).Add(new Cube());
                lv_effectList.Items.Refresh();
            }
        }
        private void CopyItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.SelectedIndex != -1) {
                (lv_objectList.ItemsSource as List<StarpointObject>).Add(new StarpointObject(lv_objectList.SelectedItem as StarpointObject));
                lv_objectList.Items.Refresh();
            } else if (listView == lv_operationsList && lv_operationsList.SelectedIndex != -1) {
                (lv_operationsList.ItemsSource as List<Operation>).Add(new Operation(lv_operationsList.SelectedItem as Operation));
                lv_operationsList.Items.Refresh();
            } else if (listView == lv_propertiesList && lv_propertiesList.SelectedIndex != -1) {
                Property p = lv_propertiesList.SelectedItem as Property;
                if (p is ContainerProperty) {
                    (lv_propertiesList.ItemsSource as List<Property>).Add(new ContainerProperty(p as ContainerProperty));
                } else if (p is IntegerProperty) {
                    (lv_propertiesList.ItemsSource as List<Property>).Add(new IntegerProperty(p as IntegerProperty));
                } else if (p is RealProperty) {
                    (lv_propertiesList.ItemsSource as List<Property>).Add(new RealProperty(p as RealProperty));
                } else if (p is EnumProperty) {
                    (lv_propertiesList.ItemsSource as List<Property>).Add(new EnumProperty(p as EnumProperty));
                }
                lv_propertiesList.Items.Refresh();
            } else if (listView == lv_requirementList && lv_requirementList.SelectedIndex != -1) {
                (lv_requirementList.ItemsSource as List<Requirement>).Add(new Requirement(lv_requirementList.SelectedItem as Requirement));
                lv_requirementList.Items.Refresh();
            } else if (listView == lv_effectList && lv_effectList.SelectedIndex != -1) {
                (lv_effectList.ItemsSource as List<Effect>).Add(new Effect(lv_effectList.SelectedItem as Effect));
                lv_effectList.Items.Refresh();
            } else if (listView == lv_collidersList && lv_collidersList.SelectedIndex != -1) {
                StarpointCollider sc = lv_collidersList.SelectedItem as StarpointCollider;
                if (sc is Cube) {
                    (lv_collidersList.ItemsSource as List<StarpointCollider>).Add(new Cube(sc as Cube));
                } else if (sc is Sphere) {
                    (lv_collidersList.ItemsSource as List<StarpointCollider>).Add(new Sphere(sc as Sphere));
                } else if (sc is Capsule) {
                    (lv_collidersList.ItemsSource as List<StarpointCollider>).Add(new Capsule(sc as Capsule));
                } else {
                    (lv_collidersList.ItemsSource as List<StarpointCollider>).Add(new Cylinder(sc as Cylinder));
                }

                lv_collidersList.Items.Refresh();
            }
        }
        private void DeleteItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.SelectedIndex != -1) {
                (lv_objectList.ItemsSource as List<StarpointObject>).RemoveAt(lv_objectList.SelectedIndex);
                lv_objectList.Items.Refresh();
            } else if (listView == lv_operationsList && lv_operationsList.SelectedIndex != -1) {
                (lv_operationsList.ItemsSource as List<Operation>).RemoveAt(lv_operationsList.SelectedIndex);
                lv_operationsList.Items.Refresh();
            } else if (listView == lv_propertiesList && lv_propertiesList.SelectedIndex != -1) {
                (lv_propertiesList.ItemsSource as List<Property>).RemoveAt(lv_propertiesList.SelectedIndex);
                lv_propertiesList.Items.Refresh();
            } else if (listView == lv_requirementList && lv_requirementList.SelectedIndex != -1) {
                (lv_requirementList.ItemsSource as List<Requirement>).RemoveAt(lv_requirementList.SelectedIndex);
                lv_requirementList.Items.Refresh();
            } else if (listView == lv_effectList && lv_effectList.SelectedIndex != -1) {
                (lv_effectList.ItemsSource as List<Effect>).RemoveAt(lv_effectList.SelectedIndex);
                lv_effectList.Items.Refresh();
            } else if (listView == lv_collidersList && lv_effectList.SelectedIndex != -1) {
                (lv_collidersList.ItemsSource as List<Effect>).RemoveAt(lv_collidersList.SelectedIndex);
                lv_collidersList.Items.Refresh();
            }
        }
        private void tb_bundleName_TextChanged(object sender, TextChangedEventArgs e) {
            SanitizeString(tb_bundleName);
            sol.bundle = tb_bundleName.Text;
        }
        private void tb_libraryName_TextChanged(object sender, TextChangedEventArgs e) {
            SanitizeString(tb_libraryName);
            sol.bundle = tb_libraryName.Text;
        }
        private void mi_colliders_new_Click(object sender, RoutedEventArgs e) {
            NewItem(lv_collidersList);
        }
        private void mi_colliders_copy_Click(object sender, RoutedEventArgs e) {
            CopyItem(lv_collidersList);
        }
        private void mi_colliders_delete_Click(object sender, RoutedEventArgs e) {
            DeleteItem(lv_collidersList);
        }
        private void mi_properties_new_Click(object sender, RoutedEventArgs e) {
            NewItem(lv_propertiesList);

        }
        private void mi_properties_copy_Click(object sender, RoutedEventArgs e) {
            CopyItem(lv_propertiesList);
        }
        private void mi_properties_delete_Click(object sender, RoutedEventArgs e) {
            DeleteItem(lv_propertiesList);
        }
        private void cb_collider_shape_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tb_collider_radius.Visibility = Visibility.Collapsed;
            tb_collider_radiusLabel.Visibility = Visibility.Collapsed;
            tb_collider_xSizeLabel.Visibility = Visibility.Collapsed;
            tb_collider_xSize.Visibility = Visibility.Collapsed;
            tb_collider_ySizeLabel.Visibility = Visibility.Collapsed;
            tb_collider_ySize.Visibility = Visibility.Collapsed;
            tb_collider_zSizeLabel.Visibility = Visibility.Collapsed;
            tb_collider_zSize.Visibility = Visibility.Collapsed;
            if ((string)cb_collider_shape.SelectedItem == "Cube") {
                tb_collider_xSizeLabel.Visibility = Visibility.Visible;
                tb_collider_xSize.Visibility = Visibility.Visible;
                tb_collider_ySizeLabel.Visibility = Visibility.Visible;
                tb_collider_ySize.Visibility = Visibility.Visible;
                tb_collider_zSizeLabel.Visibility = Visibility.Visible;
                tb_collider_zSize.Visibility = Visibility.Visible;
            } else if ((string)cb_collider_shape.SelectedItem == "Sphere") {
                tb_collider_radius.Visibility = Visibility.Visible;
                tb_collider_radiusLabel.Visibility = Visibility.Visible;
            }
            if ((string)cb_collider_shape.SelectedItem == "Capsule") {
                tb_collider_radius.Visibility = Visibility.Visible;
                tb_collider_radiusLabel.Visibility = Visibility.Visible;
                tb_collider_ySizeLabel.Visibility = Visibility.Visible;
                tb_collider_ySize.Visibility = Visibility.Visible;
            }
            if ((string)cb_collider_shape.SelectedItem == "Cylinder") {
                tb_collider_radius.Visibility = Visibility.Visible;
                tb_collider_radiusLabel.Visibility = Visibility.Visible;
                tb_collider_ySizeLabel.Visibility = Visibility.Visible;
                tb_collider_ySize.Visibility = Visibility.Visible;
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
        private void tc_object_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_opProp.SelectedIndex = tc_object.SelectedIndex;
        }
        private void tc_operation_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_reqEff.SelectedIndex = tc_operation.SelectedIndex;
        }
        private void tc_reqEff_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_operation.SelectedIndex = tc_reqEff.SelectedIndex;
        }
        private void mi_requirements_new_Click(object sender, RoutedEventArgs e) {
            NewItem(lv_requirementList);
        }
        private void mi_requirements_copy_Click(object sender, RoutedEventArgs e) {
            CopyItem(lv_requirementList);
        }
        private void mi_requirements_delete_Click(object sender, RoutedEventArgs e) {
            DeleteItem(lv_requirementList);
        }
        private void tc_opProp_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tc_object.SelectedIndex = tc_opProp.SelectedIndex;
        }
        private void mi_effects_new_Click(object sender, RoutedEventArgs e) {
            NewItem(lv_effectList);
        }
        private void mi_effects_copy_Click(object sender, RoutedEventArgs e) {
            CopyItem(lv_effectList);
        }
        private void mi_effects_delete_Click(object sender, RoutedEventArgs e) {
            DeleteItem(lv_effectList);
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
        private void mi_operations_new_Click(object sender, RoutedEventArgs e) {
            NewItem(lv_operationsList);
        }
        private void mi_operations_copy_Click(object sender, RoutedEventArgs e) {
            CopyItem(lv_operationsList);
        }
        private void mi_operations_delete_Click(object sender, RoutedEventArgs e) {
            DeleteItem(lv_operationsList);
        }
        private void lv_objectList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tb_objectDryWeight.Background = Brushes.White;
            if (lv_objectList.SelectedItem != null) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                tb_objectName.Text = so.name;
                tb_objectDryWeight.Text = so.dryWeight.ToString();
            }
        }
        private void tb_objectName_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                SanitizeString(tb_objectName);
                so.name = tb_objectName.Text;
                Refresh();
            }
        }
        private void mi_objects_new_Click(object sender, RoutedEventArgs e) {
            NewItem(lv_objectList);
        }
        private void mi_objects_copy_Click(object sender, RoutedEventArgs e) {
            CopyItem(lv_objectList);
        }
        private void mi_objects_delete_Click(object sender, RoutedEventArgs e) {
            DeleteItem(lv_objectList);
        }
        private void SanitizeString(TextBox textbox) {
            textbox.Text = new string((from c in textbox.Text where !char.IsWhiteSpace(c) select c).ToArray());
        }
        private float? SanitizeFloat(TextBox textbox) {
            float value;
            if (float.TryParse(textbox.Text, out value)) {
                textbox.Text = value.ToString();
                textbox.Background = Brushes.White;
                return value;
            } else {
                textbox.Background = Brushes.Red;
                return null;
            }
        }
        private void tb_objectDryWeight_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                float? value = SanitizeFloat(tb_objectDryWeight);
                if (value.HasValue) {
                    so.dryWeight = (float)value;
                }
            }
        }
        #endregion

        private void b_loadModel_Click(object sender, RoutedEventArgs e) {

        }
    }

}
