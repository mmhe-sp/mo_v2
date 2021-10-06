using MMHE.MO.Business.Repositories;
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
            jcsRepeater.DataSource = new JCSRepository().GetAll("1.21M0025", "264636");
            jcsRepeater.DataBind();
        }
    }
}
