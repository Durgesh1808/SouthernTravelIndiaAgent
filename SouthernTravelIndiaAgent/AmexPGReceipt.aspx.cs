//using SouthernTravelIndiaAgent.BAL;
//using SouthernTravelIndiaAgent.DAL;
//using SouthernTravelIndiaAgent.DTO;
//using System;
//using System.Collections.Generic;
//using System.Configuration;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace SouthernTravelIndiaAgent
//{
//    public partial class AmexPGReceipt : System.Web.UI.Page
//    {
//        #region "Member Variable(s)"
//        public static string Version
//        {
//            get
//            {
//                // Return the Example Code Version
//                return "3.0.0";
//            }
//        }
//        protected string PaymentID, auth, result, ref1, trackid, tranid, postdate, errort, amt, OrderId;
//        protected int i;
//        string strResponceIP = "", TranInqResponse = "", ResPaymentId = "", ResResult = "", ResErrorText = "", ResPosdate = "", ResTranId = "", ResAuth = "", ResAVR = "", ResAmount = "",
//            ResErrorNo = "", ResTrackID = "", ResRef = "", Resudf1 = "", Resudf2 = "", Resudf3 = "", Resudf4 = "", Resudf5 = "", Resudf6 = "",
//            lPayMode = "AMEX Payment", lEMIMonth = "FullPayment", lPayDate = "";
//        ClsBal pvBAL = new ClsBal();
//        #endregion
//        #region "Event(s)"
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            if (!IsPostBack)
//            {
//                pnlReceiptError.Visible = false;
//                pnlResponse.Visible = false;
//            }
//            fromgateway();
//            ////#if !IsLive
//            //         fromgateway();   
//            ////#else
//            //        //direct(); //For Local Testing Purpose (Please comment at the time of Live)
//            ////#endif


//        }
//        #endregion
//        #region "Method(s)"
//        private void fromgateway()
//        {
//            try
//            {
//                // Create the VPCRequest object
//                VPCRequest conn = new VPCRequest();
//                conn.SetSecureSecret(ConfigurationSettings.AppSettings["SecureSecret"].ToString());

//                // Process the response
//                conn.Process3PartyResponse(Page.Request.QueryString);
//                // Check if the transaction was successful or if there was an error
//                String vpc_TxnResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");
//                // Set the display fields for the receipt with the result fields
//                // Core Fields
//                Label_vpc_TxnResponseCode.Text = vpc_TxnResponseCode;
//                Label_vpc_MerchTxnRef.Text = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
//                Label_vpc_Amount.Text = conn.GetResultField("vpc_Amount", "Unknown");
//                Label_vpc_Message.Text = conn.GetResultField("vpc_Message", "Unknown");
//                Label_vpc_ReceiptNo.Text = conn.GetResultField("vpc_ReceiptNo", "Unknown");
//                Label_vpc_AcqResponseCode.Text = conn.GetResultField("vpc_AcqResponseCode", "Unknown");
//                Label_vpc_AuthorizeId.Text = conn.GetResultField("vpc_AuthorizeId", "Unknown");
//                Label_vpc_BatchNo.Text = conn.GetResultField("vpc_BatchNo", "Unknown");
//                Label_vpc_TransactionNo.Text = conn.GetResultField("vpc_TransactionNo", "Unknown");
//                Label_vpc_Card.Text = conn.GetResultField("vpc_Card", "Unknown");
//                Label_TxnResponseCodeDesc.Text = PaymentCodesHelper.GetTxnResponseCodeDescription(Label_vpc_TxnResponseCode.Text);

//                string response = Label_vpc_TxnResponseCode.Text + "-" + Label_vpc_MerchTxnRef.Text + "-" + Label_vpc_Amount.Text + "-" +
//                    Label_vpc_Message.Text + "-" + Label_vpc_ReceiptNo.Text + "-" + Label_vpc_AcqResponseCode.Text + "-" +
//                    Label_vpc_AuthorizeId.Text + "-" + Label_vpc_BatchNo.Text + "-" + Label_vpc_TransactionNo.Text + "-" + Label_vpc_Card.Text + "-" +
//                    Label_TxnResponseCodeDesc.Text;

//                InsertPGResponseAndRequest(Label_vpc_MerchTxnRef.Text, Label_vpc_BatchNo.Text, "Response", response);

//                // Address Verification / Advanced Address Verification
//                String vpc_avsResultCode = conn.GetResultField("vpc_AVSResultCode", "Unknown");
//                //String vpc_avsResultCode = "Y";
//                if ((vpc_TxnResponseCode == "0") && (vpc_avsResultCode == "Y"))
//                {
//                    // Create a new VPCRequest Object and set the proxy details if required
//                    conn = new VPCRequest();
//                    conn.SetProxyHost(ConfigurationSettings.AppSettings["ProxyHost"].ToString());
//                    conn.SetProxyUser(ConfigurationSettings.AppSettings["ProxyUser"].ToString());
//                    conn.SetProxyPassword(ConfigurationSettings.AppSettings["ProxyPassword"].ToString());
//                    conn.SetProxyDomain(ConfigurationSettings.AppSettings["ProxyDomain"].ToString());

//                    // Add the Required Fields
//                    conn.AddDigitalOrderField("vpc_Version", ConfigurationSettings.AppSettings["vpc_Version"].ToString());
//                    conn.AddDigitalOrderField("vpc_AccessCode", ConfigurationSettings.AppSettings["vpc_AccessCode"].ToString());
//                    conn.AddDigitalOrderField("vpc_Merchant", ConfigurationSettings.AppSettings["vpc_Merchant"].ToString());
//                    conn.AddDigitalOrderField("vpc_User", ConfigurationSettings.AppSettings["vpc_User"].ToString());
//                    conn.AddDigitalOrderField("vpc_Password", ConfigurationSettings.AppSettings["vpc_Password"].ToString());
//                    conn.AddDigitalOrderField("vpc_Command", "capture");
//                    conn.AddDigitalOrderField("vpc_MerchTxnRef", Label_vpc_MerchTxnRef.Text.Substring(0, Label_vpc_MerchTxnRef.Text.Length - 2) + "-C");
//                    conn.AddDigitalOrderField("vpc_TransNo", Label_vpc_TransactionNo.Text);
//                    conn.AddDigitalOrderField("vpc_Amount", Label_vpc_Amount.Text);
//                    // Perform the transaction
//                    try
//                    {
//                        conn.SendRequest();
//                    }
//                    catch
//                    { }
//                    // Check if the transaction was successful or if there was an error
//                    String ResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");

//                    string ss = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
//                    string ss1 = conn.GetResultField("vpc_Message", "");
//                    string ss2 = ResponseCode;
//                    string ss3 = conn.GetResultField("vpc_AcqResponseCode", "Unknown");
//                    string ss4 = conn.GetResultField("vpc_TransactionNo", "Unknown");
//                    string ss5 = conn.GetResultField("vpc_ReceiptNo", "Unknown");
//                    string ss6 = conn.GetResultField("vpc_BatchNo", "Unknown");

//                }

//                // Display the Response Data
//                pnlResponse.Visible = true;
//                if (Label_vpc_Message.Text == "Approved")
//                {
//                    string Vmmp_txn = Label_vpc_BatchNo.Text;
//                    string Resudf5 = Label_vpc_MerchTxnRef.Text;
//                    ResTranId = Label_vpc_TransactionNo.Text;
//                    string Vbnk_nam = "AMEX Bank";
//                    amt = Label_vpc_Amount.Text;
//                    ResAmount = amt.Substring(0, (amt.Length - 2));
//                    ResRef = Label_vpc_ReceiptNo.Text;
//                    ResTrackID = Label_vpc_AuthorizeId.Text;
//                    ResResult = "CAPTURED";
//                    //int lStatus = 0;
//                    string lRpsStatus = Label_TxnResponseCodeDesc.Text;
//                    ClsAdo pclsObj = null;
//                    try
//                    {
//                        pclsObj = new ClsAdo();
//                        if (ResResult.ToUpper() == "CAPTURED")
//                        {
//                            int lStatus = pclsObj.fnInsertPaymentHDFCPG(Vmmp_txn, Resudf5, Resudf2, Vbnk_nam, Convert.ToDecimal(ResAmount), ResRef,
//                         ResTranId, ResTrackID, ResPosdate, ResResult, ResErrorText, Resudf1, Resudf2, Resudf3, Resudf4, Resudf5, lEMIMonth, "");

//                            if (lStatus > 0)
//                            {
//                                Response.Redirect("http://www.southerntravelsindia.com/PaymentResult.aspx?RID=" + lStatus.ToString() + "&PayMode=" + lPayMode + "&BankName=" + Vbnk_nam);
//                                //Response.Redirect("http://localhost:54618/Southern_Travels/PaymentResult.aspx?RID=" + lStatus.ToString() + "&PayMode=" + lPayMode + "&BankName=" + Vbnk_nam);
//                            }
//                        }
//                        else
//                        {
//                            Response.Redirect("http://www.southerntravelsindia.com/PaymentError.aspx?Message=" + lRpsStatus);
//                        }
//                    }
//                    catch (Exception Ex)
//                    {
//                        //Response.Redirect("http://www.southerntravelsindia.com/PaymentError.aspx?Message=" + Ex.Message);
//                    }
//                    finally
//                    {
//                        if (pclsObj != null)
//                        {
//                            pclsObj = null;
//                        }
//                    }
//                }
//            }
//            catch (Exception ex)
//            {
//                // Capture and Display the Error information
//                lblReceiptErrorMessage.Text = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
//                pnlReceiptError.Visible = true;
//            }
//        }

//        private void fromgateway_OLD()
//        {
//            //try
//            //{
//            //    // Create the VPCRequest object
//            //    VPCRequest conn = new VPCRequest(ConfigurationSettings.AppSettings["PaymentServerURL_Auth"].ToString());
//            //    conn.setSecureSecret(ConfigurationSettings.AppSettings["SecureSecret"].ToString());

//            //    // Process the response
//            //    conn.process3PartyResponse(Page.Request.QueryString);
//            //    // Check if the transaction was successful or if there was an error
//            //    String vpc_TxnResponseCode = conn.getResultField("vpc_TxnResponseCode", "Unknown");
//            //    // Set the display fields for the receipt with the result fields
//            //    // Core Fields
//            //    Label_vpc_TxnResponseCode.Text = vpc_TxnResponseCode;
//            //    Label_vpc_MerchTxnRef.Text = conn.getResultField("vpc_MerchTxnRef", "Unknown");
//            //    Label_vpc_Amount.Text = conn.getResultField("vpc_Amount", "Unknown");
//            //    Label_vpc_Message.Text = conn.getResultField("vpc_Message", "Unknown");
//            //    Label_vpc_ReceiptNo.Text = conn.getResultField("vpc_ReceiptNo", "Unknown");
//            //    Label_vpc_AcqResponseCode.Text = conn.getResultField("vpc_AcqResponseCode", "Unknown");
//            //    Label_vpc_AuthorizeId.Text = conn.getResultField("vpc_AuthorizeId", "Unknown");
//            //    Label_vpc_BatchNo.Text = conn.getResultField("vpc_BatchNo", "Unknown");
//            //    Label_vpc_TransactionNo.Text = conn.getResultField("vpc_TransactionNo", "Unknown");
//            //    Label_vpc_Card.Text = conn.getResultField("vpc_Card", "Unknown");
//            //    Label_TxnResponseCodeDesc.Text = PaymentCodesHelper.getTxnResponseCodeDescription(Label_vpc_TxnResponseCode.Text);


//            //    // Address Verification / Advanced Address Verification
//            //    String vpc_avsResultCode = conn.getResultField("vpc_AVSResultCode", "Unknown");
//            //    //String vpc_avsResultCode = "Y";
//            //    if ((vpc_TxnResponseCode == "0") && (vpc_avsResultCode == "Y"))
//            //    {
//            //        // Create a new VPCRequest Object and set the proxy details if required
//            //        //conn = new VPCRequest(ConfigurationSettings.AppSettings["PaymentServerURL_Auth"].ToString());
//            //        conn = new VPCRequest();
//            //        conn.SetProxyHost(ConfigurationSettings.AppSettings["ProxyHost"].ToString());
//            //        conn.SetProxyUser(ConfigurationSettings.AppSettings["ProxyUser"].ToString());
//            //        conn.SetProxyPassword(ConfigurationSettings.AppSettings["ProxyPassword"].ToString());
//            //        conn.SetProxyDomain(ConfigurationSettings.AppSettings["ProxyDomain"].ToString());

//            //        // Add the Required Fields
//            //        conn.AddDigitalOrderField("vpc_Version", ConfigurationSettings.AppSettings["vpc_Version"].ToString());
//            //        conn.AddDigitalOrderField("vpc_AccessCode", ConfigurationSettings.AppSettings["vpc_AccessCode"].ToString());
//            //        conn.AddDigitalOrderField("vpc_Merchant", ConfigurationSettings.AppSettings["vpc_Merchant"].ToString());
//            //        conn.AddDigitalOrderField("vpc_User", ConfigurationSettings.AppSettings["vpc_User"].ToString());
//            //        conn.AddDigitalOrderField("vpc_Password", ConfigurationSettings.AppSettings["vpc_Password"].ToString());
//            //        conn.AddDigitalOrderField("vpc_Command", "capture");
//            //        conn.AddDigitalOrderField("vpc_MerchTxnRef", Label_vpc_MerchTxnRef.Text.Substring(0, Label_vpc_MerchTxnRef.Text.Length - 2) + "-C");
//            //        conn.AddDigitalOrderField("vpc_TransNo", Label_vpc_TransactionNo.Text);
//            //        conn.AddDigitalOrderField("vpc_Amount", Label_vpc_Amount.Text);
//            //        // Perform the transaction
//            //        conn.SendRequest();
//            //        // Check if the transaction was successful or if there was an error
//            //        String ResponseCode = conn.GetResultField("vpc_TxnResponseCode", "Unknown");

//            //        string ss = conn.GetResultField("vpc_MerchTxnRef", "Unknown");
//            //        string ss1 = conn.GetResultField("vpc_Message", "");
//            //        string ss2 = ResponseCode;
//            //        string ss3 = conn.GetResultField("vpc_AcqResponseCode", "Unknown");
//            //        string ss4 = conn.GetResultField("vpc_TransactionNo", "Unknown");
//            //        string ss5 = conn.GetResultField("vpc_ReceiptNo", "Unknown");
//            //        string ss6 = conn.GetResultField("vpc_BatchNo", "Unknown");

//            //    }

//            //    // Display the Response Data
//            //    pnlResponse.Visible = true;
//            //    if (Label_vpc_Message.Text == "Approved")
//            //    {
//            //        result = "CAPTURED";
//            //        PaymentID = Label_vpc_TransactionNo.Text;
//            //        amt = Label_vpc_Amount.Text;
//            //        amt = amt.Substring(0, (amt.Length - 2));
//            //        auth = "";
//            //        ref1 = Label_vpc_ReceiptNo.Text;
//            //        trackid = Label_vpc_AuthorizeId.Text;
//            //        tranid = Label_vpc_TransactionNo.Text;
//            //        postdate = "";
//            //        errort = "";
//            //        OrderId = Convert.ToString(Session["orderid"]);
//            //        trap_PaymentDetails();
//            //    }
//            //    else
//            //    {
//            //        result = "Failed";
//            //        PaymentID = "";
//            //        amt = "0";
//            //        auth = "";
//            //        ref1 = "";
//            //        trackid = "";
//            //        tranid = "";
//            //        postdate = "";
//            //        errort = "";
//            //        pnlReceiptError.Visible = true;
//            //        lblReceiptErrorMessage.Text = Label_TxnResponseCodeDesc.Text;
//            //        pnlReceiptError.Visible = true;
//            //    }
//            //}
//            //catch (Exception ex)
//            //{
//            //    // Capture and Display the Error information
//            //    lblReceiptErrorMessage.Text = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : "");
//            //    pnlReceiptError.Visible = true;
//            //}
//        }
//        private void trap_PaymentDetails()
//        {
//            string orderDetails = "", strOrderId = "";
//            if (Session["orderid"] != null)
//                strOrderId = Session["orderid"].ToString();
//            else
//                Session["orderid"] = OrderId;

//            if ((Session["Fixed"] != null) && (Session["Fixed"].ToString() == "FixedTours"))
//            {


