using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class InstaMojoPayment : System.Web.UI.Page
    {

        public string _orderid { get; set; }
        protected void Page_Load(object sender, EventArgs e)
        {
            SendPostRequestNew();
        }

        protected void SendPostRequestNew()
        {
            string Insta_client_id = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Insta_client_id"]),//"DefMJYD2BqF4zVF2L8iOXrcDS6yWFVXB5tTGvDnN",
            Insta_client_secret = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Insta_client_secret"]),//"rmP59Pxo6praYmSW83Ey7OPE7oYjVa8uRzttYPwhWxfnAhXQh2fPUpZXHekJPLWqtASLyIFi4uXuQes7oVSkY0QKM6vDk1LkEOC4PPIIstp6fsEpwxScW8vVgK79nOZg",
                      Insta_Endpoint = InstamojoConstants.INSTAMOJO_API_ENDPOINT,
                      Insta_Auth_Endpoint = InstamojoConstants.INSTAMOJO_AUTH_ENDPOINT;
            Instamojo objClass = InstamojoImplementation.getApi(Insta_client_id, Insta_client_secret, Insta_Endpoint, Insta_Auth_Endpoint);
            CreatePaymentOrder(objClass);

        }


        public void CreatePaymentOrder(Instamojo objClass)
        {
            ClsAdo lLinq = new ClsAdo();
            List<Get_PgResponse_SPResult> lResult = null;
            PaymentOrder objPaymentRequest = new PaymentOrder();

            try
            {
                lResult = new List<Get_PgResponse_SPResult>();
                lResult = lLinq.fnGet_PgResponse(Request["RID"].ToString());

                if (lResult != null && lResult.Count > 0)
                {

                    string REMOTE_ADDR = Request.ServerVariables["REMOTE_ADDR"].ToString();
                    string lDate = DateTime.Now.ToString();
                    string lTxnid = lResult[0].Udf5.ToString();
                    _orderid = lTxnid;
                    double lAmount = Convert.ToDouble(lResult[0].Amount);// "Minimum 51"

                    string lUDF1 = lResult[0].Udf1.ToString();// "Santosh Kumar";
                    string lUDF2 = lResult[0].Udf2.ToString();// "Santosh.kumar@Sirez.com";
                    string lUDF3 = lResult[0].Udf3.ToString();// "9990138893";
                    string lUDF4 = lResult[0].Udf4.ToString();// "C205 Naraina New Delhi 110025";
                    string lUDF5 = lResult[0].Udf5.ToString();
                    string lUDF6 = lResult[0].TranID.ToString();

                    //Required POST parameters

                    string GetRandomNo = Convert.ToString(GenerateRandomNo());

                    objPaymentRequest.name = lUDF1;
                    objPaymentRequest.email = lUDF2;
                    objPaymentRequest.phone = lUDF3;
                    objPaymentRequest.description = lUDF5;
                    objPaymentRequest.amount = lAmount;
                    objPaymentRequest.currency = "INR";
                    objPaymentRequest.transaction_id = GetRandomNo + "-" + lUDF5;
                    //objPaymentRequest.transaction_id = Convert.ToString(Request["RID"]);


                    //string randomName = Path.GetRandomFileName();
                    //randomName = randomName.Replace(".", string.Empty);
                    //objPaymentRequest.redirect_url = "https://swaggerhub.com/api/saich/pay-with-instamojo/1.0.0";

                    objPaymentRequest.redirect_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["Redirect_Url"]);

                    //webhook_url Is Optional
                    objPaymentRequest.webhook_url = Convert.ToString(System.Configuration.ConfigurationManager.AppSettings["webhook_url"]); //"https://your.server.com/webhook";
                                                                                                                                            //Extra POST parameters 

                    if (objPaymentRequest.validate())
                    {
                        if (objPaymentRequest.emailInvalid)
                        {
                            ClsCommon.ShowAlert("Email is not valid");
                        }
                        if (objPaymentRequest.nameInvalid)
                        {
                            ClsCommon.ShowAlert("Name is not valid");
                        }
                        if (objPaymentRequest.phoneInvalid)
                        {
                            ClsCommon.ShowAlert("Phone is not valid");
                        }
                        if (objPaymentRequest.amountInvalid)
                        {
                            ClsCommon.ShowAlert("Amount is not valid");
                        }
                        if (objPaymentRequest.currencyInvalid)
                        {
                            ClsCommon.ShowAlert("Currency is not valid");
                        }
                        if (objPaymentRequest.transactionIdInvalid)
                        {
                            ClsCommon.ShowAlert("Transaction Id is not valid");
                        }
                        if (objPaymentRequest.redirectUrlInvalid)
                        {
                            ClsCommon.ShowAlert("Redirect Url Id is not valid");
                        }
                        if (objPaymentRequest.webhookUrlInvalid)
                        {
                            ClsCommon.ShowAlert("Webhook URL is not valid");
                        }

                    }
                    else
                    {
                        try
                        {
                            CreatePaymentOrderResponse objPaymentResponse = objClass.createNewPaymentRequest(objPaymentRequest);
                            string url = objPaymentResponse.payment_options.payment_url.Replace("?embed=form", "");

                            InsertPGResponseAndRequest(_orderid, "", "Request", url);

                            Response.Redirect(url, false);
                        }
                        catch (ArgumentNullException ex)
                        {
                            ClsCommon.ShowAlert(ex.Message);
                        }
                        catch (WebException ex)
                        {
                            ClsCommon.ShowAlert(ex.Message);
                        }
                        catch (IOException ex)
                        {
                            ClsCommon.ShowAlert(ex.Message);
                        }
                        catch (InvalidPaymentOrderException ex)
                        {
                            if (!ex.IsWebhookValid())
                            {
                                ClsCommon.ShowAlert("Webhook is invalid");

                            }

                            if (!ex.IsCurrencyValid())
                            {
                                ClsCommon.ShowAlert("Currency is Invalid");
                            }

                            if (!ex.IsTransactionIDValid())
                            {
                                ClsCommon.ShowAlert("Transaction ID is Inavlid");
                            }
                        }
                        catch (ConnectionException ex)
                        {
                            ClsCommon.ShowAlert(ex.Message);
                        }
                        catch (BaseException ex)
                        {
                            ClsCommon.ShowAlert(ex.Message);
                        }
                        catch (Exception ex)
                        {
                            ClsCommon.ShowAlert("Error:" + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Redirect("PaymentError.aspx?Message=" + ex.Message);
            }
        }


        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
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