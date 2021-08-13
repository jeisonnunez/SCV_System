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
using Negocio;
using HashCode;


namespace Presentacion
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        Negocios cn = new Negocios();

        HashCodes hc = new HashCodes();
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if (cn.conSQL(textBox.Text, textBox1.Text) != null)
            {
                MessageBox.Show("Login Exitoso");
              
            }
            else
            {
                MessageBox.Show("Error");               
            }
         
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {
            Registro registro = new Registro();

            registro.ShowDialog();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnCrearEmpresa_Click(object sender, RoutedEventArgs e)
        {
            Empresa empresa = new Empresa();

            empresa.ShowDialog();
        }
    }
}
