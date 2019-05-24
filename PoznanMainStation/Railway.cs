using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace PoznanMainStation
{
    abstract public class Railway : IRunnable
    {
        public abstract void Update();
        protected int frequency = 1000;
        protected bool hasFinished { get; set; }
        bool IRunnable.hasFinished { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Run()
        {
            while (!hasFinished)
            {
                Update();
                Thread.Sleep(frequency);
            }
        }

        public IEnumerator<float> CoroutineUpdate()
        {
            while (true)
            {
                Update();
                Thread.Sleep(frequency);
                if (hasFinished) yield break;
                yield return 0;
            }
        }

        public Railway()
        {
            hasFinished = false;
        }
    }
}
