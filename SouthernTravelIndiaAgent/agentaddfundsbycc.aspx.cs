using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
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
    public partial class agentaddfundsbycc : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        ClsAdo pClsObj = null;
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["AgentId"] != null)
            {
                this.txtAmount.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                Submit.Attributes.Add("onclick", "javascript:return Cashvalidation();");
                DataTable dt3 = null;
                try
                {
                    dt3 = ClsAgentTransaction.Agent_Availablebalance(Convert.ToInt32(Session["AgentId"]));


                    if (dt3.Rows.Count == 0)
                        lblBalance.Text = "Empty";
                    if (dt3.Rows.Count > 0)
                        lblBalance.Text = dt3.Rows[0]["AvailableBalance"].ToString();
                    if (!IsPostBack)
                    {
                        txtDepositDate.Text = System.DateTime.Today.ToShortDateString();
                        txtDepositDate.ReadOnly = true;
                    }
                    if ((!IsPostBack) && (Convert.ToString(Request.QueryString["TransactionID"]) != null) && (Convert.ToString(Request.QueryString["TransactionID"]) != ""))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "Ypdation", "<script>alert('Thanks for depositing the amount.');window.location.href='agentaddfundsbycc.aspx';</script>");
                        txtDepositBy.Text = "";
                        txtDepositDate.Text = "";
                    }
                    if ((!IsPostBack) && (Convert.ToString(Request.QueryString["TransactionID"]) == ""))
                    {
                        ClientScript.RegisterStartupScript(typeof(string), "Ypdation1", "<script>alert('your cc details are incorrect');window.location.href='agentaddfundsbycc.aspx';</script>");
                        txtDepositBy.Text = "";
                        txtDepositDate.Text = "";
                    }
                }
                finally
                {
                    if (dt3 != null)
                    {
                        dt3.Dispose();
                        dt3 = null;
                    }
                }
            }
            else
                Response.Redirect("agentlogin.aspx");
        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                pClsObj = new ClsAdo();
                string userid = Session["UserId"].ToString();
                string strId = Session["AgentId"].ToString();
                string strName = Session["AgentFname"].ToString();
                string amount = DataLib.funClear(txtAmount.Text);
                string strCardHolderName = txtDepositBy.Text.Trim().Replace("'", "''");
                Session["strBankCode"] = CSTBANKID.Value;
                string strdetails = "AAFCC" + strId + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() +
                    System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString();

                if (rdoNetBanking.Checked)
                {
                    Session["mode"] = "AgCredit";
                    Session["orderid"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 29, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    dt = new DataTable();
                    dt = ClsAgentTransaction.Agent_Details(userid);
                    string strAdd = "", strMobile = "", strland = "", strEmail = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAdd = dt.Rows[0]["address"].ToString().Replace(",", " ");
                        strMobile = dt.Rows[0]["mobile"].ToString();
                        strland = dt.Rows[0]["landline"].ToString();
                        strEmail = dt.Rows[0]["email"].ToString();
                    }
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 250)
                        strAdd = strAdd.Substring(0, 249);
                    Session["billingAddress"] = strAdd; //For Test
                    int val = clsBLL.PaymentTable_Entry_hdfc(Session["orderid"].ToString(), "ST", Convert.ToDecimal(amount), "", 'N', "Tech Process", '0', "FullPayment", "AgCredit");
                    if (val == 0)
                    {
                        string TranAmount = amount;

                        ClsAdo clsObj = null;
                        int? lStatus = 0;
                        try
                        {
                            clsObj = new ClsAdo();
                            string lTempTranID = System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

                            lStatus = clsObj.fnInsertPaymentHDFCPG("", Convert.ToString(Session["orderid"].ToString()), strEmail, "", Convert.ToDecimal(TranAmount),
                                "", lTempTranID, "", "", "", "", strId, strEmail, strMobile, strAdd, Session["orderid"].ToString(), "", "AgCredit");
                        }
                        finally
                        {
                            if (clsObj != null)
                            {
                                clsObj = null;
                            }

                        }
                        if (lStatus > 0)
                        {
                            Response.Redirect("../TechProcessPayment_Request.aspx?RID=" + lStatus.ToString() + "&SectionName=AgCredit" + "&FirstName=" + txtDepositBy.Text.Trim());
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                        }

                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                    /*Session["mode"] = "AgDr";
                    Session["TransId"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 17, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    Response.Redirect("../payment.aspx");*/
                }
                else if (rbtnAtom.Checked)
                {
                    Session["mode"] = "AgCredit";
                    Session["orderid"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 29, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    dt = new DataTable();
                    dt = ClsAgentTransaction.Agent_Details(userid);
                    string strAdd = "", strMobile = "", strland = "", strEmail = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAdd = dt.Rows[0]["address"].ToString().Replace(",", " ");
                        strMobile = dt.Rows[0]["mobile"].ToString();
                        strland = dt.Rows[0]["landline"].ToString();
                        strEmail = dt.Rows[0]["email"].ToString();
                    }
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 250)
                        strAdd = strAdd.Substring(0, 249);
                    Session["billingAddress"] = strAdd; //For Test
                    int val = clsBLL.PaymentTable_Entry_hdfc(Session["orderid"].ToString(), "ST", Convert.ToDecimal(amount), "", 'N', "Atom Payment", '1', "FullPayment", "AgCredit");
                    if (val == 0)
                    {
                        string TranAmount = amount;

                        ClsAdo clsObj = null;
                        int? lStatus = 0;
                        try
                        {
                            clsObj = new ClsAdo();
                            string lTempTranID = System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() +
            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

                            lStatus = clsObj.fnInsertPaymentHDFCPG("", Convert.ToString(Session["orderid"].ToString()), strEmail, "", Convert.ToDecimal(TranAmount),
                                "", lTempTranID, "", "", "", "", txtDepositBy.Text, strEmail, strMobile, strAdd, Session["orderid"].ToString(), "", "AgCredit");
                        }
                        finally
                        {
                            if (clsObj != null)
                            {
                                clsObj = null;
                            }

                        }
                        if (lStatus > 0)
                        {
                            Response.Redirect("../AtomPayment.aspx?RID=" + lStatus.ToString() + "&SectionName=AgCredit" + "&FirstName=" + txtDepositBy.Text.Trim());
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                        }

                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                }
                else if (rbtnPayu.Checked)
                {
                    Session["mode"] = "AgCredit";
                    Session["orderid"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 29, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    dt = new DataTable();
                    dt = ClsAgentTransaction.Agent_Details(userid);
                    string strAdd = "", strMobile = "", strland = "", strEmail = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAdd = dt.Rows[0]["address"].ToString().Replace(",", " ");
                        strMobile = dt.Rows[0]["mobile"].ToString();
                        strland = dt.Rows[0]["landline"].ToString();
                        strEmail = dt.Rows[0]["email"].ToString();
                    }
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 250)
                        strAdd = strAdd.Substring(0, 249);
                    Session["billingAddress"] = strAdd; //For Test
                    int val = clsBLL.PaymentTable_Entry_hdfc(Session["orderid"].ToString(), "ST", Convert.ToDecimal(amount), "", 'N', "PayU Payment", '1', "FullPayment", "AgCredit");
                    if (val == 0)
                    {
                        string TranAmount = amount;

                        ClsAdo clsObj = null;
                        int? lStatus = 0;
                        try
                        {
                            clsObj = new ClsAdo();
                            string lTempTranID = System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() +
                            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

                            lStatus = clsObj.fnInsertPaymentHDFCPG("", Convert.ToString(Session["orderid"].ToString()), strEmail, "", Convert.ToDecimal(TranAmount),
                                "", lTempTranID, "", "", "", "", strId, strEmail, strMobile, strAdd, Session["orderid"].ToString(), "", "AgCredit");
                        }
                        finally
                        {
                            if (clsObj != null)
                            {
                                clsObj = null;
                            }

                        }
                        if (lStatus > 0)
                        {
                            Response.Redirect("../PayUPaymet.aspx?RID=" + lStatus.ToString() + "&SectionName=AgCredit" + "&FirstName=" + txtDepositBy.Text.Trim());
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                        }

                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                }
                else if (rdoCC.Checked || rdoCD.Checked)
                {
                    Session["mode"] = "AgCredit";
                    Session["orderid"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 29, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    dt = new DataTable();
                    dt = ClsAgentTransaction.Agent_Details(userid);
                    string strAdd = "", strMobile = "", strland = "", strEmail = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAdd = dt.Rows[0]["address"].ToString().Replace(",", " ");
                        strMobile = dt.Rows[0]["mobile"].ToString();
                        strland = dt.Rows[0]["landline"].ToString();
                        strEmail = dt.Rows[0]["email"].ToString();
                    }
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 250)
                        strAdd = strAdd.Substring(0, 249);
                    Session["billingAddress"] = strAdd; //For Test
                    int val = clsBLL.PaymentTable_Entry_hdfc(Session["orderid"].ToString(), "ST", Convert.ToDecimal(amount), "", 'N', "CC Payment", '1', "FullPayment", "AgCredit");
                    if (val == 0)
                    {
                        string TranAmount = amount;
                        //string lPaymentAmt = ConfigurationSettings.AppSettings["PaymentAmt"].ToString();
                        //if (lPaymentAmt == "1")
                        //{
                        //    TranAmount = "1";
                        //}

                        ClsAdo clsObj = null;
                        int? lStatus = 0;
                        try
                        {
                            clsObj = new ClsAdo();

                            lStatus = clsObj.fnInsertPaymentHDFCPG("", Convert.ToString(Session["orderid"].ToString()), strEmail, "", Convert.ToDecimal(TranAmount),
                                "", "", "", "", "", "", strId, strEmail, strMobile, strAdd, Session["orderid"].ToString(), "", "AgCredit");
                        }
                        finally
                        {
                            if (clsObj != null)
                            {
                                clsObj = null;
                            }

                        }
                        if (lStatus > 0)
                        {
                            Response.Redirect("../HDFCCreditCardPayment.aspx?RID=" + lStatus.ToString() + "&SectionName=AgCredit");
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                        }
                        /*Response.Redirect("../HDFCCreditCardPayment.aspx?Amt=" + TranAmount + "&OrderId=" + Session["orderid"].ToString() +
                            "&EmailId=" + strEmail + "&ts=" + strId + "&ba=" + strAdd + "&mb=" + strMobile + "&SectionName=AgCredit");
                       */
                        // Response.Redirect("../PaymentHDFC.asp?Amt=" + amount + "&OrderId=" + Session["orderid"].ToString() + "&EmailId=" + strEmail + "&ts=" + strId + "&ba=" + strAdd + "&mb=" + strMobile + "");
                        /*string url = "../TestCreditCardPayment.aspx?Amt=" + amount + "&OrderId=" + Session["orderid"].ToString() + "&EmailId=" + strEmail + "&ts=" + strId + "&ba=" + strAdd + "&mb=" + strMobile
                            + "&result=CAPTURED&SectionName=AgCredit";
                          Response.Redirect(url);*/
                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                }
                else if (rdoamex.Checked == true)
                {
                    Session["mode"] = "AgCredit";
                    Session["orderid"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 29, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    dt = new DataTable();
                    dt = ClsAgentTransaction.Agent_Details(userid);
                    string strAdd = "", strMobile = "", strland = "", strEmail = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAdd = dt.Rows[0]["address"].ToString().Replace(",", " ");
                        strMobile = dt.Rows[0]["mobile"].ToString();
                        strland = dt.Rows[0]["landline"].ToString();
                        strEmail = dt.Rows[0]["email"].ToString();
                    }
                    string REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    #region Optimize Code
                    /*SqlParameter[] param1 = new SqlParameter[13];
                 param1[0] = new SqlParameter("@orderId", Session["orderid"].ToString());
                 param1[1] = new SqlParameter("@itemCode", "ST");
                 param1[2] = new SqlParameter("@Amount", Convert.ToDecimal(amount));
                 param1[3] = new SqlParameter("@BankName", "AMEX");
                 param1[4] = new SqlParameter("@IsPaid", "N");
                 param1[5] = new SqlParameter("@gatewayBID", "");
                 param1[6] = new SqlParameter("@orderDetails", "");
                 param1[7] = new SqlParameter("@currency", "INR");
                 param1[8] = new SqlParameter("@payMode", "AMEX Credit Card");
                 param1[9] = new SqlParameter("@ccChargeAmt", decimal.Round(0));
                 param1[10] = new SqlParameter("@isAMEX", 1);
                 param1[11] = new SqlParameter("@ip", REMOTE_ADDR);
                 param1[12] = new SqlParameter("@TotalAmount", Convert.ToDecimal(amount));
                 int val = DataLib.InsStoredProcData("insert_AMEX_tbl_PaymentDetails", param1);*/
                    #endregion
                    /*int val = pClsObj.fninsert_Amex_tbl_PaymentDetails(Convert.ToString(Session["orderid"]), "ST", Convert.ToDecimal(amount), "AMEX", 'N',
                              "", "", "INR", "AMEX Credit Card", decimal.Round(0), true, REMOTE_ADDR, Convert.ToDecimal(amount));
                    */
                    string TranAmount = amount;
                    //string lPaymentAmt = ConfigurationSettings.AppSettings["PaymentAmt"].ToString();
                    //if (lPaymentAmt == "1")
                    //{
                    //    TranAmount = "1";
                    //}

                    ClsAdo clsObj = null;
                    int? lStatus = 0;
                    try
                    {
                        clsObj = new ClsAdo();

                        lStatus = clsObj.fnInsertPaymentHDFCPG("", Convert.ToString(Session["orderid"].ToString()), strEmail, "", Convert.ToDecimal(TranAmount),
                            "", "", "", "", "", "", strId, strEmail, strMobile, strAdd, Session["orderid"].ToString(), "", "AgCredit");
                    }
                    finally
                    {
                        if (clsObj != null)
                        {
                            clsObj = null;
                        }

                    }
                    if (lStatus > 0)
                    {
                        try
                        {
                            if (strAdd.Length > 0)
                                strAdd = strAdd.Replace("&", "");
                            if (strAdd.Length > 0)
                                strAdd = strAdd.Replace("&", "");
                            if (strAdd.Length > 250)
                                strAdd = strAdd.Substring(0, 249);
                            /*
                            VPCRequest conn = new VPCRequest(ConfigurationSettings.AppSettings["PaymentServerURL_Auth"].ToString());
                            conn.setSecureSecret(ConfigurationSettings.AppSettings["SecureSecret"].ToString());
                            conn.addDigitialOrderField("vpc_Version", ConfigurationSettings.AppSettings["vpc_Version"].ToString());
                            conn.addDigitialOrderField("vpc_ReturnURL", ConfigurationSettings.AppSettings["vpc_ReturnURL"].ToString());
                            conn.addDigitialOrderField("vpc_AccessCode", ConfigurationSettings.AppSettings["vpc_AccessCode"].ToString());
                            conn.addDigitialOrderField("vpc_Merchant", ConfigurationSettings.AppSettings["vpc_Merchant"].ToString());
                            conn.addDigitialOrderField("vpc_Command", "pay");
                            conn.addDigitialOrderField("vpc_MerchTxnRef", Session["orderid"].ToString());
                            conn.addDigitialOrderField("vpc_Orderinfo", Session["orderid"].ToString());
                            conn.addDigitialOrderField("vpc_CustomerIpAddress", REMOTE_ADDR);
                            conn.addDigitialOrderField("TravelSector", strId);
                            conn.addDigitialOrderField("billingAddress", strAdd);
                            conn.addDigitialOrderField("vpc_Amount", Convert.ToString(Convert.ToInt32(amount) * 100));*/

                            VPCRequest conn = new VPCRequest();
                            conn.SetSecureSecret(ConfigurationSettings.AppSettings["SecureSecret"].ToString());
                            conn.AddDigitalOrderField("vpc_Version", ConfigurationSettings.AppSettings["vpc_Version"].ToString());
                            conn.AddDigitalOrderField("vpc_ReturnURL", ConfigurationSettings.AppSettings["vpc_ReturnURL"].ToString());
                            conn.AddDigitalOrderField("vpc_AccessCode", ConfigurationSettings.AppSettings["vpc_AccessCode"].ToString());
                            conn.AddDigitalOrderField("vpc_Merchant", ConfigurationSettings.AppSettings["vpc_Merchant"].ToString());
                            conn.AddDigitalOrderField("vpc_Command", "pay");
                            conn.AddDigitalOrderField("vpc_MerchTxnRef", Session["orderid"].ToString());
                            conn.AddDigitalOrderField("vpc_Orderinfo", Session["orderid"].ToString());
                            conn.AddDigitalOrderField("vpc_CustomerIpAddress", REMOTE_ADDR);
                            conn.AddDigitalOrderField("TravelSector", strId);
                            conn.AddDigitalOrderField("billingAddress", strAdd);
                            conn.AddDigitalOrderField("vpc_Amount", Convert.ToString(Convert.ToDecimal(amount) * 100));

                            String url = conn.Create3PartyQueryString();
                            Page.Response.Redirect(url);
                        }
                        catch (Exception ex)
                        {
                            Response.Write(ex.ToString());
                        }

                    }
                }
                else if (rbtnInstamojo.Checked)
                {
                    Session["mode"] = "AgCredit";
                    Session["orderid"] = Convert.ToString(ClsAgentTransaction.agent_addfunds_Pendingentry(Convert.ToInt32(strId), strName, 29, Convert.ToDecimal(amount), "Pending", strdetails, strCardHolderName));
                    dt = new DataTable();
                    dt = ClsAgentTransaction.Agent_Details(userid);
                    string strAdd = "", strMobile = "", strland = "", strEmail = "";
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        strAdd = dt.Rows[0]["address"].ToString().Replace(",", " ");
                        strMobile = dt.Rows[0]["mobile"].ToString();
                        strland = dt.Rows[0]["landline"].ToString();
                        strEmail = dt.Rows[0]["email"].ToString();
                    }
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 0)
                        strAdd = strAdd.Replace("&", "");
                    if (strAdd.Length > 250)
                        strAdd = strAdd.Substring(0, 249);
                    Session["billingAddress"] = strAdd; //For Test
                    int val = 0;
                    int result = 0;
                    val = clsBLL.PaymentTable_Entry_hdfc(Session["orderid"].ToString(), "ST", Convert.ToDecimal(amount), "Instamojo Bank", 'N', "Instamojo Payment", '1', "FullPayment", "AgCredit");

                    //try
                    //{
                    //    result = ClsCommon.InsertPGRequerst(Convert.ToString(Session["orderid"]), "ST", Convert.ToDecimal(amount), "Instamojo Bank", "INR", "Instamojo Bank",
                    //        Convert.ToString(HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"]),
                    //        "FullPayment", "AgCredit", strEmail, txtDepositBy.Text, strEmail, strMobile, strAdd, Convert.ToString(Session["AgentId"]));

                    //    if (result > 0)
                    //    {
                    //        Response.Redirect("../InstaMojoPayment.aspx?RID=" + result.ToString() + "&SectionName=AgCredit" + "&FirstName=" + txtDepositBy.Text.Trim());
                    //    }
                    //    else
                    //    {

                    //        ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                    //    }
                    //}
                    //catch (Exception ex)
                    //{
                    //    ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                    //}

                    if (val == 0)
                    {
                        string TranAmount = amount;

                        ClsAdo clsObj = null;
                        int? lStatus = 0;
                        try
                        {
                            clsObj = new ClsAdo();
                            string lTempTranID = System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() +
                            System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();

                            lStatus = clsObj.fnInsertPaymentHDFCPG("", Convert.ToString(Session["orderid"].ToString()), strEmail, "", Convert.ToDecimal(TranAmount),
                                "", lTempTranID, "", "", "", "", txtDepositBy.Text, strEmail, strMobile, strAdd, Session["orderid"].ToString(), "", "AgCredit");
                        }
                        finally
                        {
                            if (clsObj != null)
                            {
                                clsObj = null;
                            }

                        }
                        if (lStatus > 0)
                        {
                            Response.Redirect("../InstaMojoPayment.aspx?RID=" + lStatus.ToString() + "&SectionName=AgCredit" + "&FirstName=" + txtDepositBy.Text.Trim());
                        }
                        else
                        {
                            ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                        }

                    }
                    else
                        ClientScript.RegisterStartupScript(typeof(string), "Error", "<script>alert('There is an error in the input fields, please contact administrator');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Selectbank", "<script>alert('Please select the Payment Option');</script>");
                }
            }
            finally
            {
                if (pClsObj != null)
                {
                    pClsObj = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }
        #endregion
    }
}