//                string str1 = "";
//                if (result == "CAPTURED")
//                {
//                    #region Optimize Code
//                    /*SqlParameter[] param18 = new SqlParameter[9];
//                        param18[0] = new SqlParameter("@PaymentID", PaymentID);
//                        param18[1] = new SqlParameter("@amt", amt);
//                        param18[2] = new SqlParameter("@result", result);
//                        param18[3] = new SqlParameter("@ref1", ref1);
//                        param18[4] = new SqlParameter("@trackid", trackid);
//                        param18[5] = new SqlParameter("@tranid", tranid);
//                        param18[6] = new SqlParameter("@postdate", postdate);
//                        param18[7] = new SqlParameter("@errort", errort);
//                        param18[8] = new SqlParameter("@OrderId", Session["orderid"].ToString());
//                        str1 = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,
//                         * ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort where orderID=@OrderId";
//                        int val = DataLib.InsOrUpdateParam(str1, param18);
//                        Response.Write("<CENTER><B>Success</B></CENTER>");
//                        fixedtempbooking();
//                        if (Convert.ToString(Session["promo"]) != "")
//                        {
//                            str1 = "update tbl_promocode set Utilized='Y',Ticketno='" + Session["orderid"].ToString() + "' 
//                         * where Promocode='" + Convert.ToString(Session["promo"]) + "'";
//                            DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, str1, false);
//                        }*/
//                    #endregion

//                    tbl_PaymentDetail pclsTbl = new tbl_PaymentDetail();
//                    pclsTbl.isHDFC = false;
//                    pclsTbl.IsPaid = 'Y';
//                    pclsTbl.PaymentId = PaymentID;
//                    pclsTbl.Amount = Convert.ToDecimal(amt);
//                    pclsTbl.result = result;
//                    pclsTbl.@ref = ref1;
//                    pclsTbl.trackid = trackid;
//                    pclsTbl.transId = tranid;
//                    pclsTbl.postDate = postdate;
//                    pclsTbl.error = errort;
//                    pclsTbl.orderId = Convert.ToString(Session["orderid"]);
//                    ClsAdo pClsObj = null;
//                    try
//                    {
//                        pClsObj = new ClsAdo();
//                        int Val = pClsObj.fnUpdatePaymentDetails(pclsTbl);

//                        fixedtempbooking();
//                        if (Convert.ToString(Session["promo"]) != "")
//                        {
//                            int chk = pClsObj.fnUpdattbl_promocode('Y', Convert.ToString(Session["orderid"]), Convert.ToString(Session["promo"]));
//                        }
//                        Session["promo"] = null;//for removing the promo session
//                        Response.Redirect("additionalcustomerdetails.aspx");
//                    }

//                    catch (Exception ex)
//                    {

//                    }
//                    finally
//                    {
//                        if (pClsObj != null)
//                        {
//                            pClsObj = null;
//                        }
//                    }

//                }
//                //else
//                //{
//                //    Response.Redirect("PaymentError.aspx");
//                //}

//            }
//            // code for agent add funds by credit card.........
//            if ((Session["mode"] != null) && (Session["mode"].ToString() == "AgCredit"))
//            {
//                string str1, str2, payMode = "Funds Add by AMEX";
//                ClsAdo pClsObj = null;
//                try
//                {
//                    pClsObj = new ClsAdo();
//                    if (result == "CAPTURED")
//                    {
//                        #region Optimize Code
//                        /* string strCharges = "select taxvalue from servicetaxmaster where rowid=10"; // To Retieve the Credit card charges
//                    string ccPercent = DataLib.GetStringData(DataLib.Connection.ConnectionString, strCharges);*/
//                        #endregion
//                        string ccPercent = pClsObj.fnGetTAXValue("AgentCC");
//                        decimal calcstaxvalue = 0.0m, UpdatedAmount = 0.0m;
//                        if (ccPercent != "" || ccPercent != null || ccPercent != "0")
//                        {
//                            calcstaxvalue = (Convert.ToDecimal(amt) * Convert.ToDecimal(ccPercent)) / 100;
//                            UpdatedAmount = Convert.ToDecimal(amt) - calcstaxvalue;
//                        }
//                        else
//                        {
//                            UpdatedAmount = Convert.ToDecimal(amt);
//                        }
//                        decimal Amnt = Convert.ToDecimal(amt);
//                        #region Optimize Code
//                        /*SqlParameter[] param21 = new SqlParameter[10];
//                    param21[0] = new SqlParameter("@PaymentID", PaymentID);
//                    param21[1] = new SqlParameter("@amt", Amnt.ToString());
//                    param21[2] = new SqlParameter("@result", result);
//                    param21[3] = new SqlParameter("@ref1", ref1);
//                    param21[4] = new SqlParameter("@trackid", trackid);
//                    param21[5] = new SqlParameter("@tranid", tranid);
//                    param21[6] = new SqlParameter("@postdate", postdate);
//                    param21[7] = new SqlParameter("@errort", errort);
//                    param21[8] = new SqlParameter("@calcstaxvalue", calcstaxvalue);
//                    param21[9] = new SqlParameter("@orderid", Session["orderid"].ToString());
//                    string strPayment = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort,ccChargeAmt=@calcstaxvalue where orderID=@orderid";
//                    int val = DataLib.InsOrUpdateParam(strPayment, param21);*/
//                        #endregion
//                        tbl_PaymentDetail pclsTbl = new tbl_PaymentDetail();
//                        pclsTbl.isHDFC = false;
//                        pclsTbl.IsPaid = 'Y';
//                        pclsTbl.PaymentId = PaymentID;
//                        pclsTbl.Amount = Convert.ToDecimal(amt);
//                        pclsTbl.result = result;
//                        pclsTbl.@ref = ref1;
//                        pclsTbl.trackid = trackid;
//                        pclsTbl.transId = tranid;
//                        pclsTbl.postDate = postdate;
//                        pclsTbl.error = errort;
//                        pclsTbl.ccChargeAmt = calcstaxvalue;
//                        pclsTbl.payMode = "AMEX Payment";
//                        pclsTbl.orderId = Convert.ToString(Session["orderid"]);

//                        int Val1 = pClsObj.fnUpdatePaymentDetails_AgCredit(pclsTbl);

//                        // Adding the funds 
//                        string strId = Session["AgentId"].ToString();
//                        string aafcode = "";// DataLib.AgentAddFundsCode();
//                        int Val = 0;
//                        try
//                        {
//                            Val = pClsObj.fnInsertUpdateAgCreditInfo(Convert.ToInt32(Session["AgentId"]), UpdatedAmount, Convert.ToString(Session["orderid"]),
//                               Convert.ToString(Session["UserId"]), Convert.ToDecimal(calcstaxvalue), ref aafcode);
//                        }
//                        catch (Exception ex)
//                        {

//                        }
//                        if (Val > 0)
//                        {
//                            // Mail Code Starts here

//                            DataTable ldtRecSet = pClsObj.fnGetAgentdetails(Convert.ToInt32(strId));
//                            DataSet dsAgent = new DataSet();
//                            if (ldtRecSet != null)
//                            {
//                                dsAgent.Tables.Add(ldtRecSet);
//                            }
//                            string strToSend = "";
//                            if (dsAgent.Tables[0].Rows.Count > 0)
//                            {
//                                string filepath = Server.MapPath(ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\AddFundsAlert.html");
//                                System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
//                                strToSend = sr.ReadToEnd();
//                                strToSend = strToSend.Replace("#AgName#", dsAgent.Tables[0].Rows[0]["Name"].ToString());
//                                strToSend = strToSend.Replace("#AGUserId#", dsAgent.Tables[0].Rows[0]["UserId"].ToString());
//                                strToSend = strToSend.Replace("#Amt#", UpdatedAmount.ToString());
//                                strToSend = strToSend.Replace("#Mode#", "Credit Card");
//                                strToSend = strToSend.Replace("#transno#", aafcode);
//                            }
//                            try
//                            {
//                                /*MailMessage sendMail = new MailMessage();
//                                sendMail.To = ConfigurationSettings.AppSettings["agentFundsAddMailId"].ToString();
//                                sendMail.From = "etickets@southerntravels.in";
//                                sendMail.Body = strToSend;
//                                sendMail.BodyFormat = MailFormat.Html;
//                                sendMail.Subject = aafcode + ": Add Funds By Credit Card";
//                                SmtpMail.Send(sendMail);*/
//                                string pTO = ConfigurationSettings.AppSettings["agentFundsAddMailId"].ToString();
//                                string pFrom = "etickets@southerntravels.in";
//                                string pSubject = aafcode + ": Add Funds By Credit Card";
//                                string pBody = strToSend;
//                                ClsCommon.sendmail(pTO, "", "", pFrom, pSubject, pBody, "");
//                            }
//                            catch (Exception ex)
//                            {
//                                Response.Write("<!-- " + ex.ToString() + " -->");
//                            }
//                            finally
//                            {
//                                if (pClsObj != null)
//                                {
//                                    pClsObj = null;
//                                }
//                            }
//                            // // Mail Code Ends here
//                            Response.Redirect("agent/agentaddfundsbycc.aspx?TransactionID=" + Session["orderid"].ToString());
//                        }
//                        #region Optimize Code
//                        /*string strId = Session["AgentId"].ToString();
//                    SqlParameter[] paramx = new SqlParameter[1];
//                    paramx[0] = new SqlParameter("@AgentId", Session["AgentId"].ToString());
//                    string qry2 = "select top 1 AvailableBalance from onlinetransactiontable where agentId=@AgentId  order by rowid  desc";
//                    DataTable dt5 = DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, qry2, paramx);
//                    if (dt5.Rows.Count == 0)
//                    {
//                        SqlParameter[] param2 = new SqlParameter[5];
//                        param2[0] = new SqlParameter("@strId", strId);
//                        param2[1] = new SqlParameter("@UpdatedAmount", UpdatedAmount);
//                        param2[2] = new SqlParameter("@orderid", Session["orderid"].ToString());
//                        param2[3] = new SqlParameter("@UserId", Session["UserId"].ToString());
//                        param2[4] = new SqlParameter("@calcstaxvalue", calcstaxvalue);
//                        string qry1 = "insert into onlinetransactiontable(agentid,agentcredit,availablebalance,Status,TransType,refno,BranchCode,UserName,CcChargeAmt) values(@strId,@UpdatedAmount,@UpdatedAmount,'Successed','29',@orderid,'Agent',@UserId,@calcstaxvalue)";
//                        int val1 = DataLib.InsOrUpdateParam(qry1, param2);

//                        string aafcode = DataLib.AgentAddFundsCode();
//                        SqlParameter[] param3 = new SqlParameter[2];
//                        param3[0] = new SqlParameter("@tcode", aafcode.ToString());
//                        param3[1] = new SqlParameter("@orderid", Session["orderid"].ToString());
//                        string strUpdate = "update Agent_TransactionTable set status='Approved',Transactioncode=@tcode where TransactionNo=@orderid";
//                        val1 = DataLib.InsOrUpdateParam(strUpdate, param3);
//                        // Mail Code Starts here
//                        SqlParameter[] sqlAgent = new SqlParameter[1];
//                        sqlAgent[0] = new SqlParameter("@agentId", strId);
//                        DataSet dsAgent = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "GetAgentdetails_sp", sqlAgent);
//                        string strToSend = "";
//                        if (dsAgent.Tables[0].Rows.Count > 0)
//                        {
//                            string filepath = Server.MapPath(ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\AddFundsAlert.html");
//                            System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
//                            strToSend = sr.ReadToEnd();
//                            strToSend = strToSend.Replace("#AgName#", dsAgent.Tables[0].Rows[0]["Name"].ToString());
//                            strToSend = strToSend.Replace("#AGUserId#", dsAgent.Tables[0].Rows[0]["UserId"].ToString());
//                            strToSend = strToSend.Replace("#Amt#", UpdatedAmount.ToString());
//                            strToSend = strToSend.Replace("#Mode#", "Amex Credit Card");
//                            strToSend = strToSend.Replace("#transno#", aafcode);
//                        }
//                        try
//                        {
//                            MailMessage sendMail = new MailMessage();
//                            sendMail.To = ConfigurationSettings.AppSettings["agentFundsAddMailId"].ToString();
//                            sendMail.From = "etickets@southerntravels.in";
//                            sendMail.Body = strToSend;
//                            sendMail.BodyFormat = MailFormat.Html;
//                            sendMail.Subject = aafcode + ": Add Funds By Amex Credit Card";
//                            SmtpMail.Send(sendMail);
//                        }
//                        catch (Exception ex)
//                        {
//                            Response.Write("<!-- " + ex.ToString() + " -->");
//                        }
//                        finally
//                        {
//                        }
//                        // Mail Code Ends here
//                        Response.Redirect("agent/agentaddfundsbycc.aspx?TransactionID=" + Session["orderid"].ToString());
//                    }
//                    if (dt5.Rows.Count > 0)
//                    {
//                        string balance;
//                        balance = dt5.Rows[0]["AvailableBalance"].ToString();

//                        decimal TotalBalance = Convert.ToDecimal(balance) + UpdatedAmount;
//                        SqlParameter[] param4 = new SqlParameter[6];
//                        param4[0] = new SqlParameter("@strId", strId);
//                        param4[1] = new SqlParameter("@UpdatedAmount", UpdatedAmount);
//                        param4[2] = new SqlParameter("@TotalBalance", TotalBalance);
//                        param4[3] = new SqlParameter("@orderid", Session["orderid"].ToString());
//                        param4[4] = new SqlParameter("@UserId", Session["UserId"].ToString());
//                        param4[5] = new SqlParameter("@calcstaxvalue", calcstaxvalue);
//                        string qry1 = "insert into onlinetransactiontable(agentid,agentcredit,availablebalance,Status,TransType,refno,BranchCode,UserName,CcChargeAmt) values(@strId,@UpdatedAmount,@TotalBalance,'Successed','29',@orderid,'Agent',@UserId,@calcstaxvalue)";
//                        int val1 = DataLib.InsOrUpdateParam(qry1, param4);
//                        if (val1 > 0)
//                        {
//                            string aafcode = DataLib.AgentAddFundsCode();
//                            string strUpdate = "update Agent_TransactionTable set status='Approved',Transactioncode='" + aafcode + "' where TransactionNo='" + Session["orderid"].ToString() + "'";
//                            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, strUpdate, false);
//                            // Mail Code Starts here
//                            SqlParameter[] sqlAgent = new SqlParameter[1];
//                            sqlAgent[0] = new SqlParameter("@agentId", strId);
//                            DataSet dsAgent = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "GetAgentdetails_sp", sqlAgent);
//                            string strToSend = "";
//                            if (dsAgent.Tables[0].Rows.Count > 0)
//                            {
//                                string filepath = Server.MapPath(ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\AddFundsAlert.html");
//                                System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
//                                strToSend = sr.ReadToEnd();
//                                strToSend = strToSend.Replace("#AgName#", dsAgent.Tables[0].Rows[0]["Name"].ToString());
//                                strToSend = strToSend.Replace("#AGUserId#", dsAgent.Tables[0].Rows[0]["UserId"].ToString());
//                                strToSend = strToSend.Replace("#Amt#", UpdatedAmount.ToString());
//                                strToSend = strToSend.Replace("#Mode#", "Amex Credit Card");
//                                strToSend = strToSend.Replace("#transno#", aafcode);
//                            }

//                            try
//                            {

