using Entidades;
using Negocio;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using Vista.Finanzas;

namespace Vista
{
    /// <summary>
    /// Lógica de interacción para TratarPlanCuentas.xaml
    /// </summary>
    public partial class TratarPlanCuentas : Document
    {
        ControladorPlanCuentas cn = new ControladorPlanCuentas();

        private string fatherNum;

        private bool sw;

        private string account;

        private int groupMask;

        public ObservableCollection<string> TipoCuenta { get; set; }
        public string FatherNum { get => fatherNum; set => fatherNum = value; }
        public bool Sw { get => sw; set => sw = value; }
        public string Account { get => account; set => account = value; }
        public int GroupMask { get => groupMask; set => groupMask = value; }        

        DataTable dt = new DataTable();

        List<ModelPlanCuentas> listCuentas = new List<ModelPlanCuentas>();

        TreeViewItem TreeViewItem;

        public DataSet dtSet = null;
        public TratarPlanCuentas()
        {
            InitializeComponent();
        }

        private void InicializacionBasica()
        {
           ClearNodes();
            
           var result= cn.ConsultaCuentas();

            if (result.Item2 == null)
            {
                dtSet = result.Item1;

                cn.CreateFatherAccount("0", null, dtSet, treeViewCuentas);

                dtSet.Clear();

                Sw = false;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizacion de la cuenta: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

        }

        private void ClearNodes()
        {
            treeViewCuentas.Items.Clear();           
        }

        private void comboBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_LostFocus(sender, e);
        }

        private void comboBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.comboBox_GotFocus(sender, e);
        }

