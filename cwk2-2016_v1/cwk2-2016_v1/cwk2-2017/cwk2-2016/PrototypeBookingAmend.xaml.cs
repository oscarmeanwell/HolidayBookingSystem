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
    /// This class is a prototype (clone) of a booking
    /// </summary>
    public partial class BookingAmend : Window
    {
        public BookingAmend()
        {
            InitializeComponent();
        }

        private void windowMain_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Overide for close window button
            e.Cancel = true;
            this.Hide();
        }

        private void btnAmend_Click(object sender, RoutedEventArgs e)
        {
            // on press off amend booking button from final amend window
            //loop through number of guests and get there age
            //check that all data is in correct format
            if (isInCorrectFormat())
            {
                int guestNO = isGuestSet(txtG1N) + isGuestSet(txtG2N) + isGuestSet(txtG3N) + isGuestSet(txtG4N);
                string[] tmpline = ManageBooking.toEdit;
                TimeSpan nights = (Convert.ToDateTime(txtEndDate.Text) - (Convert.ToDateTime(txtStartDate.Text))); //get stay length
                string rec = tmpline[0] + "," + tmpline[1] + "," + txtStartDate.Text + "," + txtEndDate.Text + "," + nights.Days + "," + guestNO + "," + getExtraCost(guestNO, nights.Days) + "," + getBCost(nights.Days) + "," + (getExtraCost(guestNO, nights.Days) + getBCost(nights.Days)) + "," + checkboxB.IsChecked.Value.ToString() + "," + txtBd.Text + "," + checkboxTea.IsChecked.Value.ToString() + "," + txtTD.Text + "," + checkboxCH.IsChecked.Value.ToString() + "," + txtCHDNm.Text + "," + txtCHStart.Text + "," + txtCHE.Text;
                string recG = tmpline[1] + "," + tmpline[0] + "," + getGuestStr();
                lbloldCost.Content = "Old Cost: " + tmpline[8] + " New Cost: " + (getExtraCost(guestNO, nights.Days) + getBCost(nights.Days));
                
                //write changes to file.
                string textBooking = File.ReadAllText(@"D:\tmp.csv");
                textBooking = textBooking.Replace(ManageBooking.editLN, rec);
                File.WriteAllText(@"D:\tmp.csv", textBooking);

                //do same for guests
                string textGuest = File.ReadAllText(@"D:\guest.csv");
                textGuest = textGuest.Replace(ManageBooking.guestLST, recG);
                File.WriteAllText(@"D:\guest.csv", textGuest);
            }
        }

        private int isGuestSet(TextBox t)
        {
            //Is guest set func
            if (!String.IsNullOrWhiteSpace(t.Text))
            {
                return 1;
            }
            return 0;
        }

        private int getExtraCost(int guestNumber, int nights)
        {
            //get cost of all extras
            int cost = 0;
            if (checkboxB.IsChecked == true) //if breakfast
            {
                cost += (guestNumber * 5) * nights;
            }
            if (checkboxTea.IsChecked == true)//if tea
            {
                cost += (guestNumber * 15) * nights; 
            }
            if (checkboxCH.IsChecked == true) //if car hire
            {
                TimeSpan period = (Convert.ToDateTime(txtCHE.Text) - (Convert.ToDateTime(txtCHStart.Text)));
                cost += period.Days * 50;
            }
            return cost;
        }
        private int getBCost(int nights)
        {
            //get cost of booking
            int cost = 0;
            if(txtG1N.Text.Length>0)
            {
                cost += addGC(int.Parse(txtG1A.Text), nights);
            }
            if (txtG2N.Text.Length > 0)
            {
                cost += addGC(int.Parse(txtG2A.Text), nights);
            }
            if (txtG3N.Text.Length > 0)
            {
                cost += addGC(int.Parse(txtG3A.Text), nights);
            }
            if (txtG4N.Text.Length > 0)
            {
                cost += addGC(int.Parse(txtG4A.Text), nights);
            }
            return cost;
            
        }
        private int addGC(int age, int nights)
        {
            //Determin and return the cost for every guest
            int cost = 0;
            if (age < 18)
            {
                cost += (30 * nights);//get number of nights and time by it here
            }
            else
            {
                cost += (50 * nights);
            }
            return cost;
        }
        public string getGuestStr()
        {
            //create a string to write to the guest.csv
            string guestlst = "";
            if (txtG1N.Text.Length > 0)
            {
                guestlst += txtG1N.Text + "," + txtGoP.Text + "," + txtG1A.Text + ",";
            }
            if (txtG2N.Text.Length > 0)
            {
                guestlst += txtG2N.Text + "," + txtG2P.Text + "," + txtG2A.Text + ",";
            }
            if (txtG3N.Text.Length > 0)
            {
                guestlst += txtG3N.Text + "," + txtG3P.Text + "," + txtG3A.Text + ",";
            }
            if (txtG4N.Text.Length > 0)
            {
                guestlst += txtG4N.Text + "," + txtG4P.Text + "," + txtG4A.Text + ",";
            }

            return guestlst.Remove(guestlst.Length - 1);

        }

        public bool isInCorrectFormat()
        {
            //check all text boxes contain data in correct format
            Booking tmpB = new Booking();
            try
            {
                bool guestflag = false;
                tmpB.StartDate = DateTime.Parse(txtStartDate.Text);
                tmpB.EndDate = DateTime.Parse(txtEndDate.Text);
                if (txtG1N.Text.Length > 0)
                {
                    Guest tmpG1 = new Guest();
                    tmpG1.Name = txtG1N.Text;
                    tmpG1.PassNum = txtGoP.Text;
                    tmpG1.Age = int.Parse(txtG1A.Text);
                    guestflag = true;
                }
                if (txtG2N.Text.Length > 0)
                {
                    Guest tmpG2 = new Guest();
                    tmpG2.Name = txtG2N.Text;
                    tmpG2.PassNum = txtG2P.Text;
                    tmpG2.Age = int.Parse(txtG2A.Text);
                    guestflag = true;
                }
                if (txtG3N.Text.Length > 0)
                {
                    Guest tmpG3 = new Guest();
                    tmpG3.Name = txtG3N.Text;
                    tmpG3.PassNum = txtG3P.Text;
                    tmpG3.Age = int.Parse(txtG3A.Text);
                    guestflag = true;
                }
                if (txtG4N.Text.Length > 0)
                {
                    Guest tmpG4 = new Guest();
                    tmpG4.Name = txtG4N.Text;
                    tmpG4.PassNum = txtG4P.Text;
                    tmpG4.Age = int.Parse(txtG4A.Text);
                    guestflag = true;
                }
                if (checkboxCH.IsChecked == true)
                {
                    CarHire tmpCH = new CarHire();
                    tmpCH.DriverNm = txtCHDNm.Text;
                    tmpCH.StartDate = DateTime.Parse(txtCHStart.Text);
                    tmpCH.EndDate = DateTime.Parse(txtCHE.Text);
                }
                if (!guestflag)
                {
                    throw new ArgumentException("Must have at least one guest", "Guest");
                }
                return true;

            }
            catch (Exception excep)
            {
                //catch errors thrown by class properties
                MessageBox.Show(excep.Message, "Error");
                return false;
            }
        }
    }
}
