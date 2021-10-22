using MMHE.MO.Models;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

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
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.FindJCS", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
					sqlDataAdapter.Fill(table);
				}
			}
			return table;
		}

		public JCSDetails GetDetails(string jcsId)
		{
			JCSDetails jCSDetails = new JCSDetails();
			SqlParameter[] parameters = new SqlParameter[1];
			parameters[0] = new SqlParameter("@JCSID", jcsId);
			DataSet dataSet = new DataSet();
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.GetJCSDetails", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
					sqlDataAdapter.Fill(dataSet);

					DataTable table = dataSet.Tables[0];
					DataRow row;
					if (table.Rows.Count > 0)
					{
						row = table.Rows[0];
						jCSDetails.Description = row.Field<string>("ContractItems");
						jCSDetails.JCSID = row.Field<Guid>("JCSID");
						jCSDetails.OwnerNo = row.Field<string>("OwnerNo");
						jCSDetails.Type = row.Field<string>("Type");
						jCSDetails.Discipline = row.Field<string>("Discipline");
						jCSDetails.WorkTitle = row.Field<string>("WorkTitle");
						jCSDetails.WBS = row.Field<string>("WBS");
						jCSDetails.StartDate = row.Field<DateTime?>("StartDate");
						jCSDetails.EndDate = row.Field<DateTime?>("EndDate");
						jCSDetails.Duration = row.Field<string>("DurationDays");
					}

					table = dataSet.Tables[1];
					jCSDetails.Activities = table.Rows.Cast<DataRow>().Select(r => new JCSActivityDetails
					{
						Sequence = r.Field<string>("SequenceNo"),
						ActivityID = r.Field<Guid>("ActivityID"),
						Remarks = r.Field<string>("Remarks").Trim(),
						UpdatedBy = r.Field<string>("UpdatedBy"),
						UpdatedOn = r.Field<DateTime>("UpdatedOn"),
						Resource = r.Field<string>("Resource")
					}).ToList();

					table = dataSet.Tables[2];
					jCSDetails.Resources = table.Rows.Cast<DataRow>().Select(r => new Option
					{
						Value = r.Field<string>("CodeID"),
						Text = r.Field<string>("Description")
					}).ToList();
				}
			}
			return jCSDetails;
		}

		public void Save(JCSDetails jcsDetails)
		{
			throw new NotImplementedException();
		}

		public void Import(string projectNo, string loggedInUser, DataRow dataRow)
		{
			SqlParameter[] parameters = new SqlParameter[6];
			parameters[0] = new SqlParameter("@ProjectNo", projectNo);
			parameters[1] = new SqlParameter("@OwnerNo", dataRow[0].ToString());
			parameters[2] = new SqlParameter("@JSL", dataRow[1].ToString());
			parameters[3] = new SqlParameter("@Description", dataRow[3].ToString());
			parameters[4] = new SqlParameter("@Discipline", dataRow[2].ToString());
			parameters[5] = new SqlParameter("@CreatedBy", loggedInUser);
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.ImportJCS", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
		}
	}
}
