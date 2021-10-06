using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Business.Repositories
{
    public class JCSRepository
    {
        public DataTable GetAll(string project, string loggedInUser)
        {
            SqlParameter[] parameters = new SqlParameter[3];
            parameters[0] = new SqlParameter("@pType", "Active");
            parameters[1] = new SqlParameter("@pProNo", project);
            parameters[2] = new SqlParameter("@pEmpID", loggedInUser);
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringMO"].ConnectionString))
            {
                using (SqlCommand command = new SqlCommand("SP_GetJCSADetails", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }
    }
}
