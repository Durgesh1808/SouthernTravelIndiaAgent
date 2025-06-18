using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class PayUPaymet : System.Web.UI.Page
    {
        public string action1 = string.Empty;
        public string hash1 = string.Empty;
        public string txnid1 = string.Empty;
        public string fName = string.Empty;
        public string surl = string.Empty;
        public string furl = string.Empty;
        public string orderid = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            ClsAdo lLinq = new ClsAdo();
            List<Get_PgResponse_SPResult> lResult = null;
            try
            {
                surl = ConfigurationManager.AppSettings["ResponseURL"];
                furl = ConfigurationManager.AppSettings["PaymentErrorURL"];

                fName = Convert.ToString(Request.QueryString["FirstName"]);
                if (fName.Contains("."))
                {
                    fName = Convert.ToString(Request.QueryString["FirstName"].Split('.').GetValue(1)).Trim();
                }

                key.Value = ConfigurationManager.AppSettings["MERCHANT_KEY"];

                lResult = new List<Get_PgResponse_SPResult>();
                lResult = lLinq.fnGet_PgResponse(Request["RID"].ToString());
                if (lResult != null && lResult.Count > 0)
                {
                    string lAmount = lResult[0].Amount.ToString(); //"1";//
                    string lOrderID = lResult[0].Udf5.ToString();
                    orderid = lOrderID;
                    string lUDF1 = lResult[0].Udf1.ToString();
                    string lUDF2 = lResult[0].Udf2.ToString();
                    string lUDF3 = lResult[0].Udf3.ToString();
                    string lUDF4 = lResult[0].Udf4.ToString();
                    string lUDF5 = lResult[0].Udf5.ToString();
                    string lTempTranID = lResult[0].TranID.ToString();

                    furl = furl + "?Message=Transaction Cancelled" + "&ResTrackId=" + lUDF5 + "&ResAmount=" + lAmount;

                    /* string lTempTranID = Request["RID"].ToString() + System.DateTime.Now.Day.ToString() + System.DateTime.Now.Month.ToString() + System.DateTime.Now.Year.ToString() +
                    System.DateTime.Now.Hour.ToString() + System.DateTime.Now.Minute.ToString() + System.DateTime.Now.Second.ToString() + System.DateTime.Now.Millisecond.ToString();
                     */
                    txnid1 = lTempTranID;// lOrderID;
                    GoPayment(lAmount, lTempTranID, lUDF1, lUDF2, lUDF3, lUDF4, lUDF5);
                }
                if (!IsPostBack)
                {
                    frmError.Visible = false; // error form
                }
                else
                {
                    //frmError.Visible = true;
                }

            }
            catch (Exception ex)
            {
                ClsCommon.LogAndSendError(ex.ToString() + " PayUPayment.aspx - Page_Load");
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

        }

        private void GoPayment(string lAmount, string lOrderID, string lUDF1, string lUDF2, string lUDF3, string lUDF4, string lUDF5)
        {
            try
            {

                string[] hashVarsSeq;
                string hash_string = string.Empty;
                txnid1 = lOrderID;

                if (string.IsNullOrEmpty(Request.Form["hash"])) // generating hash value
                {

                    frmError.Visible = false;
                    hashVarsSeq = ConfigurationManager.AppSettings["hashSequence"].Split('|'); // spliting hash sequence from config
                    hash_string = "";
                    //C0Dr8m|txnid1|lAmount|lUDF1|ST|lUDF2|lUDF1|lUDF2|lUDF3|lUDF5|lUDF5||||||
                    foreach (string hash_var in hashVarsSeq)
                    {
                        if (hash_var == "key")
                        {
                            hash_string = hash_string + ConfigurationManager.AppSettings["MERCHANT_KEY"];
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "txnid")
                        {
                            hash_string = hash_string + txnid1;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "amount")
                        {
                            hash_string = hash_string + Convert.ToDecimal(lAmount/*Request.Form[hash_var]*/).ToString("g29");
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "productinfo")
                        {
                            hash_string = hash_string + lUDF1.ToString() + " (" + lUDF5.ToString() + ")";
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "firstname")
                        {
                            hash_string = hash_string + fName;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "email")
                        {
                            hash_string = hash_string + lUDF2;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf1")
                        {
                            hash_string = hash_string + lUDF1;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf2")
                        {
                            hash_string = hash_string + lUDF2;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf3")
                        {
                            hash_string = hash_string + lUDF3;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf4")
                        {
                            hash_string = hash_string + lUDF4;
                            hash_string = hash_string + '|';
                        }
                        else if (hash_var == "udf5")
                        {
                            hash_string = hash_string + lUDF5;
                            hash_string = hash_string + '|';
                        }
                        else
                        {

                            hash_string = hash_string + (Request.Form[hash_var] != null ? Request.Form[hash_var] : "");// isset if else
                            hash_string = hash_string + '|';
                        }
                    }

                    hash_string += ConfigurationManager.AppSettings["SALT"];// appending SALT

                    hash1 = Generatehash512(hash_string).ToLower();         //generating hash
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";// setting URL
                }

                else if (!string.IsNullOrEmpty(Request.Form["hash"]))
                {
                    hash1 = Request.Form["hash"];
                    action1 = ConfigurationManager.AppSettings["PAYU_BASE_URL"] + "/_payment";

                }

                if (!string.IsNullOrEmpty(hash1))
                {
                    hash.Value = hash1;
                    txnid.Value = txnid1;

                    System.Collections.Hashtable data = new System.Collections.Hashtable(); // adding values in gash table for data post
                    data.Add("hash", hash.Value);
                    data.Add("txnid", txnid.Value);
                    data.Add("key", key.Value);
                    string AmountForm = Convert.ToDecimal(lAmount.Trim()).ToString("g29");// eliminating trailing zeros
                    data.Add("amount", AmountForm);
                    data.Add("firstname", fName);
                    data.Add("email", lUDF2);
                    data.Add("phone", lUDF3);
                    data.Add("productinfo", lUDF1 + " (" + lUDF5 + ")");
                    data.Add("surl", surl);
                    data.Add("furl", furl);

                    data.Add("lastname", "");
                    data.Add("curl", "");
                    data.Add("address1", "");
                    data.Add("address2", "");
                    data.Add("city", "");
                    data.Add("state", "");
                    data.Add("country", "");
                    data.Add("zipcode", "");
                    data.Add("udf1", lUDF1);
                    data.Add("udf2", lUDF2);
                    data.Add("udf3", lUDF3);
                    data.Add("udf4", lUDF4);
                    data.Add("udf5", lUDF5);
                    data.Add("pg", "CC");
                    data.Add("drop_category", "COD");

                    string strForm = PreparePOSTForm(action1, data);

                    InsertPGResponseAndRequest(orderid, "", "Request", strForm);

                    Page.Controls.Add(new LiteralControl(strForm));

                }

                else
                {
                    //no hash

                }

            }

            catch (Exception ex)
            {
                ClsCommon.LogAndSendError(ex.ToString() + " PayUPayment.aspx - GoPayment-PreparePOSTForm");
                Response.Write("<span style='color:red'>" + ex.Message + "</span>");

            }
        }
        private string PreparePOSTForm(string url, System.Collections.Hashtable data)      // post form
        {
            //Set a name for the form
            string formID = "PostForm";
            //Build the form using the specified data to be posted.
            StringBuilder strForm = new StringBuilder();
            strForm.Append("<form id=\"" + formID + "\" name=\"" +
                           formID + "\" action=\"" + url +
                           "\" method=\"POST\">");

            foreach (System.Collections.DictionaryEntry key in data)
            {

                strForm.Append("<input type=\"hidden\" name=\"" + key.Key +
                               "\" value=\"" + key.Value + "\">");
            }


            strForm.Append("</form>");
            //Build the JavaScript which will do the Posting operation.
            StringBuilder strScript = new StringBuilder();
            strScript.Append("<script language='javascript'>");
            strScript.Append("var v" + formID + " = document." +
                             formID + ";");
            strScript.Append("v" + formID + ".submit();");
            strScript.Append("</script>");
            //Return the form and the script concatenated.
            //(The order is important, Form then JavaScript)
            return strForm.ToString() + strScript.ToString();
        }
        public string Generatehash512(string text)
        {

            byte[] message = Encoding.UTF8.GetBytes(text);

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] hashValue;
            SHA512Managed hashString = new SHA512Managed();
            string hex = "";
            hashValue = hashString.ComputeHash(message);
            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;

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