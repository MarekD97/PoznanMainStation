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
using System.ComponentModel;

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
        Timer m_Timer;
        static string tempString = "";

        private BackgroundWorker m_oBackgroundWorker = null;

        static void GenerateRunnables()
        {
            runnables.Add(station);
            for (int i = 0; i < numberOfTrains; i++)
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

        public void WriteToLog()
        {
            for( int i=0; i < 10; i++)
            {
                Debug.WriteLine("Iteration: " + i);
            }
        }

        public MainWindow()
        {
            InitializeComponent();


            //You have to Init the Dispatcher in the UI thread! 
            //init once per application (if there is only one Dispatcher).
            //ThreadInvoker.Instance.InitDispacter();
           // m_Timer = new Timer(TimerCallback, null, 1000, 1000);

            //GenerateRunnables();
            //RunThreads();

            //Debug.WriteLine("Funkcja głowna");
        }

        private void ThreadStart_Click(object sender, RoutedEventArgs e)
        {
            if (null == m_oBackgroundWorker)
            {
                m_oBackgroundWorker = new BackgroundWorker();
                m_oBackgroundWorker.DoWork +=
                    new DoWorkEventHandler(m_oBackgroundWorker_DoWork);
                m_oBackgroundWorker.RunWorkerCompleted +=
                    new RunWorkerCompletedEventHandler(
                    m_oBackgroundWorker_RunWorkerCompleted);
                m_oBackgroundWorker.ProgressChanged +=
                    new ProgressChangedEventHandler(m_oBackgroundWorker_ProgressChanged);
                m_oBackgroundWorker.WorkerReportsProgress = true;
                m_oBackgroundWorker.WorkerSupportsCancellation = true;
            }
            //pbProgress.Value = 0;
            //txtLog.Text = "Uruchomiono zadanie.\n";
            m_oBackgroundWorker.RunWorkerAsync();
        }

        private void ThreadStop_Click(object sender, RoutedEventArgs e)
        {
            if ((null != m_oBackgroundWorker) && m_oBackgroundWorker.IsBusy)
            {
                m_oBackgroundWorker.CancelAsync();
            }
        }

        private void m_oBackgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ///this.label1.Text = "The answer is: " + e.Result.ToString();
            //this.button1.Enabled = true;
             Debug.Write("m_oBackgroundWorker_RunWorkerCompleted ");

        }

        void m_oBackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int nCounter = 1; nCounter <= 100; ++nCounter)
            {
                if (m_oBackgroundWorker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                Thread.Sleep(1000);
                Debug.Write("_DoWork " + nCounter);
                m_oBackgroundWorker.ReportProgress(nCounter);
                tempString = String.Format("nCounter = {0}\n", nCounter);
            }
        }

        void m_oBackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            /*
            if (0 == (e.ProgressPercentage % 5))
            {
                AppendLog(e.ProgressPercentage.ToString() + "%\n");
            }
            pbProgress.Value = e.ProgressPercentage;
            */

            Debug.Write("ProcessChanged");
            logThreadTextBox.Text += tempString;
        }

        private void AppendLog(string sText)
        {
           Debug.WriteLine(sText);
        }

        /*
        protected void TimerCallback(object state)
        {
            var numberOfChars = ThreadInvoker.Instance.RunByUiThread(() =>
            {
                GenerateRunnables();
                RunThreads();
                //Thread thread = new Thread(WriteToLog);
                //thread.Start();
                //thread.Join();

                return 1;//return number of chars written.
            });

        }
        */
    }
}
