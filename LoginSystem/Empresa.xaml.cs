using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Negocio;

namespace Presentacion
{
    /// <summary>
    /// Lógica de interacción para Empresa.xaml
    /// </summary>
    public partial class Empresa : Window
    {
        Negocios cn = new Negocios();
        public Empresa()
        {
            InitializeComponent();
        }

        private void btnCrearEmpresa_Click(object sender, RoutedEventArgs e)
        {
            if (cn.CrearEmpresa(txtEmpresa.Text) == -1)
            {
                MessageBox.Show("La base de datos " + txtEmpresa.Text + " se creo correctamente");
            }
            else
            {
                MessageBox.Show("Error en la creacion de la base de datos");
            }
           
        }
    }
}
