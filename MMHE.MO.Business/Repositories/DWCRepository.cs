﻿using MMHE.MO.Models;
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
    public class DWCRepository
    {
        public List<DWCActivity> GetAll(string project, string loggedInUser)
        {
            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@pProNo", project);
            parameters[1] = new SqlParameter("@pEmpID", loggedInUser);
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
                        //Type = r.Field<string>("Type"),
                        Subscontractor = r.Field<string>("SubContractor"),
                        //WBS = r.Field<string>("WBS"),
                        JSLRemarks = r.Field<string>("JSLRemarks"),
                        IWRStatus = r.Field<string>("IWRStatus"),
                        ActivityDiscipline = r.Field<string>("ActivityDiscipline")
                    }).ToList();
                }
            }
            return dwc;
        }

        public void SaveProgress(string project, string loggedInUser, UpdatedDWCProgress dwc)
        {
            XElement root = new XElement("DWC");
            root.Add(new XElement("ProjectNo", project));
            root.Add(new XElement("Today", dwc.Today));
            root.Add(new XElement("Tomorrow", dwc.Tomorrow));

            XElement activities = new XElement("Activities");
            XElement activity;
            root.Add(activities);

            XElement jcs = new XElement("JCS");
            XElement jcsRow;
            root.Add(jcs);
            foreach (var j in dwc.JCS)
            {
                jcsRow = new XElement("Row");
                jcsRow.Add(new XElement("JCSID", j.JCSID));
                jcsRow.Add(new XElement("Remarks", j.Remarks));
                jcsRow.Add(new XElement("Today", j.Today));
                jcsRow.Add(new XElement("Tomorrow", j.Tomorrow));
                jcs.Add(jcsRow);
                foreach (var item in j.Activities)
                {
                    activity = new XElement("Activity");
                    activity.Add(new XElement("ActivityID", item.ActivityID));
                    if (string.IsNullOrWhiteSpace(item.SubContractorRemarks))
                        activity.Add(new XElement("SubContractorRemarks", item.SubContractorRemarks));
                    if (string.IsNullOrWhiteSpace(item.Remarks))
                        activity.Add(new XElement("Remarks", item.Remarks));
                    activity.Add(new XElement("Completion", item.Completion));
                    if (string.IsNullOrWhiteSpace(item.Today))
                        activity.Add(new XElement("Today", item.Today));
                    if (string.IsNullOrWhiteSpace(item.Tomorrow))
                        activity.Add(new XElement("Tomorrow", item.Tomorrow));
                    activity.Add(new XElement("JCSID", j.JCSID));
                    activities.Add(activity);
                }
            }

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@DWC", new SqlXml(XElement.Parse(root.ToString()).CreateReader()));
            parameters[1] = new SqlParameter("@UpdatedBy", loggedInUser);

            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.SaveDWCProgress", connection))
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
