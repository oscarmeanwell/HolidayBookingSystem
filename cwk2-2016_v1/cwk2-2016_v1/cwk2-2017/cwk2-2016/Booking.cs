using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cwk2_2016
{
    public class Booking : Extras
    {
        /// <summary>
        /// Created on 20/11/2016 by Oscar Meanwell
        /// Last Modified 04/12/2016
        /// This class stores information about a booking
        /// </summary>
        private DateTime arrivaldate;
        private DateTime departuredate;
        private int referenceNum;
        private int staylength;
        private int guestnum;

        public string ArrivalDate
        {
            get { return arrivaldate.ToString(); }
            set 
            {
                try 
                { 
                    arrivaldate = DateTime.Parse(value); //Parse string to date
                }
                catch 
                { 
                    throw new ArgumentException("Must Contain a valid Start Date", "Date"); 
                }
            }
        }

        public string DepartureDate
        {
            get { return departuredate.ToString(); }
            set 
            { 
                try
                {
                    departuredate = DateTime.Parse(value); //parse string to date
                }
                catch
                {
                    throw new ArgumentException("Must Contain a valid End Date", "Date");
                }  
            }
        }

        public int ReferenceNumber
        {
           get { return referenceNum; }
           set { referenceNum = value; }
        }

        public double getCost
        {
            get
            {
                //For every guest created calculate total cost
                Guest[] guestlist = { Window1.guestOne, Window1.guestTwo, Window1.guestThree, Window1.guestFour };
                double cost = 0;
                foreach (Guest i in guestlist)
                {
                    if (!String.IsNullOrEmpty(i.Name))
                    {
                        if (i.Age < 18)
                        {
                            cost += (30 * StayLength); //get number of nights and times by it here
                        }
                        else
                        {
                            cost += (50 * StayLength);
                        }
                    }
                }
               
                return cost;
            }
        }
        public int StayLength
        {
            //Method to calculate and store booking length in days
            get 
            {
                TimeSpan nights = (Convert.ToDateTime(departuredate) - (Convert.ToDateTime(arrivaldate)));
                return staylength = nights.Days;
            }
        }
        public int guestNum
        {
            //Store the number of guests (useful)
            get
            {
                int tmp = 0;
                Guest[] guestlist = { Window1.guestOne, Window1.guestTwo, Window1.guestThree, Window1.guestFour };
                foreach(Guest i in guestlist)
                {
                    if (!String.IsNullOrEmpty(i.Name))
                    {
                        tmp += 1;
                    }
                }
                return guestnum = tmp;
            }
        }
    }
}



            