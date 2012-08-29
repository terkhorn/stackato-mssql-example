using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HelloStackato
{
    public partial class HelloPage : Page
    {
        private const string ConnString = @"foo";

        protected void Page_Load(object sender, EventArgs e)
        {
            databaseInfo.Text = OpenSqlConnection(ConnString);
        }

        private static string OpenSqlConnection(string connectionString)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                return string.Format("ServerVersion: {0} -- State: {1}", connection.ServerVersion, connection.State);
            }
        }
    }
}
