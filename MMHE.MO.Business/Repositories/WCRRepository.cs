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
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }

        public bool UpdateWCRStatus(string loggedInUser, string ownerNo)
        {            
            try
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@Employeeid", loggedInUser);
                parameters[1] = new SqlParameter("@OwnerNo", ownerNo);
                using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
                {
                    using (SqlCommand command = new SqlCommand("MO.UpdateWCRStatus", connection))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddRange(parameters);
                        connection.Open();
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            } 
        }

        //public bool CheckWCRStatus(string loggedInUser, string ownerNo)
        //{
        //    string status = "";
        //    try
        //    {               
        //        SqlParameter[] parameters = new SqlParameter[2];
        //        parameters[0] = new SqlParameter("@Employeeid", loggedInUser);
        //        parameters[1] = new SqlParameter("@OwnerNo", ownerNo);
        //        using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
        //        {
        //            using (SqlCommand command = new SqlCommand("MO.CheckWCRStatus", connection))
        //            {
        //                command.CommandType = CommandType.StoredProcedure;
        //                command.Parameters.AddRange(parameters);
        //                connection.Open();
        //                command.ExecuteNonQuery();
        //                status = (string)command.Parameters["@DtWCRAct"].Value;
        //                connection.Close();
        //            }
        //        }
        //        if (status == "1")
        //        {
        //            return false;
        //        }
        //        else
        //        {
        //            return true;
        //        }
                
        //    }
        //    catch(Exception ex)
        //    {
        //        return false;
        //    }
        //}
    }
}
