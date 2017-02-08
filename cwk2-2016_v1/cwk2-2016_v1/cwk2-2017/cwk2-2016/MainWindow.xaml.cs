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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections;
using System.IO;

namespace cwk2_2016
{
    /// <summary>
    /// Created on 20/11/2016 by Oscar Meanwell
    /// Last Modified 04/12/2016
    /// This class provides a GUI to create a booking from
    /// Also provides GUI to amend bookings, however only by reference
    /// </summary>
    public partial class MainWindow : Window
    {
        //Create booking and Customer as static so can be accessed outside this
        public static Booking booking = new Booking();
        public static Customer cust = new Customer();
        Extras extrasWind = new Extras();
        Window1 guestWind = new Window1();
        ManageBooking manage = new ManageBooking();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {    
            guestWind.Show(); //Show add guests window
        }

        private void btnAddExtras_Click(object sender, RoutedEventArgs e)
        {
            extrasWind.Show(); //Show add extras window
        }

        private void btnSubmit_Click(object sender, RoutedEventArgs e)
        {
            //On click of addall to booking
            //Unit test is carried out to show functionality of classes 
            //Tests are outputted to the Console.
            UnitTest test = new UnitTest();
            bool flag = false;
            booking = new Booking(); //flush booking/cust to remove any old data
            cust = new Customer();
            try
            {
                booking.ArrivalDate = txtArrivalDate.Text;
                booking.DepartureDate = txtDepDate.Text;
                cust.Name = txtCustName.Text;
                cust.Address = txtCustAdd.Text;
                booking.ReferenceNumber = int.Parse(File.ReadLines(@"D:\refno.txt").Last()) + 1; //get refno and ++
                if (!isGuestSet())
                {
                    throw new ArgumentException("Please Add at least one Guest", "Guest");
                }
                if (Convert.ToDateTime(booking.DepartureDate) < Convert.ToDateTime(booking.ArrivalDate))
                {
                    throw new ArgumentException("Departure Date must be after arrival date", "Date");
                }
                flag = true;
                
            } 
            catch (Exception excep)
            {
                //catch errors thrown by class properties
                MessageBox.Show(excep.Message, "Error");
            }
            if (flag)
            {
                //If no errors in data
                double price = CalculateCost();
                lblCost.Content = "Cost: " + price;
                Guest[] guestlist = { Window1.guestOne, Window1.guestTwo, Window1.guestThree, Window1.guestFour };
                List<Guest> SetGuests = new List<Guest>();
                foreach(Guest x in guestlist)
                {
                    if(!String.IsNullOrEmpty(x.Name))
                    {
                        SetGuests.Add(x);
                    }
                }
                SingletonFileHandler.RESET(); //reset singleton so new data can be added
                SingletonFileHandler fh = SingletonFileHandler.Instance(booking, cust, SetGuests, extrasWind);
            }
        }

        private double CalculateCost()
        {
            //Get cost
            double cost = 0;
            cost += booking.getCost;
            cost += ((extrasWind.GetCarHireCost) + ((extrasWind.GetMealCost)));
            return cost;
        }

        private bool isGuestSet()
        {
            //Method to determin if a guest is set
           Guest[] guestlist = { Window1.guestOne, Window1.guestTwo, Window1.guestThree, Window1.guestFour };
            bool existsflag = false;
            foreach(Guest i in guestlist)
            {
                if (!String.IsNullOrEmpty(i.Name))
                {
                    existsflag = true;
                }
            }
            return existsflag; 
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            //on load booking press
            manage.Show();
        }
    }
}