//                                MailMessage sendMail = new MailMessage();
//                                sendMail.To = ConfigurationSettings.AppSettings["agentFundsAddMailId"].ToString();
//                                sendMail.From = "etickets@southerntravels.in";
//                                sendMail.Body = strToSend;
//                                sendMail.BodyFormat = MailFormat.Html;
//                                sendMail.Subject = aafcode + ": Add Funds By Amex Credit Card";
//                                SmtpMail.Send(sendMail);
//                            }
//                            catch (Exception ex)
//                            {
//                                Response.Write("<!-- " + ex.ToString() + " -->");
//                            }
//                            finally
//                            {
//                            }
//                            // Mail Code Ends here
//                            Response.Redirect("agent/agentaddfundsbycc.aspx?TransactionID=" + Session["orderid"].ToString());
//                        }
//                        else
//                        {
//                            Response.Write("sdfsdfsdfsd");
//                            Response.End();
//                        }
//                    }*/
//                        #endregion
//                    }
//                    else
//                    {
//                        Response.Write("<CENTER><B>Failure</B></CENTER>");
//                        Response.Redirect("agent/agentaddfundsbycc.aspx?TransactionID=");
//                        Response.Write(DateTime.Today.AddDays(-1));
//                    }
//                }
//                finally
//                {
//                    if (pClsObj != null)
//                    {
//                        pClsObj = null;
//                    }
//                }
//            }
//            if ((Session["mode"] != null) && (Session["mode"].ToString() == "car"))
//            {
//                string str1, payMode = "Online User";
//                #region Optimize Code
//                /*SqlParameter[] param6 = new SqlParameter[10];
//                param6[0] = new SqlParameter("@PaymentID", PaymentID);
//                param6[1] = new SqlParameter("@amt", amt);
//                param6[2] = new SqlParameter("@auth", auth);
//                param6[3] = new SqlParameter("@result", result);
//                param6[4] = new SqlParameter("@ref1", ref1);
//                param6[5] = new SqlParameter("@trackid", trackid);
//                param6[6] = new SqlParameter("@tranid", tranid);
//                param6[7] = new SqlParameter("@postdate", postdate);
//                param6[8] = new SqlParameter("@errort", errort);
//                param6[9] = new SqlParameter("@TransactionNo", Session["TransactionNo"].ToString());
//                str1 = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,Auth=@auth,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort where orderID=@TransactionNo";
//                int val = DataLib.InsOrUpdateParam(str1, param6);
//                SqlParameter[] param7 = new SqlParameter[1];
//                param7[0] = new SqlParameter("@strOrderId", strOrderId);
//                str1 = "UPDATE tbl_CarBookings_Log SET paySucceeded =1 where cabid =@strOrderId";
//                val = DataLib.InsOrUpdateParam(str1, param7);*/
//                #endregion
//                tbl_PaymentDetail pclsTbl = new tbl_PaymentDetail();
//                pclsTbl.isHDFC = false;
//                pclsTbl.IsPaid = 'Y';
//                pclsTbl.PaymentId = PaymentID;
//                pclsTbl.Amount = Convert.ToDecimal(amt);
//                pclsTbl.Auth = auth;
//                pclsTbl.result = result;
//                pclsTbl.@ref = ref1;
//                pclsTbl.trackid = trackid;
//                pclsTbl.transId = tranid;
//                pclsTbl.postDate = postdate;
//                pclsTbl.error = errort;
//                pclsTbl.orderId = Convert.ToString(Session["TransactionNo"]);
//                ClsAdo pClsObj = null;
//                try
//                {
//                    pClsObj = new ClsAdo();
//                    int Val1 = pClsObj.fnUpdatePaymentDetails(pclsTbl);



//                    int Val2 = pClsObj.fnUpdatetbl_CarBookings_Log(strOrderId, true);
//                }
//                finally
//                {
//                    if (pClsObj != null)
//                    {
//                        pClsObj = null;
//                    }
//                }
//                Response.Write("<CENTER><B>Success</B></CENTER>");
//                string Email, orderid;
//                Email = Session["EmailId"].ToString();
//                orderid = Session["orderid"].ToString();
//                Response.Redirect("additionalcustomerdetails.aspx?orderid=" + orderid + "&emailid=" + Email);
//            }

//            if (Session["splTour"] != null)
//            {
//                string strOrder = "";
//                if (Session["strQId"] != null)
//                    strOrder = Session["strQId"].ToString();
//                #region Optimize Code
//                /*SqlParameter[] param8 = new SqlParameter[1];
//                param8[0] = new SqlParameter("@strOrder", strOrder);
//                string strSelect = "select * from spl_tourEnquiry(nolock)  where id=@strOrder";
//                DataTable dt7 = DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, strSelect, param8);*/
//                #endregion
//                ClsAdo pclsObj = null;
//                try
//                {
//                    pclsObj = new ClsAdo();
//                    if (result == "CAPTURED")
//                    {
//                        DataTable dt7 = pclsObj.fnGetspl_tourEnquiry(strOrder);
//                        if (dt7.Rows.Count > 0)
//                        {

//                            string PNRNo = dt7.Rows[0]["pnrNo"].ToString();

//                            string strRefno = dt7.Rows[0]["ticketno"].ToString();
//                            string MobileNo = dt7.Rows[0]["Mobile"].ToString();
//                            int CustomerId = Convert.ToInt32(dt7.Rows[0]["Id"]);

//                            string FirstName = dt7.Rows[0]["FirstName"].ToString();
//                            string TourName = dt7.Rows[0]["TourName"].ToString();
//                            DateTime JourneyDate = Convert.ToDateTime(dt7.Rows[0]["JourneyDate"]);


//                            decimal ticketamt = Convert.ToDecimal(dt7.Rows[0]["fare"].ToString());
//                            decimal serviceT = Convert.ToDecimal(dt7.Rows[0]["servicetax"].ToString());
//                            decimal strCCharges = Convert.ToDecimal(dt7.Rows[0]["cccharges"].ToString());
//                            if (strCCharges <= 0)
//                                strCCharges = 0;
//                            decimal strCredit = (ticketamt + serviceT + strCCharges);

//                            tbl_PaymentDetail psplclsTbl = new tbl_PaymentDetail();
//                            psplclsTbl.isHDFC = true;
//                            psplclsTbl.IsPaid = 'Y';
//                            psplclsTbl.PaymentId = PaymentID;
//                            psplclsTbl.Amount = Convert.ToDecimal(amt);
//                            psplclsTbl.result = result;
//                            psplclsTbl.@ref = ref1;
//                            psplclsTbl.trackid = trackid;
//                            psplclsTbl.transId = tranid;
//                            psplclsTbl.postDate = postdate;
//                            psplclsTbl.error = errort;
//                            psplclsTbl.orderId = strRefno;
//                            int val = pclsObj.fnUpdatePaymentDetails(psplclsTbl);
//                            if (val > 0)
//                            {
//                                val = pclsObj.fnOnlineUpdateSPLTourDetail(psplclsTbl, "Amex Payment", ticketamt, serviceT, strCCharges, strCredit,
//                                   Convert.ToDecimal(strOrder));
//                                if (val > 0)
//                                {
//                                    try
//                                    {
//                                        if (MobileNo != "")
//                                        {
//                                            //string SMSText = "Booking PNR No: {0}, TktNo(s): {1}. Check more via Login.";
//                                            //SMSText = String.Format(SMSText, PNRNo, strRefno);
//                                            //DataLib.sendsms(Convert.ToInt32(CustomerId), MobileNo.Trim(), SMSText, "OnLineUser", "EBK0001", "splTour", strRefno, PNRNo);
//                                            DataTable ldtSMSDetails = pclsObj.fnGetspl_tourEnquiry(strOrder);
//                                            if (ldtSMSDetails.Rows.Count > 0)
//                                            {
//                                                strRefno = ldtSMSDetails.Rows[0]["ticketno"].ToString();
//                                            }
//                                            string SMSText = "Dear Guest, Booking for {0} on {1} with Ticket No {2} is confirmed, Your PNR is: {3}";
//                                            SMSText = String.Format(SMSText, TourName, JourneyDate.ToString("dd/MMM/yyyy"), strRefno, PNRNo);
//                                            DataLib.sendsms(Convert.ToInt32(CustomerId), MobileNo.Trim(), SMSText, FirstName, "EBK0001", "splTour", strRefno, PNRNo);
//                                        }
//                                    }
//                                    catch { }
//                                    if (Convert.ToString(Session["Kerala"]) == "Kerala")
//                                        Response.Redirect("http://www.atriptokerala.com/ticket.html?Id=" + strOrder + "");
//                                    else
//                                        Response.Redirect("SpltourTicket.aspx?Id=" + strOrder + "");
//                                    //Response.Redirect("Special-Tour-Tickets_" + strOrder + "");

//                                }
//                                else
//                                {
//                                    if (Convert.ToString(Session["Kerala"]) == "Kerala")
//                                        Response.Redirect("http://www.atriptokerala.com/Error.html");
//                                    else
//                                        Response.Redirect("PaymentError.aspx");
//                                }
//                            }
//                        }

//                    }
//                    else
//                        Response.Redirect("PaymentError.aspx");
//                    #region Optimize Code
//                    /*
//                    DataTable dt7 = pClsObj.fnGetspl_tourEnquiry(strOrder);
//                    if (dt7.Rows.Count > 0)
//                    {
//                        if (result == "CAPTURED")
//                        {

//                            string PNRNo = dt7.Rows[0]["pnrNo"].ToString();
//                            string MobileNo = dt7.Rows[0]["Mobile"].ToString();
//                            int CustomerId = Convert.ToInt32(dt7.Rows[0]["Id"]);
//                            string strRefno = dt7.Rows[0]["ticketno"].ToString();
//                            DateTime JourneyDate = Convert.ToDateTime(dt7.Rows[0]["JourneyDate"]);

//                            string CustomerFName = dt7.Rows[0]["FirstName"].ToString();
//                            string TourName = dt7.Rows[0]["TourName"].ToString();



//                            decimal ticketamt = Convert.ToDecimal(dt7.Rows[0]["fare"].ToString());
//                            decimal serviceT = Convert.ToDecimal(dt7.Rows[0]["servicetax"].ToString());
//                            decimal strCCharges = Convert.ToDecimal(dt7.Rows[0]["cccharges"].ToString());
//                            if (strCCharges <= 0)
//                            {
//                                strCCharges = 0;
//                            }
//                            decimal strCredit = (ticketamt + serviceT + strCCharges);

//                            tbl_PaymentDetail psplclsTbl = new tbl_PaymentDetail();
//                            psplclsTbl.isHDFC = false;
//                            psplclsTbl.IsPaid = 'Y';
//                            psplclsTbl.PaymentId = PaymentID;
//                            psplclsTbl.Amount = Convert.ToDecimal(amt);
//                            psplclsTbl.result = result;
//                            psplclsTbl.@ref = ref1;
//                            psplclsTbl.trackid = trackid;
//                            psplclsTbl.transId = tranid;
//                            psplclsTbl.postDate = postdate;
//                            psplclsTbl.error = errort;
//                            psplclsTbl.orderId = strRefno;

//                            int val = pClsObj.fnUpdatePaymentDetails(psplclsTbl);


//                            OnlineTransactionTable pclsTbl = new OnlineTransactionTable();
//                            pclsTbl.AgentId = 0;
//                            pclsTbl.RefNo = strRefno;
//                            pclsTbl.TransType = 27;
//                            pclsTbl.TicketAmount = ticketamt;
//                            pclsTbl.PaymentMode = "Amex Payment";
//                            pclsTbl.UserName = "OnlineUser";
//                            pclsTbl.BranchCode = "EBK0001";
//                            pclsTbl.Status = "S";
//                            pclsTbl.ServiceTax = serviceT;
//                            pclsTbl.ccChargeAmt = strCCharges;
//                            pclsTbl.Credit = strCredit;
//                            val = pClsObj.fnInsertSplTour_OnlineTransactionTable(pclsTbl);
//                            if (val > 0)
//                            {


//                            }
//                            if (val > 0)
//                            {

//                                val = pClsObj.fnUpdatespl_tourEnquiry('S', Convert.ToDecimal(strOrder));
//                            }
//                            if (val > 0)
//                            {


//                                try
//                                {
//                                    if (MobileNo != "")
//                                    {


//                                        string SMSText = "Dear Guest, Booking for {0} on {1} with Ticket No {2} is confirmed, Your PNR is: {3}";
//                                        SMSText = String.Format(SMSText, TourName, JourneyDate, strRefno, PNRNo);

//                                        //DataLib.sendsms(Convert.ToInt32(crowid), Session["mobile"].ToString().Trim(), strSms.ToString(), "OnLineUser", "EBK0001");
//                                        DataLib.sendsms(Convert.ToInt32(CustomerId), MobileNo.Trim(), SMSText, "OnLineUser", "EBK0001", "splTour", strRefno, PNRNo);



//                                    }



//                                }
//                                catch { }


//                                if (Convert.ToString(Session["Kerala"]) == "Kerala")
//                                {
//                                    Response.Redirect("http://www.atriptokerala.com/ticket.html?Id=" + strOrder + "");
//                                }
//                                else
//                                {
//                                    Response.Redirect("SpltourTicket.aspx?Id=" + strOrder + "");
//                                }
//                            }
//                            else
//                            {
//                                if (Convert.ToString(Session["Kerala"]) == "Kerala")
//                                {
//                                    Response.Redirect("http://www.atriptokerala.com/Error.html");
//                                }
//                                else
//                                {
//                                    Response.Redirect("PaymentError.aspx");
//                                }
//                            }
//                        }
//                        else
//                        {
//                            string strRefno = dt7.Rows[0]["ticketno"].ToString();
//                            decimal ticketamt = Convert.ToDecimal(dt7.Rows[0]["fare"].ToString());
//                            decimal serviceT = Convert.ToDecimal(dt7.Rows[0]["servicetax"].ToString());
//                            #region Optimize Code
//                             SqlParameter[] param12 = new SqlParameter[9];
//                        param12[0] = new SqlParameter("@PaymentID", PaymentID);
//                        param12[1] = new SqlParameter("@amt", amt);
//                        param12[2] = new SqlParameter("@result", result);
//                        param12[3] = new SqlParameter("@ref1", ref1);
//                        param12[4] = new SqlParameter("@trackid", trackid);
//                        param12[5] = new SqlParameter("@tranid", tranid);
//                        param12[6] = new SqlParameter("@postdate", postdate);
//                        param12[7] = new SqlParameter("@errort", errort);
//                        param12[8] = new SqlParameter("@strRefno", strRefno);
//                        string str1 = "update tbl_PaymentDetails set IsAmex=1, IsPaid='N', PaymentId=@PaymentID, Amount=@amt,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort where orderID=@strRefno";
//                        int val = DataLib.InsOrUpdateParam(str1, param12);
//                        SqlParameter[] param13 = new SqlParameter[1];
//                        param13[0] = new SqlParameter("@strOrder", strOrder);
//                        string str5 = "update  spl_tourEnquiry set status='N' where id=@strOrder";
//                        val = DataLib.InsOrUpdateParam(str5, param13);
//                            #endregion
//                            tbl_PaymentDetail pclsTbl = new tbl_PaymentDetail();
//                            pclsTbl.isHDFC = false;
//                            pclsTbl.IsPaid = 'N';
//                            pclsTbl.PaymentId = PaymentID;
//                            pclsTbl.Amount = Convert.ToDecimal(amt);
//                            pclsTbl.result = result;
//                            pclsTbl.@ref = ref1;
//                            pclsTbl.trackid = trackid;
//                            pclsTbl.transId = tranid;
//                            pclsTbl.postDate = postdate;
//                            pclsTbl.error = errort;
//                            pclsTbl.orderId = strRefno;

//                            int Val = pClsObj.fnUpdatePaymentDetails(pclsTbl);

//                            if (Convert.ToString(Session["Kerala"]) == "Kerala")
//                            {
//                                Response.Redirect("http://www.atriptokerala.com/Error.html");
//                            }
//                            else
//                            {
//                                Response.Redirect("PaymentError.aspx");
//                            }
//                        }
//                    }*/
//                    #endregion
//                }
//                finally
//                {
//                    if (pclsObj != null)
//                    {
//                        pclsObj = null;
//                    }
//                }

//            }
//            if ((Session["mode"] != null) && (Session["mode"].ToString() == "hotel"))
//            {

//                if (result == "CAPTURED")
//                {
//                    string oID = "HTL000" + strOrderId;
//                    #region Optimize Code
//                    /*SqlParameter[] param14 = new SqlParameter[9];
//                    param14[0] = new SqlParameter("@PaymentID", PaymentID);
//                    param14[1] = new SqlParameter("@amt", amt);
//                    param14[2] = new SqlParameter("@result", result);
//                    param14[3] = new SqlParameter("@ref1", ref1);
//                    param14[4] = new SqlParameter("@trackid", trackid);
//                    param14[5] = new SqlParameter("@tranid", tranid);
//                    param14[6] = new SqlParameter("@postdate", postdate);
//                    param14[7] = new SqlParameter("@errort", errort);
//                    param14[8] = new SqlParameter("@Orderid", oID);
//                    string str1 = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort  where orderID=@Orderid";
//                    int val = DataLib.InsOrUpdateParam(str1, param14);*/
//                    #endregion
//                    tbl_PaymentDetail pclsTbl = new tbl_PaymentDetail();
//                    pclsTbl.isHDFC = false;
//                    pclsTbl.IsPaid = 'Y';
//                    pclsTbl.PaymentId = PaymentID;
//                    pclsTbl.Amount = Convert.ToDecimal(amt);
//                    pclsTbl.result = result;
//                    pclsTbl.@ref = ref1;
//                    pclsTbl.trackid = trackid;
//                    pclsTbl.transId = tranid;
//                    pclsTbl.postDate = postdate;
//                    pclsTbl.error = errort;
//                    pclsTbl.orderId = oID;
//                    ClsAdo pClsObj = null;
//                    try
//                    {
//                        pClsObj = new ClsAdo();
//                        int Val = pClsObj.fnUpdatePaymentDetails(pclsTbl);
//                    }
//                    finally
//                    {
//                        if (pClsObj != null)
//                        {
//                            pClsObj = null;
//                        }
//                    }
//                    Response.Redirect("htlTicket.aspx");
//                }
//                else
//                {
//                    Response.Redirect("PaymentError.aspx");
//                }

