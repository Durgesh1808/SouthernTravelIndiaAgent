using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentForgotPassword : System.Web.UI.Page
    {
        public string password, sgeneratepass;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                btnAdd.Attributes.Add("onclick", "return validation();");
                Random r = new Random();
                password = r.Next().ToString();
                if (password.Length > 6)
                    password = password.Remove(6);
                ViewState["strPass"] = "";
                ViewState["Spass"] = password.Trim().ToString();
                ClientScript.RegisterStartupScript(typeof(string), "stsrtupSend8", "<script>fnMd5('" + ViewState["Spass"].ToString() + "');</script>");
            }
            else
                ViewState["strPass"] = tmpEnValue.Value;
        }
        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            ViewState["strPass"] = tmpEnValue.Value;

            string email = txtmail.Text.Trim().Replace("'", "''").Replace("</script>", "").Replace("--", "").ToString();
            //DataTable dt = ClsAgentTransaction.Agent_ForgotPassword(email, ViewState["strPass"].ToString());
            DataTable dt = ClsAgentTransaction.Agent_ForgotPassword(email, tmpEnValue.Value);
            if (dt.Rows.Count > 0)
            {
                System.IO.StreamReader sr = new System.IO.StreamReader(Server.MapPath(ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\agentforgotpass.html"));
                string strToSend = sr.ReadToEnd();
                strToSend = strToSend.Replace("#membername#", dt.Rows[0]["Fname"].ToString());
                strToSend = strToSend.Replace("#MemberUserName#", dt.Rows[0]["UserId"].ToString());
                strToSend = strToSend.Replace("#MemberPassword#", ViewState["Spass"].ToString());
                string supportEmail = ConfigurationManager.AppSettings["SupportEmail"];
                ClsCommon.SendMail(txtmail.Text, "", "",supportEmail, "Your reset password.", strToSend);
                ClientScript.RegisterStartupScript(typeof(string), "stsrtupSend7", "<script>alert('You will get the new Password shortly on your registered Mail id');window.location.href='AgentLogin.aspx'</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(string), "stsrtupSend1", "<script>alert('This email id is not registerd with us or please input valid email id.');</script>");
                txtmail.Text = "";
                txtmail.Focus();
            }
        }
        protected void btnCancel_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("AgentLogin.aspx");
        }
    }
}