using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using HashCode;
using Negocio;


namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para Registro.xaml
    /// </summary>
    public partial class Registro : Window
    {
        Negocios cn = new Negocios();
                
        public Registro()
        {
            InitializeComponent();          
        }

        private void btnRegistro_Click(object sender, RoutedEventArgs e)
        {

           if( cn.regUsuario(txtUser.Text, txtPassword.Text, tXtFullName.Text) == 1)
            {
                MessageBox.Show("User Added to DataBase");
            }
            else
            {
                MessageBox.Show("Error");
            }

        }
    }
}
