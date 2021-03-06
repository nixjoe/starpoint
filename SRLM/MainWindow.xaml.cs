﻿using System;
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

namespace SRLM {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        bool hasChangedSinceSave;
        public SRL library { get; set; }

        public MainWindow() {
            InitializeComponent();
            DataContext = this;
            library = new SRL();
            Update();
            ClearUnsavedFlag();
        }

        public void Update() {
            tb_libraryName.IsEnabled = library != null;
            tb_bundleName.IsEnabled = library != null;
            tb_libraryVersion.IsEnabled = library != null;
            lv_resourceList.IsEnabled = library != null;
            mi_libraryView_delete.IsEnabled = library != null;
            mi_libraryView_new.IsEnabled = library != null;
            mi_libraryView_copy.IsEnabled = library != null;
            mi_saveLibrary.IsEnabled = library != null;
            tb_resourceName.IsEnabled = lv_resourceList.SelectedItem != null;
            tb_resourceWeight.IsEnabled = lv_resourceList.SelectedItem != null;
            if (library != null) {
                tb_libraryVersion.Text = library.version;
                tb_bundleName.Text = library.bundle;
                tb_libraryName.Text = library.name;

                Title = "Starpoint Resource Library Manager - " + library.bundle + "." + library.name + ".srl";
                if (hasChangedSinceSave) {
                    Title += "*";
                }
                lv_resourceList.ItemsSource = library.resources;
                lv_resourceList.Items.Refresh();
            } else {
                Title = "Starpoint Resource Library Manager";
            }

        }

        private void mi_libraryView_new_Click(object sender, RoutedEventArgs e) {
            hasChangedSinceSave = true;
            string name = "Resource " + (library.resources.Count + 1);
            library.AddResource(new StarpointResource(name, 0.0f));
            Update();
        }

        private void mi_libraryView_copy_Click(object sender, RoutedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                hasChangedSinceSave = true;
                StarpointResource selected = lv_resourceList.SelectedItem as StarpointResource;
                library.AddResource(new StarpointResource(selected.name, selected.weight));
            }
            Update();
        }

        private void mi_libraryView_delete_Click(object sender, RoutedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                hasChangedSinceSave = true;
                library.resources.Remove(lv_resourceList.SelectedItem as StarpointResource);
            }
            Update();
        }

        private void mi_newLibrary_Click(object sender, RoutedEventArgs e) {
            if (hasChangedSinceSave) {
                if (MessageBox.Show("There are unsaved changes in the library! Are you sure you want to close this library?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                    return;
                }
            }
            hasChangedSinceSave = false;
            library = new SRL();
            Update();
            ClearUnsavedFlag();
        }

        private void ClearUnsavedFlag() {
            hasChangedSinceSave = false;
            if (Title.Last() == '*') {
                Title = Title.Substring(0, Title.Length - 1);
            }
        }

        private void mi_saveLibrary_Click(object sender, RoutedEventArgs e) {
            if ((from StarpointResource s in library.resources select s.name).Count() != (from StarpointResource s in library.resources select s.name).Distinct().Count()) {
                MessageBox.Show("All Resources must have unique names!");
            } else {
                try {
                    library.Save();
                    hasChangedSinceSave = false;
                    Update();
                } catch {
                    MessageBox.Show("Something went wrong! Library not saved, please try again!");
                }
            }
        }

        private void mi_loadLibrary_Click(object sender, RoutedEventArgs e) {
            if (hasChangedSinceSave) {
                if (MessageBox.Show("There are unsaved changes in the library! Are you sure you want to close this library?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                    return;
                }
            }
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = Environment.CurrentDirectory + @"\Created Libraries";
            dialog.CheckFileExists = true;
            if (dialog.ShowDialog() == true) {
                library = SRL.Load(dialog.FileName);
                Update();
                ClearUnsavedFlag();
            }
            
        }

        private void tb_libraryName_TextChanged(object sender, TextChangedEventArgs e) {
            if (library != null) {
                hasChangedSinceSave = true;
                SanitizeString(tb_libraryName);
                library.name = tb_libraryName.Text;
            }
            Update();
        }

        private void tb_bundleName_TextChanged(object sender, TextChangedEventArgs e) {
            if (library != null) {
                SanitizeString(tb_bundleName);
                library.bundle = tb_bundleName.Text;
                hasChangedSinceSave = true;
            }
            Update();
        }
        private void SanitizeString(TextBox textbox) {
            textbox.Text = new string((from c in textbox.Text where !Path.GetInvalidFileNameChars().Contains(c) && c != '.' select c).ToArray());
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
            if (lv_resourceList.SelectedIndex >= 0 && (lv_resourceList.SelectedItem as StarpointResource).name != tb_resourceName.Text) {
                (lv_resourceList.SelectedItem as StarpointResource).name = tb_resourceName.Text;
                hasChangedSinceSave = true;
                Update();
            }
        }

        private void tb_resourceWeight_TextChanged(object sender, TextChangedEventArgs e) {
            if (lv_resourceList.SelectedIndex >= 0) {
                float parsedVal;
                if (float.TryParse(tb_resourceWeight.Text, out parsedVal)) {
                    if (parsedVal != (lv_resourceList.SelectedItem as StarpointResource).weight) {
                        (lv_resourceList.SelectedItem as StarpointResource).weight = parsedVal;
                        hasChangedSinceSave = true;
                    }
                    Update();
                } else {
                    tb_resourceWeight.Text = (lv_resourceList.SelectedItem as StarpointResource).weight.ToString();
                    tb_resourceWeight.CaretIndex = tb_resourceWeight.Text.Length;
                }
            }
        }

        private void mi_libraryView_sort_Click(object sender, RoutedEventArgs e) {
            library.resources.Sort(delegate(StarpointResource x, StarpointResource y) {
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

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e) {
            if (hasChangedSinceSave) {
                if (MessageBox.Show("There are unsaved changes in the library! Are you sure you want to quit?", "Warning!", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No) {
                    e.Cancel = true;
                }
            }
        }

        private void tb_libraryVersion_TextChanged(object sender, TextChangedEventArgs e) {
            if (library != null) {
                hasChangedSinceSave = true;
                library.version = tb_libraryVersion.Text;
            }
            Update();
        }
    }
}
