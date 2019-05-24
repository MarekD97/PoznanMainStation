using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PoznanMainStation
{
    class Station : Railway
    {
        public override void Update()
        {
            Debug.WriteLine("Update klasy Station");
        }

        public Station()
        {

        }
    }
}
