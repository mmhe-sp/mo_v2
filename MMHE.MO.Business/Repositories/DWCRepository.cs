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
	public class DWCRepository
	{
		public List<DWCActivity> GetAll(string project, string loggedInUser)
		{
			SqlParameter[] parameters = new SqlParameter[3];
			parameters[0] = new SqlParameter("@pType", "Active");
			parameters[1] = new SqlParameter("@pProNo", project);
			parameters[2] = new SqlParameter("@pEmpID", loggedInUser);
			DataTable table = new DataTable();
			List<DWCActivity> dwc = null;
			using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
			{
				using (SqlCommand command = new SqlCommand("MO.FindDWC", connection))
				{
					command.CommandType = CommandType.StoredProcedure;
					command.Parameters.AddRange(parameters);
					SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
					sqlDataAdapter.Fill(table);
					dwc = table.Rows.Cast<DataRow>().Select(r => new DWCActivity
					{
						JCSID = r.Field<Guid>("JCSID"),
						ActivityID = r.Field<Guid?>("ActivityID"),
						ActivityType = r.Field<string>("ActivityType"),
						StartDate = r.Field<DateTime?>("StartDate"),
						EndDate = r.Field<DateTime?>("EndDate"),
						ActivityTitle = r.Field<string>("ActivityTitle"),
						Work_Title = r.Field<string>("Work_Title"),
						OwnerNo = r.Field<string>("OwnerNo"),
						MyGroup = r.Field<string>("MyGroup"),
						CompletionPer = r.Field<decimal>("CompletionPer"),
						lType = r.Field<string>("lType"),
						JSLStatus = r.Field<string>("JSLStatus"),
						Type = r.Field<string>("Type"),
						Subscontractor = r.Field<string>("Subscontractor"),
						WBS = r.Field<string>("WBS"),
						JSLRemarks = r.Field<string>("JSLRemarks")
					}).ToList();
				}
			}
			return dwc;
		}
	}
}
