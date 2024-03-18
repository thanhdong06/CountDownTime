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
using System.Windows.Threading;

namespace CountDownTime
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private DispatcherTimer timer;
        private int Inicountdown;
        private int countdown;
        public MainWindow()
        {
            InitializeComponent();
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            Inicountdown = 60;
            ResetCountdown();

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan time = TimeSpan.FromSeconds(Inicountdown);
            CountdownLabel.Content = $"Countdown: {time.ToString(@"mm\:ss")}";
            Inicountdown--;
            if (Inicountdown < 0)
            {
                timer.Stop();
                if (AlarmCheckBox.IsChecked == true)
                {
                    PlayAlarmSound(); // Phát nhạc chuông
                    ShowNewYearGreetingWindow();
                }
                else
                {
                    ShowNewYearGreetingWindow();
                }               
            }
        }
        private void ShowNewYearGreetingWindow()
        {
            HPNY greetingWindow = new HPNY();
            greetingWindow.ShowDialog();
        }

        private void Btn_Start_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(CountdownInput.Text, out int seconds))
            {
                if (seconds > 0)
                {
                    Inicountdown = seconds;
                    ResetCountdown();
                    timer.Start();
                }
                else
                {
                    MessageBox.Show("Please enter a valid positive number of seconds.");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of seconds.");
            }
        }
        private void ResetCountdown()
        {
            countdown = Inicountdown;
            TimeSpan time = TimeSpan.FromSeconds(countdown);
            CountdownLabel.Content = $"Countdown: {time.ToString(@"mm\:ss")}";
        }
        private void PlayAlarmSound()
        {
            MediaPlayer mediaPlayer = new MediaPlayer();
            mediaPlayer.Open(new Uri("sound/iphone_15.wav", UriKind.Relative));
            mediaPlayer.Play();
        }

        private void AlarmCheckBox_Checked(object sender, RoutedEventArgs e)
        {        
        }
        private void AlarmCheckBox_UnChecked(object sender, RoutedEventArgs e)
        {           
        }
    }
}
