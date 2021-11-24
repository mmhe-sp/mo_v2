using MMHE.MO.Models;

using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Xml.Linq;

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
						jCSDetails.CanSubmit = row.Field<bool>("CanSubmit");
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
					jCSDetails.Resources = table.Rows.Cast<DataRow>().Select(r => new ResourceDetails
					{
						CodeID = r.Field<string>("CodeID"),
						Description = r.Field<string>("Description"),
						Type = r.Field<string>("Type")
					}).ToList();
				}
			}
			return jCSDetails;
		}

		public void Submit(Guid jCSID, string id)
		{
			SqlParameter[] parameters = new SqlParameter[2];
			parameters[0] = new SqlParameter("@JCSId", jCSID);
			parameters[1] = new SqlParameter("@SubmittedBy", id);

			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.SubmitJCSDetails", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
		}

		public void Save(JCSDetails jcsDetails, string updatedBy)
		{
			XElement root = new XElement("JCS");
			root.Add(new XElement("ID", jcsDetails.JCSID));
			if (jcsDetails.StartDate.HasValue)
				root.Add(new XElement("StartDate", jcsDetails.StartDate.Value.ToString("MM/dd/yyyy")));

			if (jcsDetails.EndDate.HasValue)
				root.Add(new XElement("EndDate", jcsDetails.EndDate.Value.ToString("MM/dd/yyyy")));
			XElement activities = new XElement("Activities");
			XElement activity;
			root.Add(activities);
			foreach (var item in jcsDetails.Activities)
			{
				activity = new XElement("Activity");
				if (item.ActivityID.HasValue)
					activity.Add(new XElement("ActivityID", item.ActivityID.Value));
				activity.Add(new XElement("Remarks", item.Remarks));
				activity.Add(new XElement("Sequence", item.Sequence));
				activity.Add(new XElement("Resource", item.Resource));
				activity.Add(new XElement("ResourceType", item.ResourceType));
				activities.Add(activity);
			}
			SqlParameter[] parameters = new SqlParameter[2];
			parameters[0] = new SqlParameter("@JCS", new SqlXml(XElement.Parse(root.ToString()).CreateReader()));
			parameters[1] = new SqlParameter("@UpdatedBy", updatedBy);

			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.SaveJCSDetails", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
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