//            }
//            if (Session["INTTours"] != null)
//            {
//                if (Session["INTTours"].ToString() == "InterNational")
//                {
//                    ClsAdo pClsObj = null;
//                    try
//                    {
//                        pClsObj = new ClsAdo();
//                        if (result == "CAPTURED")
//                        {
//                            string strTicket = DataLib.INTticketCode();

//                            string strorderid = Session["strINTId"].ToString();
//                            string[] para = new string[3];
//                            string[] values = new string[3];
//                            #region Optimize Code
//                            /*para[0] = "@TicketCode";
//                        para[1] = "@strIntOrderId";
//                        para[2] = "@Status";

//                        values[0] = DataLib.funClear(strTicket);
//                        values[1] = DataLib.funClear(strorderid);
//                        values[2] = "S";

//                        string strUpdate = "update tbl_IntPaymentDetails set Status=@status,Ticketno=@TicketCode where PnrNo=@strIntOrderId";
//                        DataLib.execute_query(strUpdate, para, values);
//                        SqlParameter[] param15 = new SqlParameter[9];
//                        param15[0] = new SqlParameter("@PaymentID", PaymentID);
//                        param15[1] = new SqlParameter("@amt", amt);
//                        param15[2] = new SqlParameter("@result", result);
//                        param15[3] = new SqlParameter("@ref1", ref1);
//                        param15[4] = new SqlParameter("@trackid", trackid);
//                        param15[5] = new SqlParameter("@tranid", tranid);
//                        param15[6] = new SqlParameter("@postdate", postdate);
//                        param15[7] = new SqlParameter("@errort", errort);
//                        param15[8] = new SqlParameter("@strINTId", Session["strINTId"].ToString());
//                        string str1 = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort,PayMode='Amex Payment'  where orderID=@strINTId";
//                        int val = DataLib.InsOrUpdateParam(str1, param15);*/
//                            #endregion
//                            int val = pClsObj.fnUpdatetbl_IntPaymentDetails(strorderid, strTicket, 'S');
//                            tbl_PaymentDetail pIntclsTbl = new tbl_PaymentDetail();
//                            pIntclsTbl.isHDFC = false;
//                            pIntclsTbl.IsPaid = 'Y';
//                            pIntclsTbl.PaymentId = PaymentID;
//                            pIntclsTbl.Amount = Convert.ToDecimal(amt);
//                            pIntclsTbl.result = result;
//                            pIntclsTbl.@ref = ref1;
//                            pIntclsTbl.trackid = trackid;
//                            pIntclsTbl.transId = tranid;
//                            pIntclsTbl.postDate = postdate;
//                            pIntclsTbl.error = errort;
//                            pIntclsTbl.payMode = "AMEX Payment";
//                            pIntclsTbl.orderId = Convert.ToString(Session["strINTId"]);

//                            val = pClsObj.fnUpdatePaymentDetails_INTTours(pIntclsTbl);

//                            if (val != 0)
//                            {

//                                int paxnum = 0;
//                                int child = Convert.ToInt32(Session["IntChild"]);
//                                int adult = Convert.ToInt32(Session["IntAdults"]);
//                                if (child > 0)
//                                {
//                                    paxnum = adult + child;
//                                }
//                                else
//                                {
//                                    paxnum = adult;
//                                }
//                                string OName;
//                                char sex = 'M';
//                                int i, OAge;
//                                for (i = 1; i <= Convert.ToInt16(paxnum); i++)
//                                {
//                                    OName = "*****";
//                                    OAge = 0;
//                                    string strOrderIds = DataLib.funClear(strorderid);
//                                    string stroName = DataLib.funClear(OName);
//                                    #region Optimize Code
//                                    /* SqlParameter[] param16 = new SqlParameter[4];
//                                param16[0] = new SqlParameter("@strorderid", strOrderIds);
//                                param16[1] = new SqlParameter("@OName", stroName);
//                                param16[2] = new SqlParameter("@OAge", OAge);
//                                param16[3] = new SqlParameter("@sex", sex);
//                                string str21 = "insert into onlinepassenger (orderid,name,age,sex) " + " values (@strorderid,@OName,@OAge,@sex)";
//                                val = DataLib.InsOrUpdateParam(str21, param16);
//                                SqlParameter[] paramt = new SqlParameter[1];
//                                paramt[0] = new SqlParameter("@strorderid", strorderid);
//                                string strGetdate = "select * from tbl_IntPaymentDetails(nolock) where pnrno=@strorderid";
//                                DataTable dt = DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, strGetdate, paramt);*/
//                                    #endregion
//                                    val = pClsObj.fnInsertOnlinePassenger(strOrderIds, stroName, Convert.ToInt16(OAge), sex);
//                                    DataTable dt = pClsObj.fnGetIntPaymentDetails(strorderid);
//                                    decimal newamt = 0, st = 0, dis = 0, Tamt = 0;
//                                    string strTour = "";
//                                    if (dt.Rows.Count > 0)
//                                    {
//                                        newamt = Convert.ToDecimal(dt.Rows[0]["Amount"].ToString());
//                                        st = Convert.ToDecimal(dt.Rows[0]["servicetax"].ToString());
//                                        dis = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
//                                        Tamt = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString());
//                                        strTour = dt.Rows[0]["TourName"].ToString();
//                                    }
//                                    #region Optimize Code
//                                    /*SqlParameter[] param17 = new SqlParameter[6];
//                                param17[0] = new SqlParameter("@strTicket", strTicket);
//                                param17[1] = new SqlParameter("@amt", amt);
//                                param17[2] = new SqlParameter("@newamt", newamt);
//                                param17[3] = new SqlParameter("@dis", dis);
//                                param17[4] = new SqlParameter("@st", st);
//                                param17[5] = new SqlParameter("@strTour", strTour);
//                                string strInsert = @"insert into onlinetransactiontable(agentid,refno,transtype,ticketamount,paymentmode,credit,UserName,branchcode,discount,servicetax,tourname)
//                                values('0',@strTicket,'32',@amt,'Amex Payment',@newamt,'OnLineUser','EBK0001',@dis,@st,@strTour)";
//                                val = DataLib.InsOrUpdateParam(strInsert, param17);*/
//                                    #endregion
//                                    OnlineTransactionTable objTblOnline = new OnlineTransactionTable();
//                                    objTblOnline.AgentId = 0;
//                                    objTblOnline.RefNo = strTicket;
//                                    objTblOnline.TransType = 32;
//                                    objTblOnline.TicketAmount = Convert.ToDecimal(amt);
//                                    objTblOnline.PaymentMode = "Net Banking";
//                                    objTblOnline.Credit = newamt;
//                                    objTblOnline.UserName = "OnLineUser";
//                                    objTblOnline.BranchCode = "EBK0001";
//                                    objTblOnline.Discount = dis;
//                                    objTblOnline.ServiceTax = st;
//                                    objTblOnline.TourName = strTour;
//                                    val = pClsObj.fnInsertINTTour_OnlineTransactionTable(objTblOnline);
//                                    Response.Redirect("additionalcustomerdetails.aspx?orderid=" + strorderid + "");
//                                }
//                            }
//                            else
//                            {
//                                Response.Redirect("PaymentError.aspx");
//                            }
//                        }
//                    }
//                    finally
//                    {
//                        if (pClsObj != null)
//                        {
//                            pClsObj = null;
//                        }
//                    }
//                }
//            }

//            if (Session["GRPTours"] != null)
//            {
//                if (Session["GRPTours"].ToString() == "GroupBooking")
//                {
//                    ClsAdo pClsObj = null;
//                    try
//                    {
//                        pClsObj = new ClsAdo();
//                        if (result == "CAPTURED")
//                        {
//                            string strorderid = Session["GRPId"].ToString();
//                            #region Optimize Code
//                            /* SqlParameter[] param21 = new SqlParameter[9];
//                        param21[0] = new SqlParameter("@PaymentID", PaymentID);
//                        param21[1] = new SqlParameter("@amt", amt);
//                        param21[2] = new SqlParameter("@result", result);
//                        param21[3] = new SqlParameter("@ref1", ref1);
//                        param21[4] = new SqlParameter("@trackid", trackid);
//                        param21[5] = new SqlParameter("@tranid", tranid);
//                        param21[6] = new SqlParameter("@postdate", postdate);
//                        param21[7] = new SqlParameter("@errort", errort);
//                        param21[8] = new SqlParameter("@orderid", Session["GRPId"].ToString());
//                        string strPayment = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,
//                        * ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort,PayMode='Amex Payment' 
//                        * where orderID=@orderid and [Id]=" + Session["val"].ToString() + " ";
//                        int val = DataLib.InsOrUpdateParam(strPayment, param21);*/
//                            #endregion
//                            tbl_PaymentDetail pGPRclsTbl = new tbl_PaymentDetail();
//                            pGPRclsTbl.isHDFC = false;
//                            pGPRclsTbl.IsPaid = 'Y';
//                            pGPRclsTbl.PaymentId = PaymentID;
//                            pGPRclsTbl.Amount = Convert.ToDecimal(amt);
//                            pGPRclsTbl.result = result;
//                            pGPRclsTbl.@ref = ref1;
//                            pGPRclsTbl.trackid = trackid;
//                            pGPRclsTbl.transId = tranid;
//                            pGPRclsTbl.postDate = postdate;
//                            pGPRclsTbl.error = errort;
//                            pGPRclsTbl.payMode = "AMEX Payment";
//                            pGPRclsTbl.orderId = Convert.ToString(Session["GRPId"]);
//                            pGPRclsTbl.id = Convert.ToDecimal(Session["val"]);

//                            int val = pClsObj.fnUpdatePaymentDetails_GRPTours(pGPRclsTbl);
//                            if (Convert.ToString(val) == "1")
//                            {
//                                if (Convert.ToString(val) == "1" && Session["FirstPay"] != null)
//                                {
//                                    Session["EndUserId"] = "EndUserId";
//                                    val = pClsObj.fnUpdatetbl_NewGroupbooking_tbl(strorderid, "", "Amex Payment", 'S');
//                                    if (val > 0)
//                                    {
//                                        Response.Redirect("additionalcustomerdetails.aspx?orderid=" + strorderid + "");
//                                    }
//                                    #region Optimize Code
//                                    /*string strTicket = DataLib.GRPticketCode();
//                                    string[] para = new string[3];
//                                    string[] values = new string[3];

//                                    val = pClsObj.fnUpdatetbl_NewGroupbooking_tbl(strorderid, strTicket, pGPRclsTbl.payMode, 'S');
//                                    int paxnum = 0;
//                                    int child = Convert.ToInt32(Session["IntChild"]);
//                                    int adult = Convert.ToInt32(Session["IntAdults"]);
//                                    if (child > 0)
//                                    {
//                                        paxnum = adult + child;
//                                    }
//                                    else
//                                    {
//                                        paxnum = adult;
//                                    }
//                                    string OName; char sex = 'M';
//                                    int i, OAge;
//                                    for (i = 1; i <= Convert.ToInt16(paxnum); i++)
//                                    {
//                                        OName = "*****";
//                                        OAge = 0;

//                                        val = pClsObj.fnInsertOnlinePassenger(strorderid, OName, Convert.ToInt16(OAge), sex);
//                                    }

//                                    DataTable dt = pClsObj.fnGetNewGroupbookingDetails(strorderid, "S");
//                                    decimal st = 0, dis = 0, Tamt = 0, GrossTotal = 0.0m;
//                                    string strTour = "", strBranchCode = "";
//                                    if (dt.Rows.Count > 0)
//                                    {
//                                        st = Convert.ToDecimal(dt.Rows[0]["staxvalue"].ToString());
//                                        dis = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
//                                        Tamt = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString());
//                                        strBranchCode = dt.Rows[0]["BranchCode"].ToString();
//                                        strTour = dt.Rows[0]["TourName"].ToString();
//                                        GrossTotal = ((Tamt + st) - dis);
//                                        Session["EndUserId"] = "EndUserId";
//                                    }

//                                    Onlinetransaction_Advance GPRClsObjAdv = new Onlinetransaction_Advance();
//                                    GPRClsObjAdv.RefNo = strTicket;
//                                    GPRClsObjAdv.TransactionType = 30;
//                                    GPRClsObjAdv.Ticketamount = GrossTotal;
//                                    GPRClsObjAdv.PaymentMode = "AMEX Payment";
//                                    GPRClsObjAdv.AdvancePaid = Convert.ToDecimal(amt);
//                                    GPRClsObjAdv.UserId = "OnLineUser";
//                                    GPRClsObjAdv.BranchCode = "EBK0001";
//                                    GPRClsObjAdv.TourName = strTour;
//                                    val = pClsObj.fnInsert_GRPToursOnlinetransaction_Advance(GPRClsObjAdv);

//                                    OnlineTransactionTable GPRClsObj = new OnlineTransactionTable();
//                                    GPRClsObj.AgentId = 0;
//                                    GPRClsObj.RefNo = strTicket;
//                                    GPRClsObj.TransType = 30;
//                                    GPRClsObj.TicketAmount = Convert.ToDecimal(Tamt);
//                                    GPRClsObj.PaymentMode = "AMEX Payment";
//                                    GPRClsObj.Credit = Convert.ToDecimal(amt);
//                                    GPRClsObj.UserName = "OnLineUser";
//                                    GPRClsObj.BranchCode = "EBK0001";
//                                    GPRClsObj.Discount = Convert.ToDecimal(dis);
//                                    GPRClsObj.ServiceTax = Convert.ToDecimal(st);
//                                    GPRClsObj.TourName = strTour;
//                                    val = pClsObj.fnInsert_GRPToursOnlinetransaction(GPRClsObj);
//                                    if (val > 0)
//                                    {
//                                        Response.Redirect("additionalcustomerdetails.aspx?orderid=" + strorderid + "");
//                                    }*/
//                                    #endregion
//                                }
//                                // IF BALANCE CLEAR this Block Will Execute
//                                else
//                                {
//                                    int Chk = pClsObj.fnGetNewGroupbookingDetails(strorderid, "S", Convert.ToDecimal(amt));
//                                    if (Chk > 0)
//                                    {
//                                        Response.Redirect("GroupTicketPage.aspx");
//                                    }
//                                    #region Optimize Code
//                                    /* DataTable dt = pClsObj.fnGetNewGroupbookingDetails(strorderid, "S");
//                                    decimal st = 0, dis = 0, Tamt = 0, GrossTotal = 0.0m, Advance = 0.0m, newCr = 0.0m;
//                                    string strTour = "", strTicketNo = "", strBranchCode = "";
//                                    if (dt.Rows.Count > 0)
//                                    {
//                                        st = Convert.ToDecimal(dt.Rows[0]["staxvalue"].ToString());
//                                        dis = Convert.ToDecimal(dt.Rows[0]["discount"].ToString());
//                                        Tamt = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString());
//                                        Advance = Convert.ToDecimal(dt.Rows[0]["Advance"].ToString());
//                                        strTour = dt.Rows[0]["TourName"].ToString();
//                                        strTicketNo = dt.Rows[0]["TicketNumber"].ToString();
//                                        GrossTotal = ((Tamt + st) - dis);
//                                        newCr = Convert.ToDecimal(amt) + Advance;
//                                        strBranchCode = dt.Rows[0]["BranchCode"].ToString();

//                                    }
//                                    // If Booking Done Via Branch And Bal Clear On line
//                                    if (strBranchCode != "EBK0001")
//                                    {

//                                        decimal strTotalOnlinePaid = 0.0m;
//                                        Onlinetransaction_Advance GPRClsObjAdv = new Onlinetransaction_Advance();
//                                        GPRClsObjAdv.RefNo = strTicketNo;
//                                        GPRClsObjAdv.TransactionType = 34;
//                                        GPRClsObjAdv.Ticketamount = GrossTotal;
//                                        GPRClsObjAdv.PaymentMode = "AMEX Payment";
//                                        GPRClsObjAdv.AdvancePaid = Convert.ToDecimal(amt);
//                                        GPRClsObjAdv.UserId = "OnLineUser";
//                                        GPRClsObjAdv.BranchCode = "EBK0001";
//                                        GPRClsObjAdv.TourName = strTour;
//                                        val = pClsObj.fnInsert_GRPToursOnlinetransaction_Advance(GPRClsObjAdv);

//                                        DataTable ldtRecSet = pClsObj.fnGetFromAdvanceTable(strTicketNo);
//                                        DataSet dtticketdetils = new DataSet();
//                                        if (ldtRecSet != null)
//                                        {
//                                            dtticketdetils.Tables.Add(ldtRecSet);

//                                        }

