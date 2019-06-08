using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoznanMainStation
{
    class Platform 
    {
        bool available;
        public int id;
        public int numberOfPassengers;

        public Platform(int id, int numPass)
        {
            this.id = id;
            this.available = true;
            this.numberOfPassengers = Program.RandomNumber(10, 100);
        }

        public bool IsFree()
        {
            return available;
        }

        public void SetAvailability(bool val)
        {
            this.available = val;
        }

        public int GetPassengers()
        {
            return numberOfPassengers;
        }
    }
}