        private void textBox_LostFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_LostFocus(sender, e);
        }

        private void textBox_GotFocus(object sender, RoutedEventArgs e)
        {
            App.textBox_GotFocus(sender, e);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            InicializacionBasica();
            
            App.Window_Closing(sender, e);
        }

        private void btnWindow_Close(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();
           
            this.Hide();
        }

        private void treeViewCuentas_Selected(object sender, RoutedEventArgs e)
        {           

            if (btnOk.Content.ToString() == "OK")
            {
                TreeViewItem = treeViewCuentas.SelectedItem as TreeViewItem;

                SelectedAccount(TreeViewItem);
            }
        }

        private void SelectedAccount(TreeViewItem item)
        {
            //listaCuentas.Clear();

            Account = item.Tag.ToString();

            var result = cn.FindCuenta(account);

            if (result.Item2 == null)
            {
                dt = result.Item1.Copy();

                listCuentas = ConvertDataTable<ModelPlanCuentas>(dt);

                GetAccount(listCuentas);

                item.IsSelected = false;
            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }


        }

        private void EnableButton(bool? tipocuenta)
        {
            if (tipocuenta == true)
            {
                btnAddCuentaNivel.IsEnabled = true;
                btnAddCuentaSubordinada.IsEnabled = true;
            }
            else
            {
                btnAddCuentaNivel.IsEnabled = true;
                btnAddCuentaSubordinada.IsEnabled = false;
            }
        }

        private void GetAccount(List<ModelPlanCuentas> list)
        {
            foreach (ModelPlanCuentas cuenta in list)
            {
                txtCuenta.Text = cuenta.AcctCode;
                txtNombre.Text = cuenta.AcctName;
                txtNivel.Text = cuenta.Levels.ToString();    
                rbCuentaActiva.IsChecked = cuenta.Postable;
                rbTitulo.IsChecked = cuenta.Postable;
                FatherNum = cuenta.FatherNum.ToString();
                GroupMask = cuenta.GroupMask;

                EnableButton(rbTitulo.IsChecked);
                //VerificaCuenta(rbTitulo.IsChecked);

              
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            InicializacionBasica();

        }

        public void LoadedWindow()
        {
            InicializacionBasica();
        }

        private void btnAddCuentaNivel_Click(object sender, RoutedEventArgs e)
        {
            Sw = true;

            SetLevel(Convert.ToInt32(txtNivel.Text));

            LimpiarCampos();

            cn.CreateNodesNivel(TreeViewItem);

            var result = cn.FindFatherNum(Account);

            if (result.Item2 == null)
            {
                FatherNum = result.Item1;

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
            }

            btnOk.Content = "Crear";

        }

        private void LimpiarCampos()
        {
            rbCuentaActiva.IsChecked = true;

            txtCuenta.Text = "";

            txtNombre.Text = "";
        }

        private void btnAddCuentaSubordinada_Click(object sender, RoutedEventArgs e)
        {
            Sw = true;

            SetLevel(Convert.ToInt32(txtNivel.Text)+1);

            LimpiarCampos();

            cn.CreateNodesSubordinada(TreeViewItem);

            FatherNum = Account;

            btnOk.Content = "Crear";
        }

        private void SetLevel(int levels)
        {
            txtNivel.Text = levels.ToString();
        }

        private void txtCuenta_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (btnOk.Content.ToString() != "Crear")
            {
                btnOk.Content = "Actualizar";
            }
           
        }

        private void rbCuentaActiva_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() != "Crear")
            {
                btnOk.Content = "Actualizar";
            }
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            List<Cuenta> listaCuenta = new List<Cuenta>();

            Cuenta cuenta = new Cuenta();

            switch (btnOk.Content.ToString())
            {
                case "OK":

                    InicializacionBasica();

                    this.Hide();

                    break;

                case "Crear":

                    cuenta.AcctCode = txtCuenta.Text;
                    cuenta.AcctName = txtNombre.Text;
                    cuenta.ActCurr = "VES";
                    cuenta.LocManTran = 'N';
                    cuenta.UserSign = Properties.Settings.Default.Usuario;
                    cuenta.ActType = 'N';
                    cuenta.CreateDate = fechaActual.GetFechaActual();
                    cuenta.FatherNum = FatherNum;
                    cuenta.CurrTotal = 0;
                    cuenta.Finanse = 'N';
                    cuenta.Levels = Convert.ToInt32(txtNivel.Text);
                    cuenta.SysTotal = 0;
                    cuenta.FcTotal = 0;
                    cuenta.Advance = 'N';
                    cuenta.GroupMask = GroupMask;

                        if (rbCuentaActiva.IsChecked == true)
                        {
                            cuenta.Postable = 'Y';
                        }
                        else
                        {
                            cuenta.Postable = 'N';
                        }

                        if (rbTitulo.IsChecked == true)
                        {
                            cuenta.Postable = 'N';
                        }
                        else
                        {
                            cuenta.Postable = 'Y';
                        }


                    listaCuenta.Add(cuenta);

                    var result = cn.InsertAccount(listaCuenta);

                    if (result.Item1 == 1)
                    {

                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Cuenta " + cuenta.AcctCode +" se creo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");                       

                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la creacion de la cuenta " + cuenta.AcctCode + ": " + result.Item2 , Brushes.Red, Brushes.White, "003-interface-2.png");
                       
                    }
                                      
                    InicializacionBasica();

                    break;


                case "Actualizar":

                    cuenta.AcctCode = txtCuenta.Text;
                    cuenta.AcctName = txtNombre.Text;
                    //cuenta.ActCurr = "VES";
                    //cuenta.LocManTran = 'N';
                    cuenta.UserSign = Properties.Settings.Default.Usuario;
                    //cuenta.ActType = 'N';
                    cuenta.CreateDate = fechaActual.GetFechaActual();
                    cuenta.FatherNum = FatherNum;

                    if (rbCuentaActiva.IsChecked == true)
                    {
                        cuenta.Postable = 'Y';
                    }
                    else
                    {
                        cuenta.Postable = 'N';
                    }

                    if (rbTitulo.IsChecked == true)
                    {
                        cuenta.Postable = 'N';
                    }
                    else
                    {
                        cuenta.Postable = 'Y';
                    }


                    listaCuenta.Add(cuenta);

                    var result1 = cn.UpdateAccountTratar(listaCuenta);

                    if (result1.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Cuenta " + cuenta.AcctCode + " se actualizo correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");
                                                
                        btnOk.Content = "OK";
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Error en la actualizarcion de la cuenta " + cuenta.AcctCode + ": " + result1.Item2, Brushes.Red, Brushes.Black, "003-interface-2.png");
                    }

                    InicializacionBasica();

                    break;
            }
        }

        private void treeViewCuentas_GotFocus(object sender, RoutedEventArgs e)
        {
            //treeViewCuentas.Background = Brushes.Coral;

           
        }

        private void Deleted_Click(object sender, RoutedEventArgs e)
        {
            if (btnOk.Content.ToString() == "OK" && String.IsNullOrWhiteSpace(txtCuenta.Text) == false)
            {

                MessageBoxResult messageBoxResult = MessageBox.Show("¿Desea eliminar la cuenta?", "Item", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    string account = txtCuenta.Text;

                    var result = cn.DeleteAccount(account);

                    if (result.Item1 == 1)
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("Cuenta: " + account + " se elimino correctamente", Brushes.LightGreen, Brushes.Black, "001-interface.png");

                        LimpiarCampos();

                        InicializacionBasica();
                    }
                    else
                    {
                        EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se puede eliminar la cuenta: " + account + " porque se realizo una transaccion con la misma: " + result.Item2, Brushes.Red, Brushes.White, "003-interface-2.png");
                    }
                }

            }
            else
            {
                EstableceLogin.GetMenuStatusBar().ShowStatusMessage("No se selecciono ningun articulo", Brushes.LightBlue, Brushes.Black, "002-interface-1.png");
            }
        }
    }
}
