using System;
using System.Timers;
using System.Windows.Controls;

namespace LoginPage.View
{
    /// <summary>
    /// Interaction logic for InfoView.xaml
    /// </summary>
    public partial class InfoView : UserControl
    {
        public static int m_counter = 0;
        public static Timer timer;

        DateTime _defaultExpireMinutes = DateTime.Now.AddSeconds(10);

        public InfoView()
        {
            InitializeComponent();
            timer = new Timer(interval: 1000);
            timer.Start();
            timer.Elapsed += OnTimerElapsed;

        }
        public int counter = 0;



        private void OnTimerElapsed(object sender, ElapsedEventArgs e)
        {
            m_counter++;
            if (m_counter == 1200)
            {
                Console.WriteLine("Timer stopped");
                timer.Stop();
            }
            Console.WriteLine("Counter" + m_counter);
            text1.Dispatcher.Invoke(() =>
            {
                text1.Text = "counter: " + m_counter;

            });
        }

        private void btnSave_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
