using System;
using System.Collections.Generic;
using System.IO;
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
//using System.Windows.Shapes;
using System.Xml.Serialization;
using Microsoft.Win32;

namespace SOLM {

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        SOL sol;
        public MainWindow() {
            InitializeComponent();
            cb_operation_actionType.ItemsSource = new List<string>(new string[] { "Discrete", "Continuous" });
            cb_operation_trigger.ItemsSource = new List<string>(new string[] { "Auto", "Semiauto", "Passive" });
            cb_effect_type.ItemsSource = new List<string>(new string[] { "Property", "Resource", "Physical", "Audio", "Visual", "Object" });
            cb_effect_assignment.ItemsSource = new List<string>(new string[] { "Equals", "Additive", "Subractive", "Multiplicative" });
            cb_property_type.ItemsSource = new List<string>(new string[] { "Container", "Integer", "Real", "Enum" });
            cb_requirement_comparison.ItemsSource = new List<string>(new string[] { "=", "<", ">", "<=", ">=", "!=" });
            cb_requirement_type.ItemsSource = new List<string>(new string[] { "Resource", "Property" });
            cb_effect_audioMode.ItemsSource = new List<string>(new string[] { "Play Once", "Loop" });
            cb_effect_physicalType.ItemsSource = new List<string>(new string[] { "AbsoluteForce", "RelativeForce", "Torque" });
            cb_collider_shape.ItemsSource = new List<string>(new string[] { "Cube", "Sphere", "Capsule", "Cylinder" });
            Directory.CreateDirectory(Environment.CurrentDirectory + @"\Created Libraries");
            sol = new SOL();
            lv_objectList.ItemsSource = sol.objects;
            Refresh();
        }
        private void Refresh() {
            int lv_propertiesList_selectedIndex = lv_propertiesList.SelectedIndex;
            tb_libraryName.Text = sol.name;
            tb_bundleName.Text = sol.bundle;
            lv_objectList.Items.Refresh();
            lv_operationsList.Items.Refresh();
            lv_propertiesList.Items.Refresh();
            lv_requirementList.Items.Refresh();
            lv_effectList.Items.Refresh();
            lv_collidersList.Items.Refresh();
            if (lv_propertiesList.SelectedIndex <= lv_propertiesList.Items.Count) {
                lv_propertiesList.SelectedIndex = lv_propertiesList_selectedIndex;
            }
        }
        private void mi_objects_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_operations_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void lv_propertiesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                tb_property_name.Text = p.name;
                if (p is ContainerProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Container");
                    tb_property_uBound.Text = (p as ContainerProperty).uBound.ToString();
                    tb_property_containerResource.Text = (p as ContainerProperty).resource;
                } else if (p is IntegerProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Integer");
                    tb_property_uBound.Text = (p as IntegerProperty).uBound.ToString();
                    tb_property_lBound.Text = (p as IntegerProperty).lBound.ToString();
                } else if (p is RealProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Real");
                    tb_property_uBound.Text = (p as RealProperty).uBound.ToString();
                    tb_property_lBound.Text = (p as RealProperty).lBound.ToString();
                } else if (p is EnumProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Enum");
                    dg_property_enumValues.ItemsSource = (p as EnumProperty).enums;
                    dg_property_enumValues.Items.Refresh();
                }
                tb_property_description.Text = p.description;
                cb_property_visible.IsChecked = p.visible;
            }
        }
        private void lv_effectList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void mi_effects_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_requirements_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void lv_requirementList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }

        private void lv_collidersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {

        }
        private void mi_properties_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_colliders_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void b_loadModel_Click(object sender, RoutedEventArgs e) {

        }
        #region completed and stable
        private void NewItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.ItemsSource != null) {
                (lv_objectList.ItemsSource as List<StarpointObject>).Add(new StarpointObject());
            } else if (listView == lv_operationsList && lv_operationsList.ItemsSource != null) {
                (lv_operationsList.ItemsSource as List<Operation>).Add(new Operation());
            } else if (listView == lv_propertiesList && lv_propertiesList.ItemsSource != null) {
                (lv_propertiesList.ItemsSource as List<Property>).Add(new ContainerProperty());
            } else if (listView == lv_requirementList && lv_requirementList.ItemsSource != null) {
                (lv_requirementList.ItemsSource as List<Requirement>).Add(new Requirement());
            } else if (listView == lv_effectList && lv_effectList.ItemsSource != null) {
                (lv_effectList.ItemsSource as List<Effect>).Add(new Effect());
            } else if (listView == lv_collidersList && lv_collidersList.ItemsSource != null) {
                (lv_collidersList.ItemsSource as List<StarpointCollider>).Add(new Cube());
            }
            Refresh();
        }
        private void CopyItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.SelectedIndex != -1) {
                (lv_objectList.ItemsSource as List<StarpointObject>).Add(new StarpointObject(lv_objectList.SelectedItem as StarpointObject));
            } else if (listView == lv_operationsList && lv_operationsList.SelectedIndex != -1) {
                (lv_operationsList.ItemsSource as List<Operation>).Add(new Operation(lv_operationsList.SelectedItem as Operation));
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
            } else if (listView == lv_requirementList && lv_requirementList.SelectedIndex != -1) {
                (lv_requirementList.ItemsSource as List<Requirement>).Add(new Requirement(lv_requirementList.SelectedItem as Requirement));
            } else if (listView == lv_effectList && lv_effectList.SelectedIndex != -1) {
                (lv_effectList.ItemsSource as List<Effect>).Add(new Effect(lv_effectList.SelectedItem as Effect));
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
            }
            Refresh();
        }
        private void DeleteItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.SelectedIndex != -1) {
                (lv_objectList.ItemsSource as List<StarpointObject>).RemoveAt(lv_objectList.SelectedIndex);
            } else if (listView == lv_operationsList && lv_operationsList.SelectedIndex != -1) {
                (lv_operationsList.ItemsSource as List<Operation>).RemoveAt(lv_operationsList.SelectedIndex);
            } else if (listView == lv_propertiesList && lv_propertiesList.SelectedIndex != -1) {
                (lv_propertiesList.ItemsSource as List<Property>).RemoveAt(lv_propertiesList.SelectedIndex);
            } else if (listView == lv_requirementList && lv_requirementList.SelectedIndex != -1) {
                (lv_requirementList.ItemsSource as List<Requirement>).RemoveAt(lv_requirementList.SelectedIndex);
            } else if (listView == lv_effectList && lv_effectList.SelectedIndex != -1) {
                (lv_effectList.ItemsSource as List<Effect>).RemoveAt(lv_effectList.SelectedIndex);
            } else if (listView == lv_collidersList && lv_effectList.SelectedIndex != -1) {
                (lv_collidersList.ItemsSource as List<Effect>).RemoveAt(lv_collidersList.SelectedIndex);
            }
            Refresh();
        }
        private void tb_bundleName_TextChanged(object sender, TextChangedEventArgs e) {
            SanitizeString(tb_bundleName);
            sol.bundle = tb_bundleName.Text;
        }
        private void tb_libraryName_TextChanged(object sender, TextChangedEventArgs e) {
            SanitizeString(tb_libraryName);
            sol.name = tb_libraryName.Text;
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
        private void lv_operationsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                tb_operationName.Text = o.name;
                cb_operation_actionType.SelectedIndex = (cb_operation_actionType.ItemsSource as List<string>).IndexOf(o.action);
                cb_operation_trigger.SelectedIndex = (cb_operation_trigger.ItemsSource as List<string>).IndexOf(o.trigger);
                tb_operationCooldown.Text = o.cooldown.ToString();
                tb_operationDescription.Text = o.description;
                lv_requirementList.ItemsSource = o.requirements;
                lv_effectList.ItemsSource = o.effects;
                Refresh();
            }
        }
        private void cb_operation_actionType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                if ((string)cb_operation_actionType.SelectedItem == "Continuous") {
                    cb_operation_trigger.ItemsSource = new List<string>(new string[] { "Auto", "Passive" });
                    o.action = cb_operation_actionType.SelectedItem.ToString();
                    if (o.trigger == "Semiauto") {
                        cb_operation_trigger.SelectedIndex = 0;
                        //o.action = "Auto";
                    }
                    cb_operation_trigger.Items.Refresh();
                    tb_operationCooldown.Visibility = Visibility.Collapsed;
                    tb_operationCooldownLabel.Visibility = Visibility.Collapsed;
                } else {
                    cb_operation_trigger.ItemsSource = new List<string>(new string[] { "Auto", "Semiauto", "Passive" });
                    o.action = cb_operation_actionType.SelectedItem.ToString();
                    cb_operation_trigger.Items.Refresh();
                    tb_operationCooldown.Visibility = Visibility.Visible;
                    tb_operationCooldownLabel.Visibility = Visibility.Visible;
                }
                Refresh();
            }
        }
        private void tb_operationName_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                o.name = tb_operationName.Text;
                Refresh();
            }
        }
        private void cb_operation_trigger_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null && cb_operation_trigger.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                o.trigger = cb_operation_trigger.SelectedItem.ToString();
                Refresh();
            }
        }
        private void tb_operationCooldown_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_operationsList.SelectedIndex != -1) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                float? value = SanitizeFloat(tb_operationCooldown);
                if (value.HasValue) {
                    o.cooldown = (float)value;
                }
            }
        }
        private void tb_operationDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                o.description = tb_operationDescription.Text;
                Refresh();
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
            tb_object_dryWeight.Background = Brushes.White;
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                tb_object_name.Text = so.name;
                tb_object_dryWeight.Text = so.dryWeight.ToString();
                tb_object_model.Text = so.model;
                lv_collidersList.ItemsSource = so.colliders;
                lv_propertiesList.ItemsSource = so.properties;
                lv_operationsList.ItemsSource = so.operations;
                Refresh();
            } else {
                tb_object_name.Text = "";
                tb_object_dryWeight.Text = "";
                tb_object_model.Text = "";
                lv_collidersList.ItemsSource = null;
                lv_propertiesList.ItemsSource = null;
                lv_operationsList.ItemsSource = null;
            }
        }
        private void tb_object_name_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedItem != null) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                so.name = tb_object_name.Text;
                Refresh();
            }
        }
        private void tb_object_model_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                SanitizeString(tb_object_model);
                so.model = tb_object_model.Text;
                Refresh();
            }
        }
        private void tb_object_dryWeight_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                float? value = SanitizeFloat(tb_object_dryWeight);
                if (value.HasValue) {
                    so.dryWeight = (float)value;
                }
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
            textbox.Text = new string((from c in textbox.Text where !Path.GetInvalidFileNameChars().Contains(c) select c).ToArray());
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
        private int? SanitizeInt(TextBox textbox) {
            int value;
            if (int.TryParse(textbox.Text, out value)) {
                textbox.Text = value.ToString();
                textbox.Background = Brushes.White;
                return value;
            } else {
                textbox.Background = Brushes.Red;
                return null;
            }
        }
        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {
            sol = new SOL();
            lv_objectList.ItemsSource = sol.objects;
            Refresh();
        }
        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {
            XmlSerializer serializer = new XmlSerializer(typeof(SOL));
            using (FileStream fs = new FileStream(Environment.CurrentDirectory + @"\Created Libraries\" + sol.fqName, FileMode.Create, FileAccess.ReadWrite)) {
                serializer.Serialize(fs, sol);
                fs.Close();
            }
        }
        private void mi_loadLibrary_Click(object sender, RoutedEventArgs e) {
            XmlSerializer serializer = new XmlSerializer(typeof(SOL));
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.CurrentDirectory + @"\Created Libraries";
            if (dialog.ShowDialog() == true) {
                if (File.Exists(dialog.FileName)) {
                    using (FileStream fs = new FileStream(dialog.FileName, FileMode.Open, FileAccess.Read)) {
                        sol = serializer.Deserialize(fs) as SOL;
                        lv_objectList.ItemsSource = sol.objects;
                        Refresh();
                        fs.Close();
                    }
                }
            }
        }
        #endregion

        private void tb_property_name_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                p.name = tb_property_name.Text;
                Refresh();
            }
        }
        private void cb_property_type_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null && cb_property_type.SelectedItem != null) {
                tb_property_uBound.Visibility = Visibility.Collapsed;
                tb_property_uBoundLabel.Visibility = Visibility.Collapsed;
                tb_property_lBound.Visibility = Visibility.Collapsed;
                tb_property_lBoundLabel.Visibility = Visibility.Collapsed;
                tb_property_containerResource.Visibility = Visibility.Collapsed;
                tb_property_containerResourceLabel.Visibility = Visibility.Collapsed;
                tb_property_enumValuesLabel.Visibility = Visibility.Collapsed;
                dg_property_enumValues.Visibility = Visibility.Collapsed;
                switch ((string)cb_property_type.SelectedItem) {
                    case "Container":
                        (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new ContainerProperty();
                        tb_property_uBound.Visibility = Visibility.Visible;
                        tb_property_uBoundLabel.Visibility = Visibility.Visible;
                        tb_property_containerResource.Visibility = Visibility.Visible;
                        tb_property_containerResourceLabel.Visibility = Visibility.Visible;
                        break;
                    case "Integer":
                        (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new IntegerProperty();
                        tb_property_uBound.Visibility = Visibility.Visible;
                        tb_property_uBoundLabel.Visibility = Visibility.Visible;
                        tb_property_lBound.Visibility = Visibility.Visible;
                        tb_property_lBoundLabel.Visibility = Visibility.Visible;
                        break;
                    case "Real":
                        (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new RealProperty();
                        tb_property_uBound.Visibility = Visibility.Visible;
                        tb_property_uBoundLabel.Visibility = Visibility.Visible;
                        tb_property_lBound.Visibility = Visibility.Visible;
                        tb_property_lBoundLabel.Visibility = Visibility.Visible;
                        break;
                    case "Enum":
                        (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new EnumProperty();
                        tb_property_enumValuesLabel.Visibility = Visibility.Visible;
                        dg_property_enumValues.Visibility = Visibility.Visible;
                        break;
                } 
            } else {
                tb_property_name.Text = "";
                tb_operationDescription.Text = "";
                cb_property_type.SelectedIndex = -1;
                cb_property_visible.IsChecked = false;
                tb_property_uBound.Visibility = Visibility.Collapsed;
                tb_property_uBoundLabel.Visibility = Visibility.Collapsed;
                tb_property_lBound.Visibility = Visibility.Collapsed;
                tb_property_lBoundLabel.Visibility = Visibility.Collapsed;
                tb_property_containerResource.Visibility = Visibility.Collapsed;
                tb_property_containerResourceLabel.Visibility = Visibility.Collapsed;
                tb_property_enumValuesLabel.Visibility = Visibility.Collapsed;
                dg_property_enumValues.Visibility = Visibility.Collapsed;
            }
            Refresh();
        }
        private void tb_property_description_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                p.description = tb_property_description.Text;
                Refresh();
            }
        }

        private void cb_property_visible_Checked(object sender, RoutedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                p.visible = (bool)cb_property_visible.IsChecked;
                Refresh();
            }
        }

        private void tb_property_uBound_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedIndex != -1) {
                Property p = lv_propertiesList.SelectedItem as Property;
                float? fValue;
                int? iValue;
                if (p is ContainerProperty) {
                    fValue = SanitizeFloat(tb_property_uBound);
                    if (fValue.HasValue) {
                        (p as ContainerProperty).uBound = (float)fValue;
                    }
                }
                if (p is RealProperty) {
                    fValue = SanitizeFloat(tb_property_uBound);
                    if (fValue.HasValue) {
                        (p as RealProperty).uBound = (float)fValue;
                    }
                }
                if (p is IntegerProperty) {
                    iValue = SanitizeInt(tb_property_uBound);
                    if (iValue.HasValue) {
                        (p as IntegerProperty).uBound = (int)iValue;
                    }
                }
            }
        }

        private void tb_property_lBound_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedIndex != -1) {
                Property p = lv_propertiesList.SelectedItem as Property;
                float? fValue;
                int? iValue;
                if (p is RealProperty) {
                    fValue = SanitizeFloat(tb_property_lBound);
                    if (fValue.HasValue) {
                        (p as RealProperty).lBound = (float)fValue;
                    }
                }
                if (p is IntegerProperty) {
                    iValue = SanitizeInt(tb_property_lBound);
                    if (iValue.HasValue) {
                        (p as IntegerProperty).lBound = (int)iValue;
                    }
                }
            }
        }

        private void tb_property_containerResource_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null && lv_propertiesList.SelectedItem is ContainerProperty) {
                ContainerProperty cp = lv_propertiesList.SelectedItem as ContainerProperty;
                cp.resource = tb_property_containerResource.Text;
                Refresh();
            }
        }
    }

}
