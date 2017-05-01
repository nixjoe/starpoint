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
using System.Xml.Serialization;
using Microsoft.Win32;
using HelixToolkit.Wpf;
using System.Windows.Media.Media3D;
using System.Diagnostics;
using System.ComponentModel;

namespace SOLM {
    public partial class MainWindow : Window {
        SOL sol;
        private bool propertyTypeChange, colliderTypeChange, requirementTypeChange, effectTypeChange;
        private readonly string CREATED_LIBRARY_PATH = Environment.CurrentDirectory + @"\Created Libraries";
        private readonly string CONVERTED_MODELS_PATH = Environment.CurrentDirectory + @"\Models";
        private bool unsavedChanges;
        public MainWindow() {
            InitializeComponent();
            propertyTypeChange = true;
            colliderTypeChange = true;
            requirementTypeChange = true;
            effectTypeChange = true;

            helixViewport.ShowViewCube = false;
            helixViewport.ShowCoordinateSystem = true;
            helixViewport.CameraMode = CameraMode.Inspect;
            helixViewport.Camera.UpDirection = new Vector3D(0, 1, 0);
            cb_operation_actionType.ItemsSource = new List<string>(new string[] { "Discrete", "Continuous" });
            cb_operation_trigger.ItemsSource = new List<string>(new string[] { "Auto", "Semiauto", "Passive" });
            cb_effect_type.ItemsSource = new List<string>(new string[] { "Property", "Resource", "Physical", "Audio", "Visual", "Object" });
            cb_effect_assignment.ItemsSource = new List<string>(new string[] { "Equals", "Additive", "Subtractive", "Multiplicative" });
            cb_property_type.ItemsSource = new List<string>(new string[] { "Container", "Integer", "Real", "Enum" });
            cb_requirement_comparison.ItemsSource = new List<string>(new string[] { "=", "<", ">", "<=", ">=", "!=" });
            cb_requirement_type.ItemsSource = new List<string>(new string[] { "Resource", "Property" });
            cb_effect_audioMode.ItemsSource = new List<string>(new string[] { "PlayOnce", "Loop" });
            cb_effect_physicalType.ItemsSource = new List<string>(new string[] { "Force", "Torque" });
            cb_collider_shape.ItemsSource = new List<string>(new string[] { "Cube", "Sphere", "Capsule", "Cylinder" });
            Directory.CreateDirectory(CREATED_LIBRARY_PATH);
            Directory.CreateDirectory(CONVERTED_MODELS_PATH);
            sol = new SOL();
            lv_objectList.ItemsSource = sol.objects;
            ClearUnsavedFlag();
            Refresh();
        }

