using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace cwk2_2016
{
    /// <summary>
    /// Created on 20/11/2016 by Oscar Meanwell
    /// Last Modified 04/12/2016
    /// This class is a singleton and is used to write data to files
    /// </summary>
    class SingletonFileHandler
    {
        private static SingletonFileHandler _instance;

        protected SingletonFileHandler(Booking booking, Customer cust, List<Guest> guests, Extras extra)
        {
            int guestRef = 0;
            if (System.IO.File.ReadAllText(@"D:\cust.csv").Contains(MainWindow.cust.Name + "," + MainWindow.cust.Address)) //if customer already exists
            {
                //if Customer already created get guestRef
                System.IO.StreamReader file = new System.IO.StreamReader(@"D:\cust.csv");
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (line.Contains(MainWindow.cust.Name + "," + MainWindow.cust.Address))
                    {
                        string [] l = line.Split(',');
                        guestRef = int.Parse(l[2]);
                    }
                }
                file.Close();
                GC.Collect();
            }
            else
            {
                //if Customer not created
                guestRef = (int.Parse(File.ReadLines(@"D:\guestno.txt").Last()) + 1);
                System.IO.File.AppendAllText(@"D:\cust.csv", (MainWindow.cust.Name + "," + MainWindow.cust.Address + "," + guestRef.ToString() + "\n"));
            }
            //Get booking & guest string
            string tmp = booking.ReferenceNumber + "," + guestRef.ToString() + "," + booking.ArrivalDate.ToString().Substring(0, 10) + "," + booking.DepartureDate.ToString().Substring(0, 10) + "," + booking.StayLength + "," + booking.guestNum + "," + extra.GetTotalCost + "," + booking.getCost + "," + (extra.GetTotalCost + booking.getCost) + "," + extra.Breakfast + "," + extra.BreakfastDietry + "," + extra.Tea + "," + extra.TeaDietry + "," + extra.carHired + "," + extra.DriverName + "," + extra.StartDate.ToString().Substring(0, 10) + "," + extra.EndDate.ToString().Substring(0, 10) + "\n";
            string tmpg = guestRef.ToString() + "," + booking.ReferenceNumber;
            foreach (Guest x in guests)
            {
                tmpg += "," + x.Name + "," + x.PassNum + "," + x.Age;
            }
            tmpg += "\n";
            
            System.IO.File.AppendAllText(@"D:\tmp.csv", tmp); //write booking info
            System.IO.File.AppendAllText(@"D:\refno.txt", (booking.ReferenceNumber.ToString() + "\n")); //write refno
            System.IO.File.AppendAllText(@"D:\guest.csv", tmpg); //write guests
            System.IO.File.AppendAllText(@"D:\guestno.txt", (guestRef.ToString() + "\n")); //write guestno
            
           
        }
        public static SingletonFileHandler Instance(Booking booking, Customer cust, List<Guest> guests, Extras extra)
        {
            if (_instance == null)
            {
                _instance = new SingletonFileHandler(booking, cust, guests, extra);
            }
            return _instance;
        }
        public static void RESET()
        {
            //if booking already created, flush class
            _instance = null;
        }
   
    }
}
