using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Business.Repositories
{
    public class WDRRepository
    {
        public DataTable GetAll(string loggedInUser, string subcontractor = null)
        {
            //SqlParameter[] parameters = new SqlParameter[1];
            //parameters[0] = new SqlParameter("@pEmpID", loggedInUser);
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.GetWDRSubmission", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Employeeid", loggedInUser);
                    command.Parameters.AddWithValue("@subcontractor", subcontractor); 
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }

        public DataTable GetSubcontractor(string loggedInUser)
        {
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.GetWDRSucontractor", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Employeeid", loggedInUser);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }
    }
}
