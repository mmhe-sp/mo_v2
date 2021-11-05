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
            var page = (Page as BasePage);
            if (page != null)
                UserName = (Page as BasePage).LoggedInUser.Name;
        }
    }
}
