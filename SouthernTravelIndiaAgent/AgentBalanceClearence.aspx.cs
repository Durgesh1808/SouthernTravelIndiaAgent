using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentBalanceClearence : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }
            else
            {
                UCAgentBalanceClearence1.fldAgentID = Convert.ToInt32(Session["AgentId"]);
                UCAgentBalanceClearence1.fldBranchCode = Convert.ToString(Session["LocalBranch"]);
                UCAgentBalanceClearence1.fldUserName = Convert.ToString(Session["UserId"]);
                UCAgentBalanceClearence1.fldPageName = "AgentBalanceClearence.aspx";
            }
        }
    }
}