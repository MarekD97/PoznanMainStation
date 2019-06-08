using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;
using System.Threading;

namespace PoznanMainStation
{
    class Station : Railway
    {
        int numberOfPlatforms;  //Liczba wygenerowanych peronów na stacji
        public TimeSpan stationTime = new TimeSpan(0, 0, 0); //Czas stacji
        public List<Platform> stationPlatforms = new List<Platform>();
        int preferredID = 0;

        List<Train> trainsToEnter = new List<Train>(); //pociągi, które czekają na wjazd
        List<Train> trainsAtPlatforms = new List<Train>(); //pociągi na peronach

        public Mutex m_entry = null;
        public Mutex m_exit = null;

        public override void Update()
        {
            //Zadania stacji

            //Aktualizacja czasu
            stationTime = stationTime.Add(new TimeSpan(0, 1, 0));
            Screen.SetStationTime(stationTime);
            //Sprawdzenie czy jakieś pociągi oczekują na wjazd
            if (trainsToEnter.Count() != 0)
            {
                //Jeśli preferowany peron przez pociąg jest dostępny (odwołanie IsFree() klasy Platform): 
                if (!trainsToEnter[0].allowedToEnter)
                {
                    if (trainsToEnter[0].GetPreferredPlatform().IsFree())
                    {
                        trainsToEnter[0].SetActualPlatform(trainsToEnter[0].GetPreferredPlatform());
                        trainsToEnter[0].IsAllowedToEnter();
                    }
                    else
                    {
                        if (preferredID == 0)
                        {
                            preferredID = trainsToEnter[0].GetPreferredPlatform().id;
                        }
                        preferredID++;
                        if (preferredID >= stationPlatforms.Count())
                        {
                            preferredID = 1;
                        }
                        if (stationPlatforms[preferredID - 1].IsFree())
                        {
                            trainsToEnter[0].SetActualPlatform(stationPlatforms[preferredID - 1]);
                            trainsToEnter[0].IsAllowedToEnter();
                        }
                    }
                }
            }

            if (trainsAtPlatforms.Count() != 0)
            {
                for (int i = 0; i<trainsAtPlatforms.Count(); i++)
                {
                    if (trainsAtPlatforms[i].GetReadyToLeave() == true)
                    {
                        trainsAtPlatforms[i].IsAllowedToLeave();
                        trainsAtPlatforms.Remove(trainsAtPlatforms[i]);
                        i--;
                    }
                }
            }

            Screen.Display();
        }

        public Station(int numberOfPlatforms, string stationName)
        {
            Screen.SetStationName(stationName);
            this.numberOfPlatforms = numberOfPlatforms;
            for (int i = 0; i < this.numberOfPlatforms; i++)
            {
                stationPlatforms.Add(new Platform(i+1, 100)); //na razie po 100 ludzi na peron, potem można zrobić losowanie
            }
            m_entry = new Mutex();
            m_exit = new Mutex();
        }

        //metoda używana przez pociągi w celu poinformowania stacji o swoim przyjeździe 
        public void TrainArrived(Train train)
        {
            trainsToEnter.Add(train);
        }

        //metoda używana przez pociągi w celu poinformowania stacji o zatrzymaniu się na peronie
        public void TrainAtPlatform(Train train)
        {
            trainsToEnter.Remove(train);
            trainsAtPlatforms.Add(train);
        }
    }
}
