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
    /// This Displays an Invoice using a GUI element
    /// </summary>
    public partial class InvoiceWindow : Window
    {
        public InvoiceWindow()
        {
            InitializeComponent();
        }

        private void windowInvoice_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //Overide for close button
            e.Cancel = true;
            this.Hide();
        }
    }
}
