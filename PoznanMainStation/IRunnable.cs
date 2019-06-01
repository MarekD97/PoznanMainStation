using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoznanMainStation
{
    interface IRunnable
    {
        void Run();
        bool hasFinished { get; set; }
    }
}
