using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentLogin : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        ClsBal pvbal = null;
        ClsAdo pLinqCls = null;
        string strPassword;
        #endregion
        #region "Property(s)"
        protected string sSalt
        {
            get
            {
                if (ViewState["sSalt"] == null)
                    return "";
                else
                    return (string)ViewState["sSalt"];
            }
            set
            {
                ViewState["sSalt"] = value;
            }
        }
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            //------------For Login Demo Purpose Begin Here----------------
            // ifconform(true);
            //------------For LOgin Demo Purpose End Here----------------   
            this.Button1.Attributes.Add("onclick", "javascript:return validate()");
            if (IsPostBack)
            {
                DataTable ldtAgentDetails = null;
                pvbal = new ClsBal();
                try
                {
                    string strAgentName = DataLib.funClear(txtagentid.Text.Replace("'", "''").Trim());

                    ldtAgentDetails = pvbal.ValidateAgent(strAgentName);
                    if (ldtAgentDetails != null)
                    {
                        string strpswd = Convert.ToString(ldtAgentDetails.Rows[0]["password"]);
                        if (!string.IsNullOrEmpty(strpswd))
                            conformation(strpswd);
                    }
                    else
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "strValidate",
                            "<script>alert('Invalid Agent ID/Password provided!');</script>");
                    }
                }
                catch (Exception)
                {
                    Response.Write("Please Enter Proper Details and Login");
                    Server.Execute("agentlogin.aspx", false);
                }
                finally
                {
                    if (pvbal != null)
                    {
                        pvbal = null;
                    }
                    if (ldtAgentDetails != null)
                    {
                        ldtAgentDetails.Dispose();
                        ldtAgentDetails = null;
                    }

                }
            }
            else
                sSalt = Convert.ToString(System.DateTime.Now.Minute + System.DateTime.Now.Second);
        }
        #endregion
        #region "Method(s)"
        void conformation(string strpass)
        {
            string pas = strpass + sSalt;
            string pas1 = txtPassword.Text.Trim(); //client digest
            if (pas == pas1)
                ifconform();
            else
            {
                ClsCommon.ShowAlert("Invalid Username or Password!");
                txtagentid.Text = "";
            }
        }
        void ifconform()
        {
            DataSet dtagent = null;
            DataTable ldtRecSet = null;
            pLinqCls = new ClsAdo();
            try
            {
                string strAgentName = DataLib.funClear(txtagentid.Text.Replace("'", "''").Trim());
                strPassword = DataLib.funClear(txtPassword.Text.Replace("'", "''").Trim());
                string strIPAdd = Request.UserHostAddress.ToString();
                string stripname = Request.UserHostName.ToString();
                #region Optimize Code
                /*SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@userid", strAgentName);
                DataSet dtagent = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "agent_session_details", param);*/
                #endregion


                ldtRecSet = new DataTable();
                ldtRecSet = pLinqCls.fnagent_session_details(strAgentName);

                dtagent = new DataSet();
                if (ldtRecSet != null)
                {
                    dtagent.Tables.Add(ldtRecSet);
                }
                if (dtagent.Tables[0].Rows.Count > 0)
                {
                    string virtualPath = "", CancelCharge = "";
                    if (Session["virtualPath"] != null)
                    {
                        virtualPath = Session["virtualPath"].ToString();
                        CancelCharge = Session["CancelCharge"].ToString();
                    }
                    Session.Clear();
                    Session["virtualPath"] = virtualPath;
                    Session["CancelCharge"] = CancelCharge;
                    Session["AgentId"] = dtagent.Tables[0].Rows[0][0];
                    Session["AgentFname"] = dtagent.Tables[0].Rows[0][1];
                    Session["AgentLname"] = dtagent.Tables[0].Rows[0][2];
                    Session["AgentLevel"] = dtagent.Tables[0].Rows[0][3];
                    Session["UserId"] = dtagent.Tables[0].Rows[0][4];
                    Session["AgentEmail"] = dtagent.Tables[0].Rows[0][5];
                    Session["LocalBranch"] = dtagent.Tables[0].Rows[0][7];
                    Globals.AgentAddress = Convert.ToString(dtagent.Tables[0].Rows[0]["address"]) + " " + Convert.ToString(dtagent.Tables[0].Rows[0]["city"]);
                    Globals.AgentPhone = Convert.ToString(dtagent.Tables[0].Rows[0]["Mobile"]) + ", " + Convert.ToString(dtagent.Tables[0].Rows[0]["landline"]);
                    Session["AgentAddress"] = Convert.ToString(dtagent.Tables[0].Rows[0]["address"]) + " " + Convert.ToString(dtagent.Tables[0].Rows[0]["city"]);
                    Session["AgentPhone"] = Convert.ToString(dtagent.Tables[0].Rows[0]["Mobile"]) + ", " + Convert.ToString(dtagent.Tables[0].Rows[0]["landline"]);
                    string strForgot = Convert.ToString(dtagent.Tables[0].Rows[0][6]);
                    /* string strLog = "insert into agent_log(AgentId,IpAddress) values('" + Session["AgentId"].ToString() + "' ,'" + strIPAdd + "')";
                     DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, strLog, false);*/
                    pLinqCls = new ClsAdo();
                    try
                    {
                        int val = pLinqCls.fnSaveAgentLogInInfo(Convert.ToInt32(Session["AgentId"]), strIPAdd);
                    }
                    finally
                    {
                        pLinqCls = null;
                    }
                    if (strForgot == "Y")
                    {
                        Session["IsForgot"] = "Y";
                        Response.Redirect("AgentChangePassWord.aspx", false);
                    }
                    else
                        Response.Redirect("agenthomepage.aspx", false);
                }
            }
            catch (Exception)
            {
                Response.Write("Please Enter Proper Details and Login");
                Response.Redirect("agentlogin.aspx", false);
            }

            finally
            {
                if (pLinqCls != null)
                {
                    pLinqCls = null;
                }
                if (dtagent != null)
                {
                    dtagent.Dispose();
                    dtagent = null;
                }
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
            }
        }
        #endregion
        //------------For Login Demo Purpose Begin Here----------------
        //void ifconform(bool autocinfirm)
        //{
        //    try
        //    {
        //        string strAgentName;
        //        strAgentName = DataLib.funClear(txtagentid.Text.Replace("'", "''").Trim());
        //        strPassword = DataLib.funClear(txtPassword.Text.Replace("'", "''").Trim());
        //        string strIPAdd = Request.UserHostAddress.ToString();
        //        DataTable dtagent = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select AgentId,Fname,Lname,AgentLevel,UserId,Email,isforgot,LocalBranch From Agent_AgentDetails --where UserId = '" + strAgentName + "' ");
        //        if (dtagent.Rows.Count > 0)
        //        {
        //            string virtualPath = "", CancelCharge = "";
        //            if (Session["virtualPath"] != null)
        //            {
        //                virtualPath = Session["virtualPath"].ToString();
        //                CancelCharge = Session["CancelCharge"].ToString();
        //            }
        //            Session.Clear();

        //            Session["virtualPath"] = virtualPath;
        //            Session["CancelCharge"] = CancelCharge;

        //            Session["AgentId"] = dtagent.Rows[0][0];
        //            Session["AgentFname"] = dtagent.Rows[0][1];
        //            Session["AgentLname"] = dtagent.Rows[0][2];
        //            Session["AgentLevel"] = dtagent.Rows[0][3];
        //            Session["UserId"] = dtagent.Rows[0][4];
        //            Session["AgentEmail"] = dtagent.Rows[0][5];
        //            Session["LocalBranch"] = dtagent.Rows[0][7];

        //            string strForgot = Convert.ToString(dtagent.Rows[0][6]);


        //            string strLog = "insert into agent_log(AgentId,IpAddress) values('" + Session["AgentId"].ToString() + "' ,'" + strIPAdd + "')";
        //            DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, strLog, false);

        //            if (strForgot == "Y")
        //            {
        //                Session["IsForgot"] = "Y";
        //                Response.Redirect("AgentChangePassWord.aspx", false);
        //            }

        //            else
        //            {
        //                Response.Redirect("agenthomepage.aspx", false);
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Please Enter Proper Details and Login");
        //        Response.Redirect("agentlogin.aspx", false);
        //    }
        //}
        //------------For Login Demo Purpose End Here----------------
    }
}