        private void MainWindow_Closing(object sender, CancelEventArgs e) {
            if (unsavedChanges) {
                if (MessageBox.Show("There are unsaved changes in the library! Are you sure you want to quit?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                    e.Cancel = true;
                }
            }
        }

        private void mi_objects_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_operations_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void lv_effectList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            if (lv_effectList.SelectedItem != null) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                requirementTypeChange = false;
                if (ef is PropertyEffect) {
                    cb_effect_type.SelectedIndex = (cb_effect_type.ItemsSource as List<string>).IndexOf("Property");
                    cb_effect_property.ItemsSource = (from p in (lv_objectList.SelectedItem as StarpointObject).properties select p.name).ToList();
                    cb_effect_property.SelectedIndex = (cb_effect_property.ItemsSource as List<string>).IndexOf((ef as PropertyEffect).property);
                    cb_effect_assignment.SelectedIndex = (cb_effect_assignment.ItemsSource as List<string>).IndexOf((ef as PropertyEffect).assignmentType.ToString());
                    tb_effect_value.Text = (ef as PropertyEffect).value;
                } else if (ef is ResourceEffect) {
                    cb_effect_type.SelectedIndex = (cb_effect_type.ItemsSource as List<string>).IndexOf("Resource");
                    cb_effect_property.ItemsSource = (from p in (lv_objectList.SelectedItem as StarpointObject).properties select p.name).ToList();
                    cb_effect_property.SelectedIndex = (cb_effect_property.ItemsSource as List<string>).IndexOf((ef as ResourceEffect).resource);
                    cb_effect_assignment.SelectedIndex = (cb_effect_assignment.ItemsSource as List<string>).IndexOf((ef as ResourceEffect).assignmentType.ToString());
                    tb_effect_value.Text = (ef as ResourceEffect).value;
                } else if (ef is PhysicalEffect) {
                    cb_effect_type.SelectedIndex = (cb_effect_type.ItemsSource as List<string>).IndexOf("Physical");
                    cb_effect_physicalType.SelectedIndex = (cb_effect_physicalType.ItemsSource as List<string>).IndexOf((ef as PhysicalEffect).physicalType.ToString());
                    tb_effect_xPos.Text = (ef as PhysicalEffect).xPos.ToString();
                    tb_effect_yPos.Text = (ef as PhysicalEffect).yPos.ToString();
                    tb_effect_zPos.Text = (ef as PhysicalEffect).zPos.ToString();
                    tb_effect_xValue.Text = (ef as PhysicalEffect).xValue.ToString();
                    tb_effect_yValue.Text = (ef as PhysicalEffect).yValue.ToString();
                    tb_effect_zValue.Text = (ef as PhysicalEffect).zValue.ToString();
                } else if (ef is AudioEffect) {
                    cb_effect_type.SelectedIndex = (cb_effect_type.ItemsSource as List<string>).IndexOf("Audio");
                    cb_effect_audioMode.SelectedIndex = (cb_effect_audioMode.ItemsSource as List<string>).IndexOf((ef as AudioEffect).audioMode.ToString());
                    tb_effect_audioClip.Text = (ef as AudioEffect).audioClip;
                } else if (ef is VisualEffect) {
                    cb_effect_type.SelectedIndex = (cb_effect_type.ItemsSource as List<string>).IndexOf("Visual");
                    tb_effect_xPos.Text = (ef as VisualEffect).xPos.ToString();
                    tb_effect_yPos.Text = (ef as VisualEffect).yPos.ToString();
                    tb_effect_zPos.Text = (ef as VisualEffect).zPos.ToString();
                    tb_effect_xRot.Text = (ef as VisualEffect).xRot.ToString();
                    tb_effect_yRot.Text = (ef as VisualEffect).yRot.ToString();
                    tb_effect_zRot.Text = (ef as VisualEffect).zRot.ToString();
                    tb_effect_visual.Text = (ef as VisualEffect).visual;
                } else if (ef is ObjectEffect) {
                    cb_effect_type.SelectedIndex = (cb_effect_type.ItemsSource as List<string>).IndexOf("Object");
                    tb_effect_xPos.Text = (ef as ObjectEffect).xPos.ToString();
                    tb_effect_yPos.Text = (ef as ObjectEffect).yPos.ToString();
                    tb_effect_zPos.Text = (ef as ObjectEffect).zPos.ToString();
                    tb_effect_xRot.Text = (ef as ObjectEffect).xRot.ToString();
                    tb_effect_yRot.Text = (ef as ObjectEffect).yRot.ToString();
                    tb_effect_zRot.Text = (ef as ObjectEffect).zRot.ToString();
                    tb_effect_xVelocity.Text = (ef as ObjectEffect).xVel.ToString();
                    tb_effect_yVelocity.Text = (ef as ObjectEffect).yVel.ToString();
                    tb_effect_zVelocity.Text = (ef as ObjectEffect).zVel.ToString();
                    tb_effect_xAngVelocity.Text = (ef as ObjectEffect).xAng.ToString();
                    tb_effect_yAngVelocity.Text = (ef as ObjectEffect).yAng.ToString();
                    tb_effect_zAngVelocity.Text = (ef as ObjectEffect).zAng.ToString();
                    tb_effect_object.Text = (ef as ObjectEffect).obj;
                }
                requirementTypeChange = true;
            }
            unsavedChanges = unsavedChangesBuffer;
            Refresh();
        }
        private void mi_effects_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_requirements_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_properties_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void mi_colliders_sort_Click(object sender, RoutedEventArgs e) {

        }
        private void b_loadModel_Click(object sender, RoutedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            UpdateViewport();
            unsavedChanges = unsavedChangesBuffer;
        }
        private void ClearUnsavedFlag() {
            unsavedChanges = false;
            if (Title.Last() == '*') {
                Title = Title.Substring(0, Title.Length - 1);
            }
        }
        private void UpdateViewport() {
            helixViewport.Children.Clear();
            helixViewport.Children.Add(new SunLight());
            GridLinesVisual3D glv = new GridLinesVisual3D();
            glv.Width = 8;
            glv.Length = 8;
            glv.MinorDistance = 0.25f;
            glv.MajorDistance = 1;
            glv.Thickness = 0.01f;
            glv.Normal = new Vector3D(0, 1, 0);
            helixViewport.Children.Add(glv);
            if (lv_objectList.SelectedItem != null) {
                StarpointObject o = lv_objectList.SelectedItem as StarpointObject;
                if (File.Exists(CONVERTED_MODELS_PATH + @"\" + (lv_objectList.SelectedItem as StarpointObject).fqModel) && !File.Exists(CONVERTED_MODELS_PATH + @"\" + (lv_objectList.SelectedItem as StarpointObject).GetObjModel())) {
                    ProcessStartInfo processStartInfo = new ProcessStartInfo();
                    processStartInfo.CreateNoWindow = true;
                    processStartInfo.UseShellExecute = false;
                    processStartInfo.RedirectStandardOutput = false;
                    processStartInfo.FileName = Environment.CurrentDirectory + @"\FbxConverter.exe";
                    processStartInfo.Arguments = CONVERTED_MODELS_PATH + @"\" + (lv_objectList.SelectedItem as StarpointObject).fqModel + " " + CONVERTED_MODELS_PATH + @"\" + (lv_objectList.SelectedItem as StarpointObject).GetObjModel();
                    processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process FbxConverter = Process.Start(processStartInfo);
                    FbxConverter.WaitForExit();
                }
                if (File.Exists(CONVERTED_MODELS_PATH + @"\" + (lv_objectList.SelectedItem as StarpointObject).GetObjModel())) {
                    ModelVisual3D mv3d = new ModelVisual3D();
                    mv3d.Content = new GeometryModel3D(LoadFromFile(CONVERTED_MODELS_PATH + @"\" + (lv_objectList.SelectedItem as StarpointObject).GetObjModel()), new DiffuseMaterial(Brushes.SlateGray));
                    Matrix3D m3d = Matrix3D.Identity;
                    m3d.Scale(new Vector3D(o.xScale, o.yScale, o.zScale));
                    mv3d.Transform = new MatrixTransform3D(m3d);
                    helixViewport.Children.Add(mv3d);
                }
                Brush colliderBrush;
                foreach (StarpointCollider c in (lv_objectList.SelectedItem as StarpointObject).colliders) {
                    if (c == lv_collidersList.SelectedItem) {
                        colliderBrush = new SolidColorBrush(Color.FromArgb(128, 255, 0, 0));
                    } else {
                        colliderBrush = new SolidColorBrush(Color.FromArgb(128, 0, 0, 255));
                    }
                    if (c is Cube) {
                        ModelVisual3D mv3d = new ModelVisual3D();
                        mv3d.Content = new GeometryModel3D(LoadFromFile(CONVERTED_MODELS_PATH + @"\cube.obj"), new DiffuseMaterial(colliderBrush));
                        Matrix3D m3d = Matrix3D.Identity;
                        m3d.Scale(new Vector3D(o.xScale, o.yScale, o.zScale));
                        m3d.Scale(new Vector3D((c as Cube).xSize, (c as Cube).ySize, (c as Cube).zSize));
                        m3d.Rotate(Euler(c.xRot, c.yRot, c.zRot));
                        m3d.Translate(new Vector3D(c.xOffset, c.yOffset, c.zOffset));
                        mv3d.Transform = new MatrixTransform3D(m3d);
                        helixViewport.Children.Add(mv3d);
                    } else if (c is Sphere) {
                        ModelVisual3D mv3d = new ModelVisual3D();
                        mv3d.Content = new GeometryModel3D(LoadFromFile(CONVERTED_MODELS_PATH + @"\sphere.obj"), new DiffuseMaterial(colliderBrush));
                        Matrix3D m3d = Matrix3D.Identity;
                        m3d.Scale(new Vector3D(o.xScale, o.yScale, o.zScale));
                        m3d.Scale(new Vector3D((c as Sphere).radius, (c as Sphere).radius, (c as Sphere).radius));
                        m3d.Rotate(Euler(c.xRot, c.yRot, c.zRot));
                        m3d.Translate(new Vector3D(c.xOffset, c.yOffset, c.zOffset));
                        mv3d.Transform = new MatrixTransform3D(m3d);
                        helixViewport.Children.Add(mv3d);
                    } else if (c is Cylinder) {
                        ModelVisual3D mv3d = new ModelVisual3D();
                        mv3d.Content = new GeometryModel3D(LoadFromFile(CONVERTED_MODELS_PATH + @"\cylinder.obj"), new DiffuseMaterial(colliderBrush));
                        Matrix3D m3d = Matrix3D.Identity;
                        m3d.Scale(new Vector3D(o.xScale, o.yScale, o.zScale));
                        m3d.Scale(new Vector3D((c as Cylinder).radius, (c as Cylinder).ySize, (c as Cylinder).radius));
                        m3d.Rotate(Euler(c.xRot, c.yRot, c.zRot));
                        m3d.Translate(new Vector3D(c.xOffset, c.yOffset, c.zOffset));
                        mv3d.Transform = new MatrixTransform3D(m3d);
                        helixViewport.Children.Add(mv3d);
                    } else if (c is Capsule) {
                        ModelVisual3D mv3d = new ModelVisual3D();
                        mv3d.Content = new GeometryModel3D(LoadFromFile(CONVERTED_MODELS_PATH + @"\capsule.obj"), new DiffuseMaterial(colliderBrush));
                        Matrix3D m3d = Matrix3D.Identity;
                        m3d.Scale(new Vector3D(o.xScale, o.yScale, o.zScale));
                        m3d.Scale(new Vector3D((c as Capsule).radius, (c as Capsule).ySize, (c as Capsule).radius));
                        m3d.Rotate(Euler(c.xRot, c.yRot, c.zRot));
                        m3d.Translate(new Vector3D(c.xOffset, c.yOffset, c.zOffset));
                        mv3d.Transform = new MatrixTransform3D(m3d);
                        helixViewport.Children.Add(mv3d);
                    }
                }
            }
            helixViewport.Items.Refresh();
        }
        #region completed and stable
        public static Quaternion Euler(float y, float x, float z) {
            y *= 0.0174533f;
            x *= 0.0174533f;
            z *= 0.0174533f;

            double yawOver2 = y * 0.5f;
            float cosYawOver2 = (float)System.Math.Cos(yawOver2);
            float sinYawOver2 = (float)System.Math.Sin(yawOver2);
            double pitchOver2 = x * 0.5f;
            float cosPitchOver2 = (float)System.Math.Cos(pitchOver2);
            float sinPitchOver2 = (float)System.Math.Sin(pitchOver2);
            double rollOver2 = z * 0.5f;
            float cosRollOver2 = (float)System.Math.Cos(rollOver2);
            float sinRollOver2 = (float)System.Math.Sin(rollOver2);
            Quaternion result = new Quaternion();
            result.W = cosYawOver2 * cosPitchOver2 * cosRollOver2 + sinYawOver2 * sinPitchOver2 * sinRollOver2;
            result.X = sinYawOver2 * cosPitchOver2 * cosRollOver2 + cosYawOver2 * sinPitchOver2 * sinRollOver2;
            result.Y = cosYawOver2 * sinPitchOver2 * cosRollOver2 - sinYawOver2 * cosPitchOver2 * sinRollOver2;
            result.Z = cosYawOver2 * cosPitchOver2 * sinRollOver2 - sinYawOver2 * sinPitchOver2 * cosRollOver2;

            return result;
        }
        private Geometry3D LoadFromFile(string objPath) {
            ModelImporter mi = new ModelImporter();
            Model3DGroup group = mi.Load(objPath);
            MeshBuilder TestMesh = new MeshBuilder(false, false);


            foreach (var m in group.Children) {
                var mGeo = m as GeometryModel3D;
                var mesh = (MeshGeometry3D)((Geometry3D)mGeo.Geometry);
                if (mesh != null) TestMesh.Append(mesh);
            }
            return TestMesh.ToMesh();

        }
        private void Refresh() {
            bool unsavedChangesBuffer = unsavedChanges;
            int lv_propertiesList_selectedIndex = lv_propertiesList.SelectedIndex;
            int lv_collidersList_selectedIndex = lv_collidersList.SelectedIndex;
            int lv_requirementsList_selectedIndex = lv_requirementList.SelectedIndex;
            int lv_effectsList_selectedIndex = lv_effectList.SelectedIndex;
            bool bufferUnsavedChanges = unsavedChanges;
            tb_libraryName.Text = sol.name;
            tb_bundleName.Text = sol.bundle;
            tb_version.Text = sol.version;
            unsavedChanges = bufferUnsavedChanges;
            lv_objectList.Items.Refresh();
            lv_operationsList.Items.Refresh();
            lv_propertiesList.Items.Refresh();
            lv_requirementList.Items.Refresh();
            lv_effectList.Items.Refresh();
            lv_collidersList.Items.Refresh();
            if (lv_propertiesList_selectedIndex <= lv_propertiesList.Items.Count) {
                lv_propertiesList.SelectedIndex = lv_propertiesList_selectedIndex;
            }
            if (lv_collidersList_selectedIndex <= lv_collidersList.Items.Count) {
                lv_collidersList.SelectedIndex = lv_collidersList_selectedIndex;
            }
            if (lv_requirementsList_selectedIndex <= lv_requirementList.Items.Count) {
                lv_requirementList.SelectedIndex = lv_requirementsList_selectedIndex;
            }
            if (lv_effectsList_selectedIndex <= lv_effectList.Items.Count) {
                lv_effectList.SelectedIndex = lv_effectsList_selectedIndex;
            }
            UpdateViewport();
            unsavedChanges = unsavedChangesBuffer;
            if (sol != null) {
                Title = "Starpoint Object Library Manager - " + sol.bundle + "." + sol.name + ".sol";
            }
            if (unsavedChanges) {
                Title += "*";
            }
        }
        private void NewItem(ListView listView) {
            if (listView == lv_objectList && listView.ItemsSource != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<StarpointObject>).Add(new StarpointObject(sol));
            } else if (listView == lv_operationsList && listView.ItemsSource != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Operation>).Add(new Operation());
            } else if (listView == lv_propertiesList && listView.ItemsSource != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Property>).Add(new ContainerProperty());
            } else if (listView == lv_requirementList && listView.ItemsSource != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Requirement>).Add(new ResourceRequirement());
            } else if (listView == lv_effectList && listView.ItemsSource != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Effect>).Add(new PropertyEffect());
            } else if (listView == lv_collidersList && listView.ItemsSource != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<StarpointCollider>).Add(new Cube());
            }
            Refresh();
        }
        private void CopyItem(ListView listView) {
            if (listView == lv_objectList && lv_objectList.SelectedIndex != -1) {
                unsavedChanges = true;
                (lv_objectList.ItemsSource as List<StarpointObject>).Add(new StarpointObject(lv_objectList.SelectedItem as StarpointObject));
            } else if (listView == lv_operationsList && lv_operationsList.SelectedIndex != -1) {
                unsavedChanges = true;
                (lv_operationsList.ItemsSource as List<Operation>).Add(new Operation(lv_operationsList.SelectedItem as Operation));
            } else if (listView == lv_propertiesList && lv_propertiesList.SelectedIndex != -1) {
                unsavedChanges = true;
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
                unsavedChanges = true;
                Requirement r = lv_requirementList.SelectedItem as Requirement;
                if (r is ResourceRequirement) {
                    (lv_requirementList.ItemsSource as List<Requirement>).Add(new ResourceRequirement(lv_requirementList.SelectedItem as ResourceRequirement));
                } else {
                    (lv_requirementList.ItemsSource as List<Requirement>).Add(new PropertyRequirement(lv_requirementList.SelectedItem as PropertyRequirement));
                }
            } else if (listView == lv_effectList && lv_effectList.SelectedIndex != -1) {
                unsavedChanges = true;
                Effect ef = lv_effectList.SelectedItem as Effect;
                if (ef is PropertyEffect) {
                    (lv_effectList.ItemsSource as List<Effect>).Add(new PropertyEffect(lv_effectList.SelectedItem as PropertyEffect));
                } else if (ef is ResourceEffect) {
                    (lv_effectList.ItemsSource as List<Effect>).Add(new ResourceEffect(lv_effectList.SelectedItem as ResourceEffect));
                } else if (ef is PhysicalEffect) {
                    (lv_effectList.ItemsSource as List<Effect>).Add(new PhysicalEffect(lv_effectList.SelectedItem as PhysicalEffect));
                } else if (ef is AudioEffect) {
                    (lv_effectList.ItemsSource as List<Effect>).Add(new AudioEffect(lv_effectList.SelectedItem as AudioEffect));
                } else if (ef is VisualEffect) {
                    (lv_effectList.ItemsSource as List<Effect>).Add(new VisualEffect(lv_effectList.SelectedItem as VisualEffect));
                } else if (ef is ObjectEffect) {
                    (lv_effectList.ItemsSource as List<Effect>).Add(new ObjectEffect(lv_effectList.SelectedItem as ObjectEffect));
                }
            } else if (listView == lv_collidersList && lv_collidersList.SelectedIndex != -1) {
                unsavedChanges = true;
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
            if (listView == lv_objectList && listView.SelectedItem != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<StarpointObject>).RemoveAt(listView.SelectedIndex);
                unsavedChanges = true;
            } else if (listView == lv_operationsList && listView.SelectedItem != null) {
                (listView.ItemsSource as List<Operation>).RemoveAt(listView.SelectedIndex);
                unsavedChanges = true;
            } else if (listView == lv_propertiesList && listView.SelectedItem != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Property>).RemoveAt(listView.SelectedIndex);
            } else if (listView == lv_requirementList && listView.SelectedItem != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Requirement>).RemoveAt(listView.SelectedIndex);
            } else if (listView == lv_effectList && listView.SelectedItem != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<Effect>).RemoveAt(listView.SelectedIndex);
            } else if (listView == lv_collidersList && listView.SelectedItem != null) {
                unsavedChanges = true;
                (listView.ItemsSource as List<StarpointCollider>).RemoveAt(listView.SelectedIndex);
            }
            Refresh();
        }
        private void tb_bundleName_TextChanged(object sender, TextChangedEventArgs e) {
            SanitizeString(tb_bundleName);
            sol.bundle = tb_bundleName.Text;
            unsavedChanges = true;
            StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
            if (so != null) {
                if (File.Exists(CONVERTED_MODELS_PATH + '\\' + (lv_objectList.SelectedItem as StarpointObject).GetObjModel()) || File.Exists(CONVERTED_MODELS_PATH + '\\' + (lv_objectList.SelectedItem as StarpointObject).fqModel)) {
                    tb_object_model.Background = Brushes.White;
                } else {
                    tb_object_model.Background = Brushes.Red;
                }
            }
            Refresh();
        }
        private void tb_libraryName_TextChanged(object sender, TextChangedEventArgs e) {
            SanitizeString(tb_libraryName);
            sol.name = tb_libraryName.Text;
            unsavedChanges = true;
            StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
            if (so != null) {
                if (File.Exists(CONVERTED_MODELS_PATH + '\\' + (lv_objectList.SelectedItem as StarpointObject).GetObjModel()) || File.Exists(CONVERTED_MODELS_PATH + '\\' + (lv_objectList.SelectedItem as StarpointObject).fqModel)) {
                    tb_object_model.Background = Brushes.White;
                } else {
                    tb_object_model.Background = Brushes.Red;
                }
            }
            Refresh();
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
            if (!(new string[] { "hp", "armor", "max temperature", "mass" }.Contains((lv_propertiesList.SelectedItem as Property).name))) {
                CopyItem(lv_propertiesList);
            }
        }
        private void mi_properties_delete_Click(object sender, RoutedEventArgs e) {
            if (!(new string[] { "hp", "armor", "max temperature", "mass" }.Contains((lv_propertiesList.SelectedItem as Property).name))) {
                DeleteItem(lv_propertiesList);
            }
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
            if (lv_collidersList.SelectedItems != null && cb_collider_shape.SelectedItem != null) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                if ((string)cb_collider_shape.SelectedItem == "Cube") {
                    if (colliderTypeChange) {
                        unsavedChanges = true;
                        (lv_collidersList.ItemsSource as List<StarpointCollider>)[lv_collidersList.SelectedIndex] = new Cube(c);
                    }
                    tb_collider_xSizeLabel.Visibility = Visibility.Visible;
                    tb_collider_xSize.Visibility = Visibility.Visible;
                    tb_collider_ySizeLabel.Visibility = Visibility.Visible;
                    tb_collider_ySize.Visibility = Visibility.Visible;
                    tb_collider_zSizeLabel.Visibility = Visibility.Visible;
                    tb_collider_zSize.Visibility = Visibility.Visible;
                } else if ((string)cb_collider_shape.SelectedItem == "Sphere") {
                    if (colliderTypeChange) {
                        unsavedChanges = true;
                        (lv_collidersList.ItemsSource as List<StarpointCollider>)[lv_collidersList.SelectedIndex] = new Sphere(c);
                    }
                    tb_collider_radius.Visibility = Visibility.Visible;
                    tb_collider_radiusLabel.Visibility = Visibility.Visible;
                }
                if ((string)cb_collider_shape.SelectedItem == "Capsule") {
                    if (colliderTypeChange) {
                        unsavedChanges = true;
                        (lv_collidersList.ItemsSource as List<StarpointCollider>)[lv_collidersList.SelectedIndex] = new Capsule(c);
                    }
                    tb_collider_radius.Visibility = Visibility.Visible;
                    tb_collider_radiusLabel.Visibility = Visibility.Visible;
                    tb_collider_ySizeLabel.Visibility = Visibility.Visible;
                    tb_collider_ySize.Visibility = Visibility.Visible;
                }
                if ((string)cb_collider_shape.SelectedItem == "Cylinder") {
                    if (colliderTypeChange) {
                        unsavedChanges = true;
                        (lv_collidersList.ItemsSource as List<StarpointCollider>)[lv_collidersList.SelectedIndex] = new Cylinder(c);
                    }
                    tb_collider_radius.Visibility = Visibility.Visible;
                    tb_collider_radiusLabel.Visibility = Visibility.Visible;
                    tb_collider_ySizeLabel.Visibility = Visibility.Visible;
                    tb_collider_ySize.Visibility = Visibility.Visible;
                }
            } else {
                tb_collider_xOffset.Text = "";
                tb_collider_yOffset.Text = "";
                tb_collider_zOffset.Text = "";
                tb_collider_xRot.Text = "";
                tb_collider_yRot.Text = "";
                tb_collider_zRot.Text = "";
                cb_collider_shape.SelectedIndex = -1;
            }
            Refresh();
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
            if (lv_effectList.SelectedItem != null && cb_effect_type.SelectedItem != null) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                switch ((string)cb_effect_type.SelectedItem) {
                    case "Property":
                        if (effectTypeChange) {
                            unsavedChanges = true;
                            (lv_effectList.ItemsSource as List<Effect>)[lv_effectList.SelectedIndex] = new PropertyEffect(ef);
                        }
                        tb_effect_propertyLabel.Visibility = Visibility.Visible;
                        tb_effect_assignmentLabel.Visibility = Visibility.Visible;
                        cb_effect_property.Visibility = Visibility.Visible;
                        cb_effect_assignment.Visibility = Visibility.Visible;
                        tb_effect_valueLabel.Visibility = Visibility.Visible;
                        tb_effect_value.Visibility = Visibility.Visible;
                        break;
                    case "Resource":
                        if (effectTypeChange) {
                            unsavedChanges = true;
                            (lv_effectList.ItemsSource as List<Effect>)[lv_effectList.SelectedIndex] = new ResourceEffect(ef);
                        }
                        tb_effect_resourceLabel.Visibility = Visibility.Visible;
                        tb_effect_resource.Visibility = Visibility.Visible;
                        tb_effect_assignmentLabel.Visibility = Visibility.Visible;
                        cb_effect_assignment.Visibility = Visibility.Visible;
                        tb_effect_valueLabel.Visibility = Visibility.Visible;
                        tb_effect_value.Visibility = Visibility.Visible;
                        break;
                    case "Physical":
                        if (effectTypeChange) {
                            unsavedChanges = true;
                            (lv_effectList.ItemsSource as List<Effect>)[lv_effectList.SelectedIndex] = new PhysicalEffect(ef);
                        }
                        tb_effect_physicalTypeLabel.Visibility = Visibility.Visible;
                        cb_effect_physicalType.Visibility = Visibility.Visible;
                        tb_effect_xValueLabel.Visibility = Visibility.Visible;
                        tb_effect_yValueLabel.Visibility = Visibility.Visible;
                        tb_effect_zValueLabel.Visibility = Visibility.Visible;
                        tb_effect_xValue.Visibility = Visibility.Visible;
                        tb_effect_yValue.Visibility = Visibility.Visible;
                        tb_effect_zValue.Visibility = Visibility.Visible;
                        tb_effect_xPosLabel.Visibility = Visibility.Visible;
                        tb_effect_yPosLabel.Visibility = Visibility.Visible;
                        tb_effect_zPosLabel.Visibility = Visibility.Visible;
                        tb_effect_xPos.Visibility = Visibility.Visible;
                        tb_effect_yPos.Visibility = Visibility.Visible;
                        tb_effect_zPos.Visibility = Visibility.Visible;
                        break;
                    case "Audio":
                        if (effectTypeChange) {
                            unsavedChanges = true;
                            (lv_effectList.ItemsSource as List<Effect>)[lv_effectList.SelectedIndex] = new AudioEffect(ef);
                        }
                        tb_effect_audioClipLabel.Visibility = Visibility.Visible;
                        tb_effect_audioModeLabel.Visibility = Visibility.Visible;
                        tb_effect_audioClip.Visibility = Visibility.Visible;
                        cb_effect_audioMode.Visibility = Visibility.Visible;
                        break;
                    case "Visual":
                        if (effectTypeChange) {
                            unsavedChanges = true;
                            (lv_effectList.ItemsSource as List<Effect>)[lv_effectList.SelectedIndex] = new VisualEffect(ef);
                        }
                        tb_effect_visualLabel.Visibility = Visibility.Visible;
                        tb_effect_visual.Visibility = Visibility.Visible;
                        tb_effect_xPosLabel.Visibility = Visibility.Visible;
                        tb_effect_yPosLabel.Visibility = Visibility.Visible;
                        tb_effect_zPosLabel.Visibility = Visibility.Visible;
                        tb_effect_xPos.Visibility = Visibility.Visible;
                        tb_effect_yPos.Visibility = Visibility.Visible;
                        tb_effect_zPos.Visibility = Visibility.Visible;
                        tb_effect_xRotLabel.Visibility = Visibility.Visible;
                        tb_effect_yRotLabel.Visibility = Visibility.Visible;
                        tb_effect_zRotLabel.Visibility = Visibility.Visible;
                        tb_effect_xRot.Visibility = Visibility.Visible;
                        tb_effect_yRot.Visibility = Visibility.Visible;
                        tb_effect_zRot.Visibility = Visibility.Visible;
                        break;
                    case "Object":
                        if (effectTypeChange) {
                            unsavedChanges = true;
                            (lv_effectList.ItemsSource as List<Effect>)[lv_effectList.SelectedIndex] = new ObjectEffect(ef);
                        }
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
                        tb_effect_xPosLabel.Visibility = Visibility.Visible;
                        tb_effect_yPosLabel.Visibility = Visibility.Visible;
                        tb_effect_zPosLabel.Visibility = Visibility.Visible;
                        tb_effect_xPos.Visibility = Visibility.Visible;
                        tb_effect_yPos.Visibility = Visibility.Visible;
                        tb_effect_zPos.Visibility = Visibility.Visible;
                        break;
                }
            }
            Refresh();
        }
        private void lv_operationsList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                bool unsavedChangesBuffer = unsavedChanges;
                Operation o = lv_operationsList.SelectedItem as Operation;
                tb_operationName.Text = o.name;
                cb_operation_actionType.SelectedIndex = (cb_operation_actionType.ItemsSource as List<string>).IndexOf(o.action);
                cb_operation_trigger.SelectedIndex = (cb_operation_trigger.ItemsSource as List<string>).IndexOf(o.trigger);
                tb_operationCooldown.Text = o.cooldown.ToString();
                tb_operationDescription.Text = o.description;
                lv_requirementList.ItemsSource = o.requirements;
                lv_effectList.ItemsSource = o.effects;
                unsavedChanges = unsavedChangesBuffer;
                Refresh();
            }
        }
        private void cb_operation_actionType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                unsavedChanges = true;
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
                unsavedChanges = true;
                Refresh();
            }
        }
        private void cb_operation_trigger_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null && cb_operation_trigger.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                o.trigger = cb_operation_trigger.SelectedItem.ToString();
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_operationCooldown_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_operationsList.SelectedIndex != -1) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                o.cooldown = tb_operationCooldown.Text;
                unsavedChanges = true;
            }
        }
        private void tb_operationDescription_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_operationsList.SelectedItem != null) {
                Operation o = lv_operationsList.SelectedItem as Operation;
                unsavedChanges = true;
                o.description = tb_operationDescription.Text;
                Refresh();
            }
        }
        private void tc_object_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            tc_opProp.SelectedIndex = tc_object.SelectedIndex;
            unsavedChanges = unsavedChangesBuffer;
        }
        private void tc_operation_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            tc_reqEff.SelectedIndex = tc_operation.SelectedIndex;
            unsavedChanges = unsavedChangesBuffer;
        }
        private void tc_reqEff_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            tc_operation.SelectedIndex = tc_reqEff.SelectedIndex;
            unsavedChanges = unsavedChangesBuffer;
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
            bool unsavedChangesBuffer = unsavedChanges;
            tc_object.SelectedIndex = tc_opProp.SelectedIndex;
            unsavedChanges = unsavedChangesBuffer;
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
            cb_requirement_property.Visibility = Visibility.Collapsed;
            tb_requirement_propertyLabel.Visibility = Visibility.Collapsed;
            tb_requirement_resource.Visibility = Visibility.Collapsed;
            tb_requirement_resourceLabel.Visibility = Visibility.Collapsed;
            if (lv_requirementList.SelectedItem != null && cb_requirement_type.SelectedItem != null) {
                Requirement r = lv_requirementList.SelectedItem as Requirement;
                switch ((string)cb_requirement_type.SelectedItem) {
                    case "Resource":
                        if (requirementTypeChange) {
                            unsavedChanges = true;
                            (lv_requirementList.ItemsSource as List<Requirement>)[lv_requirementList.SelectedIndex] = new ResourceRequirement(r);
                        }
                        tb_requirement_resource.Visibility = Visibility.Visible;
                        tb_requirement_resourceLabel.Visibility = Visibility.Visible;
                        break;
                    case "Property":
                        if (requirementTypeChange) {
                            unsavedChanges = true;
                            (lv_requirementList.ItemsSource as List<Requirement>)[lv_requirementList.SelectedIndex] = new PropertyRequirement(r);
                        }
                        cb_requirement_property.Visibility = Visibility.Visible;
                        tb_requirement_propertyLabel.Visibility = Visibility.Visible;
                        break;
                }
            } else {
                cb_requirement_type.SelectedIndex = -1;
                cb_requirement_comparison.SelectedIndex = -1;
            }
            Refresh();
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
            bool unsavedChangesBuffer = unsavedChanges;
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                tb_object_name.Text = so.name;
                tb_object_model.Text = so.model;
                lv_collidersList.ItemsSource = so.colliders;
                lv_propertiesList.ItemsSource = so.properties;
                lv_operationsList.ItemsSource = so.operations;
                tb_object_xScale.Text = so.xScale.ToString();
                tb_object_yScale.Text = so.yScale.ToString();
                tb_object_zScale.Text = so.zScale.ToString();
                
            } else {
                tb_object_name.Text = "";
                tb_object_model.Text = "";
                tb_object_xScale.Text = "";
                tb_object_yScale.Text = "";
                tb_object_zScale.Text = "";
                lv_collidersList.ItemsSource = null;
                lv_propertiesList.ItemsSource = null;
                lv_operationsList.ItemsSource = null;
            }
            unsavedChanges = unsavedChangesBuffer;
            Refresh();
        }
        private void tb_object_name_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedItem != null) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                so.name = tb_object_name.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_object_model_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedIndex != -1) {
                StarpointObject so = lv_objectList.SelectedItem as StarpointObject;
                SanitizeString(tb_object_model, true);
                so.model = tb_object_model.Text;
                unsavedChanges = true;
                if (File.Exists(CONVERTED_MODELS_PATH + '\\' + so.GetObjModel()) || File.Exists(CONVERTED_MODELS_PATH + '\\' + so.fqModel)) {
                    tb_object_model.Background = Brushes.White;
                } else {
                    tb_object_model.Background = Brushes.Red;
                }
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
        private void lv_propertiesList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                tb_property_name.Text = p.name;
                if (new string[] { "hp", "armor", "max temperature", "mass" }.Contains(p.name)) {
                    tb_property_name.IsReadOnly = true;
                    cb_property_type.IsReadOnly = true;
                    tb_property_lBound.IsReadOnly = true;
                } else {
                    tb_property_name.IsReadOnly = false;
                    cb_property_type.IsReadOnly = false;
                    tb_property_lBound.IsReadOnly = false;
                }

                propertyTypeChange = false;
                if (p is ContainerProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Container");
                    tb_property_uBound.Text = (p as ContainerProperty).uBound.ToString();
                    tb_property_containerResource.Text = (p as ContainerProperty).resource;
                    tb_property_default.Text = (p as ContainerProperty).defaultValue.ToString();
                } else if (p is IntegerProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Integer");
                    tb_property_uBound.Text = (p as IntegerProperty).uBound.ToString();
                    tb_property_lBound.Text = (p as IntegerProperty).lBound.ToString();
                    tb_property_default.Text = (p as IntegerProperty).defaultValue.ToString();
                } else if (p is RealProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Real");
                    tb_property_uBound.Text = (p as RealProperty).uBound.ToString();
                    tb_property_lBound.Text = (p as RealProperty).lBound.ToString();
                    tb_property_default.Text = (p as RealProperty).defaultValue.ToString();
                } else if (p is EnumProperty) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf("Enum");
                    dg_property_enumValues.ItemsSource = (p as EnumProperty).enums;
                    tb_property_default.Text = (p as EnumProperty).defaultValue.ToString();
                    dg_property_enumValues.Items.Refresh();
                }
                propertyTypeChange = true;
                tb_property_description.Text = p.description;
                cb_property_visible.IsChecked = p.visible;
                cb_property_control.IsChecked = p.control;
            }
            unsavedChanges = unsavedChangesBuffer;
            Refresh();
        }
        private void tb_property_name_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                if (!(new string[] { "hp", "armor", "max temperature", "mass" }.Contains(tb_property_name.Text))) {
                    p.name = tb_property_name.Text;
                    unsavedChanges = true;
                    tb_property_name.Background = Brushes.White;
                } else if (p.name != tb_property_name.Text) {
                    tb_property_name.Background = Brushes.Red;
                } else {
                    tb_property_name.Background = Brushes.White;
                }
                Refresh();
            }
        }
        private void cb_property_type_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            tb_property_uBound.Visibility = Visibility.Collapsed;
            tb_property_uBoundLabel.Visibility = Visibility.Collapsed;
            tb_property_lBound.Visibility = Visibility.Collapsed;
            tb_property_lBoundLabel.Visibility = Visibility.Collapsed;
            tb_property_containerResource.Visibility = Visibility.Collapsed;
            tb_property_containerResourceLabel.Visibility = Visibility.Collapsed;
            tb_property_enumValuesLabel.Visibility = Visibility.Collapsed;
            dg_property_enumValues.Visibility = Visibility.Collapsed;
            if (lv_propertiesList.SelectedItem != null && cb_property_type.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                switch ((string)cb_property_type.SelectedItem) {
                    case "Container":
                        if (propertyTypeChange && !(new string[] { "hp", "armor", "max temperature", "mass" }).Contains(p.name)) {
                            unsavedChanges = true;
                            (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new ContainerProperty(p);
                        }
                        tb_property_uBound.Visibility = Visibility.Visible;
                        tb_property_uBoundLabel.Visibility = Visibility.Visible;
                        tb_property_containerResource.Visibility = Visibility.Visible;
                        tb_property_containerResourceLabel.Visibility = Visibility.Visible;
                        break;
                    case "Integer":
                        if (propertyTypeChange && !(new string[] { "hp", "armor", "max temperature", "mass" }).Contains(p.name)) {
                            unsavedChanges = true;
                            (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new IntegerProperty(p);
                        }
                        tb_property_uBound.Visibility = Visibility.Visible;
                        tb_property_uBoundLabel.Visibility = Visibility.Visible;
                        tb_property_lBound.Visibility = Visibility.Visible;
                        tb_property_lBoundLabel.Visibility = Visibility.Visible;
                        break;
                    case "Real":
                        if (propertyTypeChange && !(new string[] { "hp", "armor", "max temperature", "mass" }).Contains(p.name)) {
                            unsavedChanges = true;
                            (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new RealProperty(p);
                        }
                        tb_property_uBound.Visibility = Visibility.Visible;
                        tb_property_uBoundLabel.Visibility = Visibility.Visible;
                        tb_property_lBound.Visibility = Visibility.Visible;
                        tb_property_lBoundLabel.Visibility = Visibility.Visible;
                        break;
                    case "Enum":
                        if (propertyTypeChange && !(new string[] { "hp", "armor", "max temperature", "mass" }).Contains(p.name)) {
                            unsavedChanges = true;
                            (lv_propertiesList.ItemsSource as List<Property>)[lv_propertiesList.SelectedIndex] = new EnumProperty(p);
                        }
                        tb_property_enumValuesLabel.Visibility = Visibility.Visible;
                        dg_property_enumValues.Visibility = Visibility.Visible;
                        break;
                }
                if ((new string[] { "hp", "armor", "max temperature", "mass" }).Contains(p.name)) {
                    cb_property_type.SelectedIndex = (cb_property_type.ItemsSource as List<string>).IndexOf(p.type.ToString());
                }
            } else {
                tb_property_name.Text = "";
                tb_operationDescription.Text = "";
                cb_property_type.SelectedIndex = -1;
                cb_property_visible.IsChecked = false;
            }
            Refresh();
        }
        private void tb_property_description_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                unsavedChanges = true;
                Property p = lv_propertiesList.SelectedItem as Property;
                p.description = tb_property_description.Text;
                Refresh();
            }
        }
        private void cb_property_visible_Checked(object sender, RoutedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                unsavedChanges = true;
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
                    fValue = SanitizeFloat(tb_property_uBound, true);
                    unsavedChanges = true;
                    (p as ContainerProperty).uBound = fValue;
                }
                if (p is RealProperty) {
                    fValue = SanitizeFloat(tb_property_uBound, true);
                    if (fValue.HasValue) {
                        if ((p as RealProperty).lBound.HasValue && (p as RealProperty).lBound >= fValue) {
                            fValue = (p as RealProperty).lBound + 0.00001f;
                            tb_property_uBound.Text = fValue.ToString();
                        }
                    }
                    unsavedChanges = true;
                    (p as RealProperty).uBound = fValue;
                }
                if (p is IntegerProperty) {
                    iValue = SanitizeInt(tb_property_uBound,true);
                    if (iValue.HasValue) {
                        if ((p as IntegerProperty).lBound.HasValue && (p as IntegerProperty).lBound >= iValue) {
                            iValue = (p as IntegerProperty).lBound + 1;
                            tb_property_uBound.Text = iValue.ToString();
                        }
                    }
                    unsavedChanges = true;
                    (p as IntegerProperty).uBound = iValue;
                }
            }
            Refresh();
        }
        private void tb_property_lBound_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedIndex != -1) {
                Property p = lv_propertiesList.SelectedItem as Property;
                float? fValue;
                int? iValue;
                if (p is RealProperty) {
                    fValue = SanitizeFloat(tb_property_lBound, true);
                    if (fValue.HasValue) {
                        if ((p as RealProperty).uBound.HasValue && (p as RealProperty).uBound <= fValue) {
                            fValue = (p as RealProperty).uBound - 0.00001f;
                            tb_property_lBound.Text = fValue.ToString();
                        }
                    }
                    unsavedChanges = true;
                    (p as RealProperty).lBound = fValue;
                }
                if (p is IntegerProperty) {
                    iValue = SanitizeInt(tb_property_lBound, true);
                    if (iValue.HasValue) {
                        if ((p as IntegerProperty).uBound.HasValue && (p as IntegerProperty).uBound <= iValue) {
                            iValue = (p as IntegerProperty).uBound - 1;
                            tb_property_lBound.Text = iValue.ToString();
                        }
                    }
                    unsavedChanges = true;
                    (p as IntegerProperty).lBound = iValue;
                }
            }
            Refresh();
        }
        private void tb_property_containerResource_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null && lv_propertiesList.SelectedItem is ContainerProperty) {
                ContainerProperty cp = lv_propertiesList.SelectedItem as ContainerProperty;
                cp.resource = tb_property_containerResource.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void SanitizeString(TextBox textbox, bool allowPeriods = false) {
            if (allowPeriods) {
                textbox.Text = new string((from c in textbox.Text where !Path.GetInvalidFileNameChars().Contains(c) select c).ToArray());
            } else {
                textbox.Text = new string((from c in textbox.Text where !Path.GetInvalidFileNameChars().Contains(c) && c != '.' select c).ToArray());
            }
        }
        private float? SanitizeFloat(TextBox textbox, bool allowNull = false) {
            float value;
            if (float.TryParse(textbox.Text, out value)) {
                textbox.Background = Brushes.White;
                return value;
            } else {
                if (allowNull && textbox.Text == "") {
                    textbox.Background = Brushes.White;
                } else {
                    textbox.Background = Brushes.Red;
                }
                return null;
            }
        }
        private int? SanitizeInt(TextBox textbox, bool allowNull = false) {
            int value;
            if (int.TryParse(textbox.Text, out value)) {
                //textbox.Text = value.ToString();
                textbox.Background = Brushes.White;
                return value;
            } else {
                if (allowNull && textbox.Text == "") {
                    textbox.Background = Brushes.White;
                } else {
                    textbox.Background = Brushes.Red;
                }
                return null;
            }
        }
        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {
            if (unsavedChanges) {
                if (MessageBox.Show("There are unsaved changes in the library! Are you sure you want to close this library?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                    return;
                }
            }
            sol = new SOL();
            unsavedChanges = true;
            lv_objectList.ItemsSource = sol.objects;
            Refresh();
        }
        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {
            if (ValidateLibrary()) {
                XmlSerializer serializer = new XmlSerializer(typeof(SOL));
                using (FileStream fs = new FileStream(Environment.CurrentDirectory + @"\Created Libraries\" + sol.fqName, FileMode.Create, FileAccess.ReadWrite)) {
                    serializer.Serialize(fs, sol);
                    fs.Close();
                    ClearUnsavedFlag();
                }
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
                        foreach (StarpointObject so in sol.objects) {
                            so.library = sol;
                        }
                        lv_objectList.ItemsSource = sol.objects;
                        Refresh();
                        fs.Close();
                        ClearUnsavedFlag();
                    }
                }
            }
        }
        private void lv_collidersList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_collidersList.SelectedItem != null) {
                bool unsavedChangesBuffer = unsavedChanges;
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                tb_collider_xOffset.Text = c.xOffset.ToString();
                tb_collider_yOffset.Text = c.yOffset.ToString();
                tb_collider_zOffset.Text = c.zOffset.ToString();
                tb_collider_xRot.Text = c.xRot.ToString();
                tb_collider_yRot.Text = c.yRot.ToString();
                tb_collider_zRot.Text = c.zRot.ToString();
                colliderTypeChange = false;
                if (c is Cube) {
                    cb_collider_shape.SelectedIndex = (cb_collider_shape.ItemsSource as List<string>).IndexOf("Cube");
                    tb_collider_xSize.Text = (c as Cube).xSize.ToString();
                    tb_collider_ySize.Text = (c as Cube).ySize.ToString();
                    tb_collider_zSize.Text = (c as Cube).zSize.ToString();
                } else if (c is Sphere) {
                    cb_collider_shape.SelectedIndex = (cb_collider_shape.ItemsSource as List<string>).IndexOf("Sphere");
                    tb_collider_radius.Text = (c as Sphere).radius.ToString();
                } else if (c is Capsule) {
                    cb_collider_shape.SelectedIndex = (cb_collider_shape.ItemsSource as List<string>).IndexOf("Capsule");
                    tb_collider_radius.Text = (c as Capsule).radius.ToString();
                    tb_collider_ySize.Text = (c as Capsule).ySize.ToString();
                } else if (c is Cylinder) {
                    cb_collider_shape.SelectedIndex = (cb_collider_shape.ItemsSource as List<string>).IndexOf("Cylinder");
                    tb_collider_radius.Text = (c as Cylinder).radius.ToString();
                    tb_collider_ySize.Text = (c as Cylinder).ySize.ToString();
                }
                colliderTypeChange = true;
                unsavedChanges = unsavedChangesBuffer;
                Refresh();
            }
        }
        private void tb_collider_xSize_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1 && lv_collidersList.SelectedItem is Cube) {
                Cube c = lv_collidersList.SelectedItem as Cube;
                float? value = SanitizeFloat(tb_collider_xSize);
                if (value.HasValue) {
                    c.xSize = (float)value;
                    unsavedChanges = true;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_ySize_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_ySize);
                if (value.HasValue) {
                    unsavedChanges = true;
                    if (c is Cube) {
                        (c as Cube).ySize = (float)value;
                    } else if (c is Capsule) {
                        (c as Capsule).ySize = (float)value;
                    } else if (c is Cylinder) {
                        (c as Cylinder).ySize = (float)value;
                    }
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_zSize_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1 && lv_collidersList.SelectedItem is Cube) {
                Cube c = lv_collidersList.SelectedItem as Cube;
                float? value = SanitizeFloat(tb_collider_zSize);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.zSize = (float)value;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_xOffset_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_xOffset);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.xOffset = (float)value;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_yOffset_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_yOffset);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.yOffset = (float)value;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_zOffset_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_zOffset);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.zOffset = (float)value;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_xRot_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_xRot);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.xRot = (float)value;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_yRot_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_yRot);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.yRot = (float)value;
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void tb_collider_zRot_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_zRot);
                if (value.HasValue) {
                    unsavedChanges = true;
                    c.zRot = (float)value;
                }
            }
            Refresh();
            UpdateViewport();
        }
        private void cb_requirement_property_DropDownOpened(object sender, EventArgs e) {
            if (lv_objectList.SelectedItem != null && lv_requirementList.SelectedItem != null) {
                Requirement r = lv_requirementList.SelectedItem as Requirement;
                cb_requirement_property.ItemsSource = (from p in (lv_objectList.SelectedItem as StarpointObject).properties select p.name).ToList();
                if (r is PropertyRequirement) {
                    cb_requirement_property.SelectedIndex = (cb_requirement_property.ItemsSource as List<string>).IndexOf((r as PropertyRequirement).property);
                }
            }
        }
        private void cb_effect_property_DropDownOpened(object sender, EventArgs e) {
            if (lv_objectList.SelectedItem != null && lv_effectList.SelectedItem != null) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                cb_effect_property.ItemsSource = (from p in (lv_objectList.SelectedItem as StarpointObject).properties select p.name).ToList();
                if (ef is PropertyEffect) {
                    cb_effect_property.SelectedIndex = (cb_effect_property.ItemsSource as List<string>).IndexOf((ef as PropertyEffect).property);
                }
            }
        }
        private void cb_requirement_property_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_requirementList.SelectedIndex != -1 && lv_requirementList.SelectedItem is PropertyRequirement && cb_requirement_property.SelectedIndex != -1) {
                PropertyRequirement r = lv_requirementList.SelectedItem as PropertyRequirement;
                r.property = cb_requirement_property.SelectedItem.ToString();
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_requirement_resource_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_requirementList.SelectedIndex != -1 && lv_requirementList.SelectedItem is ResourceRequirement) {
                ResourceRequirement r = lv_requirementList.SelectedItem as ResourceRequirement;
                r.resource = tb_requirement_resource.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void cb_requirement_comparison_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_requirementList.SelectedItem != null && cb_requirement_comparison.SelectedIndex != -1) {
                Requirement r = lv_requirementList.SelectedItem as Requirement;
                r.comparison = ((Requirement.ComparisonType)cb_requirement_comparison.SelectedIndex);
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_requirement_value_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_requirementList.SelectedIndex != -1) {
                Requirement r = lv_requirementList.SelectedItem as Requirement;
                r.value = tb_operationCooldown.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void cb_effect_property_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is PropertyEffect && cb_effect_property.SelectedIndex != -1) {
                PropertyEffect ef = lv_effectList.SelectedItem as PropertyEffect;
                ef.property = cb_effect_property.SelectedItem.ToString();
                unsavedChanges = true;
                Refresh();
            }
        }
        private void cb_effect_assignment_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_effectList.SelectedItem != null && cb_effect_assignment.SelectedIndex != -1) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                unsavedChanges = true;
                if (ef is PropertyEffect) {
                    (ef as PropertyEffect).assignmentType = ((Effect.AssignmentType)cb_effect_assignment.SelectedIndex);
                } else if (ef is ResourceEffect) {
                    (ef as ResourceEffect).assignmentType = ((Effect.AssignmentType)cb_effect_assignment.SelectedIndex);
                }
                Refresh();
            }
        }
        private void tb_effect_resource_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ResourceEffect) {
                ResourceEffect ef = lv_effectList.SelectedItem as ResourceEffect;
                ef.resource = tb_effect_resource.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void lv_requirementList_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            bool unsavedChangesBuffer = unsavedChanges;
            if (lv_requirementList.SelectedItem != null) {
                Requirement r = lv_requirementList.SelectedItem as Requirement;
                cb_requirement_comparison.SelectedIndex = (int)r.comparison;
                tb_requirement_value.Text = r.value;
                requirementTypeChange = false;
                if (r is PropertyRequirement) {
                    cb_requirement_type.SelectedIndex = (cb_requirement_type.ItemsSource as List<string>).IndexOf("Property");
                    cb_requirement_property.ItemsSource = (from p in (lv_objectList.SelectedItem as StarpointObject).properties select p.name).ToList();
                    cb_requirement_property.SelectedIndex = (cb_requirement_property.ItemsSource as List<string>).IndexOf((r as PropertyRequirement).property);
                } else if (r is ResourceRequirement) {
                    cb_requirement_type.SelectedIndex = (cb_requirement_type.ItemsSource as List<string>).IndexOf("Resource");
                    tb_requirement_resource.Text = (r as ResourceRequirement).resource;
                }
                requirementTypeChange = true;
            }
            unsavedChanges = unsavedChangesBuffer;
        }
        private void tb_collider_radius_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_collidersList.SelectedIndex != -1) {
                StarpointCollider c = lv_collidersList.SelectedItem as StarpointCollider;
                float? value = SanitizeFloat(tb_collider_radius);
                if (value.HasValue) {
                    unsavedChanges = true;
                    if (c is Sphere) {
                        (c as Sphere).radius = (float)value;
                    } else if (c is Capsule) {
                        (c as Capsule).radius = (float)value;
                    } else if (c is Cylinder) {
                        (c as Cylinder).radius = (float)value;
                    }
                }
            }
            UpdateViewport();
            Refresh();
        }
        private void cb_effect_physicalType_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_effectList.SelectedItem != null && lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is PhysicalEffect && cb_effect_physicalType.SelectedIndex != -1) {
                PhysicalEffect ef = lv_effectList.SelectedItem as PhysicalEffect;
                ef.physicalType = ((Effect.PhysicalType)cb_effect_physicalType.SelectedIndex);
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_value_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                unsavedChanges = true;
                if (ef is PropertyEffect) {
                    (ef as PropertyEffect).value = tb_effect_value.Text;
                } else if (ef is ResourceEffect) {
                    (ef as ResourceEffect).value = tb_effect_value.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_xValue_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is PhysicalEffect) {
                PhysicalEffect ef = lv_effectList.SelectedItem as PhysicalEffect;
                ef.xValue = tb_effect_xValue.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_yValue_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is PhysicalEffect) {
                PhysicalEffect ef = lv_effectList.SelectedItem as PhysicalEffect;
                ef.yValue = tb_effect_yValue.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_zValue_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is PhysicalEffect) {
                PhysicalEffect ef = lv_effectList.SelectedItem as PhysicalEffect;
                ef.zValue = tb_effect_zValue.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_audioClip_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is AudioEffect) {
                AudioEffect ef = lv_effectList.SelectedItem as AudioEffect;
                ef.audioClip = tb_effect_audioClip.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void cb_effect_audioMode_SelectionChanged(object sender, SelectionChangedEventArgs e) {
            if (lv_effectList.SelectedItem != null && lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is AudioEffect && cb_effect_audioMode.SelectedIndex != -1) {
                AudioEffect ef = lv_effectList.SelectedItem as AudioEffect;
                ef.audioMode = ((Effect.AudioMode)cb_effect_audioMode.SelectedIndex);
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_visual_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is VisualEffect) {
                VisualEffect ef = lv_effectList.SelectedItem as VisualEffect;
                (ef as VisualEffect).visual = tb_effect_visual.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_object_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                (ef as ObjectEffect).obj = tb_effect_object.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_xPos_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                unsavedChanges = true;
                if (ef is PhysicalEffect) {
                    (ef as PhysicalEffect).xPos = tb_effect_xPos.Text;
                } else if (ef is VisualEffect) {
                    (ef as VisualEffect).xPos = tb_effect_xPos.Text;
                } else if (ef is ObjectEffect) {
                    (ef as ObjectEffect).xPos = tb_effect_xPos.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_yPos_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                unsavedChanges = true;
                Effect ef = lv_effectList.SelectedItem as Effect;
                if (ef is PhysicalEffect) {
                    (ef as PhysicalEffect).yPos = tb_effect_yPos.Text;
                } else if (ef is VisualEffect) {
                    (ef as VisualEffect).yPos = tb_effect_yPos.Text;
                } else if (ef is ObjectEffect) {
                    (ef as ObjectEffect).yPos = tb_effect_yPos.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_zPos_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                Effect ef = lv_effectList.SelectedItem as Effect;
                unsavedChanges = true;
                if (ef is PhysicalEffect) {
                    (ef as PhysicalEffect).zPos = tb_effect_zPos.Text;
                } else if (ef is VisualEffect) {
                    (ef as VisualEffect).zPos = tb_effect_zPos.Text;
                } else if (ef is ObjectEffect) {
                    (ef as ObjectEffect).zPos = tb_effect_zPos.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_xRot_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                unsavedChanges = true;
                Effect ef = lv_effectList.SelectedItem as Effect;
                if (ef is VisualEffect) {
                    (ef as VisualEffect).xRot = tb_effect_xRot.Text;
                } else if (ef is ObjectEffect) {
                    (ef as ObjectEffect).xRot = tb_effect_xRot.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_yRot_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                unsavedChanges = true;
                Effect ef = lv_effectList.SelectedItem as Effect;
                if (ef is VisualEffect) {
                    (ef as VisualEffect).yRot = tb_effect_yRot.Text;
                } else if (ef is ObjectEffect) {
                    (ef as ObjectEffect).yRot = tb_effect_yRot.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_zRot_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1) {
                unsavedChanges = true;
                Effect ef = lv_effectList.SelectedItem as Effect;
                if (ef is VisualEffect) {
                    (ef as VisualEffect).zRot = tb_effect_zRot.Text;
                } else if (ef is ObjectEffect) {
                    (ef as ObjectEffect).zRot = tb_effect_zRot.Text;
                }
                Refresh();
            }
        }
        private void tb_effect_xVelocity_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                unsavedChanges = true;
                ef.xVel = tb_effect_xVelocity.Text;
                Refresh();
            }
        }
        private void tb_effect_yVelocity_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                ef.yVel = tb_effect_yVelocity.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_zVelocity_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                ef.zVel = tb_effect_zVelocity.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_xAngVelocity_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                ef.xAng = tb_effect_xAngVelocity.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void cb_property_control_Checked(object sender, RoutedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                p.control = (bool)cb_property_control.IsChecked;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_property_default_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_propertiesList.SelectedItem != null) {
                Property p = lv_propertiesList.SelectedItem as Property;
                unsavedChanges = true;
                if (p is ContainerProperty) {
                    float? value = SanitizeFloat(tb_property_default);
                    if (value.HasValue) {
                        (p as ContainerProperty).defaultValue = value.Value;
                    }
                } else if (p is RealProperty) {
                    float? value = SanitizeFloat(tb_property_default);
                    if (value.HasValue) {
                        (p as RealProperty).defaultValue = value.Value;
                    }
                } else if (p is IntegerProperty) {
                    int? value = SanitizeInt(tb_property_default);
                    if (value.HasValue) {
                        (p as IntegerProperty).defaultValue = value.Value;
                    }
                } else if (p is EnumProperty) {
                    int? value = SanitizeInt(tb_property_default);
                    if (value.HasValue) {
                        (p as EnumProperty).defaultValue = value.Value;
                    }
                }
                Refresh();
            }
        }
        private void tb_object_xScale_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedItem != null) {
                unsavedChanges = true;
                StarpointObject o = lv_objectList.SelectedItem as StarpointObject;
                float? value = SanitizeFloat(tb_object_xScale);
                if (value.HasValue) {
                    o.xScale = value.Value;
                }
            }
            Refresh();
        }
        private void tb_object_yScale_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedItem != null) {
                unsavedChanges = true;
                StarpointObject o = lv_objectList.SelectedItem as StarpointObject;
                float? value = SanitizeFloat(tb_object_yScale);
                if (value.HasValue) {
                    o.yScale = value.Value;
                }
            }
            Refresh();
        }
        private void tb_object_zScale_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_objectList.SelectedItem != null) {
                unsavedChanges = true;
                StarpointObject o = lv_objectList.SelectedItem as StarpointObject;
                float? value = SanitizeFloat(tb_object_zScale);
                if (value.HasValue) {
                    o.zScale = value.Value;
                }
            }
            Refresh();
        }
        private void tb_version_TextChanged(object sender, TextChangedEventArgs e) {
            sol.version = tb_version.Text;
            unsavedChanges = true;
            Refresh();
        }
        private void tb_effect_yAngVelocity_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                ef.yAng = tb_effect_yAngVelocity.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        private void tb_effect_zAngVelocity_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_effectList.SelectedIndex != -1 && lv_effectList.SelectedItem is ObjectEffect) {
                ObjectEffect ef = lv_effectList.SelectedItem as ObjectEffect;
                ef.zAng = tb_effect_yAngVelocity.Text;
                unsavedChanges = true;
                Refresh();
            }
        }
        #endregion
        private bool ValidateLibrary() {
            int errorCount = 0;
            List<string> objectNames = new List<string>();
            foreach (StarpointObject so in sol.objects) {
                if (!objectNames.Contains(so.name)) {
                    objectNames.Add(so.name);
                } else {
                    MessageBox.Show("There is already an object with the name " + so.name + "!");
                    errorCount++;
                }
                List<string> propertyNames = new List<string>();
                foreach (Property p in so.properties) {
                    if (p is ContainerProperty) {
                        ContainerProperty cp = p as ContainerProperty;
                        if (cp.uBound.HasValue && cp.defaultValue > cp.uBound.Value) {
                            MessageBox.Show("Property " + p.name + " in " + so.name + " has a default value outside of specified bounds!");
                            errorCount++;
                        }
                    } else if (p is RealProperty) {
                        RealProperty rp = p as RealProperty;
                        if ((rp.uBound.HasValue && rp.defaultValue > rp.uBound.Value) || (rp.lBound.HasValue && rp.defaultValue < rp.lBound.Value)) {
                            MessageBox.Show("Property " + p.name + " in " + so.name + " has a default value outside of specified bounds!");
                            errorCount++;
                        }
                    } else if (p is IntegerProperty) {
                        IntegerProperty ip = p as IntegerProperty;
                        if ((ip.uBound.HasValue && ip.defaultValue > ip.uBound.Value) || (ip.lBound.HasValue && ip.defaultValue < ip.lBound.Value)) {
                            MessageBox.Show("Property " + p.name + " in " + so.name + " has a default value outside of specified bounds!");
                            errorCount++;
                        }
                    } else if (p is EnumProperty) {
                        EnumProperty ep = p as EnumProperty;
                        List<EnumPropertyValue> epvList = new List<EnumPropertyValue>();
                        foreach (EnumPropertyValue epv in ep.enums) {
                            if (epvList.Exists(x => x.name == epv.name || x.value == epv.value)) {
                                MessageBox.Show("Property " + p.name + " in " + so.name + " has one or more duplicate enum values or names!");
                                errorCount++;
                                break;
                            } else {
                                epvList.Add(epv);
                            }
                        }
                        if (!(from epv in ep.enums select epv.value).Contains(ep.defaultValue)) {
                            MessageBox.Show("Property " + p.name + " in " + so.name + " has a default value not specified in enum list!");
                            errorCount++;
                        }
                    }
                    if (!propertyNames.Contains(p.name)) {
                        propertyNames.Add(p.name);
                    } else {
                        MessageBox.Show("There is already a property with the name " + p.name + " in object " + so.name + "!");
                        errorCount++;
                    }
                }
            }
            if (errorCount != 0) {
                MessageBox.Show("Library NOT SAVED. There " + (errorCount == 1 ? "is " : "are ") + errorCount + " problem" + (errorCount == 1 ? "" : "s") + " with this library. Please fix them before trying to save the library.", "SAVE FAILED!", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            return errorCount == 0;
        }
    }

}
