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
        private string GetConnString()
        {
            var rootWebConfig = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("/HelloStackato");
            if (rootWebConfig.ConnectionStrings.ConnectionStrings.Count > 0)
            {
                var connectionStringSettings = rootWebConfig.ConnectionStrings.ConnectionStrings["Default"];
                if (connectionStringSettings != null)
                {
                    return connectionStringSettings.ConnectionString;
                }

                throw new Exception("No Default connection string");
            }

            throw new Exception("Can't find connection strings in root web.config");
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            databaseInfo.Text = ExecuteSQLStatements(GetConnString());
        }

        private static string ExecuteSQLStatements(string connectionString)
        {
            string insertSQL =
                string.Format(
                    @"INSERT INTO dbo.Sample (Text) VALUES ('Hello at {0} UTC');
                                         SELECT CAST(scope_identity() AS int)",
                    DateTime.UtcNow);

            using (var connection = new SqlConnection(connectionString))
            {
                var insertCmd = new SqlCommand(insertSQL, connection);
                connection.Open();
                var resultID = (Int32) insertCmd.ExecuteScalar();

                string selectSQL = string.Format(@"SELECT Text FROM dbo.Sample WHERE ID = {0}", resultID);

                var selectCmd = new SqlCommand(selectSQL, connection);

                return (string) selectCmd.ExecuteScalar();
            }
        }
    }
}
