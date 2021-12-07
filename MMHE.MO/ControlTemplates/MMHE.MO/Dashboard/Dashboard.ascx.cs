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
        protected void Page_Load(object sender, EventArgs e)
        {
            
            var user = (Page as BasePage).LoggedInUser;
            DashboardStatistics result = new DashboardRepository().GetAll(user.Id.ToString());
            tJSLStatistics.DataSource = result.Statistics;
            foreach (DataRow dr in result.TotalJSL.Rows) 
            {
                TotalJSLItems.Text = dr["TotalJSLItems"].ToString(); 
            }

            tJSLStatistics.DataBind();
        }
    }
}
