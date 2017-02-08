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
    /// This class provides a GUI to add guests
    /// It also adds and stores guest
    /// </summary>
    public partial class Window1 : Window
    {
        //Create guests as static so they can be accessed from another class
        //Static used as inheritance would be to messy 
        public static Guest guestOne = new Guest();
        public static Guest guestTwo = new Guest();
        public static Guest guestThree = new Guest();
        public static Guest guestFour = new Guest();
        public Window1()
        {
            InitializeComponent();
            //If guests already added and window closed re-add guests
            if (!String.IsNullOrEmpty(guestOne.Name))
            {
                txtGuest1Nm.Text = guestOne.Name;
                txtGuest1Age.Text = guestOne.Age.ToString();
                txtGuest1Pass.Text = guestOne.PassNum;
            }
            if (!String.IsNullOrEmpty(guestTwo.Name))
            {
                txtGuest2Nm.Text = guestTwo.Name;
                txtGuest2Age.Text = guestTwo.Age.ToString();
                txtGuest2Pass.Text = guestTwo.PassNum;
            }
            if (!String.IsNullOrEmpty(guestThree.Name))
            {
                txtGuest3Nm.Text = guestThree.Name;
                txtGuest3Age.Text = guestThree.Age.ToString();
                txtGuest3Pass1.Text = guestThree.PassNum;
            }
            if (!String.IsNullOrEmpty(guestFour.Name))
            {
                txtGuest4Nm.Text = guestFour.Name;
                txtGuest4Age.Text = guestFour.Age.ToString();
                txtGuest4Pass.Text = guestFour.PassNum;
            }
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            //On click of add guests
            //set all the guests
            bool errorflag = false;

            if (!String.IsNullOrWhiteSpace(txtGuest1Nm.Text))
            {
                try
                {
                    guestOne.Name = txtGuest1Nm.Text;
                    guestOne.PassNum = txtGuest1Pass.Text;
                    guestOne.Age = int.Parse(txtGuest1Age.Text);
                }

                catch (Exception excep)
                {
                    //catch errors thrown by class properties
                    MessageBox.Show(excep.Message, "Error");
                    errorflag = true;
                }
            }

            if (!String.IsNullOrWhiteSpace(txtGuest2Nm.Text))
            {   
                try
                {
                    guestTwo.Name = txtGuest2Nm.Text;
                    guestTwo.PassNum = txtGuest2Pass.Text;
                    guestTwo.Age = int.Parse(txtGuest2Age.Text);
                }

                catch (Exception excep)
                {
                    //catch errors thrown by class properties
                    MessageBox.Show(excep.Message, "Error");
                    errorflag = true;
                }
            }

            if (!String.IsNullOrWhiteSpace(txtGuest3Nm.Text))
            {
                try
                {
                    guestThree.Name = txtGuest3Nm.Text;
                    guestThree.PassNum = txtGuest3Pass1.Text;
                    guestThree.Age = int.Parse(txtGuest3Age.Text);
                }
                catch (Exception excep)
                {
                    //catch errors thrown by class properties
                    MessageBox.Show(excep.Message, "Error");
                    errorflag = true;
                }
            }

           if (!String.IsNullOrWhiteSpace(txtGuest4Nm.Text))
            {
                try
                {
                    guestFour.Name = txtGuest4Nm.Text;
                    guestFour.PassNum = txtGuest4Pass.Text;
                    guestFour.Age = int.Parse(txtGuest4Age.Text);
                }
                catch (Exception excep)
                {
                    //catch errors thrown by class properties
                    MessageBox.Show(excep.Message, "Error");
                    errorflag = true;
                }
            }

           if (!errorflag)
            {
               //If all guest data in good format
                this.Hide();
            }
            
        }

        private void frameWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
        }
    }
}
