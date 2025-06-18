using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AtomPayment : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            SendPostRequestNew();
        }
        protected void SendPostRequestNew()
        {
            ClsAdo lLinq = new ClsAdo();
            List<Get_PgResponse_SPResult> lResult = null;
            string txnData = "";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://payment.atomtech.in/paynetz/epi/fts?");  // For Live
                                                                                                                             //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("http://203.114.240.183/paynetz/epi/fts?"); // For Test
                                                                                                                             //HttpWebRequest request = (HttpWebRequest)WebRequest.Create("https://paynetzuat.atomtech.in/paynetz/epi/fts?"); // For Test
                request.Method = "POST";
                //------
                request.ContentType = "application/x-www-form-urlencoded";
                request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; CK={CVxk71YSfgiE6+6P6ftT7lWzblrdvMbRqavYf/6OcMIH8wfE6iK7TNkcwFAsxeChX7qRAlQhvPWso3KI6Jthvnvls9scl+OnAEhsgv+tuvs=}; .NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                //string postData = "login=160&pass=Test@123&ttype=NBFundTransfer&prodid=NSE&amt=1&txncurr=INR&txnscamt=0&clientcode=007=&txnid=1234567&date=25/06/2012%2012:23:23&custacc=123456789012";
                lResult = new List<Get_PgResponse_SPResult>();
                lResult = lLinq.fnGet_PgResponse(Request["RID"].ToString());
                string lEmiMonth = Convert.ToString(Request["lEMIMonth"]);
                string lBankName = Convert.ToString(Request["lEMIBank"]);
                if (lResult != null && lResult.Count > 0)
                {
                    // Test Credential 
                    //string lLoginID = "197";//"160"; //197
                    //string LPWS = "Test@123";
                    //string lProdID = "NSE";
                    //string reqHashKey = "KEY123657234";

                    string reqHashKey = "";
                    string lLoginID = "";
                    string LPWS = "";
                    string lProdID = "";

                    lLoginID = "1460";
                    LPWS = "SOUTHERN@123";
                    lProdID = "B2C";
                    reqHashKey = "62c3dd0c40b0ac53ca";

                    // Live Credential  For Agent 
                    Session["SectionNameForRes"] = null;
                    if (Convert.ToString(Request["SectionName"]) == "AgCredit")
                    {
                        Session["SectionNameForRes"] = "AgCredit";
                        reqHashKey = "a905fb6de39f37387d";
                        lLoginID = "1461";
                        LPWS = "SOUTHERN@123";
                        lProdID = "B2B";
                    }

                    string lDate = DateTime.Now.ToString();
                    string lTxnid = lResult[0].Udf5.ToString();
                    string lClientCode = "007";
                    string lCustAC = "";
                    string lTxnscAmt = "0";
                    string lTxnCurr = "INR";
                    string lAmount = lResult[0].Amount.ToString();// "Minimum 51";
                    string lURL = "https://www.southerntravelsindia.com/AtomPaymentResponce.aspx";
                    //string lURL = "http://localhost:54618/Southern_Travels2017/AtomPaymentResponce.aspx";
                    string lUDF1 = lResult[0].Udf1.ToString();// "Santosh Kumar";
                    string lUDF2 = lResult[0].Udf2.ToString();// "Santosh.kumar@Sirez.com";
                    string lUDF3 = lResult[0].Udf3.ToString();// "9990138893";
                    string lUDF4 = lResult[0].Udf4.ToString();// "C205 Naraina New Delhi 110025";
                    string lUDF5 = lBankName; // lResult[0].Udf5.ToString();
                    string lUDF6 = lEmiMonth; //02 or 03 or 04 etc EMI

                    string postData = "login=" + lLoginID + "&pass=" + LPWS + "&ttype=NBFundTransfer&prodid=" + lProdID +
                        "&amt=" + lAmount + "&txncurr=" + lTxnCurr + "&txnscamt=" + lTxnscAmt + "&clientcode=" + lClientCode +
                        "=&txnid=" + lTxnid + "&date=" + lDate + "&custacc=" + lCustAC + "0&udf1=" + lUDF1 + "&udf2=" + lUDF2 + "&udf3=" + lUDF3 + "&udf4=" + lUDF4;

                    if (Request["lEMIMonth"] != null && Request["lEMIMonth"].ToString() == "3 Month")
                    {
                        lUDF6 = "02";
                        postData = postData + "&udf5=" + lUDF5 + "&udf6=" + lUDF6;
                    }
                    if (Request["lEMIMonth"] != null && Request["lEMIMonth"].ToString() == "6 Month")
                    {
                        lUDF6 = "03";
                        postData = postData + "&udf5=" + lUDF5 + "&udf6=" + lUDF6;
                    }
                    if (Request["lEMIMonth"] != null && Request["lEMIMonth"].ToString() == "9 Month")
                    {
                        lUDF6 = "04";
                        postData = postData + "&udf5=" + lUDF5 + "&udf6=" + lUDF6;
                    }
                    if (Request["lEMIMonth"] != null && Request["lEMIMonth"].ToString() == "12 Month")
                    {
                        lUDF6 = "05";
                        postData = postData + "&udf5=" + lUDF5 + "&udf6=" + lUDF6;
                    }
                    if (Request["lEMIMonth"] != null && Request["lEMIMonth"].ToString() == "18 Month")
                    {
                        lUDF6 = "06";
                        postData = postData + "&udf5=" + lUDF5 + "&udf6=" + lUDF6;
                    }
                    if (Request["lEMIMonth"] != null && Request["lEMIMonth"].ToString() == "24 Month")
                    {
                        lUDF6 = "07";
                        postData = postData + "&udf5=" + lUDF5 + "&udf6=" + lUDF6;
                    }

                    postData = postData + "&ru=" + lURL + "";



                    string signature = "";
                    string strsignature = lLoginID + LPWS + "NBFundTransfer" + lProdID + lTxnid + lAmount + lTxnCurr;
                    signature = Generatehash512(strsignature, reqHashKey);
                    postData = postData + "&signature=" + signature + "";

                    InsertPGResponseAndRequest(lTxnid, "", "Request", postData);

                    txnData = "https://payment.atomtech.in/paynetz/epi/fts?" + postData;





                    //byte[] byteArray = Encoding.UTF8.GetBytes(postData);

                    //request.ContentType = "application/x-www-form-urlencoded";

                    //request.ContentLength = byteArray.Length;
                    //request.AllowAutoRedirect = true;

                    //request.Proxy.Credentials = CredentialCache.DefaultCredentials;

                    //Stream dataStream = request.GetRequestStream();

                    //dataStream.Write(byteArray, 0, byteArray.Length);

                    //dataStream.Close();

                    //WebResponse response = request.GetResponse();

                    //XmlDocument objXML = new XmlDocument();

                    //dataStream = response.GetResponseStream();

                    ////objXML.Load(dataStream);

                    ////string TxnId = objXML.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[2].InnerText;

                    ////string Token = objXML.DocumentElement.ChildNodes[0].ChildNodes[0].ChildNodes[3].InnerText;
                    ////txnData = "ttype=NBFundTransfer&txnStage=1&tempTxnId=" + TxnId + "&token=" + Token;
                    //txnData = postData;
                    //dataStream.Close();
                    //response.Close();
                    //Response.Redirect("https://payment.atomtech.in/paynetz/epi/fts?" + txnData);  // For Live

                    //Response.Redirect("http://203.114.240.183/paynetz/epi/fts?" + txnData); // For Test
                }
            }
            catch (Exception ex)
            {
                //Response.Redirect("PaymentError.aspx?Message=" + ex.Message);
                ClsCommon.LogAndSendError(ex.ToString() + " Atom Payment Page- SendPostRequestNew");
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");
            }
            finally
            {
                if (lLinq != null)
                {
                    lLinq = null;
                }
                if (lResult != null)
                {
                    lResult = null;
                }
            }
            try
            {
                //Response.Redirect("https://payment.atomtech.in/paynetz/epi/fts?" + txnData,false);  // For Live
                HttpContext.Current.Response.Redirect(txnData, false);
            }
            catch (Exception ex)
            {
                ClsCommon.LogAndSendError(ex.ToString() + " Atom Payment Page- SendPostRequestNew");
                //Response.Write(ex.ToString());
            }
            //Response.Redirect("https://paynetzuat.atomtech.in/paynetz/epi/fts?" + txnData);  //For Test
        }

        //Code for Encode
        //---------------------------------

        private static string EncodeTo64UTF8(String str)
        {
            byte[] toEncode2Bytes = System.Text.Encoding.UTF8.GetBytes(str);
            string sReturnValue = System.Convert.ToBase64String(toEncode2Bytes);
            return sReturnValue;
        }

        public string Generatehash512(string text, string reqHasKey)
        {
            string reqHashKey = reqHasKey;
            string signature = "";
            byte[] bytes = Encoding.UTF8.GetBytes(reqHashKey);
            byte[] bt = new System.Security.Cryptography.HMACSHA512(bytes).ComputeHash(Encoding.UTF8.GetBytes(text));
            signature = byteToHexString(bt).ToLower();
            return signature;
        }

        public static string byteToHexString(byte[] byData)
        {
            StringBuilder sb = new StringBuilder((byData.Length * 2));
            for (int i = 0; (i < byData.Length); i++)
            {
                int v = (byData[i] & 255);
                if ((v < 16))
                {
                    sb.Append('0');
                }
                sb.Append(v.ToString("X"));
            }
            return sb.ToString();
        }

        public void InsertPGResponseAndRequest(string orderId, string trackid, string type, string msgStr)
        {
            //int lStatus = 0;
            SqlConnection lConn = null;
            SqlCommand lCommand = null;
            try
            {


                String strCn = System.Configuration.ConfigurationManager.AppSettings["southernconn"];
                lConn = new SqlConnection(strCn);
                lCommand = new SqlCommand(StoredProcedures.InsertPGResponseAndRequest_SP, lConn);
                lCommand.CommandTimeout = 20 * 1000;
                lCommand.CommandType = CommandType.StoredProcedure;
                lCommand.Parameters.AddWithValue("@OrderId", orderId);
                lCommand.Parameters.AddWithValue("@TrackingID", trackid);
                lCommand.Parameters.AddWithValue("@ParamType", type);
                lCommand.Parameters.AddWithValue("@MessageString", msgStr);

                if (lConn.State == ConnectionState.Closed)
                {
                    lConn.Open();
                }

                lCommand.ExecuteNonQuery();

                //return lStatus = 1;
            }
            catch (Exception ex)
            {
                //return lStatus = 0;
            }
            finally
            {
                lConn.Close();
            }
        }
    }
}