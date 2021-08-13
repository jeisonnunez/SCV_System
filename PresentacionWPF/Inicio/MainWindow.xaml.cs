using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();

           
        }

        public void LoadLogin()
        {
            Login ventanaLogin = new Login();

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
            ventanaLogin.BeginAnimation(UIElement.OpacityProperty, animation);

            ventanaLogin.Show();

            
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.Shutdown();
        }

        public void ShowStatusMessage(string message, SolidColorBrush solidColorBrush, SolidColorBrush foreground, string path)
        {
            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));

            StatusMessage.BeginAnimation(UIElement.OpacityProperty, animation);
            statusImg.BeginAnimation(UIElement.OpacityProperty, animation);
            StatusMessage.Foreground = foreground;
            StatusMessage.Content = message;
            StatusMessage.Background = solidColorBrush;
            StatusMessage.FontWeight = FontWeights.UltraBold;
            statusImg.Background = solidColorBrush;
            img.Source = new BitmapImage(new Uri(path, UriKind.Relative));
            var timer = new System.Timers.Timer();
            timer.Interval = 4000; //2 seconds
            timer.Elapsed += delegate (object sender, System.Timers.ElapsedEventArgs e)
            {
                //stop the timer
                timer.Stop();
                //remove the StatusMessage text using a dispatcher, because timer operates in another thread
                this.Dispatcher.BeginInvoke(new Action(() =>
                {
                    StatusMessage.Content = "";
                    StatusMessage.Background = Brushes.Transparent;
                    StatusMessage.Foreground = Brushes.Transparent;
                    statusImg.Background = Brushes.Transparent;
                    img.Source = new BitmapImage(new Uri("", UriKind.Relative));
                }));
            };
            timer.Start();
        }
    }
}
