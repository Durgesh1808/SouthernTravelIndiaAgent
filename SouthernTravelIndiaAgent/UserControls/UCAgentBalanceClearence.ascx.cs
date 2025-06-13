using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent.UserControls
{
    public partial class UCAgentBalanceClearence : System.Web.UI.UserControl
    {
        #region "Member Variable(s)"
        ClsAdo clsObj = null;
        int pvAgentID;
        string pvBranchCode, pvUserName, pvPageName;
        #endregion
        #region "Property(s)"
        public int fldAgentID
        {
            get { return pvAgentID; }
            set { pvAgentID = value; }
        }
        public string fldBranchCode
        {
            get { return pvBranchCode; }
            set { pvBranchCode = value; }
        }
        public string fldUserName
        {
            get { return pvUserName; }
            set { pvUserName = value; }
        }
        public string fldPageName
        {
            get { return pvPageName; }
            set { pvPageName = value; }
        }
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {

            Submit.Attributes.Add("onclick", "return search();");
            btnpay.Attributes.Add("onclick", "return validation();");

        }
        protected void Submit_Click(object sender, EventArgs e)
        {

            clsObj = null;
            DataTable dtpaymodes = null;
            try
            {
                clsObj = new ClsAdo();
                if ((txtticketno.Value != "") && (txtticketno.Value.Trim().Length > 3))
                {
                    string ticketno = Convert.ToString(txtticketno.Value);
                    string start = ticketno.Substring(0, 3);
                    string k = start.ToUpper();
                    start = k;
                    clear();
                    trExceeding.Visible = false;

                    if (start == "SPL")
                    {
                        trExceeding.Visible = true;
                        special();
                    }
                    else if (start == "EBK")
                    {
                        fixedtour();
                    }
                    else if (start == "INT")
                    {
                        International();
                    }
                    else
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "invalid", "<script>alert('Please Enter valid Ticketno');</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Please Enter valid Ticket Number.');", true);
                    }
                }
                else
                {
                    clear();
                    btnpay.Enabled = false;
                    //ClientScript.RegisterStartupScript(GetType(), "Warning", "<script>alert('Please Enter Ticket Number');</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Please Enter Ticket Number.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dtpaymodes != null)
                {
                    dtpaymodes.Dispose();
                    dtpaymodes = null;
                }
            }
            txtticketno.Attributes.Add("readonly", "readonly");
        }
        protected void btnpay_Click(object sender, EventArgs e)
        {
            if ((txtticketno.Value != "") && (txtticketno.Value.Trim().Length > 3))
            {
                string ticketno = Convert.ToString(txtticketno.Value);
                string start = ticketno.Substring(0, 3);
                string k = start.ToUpper();
                start = k;
                if (start == "SPL")
                {
                    specialbalanceclear();
                }
                else if (start == "EBK")
                {
                    if ((Convert.ToDecimal(txtbalancetill.Value) < Convert.ToDecimal(txtbalancepaidnow.Value)))
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "exc1", "<script>alert('Amount Paid is greater than Balance Amount');</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Amount Paid is greater than Balance Amount.');", true);
                    }
                    else
                    {
                        fixedbalanceclear();
                    }
                }
                else if (start == "INT")
                {
                    if ((Convert.ToDecimal(txtbalancetill.Value) < Convert.ToDecimal(txtbalancepaidnow.Value)))
                    {
                        //ClientScript.RegisterStartupScript(GetType(), "exc1", "<script>alert('Amount Paid is greater than Balance Amount');</script>");
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Amount Paid is greater than Balance Amount.');", true);
                    }
                    else
                    {
                        Interbalanceclear();
                    }
                }
                //this.RegisterStartupScript("payment", "<script>alert('Payment submitted successfully.');window.location.href='BranchBalanceClearence.aspx';</script>");
                //ScriptManager.RegisterStartupScript(this, typeof(Page), "UniqueID", "alert('Payment submitted successfully.')", true);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Payment submitted successfully.');window.location.href='" + fldPageName + "';", true);

                clear();
                btnpay.Enabled = true;

            }
            else
            {
                clear();
                btnpay.Enabled = false;
                //ClientScript.RegisterStartupScript(GetType(), "Warning", "<script>alert('Please Enter Proper Ticket Number');</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Please Enter Proper Ticket Number.');", true);
            }
        }
        protected void btnPrint_Click(object sender, EventArgs e)
        {

        }
        protected void btnreset_Click(object sender, EventArgs e)
        {
            clear();
            txtticketno.Value = "";
            txtticketno.Attributes.Remove("readonly");
        }
        #endregion
        #region "Method(s)"
        public void special()
        {

            clsObj = null;
            DataSet ldsRec = null;
            DataTable dtticketdetils = null, dtgrid = null;
            try
            {
                clsObj = new ClsAdo();
                ldsRec = clsObj.fnAllTourBalAgent(DataLib.funClear(txtticketno.Value), Convert.ToInt32(fldAgentID), "SplFare");
                dtticketdetils = ldsRec.Tables[0];
                if (dtticketdetils.Rows.Count > 0)
                {

                    txttourname.Text = Convert.ToString(dtticketdetils.Rows[0]["TourName"]);
                    txtgroupleadername.Value = Convert.ToString(dtticketdetils.Rows[0]["CustomerName"]);
                    txtamount.Value = Convert.ToString(dtticketdetils.Rows[0]["TotalAmount"]);
                    txtTax.Value = Convert.ToString(dtticketdetils.Rows[0]["STaxValue"]);
                    //txtdiscount.Value = Convert.ToString(dtticketdetils.Rows[0]["Discount"]);
                    txtdiscount.Value = "0";
                    //txttotalwithtax.Value = Convert.ToString(dtticketdetils.Rows[0]["AmtWithTax"]);
                    txttotalwithtax.Value = Convert.ToString(Convert.ToDecimal(txtamount.Value) + Convert.ToDecimal(txtTax.Value));
                    txtamountpaidtill.Value = Convert.ToString(dtticketdetils.Rows[0]["Advance"]);
                    txtbalancetill.Value = Convert.ToString(Convert.ToDecimal(txttotalwithtax.Value) - Convert.ToDecimal(txtamountpaidtill.Value));

                    dtgrid = ldsRec.Tables[1];
                    dgpaymentdetails.DataSource = dtgrid;
                    dgpaymentdetails.DataBind();
                    btnpay.Enabled = true;

                }
                else
                {
                    clear();
                    btnpay.Enabled = false;
                    //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Invalid Ticket Details');</script>");

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Invalid Ticket Details.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsRec != null)
                {
                    ldsRec.Dispose();
                    ldsRec = null;
                }
                if (dtticketdetils != null)
                {
                    dtticketdetils.Dispose();
                    dtticketdetils = null;
                }
                if (dtgrid != null)
                {
                    dtgrid.Dispose();
                    dtgrid = null;
                }
            }
        }
        public void fixedtour()
        {

            clsObj = null;
            DataSet ldsRec = null;
            DataTable dtticketdetils = null, ldtHierarchyCanChr = null, dtgrid = null;
            try
            {
                clsObj = new ClsAdo();
                ldsRec = clsObj.fnAllTourBalAgent(DataLib.funClear(txtticketno.Value), Convert.ToInt32(fldAgentID), "FixFare");
                dtticketdetils = ldsRec.Tables[0];
                if (dtticketdetils.Rows.Count > 0)
                {
                    //txtPrev.Value = "0.00";
                    txttourname.Text = Convert.ToString(dtticketdetils.Rows[0]["TourName"]);
                    txtgroupleadername.Value = Convert.ToString(dtticketdetils.Rows[0]["CustomerName"]);
                    txtamount.Value = Convert.ToString(dtticketdetils.Rows[0]["Totalamount"]);
                    txtTax.Value = Convert.ToString(dtticketdetils.Rows[0]["STaxValue"]);
                    txtdiscount.Value = Convert.ToString(dtticketdetils.Rows[0]["Discount"]);
                    txttotalwithtax.Value = Convert.ToString(dtticketdetils.Rows[0]["AmtWithTax"]);
                    txtamountpaidtill.Value = Convert.ToString(dtticketdetils.Compute("Sum(Advance)", ""));//.Rows[0]["Advance"]);
                    if ((Convert.ToDecimal(txttotalwithtax.Value) - Convert.ToDecimal(txtamountpaidtill.Value)) > 0)
                    {
                        decimal pTotCanCharges = 0;
                        int? lHierarchyCounter = 0;
                        ldtHierarchyCanChr = clsObj.fnGetCancelTktHierarchy(Convert.ToString(txtticketno.Value),
                              ref lHierarchyCounter);
                        if (ldtHierarchyCanChr != null && ldtHierarchyCanChr.Rows.Count > 1)
                        {
                            pTotCanCharges = decimal.Round(Convert.ToDecimal(ldtHierarchyCanChr.Compute("Sum(TktCanCharge)", "RowID >0").ToString()));
                            if (lHierarchyCounter > 1)
                            {
                                txtPrev.Value = Convert.ToString(pTotCanCharges);
                                txtbalancetill.Value = Convert.ToString(Convert.ToDecimal(txttotalwithtax.Value) - Convert.ToDecimal(txtamountpaidtill.Value) + Convert.ToDecimal(txtPrev.Value));
                            }
                        }
                        else
                        {
                            txtbalancetill.Value = Convert.ToString(Convert.ToDecimal(txttotalwithtax.Value) - Convert.ToDecimal(txtamountpaidtill.Value));
                        }
                    }
                    else
                    {
                        txtbalancetill.Value = Convert.ToString(Convert.ToDecimal(txttotalwithtax.Value) - Convert.ToDecimal(txtamountpaidtill.Value));
                    }

                    dtgrid = ldsRec.Tables[1];
                    dgpaymentdetails.DataSource = dtgrid;
                    dgpaymentdetails.DataBind();
                    btnpay.Enabled = true;
                }
                else
                {
                    clear();
                    btnpay.Enabled = false;
                    //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Invalid Ticket Details');</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Invalid Ticket Details.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsRec != null)
                {
                    ldsRec.Dispose();
                    ldsRec = null;
                }
                if (dtticketdetils != null)
                {
                    dtticketdetils.Dispose();
                    dtticketdetils = null;
                }
                if (ldtHierarchyCanChr != null)
                {
                    ldtHierarchyCanChr.Dispose();
                    ldtHierarchyCanChr = null;
                }
                if (dtgrid != null)
                {
                    dtgrid.Dispose();
                    dtgrid = null;
                }
            }
        }
        public void International()
        {

            clsObj = null;
            DataSet ldsRec = null;
            DataTable dtticketdetils = null, dtgrid = null;
            try
            {
                clsObj = new ClsAdo();
                ldsRec = clsObj.fnInternationalTourBal(DataLib.funClear(txtticketno.Value));
                dtticketdetils = ldsRec.Tables[0];
                if (dtticketdetils.Rows.Count > 0)
                {
                    txttourname.Text = Convert.ToString(dtticketdetils.Rows[0]["TourName"]);
                    txtgroupleadername.Value = Convert.ToString(dtticketdetils.Rows[0]["GroupleaderName"]);
                    txtamount.Value = Convert.ToString(dtticketdetils.Rows[0]["TicketAmount"]);
                    txtTax.Value = Convert.ToString(dtticketdetils.Rows[0]["ServiceTax"]);
                    txtdiscount.Value = Convert.ToString(dtticketdetils.Rows[0]["Discountamount"]);
                    txttotalwithtax.Value = Convert.ToString(dtticketdetils.Rows[0]["TotalAmount"]);
                    txtamountpaidtill.Value = Convert.ToString(dtticketdetils.Rows[0]["advanceamount"]);
                    txtbalancetill.Value = Convert.ToString(Convert.ToDecimal(txttotalwithtax.Value) - Convert.ToDecimal(txtamountpaidtill.Value));

                    dtgrid = ldsRec.Tables[1];
                    dgpaymentdetails.DataSource = dtgrid;
                    dgpaymentdetails.DataBind();
                    btnpay.Enabled = true;
                }
                else
                {
                    clear();
                    btnpay.Enabled = false;
                    //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Invalid Ticket Details');</script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "invalid", "alert('Invalid Ticket Details.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsRec != null)
                {
                    ldsRec.Dispose();
                    ldsRec = null;
                }
                if (dtticketdetils != null)
                {
                    dtticketdetils.Dispose();
                    dtticketdetils = null;
                }
                if (dtgrid != null)
                {
                    dtgrid.Dispose();
                    dtgrid = null;
                }
            }
        }
        public void specialbalanceclear()
        {

            clsObj = null;
            try
            {

                clsObj = new ClsAdo();
                decimal? pBalance = 0;
                string lBalance = "0";

                DataTable dt1 = clsObj.fnGetAgentPayDetail("0", Convert.ToInt32(fldAgentID), ref lBalance);
                pBalance = Convert.ToDecimal(lBalance);
                decimal cre = decimal.Round(Convert.ToDecimal(txtamountpaidtill.Value) + Convert.ToDecimal(txtbalancepaidnow.Value));
                if (Convert.ToDecimal(txtbalancepaidnow.Value) <= pBalance)
                {
                    if (float.Parse(txtExceedingAmt.Value) > 0)
                    {
                        int val = clsObj.fnInsertUpdateAgentBalanceClearence("SplFare", DataLib.funClear(txtticketno.Value), Convert.ToString(fldBranchCode),
                        Convert.ToString(fldUserName), Convert.ToDecimal(txttotalwithtax.Value), Convert.ToDecimal(txtExceedingAmt.Value),
                        txttourname.Text, "", "", "", "", Convert.ToDecimal(cre), 13, 0, decimal.Round(Convert.ToDecimal(txtbalancepaidnow.Value)));
                    }
                    else
                    {
                        int val = clsObj.fnInsertUpdateAgentBalanceClearence("SplFare", DataLib.funClear(txtticketno.Value), Convert.ToString(fldBranchCode),
                        Convert.ToString(fldUserName), Convert.ToDecimal(txttotalwithtax.Value), Convert.ToDecimal(txtbalancepaidnow.Value),
                        txttourname.Text, "", "", "", "", Convert.ToDecimal(cre), 13, 0, decimal.Round(Convert.ToDecimal(txtbalancepaidnow.Value)));
                    }
                    clear();
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Insufficient funds.');</script>");
                    //RegisterStartupScript("Error", "<Script>alert('Insufficient funds');</Script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Insufficient funds.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
        public void fixedbalanceclear()
        {

            clsObj = null;

            try
            {
                clsObj = new ClsAdo();

                decimal? pBalance = 0;
                string lBalance = "0";

                DataTable dt1 = clsObj.fnGetAgentPayDetail("0", Convert.ToInt32(fldAgentID), ref lBalance);
                pBalance = Convert.ToDecimal(lBalance);

                decimal cre = decimal.Round(Convert.ToDecimal(txtamountpaidtill.Value) + Convert.ToDecimal(txtbalancepaidnow.Value));
                if (Convert.ToDecimal(txtbalancepaidnow.Value) <= pBalance)
                {
                    int val = clsObj.fnInsertUpdateAgentBalanceClearence("FixFare", DataLib.funClear(txtticketno.Value), Convert.ToString(fldBranchCode),
                        Convert.ToString(fldUserName), Convert.ToDecimal(txttotalwithtax.Value), Convert.ToDecimal(txtbalancepaidnow.Value),
                        txttourname.Text, "", "", "", "", Convert.ToDecimal(cre), 6, 0, Math.Round(Convert.ToDecimal(txtbalancepaidnow.Value), 0, MidpointRounding.AwayFromZero));
                    clear();
                }
                else
                {
                    //ClientScript.RegisterStartupScript(GetType(), "Error", "<script>alert('Insufficient funds.');</script>");
                    //RegisterStartupScript("Error", "<Script>alert('Insufficient funds');</Script>");
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Insufficient funds.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
        public void Interbalanceclear()
        {

            clsObj = null;

            try
            {
                clsObj = new ClsAdo();

                decimal? pBalance = 0;
                string lBalance = "0";

                DataTable dt1 = clsObj.fnGetAgentPayDetail("0", Convert.ToInt32(fldAgentID), ref lBalance);
                pBalance = Convert.ToDecimal(lBalance);

                decimal cre = decimal.Round(Convert.ToDecimal(txtamountpaidtill.Value) + Convert.ToDecimal(txtbalancepaidnow.Value));
                if (Convert.ToDecimal(txtbalancepaidnow.Value) <= pBalance)
                {
                    int val = clsObj.fnInsertUpdateAgentBalanceClearence("InterFare", DataLib.funClear(txtticketno.Value), Convert.ToString(fldBranchCode),
                        Convert.ToString(fldUserName), Convert.ToDecimal(txttotalwithtax.Value), Convert.ToDecimal(txtbalancepaidnow.Value),
                        txttourname.Text, "", "", "", "", Convert.ToDecimal(cre), 6, 0, Math.Round(Convert.ToDecimal(txtbalancepaidnow.Value), 0, MidpointRounding.AwayFromZero));
                    clear();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alrtmsg", "alert('Insufficient funds.');", true);
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
        public void clear()
        {
            txttourname.Text = "";
            txtgroupleadername.Value = "";
            //txtAdults.Value = "";
            //txtchilds.Value = "";
            txtamount.Value = "";
            txtTax.Value = "";
            txtdiscount.Value = "";
            txttotalwithtax.Value = "";
            txtamountpaidtill.Value = "";
            txtbalancetill.Value = "";
            txtbalancepaidnow.Value = "0";
            txtbalancepending.Value = "";
            txtExceedingAmt.Value = "0";


            dgpaymentdetails.DataSource = null;
            dgpaymentdetails.DataBind();

            //

        }
        #endregion
    }
}