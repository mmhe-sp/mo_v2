using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using MMHE.MO.UI;
using System;
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
        protected void Page_Load(object sender, EventArgs e)
        {
            
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
            tJSLStatistics.DataBind();
        }
    }
}
