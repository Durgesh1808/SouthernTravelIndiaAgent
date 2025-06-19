using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent.UserControls
{
    public partial class UcFooter : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (DateTime.Now.Hour >= 20 || DateTime.Now.Hour <= 10)
            {
                divPChat.Visible = false;
            }
            else
            {
                divPChat.Visible = true;
            }
        }
    }
}