//                                        if (dtticketdetils.Tables[0].Rows[0]["OnlineClear"] == null || dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString() == "")
//                                        {
//                                            strTotalOnlinePaid = Convert.ToDecimal(amt);
//                                        }
//                                        else
//                                        {
//                                            strTotalOnlinePaid = (Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString()) + Convert.ToDecimal(amt));
//                                        }
//                                        val = pClsObj.fnUpdate_BranchGRPToursonlinetransactiontable(Convert.ToDecimal(newCr), strTicketNo,
//                                            Convert.ToDecimal(strTotalOnlinePaid));

//                                    }

//                                     // If Booking Done Via Control panel Off Line Booking And Bal Clear On line
//                                    else
//                                    {

//                                        Onlinetransaction_Advance GPRClsObjAdv = new Onlinetransaction_Advance();
//                                        GPRClsObjAdv.RefNo = strTicketNo;
//                                        GPRClsObjAdv.TransactionType = 30;
//                                        GPRClsObjAdv.Ticketamount = GrossTotal;
//                                        GPRClsObjAdv.PaymentMode = "AMEX Payment";
//                                        GPRClsObjAdv.AdvancePaid = Convert.ToDecimal(amt);
//                                        GPRClsObjAdv.UserId = "OnLineUser";
//                                        GPRClsObjAdv.BranchCode = "EBK0001";
//                                        GPRClsObjAdv.TourName = strTour;
//                                        val = pClsObj.fnInsert_GRPToursOnlinetransaction_Advance(GPRClsObjAdv);
//                                        val = pClsObj.fnUpdate_GRPToursonlinetransactiontable(Convert.ToDecimal(newCr), strTicketNo);
//                                    }

//                                    val = pClsObj.fnUpdateBranchtbl_NewGroupbooking_tbl(Convert.ToDecimal(newCr), strTicketNo);
//                                    Response.Redirect("GroupTicketPage.aspx");*/
//                                    #endregion
//                                }

//                            }
//                        }
//                        else
//                        {
//                            Response.Redirect("PaymentError.aspx");
//                        }
//                    }
//                    finally
//                    {
//                        if (pClsObj != null)
//                        {
//                            pClsObj = null;
//                        }

//                    }
//                }
//            }
//            if (Session["ITI_GRPTours"] != null && Session["ITI_GRPTours"].ToString() == "ITI_GroupBooking")
//            {

//                ClsAdo pclsObj = null;
//                clsEndUserDBML pPLOrOther = null;
//                try
//                {
//                    pclsObj = new ClsAdo();
//                    if (result == "CAPTURED")
//                    {
//                        if (Session["ITI_FirstPay"] != null)
//                        {
//                            Session["EndUserId"] = "EndUserId";
//                            pPLOrOther = new clsEndUserDBML();
//                            try
//                            {
//                                int lStatus = pPLOrOther.fnSaveGroupItineraryBooking(PaymentID, Convert.ToDecimal(amt), result, ref1, tranid, tranid, postdate, errort,
//                                       "", "", "", "", -1,
//                                       OrderId, "AMEX Payment", "GroupItineraryBooking", 0);
//                                if (lStatus > 0)
//                                {
//                                    Response.Redirect("additionalcustomerdetails.aspx?orderid=" + OrderId + "&Code=IT");
//                                }
//                            }
//                            finally
//                            {
//                                pPLOrOther = null;
//                            }
//                        }
//                        else
//                        {

//                            pPLOrOther = new clsEndUserDBML();
//                            int Chk = pPLOrOther.fnGroupItineraryBookingBalanceClear(PaymentID, Convert.ToDecimal(amt), result, ref1, tranid, tranid, postdate, errort,
//                                      "", "", "", "", -1,
//                                      OrderId, "AMEX Payment", "GroupItineraryBooking", 0);
//                            if (Chk > 0)
//                            {
//                                Response.Redirect("GroupItineraryTicket.aspx?orderid=" + OrderId + "&Code=IT");
//                            }
//                        }
//                    }

//                }
//                finally
//                {
//                    if (pclsObj != null)
//                    {
//                        pclsObj = null;
//                    }
//                    if (pPLOrOther != null)
//                    {
//                        pPLOrOther = null;
//                    }
//                }
//            }
//            if ((Session["BranchCumOn"] != null) && (Session["BranchCumOn"].ToString() == "BranchCumOnLine"))
//            {
//                #region Optimize Code
//                /*SqlParameter[] paramst = new SqlParameter[1];
//                paramst[0] = new SqlParameter("@TicketNo", Session["BranchTicket"].ToString());
//                DataSet dtticketdetils = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "GetFromAdvanceTable_sp", paramst);*/
//                #endregion
//                ClsAdo pClsObj = null;
//                try
//                {
//                    pClsObj = new ClsAdo();
//                    DataTable ldtRecSet = pClsObj.fnGetFromAdvanceTable(Convert.ToString(Session["BranchTicket"]));
//                    DataSet dtticketdetils = new DataSet();
//                    if (ldtRecSet != null)
//                    {
//                        dtticketdetils.Tables.Add(ldtRecSet);
//                    }
//                    if (dtticketdetils.Tables[0].Rows.Count > 0)
//                    {
//                        #region Optimize Code
//                        /* SqlParameter[] param21 = new SqlParameter[10];
//                    param21[0] = new SqlParameter("@PaymentID", PaymentID);
//                    param21[1] = new SqlParameter("@amt", Session["BalancePaid"].ToString());
//                    param21[2] = new SqlParameter("@result", result);
//                    param21[3] = new SqlParameter("@ref1", ref1);
//                    param21[4] = new SqlParameter("@trackid", trackid);
//                    param21[5] = new SqlParameter("@tranid", tranid);
//                    param21[6] = new SqlParameter("@postdate", postdate);
//                    param21[7] = new SqlParameter("@errort", errort);
//                    param21[8] = new SqlParameter("@orderid", dtticketdetils.Tables[0].Rows[0]["orderid"].ToString());
//                    param21[9] = new SqlParameter("@RowID", Session["val"].ToString());
//                    string strPayment = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort where orderID=@orderid and id=@RowID";
//                    int val = DataLib.InsOrUpdateParam(strPayment, param21);*/
//                        #endregion
//                        tbl_PaymentDetail pGPRclsTbl = new tbl_PaymentDetail();
//                        pGPRclsTbl.isHDFC = false;
//                        pGPRclsTbl.IsPaid = 'Y';
//                        pGPRclsTbl.PaymentId = PaymentID;
//                        pGPRclsTbl.Amount = Convert.ToDecimal(Session["BalancePaid"]);
//                        pGPRclsTbl.result = result;
//                        pGPRclsTbl.@ref = ref1;
//                        pGPRclsTbl.trackid = trackid;
//                        pGPRclsTbl.transId = tranid;
//                        pGPRclsTbl.postDate = postdate;
//                        pGPRclsTbl.error = errort;
//                        pGPRclsTbl.payMode = "AMEX Payment";
//                        pGPRclsTbl.orderId = Convert.ToString(dtticketdetils.Tables[0].Rows[0]["orderid"]);
//                        pGPRclsTbl.id = Convert.ToDecimal(Session["val"]);

//                        int val = pClsObj.fnUpdatePaymentDetails_GRPTours(pGPRclsTbl);
//                        if (result == "CAPTURED")
//                        {
//                            if (dtticketdetils.Tables[0].Rows[0]["BranchCode"].ToString() != "EBK0001")
//                            {
//                                if (dtticketdetils.Tables[0].Rows[0]["TransactionType"].ToString() == "6")
//                                {
//                                    #region Optimize Code
//                                    /*SqlParameter[] sqlClearBal = new SqlParameter[8];
//                                sqlClearBal[0] = new SqlParameter("@TicketNo", Session["BranchTicket"].ToString());
//                                sqlClearBal[1] = new SqlParameter("@BranchCode", "EBK001");
//                                sqlClearBal[2] = new SqlParameter("@UserId", "0");
//                                sqlClearBal[3] = new SqlParameter("@TicketAmount", dtticketdetils.Tables[0].Rows[0]["TicketAmount"].ToString());
//                                sqlClearBal[4] = new SqlParameter("@AdvancePaid", Session["BalancePaid"].ToString());
//                                sqlClearBal[5] = new SqlParameter("@TourName", dtticketdetils.Tables[0].Rows[0]["TourName"].ToString());
//                                sqlClearBal[6] = new SqlParameter("@TransactionType", "32");
//                                sqlClearBal[7] = new SqlParameter("@PaymentMode", "2");

//                                string strInsert = @"insert into Onlinetransaction_Advance(refNo,BranchCode,UserId,TicketAmount,AdvancePaid,TourName,TransactionType,PaymentMode)
//                                       values(@TicketNo,@BranchCode,@UserId,@TicketAmount,@AdvancePaid,@TourName,@TransactionType,@PaymentMode)";
//                                int k = DataLib.InsOrUpdateParam(strInsert, sqlClearBal);*/
//                                    #endregion


//                                    Onlinetransaction_Advance GPRClsObjAdv = new Onlinetransaction_Advance();
//                                    GPRClsObjAdv.RefNo = Convert.ToString(Session["BranchTicket"]);
//                                    GPRClsObjAdv.TransactionType = 32;
//                                    GPRClsObjAdv.Ticketamount = Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["TicketAmount"]);
//                                    GPRClsObjAdv.PaymentMode = "2";
//                                    GPRClsObjAdv.AdvancePaid = Convert.ToDecimal(Session["BalancePaid"]);
//                                    GPRClsObjAdv.UserId = "0";
//                                    GPRClsObjAdv.BranchCode = "EBK001";
//                                    GPRClsObjAdv.TourName = Convert.ToString(dtticketdetils.Tables[0].Rows[0]["TourName"]);
//                                    int k = pClsObj.fnInsert_GRPToursOnlinetransaction_Advance(GPRClsObjAdv);
//                                    decimal strTotalAdvance = Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["Credit"].ToString());
//                                    strTotalAdvance = strTotalAdvance + Convert.ToDecimal(Session["BalancePaid"].ToString());
//                                    decimal strTotalOnlinePaid = 0;
//                                    if (dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString() == "")
//                                    {
//                                        strTotalOnlinePaid = Convert.ToDecimal(Session["BalancePaid"].ToString());
//                                    }
//                                    else
//                                    {
//                                        strTotalOnlinePaid = (Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString()) + Convert.ToDecimal(Session["BalancePaid"].ToString()));
//                                    }
//                                    #region Optimize Code
//                                    /*SqlParameter[] sqlUpdateOnline = new SqlParameter[3];
//                                sqlUpdateOnline[0] = new SqlParameter("@credit", strTotalAdvance);
//                                sqlUpdateOnline[1] = new SqlParameter("@OnlineClear", strTotalOnlinePaid);
//                                sqlUpdateOnline[2] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate = "update onlinetransactiontable set credit=@credit,OnlineClear= @OnlineClear where refno=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate, sqlUpdateOnline);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdate_BranchGRPToursonlinetransactiontable(Convert.ToDecimal(strTotalAdvance),
//                                        Convert.ToString(Session["BranchTicket"]),
//                                           Convert.ToDecimal(strTotalOnlinePaid));

//                                    //UPDATE tICKET dETAILS
//                                    #region Optimize Code
//                                    /*SqlParameter[] sqlUpdateTICKET = new SqlParameter[2];
//                                sqlUpdateTICKET[0] = new SqlParameter("@credit", strTotalAdvance);

//                                sqlUpdateTICKET[1] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate1 = "update ticketdetails set advance=@credit where TicketNo=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate1, sqlUpdateTICKET);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdateTicketDetails(Convert.ToDecimal(strTotalAdvance), Convert.ToString(Session["BranchTicket"]));
//                                    //UPDATE onlinetoursbooking Table
//                                    #region Optimize Code
//                                    /*SqlParameter[] toursbooking = new SqlParameter[2];
//                                toursbooking[0] = new SqlParameter("@credit", strTotalAdvance);

//                                toursbooking[1] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate2 = "update onlinetoursbooking set advancepaid=@credit where ticketcode=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate2, toursbooking);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdateSPLOnlineToursBooking(Convert.ToDecimal(strTotalAdvance), Convert.ToString(Session["BranchTicket"]));
//                                    Response.Redirect("Tickets.aspx?orderid=" + dtticketdetils.Tables[0].Rows[0]["orderid"].ToString());

//                                }
//                            }
//                            else
//                            {
//                                if (dtticketdetils.Tables[0].Rows[0]["TransactionType"].ToString() == "9")
//                                {
//                                    #region Optimize Code
//                                    /* SqlParameter[] sqlClearBal = new SqlParameter[8];
//                                sqlClearBal[0] = new SqlParameter("@TicketNo", Session["BranchTicket"].ToString());
//                                sqlClearBal[1] = new SqlParameter("@BranchCode", "EBK0001");
//                                sqlClearBal[2] = new SqlParameter("@UserId", "0");
//                                sqlClearBal[3] = new SqlParameter("@TicketAmount", dtticketdetils.Tables[0].Rows[0]["TicketAmount"].ToString());
//                                sqlClearBal[4] = new SqlParameter("@AdvancePaid", Session["BalancePaid"].ToString());
//                                sqlClearBal[5] = new SqlParameter("@TourName", dtticketdetils.Tables[0].Rows[0]["TourName"].ToString());
//                                sqlClearBal[6] = new SqlParameter("@TransactionType", "9");
//                                sqlClearBal[7] = new SqlParameter("@PaymentMode", "2");

//                                string strInsert = @"insert into Onlinetransaction_Advance(refNo,BranchCode,UserId,TicketAmount,AdvancePaid,TourName,TransactionType,PaymentMode)
//                                       values(@TicketNo,@BranchCode,@UserId,@TicketAmount,@AdvancePaid,@TourName,@TransactionType,@PaymentMode)";
//                                int k = DataLib.InsOrUpdateParam(strInsert, sqlClearBal);*/
//                                    #endregion
//                                    Onlinetransaction_Advance GPRClsObjAdv = new Onlinetransaction_Advance();
//                                    GPRClsObjAdv.RefNo = Convert.ToString(Session["BranchTicket"]);
//                                    GPRClsObjAdv.TransactionType = 9;
//                                    GPRClsObjAdv.Ticketamount = Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["TicketAmount"]);
//                                    GPRClsObjAdv.PaymentMode = "2";
//                                    GPRClsObjAdv.AdvancePaid = Convert.ToDecimal(Session["BalancePaid"]);
//                                    GPRClsObjAdv.UserId = "0";
//                                    GPRClsObjAdv.BranchCode = "EBK001";
//                                    GPRClsObjAdv.TourName = Convert.ToString(dtticketdetils.Tables[0].Rows[0]["TourName"]);
//                                    int k = pClsObj.fnInsert_GRPToursOnlinetransaction_Advance(GPRClsObjAdv);

//                                    decimal strTotalAdvance = Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["Credit"].ToString());
//                                    strTotalAdvance = strTotalAdvance + Convert.ToDecimal(Session["BalancePaid"].ToString());

//                                    decimal strTotalOnlinePaid = 0;
//                                    if (dtticketdetils.Tables[0].Rows[0]["OnlineClear"] == null || dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString() == "")
//                                    {
//                                        strTotalOnlinePaid = Convert.ToDecimal(Session["BalancePaid"].ToString());
//                                    }
//                                    else
//                                    {
//                                        strTotalOnlinePaid = (Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString()) + Convert.ToDecimal(Session["BalancePaid"].ToString()));
//                                    }
//                                    #region Optimize Code
//                                    /*SqlParameter[] sqlUpdateOnline = new SqlParameter[3];
//                                sqlUpdateOnline[0] = new SqlParameter("@credit", strTotalAdvance);
//                                sqlUpdateOnline[1] = new SqlParameter("@OnlineClear", strTotalOnlinePaid);
//                                sqlUpdateOnline[2] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate = "update onlinetransactiontable set credit=@credit where refno=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate, sqlUpdateOnline);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdate_GRPToursonlinetransactiontable(Convert.ToDecimal(strTotalAdvance),
//                                        Convert.ToString(Session["BranchTicket"]));

//                                    //UPDATE tICKET dETAILS
//                                    #region Optimize Code
//                                    /*SqlParameter[] sqlUpdateTICKET = new SqlParameter[2];
//                                sqlUpdateTICKET[0] = new SqlParameter("@credit", strTotalAdvance);

//                                sqlUpdateTICKET[1] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate1 = "update ticketdetails set advance=@credit where Ticketno=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate1, sqlUpdateTICKET);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdateTicketDetails(Convert.ToDecimal(strTotalAdvance), Convert.ToString(Session["BranchTicket"]));
//                                    //UPDATE onlinetoursbooking Table
//                                    #region Optimize Code
//                                    /*SqlParameter[] toursbooking = new SqlParameter[2];
//                                toursbooking[0] = new SqlParameter("@credit", strTotalAdvance);

