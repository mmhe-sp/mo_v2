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
	public class ProjectsRepository
	{
		public List<Option> FindProjects(string project)
		{
			SqlParameter[] parameters = new SqlParameter[3];
			parameters[0] = new SqlParameter("@project", project);
			DataTable table = new DataTable();
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.FindProjects", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
					sqlDataAdapter.Fill(table);
				}
			}
			return table.Rows.Cast<DataRow>().Select(d => new Option
			{
				Value = d.Field<string>("ProjectId"),
				Text = d.Field<string>("ProjectName")
			}).ToList();
		}
	}
}
