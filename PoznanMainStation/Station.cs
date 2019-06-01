using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Timers;

namespace PoznanMainStation
{
    class Station : Railway
    {
        int numberOfPlatforms;  //Liczba wygenerowanych peronów na stacji
        public TimeSpan stationTime = new TimeSpan(0, 0, 0); //Czas stacji
        public List<Platform> stationPlatforms = new List<Platform>();

        List<Train> trainsToEnter = new List<Train>(); //pociągi, które czekają na wjazd
        List<Train> trainsAtPlatforms = new List<Train>(); //pociągi na peronach

        public override void Update()
        {
            //Zadania stacji

            //Aktualizacja czasu
            Console.WriteLine("Czas stacji: {0}", stationTime);
            stationTime = stationTime.Add(new TimeSpan(0, 1, 0));

            //Sprawdzenie czy jakieś pociągi oczekują na wjazd
            if (trainsToEnter != null)
            {
                for (int i = trainsToEnter.Count - 1; i >= 0; i--)
                {
                    //Jeśli preferowany peron przez pociąg jest dostępny (odwołanie IsFree() klasy Platform):
                    //Semafor - jeśli przejazd nie jest bolokowany przez inny pociąg - wjazd
                    // jeśli jest zablokowany - oczekiwanie  
                    if(trainsToEnter[i].GetPreferredPlatform().IsFree())
                    {
                        trainsToEnter[i].SetActualPlatform(trainsToEnter[i].GetPreferredPlatform());
                        trainsToEnter[i].IsAllowedToEnter();
                    }
                    else
                    {
                        //jeśli peron zajęty, to stacja przydziela inny
                        //tr.SetActualPlatform(inny)
                    }
                }
            }

            if (trainsAtPlatforms != null)
            {
                for (int i = trainsAtPlatforms.Count-1; i>=0; i--)
                {
                    if (trainsAtPlatforms[i].GetReadyToLeave() == true)
                    {
                        trainsAtPlatforms[i].IsAllowedToLeave();
                        trainsAtPlatforms.Remove(trainsAtPlatforms[i]);
                    }
                    //Sprawdzenie, czy jakieś pociągi nie mają statusu wyjazdu
                    //Semafor - jeśli przejazd nie jest bolokowany przez inny pociąg - wyjazd
                    // jeśli jest zablokowany - oczekiwanie
                }
            }
        }

        public Station(int numberOfPlatforms)
        {
            this.numberOfPlatforms = numberOfPlatforms;
            for (int i = 0; i < this.numberOfPlatforms; i++)
            {
                stationPlatforms.Add(new Platform(i, 100)); //na razie po 100 ludzi na peron, potem można zrobić losowanie
            }
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
