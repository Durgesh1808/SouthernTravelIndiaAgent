using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class HDFCCreditCardPayment : System.Web.UI.Page
    {
        //CCACrypto ccaCrypto = new CCACrypto();

        //public string SiteURL = System.Configuration.ConfigurationManager.AppSettings["SiteURL"];
        //public string Merchant_Id = System.Configuration.ConfigurationManager.AppSettings["Merchant_Id"];
        //public string strEncRequest = "";
        //public string strAccessCode = System.Configuration.ConfigurationManager.AppSettings["AccessCode"];
        //public string WorkingKey = System.Configuration.ConfigurationManager.AppSettings["WorkingKey"];
        //public string CCAVTranURL = System.Configuration.ConfigurationManager.AppSettings["CCAVTranURL"];
        //public string strRequest = string.Empty;
        //public string Encrypteddata = string.Empty;

        //protected void Page_Load(object sender, EventArgs e)
        //{
        //    if (!IsPostBack)
        //    {
        //        ClsAdo lLinq = new ClsAdo();
        //        List<Get_PgResponse_SPResult> lResult = null;


        //        try
        //        {
        //            lResult = new List<Get_PgResponse_SPResult>();
        //            lResult = lLinq.fnGet_PgResponse(Request["RID"].ToString());
        //            if (lResult != null && lResult.Count > 0)
        //            {

        //                string merchant_id = Merchant_Id;
        //                string order_id = lResult[0].Udf5.ToString();
        //                string currency = "INR";
        //                string amount = lResult[0].Amount.ToString();
        //                string redirect_url = "GetHDFCPaymentResponse.aspx";
        //                string cancel_url = "PaymentError.aspx";
        //                string merchant_param1 = lResult[0].Udf1.ToString();
        //                string merchant_param2 = lResult[0].Udf2.ToString();
        //                string merchant_param3 = lResult[0].Udf3.ToString();
        //                string merchant_param4 = lResult[0].Udf4.ToString();
        //                string merchant_param5 = lResult[0].Udf5.ToString();
        //                string billing_email = lResult[0].EmailID.ToString();
        //                string promo_code = "";
        //                //string TranUrl = "https://test.ccavenue.com/transaction/transaction.do?command=initiateTransaction";

        //                RequestParameter objparam = new RequestParameter();

        //                string TranTrackid = DateTime.Now.Year.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Day.ToString()
        //                                       + DateTime.Now.Hour.ToString()
        //                                       + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();

        //                //objparam.tid = lResult[0].Udf5.ToString();
        //                objparam.tid = TranTrackid;
        //                objparam.merchant_id = Merchant_Id;
        //                objparam.order_id = order_id;
        //                objparam.amount = amount;
        //                string Redirect_Url = SiteURL + redirect_url;
        //                string Cancel_Url = SiteURL + cancel_url;
        //                objparam.currency = currency;
        //                objparam.redirect_url = Redirect_Url;
        //                objparam.cancel_url = Cancel_Url;
        //                objparam.billing_email = billing_email;
        //                objparam.merchant_param1 = merchant_param1;
        //                objparam.merchant_param2 = merchant_param2;
        //                objparam.merchant_param3 = merchant_param3;
        //                objparam.merchant_param4 = merchant_param4;
        //                objparam.merchant_param5 = merchant_param5;

        //                string strencryptdata = AddParameter(objparam);
        //                strEncRequest = strencryptdata;

        //                //UpdatePGResponseAndRequest(order_id, strRequest, "", "REQ");
        //                InsertPGResponseAndRequest(order_id, "", "Request", strRequest);


        //                string lSectionName = "";
        //                if (Request["SectionName"] != null)
        //                {
        //                    lSectionName = Request["SectionName"].ToString();
        //                }
        //                ClsAdo clsObj = null;
        //                int? lStatus = 0;
        //                try
        //                {
        //                    clsObj = new ClsAdo();

        //                    lStatus = clsObj.fnInsertPaymentHDFCPG("", order_id, merchant_param2, "", Convert.ToDecimal(amount), "", "", TranTrackid,
        //                         "", "", "", merchant_param1, merchant_param2, merchant_param3, merchant_param4, order_id, "", lSectionName);
        //                }
        //                finally
        //                {
        //                    if (clsObj != null)
        //                    {
        //                        clsObj = null;
        //                    }
        //                }
        //                redirectopayment();
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            ClsCommon.LogAndSendError(ex.ToString() + " HDFCCreditCardPayment.aspx - PageLoad");
        //        }
        //        finally
        //        {
        //            if (lLinq != null)
        //            {
        //                lLinq = null;
        //            }
        //            if (lResult != null)
        //            {
        //                lResult = null;
        //            }
        //        }
        //    }
        //}

        //private void redirectopayment()
        //{
        //    string outputHTML = "<html>";
        //    outputHTML += "<head>";
        //    //outputHTML += "<script type='text/javascript' src='http://ajax.googleapis.com/ajax/libs/jquery/1.10.2/jquery.min.js'></script>";
        //    //outputHTML += "<script type='text/javascript'>";
        //    //outputHTML += "$(document).ready(function () {  $('#nonseamless').submit();  });";
        //    //outputHTML += "</script>";
        //    outputHTML += "<title>Merchant Check Out Page</title>";
        //    outputHTML += "</head>";
        //    outputHTML += "<body>";
        //    outputHTML += "<center><h1>Please do not refresh this page...</h1></center>";
        //    outputHTML += "<form id='onseamless' method='post' name='redirect' action='" + CCAVTranURL + "'>";
        //    //outputHTML += "<table border='0'>";
        //    //outputHTML += "<tbody>";

        //    outputHTML += "<input type='hidden' id='encRequest' name='encRequest' value='" + strEncRequest + "'>";
        //    outputHTML += "<input type='hidden' name='access_code' id='Hidden1' value='" + strAccessCode + "'>";
        //    //outputHTML += "</tbody>";
        //    //outputHTML += "</table>";
        //    outputHTML += "<script type='text/javascript'>";
        //    outputHTML += "document.redirect.submit();";
        //    //outputHTML += "$(document).ready(function () {  $('#nonseamless').submit();  });";
        //    outputHTML += "</script>";
        //    outputHTML += "</form>";
        //    outputHTML += "</body>";
        //    outputHTML += "</html>";
        //    Response.Write(outputHTML);

        //    ClsCommon.LogAndSendError("HDFCCreditCardRequest-" + strEncRequest);

        //}

        //public string AddParameter(RequestParameter Requestdata)
        //{

        //    if (Requestdata.order_id != string.Empty && Requestdata.merchant_id != string.Empty && Requestdata.currency != string.Empty && Requestdata.amount != string.Empty && Requestdata.redirect_url != string.Empty && Requestdata.cancel_url != string.Empty)
        //    {
        //        strRequest = "tid=" + Requestdata.tid + "&order_id=" + Requestdata.order_id + "&currency=INR&amount=" + Requestdata.amount + "&merchant_id=" + Requestdata.merchant_id + "&redirect_url=" + Requestdata.redirect_url + "&cancel_url=" + Requestdata.cancel_url;
        //        if (Requestdata.merchant_param1 != null)
        //        {
        //            strRequest = strRequest + " &merchant_param1=" + Requestdata.merchant_param1;
        //        }
        //        if (Requestdata.merchant_param2 != null)
        //        {
        //            strRequest = strRequest + " &merchant_param2=" + Requestdata.merchant_param2;
        //        }
        //        if (Requestdata.merchant_param3 != null)
        //        {
        //            strRequest = strRequest + " &merchant_param3=" + Requestdata.merchant_param3;
        //        }
        //        if (Requestdata.merchant_param4 != null)
        //        {
        //            strRequest = strRequest + " &merchant_param4=" + Requestdata.merchant_param4;
        //        }
        //        if (Requestdata.merchant_param5 != null)
        //        {
        //            strRequest = strRequest + " &merchant_param5=" + Requestdata.merchant_param5;
        //        }
        //        if (Requestdata.promo_code != null)
        //        {
        //            strRequest = strRequest + " &promo_code=" + Requestdata.promo_code;
        //        }
        //        Encrypteddata = ccaCrypto.Encrypt(strRequest, WorkingKey);

        //    }

        //    return Encrypteddata;

        //}

        //public class RequestParameter
        //{
        //    public string tid { get; set; }
        //    public string merchant_id { get; set; }
        //    public string order_id { get; set; }
        //    public string amount { get; set; }
        //    public string currency { get; set; }
        //    public string redirect_url { get; set; }
        //    public string cancel_url { get; set; }
        //    public string merchant_param1 { get; set; }
        //    public string merchant_param2 { get; set; }
        //    public string merchant_param3 { get; set; }
        //    public string merchant_param4 { get; set; }
        //    public string merchant_param5 { get; set; }
        //    public string promo_code { get; set; }
        //    public string billing_email { get; set; }

        //}

        //public void InsertPGResponseAndRequest(string orderId, string trackid, string type, string msgStr)
        //{
        //    //int lStatus = 0;
        //    SqlConnection lConn = null;
        //    SqlCommand lCommand = null;
        //    try
        //    {


        //        String strCn = System.Configuration.ConfigurationManager.AppSettings["southernconn"];
        //        lConn = new SqlConnection(strCn);
        //        lCommand = new SqlCommand(StoredProcedures.InsertPGResponseAndRequest_SP, lConn);
        //        lCommand.CommandTimeout = 20 * 1000;
        //        lCommand.CommandType = CommandType.StoredProcedure;
        //        lCommand.Parameters.AddWithValue("@OrderId", orderId);
        //        lCommand.Parameters.AddWithValue("@TrackingID", trackid);
        //        lCommand.Parameters.AddWithValue("@ParamType", type);
        //        lCommand.Parameters.AddWithValue("@MessageString", msgStr);

        //        if (lConn.State == ConnectionState.Closed)
        //        {
        //            lConn.Open();
        //        }

        //        lCommand.ExecuteNonQuery();

        //        //return lStatus = 1;
        //    }
        //    catch (Exception ex)
        //    {
        //        //return lStatus = 0;
        //    }
        //    finally
        //    {
        //        lConn.Close();
        //    }
        //}


    }
}