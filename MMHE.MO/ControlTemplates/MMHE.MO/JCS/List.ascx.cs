using MMHE.MO.Business.Repositories;
using MMHE.MO.UI;
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace MMHE.MO.Controls.JCS
{
    public partial class List : UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (Page as BasePage).LoggedInUser;

            jcsRepeater.DataSource = new JCSRepository().GetAll(user.ProjectId, user.Id.ToString());
            jcsRepeater.DataBind();
        }
    }
}
