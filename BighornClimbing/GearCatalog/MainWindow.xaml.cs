using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Windows.Input;


namespace GearCatalog
{
    public partial class MainWindow : Window
    {
        private Database db = new Database();
        private ObservableCollection<Gear> gearList;
        private List<Category> categoryList;


        public MainWindow()
        {
            InitializeComponent();

            RefreshList();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Gear NewGear = new Gear();

            NewGear.Name = GearNameTextBox.Text;
            NewGear.CategoryId = categoryList[CategoryComboBox.SelectedIndex].Id;
            NewGear.Description = GearDescriptionTextBox.Text;
            NewGear.Brand = GearBrandTextBox.Text;
            NewGear.WeightGrams = Int32.Parse(WeightGramsTextBox.Text);
            NewGear.LengthMM = Int32.Parse(LengthTextBox.Text);
            NewGear.WidthMM = Int32.Parse(WidthTextBox.Text);
            NewGear.DepthMM = Int32.Parse(DepthTextBox.Text);
            NewGear.Locking = 0;

            if (LockingComboBox.SelectedIndex == 1)
            {
                NewGear.Locking = 1;
            }

            int newGearId = db.InsertGear(NewGear);
            NewGear.GearId = newGearId;
            gearList.Add(NewGear);

            GearNameTextBox.Text = "";
            CategoryComboBox.SelectedIndex = 0;
            GearDescriptionTextBox.Text = "";
            GearBrandTextBox.Text = "";
            WeightGramsTextBox.Text = "";
            LengthTextBox.Text = "";
            WidthTextBox.Text = "";
            DepthTextBox.Text = "";
            LockingComboBox.SelectedIndex = 0;
        }

        private void DeleteSelectionButton_Click(object sender, RoutedEventArgs e)
        {
            List<Gear> gearToRemove = new List<Gear>();

            foreach (var item in GearListBox.SelectedItems)
            {
                gearToRemove.Add((Gear)item);
            }

            db.RemoveGear(gearToRemove);

            foreach (Gear element in gearToRemove)
            {
                gearList.Remove(element);
            }
        }

        /* Regex matches any non-numeric characters. Any inputs this matches are
        marked as "handled" to stop propagation. */
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
      
        private void EditGearButton_Click(object sender, RoutedEventArgs e)
        {
            EditGearWindow editWin = new EditGearWindow();
            editWin.Closed += (object sender, EventArgs e) => { RefreshList(); };
            editWin.Show();
        }

        private void RefreshList()
        {
            gearList = new ObservableCollection<Gear>(db.ReadGear());
            categoryList = db.ReadCategories();
            CategoryComboBox.ItemsSource = categoryList;
            GearListBox.ItemsSource = gearList;
        }

    }
}