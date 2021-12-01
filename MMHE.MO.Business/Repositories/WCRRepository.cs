using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Business.Repositories
{
    public class WCRRepository
    {
        public DataTable GetAll(string loggedInUser)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.GetWCRSubmission", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Employeeid", loggedInUser);
                    //command.Parameters.AddRange(parameters);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }
    }
}
