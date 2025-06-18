using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class TechProcessPayment_Request : System.Web.UI.Page
    {
        public string rdurl = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            SendPostRequestNew();
        }
        protected void SendPostRequestNew()
        {
            //ClsAdo lLinq = new ClsAdo();
            //List<Get_PgResponse_SPResult> lResult = null;
            //COM.TPSLUtil1 objTPSLUtil1 = new COM.TPSLUtil1();
            //COM.CheckSumRequestBean objCheckSumRequestBean = new COM.CheckSumRequestBean();

            //try
            //{
            //    lResult = new List<Get_PgResponse_SPResult>();
            //    lResult = lLinq.fnGet_PgResponse(Request["RID"].ToString());
            //    string lEmiMonth = Convert.ToString(Request["lEMIMonth"]);
            //    string lBankName = Convert.ToString(Request["lEMIBank"]);
            //    if (lResult != null && lResult.Count > 0)
            //    {
            //        string lDate = DateTime.Now.ToString();
            //        string lTxnid = lResult[0].Udf5.ToString();
            //        string lCustAC = "1";
            //        string lTxnscAmt = "1"; // "For Testing Amt 1";
            //        string lAmount = lResult[0].Amount.ToString();  // "Minimum 1";
            //        string lUDF1 = lResult[0].Udf1.ToString();  // "Santosh Kumar";
            //        string lUDF2 = lResult[0].Udf2.ToString();  // "Santosh.kumar@Sirez.com";
            //        string lUDF3 = lResult[0].Udf3.ToString();  // "9990138893";
            //        string lUDF4 = lResult[0].Udf4.ToString();  // "C205 Naraina New Delhi 110025";
            //        string lUDF5 = lResult[0].Udf5.ToString();

            //        objCheckSumRequestBean.MerchantTranId = lTxnid;
            //        objCheckSumRequestBean.MarketCode = "L440";
            //        objCheckSumRequestBean.AccountNo = lCustAC;
            //        objCheckSumRequestBean.Amt = lAmount;
            //        objCheckSumRequestBean.BankCode = "NA";
            //        objCheckSumRequestBean.PropertyPath = "C:\\HostingSpaces\\southern\\southerntravelsindia.com\\wwwroot\\Property\\MerchantDetails.property";

            //        string strMsg = objTPSLUtil1.transactionRequestMessage(objCheckSumRequestBean);
            //        if (!strMsg.Equals(""))
            //        {
            //            rdurl = "https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg;

            //            //Response.Redirect("https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg);
            //            //Response.Redirect("https://www.tpsl-india.in/PaymentGateway/TransactionRequest.jsp?msg=" + strMsg);
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Response.Redirect("PaymentError.aspx?Message=" + ex.Message);
            //    //Response.Write("<span style='color:red'>" + ex.Message + "</span>");
            //}
            //finally
            //{
            //    if (lLinq != null)
            //    {
            //        lLinq = null;
            //    }
            //    if (lResult != null)
            //    {
            //        lResult = null;
            //    }
            //    if (objCheckSumRequestBean != null)
            //    {
            //        objCheckSumRequestBean = null;
            //    }
            //    if (objTPSLUtil1 != null)
            //    {
            //        objTPSLUtil1 = null;
            //    }
            //}
        }
    }
}