//                                toursbooking[1] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate2 = "update onlinetoursbooking set MinimumPay=@credit where ticketcode=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate2, toursbooking);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdateSPLOnlineToursBook(Convert.ToDecimal(strTotalAdvance), Convert.ToString(Session["BranchTicket"]));
//                                    Response.Redirect("Tickets.aspx?orderid=" + dtticketdetils.Tables[0].Rows[0]["orderid"].ToString());
//                                }

//                            }
//                        }

//                        else
//                        {
//                            Response.Redirect("PaymentError.aspx");
//                        }
//                    }

//                }
//                finally
//                {
//                    if (pClsObj != null)
//                    {
//                        pClsObj = null;
//                    }
//                }
//            }
//            else if ((Session["BranchCumSPL"] != null) && (Session["BranchCumSPL"].ToString() == "BranchCumSpecial"))
//            {
//                #region Optimize Code
//                /*SqlParameter[] paramst = new SqlParameter[1];
//                paramst[0] = new SqlParameter("@TicketNo", Session["BranchTicket"].ToString());
//                DataSet dtticketdetils = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "GetFromAdvanceTable_sp", paramst);*/
//                #endregion
//                ClsAdo pClsObj = null;
//                try
//                {
//                    pClsObj = new ClsAdo();
//                    DataTable ldtRecSet = pClsObj.fnGetFromAdvanceTable(Convert.ToString(Session["BranchTicket"]));
//                    DataSet dtticketdetils = new DataSet();
//                    if (ldtRecSet != null)
//                    {
//                        dtticketdetils.Tables.Add(ldtRecSet);
//                    }
//                    if (dtticketdetils.Tables[0].Rows.Count > 0)
//                    {
//                        #region Optimize Code
//                        /*SqlParameter[] param21 = new SqlParameter[9];
//                    param21[0] = new SqlParameter("@PaymentID", PaymentID);
//                    param21[1] = new SqlParameter("@amt", Session["BalancePaid"].ToString());
//                    param21[2] = new SqlParameter("@result", result);
//                    param21[3] = new SqlParameter("@ref1", ref1);
//                    param21[4] = new SqlParameter("@trackid", trackid);
//                    param21[5] = new SqlParameter("@tranid", tranid);
//                    param21[6] = new SqlParameter("@postdate", postdate);
//                    param21[7] = new SqlParameter("@errort", errort);
//                    param21[8] = new SqlParameter("@orderid", dtticketdetils.Tables[0].Rows[0]["orderid"].ToString());
//                    string strPayment = "update tbl_PaymentDetails set IsAmex=1, IsPaid='Y', PaymentId=@PaymentID, Amount=@amt,result=@result,ref=@ref1,trackid=@trackid,transId=@tranid,postDate=@postdate,error=@errort where orderID=@orderid";
//                    int val = DataLib.InsOrUpdateParam(strPayment, param21);*/
//                        #endregion
//                        tbl_PaymentDetail pclsTbl = new tbl_PaymentDetail();
//                        pclsTbl.isHDFC = false;
//                        pclsTbl.IsPaid = 'Y';
//                        pclsTbl.PaymentId = PaymentID;
//                        pclsTbl.Amount = Convert.ToDecimal(Session["BalancePaid"]);
//                        pclsTbl.result = result;
//                        pclsTbl.@ref = ref1;
//                        pclsTbl.trackid = trackid;
//                        pclsTbl.transId = tranid;
//                        pclsTbl.postDate = postdate;
//                        pclsTbl.error = errort;
//                        pclsTbl.orderId = Convert.ToString(dtticketdetils.Tables[0].Rows[0]["orderid"]);

//                        int val = pClsObj.fnUpdatePaymentDetails(pclsTbl);
//                        if (result == "CAPTURED")
//                        {
//                            if (dtticketdetils.Tables[0].Rows[0]["BranchCode"].ToString() != "EBK001")
//                            {
//                                if (dtticketdetils.Tables[0].Rows[0]["TransactionType"].ToString() == "13")
//                                {
//                                    #region Optimize Code
//                                    /*SqlParameter[] sqlClearBal = new SqlParameter[8];
//                                sqlClearBal[0] = new SqlParameter("@TicketNo", Session["BranchTicket"].ToString());
//                                sqlClearBal[1] = new SqlParameter("@BranchCode", "EBK001");
//                                sqlClearBal[2] = new SqlParameter("@UserId", "0");
//                                sqlClearBal[3] = new SqlParameter("@TicketAmount", dtticketdetils.Tables[0].Rows[0]["TicketAmount"].ToString());
//                                sqlClearBal[4] = new SqlParameter("@AdvancePaid", Session["BalancePaid"].ToString());
//                                sqlClearBal[5] = new SqlParameter("@TourName", dtticketdetils.Tables[0].Rows[0]["TourName"].ToString());
//                                sqlClearBal[6] = new SqlParameter("@TransactionType", "33");
//                                sqlClearBal[7] = new SqlParameter("@PaymentMode", "Amex Payment");

//                                string strInsert = @"insert into Onlinetransaction_Advance(refNo,BranchCode,UserId,TicketAmount,AdvancePaid,TourName,TransactionType,PaymentMode)
//                                       values(@TicketNo,@BranchCode,@UserId,@TicketAmount,@AdvancePaid,@TourName,@TransactionType,@PaymentMode)";
//                                int k = DataLib.InsOrUpdateParam(strInsert, sqlClearBal);*/
//                                    #endregion
//                                    Onlinetransaction_Advance GPRClsObjAdv = new Onlinetransaction_Advance();
//                                    GPRClsObjAdv.RefNo = Convert.ToString(Session["BranchTicket"]);
//                                    GPRClsObjAdv.TransactionType = 33;
//                                    GPRClsObjAdv.Ticketamount = Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["TicketAmount"]);
//                                    GPRClsObjAdv.PaymentMode = "Net Banking";
//                                    GPRClsObjAdv.AdvancePaid = Convert.ToDecimal(Session["BalancePaid"]);
//                                    GPRClsObjAdv.UserId = "0";
//                                    GPRClsObjAdv.BranchCode = "EBK001";
//                                    GPRClsObjAdv.TourName = Convert.ToString(dtticketdetils.Tables[0].Rows[0]["TourName"]);
//                                    int k = pClsObj.fnInsert_GRPToursOnlinetransaction_Advance(GPRClsObjAdv);


//                                    decimal strTotalAdvance = Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["Credit"].ToString());
//                                    strTotalAdvance = strTotalAdvance + Convert.ToDecimal(Session["BalancePaid"].ToString());

//                                    decimal strTotalOnlinePaid = 0;
//                                    if (dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString() == "")
//                                    {
//                                        strTotalOnlinePaid = Convert.ToDecimal(Session["BalancePaid"].ToString());
//                                    }
//                                    else
//                                    {
//                                        strTotalOnlinePaid = (Convert.ToDecimal(dtticketdetils.Tables[0].Rows[0]["OnlineClear"].ToString()) + Convert.ToDecimal(Session["BalancePaid"].ToString()));
//                                    }
//                                    #region Optimize Code
//                                    /* SqlParameter[] sqlUpdateOnline = new SqlParameter[3];
//                                sqlUpdateOnline[0] = new SqlParameter("@credit", strTotalAdvance);
//                                sqlUpdateOnline[1] = new SqlParameter("@OnlineClear", strTotalOnlinePaid);
//                                sqlUpdateOnline[2] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate = "update onlinetransactiontable set credit=@credit,OnlineClear= @OnlineClear where refno=@refno";
//                                DataLib.InsOrUpdateParam(strUpdate, sqlUpdateOnline);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdate_BranchGRPToursonlinetransactiontable(Convert.ToDecimal(strTotalAdvance),
//                                       Convert.ToString(Session["BranchTicket"]),
//                                          Convert.ToDecimal(strTotalOnlinePaid));
//                                    //UPDATE Spl Tour Enquiry table dETAILS
//                                    #region Optimize Code
//                                    /* SqlParameter[] sqlUpdateTICKET = new SqlParameter[2];
//                                sqlUpdateTICKET[0] = new SqlParameter("@credit", strTotalAdvance);

//                                sqlUpdateTICKET[1] = new SqlParameter("@refno", Session["BranchTicket"].ToString());
//                                string strUpdate1 = "update spl_tourenquiry set AdvancePaid=@credit where Ticketno=@refno";
//                                DataLib.WithOPInsOrUpdateParam(strUpdate1, sqlUpdateTICKET);*/


//                                    /*SqlParameter[] sqlGetId = new SqlParameter[1];

//                                    sqlGetId[0] = new SqlParameter("@refno", Session["BranchTicket"].ToString());

//                                    string strOp = "select [id] from spl_tourenquiry where Ticketno=@refno";
//                                    string strID = DataLib.GetStringDataWithParam(DataLib.Connection.ConnectionString, strOp, sqlGetId);*/
//                                    #endregion
//                                    val = pClsObj.fnUpdatespltourenquiry(Convert.ToDecimal(strTotalAdvance), Convert.ToString(Session["BranchTicket"]));
//                                    int strID = pClsObj.fnGetIDspltourenquiry(Convert.ToString(Session["BranchTicket"]));
//                                    Response.Redirect("SpltourTicket.aspx?Id=" + strID + "");
//                                }
//                            }
//                        }
//                        else
//                        {
//                            Response.Redirect("PaymentError.aspx");
//                        }
//                    }
//                }
//                finally
//                {

//                    if (pClsObj != null)
//                    {
//                        pClsObj = null;
//                    }
//                }
//            }


//        }
//        private void direct()
//        {
//            // Display the Response Data
//            pnlResponse.Visible = false;
//            pnlReceiptError.Visible = false;
//            if ((Request.QueryString["frompromo"] == "Y") && (Request.QueryString["Amt"] != "0"))
//            {
//                result = "CAPTURED";
//                PaymentID = "0";
//                amt = Request.QueryString["Amt"];// "000";
//                amt = amt.Substring(0, (amt.Length - 2));
//                auth = "";
//                ref1 = "";
//                trackid = "";
//                tranid = "";
//                postdate = "";
//                errort = "";
//                OrderId = Convert.ToString(Session["orderid"]);
//                trap_PaymentDetails();
//            }
//            else
//            {
//                result = "Failed";
//                PaymentID = "";
//                amt = "0";
//                auth = "";
//                ref1 = "";
//                trackid = "";
//                tranid = "";
//                postdate = "";
//                errort = "";
//                pnlReceiptError.Visible = true;
//                lblReceiptErrorMessage.Text = "Error Occured, please try again";
//                pnlReceiptError.Visible = true;
//            }
//        }
//        private void fixedtempbooking()
//        {
//            string orderid = OrderId;
//            ClsAdo pClsObj = null;
//            try
//            {
//                pClsObj = new ClsAdo();
//                int val = pClsObj.fnInsertFinalFixBookingInfoEndUser_New(orderid, Convert.ToString(Session["promo"]),
//                   Convert.ToDecimal(Session["PromoDisValue"]), "AMEX Payment", "CustomerBusTicket");
//            }
//            finally
//            {
//                if (pClsObj != null)
//                {
//                    pClsObj = null;
//                }
//            }
//        }
//        private void fixedtempbooking_Old()
//        {

//            ClsAdo pClsObj = null;
//            try
//            {
//                pClsObj = new ClsAdo();
//                string dm = dummypassengers();
//                if (dm == "Y")
//                {
//                    decimal strMinAmount = 0.0m;
//                    DateTime strJdate;
//                    string[] str1;
//                    int rowid, Busno = 0, TicketBusnoRowid, TicketBusno1Rowid, TicketNameRowid;
//                    string TicketCode, BranchShortCode, ContactPerson, sqladd, updatestring, seatstring, orderid;
//                    string[] tblseatfld = new string[51];
//                    string busserialno;
//                    Session["blockStatus"] = null;
//                    Session["blockStr"] = null;
//                    orderid = OrderId;
//                    string TicketNo = "";
//                    DateTime Bookingdate;
//                    decimal Amount, Discount = 0.0M, advancePaid = 0.0m;
//                    decimal adultfare, adultstwinfare, adultstriplefare, singleadultfare, dormitoryfare;
//                    decimal childfare, childwithoutbedfare, TotalAmount;
//                    decimal STaxValue, cctax, calcstaxvalue, calccctaxvalue;
//                    string GroupLeader = "", Address = "", City = "", State = "", PickUppoint = "";
//                    string Country = "", Zip = "", TelNo = "", Email = "";
//                    string CurrentUserBranchCode = "EBK0001", TourName, PayMode = "AMEX Payment";
//                    int TourNo, NoOfSeats, TourSerial;
//                    DateTime ReportTime = DateTime.Today, DepartTime = DateTime.Today;
//                    Int16 NAdult, Nadultstwin, Nadultstriple, Nsingleadult, NChild, Nchildwithoutbed, Ndormitory;
//                    string RelTelNo = "", Remarks = "", mobile = "";
//                    string promocode = "", promovalue = "0", utilpromo = "0";

//                    #region Optimize Code
//                    /* DataTable dtTemp = DataLib.GetDataTable(DataLib.Connection.ConnectionString, 
//                * "select OnlineCustomer.rowid,FirstName,LastName,OnlineCustomer.email,Addr1,addr2,City,state,Country,zipcode,PhoneNo,RelativePhoneNo,
//                * AlternativeNo,Mobile,Fax,OnlineCustomer.DOB as dateofbirth,company,occupation,remark,maritalstatus,DOA 
//                * from OnlineCustomer inner join OnlineCustByOrder on OnlineCustomer.rowid=OnlineCustByOrder.OcustRowid 
//                * where OnlineCustByOrder.orderid='" + orderid + "'");*/
//                    #endregion
//                    DataSet ldsRecSet = pClsObj.fnGetFixedTempBookingt(orderid);
//                    DataTable dtTemp = new DataTable();
//                    if (ldsRecSet != null)
//                        dtTemp = ldsRecSet.Tables[0];
//                    if (dtTemp.Rows.Count > 0)
//                    {
//                        if (dtTemp.Rows[0]["LastName"] != null)
//                            GroupLeader = dtTemp.Rows[0]["FirstName"].ToString() + " " + dtTemp.Rows[0]["LastName"].ToString();
//                        else
//                            GroupLeader = dtTemp.Rows[0]["FirstName"].ToString();
//                        Address = dtTemp.Rows[0]["Addr1"].ToString();
//                        if (dtTemp.Rows[0]["City"] != null)
//                            City = dtTemp.Rows[0]["City"].ToString();
//                        if (dtTemp.Rows[0]["state"] != null)
//                            State = dtTemp.Rows[0]["state"].ToString();
//                        if (dtTemp.Rows[0]["Country"] != null)
//                            Country = dtTemp.Rows[0]["Country"].ToString();
//                        if (dtTemp.Rows[0]["zipcode"] != null)
//                            Zip = dtTemp.Rows[0]["zipcode"].ToString();
//                        if (dtTemp.Rows[0]["PhoneNo"] != null)
//                            TelNo = dtTemp.Rows[0]["PhoneNo"].ToString();
//                        if (dtTemp.Rows[0]["RelativePhoneNo"] != null)
//                            RelTelNo = dtTemp.Rows[0]["RelativePhoneNo"].ToString();
//                        Email = dtTemp.Rows[0]["email"].ToString();
//                        if (dtTemp.Rows[0]["remark"] != null)
//                            Remarks = dtTemp.Rows[0]["remark"].ToString();
//                        if (dtTemp.Rows[0]["Mobile"] != null)
//                            mobile = dtTemp.Rows[0]["Mobile"].ToString();
//                    }


