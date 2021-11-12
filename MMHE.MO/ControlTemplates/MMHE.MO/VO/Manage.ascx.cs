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
        public bool CanApprove { get; set; }
        public bool CanPrint { get; set; }
        public int LastRowIndex = 0;
        public string JCSId = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;
            bool isNew = false;
            JCSId = Request.QueryString["id"];
            if (string.IsNullOrWhiteSpace(JCSId))
            {
                isNew = true;
                JCSId = Guid.Empty.ToString();
            }
            Details = new VORepository().GetVODetails(JCSId, user.ProjectId, user.Id);
            Details.Type = Type;
            jcsRepeater.DataSource = Details.Activities;
            jcsRepeater.DataBind();
            LastRowIndex = Details.Activities.Count + 1;
            if (Details.CanApprove && !isNew)
                CanApprove = true;

            if (Details.CanPrint && !isNew)
                CanPrint = true;
        }

        public string Type
        {
            get
            {
                string type = Request.QueryString["Type"];
                if (string.IsNullOrWhiteSpace(type) || string.Equals(type, "vo", StringComparison.InvariantCultureIgnoreCase))
                    return "VO";
                else
                    return "AWO";
            }
        }
    }
}
