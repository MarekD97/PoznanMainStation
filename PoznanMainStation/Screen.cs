using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoznanMainStation
{
    class Screen
    {
        private static string stationTimeText;
        private static string stationNameText;

        private static List<string> trainsArrivalsText = new List<string>();
        private static List<string> trainsDeparturesText = new List<string>();
        private static List<string> trainsAtStationText = new List<string>();

        private static List<int> trainsArrivalsPlatformText = new List<int>();
        private static List<int> trainsDeparturesPlatformText = new List<int>();
        private static List<int> trainsAtStationPlatformText = new List<int>();

        private static string tableTemplate = "{0,15} {1,15} {2,2} {3,15} {4,15} {5,2} {6,15} {7,15} {8,2}";

        public static void Display()
        {
            Console.Beep();
            Console.Clear();
            Console.WriteLine("[{0}] Stacja: {1}", stationTimeText, stationNameText);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("{0,15} {1,18} {2,15} {3,18} {4,15} {5,18}", "PRZYJAZDY", "|", "NA STACJI", "|", "ODJAZDY", "");
            Console.WriteLine(tableTemplate, "POCIĄG", "PERON", "|", "POCIĄG", "PERON", "|", "POCIĄG", "PERON", "");
            Console.BackgroundColor = ConsoleColor.Black;

            int listRow = 0;
            int listCountArrivals = 0;
            int listCountAtStation = 0;
            int listCountDepartures = 0;

            if (trainsArrivalsText != null)
                listCountArrivals = trainsArrivalsText.Count;
            if (trainsDeparturesText != null)
                listCountDepartures = trainsDeparturesText.Count;
            if (trainsAtStationText != null)
                listCountAtStation = trainsAtStationText.Count;

            listRow = Math.Max(Math.Max(listCountArrivals, listCountDepartures), listCountAtStation);
            if (listRow < 10)
                listRow = 10;

            for (int i = 0; i < listRow; i++)
            {
                string[] elementOfRow = new string[6];

                if (i < listCountArrivals)
                {
                    elementOfRow[0] = trainsArrivalsText.ElementAt(i);
                    elementOfRow[1] = trainsArrivalsPlatformText.ElementAt(i).ToString();
                }
                else
                {
                    elementOfRow[0] = "";
                    elementOfRow[1] = "";
                }
                if (i < listCountAtStation)
                {

                    elementOfRow[2] = trainsAtStationText.ElementAt(i);
                    elementOfRow[3] = trainsAtStationPlatformText.ElementAt(i).ToString();
                }
                else
                {
                    elementOfRow[2] = "";
                    elementOfRow[3] = "";
                }
                if (i < listCountDepartures)
                {
                    elementOfRow[4] = trainsDeparturesText.ElementAt(i);
                    elementOfRow[5] = trainsDeparturesPlatformText.ElementAt(i).ToString();
                }
                else
                {
                    elementOfRow[4] = "";
                    elementOfRow[5] = "";
                }
                Console.WriteLine(tableTemplate, elementOfRow[0], elementOfRow[1], "|", elementOfRow[2], elementOfRow[3], "|", elementOfRow[4], elementOfRow[5], "");
            }

            Console.WriteLine();
        }

        public static void SetStationTime(TimeSpan stationTime)
        {
            int[] time = new int[3];
            time[0] = stationTime.Hours;
            time[1] = stationTime.Minutes;
            time[2] = stationTime.Seconds;

            stationTimeText = null;
            for (int i = 0; i < 3; i++) 
            {
                if (time[i] < 10) 
                    stationTimeText += "0";
                stationTimeText += time[i].ToString();
                if (i < 2)
                    stationTimeText += ":";
            }
        }

        public static void SetStationName (string stationName)
        {
            stationNameText = stationName;
        }

        public static void AddTrainArrival(int idTrain, int platform)
        {
            trainsArrivalsText.Add("P" + idTrain.ToString());
            trainsArrivalsPlatformText.Add(platform);

        }

        public static void AddTrainDeparture(int idTrain, int platform)
        {
            trainsAtStationText.Remove("P" + idTrain.ToString());
            trainsAtStationPlatformText.Remove(platform);

            trainsDeparturesText.Add("P" + idTrain.ToString());
            trainsDeparturesPlatformText.Add(platform);
        }

        public static void AddTrainAtStation(int idTrain, int platform)
        {
            trainsArrivalsText.Remove("P" + idTrain.ToString());
            trainsArrivalsPlatformText.Remove(platform);

            trainsAtStationText.Add("P" + idTrain.ToString());
            trainsAtStationPlatformText.Add(platform);
        }

        public static void RemoveTrain(int idTrain, int platform)
        {
            trainsDeparturesText.Remove("P" + idTrain.ToString());
            trainsDeparturesPlatformText.Remove(platform);
        }
    }
}
