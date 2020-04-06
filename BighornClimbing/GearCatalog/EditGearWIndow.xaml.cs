using System.Collections.ObjectModel;
using System.Windows;

namespace GearCatalog
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class EditGearWindow : Window
    {
        private Database db = new Database();
        private ObservableCollection<Gear> gearList;

        public EditGearWindow()
        {
            InitializeComponent();
            gearList = new ObservableCollection<Gear>(db.ReadGear());

            GearToEditListbox.ItemsSource = gearList;
        }

        private void GearToEditListbox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
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
            db.EditGear(gearList[GearToEditListbox.SelectedIndex]);

            this.Close();
        }

        private void CancelEditGearButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
