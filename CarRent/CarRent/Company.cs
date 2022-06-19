using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class Company
    {
        private string name;
        private List<RentCar> rentCars;


        public string Name
        {
            get { return name; }
            set { name = value;}
        }
        public List<RentCar> RentCars
        {
            get { return rentCars; }
        }

        public void AddRentCars(RentCar car)
        {
            rentCars.Add(car);
        }



        public Company(string name)
        {
            this.name = name;
            rentCars = new List<RentCar>();
        }

        public double getTotalIncome()
        {
            double fee = 0;
    
            foreach (RentCar car in rentCars)
            {
                fee += car.Fee;
            }
            return fee;
        }

        public double getAvgDistance()
        {
            double avgDistance = 0;

            foreach (RentCar car in rentCars)
            {
                avgDistance += car.KmDriven;
            }
            return avgDistance / rentCars.Count;
        }

        public int getLongestPeriod()
        {
            int longestPeriod = 0;

            foreach (RentCar car in rentCars)
            {
                if(car.Period > longestPeriod)
                {
                    longestPeriod = car.Period;
                }
            }
            return longestPeriod;
        }
    }
}
