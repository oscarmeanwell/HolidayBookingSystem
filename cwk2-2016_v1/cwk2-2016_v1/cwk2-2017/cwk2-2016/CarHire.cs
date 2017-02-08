using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwk2_2016
{
    public class CarHire
    {
        /// <summary>
        /// Created on 20/11/2016 by Oscar Meanwell
        /// Last Modified 04/12/2016
        /// This class stores data about a car hire
        /// </summary>
        private string drivernm;
        private DateTime startdate;
        private DateTime enddate;
        private bool flag = false;

        public bool IsHired
        {
            //If they wish to hire a car.
            get { return flag; }
        }

        public string DriverNm
        {
            get { return drivernm; }
            set
            {
                if (String.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Driver must have a name", "Name");
                }
                drivernm = value;
                flag = true; //set flag to show a car is hired
            }
        }

        public DateTime StartDate
        {
            //Car Hire start date getter and setter
            get { return startdate; }
            set
            {
                if (String.IsNullOrWhiteSpace(value.ToString()))
                {
                    throw new ArgumentException("Hire must have a start date", "Start Date");
                }
                startdate = value;
            }
        }

        public DateTime EndDate
        {
            //car hire end date getter and setter
            get { return enddate; }
            set
            {
                if (String.IsNullOrWhiteSpace(value.ToString()))
                {
                    throw new ArgumentException("Hire must have an end date", "End Date");
                }
                enddate = value;
            }
        }

        public int getCost
        {
            //Get the cost of car hire
            get 
            {
                if (flag)
                {
                    TimeSpan period = (Convert.ToDateTime(enddate) - (Convert.ToDateTime(startdate)));
                    return (period.Days * 50);
                }
                    
                else
                {
                    return 0;
                }
                    
            }
        }
    }
}