//                    #region Optimize Code
//                    /* DataSet sqlds = new DataSet();
//                DataTable dt1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select OnlineToursBooking.rowid,tourid,doj,dob,BusEnvType,NoofAdults,NoofChild,TourName,Amount,Tax,CalcTaxvalue,TotalAmount,SeatNo,TourSerial,BusSerialNo,PickUppointid,adultfare,childfare,NoofAdultsTwin,NoofAdultsTriple,ChildWithoutbed,SingleAdult,dormitory,adultstwinfare,adultstriplefare,childwithoutbedfare,singleadultfare,dormitoryfare,CreditCardFee,CalcCreditCardFee,Discount,advancePaid,MinimumPay,OnlineToursBooking.DOJ from OnlineToursBooking inner join OnlineCustByOrder on OnlineToursBooking.orderid=OnlineCustByOrder.orderid  where OnlineToursBooking.orderid='" + orderid + "' order by OnlineToursBooking.totalamount desc");
//                dt1.TableName = "OTB";
//                sqlds.Tables.Add(dt1);
//                dt1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select name,age,sex from OnlinePassenger inner join OnlineCustByOrder on OnlinePassenger.orderid=OnlineCustByOrder.orderid where OnlinePassenger.orderid='" + orderid + "'");
//                dt1.TableName = "TPSD";
//                sqlds.Tables.Add(dt1);*/
//                    #endregion
//                    int it;
//                    int totaltickets = ldsRecSet.Tables[1].Rows.Count;
//                    for (it = 0; it < ldsRecSet.Tables[1].Rows.Count; it++)
//                    {
//                        Bookingdate = Convert.ToDateTime(ldsRecSet.Tables[1].Rows[it]["dob"].ToString());
//                        NoOfSeats = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofChild"].ToString()) + Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofAdults"].ToString()) + Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofAdultsTwin"].ToString()) + Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofAdultsTriple"].ToString()) + Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["ChildWithoutbed"].ToString()) + Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["SingleAdult"].ToString()) + Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["dormitory"].ToString());
//                        Amount = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["Amount"].ToString());
//                        adultfare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["adultfare"].ToString());
//                        adultstwinfare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["adultstwinfare"].ToString());
//                        adultstriplefare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["adultstriplefare"].ToString());
//                        singleadultfare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["singleadultfare"].ToString());
//                        childfare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["childfare"].ToString());
//                        childwithoutbedfare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["childwithoutbedfare"].ToString());
//                        dormitoryfare = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["dormitoryfare"].ToString());

//                        TotalAmount = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["TotalAmount"].ToString());
//                        if ((Convert.ToString(ldsRecSet.Tables[1].Rows[it]["Discount"]) != null) && (Convert.ToString(ldsRecSet.Tables[1].Rows[it]["Discount"]) != ""))
//                        {
//                            Discount = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["Discount"].ToString());
//                        }
//                        else
//                        {
//                            Discount = 0;
//                        }
//                        if ((Convert.ToString(ldsRecSet.Tables[1].Rows[it]["AdvancePaid"]) != null) && (Convert.ToString(ldsRecSet.Tables[1].Rows[it]["AdvancePaid"]) != ""))
//                        {
//                            advancePaid = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["AdvancePaid"].ToString());
//                        }
//                        else
//                        {
//                            advancePaid = 0;
//                        }

//                        STaxValue = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["Tax"].ToString());
//                        calcstaxvalue = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["CalcTaxvalue"].ToString());
//                        cctax = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["CreditCardFee"].ToString());
//                        calccctaxvalue = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["CalcCreditCardFee"].ToString());

//                        strMinAmount = Convert.ToDecimal(ldsRecSet.Tables[1].Rows[it]["MinimumPay"].ToString());

//                        // For Reduce the amount of promocode option value from the existing amount
//                        if (Convert.ToString(Session["promo"]) != "")
//                        {
//                            promocode = Convert.ToString(Session["promo"]);
//                            if (Convert.ToString(Session["promo"]) != "")
//                            {
//                                //promovalue = DataLib.Promocodevalue(Amount, Convert.ToString(Session["promo"]));
//                                promovalue = Session["PromoDisValue"].ToString();
//                            }
//                            else
//                            {
//                                promovalue = "0";
//                            }
//                            if (totaltickets == 1)
//                            {
//                                if (TotalAmount >= Convert.ToDecimal(Convert.ToInt32(promovalue)))
//                                    TotalAmount = Convert.ToDecimal(Convert.ToInt32(TotalAmount));
//                                else
//                                    TotalAmount = 0;
//                                if (strMinAmount >= Convert.ToDecimal(Convert.ToInt32(promovalue)))
//                                {
//                                    decimal min = Convert.ToDecimal(promovalue);
//                                    strMinAmount = Convert.ToDecimal(Convert.ToInt32(strMinAmount));
//                                    promovalue = "0";
//                                    utilpromo = Convert.ToString(min);
//                                }
//                                else
//                                {
//                                    decimal min = strMinAmount;
//                                    strMinAmount = 0;
//                                    promovalue = Convert.ToString(Convert.ToDecimal(promovalue) - min);
//                                    utilpromo = Convert.ToString(min);
//                                }
//                            }
//                            else
//                            {
//                                if (TotalAmount >= Convert.ToDecimal(Convert.ToInt32(promovalue)))
//                                    TotalAmount = Convert.ToDecimal(Convert.ToInt32(TotalAmount));
//                                else
//                                    TotalAmount = 0;
//                                if (strMinAmount >= Convert.ToDecimal(Convert.ToInt32(promovalue)))
//                                {
//                                    decimal min = Convert.ToDecimal(promovalue);
//                                    strMinAmount = Convert.ToDecimal(Convert.ToInt32(strMinAmount));
//                                    promovalue = "0";
//                                    utilpromo = Convert.ToString(min);
//                                }
//                                else
//                                {
//                                    decimal min = strMinAmount;
//                                    strMinAmount = 0;
//                                    promovalue = Convert.ToString(Convert.ToDecimal(promovalue) - min);
//                                    utilpromo = Convert.ToString(min);
//                                }
//                            }
//                        }
//                        #region Optimize Code

//                        /*DataTable dt11 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select PickUpPlace,ReportTime,DepartTime 
//                     * from pickupmaster where Rowid=" + ldsRecSet.Tables[1].Rows[it]["PickUppointid"].ToString());*/
//                        #endregion
//                        strJdate = Convert.ToDateTime(ldsRecSet.Tables[1].Rows[it]["DOJ"].ToString());
//                        DataTable dt11 = pClsObj.fnGetPickupmaster(Convert.ToString(ldsRecSet.Tables[1].Rows[it]["PickUppointid"]));
//                        if (dt11.Rows.Count > 0)
//                        {
//                            PickUppoint = dt11.Rows[0]["PickUpPlace"].ToString();
//                            ReportTime = Convert.ToDateTime(dt11.Rows[0]["ReportTime"].ToString());
//                            DepartTime = Convert.ToDateTime(dt11.Rows[0]["DepartTime"].ToString());
//                            Response.Write(PickUppoint);
//                        }
//                        NAdult = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofAdults"].ToString());
//                        Nadultstwin = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofAdultsTwin"].ToString());
//                        Nadultstriple = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofAdultsTriple"].ToString());
//                        Nsingleadult = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["SingleAdult"].ToString());
//                        NChild = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["NoofChild"].ToString());
//                        Nchildwithoutbed = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["ChildWithoutbed"].ToString());
//                        Ndormitory = Convert.ToInt16(ldsRecSet.Tables[1].Rows[it]["dormitory"].ToString());

//                        TourName = ldsRecSet.Tables[1].Rows[it]["TourName"].ToString();
//                        busserialno = Convert.ToString(ldsRecSet.Tables[1].Rows[it]["BusSerialNo"].ToString());
//                        TourNo = Convert.ToInt32(ldsRecSet.Tables[1].Rows[it]["tourid"].ToString());
//                        TourSerial = Convert.ToInt32(ldsRecSet.Tables[1].Rows[it]["TourSerial"].ToString());
//                        seatstring = Convert.ToString(ldsRecSet.Tables[1].Rows[it]["SeatNo"].ToString());

//                        string str11 = "";
//                        //RetrieveType = "TicketNumber";
//                        int tNo = 0;
//                        TicketCode = pvBAL.GenerateNewFixedTourTkt(orderid);
//                        str11 = Convert.ToInt32(TicketCode.Replace("EBK", string.Empty)).ToString();
//                        tNo = Convert.ToInt32(str11);
//                        TicketNo = str11;
//                        rowid = tNo;
//                        #region Optimize Code
//                        /*str11 = rowidretrieval("TicketNumber");
//                    tNo = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str11));
//                    rowid = tNo;

//                    tNo = tNo + 1;
//                    TicketNo = Convert.ToString(tNo);
//                    rowid = rowid + 1;

//                    //Generating the Ticket Code which is store in the Ticket Details table 
//                    BranchShortCode = "EBK";
//                    string ticketstring;
//                    int totalchars;
//                    ticketstring = TicketNo.ToString();
//                    totalchars = ticketstring.Length;
//                    TicketCode = "";
//                    for (i = 0; i <= (5 - totalchars); i++)
//                    {
//                        TicketCode = TicketCode + 0;
//                    }
//                    TicketCode = BranchShortCode + TicketCode + ticketstring;
//                    ticketstring = "";  */
//                        #endregion
//                        string BlockedString, BlockedString1, BlockedString2;
//                        ContactPerson = GroupLeader;
//                        BlockedString1 = System.DateTime.Today.Date.ToShortDateString();
//                        BlockedString2 = System.DateTime.Today.Date.ToShortDateString();
//                        BlockedString = TicketCode + "\n" + "Mr/Ms." + ContactPerson.Trim().Replace("'", "''") + "\n" + BlockedString1 + " " + BlockedString2;
//                        str1 = seatstring.ToString().Split(',');
//                        string[] ser = busserialno.Split(',');
//                        string seat = "";
//                        //=============start================

//                        if (ser.Length > 1)
//                        {
//                            string sb = "";
//                            for (int z = 0; z < ser.Length; z++)
//                            {
//                                //sb = sb + "," + str1[z].Substring(1, 1);
//                                sb = sb + "," + busnoretrival(ser[z]);
//                            }
//                            sb = sb.TrimStart(',').TrimEnd(',');
//                            string[] busn = sb.Split(',');
//                            for (int k = 0; k < ser.Length; k++)
//                            {
//                                seat = "";
//                                for (int i = 0; i < str1.Length; i++)
//                                {
//                                    if (str1[i].Length > 3)
//                                    {
//                                        if (str1[i].Substring(1, 1) == Convert.ToString(busn[k]))
//                                        {
//                                            tblseatfld[i] = str1[i].Substring(2, 2);
//                                            if (seat == "")
//                                            {
//                                                seat = str1[i].Substring(2, 2);
//                                            }
//                                            else
//                                            {
//                                                seat = seat + "," + str1[i].Substring(2, 2);
//                                            }
//                                        }
//                                    }
//                                    else
//                                    {
//                                        if (str1[i].Substring(1, 1) == Convert.ToString(busn[k]))
//                                        {
//                                            tblseatfld[i] = str1[i].Substring(2, 1);
//                                            if (seat == "")
//                                            {
//                                                seat = str1[i].Substring(2, 1);
//                                            }
//                                            else
//                                            {
//                                                seat = seat + "," + str1[i].Substring(2, 1);
//                                            }
//                                        }
//                                    }
//                                }
//                                seat = seat.Replace(",,", ",").TrimStart(',').TrimEnd(',');
//                                string[] sd = seat.Split(',');
//                                string tbnseat = "";
//                                for (i = 0; i < sd.Length; i++)
//                                {
//                                    if (Convert.ToString(sd[i]) != "")
//                                    {
//                                        #region Optimize Code
//                                        /*string strName = "select [name] from onlinepassenger where orderid='" + orderid + "'";
//                                    DataTable dt = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strName);
//                                    if (dt.Rows.Count > 0)
//                                    {
//                                        string CPerson = dt.Rows[i]["name"].ToString();
//                                        BlockedString1 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                        BlockedString2 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                        BlockedString = TicketCode + "\n" + GroupLeader.Trim().Replace("'", "''") + "\n" + BlockedString1 + " " + BlockedString2;

//                                        updatestring = "UPDATE SeatArrangement set s" + sd[i] + " = '" + BlockedString + " 'where TourSerial = " + ser[k] + " ";
//                                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, updatestring, false);


//                                    }*/
//                                        #endregion
//                                        BlockedString1 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                        BlockedString2 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                        BlockedString = TicketCode + "\n" + GroupLeader.Trim().Replace("'", "''") + "\n" + BlockedString1 + " " + BlockedString2;
//                                        string pSeats = "s" + sd[i];
//                                        int val = pClsObj.fnBlockUnBlockSeats_sp(Convert.ToString(ser[k]), pSeats, BlockedString, true);
//                                        if (val > 0)
//                                            if (tbnseat == "")
//                                            {
//                                                tbnseat = "s" + sd[i];
//                                            }
//                                            else
//                                            {
//                                                tbnseat = tbnseat + "," + "s" + sd[i];
//                                            }
//                                    }
//                                }
//                                #region Optimize Code
//                                /*int seatArrangeRowid = 0;

//                            string strQry = "Select RowId,BusNo from SeatArrangement where TourSerial = " + ser[k] + "";
//                            DataTable dtReturn1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strQry);*/
//                                #endregion
//                                int seatArrangeRowid = 0;
//                                DataTable dtReturn1 = pClsObj.fnGetSeatArrangement(Convert.ToInt32(ser[k]));
//                                if (dtReturn1.Rows.Count > 0)
//                                {
//                                    Busno = Convert.ToInt32(dtReturn1.Rows[0]["BusNo"]);
//                                    seatArrangeRowid = Convert.ToInt32(dtReturn1.Rows[0]["RowId"]);
//                                }
//                                #region Optimize Code
//                                /*string RetrieveType1;
//                            RetrieveType1 = "TicketBusNo";
//                            str11 = rowidretrieval(RetrieveType1);
//                            TicketBusnoRowid = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str11));
//                            TicketBusno1Rowid = TicketBusnoRowid;

//                            TicketBusnoRowid = TicketBusnoRowid + 1;
//                            TicketBusno1Rowid = TicketBusno1Rowid + 1;

//                            sqladd = "INSERT INTO TicketBusno(RowId,TicketNo,BusSerialNo,BusNo,SeatNumbers)values(" + TicketBusnoRowid + ",'" + TicketCode + "','" + ser[k] + "'," + Busno + ",'" + tbnseat + "')";
//                            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);
//                            TicketBusnoRowid = TicketBusnoRowid + 1;


//                            TicketBusnoRowid = TicketBusnoRowid - 1;
//                            sqladd = "update newkeytable_online set RespectiveRowId=" + TicketBusnoRowid + " where KeyType = 'TicketBusNo'";
//                            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);*/
//                                #endregion
//                                TicketBusnoRowid = pClsObj.fnRowIDRetrieval("TicketBusNo", "RowId", "TicketBusno");
//                                int valc = pClsObj.fnInsertTicketBusno(TicketBusnoRowid, TicketCode, ser[k], Convert.ToString(Busno), tbnseat);

//                            }
//                        }
//                        else
//                        {
//                            seat = seatstring.Replace(",,", ",").TrimStart(',').TrimEnd(',');
//                            string[] sd = seat.Split(',');
//                            string tbnseat = "";
//                            for (i = 0; i < sd.Length; i++)
//                            {
//                                if (Convert.ToString(sd[i]) != "")
//                                {
//                                    #region Optimize Code
//                                    /*string strName = "select [name] from onlinepassenger where orderid='" + orderid + "'";
//                                DataTable dt = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strName);
//                                if (dt.Rows.Count > 0)
//                                {
//                                    string CPerson = dt.Rows[i]["name"].ToString();
//                                    BlockedString1 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                    BlockedString2 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                    BlockedString = TicketCode + "\n" + GroupLeader.Trim().Replace("'", "''") + "\n" + BlockedString1 + " " + BlockedString2;
//                                    updatestring = "UPDATE SeatArrangement set " + sd[i] + " = '" + BlockedString + " 'where TourSerial = " + ser[0] + " ";
//                                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, updatestring, false);

//                                }*/
//                                    #endregion
//                                    BlockedString1 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                    BlockedString2 = Convert.ToString(System.DateTime.Today.Date.ToShortDateString());
//                                    BlockedString = TicketCode + "\n" + GroupLeader.Trim().Replace("'", "''") + "\n" + BlockedString1 + " " + BlockedString2;
//                                    int val = pClsObj.fnBlockUnBlockSeats_sp(Convert.ToString(ser[0]), sd[i], BlockedString, true);
//                                    if (val > 0)
//                                        if (tbnseat == "")
//                                        {
//                                            tbnseat = sd[i];
//                                        }
//                                        else
//                                        {
//                                            tbnseat = tbnseat + "," + sd[i];
//                                        }
//                                }
//                            }
//                            int seatArrangeRowid = 0;
//                            #region Optimize Code
//                            /*sqladd = "Select RowId,BusNo from SeatArrangement where TourSerial = " + ser[0] + "";
//                        DataTable dtb = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sqladd);*/
//                            #endregion
//                            DataTable dtb = pClsObj.fnGetSeatArrangement(Convert.ToInt32(ser[0]));
//                            if (dtb.Rows.Count > 0)
//                            {
//                                Busno = Convert.ToInt32(dtb.Rows[0]["BusNo"]);
//                                seatArrangeRowid = Convert.ToInt32(dtb.Rows[0]["RowId"]);
//                            }
//                            #region Optimize Code
//                            /* string RetrieveType1;
//                        RetrieveType1 = "TicketBusNo";

