using MMHE.MO.UI;

using System;
using System.Web.UI;

namespace MMHE.MO.Controls
{
	public partial class TopMenu : UserControl
    {
        public string UserName { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = (Page as BasePage).LoggedInUser.Name;
        }
    }
}
