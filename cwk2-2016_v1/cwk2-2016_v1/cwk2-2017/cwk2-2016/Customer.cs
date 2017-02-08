using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwk2_2016
{
    public class Customer
    {
        /// <summary>
        /// Created on 20/11/2016 by Oscar Meanwell
        /// Last Modified 04/12/2016
        /// This class stores information about a customer
        /// </summary>
        private string name;
        private string address;
        private int custrefnum;

        public string Name
        { 
            //Getter and setter for customer name
            get { return name; }
            set
            { 
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Customer must have a name", "Name");
                }
                name = value; 
            }

        }

        public string Address
        {
            //Getter and setter for customer address
            get { return address; }
            set 
            { 
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Customer must have an address", "Address");
                }
                address = value; 
            }
        }

        public int CustRefNum
        {
            //Customer reference number
            get { return custrefnum; }
            set
            { 
                custrefnum = value; 
            }
            
        }
    }
}
