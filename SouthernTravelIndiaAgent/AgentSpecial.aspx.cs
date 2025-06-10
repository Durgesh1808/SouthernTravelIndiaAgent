using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentSpecial : System.Web.UI.Page
    {
        protected string mode = "agent";
        protected void Page_Load(object sender, EventArgs e)
        {
            mode = "agent";
        }
    }
}