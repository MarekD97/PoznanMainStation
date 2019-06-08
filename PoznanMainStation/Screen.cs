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

        private class Trains
        {
            public string name;
            public int platform;
            public string timeArrival;
            public string timeDeparture;
            public int passengers;
        }

        private static List<Trains> atStation = new List<Trains>();
        private static List<Trains> arrivals = new List<Trains>();
        private static List<Trains> departures = new List<Trains>();
        

        //private static string tableTemplate = "{0,15} {1,15} {2,2} {3,15} {4,15} {5,2} {6,15} {7,15} {8,2}";
        private static string tableTemplate = "{0,10}  |{1,15}  |{2,10}  |{3,10}  |{4,15}  |{5,10}  ";

        public static void Display()
        {
            Console.Clear();
            Console.WriteLine("[{0}] Stacja: {1}", stationTimeText, stationNameText);
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            //Console.WriteLine("{0,15} {1,18} {2,15} {3,18} {4,15} {5,18}", "PRZYJAZDY", "|", "NA STACJI", "|", "ODJAZDY", "");
            //Console.WriteLine(tableTemplate, "POCIĄG", "PREF. PERON", "|", "POCIĄG", "NA PERONIE", "|", "POCIĄG", "ODJ. Z PERONU", "");

            Console.WriteLine(tableTemplate, "PERON", "POCIĄG", "PRZYJAZD", "ODJAZD", "L. PASAŻERÓW", "STATUS");
            Console.BackgroundColor = ConsoleColor.Black;

            if (atStation != null)
            {
                for (int i = 0; i < 6; i++)
                {
                    if (i < atStation.Count)
                    {
                        Console.WriteLine(tableTemplate, atStation.ElementAt(i).platform, atStation.ElementAt(i).name, atStation.ElementAt(i).timeArrival, atStation.ElementAt(i).timeDeparture, atStation.ElementAt(i).passengers.ToString(), "");
                    }
                    else
                    {
                        Console.WriteLine(tableTemplate, "", "", "", "", "", "");
                    }
                }
            }

            Console.WriteLine();

            Console.WriteLine("{0,28} {1,43}", "PRZYJAZDY", "ODJAZDY");
            Console.BackgroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine(tableTemplate, "GODZINA", "NR POCIĄGU", "PERON", "GODZINA", "NR POCIĄGU", "PERON");
            Console.BackgroundColor = ConsoleColor.Black;

            int listRow = 0;
            int listCountArrivals = 0;
            int listCountDepartures = 0;

            if (arrivals != null)
                listCountArrivals = arrivals.Count;
            if (departures != null)
                listCountDepartures = departures.Count;

            listRow = Math.Max(listCountArrivals, listCountDepartures);
            if (listRow < 16)
                listRow = 16;

            for (int i = 0; i < listRow; i++)
            {
                string[] elementOfRow = new string[6];

                if (i < listCountArrivals)
                {
                    elementOfRow[0] = arrivals.ElementAt(i).timeArrival;
                    elementOfRow[1] = arrivals.ElementAt(i).name;
                    elementOfRow[2] = arrivals.ElementAt(i).platform.ToString();
                }
                else
                {
                    elementOfRow[0] = "";
                    elementOfRow[1] = "";
                    elementOfRow[2] = "";
                }
                if (i < listCountDepartures)
                {
                    elementOfRow[3] = departures.ElementAt(i).timeDeparture;
                    elementOfRow[4] = departures.ElementAt(i).name;
                    elementOfRow[5] = departures.ElementAt(i).platform.ToString();
                }
                else
                {
                    elementOfRow[3] = "";
                    elementOfRow[4] = "";
                    elementOfRow[5] = "";
                }
                Console.WriteLine(tableTemplate, elementOfRow[0], elementOfRow[1], elementOfRow[2], elementOfRow[3], elementOfRow[4], elementOfRow[5]);
            }

            Console.WriteLine();

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

        public static void AddTrainArrival(int idTrain, int platform, TimeSpan arrival)
        {
            Trains train = new Trains
            {
                name = "TLK " + idTrain.ToString(),
                platform = platform,
                timeArrival = arrival.ToString()
            };
            arrivals.Add(train);

        }

        public static void AddTrainDeparture(int idTrain, int platform, TimeSpan departure)
        {
            int index = atStation.FindIndex(
              delegate (Trains trainIndex)
              {
                  return trainIndex.name == "TLK " + idTrain;
              });
            atStation.RemoveAt(index);
            Trains train = new Trains
            {
                name = "TLK " + idTrain.ToString(),
                platform = platform,
                timeDeparture = departure.ToString()
            };
            departures.Add(train);
        }

        public static void AddTrainAtStation(int idTrain, int platform, TimeSpan arrival, TimeSpan departure, int passengers)
        {
            int index = arrivals.FindIndex(
              delegate (Trains trainIndex)
              {
                  return trainIndex.name == "TLK "+idTrain;
              });
            arrivals.RemoveAt(index);
            Trains train = new Trains
            {
                name = "TLK " + idTrain.ToString(),
                platform = platform,
                timeArrival = arrival.ToString(),
                timeDeparture = departure.ToString(),
                passengers = passengers
            };

            atStation.Add(train);
        }

        public static void RemoveTrain(int idTrain, int platform)
        {
            int index = departures.FindIndex(
              delegate (Trains trainIndex)
              {
                  return trainIndex.name == "TLK " + idTrain;
              });
            departures.RemoveAt(index);
        }
    }
}
