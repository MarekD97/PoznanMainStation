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

        public Platform(int id)
        {
            this.id = id;
            this.available = true;
        }

        bool IsFree()
        {
            return available;
        }
    }
}
