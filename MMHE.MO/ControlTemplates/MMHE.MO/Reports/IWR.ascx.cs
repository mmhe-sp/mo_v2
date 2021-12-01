using MMHE.MO.Business.Repositories;
using MMHE.MO.UI;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls.Reports
{
    public partial class IWR : UserControl
    {
        public string JCSId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;
            JCSId = Request.QueryString["id"];
            new VORepository().Print(JCSId, user.ProjectId, user.Id);
        }
    }
}