//                        str11 = rowidretrieval(RetrieveType1);
//                        TicketBusnoRowid = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str11));
//                        TicketBusno1Rowid = TicketBusnoRowid;

//                        TicketBusnoRowid = TicketBusnoRowid + 1;
//                        TicketBusno1Rowid = TicketBusno1Rowid + 1;

//                        sqladd = "INSERT INTO TicketBusno(RowId,TicketNo,BusSerialNo,BusNo,SeatNumbers)values(" + TicketBusnoRowid + ",'" + TicketCode + "','" + ser[0] + "'," + Busno + ",'" + tbnseat + "')";
//                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);
//                        TicketBusnoRowid = TicketBusnoRowid + 1;
//                        TicketBusnoRowid = TicketBusnoRowid - 1;
//                        sqladd = "update newkeytable_online set RespectiveRowId=" + TicketBusnoRowid + " where KeyType = 'TicketBusNo'";
//                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);*/
//                            #endregion
//                            TicketBusnoRowid = pClsObj.fnRowIDRetrieval("TicketBusNo", "RowId", "TicketBusno");
//                            int valCk = pClsObj.fnInsertTicketBusno(TicketBusnoRowid, TicketCode, ser[0], Convert.ToString(Busno), tbnseat);
//                        }
//                        //================end====================Session["LocalBranch"]
//                        #region Optimize Code
//                        /*string sqlstr = "INSERT INTO TicketDetails(RowId,TicketNo,GroupLeader,AgentId, Bookingdate,NoOfSeats,Amount,Advance,Discount,AgtComm,PickupPoint,ReportTime,DepartTime,NAdult,NChild,Address,City,State,Country,Zip,TelNo,Email,Cancelled,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode,Remarks,RelTelNo,adultfare,childfare,ssupplement,TotalAmount,STaxValue,noofadultstwin,noofadultstriple,childwithoutbed,singleadult,adultstwinfare,adultstriplefare,childwithoutbedfare,singleadultfare,creditcardfee,dormitory,dormitoryfare,PromoCodeDiscount) values (" + rowid + ",'" + TicketCode + "','" + ContactPerson.Replace("'", "''").Trim() + "','0',getdate()," + NoOfSeats + "," + decimal.Round(Amount, 0) + "," + decimal.Round(advancePaid, 0) + "," + decimal.Round(Discount, 0) + ",'0','" + PickUppoint.Replace("'", "''") + "','" + ReportTime + "','" + DepartTime + "'," + NAdult + "," + NChild + ",'" + Address.Replace("'", "''") + "','" + City.Replace("'", "''") + "','" + State + "','" + Country + "','" + Zip + "','" + mobile + "','" + Email + "','N','Online_User','EBK0001','','','" + Remarks.Replace("'", "''") + "','" + RelTelNo + "'," + adultfare + "," + childfare + ",0," + decimal.Round(TotalAmount, 0) + "," + STaxValue + "," + Nadultstwin + "," + Nadultstriple + "," + Nchildwithoutbed + "," + Nsingleadult + "," + adultstwinfare + "," + adultstriplefare + "," + childwithoutbedfare + "," + singleadultfare + "," + cctax + "," + Ndormitory + "," + dormitoryfare + "," + Convert.ToInt32(utilpromo) + ")";
//                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqlstr, false);

//                    sqlstr = "Update OnlineToursBooking set TicketCode='" + TicketCode + "' where orderid='" + orderid + "' and tourid='" + TourNo + "' and rowid=" + Convert.ToInt32(ldsRecSet.Tables[1].Rows[it]["rowid"].ToString()) + "";
//                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqlstr, false);

//                    string strid = "select transactiontypeid from transactiontypemaster where transactionname='CustomerBusTicket'";
//                    string transtype = DataLib.GetStringData(DataLib.Connection.ConnectionString, strid);
//                    decimal total = Convert.ToDecimal(Amount) + Convert.ToDecimal(calcstaxvalue) + Convert.ToDecimal(calccctaxvalue);
//                    sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,Commission,PaymentMode,Number,BankName,Debit,Credit,Remarks,UserName,BranchCode,ImpersonatingUserName,ImpersonatingBranchCode,Cashier,TransState,Status,ServiceTax,TourName,ccChargeAmt,PromoCode,PromoCodeValue)values(0,'" + TicketCode + "'," + Convert.ToInt16(transtype) + ",0,0,0," + decimal.Round(Amount, 0) + ",0,'" + PayMode + "','','',0," + strMinAmount + ",'','OnLineUser','" + CurrentUserBranchCode + "','','','','P','S'," + decimal.Round(calcstaxvalue, 0) + ",'" + TourName + "','" + decimal.Round(calccctaxvalue) + "','" + promocode + "','" + Convert.ToInt32(utilpromo) + "')";

//                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqladd, false);
//                    // Codind for insertion in Advance table to Implement Part Payment System--------
//                    string AdvPayMode = "2";

//                    SqlParameter[] paramAdvance = new SqlParameter[8];
//                    paramAdvance[0] = new SqlParameter("@TicketCode", TicketCode);
//                    paramAdvance[1] = new SqlParameter("@transtype", transtype);
//                    paramAdvance[2] = new SqlParameter("@Amount", total);
//                    paramAdvance[3] = new SqlParameter("@PayMode", AdvPayMode);
//                    paramAdvance[4] = new SqlParameter("@total", strMinAmount);
//                    paramAdvance[5] = new SqlParameter("@TourName", TourName);
//                    paramAdvance[6] = new SqlParameter("@NoOfSeats", NoOfSeats);
//                    paramAdvance[7] = new SqlParameter("@DOJ", strJdate);

//                    sqladd = "Insert into OnlineTransaction_Advance(RefNo,Branchcode,UserId,TransactionType,TicketAmount,PaymentMode,AdvancePaid,TourName,noofPax,doj)values(@TicketCode,'EBK0001','0',@transtype,@Amount,@PayMode,@total,@TourName,@NoOfSeats,@DOJ)";


//                    int vv = DataLib.InsOrUpdateParam(sqladd, paramAdvance);*/
//                        #endregion

//                        TicketDetail objTbl = new TicketDetail();
//                        objTbl.RowId = rowid;
//                        objTbl.TicketNo = TicketCode;
//                        objTbl.GroupLeader = ContactPerson.Replace("'", "''").Trim();
//                        objTbl.AgentId = 0;
//                        objTbl.Bookingdate = DateTime.Now;
//                        objTbl.NoOfSeats = Convert.ToByte(NoOfSeats);
//                        objTbl.Amount = decimal.Round(Amount, 0);
//                        objTbl.Advance = decimal.Round(advancePaid, 0);
//                        objTbl.Discount = decimal.Round(Discount, 0);
//                        objTbl.AgtComm = 0;
//                        objTbl.PickUppoint = PickUppoint.Replace("'", "''");
//                        objTbl.ReportTime = ReportTime;
//                        objTbl.DepartTime = DepartTime;

//                        objTbl.NAdult = NAdult;
//                        objTbl.NChild = NChild;
//                        objTbl.Address = Address.Replace("'", "''");
//                        objTbl.City = City.Replace("'", "''");
//                        objTbl.State = State;
//                        objTbl.Country = Country;
//                        objTbl.Zip = Zip;
//                        objTbl.TelNo = mobile;
//                        objTbl.Email = Email;
//                        objTbl.Cancelled = 'N';
//                        objTbl.UserName = "Online_User";
//                        objTbl.BranchCode = "EBK0001";
//                        objTbl.ImpersonatingUserName = "";
//                        objTbl.ImpersonatingBranchCode = "";
//                        objTbl.Remarks = Remarks.Replace("'", "''");
//                        objTbl.RelTelNo = RelTelNo;
//                        objTbl.adultfare = adultfare;
//                        objTbl.childfare = childfare;
//                        objTbl.ssupplement = 0;
//                        objTbl.TotalAmount = decimal.Round(TotalAmount, 0);
//                        objTbl.STaxValue = STaxValue;
//                        objTbl.noofadultstwin = Nadultstwin;
//                        objTbl.noofadultstriple = Nadultstriple;
//                        objTbl.childwithoutbed = Nchildwithoutbed;
//                        objTbl.singleadult = Nsingleadult;
//                        objTbl.adultstwinfare = adultstwinfare;
//                        objTbl.adultstriplefare = adultstriplefare;
//                        objTbl.childwithoutbedfare = childwithoutbedfare;
//                        objTbl.singleadultfare = singleadultfare;
//                        objTbl.creditcardfee = cctax;
//                        objTbl.dormitory = Ndormitory;
//                        objTbl.dormitoryfare = dormitoryfare;
//                        objTbl.PromoCodeDiscount = Convert.ToInt32(utilpromo);
//                        int Val12 = pClsObj.fnInsertTicketDetails(objTbl);

//                        int Val13 = pClsObj.fnUpdateOnlineToursBooking(TicketCode, Convert.ToString(Session["orderid"]),
//                            Convert.ToString(TourNo), Convert.ToInt32(ldsRecSet.Tables[1].Rows[it]["rowid"]));

//                        OnlineTransactionTable clsobjTbl = new OnlineTransactionTable();
//                        clsobjTbl.AgentId = 0;
//                        clsobjTbl.RefNo = TicketCode;
//                        clsobjTbl.AgentCredit = 0;
//                        clsobjTbl.AvailableBalance = 0;
//                        clsobjTbl.AgentDebit = 0;
//                        clsobjTbl.TicketAmount = decimal.Round(Amount, 0);
//                        clsobjTbl.Commission = 0;
//                        clsobjTbl.PaymentMode = PayMode;
//                        clsobjTbl.Number = "";
//                        clsobjTbl.BankName = "";
//                        clsobjTbl.Debit = 0;
//                        clsobjTbl.Credit = strMinAmount;
//                        clsobjTbl.Remarks = "";
//                        clsobjTbl.UserName = "OnLineUser";
//                        clsobjTbl.BranchCode = CurrentUserBranchCode;
//                        clsobjTbl.ImpersonatingUserName = "";
//                        clsobjTbl.ImpersonatingBranchCode = "";
//                        clsobjTbl.Cashier = "";
//                        clsobjTbl.TransState = 'P';
//                        clsobjTbl.Status = "S";
//                        clsobjTbl.ServiceTax = decimal.Round(calcstaxvalue, 0);
//                        clsobjTbl.TourName = TourName;
//                        clsobjTbl.ccChargeAmt = decimal.Round(calccctaxvalue);
//                        clsobjTbl.PromoCode = promocode;
//                        clsobjTbl.PromoCodeValue = Convert.ToInt32(utilpromo);
//                        int Val14 = pClsObj.fnInsertOnlineTransactionTable(clsobjTbl);

//                        decimal total = Convert.ToDecimal(Amount) + Convert.ToDecimal(calcstaxvalue) + Convert.ToDecimal(calccctaxvalue);
//                        Onlinetransaction_Advance pclsTblObj = new Onlinetransaction_Advance();
//                        pclsTblObj.RefNo = TicketCode;
//                        pclsTblObj.BranchCode = "EBK0001";
//                        pclsTblObj.UserId = "0";
//                        pclsTblObj.Ticketamount = total;
//                        pclsTblObj.PaymentMode = "2";
//                        pclsTblObj.AdvancePaid = strMinAmount;
//                        pclsTblObj.TourName = TourName;
//                        pclsTblObj.noofpax = Convert.ToString(NoOfSeats);
//                        pclsTblObj.DOJ = strJdate;

//                        int Val15 = pClsObj.fnInsertOnlineTransaction_Advance(pclsTblObj);
//                        // Ends Here...................................................................

//                        // sqlstr = "update newkeytable_online set RespectiveRowid=" + tNo + " where KeyType = 'TicketNumber' ";
//                        // DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqlstr, false);


//                        //*************************************************************************************
//                        //Transaction in Ticketnames table
//                        //*************************************************************************************
//                        #region Optimize Code
//                        /*string OName;
//                    int Age;
//                    string sex;
//                    sqlstr = "Select respectiveRowId from newkeytable_online where KeyType='TicketNames'";
//                    TicketNameRowid = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, sqlstr));
//                    int itn;
//                    for (itn = 0; itn < ldsRecSet.Tables[2].Rows.Count; itn++)
//                    {
//                        OName = Convert.ToString(ldsRecSet.Tables[2].Rows[itn]["name"]);
//                        Age = Convert.ToInt16(ldsRecSet.Tables[2].Rows[itn]["age"]);
//                        sex = Convert.ToString(ldsRecSet.Tables[2].Rows[itn]["sex"]);

//                        sqlstr = "insert into TicketNames(RowId,TicketNo,Name,Age,Sex) values(" + TicketNameRowid + ",'" + TicketCode + "','" + OName + "','" + Age + "','" + sex + "')";
//                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqlstr, false);
//                        TicketNameRowid = TicketNameRowid + 1;
//                    }
//                    sqlstr = "update newkeytable_online set RespectiveRowid=" + TicketNameRowid + " where KeyType = 'TicketNames' ";
//                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqlstr, false);*/
//                        #endregion

//                        TicketName pvClsObj = null;
//                        string OName;
//                        int Age;
//                        string sex;
//                        for (int itn = 0; itn < ldsRecSet.Tables[2].Rows.Count; itn++)
//                        {
//                            TicketBusnoRowid = pClsObj.fnRowIDRetrieval("TicketNames", "RowId", "TicketNames");

//                            OName = Convert.ToString(ldsRecSet.Tables[2].Rows[itn]["name"]);
//                            Age = Convert.ToInt16(ldsRecSet.Tables[2].Rows[itn]["age"]);
//                            sex = Convert.ToString(ldsRecSet.Tables[2].Rows[itn]["sex"]);

//                            pvClsObj = new TicketName();
//                            //                          pvClsObj.RowId = TicketBusnoRowid;
//                            pvClsObj.TicketNo = TicketCode;
//                            pvClsObj.Name = OName;
//                            pvClsObj.Age = Convert.ToByte(Age);
//                            pvClsObj.Sex = Convert.ToChar(sex);
//                            int Val16 = pClsObj.fnInsertTicketNames(pvClsObj);

//                        }

//                        BranchShortCode = null;
//                        TicketNo = null;
//                        Response.Write("step" + it);
//                    }
//                }
//            }
//            finally
//            {
//                if (pClsObj != null)
//                {
//                    pClsObj = null;
//                }
//            }
//        }
//        string rowidretrieval(string s)
//        {
//            return "Select RespectiveRowId from NewKeyTable_Online where KeyType = '" + s + "'";
//        }
//        private string dummypassengers()
//        {
//            ClsAdo clsObj = null;
//            try
//            {
//                clsObj = new ClsAdo();
//                if (clsObj.fnFixed_Dummypassengers(Convert.ToString(Session["orderid"]), Convert.ToString(Session["EmailId"])) == 0)
//                    return "Y";
//                else
//                    return "N";
//            }
//            finally
//            {
//                if (clsObj != null)
//                {
//                    clsObj = null;
//                }
//            }
//        }
//        string busnoretrival(string rowid)
//        {
//            #region Optimze Code
//            /*string row = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select busno from busallot where rowid=" + rowid + "");
//            return row;*/
//            #endregion
//            ClsAdo pClsObj = null;
//            try
//            {
//                pClsObj = new ClsAdo();
//                int pBusNo = pClsObj.fnGetBusNoPay(Convert.ToInt32(rowid));
//                return Convert.ToString(pBusNo);
//            }
//            finally
//            {
//                if (pClsObj != null)
//                {
//                    pClsObj = null;
//                }
//            }
//        }
//        public void InsertPGResponseAndRequest(string orderId, string trackid, string type, string msgStr)
//        {
//            //int lStatus = 0;
//            SqlConnection lConn = null;
//            SqlCommand lCommand = null;
//            try
//            {


//                String strCn = System.Configuration.ConfigurationManager.AppSettings["southernconn"];
//                lConn = new SqlConnection(strCn);
//                lCommand = new SqlCommand("InsertPGResponseAndRequest_SP", lConn);
//                lCommand.CommandTimeout = 20 * 1000;
//                lCommand.CommandType = CommandType.StoredProcedure;
//                lCommand.Parameters.AddWithValue("@OrderId", orderId);
//                lCommand.Parameters.AddWithValue("@TrackingID", trackid);
//                lCommand.Parameters.AddWithValue("@ParamType", type);
//                lCommand.Parameters.AddWithValue("@MessageString", msgStr);

//                if (lConn.State == ConnectionState.Closed)
//                {
//                    lConn.Open();
//                }

//                lCommand.ExecuteNonQuery();

//                //return lStatus = 1;
//            }
//            catch (Exception ex)
//            {
//                //return lStatus = 0;
//            }
//            finally
//            {
//                lConn.Close();
//            }
//        }
//        #endregion
//    }
//}