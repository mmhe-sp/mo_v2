using MMHE.MO.Models;

using System;
using System.Data;
using System.Data.SqlClient;

namespace MMHE.MO.Business.Repositories
{
	public class UserRepository
	{
		public IdentityUser GetUserDetails(string userName)
		{
			DataTable dt = new DataTable();
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("EmployeeId", connection))
				{

					connection.Open();
					command.Parameters.AddWithValue("@LoginName", userName);
					SqlDataAdapter da = new SqlDataAdapter(command);
					da.Fill(dt);
					connection.Close();
				}
			}
			IdentityUser user = new IdentityUser();
			if (dt.Rows.Count > 0)
			{
				user.Id = Convert.ToInt32(dt.Rows[0]["EmployeeId"].ToString());
				user.Name = dt.Rows[0]["Name"].ToString();
				user.Email = dt.Rows[0]["Email"].ToString();
				user.WBS = dt.Rows[0]["WBS"].ToString();
				user.ProjectId = dt.Rows[0]["ProjectId"].ToString();
				user.ProjectName = dt.Rows[0]["ProjectName"].ToString();
			}
			return user;
		}
	}
}
