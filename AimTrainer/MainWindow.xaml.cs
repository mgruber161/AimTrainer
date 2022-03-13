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

namespace AimTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        protected readonly DispatcherTimer timer1 = new DispatcherTimer();
        protected readonly DispatcherTimer timer2 = new DispatcherTimer();
        protected readonly DispatcherTimer timer3 = new DispatcherTimer();
        Random random = new Random(DateTime.Now.Millisecond);
        private int counter = 0;
        int timercount = 60;
        double ballwidth = 30;
        public MainWindow()
        {
            InitializeComponent();
            timer1.Interval = TimeSpan.FromMilliseconds(2500);
            timer1.Tick += Timer_Tick;
            timer2.Interval = TimeSpan.FromMilliseconds(1000);
            timer2.Tick += Timer2_Tick;
            timer3.Interval = TimeSpan.FromMilliseconds(1);
            timer3.Tick += Timer3_Tick;
            DifficultySlider.Minimum = 10;
            DifficultySlider.Maximum = 50;
            DifficultySlider.Value = 30;
        }

        private void Timer3_Tick(object? sender, EventArgs e)
        {
            Ball.Width = Ball.ActualHeight * 0.985;
            Ball.Height = Ball.ActualWidth * 0.985;
        }

        private void Timer2_Tick(object? sender, EventArgs e)
        {
            if(timercount==0)
            {
                timer1.Stop();
                timer2.Stop();
                timer3.Stop();
                DifficultySlider.IsEnabled = true;
            }
            else
            {
                timercount--;
                SecondCountLabel.Content = timercount.ToString();
            }
        }

        private void Timer_Tick(object? sender, EventArgs e)
        {
            MoveBallToRandomPos();
        }

        private void MoveBallToRandomPos()
        {
            timer1.Stop();
            timer1.Start();

            Ball.Width = ballwidth;
            Ball.Height = ballwidth;

            var posX = random.Next(0, (int)(Court.ActualWidth - Ball.Width));
            var posY = random.Next(0, (int)(Court.ActualHeight - Ball.Height));

            Canvas.SetLeft(Ball, posX);
            Canvas.SetTop(Ball, posY);
        }

        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer1.Start();
            timer2.Start();
            timer3.Start();
            DifficultySlider.IsEnabled = false;
            MoveBallToRandomPos();
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            timer1.Stop();
            timer2.Stop();
            timer3.Stop();
            DifficultySlider.IsEnabled= true;
            counter = 0;
            timercount = 60;
            ClickCountLabel.Content = counter.ToString() + " Hits";
            SecondCountLabel.Content = timercount.ToString();
            
        }

        private void Ball_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(timer1.IsEnabled)
            {
                counter++;
                ClickCountLabel.Content = counter.ToString() + " Hits";
                MoveBallToRandomPos();
            }
            e.Handled = true;
        }

        private void Court_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if(timer1.IsEnabled)
                MoveBallToRandomPos();
        }

        private void DifficultySlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ballwidth = DifficultySlider.Value;
            Ball.Width = ballwidth;
            Ball.Height = ballwidth;
        }
    }
}
