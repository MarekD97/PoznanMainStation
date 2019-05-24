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
        int arrivalTime;            //Czas przyjazdu pociągu
        int departureTime;          //Czas odjazdu pociągu
        int numberOfPassengers;     //Ilość pasażerów w pociągu
        int capacity;               //Ładowność/Maksymalna liczba pasażerów w pociągu

        public override void Update()
        {
            Debug.WriteLine("Update klasy Train");
        }

        public Train()
        {
            Debug.Write("Pociąg stworzony");
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
