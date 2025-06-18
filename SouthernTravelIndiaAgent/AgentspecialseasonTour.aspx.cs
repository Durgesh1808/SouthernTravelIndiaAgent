using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentspecialseasonTour : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        protected decimal totalAmount = 0, stax1;
        protected string strTourId, endecript = System.Configuration.ConfigurationManager.AppSettings["ENCRY"].ToString();
        DateTime pvJourneyDate = DateTime.Now;
        char[] pvSeatSplitter = { '-' };
        ClsAdo pClsObj = null;
        STSPLOrOther pvClsSpl = null;
        #endregion
        #region "Property(s)"
        /// <summary>
        /// Get or Set Journey Date
        /// </summary>
        public DateTime fldJourneyDate
        {
            get
            {
                if (txtDate.Value.Trim() != string.Empty)
                {
                    string[] DateArr3 = new string[3];
                    char[] splitter1 = { '/' };
                    DateArr3 = txtDate.Value.Split(splitter1);
                    pvJourneyDate = new DateTime(Convert.ToInt32(DateArr3[2]), Convert.ToInt32(DateArr3[1]),
                        Convert.ToInt32(DateArr3[0]));
                }
                else
                {
                    pvJourneyDate = DateTime.Now;
                }
                return pvJourneyDate;
            }
            set
            {
                pvJourneyDate = value;
            }
        }
        public int IsChildPaxAvailable
        {
            get { return Convert.ToInt32(this.ViewState["IsChildPaxAvailable"]); }
            set { this.ViewState["IsChildPaxAvailable"] = value; }
        }
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
            {
                //Response.Redirect("sessionexpire.aspx");
                Response.Redirect("agentlogin.aspx");
            }
            if (!IsPostBack)
            {
                chkPromotions.Checked = true;
                FillTourCar();
                BindCountryNameNew(-1);
                BindStateName_New();
                // BindStateName();
                //txtServiceTax.Value = Convert.ToString(decimal.Round(Convert.ToDecimal(DataLib.GetserviceTax("TP")), 2));
                txtCCVal.Value = Convert.ToString(decimal.Round(Convert.ToDecimal(DataLib.GetserviceTax("CC")), 2));
                this.txtSinglePax.Attributes.Add("onkeypress", "return chkNumeric(event);");
                this.Submit.Attributes.Add("onclick", "javascript:return Validationcheck();");
                this.txtMobile.Attributes.Add("onkeypress", "return chkNumeric(event);");
                this.txtAlternateMobile.Attributes.Add("onkeypress", "return chkNumeric(event);");
                this.txtphone.Attributes.Add("onkeypress", "return chkNumeric(event);");
                this.txtAadharNo.Attributes.Add("onkeypress", "return chkNumeric(event);");
                this.txtDate.Attributes.Add("onkeypress", "javascript:return keyboardlock();");
                this.txtSinglePax.Attributes.Add("onkeydown", "javascript:return fnchkSingle();");

                txtChildWithMatress.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");
                txtChildWhoutMatress.Attributes.Add("onKeyPress", "javascript:return isNumberKey(event);");

                //this.txtSinglePax.Attributes.Add("onkeydown", "javascript:return fnchkSingle();");
                int tourid = Convert.ToInt32(Request.QueryString["tourid"].ToString());
                strTourId = Convert.ToString(tourid);
                tid.Value = Convert.ToString(tourid);

                if (strTourId.ToString() == "63" || strTourId.ToString() == "91")
                    rdoStandard.Disabled = true;
                if (strTourId.ToString() == "64" ||
                    strTourId.ToString() == "92")
                    rdoDeluxe.Disabled = true;
                #region Optimize Code
                /*string strTour = "select tourname,tourcode,duration,City,DeptTime from Spl_TourMaster(nolock) where tourid=@strTourId ";
                DataTable strTourName = new DataTable();
                SqlParameter[] para = new SqlParameter[1];
                para[0] = new SqlParameter("@strTourId", DataLib.funClear(strTourId));
                strTourName = DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, strTour, para);*/
                #endregion
                DataTable strTourName = null;
                pvClsSpl = null;
                try
                {
                    pvClsSpl = new STSPLOrOther();
                    strTourName = pvClsSpl.fnGetSPLTourMaster(Convert.ToInt32(strTourId));
                    if (strTourName.Rows.Count > 0)
                    {
                        txttourName.Text = Convert.ToString(strTourName.Rows[0]["tourname"].ToString())
                            + "/" + Convert.ToString(strTourName.Rows[0]["duration"].ToString());
                        etourname.Value = txttourName.Text;
                        // txttourName.ReadOnly = true;
                        //lblTourCode.Text = Convert.ToString(strTourName.Rows[0]["tourCode"].ToString());
                        Session["TravelSector"] = Convert.ToString(strTourName.Rows[0]["tourCode"].ToString());
                        lblPkPoint.Text = Convert.ToString(strTourName.Rows[0]["City"].ToString());
                        depttime.Value = Convert.ToString(strTourName.Rows[0]["DeptTime"].ToString());
                        if (strTourName.Rows[0]["IsNonAccommodation"] != null && Convert.ToBoolean(strTourName.Rows[0]["IsNonAccommodation"]) == true)
                        {
                            txtServiceTax.Value = Convert.ToString(decimal.Round(Convert.ToDecimal(DataLib.GetserviceTax("SPLNA")), 2));
                        }
                        else
                        {
                            txtServiceTax.Value = Convert.ToString(decimal.Round(Convert.ToDecimal(DataLib.GetserviceTax("SPLA")), 2));
                        }
                    }
                }
                finally
                {
                    if (pvClsSpl != null)
                    {
                        pvClsSpl = null;
                    }
                    if (strTourName != null)
                    {
                        strTourName.Dispose();
                    }
                }
                ViewState["tourno"] = tourid;
                GetTourFare(tourid, fldJourneyDate, -1, -1);
                // for MD5 Password Purpose Start
                Random r = new Random();
                string password = r.Next().ToString();
                if (password.Length > 6)
                {
                    password = password.Remove(6);
                }
                ViewState["Spass"] = password.Trim().ToString();
                ClientScript.RegisterStartupScript(typeof(string), "stsrtupSend8", "<script>fnMd5('" + ViewState["Spass"].ToString() + "');</script>");
                password = tmpEnValue.Value;
                // End For MD5 Password
            }
            {
                strTourId = tid.Value;
                ViewState["strPass"] = tmpEnValue.Value;
            }
            SpecialTourFarePanelUC.fldTourID = Convert.ToInt32(strTourId);

            if (txtDate.Value != "")
            {
                string[] pJourneyDate = txtDate.Value.Split('/');

                DateTime lJourneyDate = new DateTime(Convert.ToInt32(pJourneyDate[2]), Convert.ToInt32(pJourneyDate[1]),
                           Convert.ToInt32(pJourneyDate[0]));
                DateTime lBlockDateFrom = new DateTime(2012, 12, 22);
                DateTime lBlockDateTo = new DateTime(2013, 1, 5);
                rdoStandard.Disabled = false;
                if (Convert.ToDateTime(Convert.ToString(lJourneyDate)) >= lBlockDateFrom && Convert.ToDateTime(Convert.ToString(lJourneyDate)) <= lBlockDateTo)
                {
                    if (rdoStandard.Disabled == false)
                    {
                        rdoStandard.Disabled = true;
                        //this.RegisterStartupScript("Error2", "<Script>alert('Standard Tour is unavailable between 12-Dec-2012 to 5-Jan-2013.');</Script>");
                    }
                }
                GetChildPaxCount(Convert.ToInt32(Request.QueryString["TourID"]), fldJourneyDate); // to hide or show child pax
            }
            MangeChildFaresection();
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            if (IsValidateChildPax())
            {
                if (ValidateFare())
                {
                    if (txtDate.Value != "")
                    {
                        string[] pJourneyDate = txtDate.Value.Split('/');

                        DateTime lJourneyDate = new DateTime(Convert.ToInt32(pJourneyDate[2]), Convert.ToInt32(pJourneyDate[1]),
                                   Convert.ToInt32(pJourneyDate[0]));
                        DateTime lBlockDateFrom = new DateTime(2012, 12, 22);
                        DateTime lBlockDateTo = new DateTime(2013, 1, 5);
                        if (Convert.ToDateTime(Convert.ToString(lJourneyDate)) >= lBlockDateFrom && Convert.ToDateTime(Convert.ToString(lJourneyDate)) <= lBlockDateTo)
                        {
                            if (rdoStandard.Disabled == false)
                            {
                                rdoStandard.Disabled = true;
                                //this.RegisterStartupScript("Error2", "<Script>alert('Standard Tour is unavailable between 12-Dec-2012 to 5-Jan-2013.');</Script>");
                                return;
                            }
                        }
                    }

                    //return;

                    string ip = Request.ServerVariables["REMOTE_ADDR"];
                    Session["splTour"] = "splTours";

                    //totalAmount = decimal.Round(Convert.ToDecimal(txtTotalFare.Value));

                    string lSelectedPax = ddlNoOfPax.SelectedValue;
                    int totalPax = 0;
                    if (Convert.ToInt32(ddlNoOfPax.SelectedItem.Text) < Convert.ToInt32(lSelectedPax.Split(pvSeatSplitter)[1]))
                    {
                        totalPax = Convert.ToInt32(lSelectedPax.Split(pvSeatSplitter)[1]);
                    }
                    else
                    {
                        totalPax = Convert.ToInt32(ddlNoOfPax.SelectedItem.Text);
                    }
                    if (IsChildPaxAvailable == 1)
                    {
                        totalPax = totalPax + ConvertStringint(hdfTotalChildWithMatress.Value) + ConvertStringint(hdfTotalChildWithoutMatress.Value);
                    }
                    string strMobile = txtMobile.Text.Replace("'", "''").Trim();
                    string lAlternateMBLNO = txtAlternateMobile.Text.Replace("'", "''").Trim();
                    string strPhone = txtphone.Text.Replace("'", "''").Trim();
                    txtTotalExtrafare.Text = (Request[this.txtTotalExtrafare.UniqueID]);
                    //string carPax = "", carPax1 = "", InnPax = "", InnPax1 = "", QuaPax = "", QuaPax1 = "", TempPax = "";
                    //string vType = "", vtype1 = "", vType2 = "", vType3 = "";
                    int category = 0;
                    if (rdoDeluxe.Checked == true)
                        category = 2;
                    else if (rdoStandard.Checked == true)
                        category = 1;
                    else if (rdoLuxury.Checked == true)
                        category = 7;
                    // Start Add For New Sub Category Ex. CP/MAP/AP Geting Fare. if remove this please comment Start to End Code. (Add By Santosh)
                    if (rdoStandard.Checked)
                    {
                        if (ddlSubCategory.SelectedValue == "0")
                        {
                            category = 3;
                        }
                        else if (ddlSubCategory.SelectedValue == "1")
                        {
                            category = 4;
                        }
                    }
                    else if (rdoDeluxe.Checked)
                    {
                        if (ddlSubCategory.SelectedValue == "0")
                        {
                            category = 5;
                        }
                        else if (ddlSubCategory.SelectedValue == "1")
                        {
                            category = 6;
                        }
                    }
                    else if (rdoLuxury.Checked)
                    {
                        if (ddlSubCategory.SelectedValue == "0")
                        {
                            category = 8;
                        }
                        else if (ddlSubCategory.SelectedValue == "1")
                        {
                            category = 9;
                        }
                    }
                    // End
                    string pickupveh = "", strFlight = "", strPktime = "", strFlightNo = "", strPKstation = "";
                    string dropveh = "", strDFlight = "", strDPktime = "", strDFlightNo = "";
                    if (RadFlight.Checked == true)
                    {
                        strFlight = txtpickVehicleNo.SelectedItem.Text.ToString();
                        pickupveh = "Flight";
                        strPKstation = txtpickVehicleNo.SelectedItem.Text.ToString();
                        strPktime = ddlPkHrs.Value + ":" + ddlPkMints.Value;
                        strFlightNo = DataLib.funClear(txtFlightNo.Text);
                    }
                    else if (RadTrain.Checked == true)
                    {
                        pickupveh = "Train";
                        strPKstation = DataLib.funClear(txtRlyStationName.Text);
                        strFlight = DataLib.funClear(txtRlyStationName.Text);
                        strPktime = ddlTrainPkHr.Value + ":" + ddlTrainPkMin.Value;
                        strFlightNo = DataLib.funClear(txtTrainNo.Text);
                    }
                    else if (RadBus.Checked == true)
                    {
                        pickupveh = "Bus";
                        strPKstation = DataLib.funClear(txtAddr.Text);
                        strFlightNo = DataLib.funClear(txtStreet.Text);
                    }
                    if (RadFlight_d.Checked == true)
                    {
                        dropveh = "Flight";
                        strDFlight = txtpickVehicleNo_d.SelectedItem.Text.ToString();
                        strDPktime = ddlPkHrs_d.Value + ":" + ddlPkMints_d.Value;
                        strDFlightNo = DataLib.funClear(txtFlightNo_d.Text);
                    }
                    else if (RadTrain_d.Checked == true)
                    {
                        dropveh = "Train";
                        strDFlight = DataLib.funClear(txtRlyStationName_d.Text);
                        strDPktime = ddlTrainPkHr_d.Value + ":" + ddlTrainPkMin_d.Value;
                        strDFlightNo = DataLib.funClear(txtTrainNo_d.Text);
                    }
                    else if (RadBus_d.Checked == true)
                    {
                        dropveh = "Bus";
                        strDFlight = DataLib.funClear(txtAddr_d.Text);
                        strDFlightNo = DataLib.funClear(txtStreet_d.Text);
                    }

                    string strPaxdetails = totalPax.ToString();
                    string strVihcle = ddlCarType.SelectedValue.ToString();

                    #region Optimize Code
                    /* string strTourDetails = "select tourname from Spl_TourMaster(nolock) where tourid=" +Convert.ToInt32(ViewState["tourno"]) + "";
                    string strTourName2 = DataLib.GetStringData(DataLib.Connection.ConnectionString, strTourDetails);
                    string strTourName2 = pClsObj.fnGetSPLTourName(Convert.ToInt32(ViewState["tourno"]));*/
                    #endregion

                    Session["billingAddress"] = DataLib.funClear(txtAddr.Text);
                    if (txtMobile.Text != null && txtMobile.Text != "")
                        Session["mobile"] = DataLib.funClear(txtMobile.Text);
                    else
                        Session["mobile"] = DataLib.funClear(txtphone.Text);
                    // Code by jaggu after changes on end user special tour--------------
                    string strSinglePax = "";
                    decimal extraFair = 0;
                    if (Convert.ToString(txtSinglePax.Text) != null && Convert.ToString(txtSinglePax.Text).ToString() != "" &&
                        Convert.ToString(txtSinglePax.Text).ToString() != "0")
                    {
                        strSinglePax = DataLib.funClear(txtSinglePax.Text);
                        extraFair = Convert.ToDecimal(txtTotalExtrafare.Text);
                    }
                    else
                    {
                        strSinglePax = "";
                        extraFair = 0;
                    }
                    if (RadFlight.Checked == true)
                    {
                        strFlight = txtpickVehicleNo.SelectedItem.Text.ToString();
                        pickupveh = "Flight";
                        strPKstation = txtpickVehicleNo.SelectedItem.Text.ToString();
                        strPktime = ddlPkHrs.Value + ":" + ddlPkMints.Value;
                        strFlightNo = DataLib.funClear(txtFlightNo.Text);
                    }
                    else if (RadTrain.Checked == true)
                    {
                        pickupveh = "Train";
                        strPKstation = DataLib.funClear(txtRlyStationName.Text);
                        strFlight = DataLib.funClear(txtRlyStationName.Text);
                        strPktime = ddlTrainPkHr.Value + ":" + ddlTrainPkMin.Value;
                        strFlightNo = DataLib.funClear(txtTrainNo.Text);
                    }
                    else if (RadBus.Checked == true)
                    {
                        pickupveh = "Bus";
                        strPKstation = DataLib.funClear(txtAddr.Text);
                        strFlightNo = DataLib.funClear(txtStreet.Text);
                    }
                    if (RadFlight_d.Checked == true)
                    {
                        dropveh = "Flight";
                        strDFlight = txtpickVehicleNo_d.SelectedItem.Text.ToString();
                        strDPktime = ddlPkHrs_d.Value + ":" + ddlPkMints_d.Value;
                        strDFlightNo = DataLib.funClear(txtFlightNo_d.Text);
                    }
                    else if (RadTrain_d.Checked == true)
                    {
                        dropveh = "Train";
                        strDFlight = DataLib.funClear(txtRlyStationName_d.Text);
                        strDPktime = ddlTrainPkHr_d.Value + ":" + ddlTrainPkMin_d.Value;
                        strDFlightNo = DataLib.funClear(txtTrainNo_d.Text);
                    }
                    else if (RadBus_d.Checked == true)
                    {
                        dropveh = "Bus";
                        strDFlight = DataLib.funClear(txtAddr_d.Text);
                        strDFlightNo = DataLib.funClear(txtStreet_d.Text);
                    }
                    if (Convert.ToString(txtSinglePax.Text) != null && Convert.ToString(txtSinglePax.Text).ToString() != ""
                        && Convert.ToString(txtSinglePax.Text).ToString() != "0")
                    {
                        strSinglePax = DataLib.funClear(txtSinglePax.Text);
                        extraFair = Convert.ToDecimal(txtTotalExtrafare.Text);
                    }
                    else
                    {
                        strSinglePax = "";
                        extraFair = 0;
                    }
                    string strfname = ddlTitle.SelectedItem.Text + ' ' + txtFName.Text;
                    string strComments = DataLib.funClear(txtComment.Text);
                    if (strComments == null)
                    {
                        strComments = "";
                    }

                    int tourid = Convert.ToInt32(ViewState["tourno"]);

                    #region Optimize Code
                    /*string ins = "Select InstentBooking from spl_tourmaster where tourid=" + tourid + "";
                string instent = DataLib.GetStringData(DataLib.Connection.ConnectionString, ins);*/
                    #endregion

                    pvClsSpl = null;
                    pvClsSpl = new STSPLOrOther();
                    string instent = "";
                    try
                    {
                        instent = pvClsSpl.fnGetInstentBooking(tourid);
                    }
                    finally
                    {
                        if (pvClsSpl != null)
                        {
                            pvClsSpl = null;
                        }
                    }
                    double noofhours;
                    if (instent == "N")
                    {
                        TimeSpan hr;
                        DateTime jdate = mmddyy2ddmmyy(txtDate.Value);
                        DateTime tdate = System.DateTime.Now;
                        hr = jdate.Subtract(tdate);
                        noofhours = hr.TotalHours;
                    }
                    else
                    {
                        noofhours = 200;
                    }
                    if ((noofhours > 24) && (totalPax <= 18))
                    {
                        string[] JourneyDate = txtDate.Value.Split('/');

                        DateTime strJourneyDate = new DateTime(Convert.ToInt32(JourneyDate[2]), Convert.ToInt32(JourneyDate[1]),
                                   Convert.ToInt32(JourneyDate[0]));

                        /*string strJourneyDate = txtDate.Value.Split('/').GetValue(1) + "/" + txtDate.Value.Split('/').GetValue(0) + "/" + 
                          txtDate.Value.Split('/').GetValue(2);*/

                        #region Optimize Code
                        /*string qry, qry1;
                    qry = "select isnull(max(RowId),0) as RowId from OnlineTransactionTable(nolock) where agentid=" + Session["AgentId"];
                    string strRowNo = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
                    if (strRowNo == "" || strRowNo == null)
                    {
                    }
                    else
                    {
                        qry1 = "select isnull(availablebalance,0) as availablebalance from OnlineTransactionTable(nolock) where agentid=" + Session["AgentId"] + " and rowid=" + strRowNo;
                        string balance = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry1);
                        if (balance == "")
                            Session["Balance"] = "0";
                        else
                            Session["Balance"] = balance;
                    }*/
                        #endregion

                        decimal? pBalance = 0;
                        string lBalance = "0";
                        decimal pAdvance = 0;
                        pClsObj = null;
                        DataTable dt1 = null;
                        try
                        {
                            pClsObj = new ClsAdo();
                            dt1 = pClsObj.fnGetAgentPayDetail("",
                            Convert.ToInt32(Session["AgentId"]), ref lBalance);
                            //pAdvance = Convert.ToDecimal(dt1.Rows[0]["AdvancePaid"].ToString());
                        }
                        finally
                        {
                            if (pClsObj != null)
                            {
                                pClsObj = null;
                            }
                            if (dt1 != null)
                            {
                                dt1.Dispose();
                                dt1 = null;
                            }
                        }
                        pBalance = Convert.ToDecimal(lBalance);
                        if (pBalance == 0)
                            Session["Balance"] = "0";
                        else
                            Session["Balance"] = pBalance;

                        #region Optimize Code
                        /* string comtds = "select TaxValue from ServiceTaxMaster where TaxType='TDS'";
                    decimal tds = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, comtds));
                    string st = "select TaxValue from ServiceTaxMaster where TaxType='TP'";
                    decimal stax = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, st));*/
                        #endregion

                        decimal tds = Convert.ToDecimal(DataLib.GetserviceTax("TDS"));
                        decimal stax = Convert.ToDecimal(DataLib.GetserviceTax("TP"));

                        int agentid = Convert.ToInt32(Session["AgentId"]);

                        #region Optimize Code
                        /*string agentlevel = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select agentlevel from agent_agentdetails where agentid=" + agentid);
                    decimal comission = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, "
                     * select agentcomission" + agentlevel.Replace("''", "").ToString() + " from Spl_TourMaster where tourid=" + tourid));

                    decimal comission = Convert.ToDecimal(pClsObj.fnGetAgentLavelComm(Convert.ToInt32(Session["AgentId"]), Convert.ToString(TourNo)));*/
                        #endregion

                        pClsObj = null;
                        decimal comission = 0;
                        bool? pIsPers = false;
                        try
                        {
                            pClsObj = new ClsAdo();
                            comission = Convert.ToDecimal(pClsObj.fnGetAgentLavelComm(Convert.ToInt32(Session["AgentId"]),
                                Convert.ToString(tourid), "TourID", "SpecialTour Booking", ref pIsPers));
                        }
                        finally
                        {
                            if (pClsObj != null)
                            {
                                pClsObj = null;
                            }
                        }

                        Session["Comission"] = Convert.ToString(comission);
                        Session["IsPercentComm"] = Convert.ToBoolean(pIsPers);

                        //decimal servicetax = (totalAmount * ((stax) / 100));

                        decimal servicetax = decimal.Round(Convert.ToDecimal(txtTotalServiceTax.Text));
                        totalAmount = decimal.Round(Convert.ToDecimal(txtFareTotal.Text) - servicetax);

                        decimal lAdvance = Convert.ToDecimal(txtFareTotal.Text);
                        if (Convert.ToInt32(hdNoofDays.Value) > 14)
                        {
                            for (int lCtr = 0; lCtr < rbtnPaymentOption.Items.Count; lCtr++)
                            {
                                if (rbtnPaymentOption.Items[lCtr].Selected)
                                    if (rbtnPaymentOption.Items[lCtr].Value == "HALF")
                                    {
                                        lAdvance = Convert.ToDecimal(txtFareTotal.Text) * Convert.ToDecimal(.5);
                                    }
                            }
                        }
                        pAdvance = lAdvance;

                        if (Convert.ToString(Session["Balance"]) != "")
                        {
                            if (Convert.ToDecimal(Convert.ToString(Session["Balance"])) <= 0)
                            {
                                ClsCommon.ShowAlert("Insufficient funds");
                                //RegisterStartupScript("Error", "<Script>alert('Insufficient funds.');</Script>");
                                return;
                            }
                            if (Convert.ToDecimal(Convert.ToString(Session["Balance"])) >= pAdvance/*(totalAmount + servicetax)*/)
                            {
                                decimal AvailableBalance = Convert.ToDecimal(Convert.ToString(Session["Balance"]));

                                //string refn = "SPL" + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Minute + System.DateTime.Now.Second;
                                //string refn = DataLib.SPLticketCode();
                                //string strPNR = DataLib.SPLpnr();

                                agentid = Convert.ToInt32(Session["AgentId"]);
                                decimal AgentCommission = 0;
                                if (lAdvance == Convert.ToDecimal(txtFareTotal.Text))
                                {
                                    AgentCommission = Convert.ToBoolean(Session["IsPercentComm"]) ? decimal.Round(((totalAmount) *
                                        ((Convert.ToDecimal(Session["Comission"])) / 100))) : decimal.Round(Convert.ToDecimal(Session["Comission"]) * totalPax);
                                }
                                decimal caltds = 0;
                                caltds = decimal.Round(Convert.ToDecimal(AgentCommission * (Convert.ToDecimal(tds) / 100)));
                                decimal AvailableBalance1 = decimal.Round(AvailableBalance + AgentCommission - caltds - (lAdvance));
                                //string strLocalBranch = pClsObj.fnGetLocalBranch(agentid);
                                #region Optimize Code
                                /*string strBranch = "select localbranch from agent_agentdetails where agentid='" + agentid + "'";
                            string strLocalBranch = DataLib.GetStringData(DataLib.Connection.ConnectionString, strBranch);


                            if (chkDrop.Checked == true)
                            {
                                string strInsert = @"INSERT INTO spl_tourEnquiry (Email, FirstName,Address,JourneyDate, TourName,
                                        TotalPax, CarType, FareCategoryType, PassengerPerVehicle,Status,tourid,fare,Servicetax,ticketno,
                                        SingleSharing,PickUpVeh,PickVehNo,PickTime,DropVeh,DropVehNo,DropTime,Mobile,Phone,Comments,
                                        station,agentid,PkStation,BranchCode,pnrno) VALUES ('" + txtMail.Text.Replace("'", "''") + "',
                             * '" + strfname + "','" + DataLib.funClear(txtAddress.Text) + "','" + strJourneyDate + "','" + txttourName.Text + "',
                             * " + totalPax + ",'" + strVihcle + "','" + category + "','" + strPaxdetails + "','S'," + tourid + ",
                             * '" + decimal.Round((totalAmount + extraFair)) + "','" + decimal.Round(servicetax) + "','" + refn + "',
                             * '" + strSinglePax + "','" + pickupveh + "','" + strFlightNo + "','" + strPktime + "','" + pickupveh + "'
                             * ,'" + strFlightNo + "','" + strPktime + "','" + strMobile + "','" + strPhone + "','" + strComments + "',
                             * '" + strDFlight + "','" + agentid + "','" + strPKstation + "','" + Session["LocalBranch"].ToString() + "',
                             * '" + strPNR + "');select @@identity;";
                                string strQueryId = DataLib.GetStringData(DataLib.Connection.ConnectionString, strInsert);

                                string sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,
                             * TicketAmount,Commission,UserName,BranchCode,TransState,Status,PaymentMode,ServiceTax,TDS,debit,credit)
                             * values(" + agentid + ",'" + refn + "',11," + decimal.Round((AgentCommission - caltds)) + ",
                             * " + decimal.Round(AvailableBalance1) + "," + decimal.Round((totalAmount + servicetax + extraFair)) + ",
                             * " + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + "," + decimal.Round((AgentCommission - caltds)) + ",
                             * '" + Session["UserId"].ToString() + "','" + strLocalBranch + "','S','S','AgentCash','" + decimal.Round(servicetax) + "',
                             * '" + decimal.Round(caltds) + "'," + decimal.Round((AgentCommission - caltds)) + ",
                             * " + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + ")";
                                DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, sqladd, false);

                                Response.Redirect("AgentSpltourTicket.aspx?Id=" + strQueryId + "");
                            }
                            else
                            {

                                string strInsert = @"INSERT INTO spl_tourEnquiry (Email, FirstName,Address,JourneyDate, TourName,
                                        TotalPax, CarType, FareCategoryType, PassengerPerVehicle,Status,tourid,fare,Servicetax,
                                        ticketno,SingleSharing,PickUpVeh,PickVehNo,PickTime,DropVeh,DropVehNo,DropTime,Mobile,Phone,
                                        Comments,station,AgentId,pkstation,BranchCode,pnrno) 
                             * VALUES ('" + txtMail.Text.Replace("'", "''") + "','" + strfname + "','" + DataLib.funClear(txtAddress.Text) + "',
                             * '" + strJourneyDate + "','" + txttourName.Text + "'," + totalPax + ",'" + strVihcle + "','" + category + "',
                             * '" + strPaxdetails + "','S'," + tourid + ",'" + decimal.Round((totalAmount + extraFair)) + "',
                             * '" + decimal.Round(servicetax) + "','" + refn + "','" + strSinglePax + "','" + pickupveh + "','" + strFlightNo + "',
                             * '" + strPktime + "','" + dropveh + "','" + strDFlightNo + "','" + strDPktime + "','" + strMobile + "',
                             * '" + strPhone + "','" + strComments + "','" + strDFlight + "','" + agentid + "','" + strPKstation + "',
                             * '" + Session["LocalBranch"].ToString() + "','" + strPNR + "');select @@identity;";
                                string strQueryId = DataLib.GetStringData(DataLib.Connection.ConnectionString, strInsert);

                                string sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,
                             * TicketAmount,Commission,UserName,BranchCode,TransState,Status,PaymentMode,ServiceTax,TDS,debit,credit)
                             * values(" + agentid + ",'" + refn + "',11," + decimal.Round((AgentCommission - caltds)) + ",
                             * " + decimal.Round(AvailableBalance1) + "," + decimal.Round((totalAmount + servicetax + extraFair)) + ",
                             * " + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + "," + decimal.Round((AgentCommission - caltds)) + ",
                             * '" + Session["UserId"].ToString() + "','" + strLocalBranch + "','S','S','AgentCash','" + decimal.Round(servicetax) + "',
                             * '" + decimal.Round(caltds) + "'," + decimal.Round((AgentCommission - caltds)) + ",
                             * " + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + ")";
                                DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, sqladd, false);

                                Response.Redirect("AgentSpltourTicket.aspx?Id=" + strQueryId + "");
                            }*/
                                #endregion
                                //pvClsSpl = null;
                                GST_spl_tourEnquiry clsTblObj = null;
                                OnlineTransactionTable TblObj = null;
                                string nationality = string.Empty;
                                string strQueryId = "";
                                GST_Data objGstData = null;
                                try
                                {
                                    objGstData = new GST_Data();
                                    //pvClsSpl = new STSPLOrOther();
                                    clsTblObj = new GST_spl_tourEnquiry();
                                    clsTblObj.Email = DataLib.funClear(txtMail.Text);
                                    clsTblObj.FirstName = DataLib.funClear(strfname);
                                    clsTblObj.Address = DataLib.funClear(txtAddress.Text);
                                    clsTblObj.JourneyDate = strJourneyDate;
                                    clsTblObj.TourName = txttourName.Text;
                                    clsTblObj.TotalPax = totalPax;
                                    clsTblObj.CarType = strVihcle;
                                    clsTblObj.FareCategoryType = Convert.ToString(category);
                                    clsTblObj.PassengerPerVehicle = strPaxdetails;
                                    clsTblObj.Status = 'S';
                                    clsTblObj.TourId = tourid;
                                    clsTblObj.Fare = decimal.Round(Convert.ToDecimal(txtFareTotal.Text) - servicetax);
                                    //clsTblObj.Servicetax = decimal.Round(servicetax);
                                    //clsTblObj.TicketNo = refn;
                                    clsTblObj.SingleSharing = strSinglePax;
                                    clsTblObj.PickUpVeh = pickupveh;
                                    clsTblObj.PickVehNo = strFlightNo;
                                    clsTblObj.PickTime = strPktime;
                                    if (chkDrop.Checked == true)
                                    {
                                        clsTblObj.DropVeh = pickupveh;
                                        clsTblObj.DropVehNo = strFlightNo;
                                        clsTblObj.DropTime = strPktime;
                                    }
                                    else
                                    {
                                        clsTblObj.DropVeh = dropveh;
                                        clsTblObj.DropVehNo = strDFlightNo;
                                        clsTblObj.DropTime = strDPktime;
                                    }
                                    clsTblObj.Mobile = strMobile;
                                    clsTblObj.Phone = strPhone;
                                    clsTblObj.CCCharges = decimal.Round(0);
                                    clsTblObj.Comments = strComments;
                                    clsTblObj.station = strDFlight;
                                    clsTblObj.PkStation = strPKstation;
                                    clsTblObj.BranchCode = Convert.ToString(Session["LocalBranch"]);
                                    clsTblObj.AgentId = agentid;
                                    clsTblObj.AdvancePaid = lAdvance;

                                    //clsTblObj.PnrNo = strPNR;
                                    //string strQueryId = Convert.ToString(pClsObj.fnInsertSPLTourDetail(clsTblObj));
                                    //Session["strQId"] = Convert.ToString(strQueryId);

                                    TblObj = new OnlineTransactionTable();

                                    //TblObj.AgentId = agentid;
                                    //TblObj.RefNo = refn;

                                    TblObj.TransType = 11;
                                    TblObj.AgentCredit = decimal.Round((AgentCommission - caltds));
                                    TblObj.AvailableBalance = decimal.Round(AvailableBalance1);
                                    TblObj.AgentDebit = decimal.Round(lAdvance /*(totalAmount + servicetax)*/ );
                                    TblObj.TicketAmount = decimal.Round(totalAmount);
                                    TblObj.Commission = decimal.Round((AgentCommission - caltds));
                                    TblObj.UserName = Session["UserId"].ToString();

                                    //TblObj.BranchCode = strLocalBranch;

                                    TblObj.TransState = 'S';
                                    //TblObj.Status = "S";
                                    TblObj.PaymentMode = "AgentCash";
                                    TblObj.ServiceTax = decimal.Round(servicetax);
                                    TblObj.TDS = decimal.Round(caltds);
                                    TblObj.Debit = decimal.Round((AgentCommission - caltds));
                                    TblObj.Credit = decimal.Round(lAdvance /*totalAmount*/);

                                    //int val = pClsObj.fnInsertSpl_TransTblAgent(TblObj);
                                    //clsTblObj.Country=dd
                                    //ddlCity.SelectedItem.Text.Trim();
                                    clsTblObj.ZipCode = txtZipCode.Text.Trim();

                                    if (rdbIsGSTApplicableYes.Checked == true)
                                    {
                                        clsTblObj.ISGSITIN = true;
                                    }
                                    else
                                    {
                                        clsTblObj.ISGSITIN = false;
                                    }

                                    if (clsTblObj.ISGSITIN == true)
                                    {
                                        clsTblObj.CustomerGSTIN = txtCustomerGSTIN.Text.Trim();
                                        clsTblObj.GstHolderName = txtGstHolderName.Text.Trim();
                                    }
                                    else
                                    {
                                        clsTblObj.CustomerGSTIN = "";
                                        clsTblObj.GstHolderName = "";
                                    }

                                    if (ddlCountry.SelectedValue == "59")
                                    {
                                        clsTblObj.State = ddlState.SelectedItem.Text.Trim();
                                        clsTblObj.City = txtCity.Text.Trim();
                                    }
                                    else
                                    {
                                        clsTblObj.State = TxtForeignState.Text;
                                        clsTblObj.City = txtForeignCity.Text.Trim();
                                    }

                                    string Country = ddlCountry.SelectedValue;
                                    string Nationality = ddlNationality.SelectedValue;
                                    string AadharNo = txtAadharNo.Value;
                                    string AadharNoImg = fupAadhar.FileName;


                                    strQueryId = Convert.ToString(objGstData.GST_InsertFinalSPLTourInfoAgent(clsTblObj, TblObj,
                                    Convert.ToString(ViewState["strPass"]), ddlTitle.SelectedItem.Text, lAlternateMBLNO,
                                    chkPromotions.Checked, ConvertStringint(hdfTotalChildWithoutMatress.Value), ConvertStringint(hdfTotalChildWithMatress.Value), Nationality, Country, AadharNo, AadharNoImg));

                                    Session["strQId"] = Convert.ToString(strQueryId);

                                    if (!string.IsNullOrEmpty(fupAadhar.FileName))
                                    {
                                        SaveAadharPhysicalImg(fupAadhar.FileName);
                                    }

                                }
                                finally
                                {
                                    if (objGstData != null)
                                    {
                                        objGstData = null;
                                    }
                                    if (clsTblObj != null)
                                    {
                                        clsTblObj = null;
                                    }
                                    if (TblObj != null)
                                    {
                                        TblObj = null;
                                    }
                                    Response.Redirect("AgentSpltourTicket.aspx?Id=" + strQueryId + "");
                                }
                            }
                            else
                            {
                                RegisterStartupScript("Error", "<Script>alert('Insufficient funds.');</Script>");
                            }
                        }
                        else
                        {
                            RegisterStartupScript("Error1", "<Script>alert('Balance Should be > 0');</Script>");
                        }
                    }
                    else
                    {
                        this.RegisterStartupScript("Error2", "<Script>alert('The date should be greater by 2 days/24hrs from the current date');</Script>");
                    }

                    #region "commented"
                    //    string strfname = ddlTitle.SelectedItem.Text + ' ' + txtFName.Text;
                    //    decimal Cc = decimal.Round(Convert.ToDecimal(txtCC.Text));
                    //    string strComments = DataLib.funClear(txtComment.Text);
                    //    decimal st = decimal.Round(Convert.ToDecimal(txtTotalServiceTax.Text) - Cc);
                    //    decimal tfare = decimal.Round(Convert.ToDecimal(txtFareTotal.Text) - Cc);

                    //    int tourid = Convert.ToInt32(ViewState["tourno"]);
                    //    string strJourneyDate = txtDate.Value.Split('/').GetValue(1) + "/" + txtDate.Value.Split('/').GetValue(0) + "/" +
                    //        txtDate.Value.Split('/').GetValue(2);
                    //    string refn = DataLib.SPLticketCode();
                    //    string strPNR = DataLib.SPLpnr();
                    //    string strInsert = @"INSERT INTO spl_tourEnquiry (Email, FirstName,Address,JourneyDate, TourName, " +
                    //        @"TotalPax, CarType, FareCategoryType, PassengerPerVehicle,Status,tourid,fare,Servicetax," +
                    //        @"ticketno,SingleSharing,PickUpVeh,PickVehNo,PickTime,DropVeh,DropVehNo,DropTime,Mobile," +
                    //        @"Phone,CCCharges,Comments,station,PkStation,BranchCode,AgentId,pnrno) VALUES " +
                    //        @"(@Email,@FirstName,@Address,@JourneyDate,@TourName,@TotalPax,@CarType,@FareCategoryType," +
                    //        @"@PassengerPerVehicle,@Status,@tourid,@fare,@Servicetax,@ticketno,@SingleSharing," +
                    //        @"@PickUpVeh,@PickVehNo,@PickTime,@DropVeh,@DropVehNo,@DropTime ,@Mobile,@Phone,@CCCharges," +
                    //        @"@Comments,@station,@PkStation,@BranchCode,@AgentId,@pnrno);select @@identity;";
                    //    SqlParameter[] splParam = new SqlParameter[30];
                    //    splParam[0] = new SqlParameter("@Email", DataLib.funClear(txtMail.Text));
                    //    splParam[1] = new SqlParameter("@FirstName", DataLib.funClear(strfname));
                    //    splParam[2] = new SqlParameter("@Address", DataLib.funClear(txtAddress.Text));
                    //    splParam[3] = new SqlParameter("@JourneyDate", strJourneyDate);
                    //    splParam[4] = new SqlParameter("@TourName", txttourName.Text);
                    //    splParam[5] = new SqlParameter("@TotalPax", totalPax);
                    //    splParam[6] = new SqlParameter("@CarType", strVihcle);
                    //    splParam[7] = new SqlParameter("@FareCategoryType", category);
                    //    splParam[8] = new SqlParameter("@PassengerPerVehicle", strPaxdetails);
                    //    splParam[9] = new SqlParameter("@Status", "N");
                    //    splParam[10] = new SqlParameter("@tourid", tourid);
                    //    splParam[11] = new SqlParameter("@fare", decimal.Round((tfare + extraFair)));
                    //    splParam[12] = new SqlParameter("@Servicetax", decimal.Round(st));
                    //    splParam[13] = new SqlParameter("@ticketno", refn);
                    //    splParam[14] = new SqlParameter("@SingleSharing", strSinglePax);
                    //    splParam[15] = new SqlParameter("@PickUpVeh", pickupveh);
                    //    splParam[16] = new SqlParameter("@PickVehNo", strFlightNo);
                    //    splParam[17] = new SqlParameter("@PickTime", strPktime);
                    //    if (chkDrop.Checked == true)
                    //    {
                    //        splParam[18] = new SqlParameter("@DropVeh", pickupveh);
                    //        splParam[19] = new SqlParameter("@DropVehNo", strFlightNo);
                    //        splParam[20] = new SqlParameter("@DropTime", strPktime);
                    //    }
                    //    else
                    //    {
                    //        splParam[18] = new SqlParameter("@DropVeh", dropveh);
                    //        splParam[19] = new SqlParameter("@DropVehNo", strDFlightNo);
                    //        splParam[20] = new SqlParameter("@DropTime", strDPktime);
                    //    }
                    //    splParam[21] = new SqlParameter("@Mobile", strMobile);
                    //    splParam[22] = new SqlParameter("@Phone", strPhone);
                    //    splParam[23] = new SqlParameter("@CCCharges", decimal.Round(0));
                    //    splParam[24] = new SqlParameter("@Comments", strComments);
                    //    splParam[25] = new SqlParameter("@station", strDFlight);
                    //    splParam[26] = new SqlParameter("@PkStation", strPKstation);
                    //    splParam[27] = new SqlParameter("@BranchCode", "EBK0001");
                    //    splParam[28] = new SqlParameter("@AgentId", "0");
                    //    splParam[29] = new SqlParameter("@pnrno", strPNR);
                    //    string strQueryId = DataLib.WithOPInsOrUpdateParam(strInsert, splParam);
                    //    Session["strQId"] = Convert.ToString(strQueryId);
                    //    string strSelect = "select * from spl_tourEnquiry(nolock)  where id='" + Session["strQId"] + "' ";
                    //    DataTable dt7 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strSelect);

                    //}
                    #endregion
                }
            }
        }

        private void SaveAadharPhysicalImg(string pFileName)
        {
            // Specify the path to save the uploaded file to.
            string lSavePath = Server.MapPath("..\\AadharImg\\");
            SaveResizedAadharImage(pFileName);
            //F1ImageName.PostedFile.SaveAs(lSavePath + pFileName);
        }
        private void SaveResizedAadharImage(string pFileName)
        {
            string newpath = Server.MapPath("..\\AadharImg\\" + pFileName + ".jpg");
            if (fupAadhar.HasFile)
            {
                System.Drawing.Bitmap bmpPostedImage = new System.Drawing.Bitmap(fupAadhar.PostedFile.InputStream);
                System.Drawing.Image objImage = ScaleImage(bmpPostedImage, 850);
                objImage.Save(newpath, ImageFormat.Png);
            }
        }

        public static System.Drawing.Image ScaleImage(System.Drawing.Image image, int maxHeight)
        {
            if (image.Height < maxHeight)
            {
                maxHeight = image.Height;
            }
            var ratio = (double)maxHeight / image.Height;

            var newWidth = (int)(image.Width * ratio);
            var newHeight = (int)(image.Height * ratio);

            var newImage = new Bitmap(newWidth, newHeight);
            using (var g = Graphics.FromImage(newImage))
            {
                if (image.Height <= maxHeight)
                {
                    g.DrawImage(image, 0, 0);
                }
                else
                {
                    g.DrawImage(image, 0, 0, newWidth, newHeight);
                }
            }
            return newImage;
        }

        public static DateTime mmddyy2ddmmyy(string date1)
        {
            string[] DateArr3 = new string[3];
            char[] splitter1 = { '/' };
            DateArr3 = date1.Split(splitter1);
            return new DateTime(Convert.ToInt32(DateArr3[2]), Convert.ToInt32(DateArr3[1]), Convert.ToInt32(DateArr3[0]));

        }
        protected void ddlCarType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Convert.ToInt32(ViewState["tourno"]) == 6 || Convert.ToInt32(ViewState["tourno"]) == 9 ||
                Convert.ToInt32(ViewState["tourno"]) == 96)
            {
                if (((fldJourneyDate >= new DateTime(2012, 4, 23)) && (fldJourneyDate <= new DateTime(2012, 6, 20))) &&
                    (rdoStandard.Checked))
                {
                    RegisterStartupScript("Error2", "<Script>alert('Standard package sold out till 20-June-2012.');</Script>");
                    return;
                }
            }
            GetTourFare(Convert.ToInt32(ViewState["tourno"]), fldJourneyDate, -1, -1);
            FillCarPax();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "getfare", "GetTourFare();", true);
            if (Convert.ToInt32(hdNoofDays.Value) > 14)
            {
                rbtnPaymentOption.Style.Add("display", "block");
            }
            else
            {
                rbtnPaymentOption.Style.Add("display", "none");
            }
        }
        protected void ddlNoOfPax_SelectedIndexChanged(object sender, EventArgs e)
        {
            int lPaxID = 0, lCategoryID = 0;
            if (rdoStandard.Checked)
            {
                lCategoryID = 1;
            }
            else if (rdoDeluxe.Checked)
            {
                lCategoryID = 2;
            }
            else
            {
                lCategoryID = 7;
            }
            //lPaxID = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[0]);
            lPaxID = GetPaxId();

            GetTourFare(Convert.ToInt32(ViewState["tourno"]), fldJourneyDate, lCategoryID, (lPaxID == 0 ? 1 : lPaxID));
            ScriptManager.RegisterStartupScript(this, this.GetType(), "getfare", "finalFare();GetTourFare();", true);
            if (Convert.ToInt32(hdNoofDays.Value) > 14)
            {
                rbtnPaymentOption.Style.Add("display", "block");
            }
            else
            {
                rbtnPaymentOption.Style.Add("display", "none");
            }


        }
        protected void btnGetFare_Click(object sender, EventArgs e)
        {

        }
        protected void ddlSubCategory_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetTourFare(Convert.ToInt32(ViewState["tourno"]), fldJourneyDate, -1, -1);
            FillCarPax();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "getfare", "GetTourFare();", true);
            if (Convert.ToInt32(hdNoofDays.Value) > 14)
            {
                rbtnPaymentOption.Style.Add("display", "block");
            }
            else
            {
                rbtnPaymentOption.Style.Add("display", "none");
            }
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindCityName();
        }
        protected void OnCheckChanged_rdbIsGSTApplicableYes(object sender, EventArgs e)
        {
            divGSTDetails.Style["display"] = "";
            divGSTDetails1.Style["display"] = "";
        }
        protected void OnCheckChanged_rdbIsGSTApplicableNo(object sender, EventArgs e)
        {
            divGSTDetails.Style["display"] = "none";
            divGSTDetails1.Style["display"] = "none";
        }
        #endregion
        #region "Method(s)"
        public void GetChildPaxCount(int pTourID, DateTime pJourneyDate)
        {
            DataTable ldtSpecialTourFare = null;
            ClsAdo clsObj = null;
            DataTable dt = new DataTable();
            try
            {
                SqlParameter[] lParam = new SqlParameter[2];
                lParam[0] = new SqlParameter("@JDate", pJourneyDate);
                lParam[1] = new SqlParameter("@TourID", pTourID);


                DataSet lds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString,StoredProcedures.GetChildPaxCount_sp, lParam);

                //---------------------Setting the child Fare--------------
                try
                {
                    if (lds.Tables[0].Rows.Count > 0)
                    {
                        IsChildPaxAvailable = 1;
                        hdfIsChildPaxAvailable.Value = "1";
                    }
                    else
                    {
                        IsChildPaxAvailable = 0;
                        hdfIsChildPaxAvailable.Value = "0";
                    }

                }
                catch (Exception ex)
                {

                }
                //----------End of setting the child without Fare-----------


            }
            catch (Exception Ex)
            {

            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldtSpecialTourFare != null)
                {
                    ldtSpecialTourFare.Dispose();
                    ldtSpecialTourFare = null;
                }
                if (dt != null)
                {
                    dt = null;
                }
            }
        }
        private void GetTourFare(int pTourID, DateTime pJourneyDate, int pCategoryID, int pPaxID)
        {
            DataTable ldtFare = null;
            try
            {
                // Start Add For New Sub Category Ex. CP/MAP/AP Geting Fare. if remove this please comment Start to End Code. (Add By Santosh)
                //if (pPaxID > 0)
                //{
                if (rdoStandard.Checked)
                {
                    if (ddlSubCategory.SelectedValue == "0")
                    {
                        pCategoryID = 3;
                    }
                    else if (ddlSubCategory.SelectedValue == "1")
                    {
                        pCategoryID = 4;
                    }
                }
                else if (rdoDeluxe.Checked)
                {
                    if (ddlSubCategory.SelectedValue == "0")
                    {
                        pCategoryID = 5;
                    }
                    else if (ddlSubCategory.SelectedValue == "1")
                    {
                        pCategoryID = 6;
                    }
                }
                else if (rdoLuxury.Checked)
                {
                    if (ddlSubCategory.SelectedValue == "0")
                    {
                        pCategoryID = 8;
                    }
                    else if (ddlSubCategory.SelectedValue == "1")
                    {
                        pCategoryID = 9;
                    }
                }
                // }
                // End
                // ldtFare = clsBLL.GetSpecialTourFare(pTourID, pJourneyDate, pCategoryID, pPaxID);
                DataSet lds = GetSpecialTourFare(pTourID, pJourneyDate, pCategoryID, pPaxID);
                ldtFare = lds.Tables[0];
                if (ldtFare.Rows.Count > 0)
                {
                    bool lflag = false;
                    Submit.Enabled = true;
                    for (int i = 0; i < ldtFare.Rows.Count; i++)
                    {
                        txtMinPax.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        txtMaxPax.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        txtHiddenFare.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                        SARFare.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        if (Convert.ToInt32(ldtFare.Rows[i]["CategoryID"]) == 3
                            || Convert.ToInt32(ldtFare.Rows[i]["CategoryID"]) == 4
                            || Convert.ToInt32(ldtFare.Rows[i]["CategoryID"]) == 5
                            || Convert.ToInt32(ldtFare.Rows[i]["CategoryID"]) == 6)
                        {
                            if (Convert.ToDecimal(txtHiddenFare.Value) == 0 && lflag == false)
                            {
                                //ddlSubCategory.SelectedIndex = -1;
                                ddlSubCategory.Enabled = false;
                            }
                            else
                            {
                                ddlSubCategory.Enabled = true;
                                lflag = true;
                            }
                        }
                        else
                        {
                            if (lflag == false)
                            {
                                //ddlSubCategory.SelectedIndex = -1;
                                ddlSubCategory.Enabled = false;
                            }
                            else
                            {
                                ddlSubCategory.Enabled = true;
                                lflag = true;
                            }
                        }
                        if (pPaxID > -1)
                        {
                            int paxN0 = 0;
                            string[] SelectedPax = ddlNoOfPax.SelectedValue.ToString().Split('-');
                            if (Convert.ToInt32(ddlNoOfPax.SelectedItem.Text) < Convert.ToInt32(SelectedPax[1]))
                            {
                                paxN0 = Convert.ToInt32(SelectedPax[1]);
                            }
                            else
                            {
                                paxN0 = Convert.ToInt32(ddlNoOfPax.SelectedItem.Text);
                            }
                            SPLDiscount(2, pTourID, paxN0);
                        }
                        #region Comment Code
                        /*if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 1)
                    {
                        car2min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        car2max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtCar12pxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtCar12pxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                    }
                    if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 2)
                    {
                        car34min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        car34max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtCar34pxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtCar34pxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                    }
                    if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 3)
                    {
                        innova45min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        innova45max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtInn45PxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtInn45PxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                    }
                    if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 4)
                    {
                        innova67min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        innova67max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtInn67PxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtInn67PxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                    }
                    if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 5)
                    {
                        qualis46min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        qualis46max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);

                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtQua46PxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtQua46PxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                    }
                    if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 6)
                    {
                        qualis78min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        qualis78max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtQua78PxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtQua78PxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                    }
                    if (Convert.ToInt32(ldtFare.Rows[i]["paxid"]) == 7)
                    {
                        tempo89min.Value = Convert.ToString(ldtFare.Rows[i]["minallowed"]);
                        tempo89max.Value = Convert.ToString(ldtFare.Rows[i]["maxallowed"]);
                        if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "1")
                        {
                            txtTem89PxHiddenSt.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARstandard.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }
                        else if (Convert.ToString(ldtFare.Rows[i]["categoryid"]) == "2")
                        {
                            txtTem89PxHiddenDl.Value = Convert.ToString(ldtFare.Rows[i]["fare"]);
                            SARdeluxe.Value = Convert.ToString(ldtFare.Rows[i]["SARfare"]);
                        }

                    } */
                        #endregion
                    }
                }
                else
                {
                    ClsCommon.ShowAlert("Sorry!.. Currently there is no fares for this tour");
                    //ClientScript.RegisterStartupScript(GetType(), "Warning", "<script>alert('Sorry!.. Currently there is no fares for this tour');</script>");
                    Submit.Enabled = false;
                }
            }
            finally
            {
                if (ldtFare != null)
                {
                    ldtFare.Dispose();
                }
            }
        }
        private void SPLDiscount(int lTourTypeID, int lTourID, int lPaxN0)
        {
            STSPLOrOther lSTPLOther = null;
            List<GetDiscount_spResult> lResult = null;
            try
            {

                lSTPLOther = new STSPLOrOther();
                lResult = new List<GetDiscount_spResult>();
                if (IsDiscountValid(lTourID.ToString()))
                {
                    lResult = lSTPLOther.fnGetDiscount(lTourTypeID, lTourID);
                    if (lResult != null && lResult.Count > 0)
                    {
                        decimal lFlatDiscount = Convert.ToDecimal(lResult[0].FlatDiscount.ToString());
                        decimal lPerDiscount = Convert.ToDecimal(lResult[0].PerDiscount.ToString());

                        bool strPer = Convert.ToBoolean(lResult[0].IsPer.ToString());

                        bool ISJourneyDate = Convert.ToBoolean(lResult[0].IsCheckIn.ToString());
                        bool IsFlat = Convert.ToBoolean(lResult[0].IsFlat.ToString());
                        bool ISBooking = Convert.ToBoolean(lResult[0].IsBooking.ToString());

                        bool IsAgent = Convert.ToBoolean(lResult[0].IsAgent.ToString());

                        if (IsAgent)
                        {
                            decimal pDiscountFare = 0;
                            decimal pDiscountAmt = 0;
                            if (!strPer)
                            {
                                pDiscountFare = Math.Round((Convert.ToDecimal(txtHiddenFare.Value) - lFlatDiscount), 2, MidpointRounding.AwayFromZero);
                                pDiscountAmt = decimal.Round(lFlatDiscount);
                            }
                            else
                            {
                                pDiscountFare = Math.Round(Convert.ToDecimal(txtHiddenFare.Value) - (Convert.ToDecimal(txtHiddenFare.Value) * lPerDiscount / 100), 2, MidpointRounding.AwayFromZero);
                                pDiscountAmt = Math.Round((Convert.ToDecimal(txtHiddenFare.Value) * lPerDiscount / 100), 2, MidpointRounding.AwayFromZero);
                            }

                            if (IsFlat)
                            {
                                lblDFare.Text = "Offered Discount: " + (Math.Round((pDiscountAmt * lPaxN0), MidpointRounding.AwayFromZero)).ToString();
                                txtHiddenFare.Value = pDiscountFare.ToString();
                                hdSPLDiscount.Value = (Math.Round(pDiscountAmt * lPaxN0, MidpointRounding.AwayFromZero)).ToString();
                            }
                            if (ISBooking)
                            {
                                if (lResult[0].BookingFrom != null && lResult[0].BookingTo != null)
                                {
                                    DateTime dtBFrom = Convert.ToDateTime(lResult[0].BookingFrom);
                                    DateTime dtBTo = Convert.ToDateTime(lResult[0].BookingTo);
                                    DateTime dtNow = DateTime.Now.Date;
                                    if (dtNow >= dtBFrom.Date && dtNow <= dtBTo.Date)
                                    {
                                        lblDFare.Text = "Offered Discount: " + (pDiscountAmt * lPaxN0).ToString();
                                        txtHiddenFare.Value = pDiscountFare.ToString();
                                        hdSPLDiscount.Value = (Math.Round(pDiscountAmt * lPaxN0, MidpointRounding.AwayFromZero)).ToString();
                                    }
                                }
                            }
                            if (ISJourneyDate)
                            {
                                if (lResult[0].CheckInFrom != null && lResult[0].CheckOutTo != null)
                                {
                                    DateTime dtCFrom = Convert.ToDateTime(lResult[0].CheckInFrom);
                                    DateTime dtCTo = Convert.ToDateTime(lResult[0].CheckOutTo);
                                    char[] splitter = { '/' };
                                    string[] DateArr = txtDate.Value.ToString().Trim().Split(splitter);
                                    DateTime dtJourneyDate = new DateTime(Convert.ToInt32(DateArr[2]), Convert.ToInt32(DateArr[1]),
                                         Convert.ToInt32(DateArr[0]));

                                    if ((dtJourneyDate >= dtCFrom.Date && dtJourneyDate <= dtCTo.Date))
                                    {
                                        lblDFare.Text = "Offered Discount: " + (pDiscountAmt * lPaxN0).ToString();
                                        txtHiddenFare.Value = pDiscountFare.ToString();
                                        hdSPLDiscount.Value = (Math.Round(pDiscountAmt * lPaxN0, MidpointRounding.AwayFromZero)).ToString();
                                    }
                                }
                            }

                        }
                    }
                }
            }
            finally
            {
                if (lSTPLOther != null)
                {
                    lSTPLOther = null;
                }
                if (lResult != null)
                {
                    lResult = null;
                }
            }
        }
        private bool IsDiscountValid(string pTourID)
        {
            return (IsDiscountDate() || (!IsDiscountDate() && !IsValidTour(pTourID)));
        }

        private bool IsDiscountDate()
        {
            return (fldJourneyDate < new DateTime(2013, 12, 22) || fldJourneyDate > new DateTime(2014, 1, 4));
        }
        private bool IsValidTour(string lTourToFind)
        {
            string[] lValidTours = new string[] { "1", "18", "19", "20", "22", "23", "25", "27", "28", "29", "30", "31", "33", "34", "39", "40", "41", "49", "66", "67", "68", "69", "79", "83", "84", "85", "86", "90", "93", "94", "97", "98", "99", "100", "101", "102", "103", "105", "110", "114", "131" };
            return lValidTours.Any(lTourToFind.Equals);
        }
        protected void FillTourCar()
        {
            DataTable ldtTourCar = null;
            try
            {
                // ldtTourCar = clsBLL.GetSpecialTourCarType(tourid);
                ldtTourCar = fnGetSpecialTourCarType(Convert.ToInt32(Request.QueryString["tourid"].ToString()));

                for (int i = ldtTourCar.Rows.Count - 1; i >= 0; i--)
                {
                    DataRow dr = ldtTourCar.Rows[i];
                    if (Convert.ToString(dr["IsPax"]) == "True")
                    {
                        ldtTourCar.Rows.Remove(dr);
                        IsChildPaxAvailable = 1;
                        hdfIsChildPaxAvailable.Value = "1";
                    }
                }

                ldtTourCar.AcceptChanges();
                ddlCarType.DataSource = ldtTourCar;
                ddlCarType.DataBind();
                ddlCarType.DataTextField = "VehicleName";
                ddlCarType.DataValueField = "VehicleID";
                ddlCarType.DataBind();
                System.Web.UI.WebControls.ListItem lSelectCarType = new System.Web.UI.WebControls.ListItem("Select", "-1");
                ddlCarType.Items.Insert(0, lSelectCarType);
            }
            finally
            {
                if (ldtTourCar != null)
                {
                    ldtTourCar.Dispose();
                }
            }
        }

        //protected void FillCarPax()
        //{
        //    int VehicleID = Convert.ToInt32(ddlCarType.SelectedValue);
        //    DataTable ldtCarPax = null;
        //    try
        //    {
        //        ldtCarPax = clsBLL.GetSpecialTourCarPax(VehicleID);
        //        if (ldtCarPax != null ? (ldtCarPax.Rows.Count > 0 ? true : false) : false)
        //        {
        //            int lMinPax = Convert.ToInt32(ldtCarPax.Rows[0]["MinPax"]), lMaxPax = Convert.ToInt32(ldtCarPax.Rows[0]["MaxPax"]),
        //                lPaxID = 0, lPaxCtr = 0, lCurMin = 0, lCurMax = 0;
        //            ArrayList lCarPax = new ArrayList();
        //            lCurMin = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MinAllowed"]);
        //            lCurMax = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MaxAllowed"]);
        //            hdfMaxVehiclePax.Value = Convert.ToString(lMaxPax);
        //            for (int Ctr = 1; Ctr <= lMaxPax; Ctr++)
        //            {
        //                if (Ctr > lCurMax)
        //                {
        //                    ++lPaxCtr;
        //                }
        //                lPaxID = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["Paxid"]);
        //                lCurMin = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MinAllowed"]);
        //                lCurMax = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MaxAllowed"]);
        //                string lPaxDetail = string.Empty;
        //                lPaxDetail = lPaxID.ToString() + "-" + lCurMin.ToString() + "-" + lCurMax.ToString() + "-" + Ctr.ToString();
        //                lCarPax.Add(new clsCarPax(lPaxDetail, Ctr.ToString()));
        //            }
        //            lCarPax.Insert(0, new clsCarPax("0", "Select"));
        //            ddlNoOfPax.DataSource = lCarPax;
        //            ddlNoOfPax.DataBind();
        //        }
        //    }
        //    finally
        //    {
        //        if (ldtCarPax != null)
        //        {
        //            ldtCarPax.Dispose();
        //        }
        //    }
        //}

        protected void FillCarPax()
        {
            int VehicleID = Convert.ToInt32(ddlCarType.SelectedValue);
            DataTable ldtCarPax = null;
            System.Collections.ArrayList lCarPax = null;
            try
            {
                //  ldtCarPax = clsBLL.GetSpecialTourCarPax(VehicleID);
                ldtCarPax = GetSpecialTourVehiclePax(VehicleID);

                if (ldtCarPax != null ? (ldtCarPax.Rows.Count > 0 ? true : false) : false)
                {
                    int lMinPax = Convert.ToInt32(ldtCarPax.Rows[0]["MinPax"]), lMaxPax = Convert.ToInt32(ldtCarPax.Rows[0]["MaxPax"]),
                        lPaxID = 0, lPaxCtr = 0, lCurMin = 0, lCurMax = 0;
                    lCarPax = new ArrayList();
                    lCurMin = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MinAllowed"]);
                    lCurMax = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MaxAllowed"]);
                    hdfMaxVehiclePax.Value = Convert.ToString(lMaxPax);
                    for (int Ctr = 1; Ctr <= lMaxPax; Ctr++)
                    {
                        if (Ctr > lCurMax)
                        {
                            ++lPaxCtr;
                        }
                        try
                        {
                            lPaxID = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["Paxid"]);
                            lCurMin = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MinAllowed"]);
                            lCurMax = Convert.ToInt32(ldtCarPax.Rows[lPaxCtr]["MaxAllowed"]);
                            string lPaxDetail = string.Empty;
                            lPaxDetail = lPaxID.ToString() + "-" + lCurMin.ToString() + "-" + lCurMax.ToString() + "-" + Ctr.ToString();
                            lCarPax.Add(new clsCarPax(lPaxDetail, Ctr.ToString()));
                        }
                        catch { }
                    }
                    lCarPax.Insert(0, new clsCarPax("0", "Select"));
                    ddlNoOfPax.DataSource = lCarPax;
                    ddlNoOfPax.DataBind();
                }
            }
            finally
            {
                if (ldtCarPax != null)
                {
                    ldtCarPax.Dispose();
                    ldtCarPax = null;
                }
                if (lCarPax != null)
                    lCarPax = null;
            }
        }

        protected float CalculateFare(int pTourID, DateTime pJourneyDate)
        {
            float lFinalFare = 0;
            int lCategoryID = 0, lPaxID = 0;
            DataTable ldtFare = null;
            try
            {
                ldtFare = clsBLL.GetSpecialTourFare(pTourID, pJourneyDate, lCategoryID, lPaxID);

                return lFinalFare;
            }
            finally
            {
                if (ldtFare != null)
                {
                    ldtFare.Dispose();
                }
            }
        }

        private bool ValidateFare()
        {
            int lPaxID = 0, lCategoryID = 0;
            if (rdoStandard.Checked)
            {
                lCategoryID = 1;
            }
            else if (rdoDeluxe.Checked)
            {
                lCategoryID = 2;
            }
            else
            {
                lCategoryID = 7;
            }
            // Start Add For New Sub Category Ex. CP/MAP/AP Geting Fare. if remove this please comment Start to End Code. (Add By Santosh)
            if (rdoStandard.Checked)
            {
                if (ddlSubCategory.SelectedValue == "0")
                {
                    lCategoryID = 3;
                }
                else if (ddlSubCategory.SelectedValue == "1")
                {
                    lCategoryID = 4;
                }
            }
            else if (rdoDeluxe.Checked)
            {
                if (ddlSubCategory.SelectedValue == "0")
                {
                    lCategoryID = 5;
                }
                else if (ddlSubCategory.SelectedValue == "1")
                {
                    lCategoryID = 6;
                }
            }
            else if (rdoLuxury.Checked)
            {
                if (ddlSubCategory.SelectedValue == "0")
                {
                    lCategoryID = 8;
                }
                else if (ddlSubCategory.SelectedValue == "1")
                {
                    lCategoryID = 9;
                }
            }
            // End
            lPaxID = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[0]);
            //lPaxID = GetPaxId();

            DataTable ldtFare = null;
            try
            {
                if (Convert.ToInt32(ViewState["tourno"]) == 6 || Convert.ToInt32(ViewState["tourno"]) == 9 ||
                    Convert.ToInt32(ViewState["tourno"]) == 96)
                {
                    if (((fldJourneyDate >= new DateTime(2012, 4, 23)) && (fldJourneyDate <= new DateTime(2012, 6, 20))) &&
                        (rdoStandard.Checked))
                    {
                        RegisterStartupScript("Error2", "<Script>alert('Standard package sold out till 20-June-2012.');</Script>");
                        return false;
                    }
                }
                ldtFare = clsBLL.GetSpecialTourFare(Convert.ToInt32(ViewState["tourno"]), fldJourneyDate, lCategoryID, lPaxID);
                decimal lBaseFare = 0, lSARFare = 0, lFinalAmtWOST = 0, lFinalAmt = 0, lPreviFinalAmt, lServiceTax = 0; ;
                int lMinAllowed = 0, lMaxAllowed = 0;
                string lSelectedPax = ddlNoOfPax.SelectedValue;
                int totalPax = 0;
                if (Convert.ToInt32(ddlNoOfPax.SelectedItem.Text) < Convert.ToInt32(lSelectedPax.Split(pvSeatSplitter)[1]))
                {
                    totalPax = Convert.ToInt32(lSelectedPax.Split(pvSeatSplitter)[1]);
                }
                else
                {
                    totalPax = Convert.ToInt32(ddlNoOfPax.SelectedItem.Text);
                }
                if (ldtFare != null ? (ldtFare.Rows.Count > 0) : false)
                {
                    lBaseFare = Convert.ToDecimal(ldtFare.Rows[0]["fare"]);
                    if (lBaseFare > 0)
                    {
                        lSARFare = Convert.ToDecimal(ldtFare.Rows[0]["SARfare"]);
                        lMinAllowed = Convert.ToInt32(ldtFare.Rows[0]["minallowed"]);
                        lMaxAllowed = Convert.ToInt32(ldtFare.Rows[0]["maxallowed"]);
                        lFinalAmtWOST = (lBaseFare * totalPax);
                        if (chkSingle.Checked)
                        {
                            lFinalAmtWOST += (Convert.ToInt32(txtSinglePax.Text) * lSARFare);

                        }
                        else
                        {
                            //lFinalAmtWOST += (Convert.ToInt32(txtSinglePax.Text) * 0);
                            lFinalAmtWOST = lFinalAmtWOST + 0;
                        }
                        lFinalAmtWOST = lFinalAmtWOST - Convert.ToDecimal(hdSPLDiscount.Value);//For Discount Cal
                                                                                               //------------New section added to add the child wiht matress and without matress fare----//

                        if (IsChildPaxAvailable == 1)
                        {
                            lFinalAmtWOST = lFinalAmtWOST + (ConvertStringint(hdfTotalChildWithMatress.Value) * ConvertStringDecimal(hdfChildWithMatressFare.Value)) + (ConvertStringint(hdfTotalChildWithoutMatress.Value) * ConvertStringDecimal(hdfChildWithoutMatressFare.Value));
                        }
                        //----------------------------------End---------------------------------------------------//

                        lServiceTax = lFinalAmtWOST * (Convert.ToDecimal(txtServiceTax.Value) / 100);
                        lFinalAmt = Math.Round((lFinalAmtWOST + lServiceTax), 0, MidpointRounding.AwayFromZero);

                        if (Convert.ToDecimal(txtFareTotal.Text) == lFinalAmt)
                        {
                            return true;
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg",
                                "alert('Fares not matching please check and try again.');", true);
                            //return false;
                            return true;
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "alert('No Fare available for this tour on selected date.');",
                            true);
                        return false;
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alertmsg", "alert('No Fare available for this tour on selected date.');",
                        true);
                    return false;
                }
            }
            finally
            {
                if (ldtFare != null)
                {
                    ldtFare.Dispose();
                }
            }
        }
        private void MangeChildFaresection()
        {
            if (IsChildPaxAvailable == 1)
            {
                tblChildPax.Visible = true;
                lblNoOfpax.Text = "No Of Adult";
                string str = txtHiddenValue.Value;

                //if (IsPostBack)
                //{
                //    //txtFareTotal.Text=hdfCaltotalFare.Value;
                //    //txtTotalServiceTax.Text=hdfCalServicetax.Value;
                //    //txtDiscount.Text = hdfCalDiscount.Value == "" ? "0" : hdfCalDiscount.Value;
                //    //txtAdvance.Text = hdfcalAdvance.Value == "" ? "0" : hdfcalAdvance.Value; ;

                //    //  if (!string.IsNullOrEmpty(txtDate.Value) && Convert.ToString(ddlCarType.SelectedValue) != "-1" && ddlNoOfPax.SelectedValue != "0")
                //  //  ScriptManager.RegisterStartupScript(this, this.GetType(), "getfareAfterPostback", "GetTourFare();", true);
                //}

            }
            else
            {
                tblChildPax.Visible = false;
                lblNoOfpax.Text = "No Of Pax";
            }

        }

        private DataTable fnGetSpecialTourCarType(int? lTourID)
        {

            DataTable pdtTable = null;
            DataSet dsSpecialCar = null;
            int returnType = 0;

            string SpecialCarSP = StoredProcedures.GetSpecialTourCarType_sp;
            SqlParameter[] param = new SqlParameter[2];

            try
            {

                param[0] = new SqlParameter("@I_TourID", lTourID);
                param[1] = new SqlParameter("@O_ReturnValue", returnType);

                dsSpecialCar = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, SpecialCarSP, param);
                pdtTable = dsSpecialCar.Tables[0];
                return pdtTable;

            }
            catch (Exception ex)
            {
                pdtTable = null;
                return pdtTable;
            }
            finally
            {

            }

        }
        private DataTable GetSpecialTourVehiclePax(int vehicleId)
        {
            DataTable dt = new DataTable();
            DataSet dsSpecialTourVehilcePax = new DataSet();

            try
            {
                string VehiclePaxSP = StoredProcedures.GetSplTourCarVehPax_New_sp;
                SqlParameter[] param = new SqlParameter[3];

                param[0] = new SqlParameter("@JDate", fldJourneyDate);
                param[1] = new SqlParameter("@TourID", strTourId);
                param[2] = new SqlParameter("@VehicleID", vehicleId);

                dsSpecialTourVehilcePax = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, VehiclePaxSP, param);
                dt = dsSpecialTourVehilcePax.Tables[0];
                return dt;
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dsSpecialTourVehilcePax != null)
                {
                    dsSpecialTourVehilcePax = null;
                }
                if (dt != null)
                {
                    dt = null;
                }
            }
            return dt;
        }
        private bool IsValidateChildPax()
        {
            bool ret = true;
            if (IsChildPaxAvailable == 1)
            {
                int MaxPax = ConvertStringint(hdfMaxVehiclePax.Value);
                int VehiclePaxSelected = ConvertStringint(ddlNoOfPax.SelectedItem.Text);
                int ChildWithoutMatress = ConvertStringint(txtChildWhoutMatress.Text);
                int ChildWithMatress = ConvertStringint(txtChildWithMatress.Text);

                if ((VehiclePaxSelected + ChildWithoutMatress + ChildWithMatress) > MaxPax)
                {
                    RegisterStartupScript("Error2", "<Script>alert('Total pax including child pax should be less then or equal to vehicle max pax</Script>");
                    ret = false;
                }
            }
            return ret;
        }

        /// <summary>
        /// Get special Tour's Fare
        /// </summary>
        /// <param name="pTourID"></param>
        /// <param name="pJourneyDate"></param>
        /// <returns></returns>

        public DataSet GetSpecialTourFare(int pTourID, DateTime pJourneyDate, int pCategoryID, int pPaxID)
        {
            DataSet lds = new DataSet();
            DataTable dt = null;
            SqlConnection con = null;
            SqlCommand cmd = null;
            SqlDataAdapter da = null;

            try
            {
                con = new SqlConnection(DataLib.getConnectionString());
                cmd = new SqlCommand(StoredProcedures.GetSpecialTourFare_sp, con);
                cmd.CommandType = CommandType.StoredProcedure;

                // Input parameters
                cmd.Parameters.AddWithValue("@I_TourID", pTourID);
                cmd.Parameters.AddWithValue("@I_TourDate", pJourneyDate);
                cmd.Parameters.AddWithValue("@I_CategoryID", pCategoryID);
                cmd.Parameters.AddWithValue("@I_PaxID", pPaxID);

                // Output parameter
                SqlParameter outputParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int);
                outputParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outputParam);

                da = new SqlDataAdapter(cmd);
                da.Fill(lds);

                //---------------------Setting the child Fare--------------
                try
                {
                    if (lds.Tables.Count > 1)
                    {
                        dt = lds.Tables[1];

                        // Child With Mattress
                        DataRow[] dr1 = dt.Select("VehicleCode='ChildWithMattress'");
                        if (dr1.Length > 0)
                        {
                            hdfChildWithMatressFare.Value = Convert.ToString(dr1[0]["fare"]);
                        }

                        // Child Without Mattress
                        DataRow[] dr2 = dt.Select("VehicleCode='ChildWithoutMattress'");
                        if (dr2.Length > 0)
                        {
                            hdfChildWithoutMatressFare.Value = Convert.ToString(dr2[0]["fare"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Optional: log ex
                }

                return lds;
            }
            catch (Exception ex)
            {
                // Optional: log ex
                return null;
            }
            finally
            {
                if (da != null) da.Dispose();
                if (cmd != null) cmd.Dispose();
                if (con != null) con.Dispose();
            }
        }

        private int GetPaxId()
        {
            //int PaxID = 0;
            //int totalChildToAdjust = 0;
            //int childWithMatress = 0;
            //int childWithoutMatress = 0;

            //if (IsChildPaxAvailable == 1) // in the current tour child with  bed and child withot bed availbale
            //{
            //    hdfTotalChildWithMatress.Value = txtChildWithMatress.Text;
            //    hdfTotalChildWithoutMatress.Value = txtChildWhoutMatress.Text;

            //    int MinAllowed = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[1]);
            //    int MaxAllowed = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[2]);

            //    hdfMaxAllowedInVehilce.Value = MaxAllowed.ToString();
            //    hdfMinAllowedInVehicle.Value = MinAllowed.ToString();
            //    hdfNoOfPaxSelected.Value = ddlNoOfPax.SelectedItem.Text;
            //    int totalAdultSelected = ConvertStringint(hdfNoOfPaxSelected.Value);

            //    childWithMatress = ConvertStringint(txtChildWithMatress.Text);
            //    childWithoutMatress = ConvertStringint(txtChildWhoutMatress.Text);
            //    int LeftChild = 0;


            //    if (MaxAllowed > totalAdultSelected)
            //    {
            //        totalChildToAdjust = MaxAllowed - totalAdultSelected;
            //    }
            //    if (childWithMatress > 0)
            //    {
            //        if (childWithMatress >= totalChildToAdjust)
            //        {
            //            //- print 'under child with matress'
            //            childWithoutMatress = childWithoutMatress - totalChildToAdjust;
            //            totalChildToAdjust = 0;
            //            // --print @childwihtmatrss
            //        }
            //        else if (childWithMatress < totalChildToAdjust)
            //        {

            //            LeftChild = totalChildToAdjust - childWithMatress;
            //            childWithMatress = 0;
            //            totalChildToAdjust = LeftChild;
            //        }
            //    }


            //    if (LeftChild > 0)
            //        totalChildToAdjust = LeftChild;

            //    if (totalChildToAdjust > 0)
            //    {
            //        if (childWithoutMatress > 0)
            //        {
            //            if (childWithoutMatress >= totalChildToAdjust)
            //            {
            //                childWithoutMatress = childWithoutMatress - totalChildToAdjust;
            //            }
            //            else if (childWithoutMatress < totalChildToAdjust)
            //            {
            //                LeftChild = totalChildToAdjust - childWithoutMatress;
            //                childWithoutMatress = 0;
            //            }
            //        }
            //    }
            //    hdfTotalChildWithMatress.Value = childWithMatress.ToString();
            //    hdfTotalChildWithoutMatress.Value = childWithoutMatress.ToString();
            //    txtHiddenFare.Value = (ConvertStringDecimal(txtHiddenFare.Value) + (ConvertStringint(hdfTotalChildWithMatress.Value) * ConvertStringDecimal(hdfChildWithMatressFare.Value)) + (ConvertStringint(hdfTotalChildWithoutMatress.Value) * ConvertStringDecimal(hdfChildWithoutMatressFare.Value))).ToString();

            //}
            //PaxID = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[0]);
            //return PaxID;


            int PaxID = 0;
            int totalChildToAdjust = 0;
            int childWithMatress = 0;
            int childWithoutMatress = 0;

            if (IsChildPaxAvailable == 1) // in the current tour child with  bed and child withot bed availbale
            {
                hdfTotalChildWithMatress.Value = txtChildWithMatress.Text;
                hdfTotalChildWithoutMatress.Value = txtChildWhoutMatress.Text;

                int MinAllowed = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[1]);
                int MaxAllowed = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[2]);

                hdfMaxAllowedInVehilce.Value = MaxAllowed.ToString();
                hdfMinAllowedInVehicle.Value = MinAllowed.ToString();
                hdfNoOfPaxSelected.Value = ddlNoOfPax.SelectedItem.Text;
                int totalAdultSelected = ConvertStringint(hdfNoOfPaxSelected.Value);

                childWithMatress = ConvertStringint(txtChildWithMatress.Text);
                childWithoutMatress = ConvertStringint(txtChildWhoutMatress.Text);
                int LeftChild = 0;


                if (MaxAllowed > totalAdultSelected)
                {
                    totalChildToAdjust = MaxAllowed - totalAdultSelected;
                }
                if (childWithMatress > 0)
                {
                    if (childWithMatress >= totalChildToAdjust)
                    {
                        //- print 'under child with matress'
                        childWithoutMatress = childWithoutMatress - totalChildToAdjust;
                        totalChildToAdjust = 0;
                        // --print @childwihtmatrss
                    }
                    else if (childWithMatress < totalChildToAdjust)
                    {

                        LeftChild = totalChildToAdjust - childWithMatress;
                        childWithMatress = 0;
                        totalChildToAdjust = LeftChild;
                    }
                }


                if (LeftChild > 0)
                    totalChildToAdjust = LeftChild;

                if (totalChildToAdjust > 0)
                {
                    if (childWithoutMatress > 0)
                    {
                        if (childWithoutMatress >= totalChildToAdjust)
                        {
                            childWithoutMatress = childWithoutMatress - totalChildToAdjust;
                        }
                        else if (childWithoutMatress < totalChildToAdjust)
                        {
                            LeftChild = totalChildToAdjust - childWithoutMatress;
                            childWithoutMatress = 0;
                        }
                    }
                }
                hdfTotalChildWithMatress.Value = childWithMatress.ToString();
                hdfTotalChildWithoutMatress.Value = childWithoutMatress.ToString();
                txtHiddenFare.Value = (ConvertStringDecimal(txtHiddenFare.Value) + (ConvertStringint(hdfTotalChildWithMatress.Value) * ConvertStringDecimal(hdfChildWithMatressFare.Value)) + (ConvertStringint(hdfTotalChildWithoutMatress.Value) * ConvertStringDecimal(hdfChildWithoutMatressFare.Value))).ToString();

            }
            PaxID = Convert.ToInt32(ddlNoOfPax.SelectedValue.Split(pvSeatSplitter)[0]);
            return PaxID;
        }

        public decimal ConvertStringDecimal(string decimalString)
        {
            Decimal decval = 0.00M;
            if (string.IsNullOrEmpty(decimalString) || decimalString == "&nbsp;")
            {
                decval = 0.00M;
            }
            else
            {
                try
                {
                    decval = Convert.ToDecimal(string.Format("{0:F2}", decimalString));
                }
                catch (Exception ex)
                {
                    decval = 0.00M;
                }
            }
            return decval;
        }

        public int ConvertStringint(string stringint)
        {
            int value = 0;
            try
            {
                value = Convert.ToInt32(stringint);
            }
            catch (Exception ex)
            {
                value = 0;
            }
            return value;

        }

        public static string GetIntegerFromDecimal(object DecimalString)
        {
            string strinteger = string.Empty;
            string strDecimalString = Convert.ToString(DecimalString);
            if (strDecimalString.IndexOf(".") > 0)
            {
                strinteger = strDecimalString.Substring(0, strDecimalString.IndexOf("."));
            }
            else
            {
                strinteger = strDecimalString;
            }
            return strinteger;
        }

        public static string RoundDecimalToStrIntegerl(object DecimalString)
        {
            string strinteger = string.Empty;
            string strDecimalString = Convert.ToString(DecimalString);
            decimal decval = 0.00M;

            try
            {
                decval = Convert.ToDecimal(string.Format("{0:F2}", DecimalString));
            }
            catch (Exception ex)
            {
                decval = 0.00M;
            }

            strDecimalString = Convert.ToString(decval);
            if (strDecimalString.ToString().IndexOf(".") > 0)
            {
                strinteger = strDecimalString.Substring(0, strDecimalString.IndexOf("."));
            }
            else
            {
                strinteger = strDecimalString;
            }
            return strinteger;
        }

        private void BindCountryNameNew(int lregionID)
        {
            DataTable ldtcountry = null;
            ClsAdo clsObj = new ClsAdo();
            try
            {
                ddlNationality.Items.Clear();
                ddlCountry.Items.Clear();
                ldtcountry = clsObj.fnGetCountry(lregionID);
                if (ldtcountry != null && ldtcountry.Rows.Count > 0)
                {
                    // ReArrangeCountry(ref ldtcountry);
                    ddlNationality.DataSource = ldtcountry;
                    ddlNationality.DataTextField = "country_name";
                    ddlNationality.DataValueField = "country_Id";
                    ddlNationality.DataBind();
                    ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));

                    ddlCountry.DataSource = ldtcountry;
                    ddlCountry.DataTextField = "country_name";
                    ddlCountry.DataValueField = "country_Id";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlNationality.Items.Clear();
                    ddlCountry.Items.Clear();
                }
            }
            catch { }
            finally
            {
                if (ldtcountry != null)
                {
                    ldtcountry.Dispose();
                    ldtcountry = null;
                }
            }
        }

        private void ReArrangeCountry(ref DataTable ldtcountry)
        {
            if (ldtcountry != null && ldtcountry.Rows.Count > 0)
            {
                DataRow[] foundRows;
                string filter = "country_name = 'India'";
                foundRows = ldtcountry.Select(filter);
                int lIndex = 0;
                foreach (DataRow dr in foundRows)
                {
                    DataRow ldr = ldtcountry.NewRow();
                    ldr.ItemArray = dr.ItemArray;
                    lIndex = dr.Table.Rows.IndexOf(dr);
                    //ldtcountry.AcceptChanges();
                    ldtcountry.Rows.InsertAt(ldr, 0);
                    ldtcountry.Rows.RemoveAt(lIndex + 1);
                    ldtcountry.AcceptChanges();
                    break;
                }
            }
        }

        private void BindStateName_New()
        {
            DataSet dscars = null;
            DataListResponse<GetCountryName_SPResult> lGetCountryName = null;
            DataListResponse<GetCountryWiseStateName_SPResult> lGetStateName = null;
            clsContractModule clsHCobj = new clsContractModule();
            ddlState.Items.Clear();
            try
            {
                lGetCountryName = new DataListResponse<GetCountryName_SPResult>();
                //lGetStateName = new DataListResponse<GetCountryWiseStateName_SPResult>();
                int lCountryID = 1;
                //if (ClsCommon.ConvertStringint(ddlCountry.SelectedValue) > 0)
                //    lCountryID = ClsCommon.ConvertStringint(ddlCountry.SelectedValue);
                clsHCobj.fldConnString = DataLib.getConnectionString();
                lGetCountryName = clsHCobj.fnGetCountryName(0);
                //lGetStateName = clsHCobj.fnGetCountryWiseStateName(lCountryID);
                if (lGetCountryName != null)
                {
                    //ddlNationality.DataSource = lGetCountryName.ResultList;
                    //ddlNationality.DataTextField = "CountryName";
                    //ddlNationality.DataValueField = "CountryID";
                    //ddlNationality.DataBind();
                    //ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));
                    //ddlNationality.SelectedValue = "1";

                    //ddlState.DataSource = lGetStateName.ResultList;
                    //ddlState.DataTextField = "StateName";
                    //ddlState.DataValueField = "StateID";
                    //ddlState.DataBind();
                    //ddlState.Items.Insert(0, new ListItem("State", "0"));
                }


                string GetServiceChargeDetails = StoredProcedures.GST_GetStateByCountryId_SP;
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@i_CountryID", "1"); //to get the all service list
                dscars = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, GetServiceChargeDetails, param);

                if (dscars.Tables[0] != null && dscars.Tables[0].Rows.Count > 0)
                {
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateName";

                    ddlState.DataSource = dscars.Tables[0];
                    ddlState.DataBind();

                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
                    //ddlState.Items.Add(new ListItem("other", "-1"));
                }
                else
                {
                    ddlState.DataSource = null;
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
                }


            }
            finally
            {
                if (clsHCobj != null)
                {
                    clsHCobj = null;
                }
                if (lGetStateName != null)
                {
                    lGetStateName = null;
                }
            }
        }

        //private void BindStateName()
        //{
        //    //DataListResponse<GetCountryName_SPResult> lGetCountryName = null;
        //    DataListResponse<GetCountryWiseStateName_SPResult> lGetStateName = null;
        //    clsContractModule clsHCobj = new clsContractModule();
        //    ddlState.Items.Clear();
        //    try
        //    {
        //        //lGetCountryName = new DataListResponse<GetCountryName_SPResult>();
        //        lGetStateName = new DataListResponse<GetCountryWiseStateName_SPResult>();
        //        int lCountryID = 1;
        //        //if (ClsCommon.ConvertStringint(ddlCountry.SelectedValue) > 0)
        //        //    lCountryID = ClsCommon.ConvertStringint(ddlCountry.SelectedValue);
        //        clsHCobj.fldConnString = ClsCommon.fldConnectionString;
        //        //lGetCountryName = clsHCobj.fnGetCountryName(0);
        //        lGetStateName = clsHCobj.fnGetCountryWiseStateName(lCountryID);
        //        if (lGetStateName.Status.Status)
        //        {
        //            //ddlNationality.DataSource = lGetCountryName.ResultList;
        //            //ddlNationality.DataTextField = "CountryName";
        //            //ddlNationality.DataValueField = "CountryID";
        //            //ddlNationality.DataBind();
        //            //ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));
        //            //ddlNationality.SelectedValue = "1";

        //            ddlState.DataSource = lGetStateName.ResultList;
        //            ddlState.DataTextField = "StateName";
        //            ddlState.DataValueField = "StateID";
        //            ddlState.DataBind();
        //            ddlState.Items.Insert(0, new ListItem("State", "0"));
        //        }
        //    }
        //    finally
        //    {
        //        if (clsHCobj != null)
        //        {
        //            clsHCobj = null;
        //        }
        //        if (lGetStateName != null)
        //        {
        //            lGetStateName = null;
        //        }
        //    }
        //}

        //private void BindCityName()
        //{
        //    DataListResponse<GetCityName_SPResult> lGetCityName = null;
        //    clsContractModule clsHCobj = new clsContractModule();
        //    ddlCity.Items.Clear();
        //    try
        //    {

        //        lGetCityName = new DataListResponse<GetCityName_SPResult>();
        //        int lStateID1 = -2;
        //        if (ClsCommon.ConvertStringint(ddlState.SelectedValue) > 0)
        //            lStateID1 = ClsCommon.ConvertStringint(ddlState.SelectedValue);
        //        clsHCobj.fldConnString = ClsCommon.fldConnectionString;
        //        lGetCityName = clsHCobj.fnGetCityName(lStateID1);

        //        if (lGetCityName.Status.Status)
        //        {
        //            ddlCity.DataSource = lGetCityName.ResultList;
        //            ddlCity.DataTextField = "CityName";
        //            ddlCity.DataValueField = "CityName";
        //            ddlCity.DataBind();
        //            ddlCity.Items.Insert(0, new ListItem("City", "0"));
        //        }
        //    }
        //    finally
        //    {
        //        if (clsHCobj != null)
        //        {
        //            clsHCobj = null;
        //        }
        //        if (lGetCityName != null)
        //        {
        //            lGetCityName = null;
        //        }
        //    }
        //}

        [System.Web.Script.Services.ScriptMethod()]
        [WebMethod]
        public static string[] GetCity(string prefixText, int count, int? contextKey)
        {
            int? lStateID1 = null;
            if (contextKey > 0)
                lStateID1 = contextKey;

            GST_Data obj = new GST_Data();
            List<GST_GetCityListByStateIdAndSearchedCityTextResult> objCity = obj.GST_GetCityListByStateIdAndSearchedCityText(prefixText, lStateID1);
            List<string> txtItems = new List<string>();
            String dbValues;
            foreach (GST_GetCityListByStateIdAndSearchedCityTextResult item in objCity)
            {
                dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.CityName.ToString(), item.CityID.ToString());
                txtItems.Add(dbValues);
            }
            return txtItems.ToArray();
        }

        #endregion
    }
    class clsCarPax
    {
        string pvPaxID = string.Empty;
        string pvDisplayString = string.Empty;
        public string fldPaxID
        {
            get
            {
                return pvPaxID;
            }
            set
            {
                pvPaxID = value;
            }
        }
        public string fldDisplayString
        {
            get
            {
                return pvDisplayString;
            }
            set
            {
                pvDisplayString = value;
            }
        }
        public clsCarPax(string pPaxID, string pDisplayString)
        {
            fldPaxID = pPaxID;
            fldDisplayString = pDisplayString;
        }
    }
}