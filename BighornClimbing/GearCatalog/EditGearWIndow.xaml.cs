using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace GearCatalog
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditGearWindow : Window
    {
        private Database db;
        private ObservableCollection<Gear> gearList;

        public EditGearWindow()
        {
            InitializeComponent();
            db = new Database();
            gearList = new ObservableCollection<Gear>(db.ReadGear());

            GearToEditListbox.ItemsSource = gearList;
            
        }

        private void GearToEditListbox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GearToEditListbox.SelectedIndex == -1)
            {
                GearNameTextBox.Text = "";
                GearDescriptionTextBox.Text = "";
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
            gearToEdit.CategoryId = gearList[GearToEditListbox.SelectedIndex].CategoryId;
            gearToEdit.Name = GearNameTextBox.Text;
            gearToEdit.Description = GearDescriptionTextBox.Text;
            gearToEdit.Brand = GearBrandTextBox.Text;
            gearToEdit.WeightGrams = Int32.Parse(WeightGramsTextBox.Text);
            gearToEdit.WidthMM = Int32.Parse(WidthTextBox.Text);
            gearToEdit.DepthMM = Int32.Parse(DepthTextBox.Text);
            gearToEdit.Locking = LockingComboBox.SelectedIndex;

            db.EditGear(gearToEdit);

            gearList[LockingComboBox.SelectedIndex] = gearToEdit;

            this.Close();
        }

        private void CancelEditGearButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
