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
using System.Windows.Shapes;

namespace cwk2_2016
{
    /// <summary>
    /// Created on 20/11/2016 by Oscar Meanwell
    /// Last Modified 04/12/2016
    /// This class stores information about Extras
    /// Has GUI element to get extras
    /// </summary>
    public partial class Extras : Window
    {
        private CarHire carhire = new CarHire(); //Create and store car hire object 
        private Meals meals = new Meals(); //Create and store meals object

        public Extras()
        {
            InitializeComponent();
        }


        public bool Breakfast
        {
            //Getter and setter for if booking has Breakfast
            get { return meals.Breakfast; }
            set { meals.Breakfast = value; }
        }

        public string BreakfastDietry
        {
            //Getter and settter for Breakfast Dietry
            get { return meals.BreakfastDietry; }
            set { meals.BreakfastDietry = value; }
        }

        public bool Tea
        {
            //Getter and setter for if booking has Dinner
            get { return meals.Tea; }
            set { meals.Tea = value; }
        }

        public string TeaDietry
        {
            //Getter and setter for tea dietry
            get { return meals.TeaDietry; }
            set { meals.TeaDietry = value; }
        }
        public string DriverName
        {
            //Get driver name from car hire and store locally
            get { return carhire.DriverNm; }
            set { carhire.DriverNm = value; }
        }

        public DateTime StartDate
        {
            //Get start date from car hire and store locally
            get { return carhire.StartDate; }
            set { carhire.StartDate = value; }
        }

        public DateTime EndDate
        {
            //Get end date from car hire and store locally
            get { return carhire.EndDate; }
            set { carhire.EndDate = value; }
        }

        public double GetCarHireCost
        {
            //Get cost from car hire and store locally
            get { return carhire.getCost; }
        }
        public double GetMealCost
        {
            //Get cost from meals and store locally
            get { return meals.getCost; }
        }

        public double GetTotalCost
        {
            //Get total cost of extras
            get
            {
                return GetMealCost + GetCarHireCost;
            }
        }
        public bool carHired
        {
            //Determin if car is hired
            get { return carhire.IsHired; }
        }
        private void btnAddToBooking_Click(object sender, RoutedEventArgs e)
        {
            //Add to booking button method
            bool flag = false;
            if (checkboxBreakfast.IsChecked == true)
            {
                meals.Breakfast = true;
                meals.BreakfastDietry = txtBreakfastD.Text;
            }
            else
            {
                meals.Breakfast = false;
                meals.BreakfastDietry = "n/a";
            }
            if (checkboxEveningM.IsChecked == true)
            {
                meals.Tea = true;
                meals.TeaDietry = txtTeaD.Text;
            }
            else
            {
                meals.Tea = false;
                meals.TeaDietry = "n/a"; 
            }

            if (checkboxCarHire.IsChecked == true)
            {
                if (DateTime.Parse(txtCarHireStartDate.Text) > DateTime.Parse(txtCarHireEndDate.Text))
                {
                    MessageBox.Show("End date must be after start date", "Date");
                    flag = true;
                }
                else
                {
                    try
                    {
                        carhire.StartDate = DateTime.Parse(txtCarHireStartDate.Text);
                        carhire.EndDate = DateTime.Parse(txtCarHireEndDate.Text);
                        carhire.DriverNm = txtCarHireDriverNm.Text;
                    }
                    catch (Exception excep)
                    {
                        //catch errors thrown by class properties
                        MessageBox.Show(excep.Message, "Error");
                        flag = true;
                    }
                }
            }

            if (!flag)
            {
                //If no errors in data, close.
                this.Close();
            }
        }

        private void windowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Override for close window button
            e.Cancel = true;
            this.Hide();
        }
    }
}
