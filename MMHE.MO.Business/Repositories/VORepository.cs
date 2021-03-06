using MMHE.MO.Models;

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MMHE.MO.Business.Repositories
{
	public class VORepository
	{

		public void Save(VODetails jcsDetails, int status, string projectNo, string updatedBy)
		{
			XElement root = new XElement("JCS");
			if (jcsDetails.JCSID.HasValue && jcsDetails.JCSID != Guid.Empty)
				root.Add(new XElement("ID", jcsDetails.JCSID));
			root.Add(new XElement("Type", jcsDetails.Type));
			root.Add(new XElement("ProjectNo", projectNo));
			root.Add(new XElement("OwnerNo", jcsDetails.OwnerNo));
			root.Add(new XElement("Discipline", jcsDetails.Discipline));
			root.Add(new XElement("WorkTitle", jcsDetails.WorkTitle));
			root.Add(new XElement("WBS", jcsDetails.WBS));
			root.Add(new XElement("WFStatus", status));
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
			SqlParameter[] parameters = new SqlParameter[3];
			parameters[0] = new SqlParameter("@JCS", new SqlXml(XElement.Parse(root.ToString()).CreateReader()));
            parameters[1] = new SqlParameter("@UpdatedBy", updatedBy);
            parameters[2] = new SqlParameter("@JCSID",Guid.Empty);
            parameters[2].Direction = ParameterDirection.Output;

			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.SaveVODetails", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
                    jcsDetails.JCSID = Guid.Parse(parameters[2].Value.ToString());
				}
			}
		}

		public void FinalApprove(VODetails vo, string projectId, string loggedInUser)
		{
			SqlParameter[] parameters = new SqlParameter[2];
			parameters[0] = new SqlParameter("@JCSID", vo.JCSID);
			parameters[1] = new SqlParameter("@UserId", loggedInUser);
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.ApproveVO", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
		}

        public void Print(string jcsId, string projectId, string loggedInUser)
		{
			SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@JCSID", jcsId);
			parameters[1] = new SqlParameter("@UserId", loggedInUser);
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.PrintVO", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					connection.Open();
					command.ExecuteNonQuery();
					connection.Close();
				}
			}
		}

		public VODetails GetVODetails(string jcsId, string voId, string project, string loggedInUser)
		{
			VODetails voDetails = new VODetails();
			SqlParameter[] parameters = new SqlParameter[4];
            if (string.IsNullOrWhiteSpace(jcsId))
                parameters[0] = new SqlParameter("@JCSID", DBNull.Value);
            else
                parameters[0] = new SqlParameter("@JCSID", jcsId);
			if (string.IsNullOrWhiteSpace(voId))
				parameters[1] = new SqlParameter("@VOID", DBNull.Value);
			else
				parameters[1] = new SqlParameter("@VOID", voId);
			parameters[2] = new SqlParameter("@ProjectNo", project);
			parameters[3] = new SqlParameter("@UserId", loggedInUser);
			DataSet dataSet = new DataSet();
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.GetVODetails", connection))
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
						voDetails.Description = row.Field<string>("ContractItems");
						voDetails.JCSID = row.Field<Guid>("JCSID");
						voDetails.OwnerNo = row.Field<string>("OwnerNo");
						voDetails.Type = row.Field<string>("Type");
						voDetails.Discipline = row.Field<string>("Discipline");
						voDetails.WorkTitle = row.Field<string>("WorkTitle");
						voDetails.WBS = row.Field<string>("WBS");
						voDetails.StartDate = row.Field<DateTime?>("StartDate");
						voDetails.EndDate = row.Field<DateTime?>("EndDate");
						voDetails.Duration = row.Field<string>("DurationDays");
						voDetails.CanPrint = row.Field<bool>("CanPrint");
						voDetails.CanApprove = row.Field<bool>("CanApprove");
						voDetails.CanSubmit = row.Field<bool>("CanSubmit");
					}

					table = dataSet.Tables[1];
					voDetails.Activities = table.Rows.Cast<DataRow>().Select(r => new JCSActivityDetails
					{
						Sequence = r.Field<string>("SequenceNo"),
						ActivityID = r.Field<Guid>("ActivityID"),
						Remarks = r.Field<string>("Remarks").Trim(),
						UpdatedBy = r.Field<string>("UpdatedBy"),
						UpdatedOn = r.Field<DateTime>("UpdatedOn"),
						Resource = r.Field<string>("Resource")
					}).ToList();

					table = dataSet.Tables[2];
					voDetails.Resources = table.Rows.Cast<DataRow>().Select(r => new ResourceDetails
					{
						CodeID = r.Field<string>("CodeID"),
						Description = r.Field<string>("Description"),
						Type = r.Field<int>("Type")
					}).ToList();

					table = dataSet.Tables[3];
					voDetails.Owners = table.Rows.Cast<DataRow>().Select(r => new OwnerDetails
					{
						OwnerNo = r.Field<string>("OwnerNo"),
						WorkTitle = r.Field<string>("WorkTitle"),
						Discipline = r.Field<string>("Discipline"),
						WBS = r.Field<string>("WBS").Split('|')[0].Trim()
					}).ToList();

					table = dataSet.Tables[4];
					voDetails.Disciplines = table.Rows.Cast<DataRow>().Select(r => new Option
					{
						Value = r.Field<string>("DisciplineCode"),
						Text = r.Field<string>("Description")
					}).ToList();

					table = dataSet.Tables[5];
					voDetails.WBSList = table.Rows.Cast<DataRow>().Select(r => new WBSDetails
					{
						ID = r.Field<string>("ID"),
						Description = r.Field<string>("Description"),
						Disciplinecode = r.Field<string>("Disciplinecode")
					}).ToList();
				}
			}
			return voDetails;
		}

	}
}
