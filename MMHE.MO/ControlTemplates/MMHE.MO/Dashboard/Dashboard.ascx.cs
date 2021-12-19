using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using MMHE.MO.UI;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.ControlTemplates.MMHE.MO.Dashboard
{
    public partial class Dashboard : UserControl
    {
        protected Label TotalJSLItems;
        protected Label lTotalNumber;
        protected Label lTotalTask;
        protected List<Statistics> statistics;
        protected void Page_Load(object sender, EventArgs e)
        {
            statistics = new List<Statistics>();
            var user = (Page as BasePage).LoggedInUser;
            DashboardStatistics result = new DashboardRepository().GetAll(user.Id.ToString());
            tJSLStatistics.DataSource = result.Statistics;
            foreach (DataRow dr in result.TotalJSL.Rows)
            {
                TotalJSLItems.Text = dr["TotalJSLItems"].ToString();
            }
            //
            foreach (DataRow dr1 in result.TotalPerson.Rows)
            {
                lTotalNumber.Text = dr1["TotalPercen"].ToString();
            }
            //TotalTask
            foreach (DataRow dr2 in result.TotalTask.Rows)
            {
                lTotalTask.Text = dr2["TotalTask"].ToString();
            }
            //Graph details
            DataTable gDetails = result.GraphDetails;

            List<string> jslStatus = gDetails.AsEnumerable().GroupBy(a => a.Field<string>("JSLStatus")).Select(a => a.Key).ToList();
            foreach (string value in jslStatus)
            {
                var count = statistics.Count(a => a.name.ToLower() == value.ToLower());
                if (count <= 0)
                    statistics.Add(new Statistics() { name = value, data = new List<int>() { 
                        gDetails.AsEnumerable().Where(row => row.Field<string>("JSLStatus") == value).Where(row => row.Field<string>("ActivityType") == "Original").Sum(row => row.Field<int>("Count1"))
                        , gDetails.AsEnumerable().Where(row => row.Field<string>("JSLStatus") == value).Where(row => row.Field<string>("ActivityType") == "Variation").Sum(row => row.Field<int>("Count1")), 
                    gDetails.AsEnumerable().Where(row => row.Field<string>("JSLStatus") == value).Where(row => row.Field<string>("ActivityType") == "Additional").Sum(row => row.Field<int>("Count1"))} });
            }
            tJSLStatistics.DataBind();
        }
        public class Statistics
        {
            public string name { get; set; }
            public List<int> data { get; set; }
        }
    }
}
