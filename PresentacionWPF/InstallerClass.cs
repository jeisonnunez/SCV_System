using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Configuration.Install;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows;

namespace Vista
{
    [RunInstaller(true)]
    public partial class InstallerClass : System.Configuration.Install.Installer
    {
        public SqlConnection cn;
        public InstallerClass()
        {
            InitializeComponent();
        }

        public void Helper(string connectionString)
        {
            cn = new SqlConnection(connectionString);
        }

        public bool IsConnection
        {
            get
            {
                if (cn.State == System.Data.ConnectionState.Closed)
                {
                    cn.Open();
                }

                return true;
            }
        }



        public override void Install(IDictionary stateSaver)
        {


            string connectionString = String.Format("Server={0}; User ID={1}; Password={2}", Context.Parameters["Servidor"], Context.Parameters["Usuario"], Context.Parameters["Contrasena"]);

            try
            {
                Helper(connectionString);

                if (IsConnection == true)
                {
                    MessageBox.Show("Connection Success", "Install", MessageBoxButton.OK, MessageBoxImage.Information);

                    string dataSource = "Server =" + Context.Parameters["Servidor"];
                    string userid = "User Id=" + Context.Parameters["Usuario"];
                    string password = "Password=" + Context.Parameters["Contrasena"];

                    dataSource = dataSource + ";" + userid + ";" + password;
                    dataSource = dataSource + ";integrated security=false;";

                    ExeConfigurationFileMap map = new ExeConfigurationFileMap();
                    //MessageBox.Show(Assembly.GetExecutingAssembly().Location + ".config");
                    //Getting the path location 
                    string configFile = string.Concat(Assembly.GetExecutingAssembly().Location, ".config");
                    map.ExeConfigFilename = configFile;
                    System.Configuration.Configuration config = System.Configuration.ConfigurationManager.
                    OpenMappedExeConfiguration(map, System.Configuration.ConfigurationUserLevel.None);
                    string connectionsection = config.ConnectionStrings.ConnectionStrings
                    ["cn"].ConnectionString;

                    ConnectionStringSettings connectionstring = null;
                    if (connectionsection != null)
                    {
                        config.ConnectionStrings.ConnectionStrings.Remove("cn");
                        MessageBox.Show("Removiendo conexion de string existente", "Install", MessageBoxButton.OK, MessageBoxImage.Information);
                    }

                    connectionstring = new ConnectionStringSettings("cn", dataSource);
                    config.ConnectionStrings.ConnectionStrings.Add(connectionstring);

                    config.Save(ConfigurationSaveMode.Modified, true);
                    ConfigurationManager.RefreshSection("connectionStrings");

                    base.Install(stateSaver);
                }
                else
                {
                    throw new InstallException("Error General");
                }

            }

            catch (InstallException ex)
            {
                MessageBox.Show(ex.Message, "Install", MessageBoxButton.OK, MessageBoxImage.Error);

                Rollback(stateSaver);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Install", MessageBoxButton.OK, MessageBoxImage.Error);

                Rollback(stateSaver);
            }


        }
    }
}
