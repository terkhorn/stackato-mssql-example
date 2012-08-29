using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelloStackato
{
    public partial class HelloPage : Page
    {
        private string ConnString { get; set; }

        private string GetConnString()
        {
            System.Configuration.Configuration rootWebConfig =
    System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/HelloStackato");
            System.Configuration.ConnectionStringSettings connString;
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                connString =
                    rootWebConfig.ConnectionStrings.ConnectionStrings["Default"];
                if (connString != null)
                {
                    return connString.ConnectionString;
                }
                
                throw new Exception("No Default connection string");
            }

            throw new Exception("Can't find connection strings in root web.config");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            ConnString = GetConnString();
            databaseInfo.Text = OpenSqlConnection(ConnString);
        }

        private static string OpenSqlConnection(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return string.Format("Current dir -- {2} -- ServerVersion: {0} -- State: {1}", connection.ServerVersion, connection.State, Directory.GetCurrentDirectory());
            }
        }
    }
}
