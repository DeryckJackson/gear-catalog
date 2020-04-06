using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Diagnostics;

namespace GearCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database db = new Database();
        private ObservableCollection<Gear> gearList;

        public MainWindow()
        {
            InitializeComponent();

            gearList = new ObservableCollection<Gear>(db.ReadGear());
            

            GearListBox.ItemsSource = gearList;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Gear NewGear = new Gear();

            NewGear.Name = GearNameTextBox.Text;
            NewGear.CategoryId = Int32.Parse(CategoryIdTextBox.Text);
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
            CategoryIdTextBox.Text = "";
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

        private void EditGearButton_Click(object sender, RoutedEventArgs e)
        {
            EditGearWindow editWin = new EditGearWindow();
            editWin.Show();
        }
    }
}