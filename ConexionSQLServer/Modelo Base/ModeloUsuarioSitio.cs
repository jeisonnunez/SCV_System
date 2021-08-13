using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Datos.Modelo_Base
{
    public class ModeloUsuarioSitio: ConexionSQLServer
    {
        private SqlConnection sqlConnection;

        public SqlConnection SqlConnection { get => sqlConnection; set => sqlConnection = value; }

        private string server;
        public string Server { get => server; set => server = value; }

        private string connectionString1;
        public string ConnectionString { get => connectionString1; set => connectionString1 = value; }
        public Tuple<bool, string> VerifyUserAdministratorSQLServer(string user, string password, string server)
        {
            bool sw = false;

            string error = null;

            try
            {
                Server = server; //verificar con instancia sql server

                ServerName = server;

                ConnectionString = String.Format("Server={0}; User ID={1}; Password={2}", Server, user, password);

                SqlServerConnectionString(ConnectionString);

                if (IsConnection == true)
                {
                    sw = true;
                }
                else
                {
                    sw = false;
                }

                return Tuple.Create(sw, error);

            }
            catch (Exception e)
            {
                return Tuple.Create(sw, e.Message);
            }


        }

        private void SqlServerConnectionString(string connectionString)
        {
            SqlConnection = new SqlConnection(connectionString);
        }

        public bool IsConnection
        {
            get
            {
                if (SqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    SqlConnection.Open();
                }

                return true;
            }
        }

        
    }
}
