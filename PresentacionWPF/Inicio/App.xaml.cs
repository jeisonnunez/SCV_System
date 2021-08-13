using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Controls.Primitives;
using System.Windows.Media.Animation;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static MainWindow mainWindow;
        static public void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

            e.Cancel = true;

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                                 (Duration)TimeSpan.FromSeconds(1));
            (sender as Window).BeginAnimation(UIElement.OpacityProperty, animation);

            (sender as Window).Hide();


        }

       static public void textBox_GotFocus(object sender, RoutedEventArgs e)
        {

            (sender as TextBox).Background = Brushes.LightBlue;
        }

       static public void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as TextBox).Background = Brushes.White;
        }

        static public void password_GotFocus(object sender, RoutedEventArgs e)
        {

            (sender as PasswordBox).Background = Brushes.LightBlue;
        }

        static public void password_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as PasswordBox).Background = Brushes.White;
        }

        static public void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {

            (sender as ComboBox).Background = Brushes.LightBlue;
        }

        static public void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBox).Background = Brushes.White;
        }

        static public void comboBoxItem_GotFocus(object sender, RoutedEventArgs e)
        {

            (sender as ComboBoxItem).Background = Brushes.LightBlue;
        }

        static public void comboBoxItem_LostFocus(object sender, RoutedEventArgs e)
        {
            (sender as ComboBoxItem).Background = Brushes.White;
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow ventanaPrincipal = new MainWindow();

            mainWindow = ventanaPrincipal;

            DoubleAnimation animation = new DoubleAnimation(0, 1,
                               (Duration)TimeSpan.FromSeconds(1));
            ventanaPrincipal.BeginAnimation(UIElement.OpacityProperty, animation);

            ventanaPrincipal.Show();

            ventanaPrincipal.WindowState = WindowState.Maximized;

            ventanaPrincipal.Width = SystemParameters.PrimaryScreenWidth;

            ventanaPrincipal.Height = SystemParameters.PrimaryScreenHeight;

            ventanaPrincipal.LoadLogin();
        }

        public static MainWindow GetMainWindowStatusBar()
        {
            return mainWindow;
        }
    }
}
