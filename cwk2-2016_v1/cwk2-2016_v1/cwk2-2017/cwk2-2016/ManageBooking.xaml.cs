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
using System.IO;

namespace cwk2_2016
{
    /// <summary>
    /// Created on 20/11/2016 by Oscar Meanwell
    /// Last Modified 04/12/2016
    /// This class provides a GUI to manage a booking.
    /// </summary>
    public partial class ManageBooking : Window
    {
        //Create as static so can be passed to edit window
        public static string[] toEdit = {};
        public static string editLN = "";
        public static string guestLST = "";
        string valuesG;
        InvoiceWindow invW = new InvoiceWindow();
        public static BookingAmend baWind = new BookingAmend();
        public ManageBooking()
        {
            InitializeComponent();
        }

        private void bookingCustomer_Click(object sender, RoutedEventArgs e)
        {
            //delete a customer button method
            btnRemB.IsEnabled = false;
            btnUpdateCust.IsEnabled = false;
            btnEditB.IsEnabled = false;
            btnProduceI.Visibility = System.Windows.Visibility.Hidden;
            custAdd.Visibility = System.Windows.Visibility.Hidden;
            custNm.Visibility = System.Windows.Visibility.Hidden;
            //update lst after customer deletion.
            populateCustomerLst();
            btnRem.IsEnabled = true;
        }
        private void populateCustomerLst()
        {
            //Helper func to populate listbox with customers
            lstBooking.Items.Clear();
            var reader = new StreamReader(File.OpenRead(@"D:\cust.csv"));
            while (!reader.EndOfStream)
            {
                lstBooking.Items.Add(reader.ReadLine());
            }
        }

        private void btnRem_Click(object sender, RoutedEventArgs e)
        {
            //Remove a customer button.
            if (lstBooking.SelectedItem == null)
            {
                MessageBox.Show("Must select a Customer", "Customer Selection");
            }
            else
            {
                string bookingsText = System.IO.File.ReadAllText(@"D:\tmp.csv");
                string[] values = lstBooking.SelectedItem.ToString().Split(',');
                if (bookingsText.Contains(values[2]))
                {
                    MessageBox.Show("Customer cannot be deleted - they have bookings.", "Customer Deletion Error");
                }
                else
                {
                    //find the customer to delete, remove them from array and re-write to file.
                    var file = new List<string>(System.IO.File.ReadAllLines(@"D:\cust.csv"));
                    int indexo = Array.IndexOf(file.ToArray(), lstBooking.SelectedItem.ToString());
                    file.RemoveAt(indexo);
                    GC.Collect(); //Call Garbage Collector to clean up and thus allow access to file.
                    File.WriteAllLines(@"D:\cust.csv", file.ToArray());
                    lstBooking.Items.Remove(lstBooking.SelectedItem.ToString());
                }
            }
        }

        private void bookingDelete_Click(object sender, RoutedEventArgs e)
        {
            //Delete Booking Button
            btnRem.IsEnabled = false;
            btnUpdateCust.IsEnabled = false;
            btnEditB.IsEnabled = false;
            btnRemB.IsEnabled = true;
            custAdd.Visibility = System.Windows.Visibility.Hidden;
            custNm.Visibility = System.Windows.Visibility.Hidden;
            btnProduceI.Visibility = System.Windows.Visibility.Hidden;
            populateBooking();
            //delete a booking button method
        }
        private void populateBooking()
        {
            //Populate listbox with bookings.
            lstBooking.Items.Clear();
            var reader = new StreamReader(File.OpenRead(@"D:\tmp.csv"));
            while (!reader.EndOfStream)
            {
                lstBooking.Items.Add(reader.ReadLine());
            }
            GC.Collect();
        }
        private void btnRemB_Click(object sender, RoutedEventArgs e)
        {
            //remove booking button
            if (lstBooking.SelectedItem == null)
            {
                MessageBox.Show("Please select a Booking");
            }
            else
            {
                //Clean up booking csv
                var file = new List<string>(System.IO.File.ReadAllLines(@"D:\tmp.csv"));
                int indexo = Array.IndexOf(file.ToArray(), lstBooking.SelectedItem.ToString());
                file.RemoveAt(indexo);
                GC.Collect(); //Call Garbage Collector to clean up and thus allow access to file.
                File.WriteAllLines(@"D:\tmp.csv", file.ToArray());
                //Clean up guest csv
                System.IO.StreamReader fileguest = new System.IO.StreamReader(@"D:\guest.csv");
                string line;
                string[] tempfromlst = lstBooking.SelectedItem.ToString().Split(',');
                List<string> toguestf = new List<string>();
                while ((line = fileguest.ReadLine()) != null)
                {
                    string[] tempsplit = line.Split(',');
                    if (tempsplit[1] != tempfromlst[0])
                    {
                        toguestf.Add(line);
                    }
                }
                fileguest.Close();
                GC.Collect();
                File.WriteAllLines(@"D:\guest.csv", toguestf.ToArray());
                lstBooking.Items.Remove(lstBooking.SelectedItem.ToString());
            }

        }

