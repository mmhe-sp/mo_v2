using MMHE.MO.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Business.Repositories
{
    public class DashboardRepository
    {
        public DashboardStatistics GetAll(string loggedInUser)
        {
            DashboardStatistics dashboardStatistics = new DashboardStatistics();
          
            DataSet dataSet = new DataSet();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.GetDashboardStatistics", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@Employeeid", loggedInUser);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(dataSet);

                    dashboardStatistics.TotalJSL = dataSet.Tables[0];
                    dashboardStatistics.Statistics = dataSet.Tables[1];
                }
            }
            return dashboardStatistics;
        }
    }
}
