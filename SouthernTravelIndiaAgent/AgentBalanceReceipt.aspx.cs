using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentBalanceReceipt : System.Web.UI.Page
    {
        protected StringBuilder s = new StringBuilder();
        ClsAdo clsObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string strPnr = "";
                if ((Request.QueryString["CID"]) != null)
                {
                    strPnr = DataLib.funClear(Request.QueryString["CID"].ToString());
                    string k = strPnr.Substring(0, 3).ToUpper();
                    if (k == "EBK")
                        fixedtour(strPnr);
                    else if (k == "SPL")
                        specialbalanceclear(strPnr);
                    else if (k == "INT")
                        InternationalTour(strPnr);
                    else
                        ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Sorry,U can not print the receipt for this ticket');</script>");
                }
                else
                    Response.Redirect("agentlogin.aspx");
            }
        }

        public void fixedtour(string strPnr)
        {
            clsObj = null;
            DataSet ldsRecSet = null;
            DataTable dtticketdetils = null, dtTicketnames = null, dt = null;
            StringBuilder Strname = null;
            try
            {
                clsObj = new ClsAdo();
                ldsRecSet = clsObj.fnFixTourBalancecReceiptt(strPnr);
                dtticketdetils = ldsRecSet.Tables[0];
                if (dtticketdetils.Rows.Count > 0)
                {
                    string strTourname = Convert.ToString(dtticketdetils.Rows[0]["TourName"]);
                    Strname = new StringBuilder();
                    Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100% border=1>");
                    Strname.Append("<TR><TD><span class=cgi>Transaction Id </span> </TD><TD class=cgi align=center ><span class=cgi>Transaction Date</span></TD><TD class=cgi align=center ><span class=cgi>Amount</span></TD></TR>");
                    dtTicketnames = ldsRecSet.Tables[1];

                    int tid = 0;
                    string bcode = "", userr = "";
                    for (int m = 0; m < dtTicketnames.Rows.Count; m++)
                    {
                        tid = Convert.ToInt32(dtTicketnames.Rows[m]["Transactionno"]);
                        decimal advance = Convert.ToDecimal(dtTicketnames.Rows[m]["advancepaid"].ToString());
                        bcode = Convert.ToString(dtTicketnames.Rows[m]["branchcode"].ToString());
                        userr = Convert.ToString(dtTicketnames.Rows[m]["userid"].ToString());
                        DateTime td = Convert.ToDateTime(dtTicketnames.Rows[m]["paymentdate"].ToString());
                        Strname.Append("<TR><TD><span >" + tid + " </span> </TD><TD  align=center ><span >" + td.ToShortDateString() + "</span></TD><TD  align=right ><span >" + advance + "</span></TD></TR>");
                    }
                    Strname.Append("</TABLE>");

                    dt = ldsRecSet.Tables[2];
                    string strOrder = "";
                    decimal stradvance1 = 0.0m, strtotal = 0.0m, strBalance = 0.0m;
                    if (dt.Rows.Count > 0)
                    {
                        strOrder = Convert.ToString(dt.Rows[0]["orderid"].ToString());
                        stradvance1 = Convert.ToDecimal(dt.Rows[0]["advancepaid"].ToString());
                        strtotal = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString());
                        strBalance = decimal.Round((strtotal - stradvance1), 2);
                    }
                    replacereceipt(Convert.ToString(tid), strOrder, strPnr, Convert.ToDateTime(dtTicketnames.Rows[0]["startdate"].ToString()),
                            strTourname, Strname.ToString(), stradvance1, strBalance, bcode, userr);
                }
                else
                    ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Invalid Ticket Details');</script>");
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtticketdetils != null)
                {
                    dtticketdetils.Dispose();
                    dtticketdetils = null;
                }
                if (dtTicketnames != null)
                {
                    dtTicketnames.Dispose();
                    dtTicketnames = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (Strname != null)
                {
                    Strname = null;
                }
            }
        }
        public void specialbalanceclear(string strPnr)
        {

            clsObj = null;
            DataSet ldsRecSet = null;
            DataTable dtticketdetils = null, dtTicketnames = null;
            StringBuilder Strname = null;
            try
            {
                clsObj = new ClsAdo();
                ldsRecSet = clsObj.fnSPLTourBalancecReceipt(strPnr);
                dtticketdetils = ldsRecSet.Tables[0];
                if (dtticketdetils.Rows.Count > 0)
                {
                    DateTime dtjourney = Convert.ToDateTime(dtticketdetils.Rows[0]["journeydate"].ToString());
                    Strname = new StringBuilder();
                    Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100% border=1>");
                    Strname.Append("<TR><TD><span class=cgi>Transaction Id </span> </TD><TD class=cgi align=center ><span class=cgi>Transaction Date</span></TD><TD class=cgi align=center ><span class=cgi>Amount</span></TD></TR>");
                    dtTicketnames = ldsRecSet.Tables[1];

                    int tid = 0;
                    string bcode = "", userr = "";
                    for (int m = 0; m < dtTicketnames.Rows.Count; m++)
                    {
                        tid = Convert.ToInt32(dtTicketnames.Rows[m]["Transactionno"]);
                        decimal advance = Convert.ToDecimal(dtTicketnames.Rows[m]["advancepaid"].ToString());
                        bcode = Convert.ToString(dtTicketnames.Rows[m]["branchcode"].ToString());
                        userr = Convert.ToString(dtTicketnames.Rows[m]["userid"].ToString());
                        DateTime td = Convert.ToDateTime(dtTicketnames.Rows[m]["paymentdate"].ToString());
                        Strname.Append("<TR><TD><span class=cgi>" + tid + " </span> </TD><TD class=cgi align=center ><span class=cgi>'" + td.ToShortDateString() + "'</span></TD><TD class=cgi align=center ><span class=cgi>" + advance + "</span></TD></TR>");
                    }
                    Strname.Append("</TABLE>");
                    replacereceipt(Convert.ToString(tid), Convert.ToString(tid), Convert.ToString(dtTicketnames.Rows[0]["refno"]), dtjourney,
                        Convert.ToString(dtTicketnames.Rows[0]["tourname"]), Strname.ToString(),
                        Convert.ToDecimal(dtTicketnames.Rows[0]["advTotal"]), Convert.ToDecimal(dtTicketnames.Rows[0]["balancDue"]),
                        bcode, userr);

                }
                else
                    ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Invalid Ticket Details');</script>");
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtticketdetils != null)
                {
                    dtticketdetils.Dispose();
                    dtticketdetils = null;
                }
                if (dtTicketnames != null)
                {
                    dtTicketnames.Dispose();
                    dtTicketnames = null;
                }
                if (Strname != null)
                {
                    Strname = null;
                }
            }
        }
        public void InternationalTour(string strPnr)
        {

            clsObj = null;
            DataSet ldsRecSet = null;
            DataTable dtticketdetils = null, dtTicketnames = null, dt = null;
            StringBuilder Strname = null;
            try
            {
                clsObj = new ClsAdo();
                ldsRecSet = clsObj.fnINTTourBalancecReceipt(strPnr);
                dtticketdetils = ldsRecSet.Tables[0];
                if (dtticketdetils.Rows.Count > 0)
                {
                    string strTourname = Convert.ToString(dtticketdetils.Rows[0]["TourName"]);
                    Strname = new StringBuilder();
                    Strname.Append("<TABLE id=Table3 cellSpacing=0 cellPadding=1  width=100% border=1>");
                    Strname.Append("<TR><TD><span class=cgi>Transaction Id </span> </TD><TD class=cgi align=center ><span class=cgi>Transaction Date</span></TD><TD class=cgi align=center ><span class=cgi>Amount</span></TD></TR>");
                    dtTicketnames = ldsRecSet.Tables[1];

                    int tid = 0;
                    string bcode = "", userr = "";
                    for (int m = 0; m < dtTicketnames.Rows.Count; m++)
                    {
                        tid = Convert.ToInt32(dtTicketnames.Rows[m]["Transactionno"]);
                        decimal advance = Convert.ToDecimal(dtTicketnames.Rows[m]["advancepaid"].ToString());
                        bcode = Convert.ToString(dtTicketnames.Rows[m]["branchcode"].ToString());
                        userr = Convert.ToString(dtTicketnames.Rows[m]["userid"].ToString());
                        DateTime td = Convert.ToDateTime(dtTicketnames.Rows[m]["paymentdate"].ToString());
                        Strname.Append("<TR><TD><span >" + tid + " </span> </TD><TD  align=center ><span >" + td.ToShortDateString() + "</span></TD><TD  align=right ><span >" + advance + "</span></TD></TR>");
                    }
                    Strname.Append("</TABLE>");

                    dt = ldsRecSet.Tables[2];
                    string strOrder = "";
                    decimal stradvance1 = 0.0m, strtotal = 0.0m, strBalance = 0.0m;
                    if (dt.Rows.Count > 0)
                    {
                        strOrder = Convert.ToString(dt.Rows[0]["orderid"].ToString());
                        stradvance1 = Convert.ToDecimal(dt.Rows[0]["advancepaid"].ToString());
                        strtotal = Convert.ToDecimal(dt.Rows[0]["totalamount"].ToString());
                        strBalance = decimal.Round((strtotal - stradvance1), 2);
                    }
                    replacereceipt(Convert.ToString(tid), strOrder, strPnr, Convert.ToDateTime(dtTicketnames.Rows[0]["startdate"].ToString()),
                            strTourname, Strname.ToString(), stradvance1, strBalance, bcode, userr);
                }
                else
                    ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Invalid Ticket Details');</script>");
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsRecSet != null)
                {
                    ldsRecSet.Dispose();
                    ldsRecSet = null;
                }
                if (dtticketdetils != null)
                {
                    dtticketdetils.Dispose();
                    dtticketdetils = null;
                }
                if (dtTicketnames != null)
                {
                    dtTicketnames.Dispose();
                    dtTicketnames = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (Strname != null)
                {
                    Strname = null;
                }
            }
        }
        public void replacereceipt(string lTrnID, string lPnr, string lTicketNo, DateTime lJdate, string lTourName,
            string lNoofTrans, decimal lAdv, decimal lBal, string lBCode, string lUserId)
        {
            s.Append(System.IO.File.ReadAllText(Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString()
                + "\\balanceclearance.html")));
            s = s.Replace("#Trnid", lTrnID);
            s = s.Replace("#pnr", lPnr);
            s = s.Replace("#ticket", lTicketNo);
            s = s.Replace("#jdate", lJdate.ToShortDateString());
            s = s.Replace("#tourName", lTourName);
            s = s.Replace("#Ptrans", lNoofTrans);
            s = s.Replace("#amtPaid", lAdv.ToString());
            s = s.Replace("#balDue", lBal.ToString());
            s = s.Replace("#branchCode", Convert.ToString(Session["LocalBranch"]));
            s = s.Replace("#branchUser", Convert.ToString(Session["UserId"]));
            s = s.Replace("#BranchEmployee", "Agent :");
            s.Replace("#GenerationTime", DateTime.Now.ToString("dd-MMM-yyyy hh:mm tt"));
        }
    }
}