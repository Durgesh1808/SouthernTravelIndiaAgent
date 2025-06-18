using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentChangePassWord : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
                Response.Redirect("AgentLogin.aspx");
            if (!IsPostBack)
            {
                ViewState["salt"] = System.DateTime.Now.Minute + System.DateTime.Now.Second;
                btnAdd.Attributes.Add("onclick", "return validate();");
            }
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {

            if (txtoldpwd.Text.Trim().ToString() != "" && txtNewpwd.Text.Trim().ToString() != "" && txtrepwd.Text.Trim().ToString() != "")
            {
                if (txtNewpwd.Text.Trim().ToString() == txtrepwd.Text.Trim().ToString())
                {
                    string stxtoldpwd = txtoldpwd.Text.Trim().Replace("'", "''");
                    string sPassword = txtNewpwd.Text.Trim().ToString().Replace("'", "''");
                    if (ClsAgentTransaction.Agent_ChangePassword(Convert.ToString(Session["UserId"]), stxtoldpwd, sPassword) == 1)
                    {
                        Session["IsForgot"] = "N";
                        ClsCommon.SuccessShowAlert("Password updated Sussessfully !");
                        //ClientScript.RegisterStartupScript(typeof(string), "startupAdd", "<script>alert('Password updated Sussessfully !');window.location.href='agenthomepage.aspx'</script>");
                    }
                    else
                    {
                        txtoldpwd.Text = "";
                        ClsCommon.ShowAlert("Your password is Wrong !");
                        //ClientScript.RegisterStartupScript(typeof(string), "startupAdd1", "<script>alert('Your password is Wrong !');</script>");
                    }
                }
                else
                    ClsCommon.ShowAlert("Both password must be Same !");
                    //ClientScript.RegisterStartupScript(typeof(string), "startupAdd", "<script>alert('Both password must be Same !');</script>");
            }
            else
                ClsCommon.ShowAlert("Password can not be blank !");
            //ClientScript.RegisterStartupScript(typeof(string), "startuperr", "<script>alert('password can not be blank !');</script>");
        }
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            if (Convert.ToString(Session["IsForgot"]) == "N")
                Response.Redirect("agenthomepage.aspx");
            else
                Response.Redirect("agenthomepage.aspx");
        }
    }
}