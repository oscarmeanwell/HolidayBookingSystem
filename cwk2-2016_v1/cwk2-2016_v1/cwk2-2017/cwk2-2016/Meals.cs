using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwk2_2016
{
    /// <summary>
    /// Created on 20/11/2016 by Oscar Meanwell
    /// Last Modified 04/12/2016
    /// This class stores information about a Meal
    /// </summary>
    public class Meals
    {
        private bool breakfast;
        private string breakfastdietry;
        private bool tea;
        private string teadietry;

        public bool Breakfast
        {
            //Getter and setter for Breakfast
            get { return breakfast; }
            set { breakfast = value; }
        }

        public bool Tea
        {
            //Getter and setter for Tea
            get { return tea; }
            set { tea = value;  }
        }

        public string BreakfastDietry
        {
            //Getter and setter for Breakfast Dietry
            get { return breakfastdietry; }
            set { breakfastdietry = value; }
        }

        public string TeaDietry
        {
            //Getter and setter for Tea Dietry
            get { return teadietry; }
            set { teadietry = value; }
        }

        public double getCost
        {
            //Get meal cost
            get
            {
                double cost = 0;
                if (breakfast)
                {
                    cost += (MainWindow.booking.StayLength*5);
                }
                if (tea)
                {
                    cost += (MainWindow.booking.StayLength*15);
                }
                return cost * MainWindow.booking.guestNum;
                //times by number of guests
                
            }
        }

    }
}
