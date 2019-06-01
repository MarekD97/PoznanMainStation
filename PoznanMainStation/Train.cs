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
            //Zadania pociągu
            Console.WriteLine("Update klasy Train");
            //Gdy czas przyjazdu == czas stacji
            //Wysłanie żądania do stacji o chęci wjazdu na peron
            //Gdy odpowiedź jest pozytywna:
            //Wjazd, wyładunek, załadunek
            //Inaczej oczekiwanie
            //Wysłanie żądania do stacji o chęci wyjazdu ze stacji
            //Gdy odpowiedź jest pozytywna:
            //wyjazd
            //Inaczej oczekiwanie


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

        void Loading()     //wyładunek i załadunek
        {

        }

        void WaitingForDeparture()  //Oczekiwanie na odjazd
        {

        }

    }
}
