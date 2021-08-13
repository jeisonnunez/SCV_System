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
using System.Windows.Shapes;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para LogMensajes.xaml
    /// </summary>
    public partial class LogMensajes : Window
    {
        private string WindowName;
        public LogMensajes()
        {
            InitializeComponent();
        }


       
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetWindowName(sender);

            InicializacionBasic();
        }

        private void GetWindowName(object sender)
        {
            Window window = (Window)sender;

            WindowName = window.Tag.ToString();
        }

        public void LoadedWindow()
        {
            InicializacionBasic();
        }

        private void InicializacionBasic()
        {
            dgLogs.ItemsSource = Menu.dtMessages.DefaultView;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            App.Window_Closing(sender, e);
        }
    }
}
