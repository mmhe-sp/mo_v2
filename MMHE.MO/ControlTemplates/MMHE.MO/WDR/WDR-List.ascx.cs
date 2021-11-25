using MMHE.MO.Business.Repositories;
using MMHE.MO.UI;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.ControlTemplates.MMHE.MO.WDR
{
    public partial class WDR_List : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;

            rWDRSubmission.DataSource = new WDRRepository().GetAll(user.Id.ToString());
            rWDRSubmission.DataBind();
        }
    }
}
