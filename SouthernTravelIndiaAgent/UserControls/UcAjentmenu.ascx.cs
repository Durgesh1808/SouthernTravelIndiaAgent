using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent.UserControls
{
    public partial class UcAjentmenu : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.UrlReferrer == null)
            {
            }
            if (Request.UserAgent.IndexOf("AppleWebKit") > 0)
                Request.Browser.Adapters.Clear();
            Menu1.DynamicHoverStyle.ForeColor = System.Drawing.Color.Black;
            //  Response.Write(System.Net.Dns.GetHostByName(Environment.MachineName).AddressList[0].ToString());   
        }
    }
}