using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentLogout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string strIPAdd = Request.UserHostAddress.ToString();
            ClsAdo pLinqCls = new ClsAdo();
            try
            {
                int val = pLinqCls.fnSaveAgentLogoutInfo(Convert.ToInt32(Session["AgentId"]), strIPAdd);
            }
            finally
            {
                pLinqCls = null;
            }
            Session["AgentId"] = null;
            Session["AgentFname"] = null;
            Session["AgentLname"] = null;
            Session["AgentLevel"] = null;
            Session.Abandon();
            Response.Redirect("AgentLogin.aspx");
        }
    }
}