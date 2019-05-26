using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PoznanMainStation
{
    class Train : Railway
    {
        int id;
        TimeSpan arrivalTime;            //Czas przyjazdu pociągu (powiedzmy na razie że czas liczymy od startu symulacji w minutach)
        TimeSpan departureTime;          //Czas odjazdu pociągu
        int numberOfPassengers;     //Ilość pasażerów w pociągu
        int capacity;               //Ładowność/Maksymalna liczba pasażerów w pociągu
        int preferredPlatform;

        public override void Update()
        {
            if (Station.stationTime >= arrivalTime)
            {
                Station.trainsToEnter.Add(this);
            }

            if (Station.stationTime >= departureTime)
            {
                Station.trainsToLeave.Add(this);
            }
            Console.WriteLine("Update klasy Train");
        }

        public Train(int id, TimeSpan arrival, TimeSpan departure, int passengers, int cap)
        {
            this.id = id;
            this.arrivalTime = arrival;
            this.departureTime = departure;
            this.numberOfPassengers = passengers;
            this.capacity = cap;
            Console.WriteLine("Pociąg {0} stworzony", this.id);
        }

        void WaitingForPlatform()   //Oczekiwanie na wolny peron
        {

        }

        void EntryToThePlatform()   //Wjazd na peron
        {

        }

        void Loading(bool mode)     //mode 0 - rozładunek, mode 1 - załadunek
        {

        }

        void WaitingForDeparture()  //Oczekiwanie na odjazd
        {

        }

    }
}
