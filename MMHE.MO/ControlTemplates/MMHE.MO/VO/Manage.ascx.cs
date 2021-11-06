using MMHE.MO.Business.Repositories;
using MMHE.MO.Models;
using MMHE.MO.UI;
using System;
using System.Web.UI;

namespace MMHE.MO.Controls.VO
{
    public partial class Manage : UserControl
    {
        public VODetails Details { get; set; }
        public int LastRowIndex = 0;
        public string JCSId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;

            JCSId = Request.QueryString["id"];
            if (string.IsNullOrWhiteSpace(JCSId))
                JCSId = Guid.Empty.ToString();
            Details = new VORepository().GetVODetails(JCSId, user.ProjectId, user.Id);
            Details.Type = "VO";
            jcsRepeater.DataSource = Details.Activities;
            jcsRepeater.DataBind();
            LastRowIndex = Details.Activities.Count + 1;
        }
    }
}
