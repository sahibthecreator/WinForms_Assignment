using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    class Car
    {
        private string name;

        private string year;

        public Car(string name, string year)
        {
            this.name = name;
            this.year = year;
        }

        public string Name
        {
            get { return name; }   // get method
            set { name = value; }  // set method
        }
        public string Year
        {
            get { return year; }   // get method
            set { year = value; }  // set method
        }
    }
}
