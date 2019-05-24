using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Diagnostics;

namespace PoznanMainStation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static List<IRunnable> runnables = new List<IRunnable>();
        private static Station station = new Station();
        private static List<Thread> threads = new List<Thread>();
        static int numberOfTrains = 10;    //Liczba wygenerowanych pociągów

        public static Mutex mutex = null;

        static void GenerateRunnables()
        {
            runnables.Add(station);
            for (int i=0;i< numberOfTrains; i++)
            {
                runnables.Add(new Train());
            }
        }

        static void RunThreads()
        {
            foreach (IRunnable a in runnables)
            {
                threads.Add(new Thread(new ThreadStart(a.Run)));
            }
            foreach (Thread t in threads)
            {
                t.Start();
            }
            foreach (Thread t in threads)
            {
                t.Join();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            mutex = new Mutex(false);
            GenerateRunnables();
            RunThreads();

            Debug.WriteLine("Funkcja głowna");
        }
    }
}
