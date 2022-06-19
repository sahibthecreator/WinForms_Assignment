using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class RentCar
    {
        private Car car;
        private DateTime startDate;
        private DateTime endDate;
        private double kmDriven;
        private int period;
        private double fee;

        public Car Car
        {
            get { return car; }   // get method
            set { car = value; }  // set method
        }

        public DateTime StartDate{
            get { return startDate; }   // get method
            set { startDate = value; }  // set method
        }

        public DateTime EndDate
        {
            get { return endDate; }   // get method
            set { endDate = value; }  // set method
        }
        public double KmDriven
        {
            get { return kmDriven; }   // get method
            set { kmDriven = value; }  // set method
        }

        public int Period
        {
            get { return period; }   // get method
            set { period = value; }  // set method
        }
        public double Fee
        {
            get { return fee; }   // get method
            set { fee = value; }  // set method
        }

        public RentCar(Car car, DateTime startDate, DateTime endDate)
        {
            this.car = car;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public double GetRentFee(double km)
        {
            double fee = 0;
            if (km > 100)
            {
                fee += 50;
                km -= 100;
                fee += (km * 0.18);
            }
            this.fee = fee;
            return fee;
        }




    }
}
