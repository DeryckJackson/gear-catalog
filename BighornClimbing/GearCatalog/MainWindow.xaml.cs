using System;
using System.Collections.Generic;
using System.Windows;

namespace GearCatalog
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Database Db;

        public MainWindow()
        {
            InitializeComponent();
            Db = new Database();
            Db.Connect();
            
            List<Gear> NewGearList = new List<Gear>();
            NewGearList = Db.ReadGear();

            foreach (Gear element in NewGearList)
            {
                GearList.Items.Add(element.Name);
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            GearList.Items.Add(GearNameTextBox.Text);

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

            if (LockingComboBox.SelectedIndex == 0)
            {
                NewGear.Locking = 1;
            }

            Db.InsertGear(NewGear);

            GearNameTextBox.Text = "";
            CategoryIdTextBox.Text = "";
            GearDescriptionTextBox.Text = "";
            GearBrandTextBox.Text = "";
            WeightGramsTextBox.Text = "";
            LengthTextBox.Text = "";
            WidthTextBox.Text = "";
            DepthTextBox.Text = "";
            LockingComboBox.SelectedIndex = 1;
        }
    }
}