        private void windowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //on click of cancel.
            e.Cancel = true;
            this.Hide();
        }

        private void bookingEditCust_Click(object sender, RoutedEventArgs e)
        {
            //On click of ammend Customer.
            //enable user to change customer name and address
            //populate listbox and then open mini window
            custNm.Visibility = Visibility.Visible;
            custAdd.Visibility = Visibility.Visible;
            btnEditB.IsEnabled = false;
            btnProduceI.Visibility = System.Windows.Visibility.Hidden;
            btnUpdateCust.IsEnabled = true;
            btnRem.IsEnabled = false;
            btnRemB.IsEnabled = false;
            populateCustomerLst();
        }
        private void lstBooking_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bind lst to text boxes for customer editing
            if (btnUpdateCust.IsEnabled)
            {
                try
                {
                    valuesG = lstBooking.SelectedItem.ToString();
                    string[] valuesGnew = valuesG.Split(',');
                    custNm.Text = valuesGnew[0];
                    custAdd.Text = valuesGnew[1];
                }
                catch (Exception)
                {
                    Console.WriteLine();
                }
            }
        }
        private void btnUpdateCust_Click(object sender, RoutedEventArgs e)
        {
            //on update customer click
            //make text boxes invisible
            if (String.IsNullOrWhiteSpace(custNm.Text) || String.IsNullOrWhiteSpace(custAdd.Text))
            {
                MessageBox.Show("Customer must have name and Address", "Customer Editing");
            }
            else
            {
                string[] valuesGnew = valuesG.Split(',');
                string x = custNm.Text + "," + custAdd.Text + "," + valuesGnew[2];
                var file = new List<string>(System.IO.File.ReadAllLines(@"D:\cust.csv"));
                int indexo = Array.IndexOf(file.ToArray(), valuesG);
                file[indexo] = x;
                GC.Collect(); //Call Garbage Collector to clean up and thus allow access to file.
                File.WriteAllLines(@"D:\cust.csv", file.ToArray());
                populateCustomerLst();
            }

        }

        private void bookingEdit_Click(object sender, RoutedEventArgs e)
        {
            //On ammend booking clicked
            //edit visibility of buttons
            custAdd.Visibility = System.Windows.Visibility.Hidden;
            custNm.Visibility = System.Windows.Visibility.Hidden;
            btnProduceI.Visibility = System.Windows.Visibility.Hidden;
            btnUpdateCust.IsEnabled = false;
            btnRem.IsEnabled = false;
            btnRemB.IsEnabled = false;
            btnEditB.IsEnabled = true;
            populateBooking();
        }

        private void btnEditB_Click(object sender, RoutedEventArgs e)
        {
            //On press of edit booking.
            //Get data from booking and add to window gui elements
            if (lstBooking.SelectedItem == null)
            {
                MessageBox.Show("Please select a booking", "Edit Booking");
            }
            else
            {
                toEdit = lstBooking.SelectedItem.ToString().Split(',');
                editLN = lstBooking.SelectedItem.ToString();
                baWind.lbloldCost.Content = "Old Cost: " + toEdit[8]; 
                baWind.txtStartDate.Text = toEdit[2];
                baWind.txtEndDate.Text = toEdit[3];
                baWind.checkboxB.IsChecked = Boolean.Parse(toEdit[9]);
                baWind.txtBd.Text = toEdit[10];
                baWind.checkboxTea.IsChecked = Boolean.Parse(toEdit[11]);
                baWind.txtTD.Text = toEdit[12];
                baWind.checkboxCH.IsChecked = Boolean.Parse(toEdit[13]);
                if (Boolean.Parse(toEdit[13]))
                {
                    baWind.txtCHDNm.Text = toEdit[14];
                    baWind.txtCHStart.Text = toEdit[15];
                    baWind.txtCHE.Text = toEdit[16];
                }
                System.IO.StreamReader file1 = new System.IO.StreamReader(@"D:\guest.csv");
                string ln;
                string [] saught = {};
                while ((ln = file1.ReadLine()) != null)
                {
                    
                    string[] tempsplit = ln.Split(',');
                    if (tempsplit[1] == toEdit[0])
                    {
                        saught = ln.Split(',');
                        guestLST = ln;
                    }
                }
                file1.Close();
                GC.Collect();
                
                int breaker = int.Parse(toEdit[5]);//get guest number and add to gui
                if (breaker >= 1)
                {
                    baWind.txtG1N.Text = saught[2];
                    baWind.txtGoP.Text = saught[3];
                    baWind.txtG1A.Text = saught[4];
                }
                if (breaker >= 2)
                {
                    baWind.txtG2N.Text = saught[5];
                    baWind.txtG2P.Text = saught[6];
                    baWind.txtG2A.Text = saught[7];
                }
                if (breaker >= 3)
                {
                    baWind.txtG3N.Text = saught[8];
                    baWind.txtG3P.Text = saught[9];
                    baWind.txtG3A.Text = saught[10];
                }
                if (breaker == 4)
                {
                    baWind.txtG4N.Text = saught[11];
                    baWind.txtG4P.Text = saught[12];
                    baWind.txtG4A.Text = saught[13];
                }
                baWind.Show();
                
            }
        }

        private void btnProduceI_Click(object sender, RoutedEventArgs e)
        {
            //on press of Produce Invoice (hidden button)
            string [] invoiceBooking = lstBooking.SelectedItem.ToString().Split(',');
            string[] getG = getGuestName(invoiceBooking);
            int breakfastHolder = 0;
            int dinnerHolder = 0;
            int carHireHolder = 0;
            if (invoiceBooking[9] == "True")
            {
                breakfastHolder = 5;
            }
            if (invoiceBooking[11] == "True")
            {
                dinnerHolder = 15;
            }
            if (invoiceBooking[13] == "True")
            {
                TimeSpan noOfDays = DateTime.Parse(invoiceBooking[16]) - DateTime.Parse(invoiceBooking[15]);
                carHireHolder = noOfDays.Days * 50;
            }
            //Create string invoice and set as label content to display
            string invoiceTxt = @"
                         Invoice for booking number: " + invoiceBooking[0] + @" 

                                 Customer: " + getG[0] + @"
                                 Address: " + getG[1] + @"

                         Booking Details:
                         
                                 Total Cost: " + invoiceBooking[8] + @"
                                 Number of Guests (Adults £50pppn, Kids £30pppn): " + invoiceBooking[5] + @"
                                 Nights:  " + invoiceBooking[4] + @"

                         Extras:
  
     	                   Breakfast (£5pppn):  " + int.Parse(invoiceBooking[4])*breakfastHolder + @"
                                 Dinner (£15pppn): " + int.Parse(invoiceBooking[4]) * dinnerHolder + @"
                                 Car Hire (£50pppd): " + carHireHolder + "";
            
            invW.txtInv.Text = invoiceTxt;
            invW.Show();
        }

        private void btnInvoice_Click(object sender, RoutedEventArgs e)
        {
            //on press of Invoice Button
            //edit visibility of buttons
            btnProduceI.Visibility = Visibility.Visible;
            custAdd.Visibility = System.Windows.Visibility.Hidden;
            custNm.Visibility = System.Windows.Visibility.Hidden;
            btnUpdateCust.IsEnabled = false;
            btnRem.IsEnabled = false;
            btnRemB.IsEnabled = false;
            btnEditB.IsEnabled = false;
            populateBooking();
        }

        private string [] getGuestName(String [] invoiceBooking)
        {
            //Get Customer name from cust.csv
            System.IO.StreamReader file1 = new System.IO.StreamReader(@"D:\cust.csv");
            string ln;
            string[] saught = { };
            while ((ln = file1.ReadLine()) != null)
            {

                string[] tempsplit = ln.Split(',');
                if (tempsplit[2] == invoiceBooking[1])
                {
                    saught = ln.Split(',');
                    break;
                }
            }
            file1.Close();
            GC.Collect();
            return saught;
        }
    }

}