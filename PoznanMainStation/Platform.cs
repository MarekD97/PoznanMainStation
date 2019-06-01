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
        int id;
        int numberOfPassengers;

        public Platform(int id)
        {
            this.id = id;
            this.available = true;
            this.numberOfPassengers = 100; //na razie stała, potem można zrobić losowanie
        }

        bool IsFree()
        {
            return available;
        }
    }
}
