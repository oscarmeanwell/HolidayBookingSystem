using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwk2_2016
{
    public class UnitTest
    {
        /// <summary>
        /// Created on 20/11/2016 by Oscar Meanwell
        /// Last Modified 04/12/2016
        /// This class tests the functionality of other classes using unit testing.
        /// </summary>
        public UnitTest()
        {
                Console.WriteLine("Unit Testing: ");
                Booking booking = new Booking();
                Guest guest = new Guest();
                try
                {
                    booking.ArrivalDate = "Oscar Meanwell"; //Should fail
                }
                catch (Exception e)
                {
                    Console.WriteLine("\tTrying to set Arrival Date to 'Oscar Meanwell'");
                    Console.WriteLine("\tFailed to set Date - error: " + e.Message);
                }
                try
                {
                    guest.Age = 100000; //Should fail out of range
                }
                catch (Exception e)
                {
                    Console.WriteLine("\tTrying to set Guest age to '100000'");
                    Console.WriteLine("\tFailed to set Guest age - error: " + e.Message);
                }
                try
                {
                    guest.PassNum = "A string longer than 10 chars"; //Should fail
                }
                catch (Exception e)
                {
                    Console.WriteLine("\tTrying to set Guest age to 'A string longer than 10 chars'");
                    Console.WriteLine("\tFailed to set Guest age - error: " + e.Message);
                }
        }
    }
}
