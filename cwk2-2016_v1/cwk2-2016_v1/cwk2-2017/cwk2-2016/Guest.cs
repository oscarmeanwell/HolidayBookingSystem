using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwk2_2016
{
    public class Guest
    {
        /// <summary>
        /// Created on 20/11/2016 by Oscar Meanwell
        /// Last Modified 04/12/2016
        /// This class stores information about a guest
        /// </summary>
        private string name;
        private string passnum;
        private int age;

        public string Name
        {
            //Getter and setter for guest name
            get { return name; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Guest must have a name", "Name");
                }
                name = value;
            }
        }

        public string PassNum
        {
            //getter and setter for passport number
            get { return passnum; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Guest must have a passport number", "Passport Number");
                }
                if (value.Length > 10) //ensure length in range
                {
                    throw new ArgumentException("Please enter a valid Passport Number", "Passport Number");
                }
                passnum = value;
            }
        }

        public int Age
        {
            //getter and setter for age
            get { return age; }
            set
            {
                if (String.IsNullOrWhiteSpace(value.ToString()))
                {
                    throw new ArgumentException("Guest must have an age", "Age");
                }
                if (value < 0 || value > 101) //ensures a realistic age
                {
                    throw new ArgumentException("Guest must have an age in range 0-101", "Age");
                }
                age = value;
            }
        }
    }
}
