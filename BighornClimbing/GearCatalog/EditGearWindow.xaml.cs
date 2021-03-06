using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;

namespace GearCatalog
{
    public partial class EditGearWindow : Window
    {
        private Database db;
        private ObservableCollection<Gear> gearList;

        public EditGearWindow()
        {
            InitializeComponent();
            db = new Database();
            RefreshList();

        }

        private void GearToEditListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GearToEditListbox.SelectedIndex == -1)
            {
                GearNameTextBox.Text = "";
                GearDescriptionTextBox.Text = "";
                CategoryComboBox.SelectedIndex = 0;
                GearBrandTextBox.Text = "";
                WeightGramsTextBox.Text = "";
                LengthTextBox.Text = "";
                WidthTextBox.Text = "";
                DepthTextBox.Text = "";
                LockingComboBox.SelectedIndex = 0;
            }
            else
            {
                GearNameTextBox.Text = gearList[GearToEditListbox.SelectedIndex].Name;
                GearDescriptionTextBox.Text = gearList[GearToEditListbox.SelectedIndex].Description;
                CategoryComboBox.SelectedValue = gearList[GearToEditListbox.SelectedIndex].CategoryId;
                GearBrandTextBox.Text = gearList[GearToEditListbox.SelectedIndex].Brand;
                WeightGramsTextBox.Text = gearList[GearToEditListbox.SelectedIndex].WeightGrams.ToString();
                LengthTextBox.Text = gearList[GearToEditListbox.SelectedIndex].LengthMM.ToString();
                WidthTextBox.Text = gearList[GearToEditListbox.SelectedIndex].WidthMM.ToString();
                DepthTextBox.Text = gearList[GearToEditListbox.SelectedIndex].DepthMM.ToString();
                LockingComboBox.SelectedIndex = gearList[GearToEditListbox.SelectedIndex].Locking;
            }
        }

        private void SaveGearButton_Click(object sender, RoutedEventArgs e)
        {
            Gear gearToEdit = new Gear();
            gearToEdit.GearId = gearList[GearToEditListbox.SelectedIndex].GearId;
            gearToEdit.CategoryId = (int)CategoryComboBox.SelectedValue;
            gearToEdit.Name = GearNameTextBox.Text;
            gearToEdit.Description = GearDescriptionTextBox.Text;
            gearToEdit.Brand = GearBrandTextBox.Text;
            gearToEdit.WeightGrams = Int32.Parse(WeightGramsTextBox.Text);
            gearToEdit.WidthMM = Int32.Parse(WidthTextBox.Text);
            gearToEdit.DepthMM = Int32.Parse(DepthTextBox.Text);
            gearToEdit.Locking = LockingComboBox.SelectedIndex;

            db.EditGear(gearToEdit);

            Close();
        }

        private void CancelEditGearButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void RefreshList()
        {
            gearList = new ObservableCollection<Gear>(db.ReadGear());
            CategoryComboBox.ItemsSource = db.ReadCategories();
            GearToEditListbox.ItemsSource = gearList;
            CategoryComboBox.SelectedIndex = 0;
        }

        /* Regex matches any non-numeric characters. Any inputs this matches are
        marked as "handled" to stop propagation. */
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
