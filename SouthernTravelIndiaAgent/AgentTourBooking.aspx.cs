using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentTourBooking : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        protected int Tourno, BusSeaterType, busno, AvailSeat, i;
        protected int[] busserial = new int[30];
        protected string Tourname, orderid, BusEnvType, taddress, tempstr, tDACFare, tDNACFare, strChkBustType = "", tAWFACFAre, tAWFnonacfare, tCWFACFAre, tCWFnonacfare;
        protected string tAACFAre, tA2ACFare, tA2NACFare, tA3ACFare, tA3NACFare, tCBACFare, tCBNACFare, tSACFare;
        protected string tSNACFare, tAnonacfare, strBookedDate, tCACFAre, tCnonacfare, tdeparttime, strhidden;
        protected int TourSerial, Toursrowid, busallotrowid, seatarrangRowid;
        protected int[] nseats = new int[100];
        protected int pickupMrowid, rid, pickuppointid, MasterRowId;

        protected string seater, strhidden1, strhiddenjdate, strhiddenAvailByjdateAC, strhiddenAvailByjdateNAC;
        protected System.DateTime selecteddate;
        protected string datestring = "", autoPost = "false";
        protected int PreviousAdults = 0, PreviousChilds = 0, totpax = 0;
        protected StringBuilder Chart;
        protected Int32 rows, max;
        string strValue = "";

        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["mode"] = "";
            if (Request.QueryString["date1"] != null)
            {
                autoPost = Request.QueryString["date1"];
            }
            if (Session["AgentId"] == null)
            {
                //Response.Redirect("sessionexpire.aspx");
                Response.Redirect("agentlogin.aspx");
            }
            if (Session["AgentId"] != null)
            {
                sess.Value = "0";
            }
            else if (Session["EndUserId"] != null)
            {
                sess.Value = "1";
            }
            ClsAdo pClsLinq = null;
            try
            {


                pClsLinq = new ClsAdo();
                if ((Request.QueryString["orderid"] == "") && (Request.QueryString["orderid"] == null))
                {
                    if (Session["blockStatus_Agent"] != null)
                    {
                        string str = Convert.ToString(Session["blockStr_Agent"]);
                        string[] seat = str.Split('#');
                        doUnBlock(seat[0], Convert.ToInt16(seat[1]));
                        Session["blockStatus_Agent"] = null;
                        Session["blockStr_Agent"] = null;
                        refresh();
                    }
                }
                if (Convert.ToString(Request.QueryString["tourid"]) != "")
                {
                    if (Request.QueryString["jdate"] != null)
                    {
                        string jsdate = (Convert.ToString(Request.QueryString["jdate"]));
                        // ViewState["DiscountChk"] = validateDiscount(Convert.ToString(jsdate.ToString()));
                        discount.Value = validateDiscount(Convert.ToString(jsdate.ToString()));
                        string acnonac = getvacantseats(Convert.ToInt32(DataLib.funClear(Request.QueryString["Tourid"])),
                            Convert.ToString(Request.QueryString["jdate"]));
                        string[] ac = acnonac.Split('^');
                        lblavailcheck.Text = "(" + ac[0] + " Seats Available)";
                        lblNONACAVAIL.Text = "(" + ac[1] + " Seats Available)";
                    }
                    if (Request.QueryString["jdate"] != null)
                    {
                        strChkBustType = ChkBusType(Convert.ToString(Convert.ToString(Request.QueryString["jdate"])));
                        BuschkType.Value = strChkBustType;
                    }

                    #region Optimize Code
                    /*string chktour = "select isaccomodation from tourmaster where tourno=" + 
                    Convert.ToInt32(DataLib.funClear(Request.QueryString["tourid"])) + "";
                string tourchk = DataLib.GetStringData(DataLib.Connection.ConnectionString, chktour);
                string tt = "";
                if (tourchk.ToUpper() == "Y")
                {
                    tt = "select TaxValue from ServiceTaxMaster where TaxType='ATP'";
                }
                else
                {
                    tt = "select TaxValue from ServiceTaxMaster where TaxType='NATP'";
                }
                service.Value = DataLib.GetStringData(DataLib.Connection.ConnectionString, tt);
                credit.Value = DataLib.GetStringData(DataLib.Connection.ConnectionString, 
                    "select TaxValue from ServiceTaxMaster where TaxType='CC'");*/
                    #endregion
                    service.Value = pClsLinq.fnGetServiceTaxIsAcc(Convert.ToInt32(
                        Request.QueryString["Tourid"]));
                    credit.Value = DataLib.GetserviceTax("CC");
                    hdServiceChargeTax.Value = Convert.ToString(pClsLinq.fnGetServiceTaxValue("SCTax"));
                    short pinDex = 0;


                    if (!Page.IsPostBack)
                    {

                        fillddlJdate(Convert.ToInt32(Request.QueryString["Tourid"]));
                        string dd = Dispup(Convert.ToInt32(Request.QueryString["Tourid"]));
                        if (dd == "No")
                        {
                            table5.Visible = false;
                            ClientScript.RegisterStartupScript(GetType(), "NoPickup",
                                "<script>alert('Sorry!.. There is no Pickup Places Currently');</script>");
                            return;
                        }
                        hidTourId.Value = Request.QueryString["Tourid"];
                        table5.Visible = false;
                        //table6.Visible = false;
                        if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
                        {
                            OrderIDH.Value = Request.QueryString["orderid"];
                            PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                            PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                            prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                            prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                            order.Value = Convert.ToString(Request.QueryString["orderid"]);
                            hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;
                        }
                    }
                    if (Request.QueryString["jdate"] != null)
                    {
                        //DateTime lBlockDateFrom = new DateTime(2014, 12, 15);
                        //DateTime lBlockDateTo = new DateTime(2015, 1, 10);
                        //if (Convert.ToDateTime(Convert.ToString(Request.QueryString["jdate"])) >= lBlockDateFrom && Convert.ToDateTime(Convert.ToString(Request.QueryString["jdate"])) <= lBlockDateTo)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert",
                        //            "alert('For Travel bookings from 15-Dec-2014 to 10-Jan-2015(both dates inclusive) Kindly call us at 1800-11-0606 or contact our respective branches.');window.location ='../SeatRequestForm.aspx?TourName=" + lblTourName.Text + "&spl=N';", true);
                        //}
                        table5.Visible = true;
                        //table6.Visible = true;
                        MasterRowId = 0;
                        if (ddlPickUp.Items.Count > 0)
                            MasterRowId = Convert.ToInt32(ddlPickUp.Items[pinDex].Value);
                        DateTime jd = Convert.ToDateTime(Request.QueryString["jdate"].ToString());
                        int tourid = Convert.ToInt32(Request.QueryString["tourid"]);
                        //getfaregrid(jd, tourid);
                        GetTourFare(jd, tourid);
                        string dd1 = Dispup(Convert.ToInt32(Request.QueryString["Tourid"]));
                        if (dd1 == "No")
                        {
                            table5.Visible = false;
                            ClientScript.RegisterStartupScript(GetType(), "NoPickup1", "<script>alert('Sorry!.. There is no Pickup Places Currently');</script>");
                            return;
                        }
                        short i = oo(MasterRowId.ToString());
                        if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
                        {
                            OrderIDH.Value = Request.QueryString["orderid"];
                            PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                            PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                            prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                            prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                            order.Value = Convert.ToString(Request.QueryString["orderid"]);
                            hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;
                        }
                        if (Request.QueryString["ltc"] != null)
                        {
                            #region Optimize Code
                            /*decimal ltCharges = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, 
                            "select isnull(LTCCharges,0) from Tbl_LTCCharges where rowid=1"));*/
                            #endregion
                            decimal ltCharges = Convert.ToDecimal(pClsLinq.fnGetLTCCharges());
                            lblLTC.Text = " Extra @" + ltCharges.ToString() + "% for LTC ";
                        }
                    }
                    else
                    {
                        Chart = new StringBuilder();
                        Chart.Append("");
                    }
                    if (Request.QueryString["pickUpIndex"] != null)
                        pinDex = short.Parse(Request.QueryString["pickUpIndex"].ToString());
                    ddlPickUp.SelectedIndex = pinDex;
                }
                else
                {
                    table5.Visible = false;
                    btnContinuee.Visible = false;
                    colorindication.Visible = false;
                }
                btnContinuee.Visible = false;
                colorindication.Visible = false;
                btncheckavail.Attributes.Add("onclick", "return checkradio();");
                btncheckavail.Attributes.Add("onclick", "return Displayfare();");
                btncheckavail.Attributes.Add("onclick", "return checkOnsubmit();return chkfield();");
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }

            }
        }
        protected void btncheckavail_Click(object sender, EventArgs e)
        {

            if (IsHelicopterAvailable(ClsCommon.ConvertStringint(Request.QueryString["Tourid"]), Convert.ToString(Request.QueryString["jdate"])) == false)
            {
                ClsCommon.ShowAlert("The Helicopter tickets are not available on this date. Please choose other date");
                return;
            }

            rbtnPaymentOption.Style.Add("display", "none");
            if (Convert.ToInt32(hdNoofDays.Value) > 14)
            {
                rbtnPaymentOption.Style.Add("display", "block");
            }


            ViewState["chkbusno"] = "a";
            btncheckavail.Enabled = false;
            btncheckavail.Visible = false;


            txtNoOfAdults.ReadOnly = true;
            txtNoOfChilds.ReadOnly = true;
            txtNoOfAdultsTriple.ReadOnly = true;
            txtNoOfAdultsTwin.ReadOnly = true;
            txtNoOfSingles.ReadOnly = true;
            txtNoOfChildBed.ReadOnly = true;
            txtNoofdormitory.ReadOnly = true;
            txtNoAWFNoOfAdults.ReadOnly = true;
            txtNoCWFNoOfChilds.ReadOnly = true;

            tAACFAre = lblAACfare.Text.ToString();
            tAnonacfare = lblANACfare.Text.ToString();
            tCACFAre = lblCACfare.Text.ToString();
            tCnonacfare = lblCNACfare.Text.ToString();
            tA2ACFare = lblA2ACfare.Text.ToString();
            tA2NACFare = lblA2NACfare.Text.ToString();
            tA3ACFare = lblA3ACfare.Text.ToString();
            tA3NACFare = lblA3NACfare.Text.ToString();
            tCBACFare = lblCBACfare.Text.ToString();
            tCBNACFare = lblCBNACfare.Text.ToString();
            tSACFare = lblSACfare.Text.ToString();
            tSNACFare = lblSNACfare.Text.ToString();
            tDACFare = lbldACfare.Text.ToString();
            tDNACFare = lbldNACfare.Text.ToString();
            tAWFACFAre = lblAWFfare.Text.ToString();
            tAWFnonacfare = lblAWFNACfare.Text.ToString();
            tCWFACFAre = lblCWFfare.Text.ToString();
            tCWFnonacfare = lblCWFNACfare.Text.ToString();
            btncheckavail.Enabled = false;

            ViewState["pickUpIndex"] = ddlPickUp.SelectedValue;
            lblPickupPlace.Text = hidPickupPlace.Value;
            lblDepTime.Text = hidDepTime.Value;

            if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
            {
                OrderIDH.Value = Request.QueryString["orderid"];
                PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                order.Value = Convert.ToString(Request.QueryString["orderid"]);
                hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;
            }
            else
            {
                OrderIDH.Value = DataLib.pnr();
            }
            orderid = OrderIDH.Value;
            Tourno = Convert.ToInt32(Request.QueryString["Tourid"]);
            if (traf.Visible == true)
            {
                if ((txtNoOfAdults.Text == "") && (txtNoOfChilds.Text == ""))
                {
                    totpax = 0;
                }
                else
                {
                    totpax = Convert.ToInt32(txtNoOfAdults.Text.ToString().Trim()) + Convert.ToInt32(txtNoOfChilds.Text.ToString().Trim());
                }
            }
            if (trAWF.Visible == true)
            {
                if ((txtNoAWFNoOfAdults.Text == "") && (txtNoCWFNoOfChilds.Text == ""))
                {
                    totpax = 0;
                }
                else
                {
                    totpax = totpax + Convert.ToInt32(txtNoAWFNoOfAdults.Text.ToString().Trim()) + Convert.ToInt32(txtNoCWFNoOfChilds.Text.ToString().Trim());
                }
            }
            if ((tra2f.Visible == true) || (tra3f.Visible == true) || (trsf.Visible == true) || (tradf.Visible == true))
            {
                if ((txtNoOfAdultsTwin.Text == "") && (txtNoOfAdultsTriple.Text == "") && (txtNoOfChildBed.Text == "")
                    && (txtNoOfSingles.Text == "") && (txtNoofdormitory.Text == ""))
                {
                    totpax = 0;
                }
                else
                {
                    totpax = Convert.ToInt32(txtNoOfAdultsTwin.Text.ToString().Trim()) + Convert.ToInt32(txtNoOfAdultsTriple.Text.ToString().Trim())
                        + Convert.ToInt32(txtNoOfChildBed.Text.ToString().Trim()) + Convert.ToInt32(txtNoOfSingles.Text.ToString().Trim())
                        + Convert.ToInt32(txtNoofdormitory.Text.ToString().Trim());
                }
            }
            if ((tra2f.Visible == true) || (tra3f.Visible == true) || (trsf.Visible == true) || (tradf.Visible == true))
            {
                if ((txtNoOfAdultsTwin.Text == "") && (txtNoOfAdultsTriple.Text == "") && (txtNoOfChildBed.Text == "") &&
                    (txtNoOfSingles.Text == "") && (txtNoofdormitory.Text == ""))
                {
                    totpax = 0;
                }
                else
                {
                    totpax = Convert.ToInt32(txtNoOfAdultsTwin.Text.ToString().Trim()) + Convert.ToInt32(txtNoOfAdultsTriple.Text.ToString().Trim()) +
                        Convert.ToInt32(txtNoOfChildBed.Text.ToString().Trim()) + Convert.ToInt32(txtNoOfSingles.Text.ToString().Trim()) +
                        Convert.ToInt32(txtNoofdormitory.Text.ToString().Trim());
                }
            }
            if (totpax == 0)
            {
                Response.Write("<script>alert('Please Enter No OF Passengers')</script>");
                btncheckavail.Enabled = true;
                return;
            }
            string avail;
            string[] ss;
            int availaseat;
            int serialno = 0;
            avail = ChekAvailability1(totpax, Tourno, Convert.ToDateTime(ddlJdate.SelectedValue));
            ss = avail.Split('-');
            string busNo = "";
            if (ss.Length > 1)
            {
                availaseat = Convert.ToInt32(ss[0]);
                serialno = Convert.ToInt32(ss[1]);
                busNo = Convert.ToString(ss[2]);
                GetAlertAccordingToBusNo(Convert.ToInt32(busNo), Convert.ToDateTime(ddlJdate.SelectedValue), Tourno);

            }
            else
            {
                availaseat = Convert.ToInt32(ss[0]);
            }
            maxSeatAllowed.Value = Convert.ToString(totpax);
            if (Request.QueryString["Tourid"] != null)
            {
                if (Convert.ToString(Request.QueryString["Tourid"]) == "131")
                {
                    if (!CheackSeatsForFD_Tour(Convert.ToInt32(Request.QueryString["Tourid"]), Convert.ToDateTime(ddlJdate.SelectedValue), 0, totpax))
                    {
                        //ClsCommon.ShowAlert("Currently there is No Available seats on your selected date.");
                        //this.RegisterStartupScript("Error", "<script>alert('Currently No seats are available for this tour with helicopter on your selected date. Please book another tour without helicopter.');window.location.href='agenthomepage.aspx';</script>");
                        ScriptManager.RegisterStartupScript(this, typeof(string), "Message", "if(confirm('Currently No seats are available for this tour with helicopter on your selected date. Please book another tour without helicopter.')){window.open('agenthomepage.aspx','_self');}else{}", true);
                        return;
                    }
                }
            }
            if (availaseat >= totpax)
            {
                btnContinuee.Visible = true;
                colorindication.Visible = true;
                btncheckavail.Visible = false;
                Chart = new StringBuilder();
                Chart.Append(DataLib.seatarr(Convert.ToString(BusSeaterType), serialno, Convert.ToInt16(busNo)));
                Chart.Append("<script>Displayfare();</script>");
            }
            else
            {
                //for multiple busseating chart Purpose
                if (Convert.ToString(ViewState["MultiPle"]) == "Y")
                {
                    string ser = "";
                    string bt = "";
                    string bno = "";
                    avail = ChekAvailabilitymultiple(totpax, Tourno, Convert.ToDateTime(ddlJdate.SelectedValue));
                    ss = avail.Split('-');
                    if (ss.Length > 1)
                    {
                        availaseat = Convert.ToInt32(ss[0]);
                        ser = Convert.ToString(ss[1]);
                        bt = Convert.ToString(ss[2]);
                        bno = Convert.ToString(ss[3]);
                    }
                    else
                    {
                        availaseat = Convert.ToInt32(ss[0]);
                    }
                    if (availaseat >= totpax)
                    {
                        btnContinuee.Visible = true;
                        colorindication.Visible = true;
                        btncheckavail.Visible = false;
                        string[] sss = ser.Split('$');
                        string[] btt = bt.Split('@');
                        string[] bbno = bno.Split('*');
                        StringBuilder st = new StringBuilder();
                        for (int k = 0; k <= sss.Length - 1; k++)
                        {
                            st.Append(DataLib.seatarr(btt[k], Convert.ToInt32(sss[k]), Convert.ToInt32(bbno[k])));
                            Chart = new StringBuilder();
                            Chart.Append(st);
                            Chart.Append("<script>Displayfare();</script>");
                        }
                    }
                    else
                    {
                        btnContinuee.Visible = false;
                        colorindication.Visible = false;
                        if (!(Request.QueryString["Rowid"] == null))
                        {
                            if (availaseat == 0)
                                lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                                    "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                            else
                            {
                                string multi = Convert.ToString(ViewState["MultiPle"]);
                                int tt = Convert.ToInt32(ViewState["tot"]);
                                if (multi == "Y")
                                {
                                    lbMsgErr.Text = "Currently there are " + tt + " available seats on Multiple Buses for this date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request" +
                                        "</a> Click here";
                                }
                                else
                                {
                                    lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request" +
                                        "</a> Click here";
                                }
                            }
                            hlmsgerr.Text = "Click here to change date.";
                            hlmsgerr.NavigateUrl = "Agentbookedtour.aspx?orderid=" + orderid;
                        }
                        else
                        {
                            if (availaseat == 0)
                                lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                                    "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                            else
                            {
                                string multi = Convert.ToString(ViewState["MultiPle"]);
                                int tt = Convert.ToInt32(ViewState["tot"]);
                                if (multi == "Y")
                                {
                                    lbMsgErr.Text = "Currently there are " + tt + " available seats on Multiple Buses for this date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request" +
                                        "</a> Click here";
                                }
                                else
                                {
                                    lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request" +
                                        "</a> Click here";
                                }
                            }
                            hlmsgerr.Text = "Please Choose another Tour booking.";
                            hlmsgerr.NavigateUrl = "agenthomepage.aspx";
                        }
                    }
                }
                else
                {
                    if (availaseat == 0)
                        lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                            "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                    else
                    {
                        lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> You Want to " +
                            "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                    }
                    hlmsgerr.Text = "Please Choose another Tour booking.";
                    hlmsgerr.NavigateUrl = "agenthomepage.aspx";
                }
                //================END==================
            }
            btncheckavail.Enabled = true;
            btnContinuee.Attributes.Add("onclick", "return checkseats();");
        }


        protected void btnContinuee_Click(object sender, EventArgs e)
        {
            string seat, numbers = "", chkbus = "", chkbus1 = "";
            string[] seatnumbers;
            seat = optedSeatNos.Value.Replace(",,", ",").TrimStart(',').TrimEnd(',');
            seatnumbers = seat.Split(',');
            for (int i = 0; i <= seatnumbers.Length - 1; i++)
            {
                if (seatnumbers[i].Length > 2)
                {
                    numbers = "s" + seatnumbers[i].Substring(1, 2);
                    chkbus = seatnumbers[i].Substring(0, 1);
                }
                else
                {
                    numbers = "s" + seatnumbers[i].Substring(1, 1);
                    chkbus = seatnumbers[i].Substring(0, 1);
                }
                tempstr = tempstr + "," + numbers;
                if (chkbus1 == "")
                {
                    chkbus1 = chkbus;
                }
                else
                {
                    chkbus1 = chkbus1 + "," + chkbus;
                }
            }
            tempstr = tempstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
            chkbus1 = chkbus1.Replace(",,", ",").TrimStart(',').TrimEnd(',');
            ViewState["chkbusno"] = chkbus1;
            string avail;
            string[] ss;
            int availaseat;
            int serialno = 0;
            avail = ChekAvailability1(seatnumbers.Length, Convert.ToInt16(Request.QueryString["Tourid"]), Convert.ToDateTime(ddlJdate.SelectedValue));
            ss = avail.Split('-');
            if (ss.Length > 1)
            {
                availaseat = Convert.ToInt32(ss[0]);
                serialno = Convert.ToInt32(ss[1]);
            }
            else
            {
                availaseat = Convert.ToInt32(ss[0]);
            }
            //for re checking the seats which were avilable or not
            if (availaseat < seatnumbers.Length)
            {
                if (Convert.ToString(ViewState["MultiPle"]) == "Y")
                {
                    string ser = "";
                    string bt = "";
                    string bno = "";
                    avail = ChekAvailabilitymultiple(seatnumbers.Length, Convert.ToInt16(Request.QueryString["Tourid"]),
                        Convert.ToDateTime(ddlJdate.SelectedValue));
                    ss = avail.Split('-');
                    if (ss.Length > 1)
                    {
                        availaseat = Convert.ToInt32(ss[0]);
                        ser = Convert.ToString(ss[1]);
                        bt = Convert.ToString(ss[2]);
                        bno = Convert.ToString(ss[3]);
                        if (bno.Contains("2"))
                            GetAlertAccordingToBusNo(2, Convert.ToDateTime(ddlJdate.SelectedValue), Tourno);

                    }
                    else
                    {
                        availaseat = Convert.ToInt32(ss[0]);
                    }
                    if (availaseat < seatnumbers.Length)
                    {
                        btnContinuee.Visible = false;
                        colorindication.Visible = false;
                        if (!(Request.QueryString["Rowid"] == null))
                        {
                            if (availaseat == 0)
                                lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                                    "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                            else
                            {
                                string multi = Convert.ToString(ViewState["MultiPle"]);
                                int tt = Convert.ToInt32(ViewState["tot"]);
                                if (multi == "Y")
                                {
                                    lbMsgErr.Text = "Currently there are " + tt + " available seats on Multiple Buses for this date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> " +
                                        "Click here";
                                }
                                else
                                {
                                    lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> " +
                                        "Click here";
                                }
                            }
                            hlmsgerr.Text = "Click here to change date.";
                            hlmsgerr.NavigateUrl = "Agentbookedtour.aspx?orderid=" + orderid;
                        }
                        else
                        {
                            if (availaseat == 0)
                                lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                                    "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                            else
                            {
                                string multi = Convert.ToString(ViewState["MultiPle"]);
                                int tt = Convert.ToInt32(ViewState["tot"]);
                                if (multi == "Y")
                                {
                                    lbMsgErr.Text = "Currently there are " + tt + " available seats on Multiple Buses for this date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request" +
                                        "</a> Click here";
                                }
                                else
                                {
                                    lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> " +
                                        "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request" +
                                        "</a> Click here";
                                }
                            }
                            hlmsgerr.Text = "Please Choose another Tour booking.";
                            hlmsgerr.NavigateUrl = "agenthomepage.aspx";
                        }
                    }
                    else
                    {
                        if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
                        {
                            OrderIDH.Value = Request.QueryString["orderid"];
                            /* PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                             PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                             prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                             prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                             order.Value = Convert.ToString(Request.QueryString["orderid"]);
                             hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;*/
                        }
                        else
                        {
                            //if (OrderIDH.Value == "")
                            //{
                            //    OrderIDH.Value = DataLib.pnr();
                            //}
                        }
                        seat = optedSeatNos.Value.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                        seatnumbers = seat.Split(',');
                        string[] serial = ser.Split('$');
                        string ttstr = "";
                        string sno = "";
                        for (int jj = 0; jj <= serial.Length - 1; jj++)
                        {
                            ttstr = "";
                            Array.Sort(seatnumbers);
                            for (int i = 0; i <= seatnumbers.Length - 1; i++)
                            {
                                if (Convert.ToInt32(seatnumbers[i].Substring(0, 1)) == jj + 1)
                                {
                                    if (seatnumbers[i].Length > 2)
                                    {
                                        numbers = "s" + seatnumbers[i].Substring(1, 2);
                                    }
                                    else
                                    {
                                        numbers = "s" + seatnumbers[i].Substring(1, 1);
                                    }
                                    ttstr = ttstr + "," + numbers;
                                }
                                //tempstr = tempstr + "," + numbers;
                            }
                            if (sno == "")
                            {
                                sno = serial[jj];
                            }
                            else
                            {
                                sno = sno + "," + serial[jj];
                            }
                            ttstr = ttstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                            //doBlock(tempstr, Convert.ToInt32(serial[jj]));
                            string seatcheck = doBlock(ttstr, Convert.ToInt32(serial[jj]));
                            if (seatcheck == "No")
                            {
                                ClientScript.RegisterStartupScript(GetType(), "check1", "<script>alert('Sorry, These Seats were Booked, " +
                                    "Please Choose Another Seats');</script>");
                                return;
                            }
                        }
                        tempstr = "";
                        for (int i = 0; i <= seatnumbers.Length - 1; i++)
                        {
                            numbers = "s" + seatnumbers[i];
                            tempstr = tempstr + "," + numbers;
                        }
                        tempstr = tempstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                        tAACFAre = lblAACfare.Text.ToString();
                        tAnonacfare = lblANACfare.Text.ToString();
                        tCACFAre = lblCACfare.Text.ToString();
                        tCnonacfare = lblCNACfare.Text.ToString();
                        tA2ACFare = lblA2ACfare.Text.ToString();
                        tA2NACFare = lblA2NACfare.Text.ToString();
                        tA3ACFare = lblA3ACfare.Text.ToString();
                        tA3NACFare = lblA3NACfare.Text.ToString();
                        tCBACFare = lblCBACfare.Text.ToString();
                        tCBNACFare = lblCBNACfare.Text.ToString();
                        tSACFare = lblSACfare.Text.ToString();
                        tSNACFare = lblSNACfare.Text.ToString();
                        tDACFare = lbldACfare.Text.ToString();
                        tDNACFare = lbldNACfare.Text.ToString();
                        tAWFACFAre = lblAWFfare.Text.ToString();
                        tAWFnonacfare = lblAWFNACfare.Text.ToString();
                        tCWFACFAre = lblCWFfare.Text.ToString();
                        tCWFnonacfare = lblCWFNACfare.Text.ToString();
                        if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
                        {
                            // OrderIDH.Value = Request.QueryString["orderid"];
                            PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                            PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                            prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                            prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                            order.Value = Convert.ToString(Request.QueryString["orderid"]);
                            hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;
                        }
                        else
                        {
                            //if (OrderIDH.Value == "")
                            //{
                            //    OrderIDH.Value = DataLib.pnr();
                            //}
                        }
                        if (!(Request.QueryString["Rowid"] == null))
                        {
                            updatebook(Convert.ToInt16(Request.QueryString["Tourid"]), seatnumbers.Length,
                                Convert.ToDateTime(ddlJdate.SelectedValue), OrderIDH.Value, Convert.ToInt32(Request.QueryString["Rowid"]), sno);
                        }
                        else
                        {
                            insertbook(Convert.ToInt16(Request.QueryString["Tourid"]), seatnumbers.Length,
                                Convert.ToDateTime(ddlJdate.SelectedValue), OrderIDH.Value, sno);
                        }
                    }
                }
                else
                {
                    btnContinuee.Visible = false;
                    colorindication.Visible = false;
                    if (!(Request.QueryString["Rowid"] == null))
                    {
                        if (availaseat == 0)
                            lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                                "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                        else
                        {
                            lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> " +
                                "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                        }
                        hlmsgerr.Text = "Click here to change date.";
                        hlmsgerr.NavigateUrl = "Agentbookedtour.aspx?orderid=" + orderid;
                    }
                    else
                    {
                        if (availaseat == 0)
                            lbMsgErr.Text = "Currently there is No Available seats on your selected date <br/> You Want to " +
                                "<a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                        else
                        {
                            lbMsgErr.Text = "Currently there is Only " + availaseat + " available seats on Your Selected date <br/> " +
                                "You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                        }
                        hlmsgerr.Text = "Please Choose another Tour booking.";
                        hlmsgerr.NavigateUrl = "agenthomepage.aspx";
                    }
                }
            }
            else
            {
                if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
                {
                    OrderIDH.Value = Request.QueryString["orderid"];
                    /*PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                    PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                    prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                    prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                    order.Value = Convert.ToString(Request.QueryString["orderid"]);
                    hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;*/
                }
                else
                {
                    OrderIDH.Value = DataLib.pnr();
                }
                string seatcheck = doBlock(tempstr, serialno);
                if (seatcheck == "No")
                {
                    ClientScript.RegisterStartupScript(GetType(), "check2", "<script>alert('Sorry, These Seats were Booked, " +
                        "Please Choose Another Seats');</script>");
                    return;
                }
                tAACFAre = lblAACfare.Text.ToString();
                tAnonacfare = lblANACfare.Text.ToString();
                tCACFAre = lblCACfare.Text.ToString();
                tCnonacfare = lblCNACfare.Text.ToString();
                tA2ACFare = lblA2ACfare.Text.ToString();
                tA2NACFare = lblA2NACfare.Text.ToString();
                tA3ACFare = lblA3ACfare.Text.ToString();
                tA3NACFare = lblA3NACfare.Text.ToString();
                tCBACFare = lblCBACfare.Text.ToString();
                tCBNACFare = lblCBNACfare.Text.ToString();
                tSACFare = lblSACfare.Text.ToString();
                tSNACFare = lblSNACfare.Text.ToString();
                tDACFare = lbldACfare.Text.ToString();
                tDNACFare = lbldNACfare.Text.ToString();
                tAWFACFAre = lblAWFfare.Text.ToString();
                tAWFnonacfare = lblAWFNACfare.Text.ToString();
                tCWFACFAre = lblCWFfare.Text.ToString();
                tCWFnonacfare = lblCWFNACfare.Text.ToString();
                if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
                {
                    //OrderIDH.Value = Request.QueryString["orderid"];
                    PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                    PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                    prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                    prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                    order.Value = Convert.ToString(Request.QueryString["orderid"]);
                    hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;
                }
                else
                {
                    //OrderIDH.Value = DataLib.pnr();
                }
                if (!(Request.QueryString["Rowid"] == null))
                {
                    updatebook(Convert.ToInt16(Request.QueryString["Tourid"]), seatnumbers.Length, Convert.ToDateTime(ddlJdate.SelectedValue),
                        OrderIDH.Value, Convert.ToInt32(Request.QueryString["Rowid"]), Convert.ToString(serialno));
                }
                else
                {
                    insertbook(Convert.ToInt16(Request.QueryString["Tourid"]), seatnumbers.Length, Convert.ToDateTime(ddlJdate.SelectedValue),
                        OrderIDH.Value, Convert.ToString(serialno));
                }
            }
            //==================END==============================      
        }
        protected void btnReset_Click(object sender, EventArgs e)
        {
            txtNoOfAdults.Text = "0";
            txtNoOfChilds.Text = "0";
            txtNoOfAdultsTriple.Text = "0";
            txtNoOfAdultsTwin.Text = "0";
            txtNoOfSingles.Text = "0";
            txtNoOfChildBed.Text = "0";
            txtNoofdormitory.Text = "0";
            txtNoAWFNoOfAdults.Text = "0";
            txtNoCWFNoOfChilds.Text = "0";
            btnContinuee.Visible = false;
            btncheckavail.Visible = true;

            optedSeatNos.Value = "";
            txtremarks.Text = "";
            txtNoOfAdults.ReadOnly = false;
            txtNoOfChilds.ReadOnly = false;
            txtNoOfAdultsTriple.ReadOnly = false;
            txtNoOfAdultsTwin.ReadOnly = false;
            txtNoOfSingles.ReadOnly = false;
            txtNoOfChildBed.ReadOnly = false;
            txtNoofdormitory.ReadOnly = false;
            txtNoAWFNoOfAdults.ReadOnly = false;
            txtNoCWFNoOfChilds.ReadOnly = false;
            rbtnPaymentOption.SelectedIndex = -1;
        }
        #endregion
        #region "Method(s)"
        private bool CheackSeatsForFD_Tour(int pTourID, DateTime pDOJ, int pUserType, int pSeat)
        {
            SqlConnection lConn = null;
            SqlCommand lCommand = null;
            SqlParameter lParam = null;
            DataSet ldsDetail = new DataSet();
            SqlDataAdapter lDataAdp = null;
            bool lFlag = false;
            try
            {
                lConn = new SqlConnection(DataLib.getConnectionString());
                lCommand = new SqlCommand("CheackSeatsForFD_Tour_SP", lConn);
                lCommand.CommandTimeout = 20 * 1000;
                lCommand.CommandType = CommandType.StoredProcedure;

                lParam = new SqlParameter("@I_TourID", SqlDbType.Int);
                lParam.Value = pTourID;
                lCommand.Parameters.Add(lParam);

                lParam = new SqlParameter("@I_DOJ", SqlDbType.SmallDateTime);
                lParam.Value = pDOJ;
                lCommand.Parameters.Add(lParam);

                lParam = new SqlParameter("@I_UserType", SqlDbType.Int);
                lParam.Value = pUserType;
                lCommand.Parameters.Add(lParam);


                lParam = new SqlParameter("@o_ReturnValue", SqlDbType.Int);
                lParam.Value = "0";
                lParam.Direction = ParameterDirection.Output;
                lCommand.Parameters.Add(lParam);

                lParam = new SqlParameter("@o_Availability", SqlDbType.Int);
                lParam.Value = "0";
                lParam.Direction = ParameterDirection.Output;
                lCommand.Parameters.Add(lParam);



                if (lConn.State == ConnectionState.Closed)
                {
                    lConn.Open();
                }
                lCommand.ExecuteNonQuery();
                int pStatus = Convert.ToInt32(lCommand.Parameters["@o_ReturnValue"].Value);
                int pAvailability = Convert.ToInt32(lCommand.Parameters["@o_Availability"].Value);
                if (pStatus > 0)
                {
                    if (pSeat <= pAvailability)
                    {
                        lFlag = true;
                    }
                }
            }
            catch (Exception ex) { }
            finally
            {
                if (lConn != null)
                {
                    if (lConn.State != ConnectionState.Closed)
                    {
                        lConn.Close();
                    }
                    lConn.Dispose();
                    lConn = null;
                }
                if (lCommand != null)
                {
                    lCommand.Dispose();
                    lCommand = null;
                }
                if (ldsDetail != null)
                {
                    ldsDetail.Dispose();
                    ldsDetail = null;
                }
                if (lDataAdp != null)
                {
                    lDataAdp.Dispose();
                    lDataAdp = null;
                }
            }
            return lFlag;
        }
        short oo(string strRowId)
        {
            ClsAdo pclsObj = null;
            DataTable dtAddress = null;
            try
            {
                pclsObj = new ClsAdo();
                dtAddress = pclsObj.fnGetPickupDetail(Convert.ToInt32(strRowId));
                lblFare.Text = "";
                hdAServiceChargeFare.Value = "0"; hdCServiceChargeFare.Value = "0";
                if (dtAddress != null && dtAddress.Rows.Count > 0)
                {
                    lblPickupPlace.Text = Convert.ToString(dtAddress.Rows[0]["Address"].ToString());
                    lblDepTime.Text = Convert.ToString(dtAddress.Rows[0]["departtime"].ToString());
                    //lblFare.Text = Convert.ToString(dtAddress.Rows[0]["Fare"].ToString());
                    if (Convert.ToDecimal(dtAddress.Rows[0]["AdultFare"].ToString()) > 0)
                    {
                        lblFare.Text = "<b>Current selected Pickup Point`s Service charge is  (Adult / Child)  : <span class=rupee>`</span>" + Convert.ToString(dtAddress.Rows[0]["AdultFare"].ToString())
                           + "/- <span class=rupee>`</span> " + Convert.ToString(dtAddress.Rows[0]["ChildFare"].ToString()) + "/-.</b>";
                        hdAServiceChargeFare.Value = Convert.ToString(dtAddress.Rows[0]["AdultFare"].ToString());
                        hdCServiceChargeFare.Value = Convert.ToString(dtAddress.Rows[0]["ChildFare"].ToString());
                    }
                }
                else
                {
                    lblPickupPlace.Text = "No Pickup Address ";
                    lblDepTime.Text = "No Departure Time";
                    lblFare.Text = "";
                    hdAServiceChargeFare.Value = "0"; hdCServiceChargeFare.Value = "0";
                }
                return Convert.ToInt16(dtAddress.Rows.Count);
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (dtAddress != null)
                {
                    dtAddress.Dispose();
                    dtAddress = null;
                }
            }
        }
        public void GetTourFare(DateTime pJourneyDate, int pTourID)
        {
            char lIsLTC = 'N';
            DataSet ldsTourFare = null;
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                ldsTourFare = clsObj.fnGetFixedTour_Fare(pTourID, pJourneyDate, lIsLTC, "Agent");
                if (ldsTourFare != null && ldsTourFare.Tables.Count > 0 && ldsTourFare.Tables[0].Rows.Count > 0)
                {
                    string strValue = "";
                    decimal newAcFare = 0.0m, newAcValue = 0.0m, newNAcFare = 0.0m, newNAcValue = 0.0m;
                    if (discount.Value != "4")
                        strValue = Convert.ToString(ldsTourFare.Tables[1].Rows[0][0]);
                    if (ldsTourFare.Tables[0].Rows.Count > 0)
                    {
                        for (int i = 0; i < ldsTourFare.Tables[0].Rows.Count; i++)
                        {
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(ldsTourFare.Tables[0].Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Math.Round(Convert.ToDecimal(
                                    ldsTourFare.Tables[0].Rows[i]["ACFare"]) - newAcValue, 0);
                            }
                            else
                            {
                                newAcFare = Math.Round(Convert.ToDecimal(
                                    ldsTourFare.Tables[0].Rows[i]["ACFare"]), 0);
                                strValue = "0";
                            }
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newNAcValue = Convert.ToDecimal(ldsTourFare.Tables[0].Rows[i]["NonACFare"]);
                                newNAcValue = ((newNAcValue * Convert.ToDecimal(strValue)) / 100);
                                newNAcFare = Math.Round(Convert.ToDecimal(
                                    ldsTourFare.Tables[0].Rows[i]["NonACFare"]) - newNAcValue, 0);
                            }
                            else
                            {
                                newNAcFare = Math.Round(Convert.ToDecimal(
                                    ldsTourFare.Tables[0].Rows[i]["NonACFare"]), 0);
                                strValue = "0";
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 1)
                            {
                                this.lblAACfare.Text = Convert.ToString(newAcFare);
                                this.lblANACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 2)
                            {
                                this.lblA2ACfare.Text = Convert.ToString(newAcFare);
                                this.lblA2NACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 3)
                            {
                                this.lblA3ACfare.Text = Convert.ToString(newAcFare);
                                this.lblA3NACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 4)
                            {
                                this.lblCBACfare.Text = Convert.ToString(newAcFare);
                                this.lblCBNACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 5)
                            {
                                this.lblSACfare.Text = Convert.ToString(newAcFare);
                                this.lblSNACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 6)
                            {
                                this.lblCACfare.Text = Convert.ToString(newAcFare);
                                this.lblCNACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 7)
                            {
                                this.lbldACfare.Text = Convert.ToString(newAcFare);
                                this.lbldNACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 8)
                            {
                                this.lblAWFfare.Text = Convert.ToString(newAcFare);
                                this.lblAWFNACfare.Text = Convert.ToString(newNAcFare);
                            }
                            if (Convert.ToInt32(ldsTourFare.Tables[0].Rows[i]["Rowid"]) == 9)
                            {
                                this.lblCWFfare.Text = Convert.ToString(newAcFare);
                                this.lblCWFNACfare.Text = Convert.ToString(newNAcFare);
                            }
                        }
                        if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) &&
                            ((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")))
                        {
                            traf.Visible = false;
                            tr7.Visible = false;
                        }
                        if (((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")) &&
                            ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                        {
                            trcf.Visible = false;
                            tr8.Visible = false;
                        }
                        if (((this.lblAWFfare.Text == "0") || (this.lblAWFfare.Text == "")) &&
                           ((this.lblAWFNACfare.Text == "0") || (this.lblAWFNACfare.Text == "")))
                        {
                            trAWF.Visible = false;
                            trAWFColor.Visible = false;
                        }
                        if (((this.lblCWFfare.Text == "0") || (this.lblCWFfare.Text == "")) &&
                            ((this.lblCWFNACfare.Text == "0") || (this.lblCWFNACfare.Text == "")))
                        {
                            trCWF.Visible = false;
                            trCWFColor.Visible = false;
                        }
                        if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) &&
                            ((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")))
                        {
                            tra2f.Visible = false;
                            tr9.Visible = false;
                        }
                        if (((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) &&
                            ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")))
                        {
                            tra3f.Visible = false;
                            tr10.Visible = false;
                        }
                        if (((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) &&
                            ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")))
                        {
                            trcbf.Visible = false;
                            tr11.Visible = false;
                        }
                        if (((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) &&
                            ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")))
                        {
                            trsf.Visible = false;
                        }
                        if (((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")) &&
                            ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                        {
                            tradf.Visible = false;
                            tr12.Visible = false; tr8.Visible = false;
                        }
                        if ((tra2f.Visible) || (tra3f.Visible) || (trcbf.Visible) ||
                            (trsf.Visible) || (tradf.Visible))
                        {
                            if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) &&
                                ((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) &&
                                ((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) &&
                                ((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) &&
                                ((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")))
                            {
                                RadAC.Enabled = false;
                                RadNAC.Checked = true;
                            }
                            if (((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")) &&
                                ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")) &&
                                ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")) &&
                                ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")) &&
                                ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                            {
                                RadNAC.Enabled = false;
                                RadAC.Checked = true;
                            }
                        }
                        if ((traf.Visible) || (trcf.Visible))
                        {
                            if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) &&
                                ((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")))
                            {
                                RadAC.Enabled = false;
                                RadNAC.Checked = true;
                            }
                            if (((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")) &&
                                ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                            {
                                RadNAC.Enabled = false;
                                RadAC.Checked = true;
                            }
                        }
                        if ((trAWF.Visible) || (trCWF.Visible))
                        {
                            if (((this.lblAWFfare.Text == "0") || (this.lblAWFfare.Text == "")) &&
                                ((this.lblCWFfare.Text == "0") || (this.lblCWFfare.Text == "")))
                            {
                                RadAC.Enabled = false;
                                RadNAC.Checked = true;
                            }
                            if (((this.lblAWFNACfare.Text == "0") || (this.lblAWFNACfare.Text == "")) &&
                                ((this.lblCWFNACfare.Text == "0") || (this.lblCWFNACfare.Text == "")))
                            {
                                RadNAC.Enabled = false;
                                RadAC.Checked = true;
                            }
                        }
                    }
                }
                else
                {
                    RadNAC.Enabled = false;
                    RadAC.Enabled = false;
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldsTourFare != null)
                {
                    ldsTourFare.Dispose();
                    ldsTourFare = null;
                }
            }
        }
        /// <summary>
        /// Not Use this Function
        /// </summary>
        /// <param name="jd"></param>
        /// <param name="tourid"></param>
        public void getfaregrid(DateTime jd, Int32 tourid)
        {
            DataTable date;
            DataTable cfare;
            DataTable rfare;
            date = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select * from revisedtourfare where tourno=" + tourid + " and isaffected='Y'");
            if (date.Rows.Count > 0)
            {
                if (jd.Date != null)
                {
                    if (Convert.ToDateTime(date.Rows[0]["Affectedfrom"].ToString()) <= jd.Date)
                    {
                        if (discount.Value != "4")
                        {
                            string strDis = "select discount from tourmaster where tourno=" + tourid + " and IsDiscountActive='Y' and Activated='Y' and isAgent='Y'";
                            strValue = DataLib.GetStringData(DataLib.Connection.ConnectionString, strDis);
                        }
                        rfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select a.tourid,a.acfare,a.nonacfare,b.Rowid,b.category,b.isaccommodation from tbl_revisedfaremaster a,tbl_tourcategory b where a.tourid=" + tourid + " and a.activated='Y' and a.categoryid=b.rowid");

                        decimal newAcFare = 0.0m, newAcValue = 0.0m, newNonAcfare = 0.0m, newNonAcValue = 0.0m;

                        if (rfare.Rows.Count > 0)
                        {
                            for (int i = 0; i < rfare.Rows.Count; i++)
                            {
                                #region "Commented"
                                /* if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                                {
                                    this.lblAACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                                {
                                    this.lblCACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                                {
                                    this.lblANACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                                {
                                    this.lblCNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                                {
                                    this.lblA2ACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                                {
                                    this.lblA3ACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                                {
                                    this.lblCBACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lblSACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                                {
                                    this.lbldACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }

                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                                {
                                    this.lblA2NACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                                {
                                    this.lblA3NACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                                {
                                    this.lblCBNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lblSNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    this.lbldNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                }*/

                                #endregion

                                //---------Code To Implemt Discount Starts..................
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                                {
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblAACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblAACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }

                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                                {

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblCACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                    //this.lblCACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 1)
                                {
                                    //  this.lblANACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblANACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblANACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 6)
                                {
                                    // this.lblCNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblCNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                                {
                                    // this.lblA2ACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblA2ACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA2ACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                                {
                                    //this.lblA3ACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblA3ACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA3ACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }

                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                                {
                                    // this.lblCBACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblCBACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCBACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    //this.lblSACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblSACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblSACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                                {
                                    //this.lbldACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lbldACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lbldACfare.Text = Convert.ToString(rfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 2)
                                {
                                    //this.lblA2NACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblA2NACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA2NACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 3)
                                {
                                    //this.lblA3NACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblA3NACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA3NACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 4)
                                {
                                    //this.lblCBNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblCBNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCBNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 5)
                                {
                                    //this.lblSNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblSNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblSNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(rfare.Rows[i]["Rowid"]) == 7)
                                {
                                    //this.lbldNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(rfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lbldNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lbldNACfare.Text = Convert.ToString(rfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }

                                //---------Code To Implemt Discount ENDS..................
                            }
                            if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) && ((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")))
                            {

                                traf.Visible = false;
                                tr7.Visible = false;
                            }
                            if (((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")) && ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                            {

                                trcf.Visible = false;
                                tr8.Visible = false;
                            }
                            if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) && ((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")))
                            {

                                tra2f.Visible = false;
                                tr9.Visible = false;
                            }
                            if (((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) && ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")))
                            {

                                tra3f.Visible = false;
                                tr10.Visible = false;
                            }
                            if (((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) && ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")))
                            {

                                trcbf.Visible = false;
                                tr11.Visible = false;
                            }
                            if (((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) && ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")))
                            {

                                trsf.Visible = false;
                            }
                            if (((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")) && ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                            {
                                tradf.Visible = false;
                                tr12.Visible = false; tr8.Visible = false;
                            }
                            if ((tra2f.Visible) || (tra3f.Visible) || (trcbf.Visible) || (trsf.Visible) || (tradf.Visible))
                            {
                                if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) && ((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) && ((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) && ((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) && ((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")))
                                {
                                    RadAC.Enabled = false;
                                    RadNAC.Checked = true;
                                }
                                if (((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")) && ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")) && ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")) && ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")) && ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                                {
                                    RadNAC.Enabled = false;
                                    RadAC.Checked = true;
                                }
                            }
                            if ((traf.Visible) || (trcf.Visible))
                            {
                                if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) && ((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")))
                                {
                                    RadAC.Enabled = false;
                                    RadNAC.Checked = true;
                                }
                                if (((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")) && ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                                {
                                    RadNAC.Enabled = false;
                                    RadAC.Checked = true;
                                }
                            }
                        }
                        else
                        {
                            RadNAC.Enabled = false;
                            RadAC.Enabled = false;
                        }
                    }
                    else
                    {
                        //string strValue = "";
                        if (discount.Value != "4")
                        {
                            string strDis = "select discount from tourmaster where tourno=" + tourid + " and IsDiscountActive='Y' and Activated='Y' and isAgent='Y'";
                            strValue = DataLib.GetStringData(DataLib.Connection.ConnectionString, strDis);
                        }

                        //  End
                        cfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select b.category,b.Rowid,a.acfare,a.nonacfare from tbl_faremaster a,tbl_tourcategory b where a.tourid=" + tourid + " and a.activated='Y' and a.categoryid=b.rowid");


                        decimal newAcFare = 0.0m, newAcValue = 0.0m, newNonAcfare = 0.0m, newNonAcValue = 0.0m;
                        if (cfare.Rows.Count > 0)
                        {
                            for (int i = 0; i < cfare.Rows.Count; i++)
                            {
                                /* if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                                 {
                                     this.lblAACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                                 {
                                     this.lblCACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                                 {
                                     this.lblANACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                                 {
                                     this.lblCNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                                 {
                                     this.lblA2ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                                 {
                                     this.lblA3ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                                 {
                                     this.lblCBACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                                 {
                                     this.lblSACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                                 {
                                     this.lbldACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                 }

                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                                 {
                                     this.lblA2NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                                 {
                                     this.lblA3NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                                 {
                                     this.lblCBNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                                 {
                                     this.lblSNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }
                                 if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                                 {
                                     this.lbldNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                 }*/



                                // ---------------CODE FOR DISCOUNT STARTS---------------
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                                {
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblAACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblAACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }

                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                                {

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblCACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                    //this.lblCACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                                {
                                    //  this.lblANACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblANACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblANACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                                {
                                    // this.lblCNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblCNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                                {
                                    // this.lblA2ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblA2ACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA2ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                                {
                                    //this.lblA3ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblA3ACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA3ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }

                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                                {
                                    // this.lblCBACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblCBACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCBACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                                {
                                    //this.lblSACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lblSACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblSACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                                {
                                    //this.lbldACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                        this.lbldACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lbldACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                                {
                                    //this.lblA2NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblA2NACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA2NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                                {
                                    //this.lblA3NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblA3NACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblA3NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                                {
                                    //this.lblCBNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblCBNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblCBNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                                {
                                    //this.lblSNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lblSNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lblSNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }
                                if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                                {
                                    //this.lbldNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                                    if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                                    {
                                        newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                        newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                        newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                        this.lbldNACfare.Text = Convert.ToString(newAcFare);
                                    }
                                    else
                                    {
                                        this.lbldNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                        strValue = "0";
                                    }
                                }



                                // COde for Discount Ends 


                            }
                            if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) && ((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")))
                            {

                                traf.Visible = false;
                                tr7.Visible = false;
                            }
                            if (((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")) && ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                            {

                                trcf.Visible = false;
                                tr8.Visible = false;
                            }
                            if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) && ((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")))
                            {

                                tra2f.Visible = false;
                                tr9.Visible = false;
                            }
                            if (((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) && ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")))
                            {

                                tra3f.Visible = false;
                                tr10.Visible = false;
                            }
                            if (((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) && ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")))
                            {

                                trcbf.Visible = false;
                                tr11.Visible = false;
                            }
                            if (((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) && ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")))
                            {

                                trsf.Visible = false;
                            }
                            if (((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")) && ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                            {
                                tradf.Visible = false;
                                tr12.Visible = false; tr8.Visible = false;
                            }
                            if ((tra2f.Visible) || (tra3f.Visible) || (trcbf.Visible) || (trsf.Visible) || (tradf.Visible))
                            {
                                if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) && ((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) && ((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) && ((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) && ((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")))
                                {
                                    RadAC.Enabled = false;
                                    RadNAC.Checked = true;
                                }
                                if (((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")) && ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")) && ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")) && ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")) && ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                                {
                                    RadNAC.Enabled = false;
                                    RadAC.Checked = true;
                                }
                            }
                            if ((traf.Visible) || (trcf.Visible))
                            {
                                if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) && ((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")))
                                {
                                    RadAC.Enabled = false;
                                    RadNAC.Checked = true;
                                }
                                if (((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")) && ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                                {
                                    RadNAC.Enabled = false;
                                    RadAC.Checked = true;
                                }
                            }

                        }
                        else
                        {
                            RadNAC.Enabled = false;
                            RadAC.Enabled = false;
                        }
                    }
                }
            }
            else
            {
                if (discount.Value != "4")
                {
                    string strDis = "select discount from tourmaster where tourno=" + tourid + " and IsDiscountActive='Y' and Activated='Y' and isAgent='Y'";
                    strValue = DataLib.GetStringData(DataLib.Connection.ConnectionString, strDis);
                }

                decimal newAcFare = 0.0m, newAcValue = 0.0m, newNonAcfare = 0.0m, newNonAcValue = 0.0m;
                cfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select b.category,b.Rowid,a.acfare,a.nonacfare from tbl_faremaster a,tbl_tourcategory b where a.tourid=" + tourid + " and a.activated='Y' and a.categoryid=b.rowid");
                if (cfare.Rows.Count > 0)
                {
                    for (int i = 0; i < cfare.Rows.Count; i++)
                    {
                        /*if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                          {
                              this.lblAACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                          {
                              this.lblCACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                          {
                              this.lblANACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                          {
                              this.lblCNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                          {
                              this.lblA2ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                          {
                              this.lblA3ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                          {
                              this.lblCBACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                          {
                              this.lblSACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                          {
                              this.lbldACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                          {
                              this.lblA2NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                          {
                              this.lblA3NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                          {
                              this.lblCBNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                          {
                              this.lblSNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }
                          if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                          {
                              this.lbldNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                          }*/


                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                        {
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lblAACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblAACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }

                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                        {

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lblCACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblCACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }
                            //this.lblCACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 1)
                        {
                            //  this.lblANACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lblANACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblANACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 6)
                        {
                            // this.lblCNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lblCNACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblCNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                        {
                            // this.lblA2ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lblA2ACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblA2ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                        {
                            //this.lblA3ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lblA3ACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblA3ACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }

                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                        {
                            // this.lblCBACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lblCBACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblCBACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                        {
                            //this.lblSACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lblSACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblSACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                        {
                            //this.lbldACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["ACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["ACFare"]) - newAcValue;

                                this.lbldACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lbldACfare.Text = Convert.ToString(cfare.Rows[i]["ACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 2)
                        {
                            //this.lblA2NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lblA2NACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblA2NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 3)
                        {
                            //this.lblA3NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lblA3NACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblA3NACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 4)
                        {
                            //this.lblCBNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lblCBNACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblCBNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 5)
                        {
                            //this.lblSNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lblSNACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lblSNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }
                        if (Convert.ToInt32(cfare.Rows[i]["Rowid"]) == 7)
                        {
                            //this.lbldNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);

                            if (strValue != "" && strValue != null && Convert.ToDecimal(strValue) > 0)
                            {
                                newAcValue = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]);
                                newAcValue = ((newAcValue * Convert.ToDecimal(strValue)) / 100);
                                newAcFare = Convert.ToDecimal(cfare.Rows[i]["NonACFare"]) - newAcValue;

                                this.lbldNACfare.Text = Convert.ToString(newAcFare);
                            }
                            else
                            {
                                this.lbldNACfare.Text = Convert.ToString(cfare.Rows[i]["NonACFare"]);
                                strValue = "0";
                            }
                        }


                    }
                    if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) && ((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")))
                    {

                        traf.Visible = false;
                        tr7.Visible = false;
                    }
                    if (((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")) && ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                    {

                        trcf.Visible = false;
                        tr8.Visible = false;
                    }
                    if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) && ((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")))
                    {

                        tra2f.Visible = false;
                        tr9.Visible = false;
                    }
                    if (((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) && ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")))
                    {

                        tra3f.Visible = false;
                        tr10.Visible = false;
                    }
                    if (((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) && ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")))
                    {

                        trcbf.Visible = false;
                        tr11.Visible = false;
                    }
                    if (((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) && ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")))
                    {

                        trsf.Visible = false;
                    }
                    if (((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")) && ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                    {
                        tradf.Visible = false;
                        tr12.Visible = false; tr8.Visible = false;
                    }
                    if ((tra2f.Visible) || (tra3f.Visible) || (trcbf.Visible) || (trsf.Visible) || (tradf.Visible))
                    {
                        if (((this.lblA2ACfare.Text == "0") || (this.lblA2ACfare.Text == "")) && ((this.lblA3ACfare.Text == "0") || (this.lblA3ACfare.Text == "")) && ((this.lblCBACfare.Text == "0") || (this.lblCBACfare.Text == "")) && ((this.lblSACfare.Text == "0") || (this.lblSACfare.Text == "")) && ((this.lbldACfare.Text == "0") || (this.lbldACfare.Text == "")))
                        {
                            RadAC.Enabled = false;
                            RadNAC.Checked = true;
                        }
                        if (((this.lblA2NACfare.Text == "0") || (this.lblA2NACfare.Text == "")) && ((this.lblA3NACfare.Text == "0") || (this.lblA3NACfare.Text == "")) && ((this.lblCBNACfare.Text == "0") || (this.lblCBNACfare.Text == "")) && ((this.lblSNACfare.Text == "0") || (this.lblSNACfare.Text == "")) && ((this.lbldNACfare.Text == "0") || (this.lbldNACfare.Text == "")))
                        {
                            RadNAC.Enabled = false;
                            RadAC.Checked = true;
                        }
                    }
                    if ((traf.Visible) || (trcf.Visible))
                    {
                        if (((this.lblAACfare.Text == "0") || (this.lblAACfare.Text == "")) && ((this.lblCACfare.Text == "0") || (this.lblCACfare.Text == "")))
                        {
                            RadAC.Enabled = false;
                            RadNAC.Checked = true;
                        }
                        if (((this.lblANACfare.Text == "0") || (this.lblANACfare.Text == "")) && ((this.lblCNACfare.Text == "0") || (this.lblCNACfare.Text == "")))
                        {
                            RadNAC.Enabled = false;
                            RadAC.Checked = true;
                        }
                    }
                }
                else
                {
                    RadNAC.Enabled = false;
                    RadAC.Enabled = false;
                }
            }
        }
        private void fillddlJdate(int TourNo)
        {
            ddlJdate.Items.Clear();
            DateTime TestDate;
            strhidden = "";
            ClsAdo pClsLinq = null;
            DataSet dsdates = null;
            try
            {
                pClsLinq = new ClsAdo();
                if (ddlJdate.Items.Count == 0)
                {
                    #region Commented
                    //ddlJdate.Items.Insert(0, new ListItem("Select date", "0"));

                    ////string dat = "select distinct(JourneyDate) as JourneyDate from Tours where (datediff(\"d\",dateadd(\"d\",1,getdate()),JourneyDate)) >= 0 and TourNo=" + TourNo + " order by journeydate DESC";
                    //string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
                    //hr = "-" + hr;
                    //string dat = "select distinct(JourneyDate) as JourneyDate from Tours where dateadd(hour," + hr + ",journeydate)>=getdate() and TourNo=" + TourNo + " order by journeydate DESC";
                    //DataTable dtdat = DataLib.GetDataTable(DataLib.Connection.ConnectionString, dat);
                    //if (dtdat.Rows.Count > 0)
                    //{
                    //    for (int i = 0; i < dtdat.Rows.Count; i++)
                    //    {
                    //        TestDate = Convert.ToDateTime(dtdat.Rows[i]["JourneyDate"]);
                    //        ddlJdate.Items.Insert(1, new ListItem(TestDate.ToString("d-MMM-yyyy"), TestDate.ToString("MM/dd/yyyy")));
                    //    }
                    //}
                    #endregion
                    ddlJdate.Items.Insert(0, new ListItem("Select date", "0"));
                    string hr = "-" + ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
                    #region Optimize Code
                    //hr = "-" + hr;
                    /*string str = "jdates_vacantseats";
                    SqlParameter[] param = new SqlParameter[2];
                    param[0] = new SqlParameter("@tourno", TourNo);
                    param[1] = new SqlParameter("@jdate", Convert.ToInt32(hr));
                    DataSet dsdates = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, str, param);*/
                    #endregion
                    dsdates = pClsLinq.fnGetjdates_vacantseats(TourNo, Convert.ToInt32(hr));
                    if (dsdates != null && dsdates.Tables[0].Rows.Count > 0)
                    {

                        for (int i = 0; i <= dsdates.Tables[0].Rows.Count - 1; i++)
                        {
                            TestDate = Convert.ToDateTime(dsdates.Tables[0].Rows[i]["sJourneyDate"]);
                            //ddlJdate.Items.Insert(1, new ListItem(TestDate.ToString("d-MMM-yyyy") + " (Ac: " + dsdates.Tables[0].Rows[i]["ac"] + "; Nonac: " + dsdates.Tables[0].Rows[i]["nonac"] + ")", TestDate.ToString("MM/dd/yyyy")));
                            if (Convert.ToInt32(dsdates.Tables[0].Rows[i]["nonac"]) > 0)
                                ddlJdate.Items.Insert(1, new ListItem(TestDate.ToString("dd-MMM-yyyy") + " (Ac: " + dsdates.Tables[0].Rows[i]["ac"] + "; Nonac: " + dsdates.Tables[0].Rows[i]["nonac"] + ")", TestDate.ToString("MM/dd/yyyy")));
                            else
                                ddlJdate.Items.Insert(1, new ListItem(TestDate.ToString("dd-MMM-yyyy") + " (Ac: " + dsdates.Tables[0].Rows[i]["ac"] + ")", TestDate.ToString("MM/dd/yyyy")));
                        }
                    }
                }
                ddlJdate.Items[0].Attributes.Add("Font-Bold", "True");
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (dsdates != null)
                {
                    dsdates.Dispose();
                    dsdates = null;
                }
            }
        }
        private string Dispup(int tourno)
        {
            string pickup = "";
            #region Optimize Code
            //string sqlstr = "select tf.tourid,tf.categoryid,tf.currencyid,tf.ACFare,tf.NonACFare,tf.ExtAmt,tf.Activated, tm.Tour_Short_Key, tm.TourName,tm.isaccomodation,pa.pickupmasterrowid,pa.pickupplace,pa.address,pm.reporttime,pm.departtime, tm.activated from tbl_faremaster tf inner join tourmaster tm on tm.tourno=tf.tourid inner join pickupaddress pa on tm.tourno= pa.tourno inner join pickupmaster pm on pa.pickupmasterrowid= pm.rowid where tm.tourno=" + tourno + " and tm.activated='Y'";

            /*string sqlstr = "select tm.Tour_Short_Key, tm.TourName,tm.isaccomodation,pa.pickupmasterrowid,pa.pickupplace,pa.address,
             * pm.reporttime,pm.departtime, tm.activated from  tourmaster tm  inner join pickupaddress pa on tm.tourno= pa.tourno  
             * inner join pickupmaster pm on pa.pickupmasterrowid= pm.rowid and  upper(pm.activated) ='Y' where tm.tourno=" + tourno + "  
             * and tm.activated='Y'";

            DataTable dtdetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sqlstr);*/
            #endregion
            DataTable dtdetails = null;
            ClsAdo clsObj = null;
            try
            {
                clsObj = new ClsAdo();
                dtdetails = clsObj.fnFixed_PickupAddr_DeptTime(tourno);
                lblFare.Text = "";
                hdAServiceChargeFare.Value = "0";
                hdCServiceChargeFare.Value = "0";
                if (dtdetails != null && dtdetails.Rows.Count > 0)
                {
                    pickup = "Yes";
                    lblTourName.Text = Convert.ToString(dtdetails.Rows[0]["TourName"]);
                    Tourname = lblTourName.Text;
                    lblPickupPlace.Text = Convert.ToString(dtdetails.Rows[0]["Address"]);
                    lblDepTime.Text = Convert.ToString(dtdetails.Rows[0]["DepartTime"]);
                    pickupMrowid = Convert.ToInt32(dtdetails.Rows[0]["Pickupmasterrowid"]);
                    if (Convert.ToDecimal(dtdetails.Rows[0]["AdultFare"].ToString()) > 0)
                    {
                        lblFare.Text = "<b>Current selected Pickup Point`s Service charge is  (Adult / Child)  : <span class=rupee>`</span>" + Convert.ToString(dtdetails.Rows[0]["AdultFare"].ToString())
                            + "/- <span class=rupee>`</span> " + Convert.ToString(dtdetails.Rows[0]["ChildFare"].ToString()) + "/-.</b>";
                        hdAServiceChargeFare.Value = Convert.ToString(dtdetails.Rows[0]["AdultFare"].ToString());
                        hdCServiceChargeFare.Value = Convert.ToString(dtdetails.Rows[0]["ChildFare"].ToString());
                    }
                    if (Convert.ToString(dtdetails.Rows[0]["isaccomodation"]) == "Y")
                    {
                        this.traf.Visible = false;
                        this.trcf.Visible = false;
                        this.trAWF.Visible = false;
                        this.trCWF.Visible = false;
                    }
                    else
                    {
                        this.tra2f.Visible = false;
                        this.tra3f.Visible = false;
                        this.trcbf.Visible = false;
                        this.trsf.Visible = false;
                        this.tradf.Visible = false;
                    }
                }
                else
                {
                    //for pickupplace insert start
                    lblFare.Text = "";
                    hdAServiceChargeFare.Value = "0"; hdCServiceChargeFare.Value = "0";
                    if (ClsCommon.Add_Fixed_PickupPlace(tourno) == 0)
                    {
                        dtdetails = clsObj.fnFixed_PickupAddr_DeptTime(tourno);
                        if (dtdetails != null && dtdetails.Rows.Count > 0)
                        {
                            pickup = "Yes";
                            lblTourName.Text = Convert.ToString(dtdetails.Rows[0]["TourName"]);
                            Tourname = lblTourName.Text;
                            lblPickupPlace.Text = Convert.ToString(dtdetails.Rows[0]["Address"]);
                            lblDepTime.Text = Convert.ToString(dtdetails.Rows[0]["DepartTime"]);
                            pickupMrowid = Convert.ToInt32(dtdetails.Rows[0]["Pickupmasterrowid"]);
                            if (Convert.ToDecimal(dtdetails.Rows[0]["AdultFare"].ToString()) > 0)
                            {
                                lblFare.Text = "<b>Current selected Pickup Point`s Service charge is  (Adult / Child)  : <span class=rupee>`</span>" + Convert.ToString(dtdetails.Rows[0]["AdultFare"].ToString())
                          + "/- <span class=rupee>`</span> " + Convert.ToString(dtdetails.Rows[0]["ChildFare"].ToString()) + "/-.</b>";
                                hdAServiceChargeFare.Value = Convert.ToString(dtdetails.Rows[0]["AdultFare"].ToString());
                                hdCServiceChargeFare.Value = Convert.ToString(dtdetails.Rows[0]["ChildFare"].ToString());
                            }
                            if (Convert.ToString(dtdetails.Rows[0]["isaccomodation"]) == "Y")
                            {
                                this.traf.Visible = false;
                                this.trcf.Visible = false;
                                this.trAWF.Visible = false;
                                this.trCWF.Visible = false;
                            }
                            else
                            {
                                this.tra2f.Visible = false;
                                this.tra3f.Visible = false;
                                this.trcbf.Visible = false;
                                this.trsf.Visible = false;
                                this.tradf.Visible = false;
                            }
                        }
                        else
                        {
                            table5.Visible = false;
                            btnContinuee.Visible = false;
                            colorindication.Visible = false;
                            pickup = "No";
                        }
                    }
                    else
                    {
                        table5.Visible = false;
                        btnContinuee.Visible = false;
                        colorindication.Visible = false;
                        pickup = "No";
                    }

                    #region Optimize Code
                    /*string qry = "select isnull(max(RowId),0) as RowId from pickupmaster(nolock)";
                string strRowid = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
                qry = "select branchname,address+', '+city as address, branchcode,(select isnull(departuretime,0) from tourmaster where tourno=" + tourno + ")as departuretime from branchmaster where branchcode=(select branchcode from tourmaster where tourno=" + tourno + ")";
                DataTable dtadd = DataLib.GetDataTable(DataLib.Connection.ConnectionString, qry);            
                string arrtime = "";
                if (dtadd.Rows.Count > 0)
                {
                    string strPickPlace = Convert.ToString(dtadd.Rows[0]["branchname"]);
                    string strAddress = Convert.ToString(dtadd.Rows[0]["address"]);
                    string bcode = Convert.ToString(dtadd.Rows[0]["branchcode"]);
                    string dept = Convert.ToString(dtadd.Rows[0]["departuretime"]);
                    if (dept.Length > 2)
                    {
                        try
                        {
                            DateTime dt = DateTime.Parse(dept).AddMinutes(-30);
                            string[] ddd = Convert.ToString(dt).Split(' ');
                            arrtime = ddd[1] + " " + ddd[2];
                        }
                        catch
                        {
                        }
                        finally
                        {
                            if (arrtime == "")
                                arrtime = "06:00:00 AM";
                        }
                    }
                    else
                    {
                        arrtime = "06:00:00 AM";
                    }
                    string strarrHrs = arrtime;
                    string strDropHrs = arrtime;
                    string active = "Y";
                    int rowId = Convert.ToInt32(strRowid) + 1;
                    string qryInsert = "insert into pickupmaster(Rowid,tourno,pickupPlace,ReportTime,departTime,activated,branchcode)values(" + rowId + ",'" + tourno + "','" + strPickPlace.Replace("'", "''") + "','" + strarrHrs + "','" + strDropHrs + "','" + active + "','" + bcode + "')";
                    DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, qryInsert, false);

                    string qryInsertAdd = "Insert into pickupAddress(PickupMasterRowId,tourno,pickupplace,address,branchcode) values(" + rowId + ",'" + tourno + "','" + strPickPlace.Replace("'", "''") + "','" + strAddress.Replace("'", "''") + "','" + bcode + "')";
                    DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, qryInsertAdd, false);

                    //sqlstr = "select tf.tourid,tf.categoryid,tf.currencyid,tf.ACFare,tf.NonACFare,tf.ExtAmt,tf.Activated, tm.Tour_Short_Key, tm.TourName,tm.isaccomodation,pa.pickupmasterrowid,pa.pickupplace,pa.address,pm.reporttime,pm.departtime, tm.activated from tbl_faremaster tf inner join tourmaster tm on tm.tourno=tf.tourid inner join pickupaddress pa on tm.tourno= pa.tourno inner join pickupmaster pm on pa.pickupmasterrowid= pm.rowid where tm.tourno=" + tourno + " and tm.activated='Y'";
                    sqlstr = "select tm.Tour_Short_Key, tm.TourName,tm.isaccomodation,pa.pickupmasterrowid,pa.pickupplace,pa.address,pm.reporttime,pm.departtime, tm.activated from  tourmaster tm  inner join pickupaddress pa on tm.tourno= pa.tourno  inner join pickupmaster pm on pa.pickupmasterrowid= pm.rowid and  upper(pm.activated) ='Y' where tm.tourno=" + tourno + "  and tm.activated='Y'";
                    dtdetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, sqlstr);
                    if (dtdetails.Rows.Count > 0)
                    {
                        pickup = "Yes";
                        lblTourName.Text = Convert.ToString(dtdetails.Rows[0]["TourName"]);
                        Tourname = lblTourName.Text;
                        lblPickupPlace.Text = Convert.ToString(dtdetails.Rows[0]["Address"]);
                        lblDepTime.Text = Convert.ToString(dtdetails.Rows[0]["DepartTime"]);
                        pickupMrowid = Convert.ToInt32(dtdetails.Rows[0]["Pickupmasterrowid"]);
                        if (Convert.ToString(dtdetails.Rows[0]["isaccomodation"]) == "Y")
                        {
                            this.traf.Visible = false;
                            this.trcf.Visible = false;
                        }
                        else
                        {
                            this.tra2f.Visible = false;
                            this.tra3f.Visible = false;
                            this.trcbf.Visible = false;
                            this.trsf.Visible = false;
                            this.tradf.Visible = false;
                        }
                    }
                    else
                    {
                        table5.Visible = false;
                        btnContinuee.Visible = false;
                        colorindication.Visible = false;
                        pickup = "No";
                    }
                }
                else
                {
                    table5.Visible = false;
                    btnContinuee.Visible = false;
                    colorindication.Visible = false;
                    pickup = "No";
                }
                //------------end-------------
            }
            return pickup;*/
                    #endregion
                }
                clsObj = new ClsAdo();
                ddlPickUp.DataSource = clsObj.fnGetMultiplePickupPoint(tourno);
                ddlPickUp.DataBind();
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dtdetails != null)
                {
                    dtdetails.Dispose();
                    dtdetails = null;
                }
            }
            return pickup;

        }
        private int GetAvailBusSeat(int CurrentBusSerial)
        {

            int Seat;
            Seat = 0;

            /*string str = "Select * from SeatArrangement(nolock) where TourSerial=" + CurrentBusSerial + "";
            DataTable vacent = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/

            ClsAdo pClsLinq = null;
            DataTable vacent = null;
            try
            {
                pClsLinq = new ClsAdo();
                vacent = pClsLinq.fnGetSeatArrangement(Convert.ToDecimal(CurrentBusSerial));
                if (vacent != null)
                {
                    for (int i = 1; i <= BusSeaterType; i++)
                    {
                        if (vacent.Rows[0]["s" + i] == DBNull.Value)
                        {
                            Seat = Seat + 1;
                            nseats[i] = 1;
                        }
                    }
                }
                return Seat;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (vacent != null)
                {
                    vacent.Dispose();
                    vacent = null;
                }
            }

        }
        private string doBlock(string tstr, int tserial)
        {
            string BlockedString, BlockedString1, BlockedString2, BlockedString3;
            BlockedString1 = Convert.ToString(DateTime.Now.ToString("dd-MMM-yyyy"));
            BlockedString2 = Convert.ToString(DateTime.Now.ToString("HH:mm"));
            BlockedString3 = OrderIDH.Value;
            //BlockedString = "Blocked By EBK0001" + "\n" + BlockedString1 + "\n" + BlockedString2 + "\n" + BlockedString3;
            BlockedString = "Blocked By EBK0001" + "\n" + BlockedString1 + "\n" + BlockedString2 + "\n" + BlockedString3;

            Boolean tt = ClsCommon.Block_Seats(tstr, tserial, BlockedString);
            if (tt == true)
            {
                Session["blockStatus_Agent"] = "blocked";
                Session["blockStr_Agent"] = tstr + "#" + tserial;
                return "Yes";
            }
            else
                return "No";

            #region Optimize Code
            /*tstr = tstr.ToString().PadLeft(tstr.Length - 1);
            string[] arr;
            arr = tstr.ToString().Split(',');
            for (int j = 0; j <= arr.Length - 1; j++)
            {
                if (Convert.ToString(arr[j]) != "")
                {
                    string check = "select " + arr[j] + " from seatarrangement where TourSerial=" + tserial + "";
                    DataTable dtcheck = DataLib.GetDataTable(DataLib.Connection.ConnectionString, check);
                    if (dtcheck.Rows.Count > 0)
                    {
                        if (dtcheck.Rows[0][0] != DBNull.Value)
                        {
                            return "No";
                        }
                    }
                }
            }

            string BlockedString, BlockedString1, BlockedString2, BlockedString3;
            BlockedString1 = Convert.ToString(DateTime.Now.Date);
            BlockedString2 = Convert.ToString(DateTime.Now.ToShortTimeString());
            BlockedString3 = orderid;
            BlockedString = "Blocked By EBK0001" + "\n" + BlockedString1 + "\n" + BlockedString2 + "\n" + BlockedString3;

            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (Convert.ToString(arr[i]) != "")
                {
                    string updat = "update seatarrangement set " + arr[i] + " ='" + BlockedString + "' where TourSerial=" + tserial + "";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, updat, false);
                }
            }
            Session["blockStatus_Agent"] = "blocked";
            Session["blockStr_Agent"] = tstr + "#" + tserial;
            return "Yes";
             * */
            #endregion
        }
        private void doUnBlock(string tstr, int tserial)
        {
            optedSeatNos.Value = "";
            ClsCommon.UnBlock_Seats(tstr, tserial);
            Session["blockStatus_Agent"] = null;
            Session["blockStr_Agent"] = null;
            #region Optimize Code
            /*tstr = tstr.ToString().PadLeft(tstr.Length - 1);
            string[] arr;
            arr = tstr.ToString().Split(',');
            for (int i = 0; i <= arr.Length - 1; i++)
            {
                if (Convert.ToString(arr[i]) != "")
                {
                    string unblock = "update seatarrangement set " + arr[i] + " =null where TourSerial=" + tserial + "";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, unblock, false);
                }
            }*/
            #endregion
        }
        private decimal CalCulateAmount(int adults, int child, int adultstwin, int adultstriple, int childbed, int singleadults, int dormitory, int NoAWFood, int NoCWFood)
        {
            if (tAACFAre == "")
                tAACFAre = "0";
            if (tCACFAre == "")
                tCACFAre = "0";
            if (tA2ACFare == "")
                tA2ACFare = "0";
            if (tA3ACFare == "")
                tA3ACFare = "0";
            if (tCBACFare == "")
                tCBACFare = "0";
            if (tSACFare == "")
                tSACFare = "0";
            if (tAnonacfare == "")
                tAnonacfare = "0";
            if (tCnonacfare == "")
                tCnonacfare = "0";
            if (tA2NACFare == "")
                tA2NACFare = "0";
            if (tA3NACFare == "")
                tA3NACFare = "0";
            if (tCBNACFare == "")
                tCBNACFare = "0";
            if (tSNACFare == "")
                tSNACFare = "0";
            if (tDACFare == "")
                tDACFare = "0";
            if (tDNACFare == "")
                tDNACFare = "0";
            if (tAWFACFAre == "")
                tAWFACFAre = "0";
            if (tCWFACFAre == "")
                tCWFACFAre = "0";
            if (tAWFnonacfare == "")
                tAWFnonacfare = "0";
            if (tCWFnonacfare == "")
                tCWFnonacfare = "0";

            decimal amt = 0;
            if (RadAC.Checked)
            {
                amt = Convert.ToDecimal(tAACFAre) * Convert.ToDecimal(adults);
                amt = amt + (Convert.ToDecimal(tCACFAre) * Convert.ToDecimal(child));
                amt = amt + (Convert.ToDecimal(tA2ACFare) * Convert.ToDecimal(adultstwin));
                amt = amt + (Convert.ToDecimal(tA3ACFare) * Convert.ToDecimal(adultstriple));
                amt = amt + (Convert.ToDecimal(tCBACFare) * Convert.ToDecimal(childbed));
                amt = amt + (Convert.ToDecimal(tSACFare) * Convert.ToDecimal(singleadults));
                amt = amt + (Convert.ToDecimal(tDACFare) * Convert.ToDecimal(dormitory));
                amt = amt + (Convert.ToDecimal(tAWFACFAre) * Convert.ToDecimal(NoAWFood));
                amt = amt + (Convert.ToDecimal(tCWFACFAre) * Convert.ToDecimal(NoCWFood));
            }
            if (RadNAC.Checked)
            {
                amt = (Convert.ToDecimal(tAnonacfare) * Convert.ToDecimal(adults));
                amt = amt + (Convert.ToDecimal(tCnonacfare) * Convert.ToDecimal(child));
                amt = amt + (Convert.ToDecimal(tA2NACFare) * Convert.ToDecimal(adultstwin));
                amt = amt + (Convert.ToDecimal(tA3NACFare) * Convert.ToDecimal(adultstriple));
                amt = amt + (Convert.ToDecimal(tCBNACFare) * Convert.ToDecimal(childbed));
                amt = amt + (Convert.ToDecimal(tSNACFare) * Convert.ToDecimal(singleadults));
                amt = amt + (Convert.ToDecimal(tDNACFare) * Convert.ToDecimal(dormitory));
                amt = amt + (Convert.ToDecimal(tAWFnonacfare) * Convert.ToDecimal(NoAWFood));
                amt = amt + (Convert.ToDecimal(tCWFnonacfare) * Convert.ToDecimal(NoCWFood));
            }
            return decimal.Round(amt);
        }
        private void insertbook(int tourno, int totpax, System.DateTime jdate, string orderid, string BusserialNo)
        {
            decimal afare = 0;
            decimal cfare = 0;
            decimal a2fare = 0;
            decimal a3fare = 0;
            decimal cbfare = 0;
            decimal safare = 0;
            decimal dfare = 0;
            decimal AdWFoodfare = 0;
            decimal CWFoodfare = 0;
            System.DateTime doj;
            doj = jdate;
            /* Previuos Code
           System.DateTime dob = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            */

            /* By Kunal */
            System.DateTime dob = System.DateTime.Now;
            /* By Kunal */

            string env = "";

            if (RadAC.Checked == true)
            {
                env = "Y";
            }
            if (RadNAC.Checked == true)
            {
                env = "N";
            }

            int adults;
            int child;
            int adultstwin;
            int adultstriple;
            int childbed;
            int singleadults;
            int dormitory, NoAWFood, NoCWFood;
            adults = Convert.ToInt32(txtNoOfAdults.Text.ToString());
            child = Convert.ToInt32(txtNoOfChilds.Text.ToString());
            adultstwin = Convert.ToInt32(txtNoOfAdultsTwin.Text.ToString());
            adultstriple = Convert.ToInt32(txtNoOfAdultsTriple.Text.ToString());
            childbed = Convert.ToInt32(txtNoOfChildBed.Text.ToString());
            singleadults = Convert.ToInt32(txtNoOfSingles.Text.ToString());
            dormitory = Convert.ToInt32(txtNoofdormitory.Text.ToString());
            NoAWFood = Convert.ToInt32(txtNoAWFNoOfAdults.Text.ToString());
            NoCWFood = Convert.ToInt32(txtNoCWFNoOfChilds.Text.ToString());

            decimal amt, ltCharges;
            amt = CalCulateAmount(adults, child, adultstwin, adultstriple, childbed, singleadults, dormitory, NoAWFood, NoCWFood);
            //Adding LTC extra in total Amout for   
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                if (Request.QueryString["ltc"] != null)
                {
                    #region Optimize Code
                    /*ltCharges = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select isnull(LTCCharges,0) from Tbl_LTCCharges where rowid=1"));*/
                    #endregion

                    ltCharges = Convert.ToDecimal(pClsLinq.fnGetLTCCharges());
                    lblLTC.Text = "Rs. " + (amt * ltCharges / 100) + " extra @" + ltCharges.ToString() + "%";
                    amt = amt + (amt * ltCharges / 100);
                }
                //End Adding LTC extra in total Amout for 


                decimal tax, ccfee, taxamt, tot, ccamt, lAdvance;
                #region Optimize Code
                /*string chktour = "select isaccomodation from tourmaster where tourno=" + tourno + "";
            string tourchk = DataLib.GetStringData(DataLib.Connection.ConnectionString, chktour);
            string tt = "";
            if (tourchk.ToUpper() == "Y")
            {
                tt = "select TaxValue from ServiceTaxMaster where TaxType='ATP'";
            }
            else
            {
                tt = "select TaxValue from ServiceTaxMaster where TaxType='NATP'";
            } 


            //string tt = "select TaxValue from ServiceTaxMaster where TaxType='TP'";
            tax = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, tt));
            tt = "select TaxValue from ServiceTaxMaster where TaxType='CC'";
            ccfee = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, tt));*/
                #endregion
                tax = Convert.ToDecimal(pClsLinq.fnGetServiceTaxIsAcc(Convert.ToInt32(Request.QueryString["Tourid"])));
                ccfee = Convert.ToDecimal(DataLib.GetserviceTax("CC"));


                taxamt = ((amt) * (tax / 100));
                tot = amt + taxamt;
                ccamt = 0;
                tot = tot + (ccamt);
                decimal pServiceChargesTotal = 0;

                int pATotPax = adults + adultstwin + adultstriple + singleadults + dormitory + NoAWFood;
                int pCTotPax = child + childbed + NoCWFood;
                decimal pServiceChargesTax = Convert.ToDecimal(DataLib.GetserviceTax("SCTax"));
                decimal pServiceChargesTaxValue = 0;
                if (hdAServiceChargeFare.Value != "" || hdAServiceChargeFare.Value != "0.00" || hdAServiceChargeFare.Value != "0")
                {
                    pServiceChargesTaxValue = (((Convert.ToDecimal(hdAServiceChargeFare.Value) * pATotPax) + (Convert.ToDecimal(hdCServiceChargeFare.Value) * pCTotPax)) * pServiceChargesTax) / 100;
                    pServiceChargesTotal = (Convert.ToDecimal(hdAServiceChargeFare.Value) * pATotPax) + (Convert.ToDecimal(hdCServiceChargeFare.Value) * pCTotPax) + pServiceChargesTaxValue;
                }
                tot = Math.Round(tot, 0, MidpointRounding.AwayFromZero) + Math.Round(pServiceChargesTotal, 0, MidpointRounding.AwayFromZero);

                lAdvance = tot;
                bool lIsPartialPaymentByAgent = false;
                if (Convert.ToInt32(hdNoofDays.Value) > 14)
                {
                    for (int lCtr = 0; lCtr < rbtnPaymentOption.Items.Count; lCtr++)
                    {
                        if (rbtnPaymentOption.Items[lCtr].Selected)
                            if (rbtnPaymentOption.Items[lCtr].Value == "HALF")
                            {
                                lAdvance = Math.Round((tot * Convert.ToDecimal(.5)), 0, MidpointRounding.AwayFromZero);
                                lIsPartialPaymentByAgent = true;
                            }
                    }
                }

                #region Optimize Code
                /*string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
            //hr = "-" + hr;

            string tourse = "Select RowId from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and dateadd(hour," + hr + ",journeydate)>=getdate() and  TourNo=" + tourno;
            TourSerial = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, tourse));


            int pkpID;
            pkpID =  Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString,"select pickupmasterrowid from pickupaddress where tourno='" + tourno + "'"));
          */
                #endregion
                TourSerial = TSerial(jdate, tourno);
                int pkpID = Convert.ToInt32(pClsLinq.fnGetPickUpMAsterRowID(tourno, "Branch", Convert.ToInt32(ddlPickUp.SelectedValue)));

                if (RadAC.Checked == true)
                {
                    afare = decimal.Round(Convert.ToDecimal(tAACFAre));
                    cfare = decimal.Round(Convert.ToDecimal(tCACFAre));
                    a2fare = decimal.Round(Convert.ToDecimal(tA2ACFare));
                    a3fare = decimal.Round(Convert.ToDecimal(tA3ACFare));
                    cbfare = decimal.Round(Convert.ToDecimal(tCBACFare));
                    safare = decimal.Round(Convert.ToDecimal(tSACFare));
                    dfare = decimal.Round(Convert.ToDecimal(tDACFare));
                    AdWFoodfare = decimal.Round(Convert.ToDecimal(tAWFACFAre));
                    CWFoodfare = decimal.Round(Convert.ToDecimal(tCWFACFAre));
                }
                if (RadNAC.Checked == true)
                {
                    afare = decimal.Round(Convert.ToDecimal(tAnonacfare));
                    cfare = decimal.Round(Convert.ToDecimal(tCnonacfare));
                    a2fare = decimal.Round(Convert.ToDecimal(tA2NACFare));
                    a3fare = decimal.Round(Convert.ToDecimal(tA3NACFare));
                    cbfare = decimal.Round(Convert.ToDecimal(tCBNACFare));
                    safare = decimal.Round(Convert.ToDecimal(tSNACFare));
                    dfare = decimal.Round(Convert.ToDecimal(tDNACFare));
                    AdWFoodfare = decimal.Round(Convert.ToDecimal(tAWFnonacfare));
                    CWFoodfare = decimal.Round(Convert.ToDecimal(tCWFnonacfare));
                }

                string str1;
                tempstr = tempstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                string rmarks = "";
                if (txtremarks.Text.Trim() != "")
                {
                    rmarks = DataLib.funClear(txtremarks.Text);
                }
                #region Optimize Code
                /*str1 = "insert into onlinetoursbooking (orderid,tourid,doj,dob,BusEnvType,NoOfAdults,Noofchild,NoofAdultsTwin,NoofAdultsTriple,ChildWithoutbed,
             * SingleAdult,TourName,Amount,Tax,CalcTaxValue,TotalAmount,seatno,BusSerialNo,TourSerial,PickupPointID,Adultfare,Childfare,adultstwinfare,
             * adultstriplefare,childwithoutbedfare,singleadultfare,CreditCardFee,CalcCreditCardFee,Remarks,dormitory,dormitoryfare,onlineDis) 
             * values('" + orderid + "','" + tourno + "','" + doj + "','" + dob + "','" + env + "'," + adults + "," + child + "," + adultstwin + ",
             * " + adultstriple + "," + childbed + "," + singleadults + ",'" + lblTourName.Text + "'," + decimal.Round(amt) + "," + tax + ",
             * " + decimal.Round(taxamt) + "," + decimal.Round(tot) + ",'" + tempstr + "','" + BusserialNo + "'," + TourSerial + ",
             * " + pkpID + "," + afare + "," + cfare + "," + a2fare + "," + a3fare + "," + cbfare + "," + safare + "," + ccfee + ",
             * " + decimal.Round(ccamt) + ",'" + rmarks + "'," + dormitory + "," + dfare + ",'"+strValue+"')";
            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);


            str1 = "insert into blockstatus values('" + orderid + "','Blocked')";
            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);*/
                #endregion


                int val = ClsCommon.Insert_OnlineToursBookingAgent(orderid, tourno, doj, dob, Convert.ToChar(env),
                  Convert.ToInt32(txtNoOfAdults.Text),
                  Convert.ToInt32(txtNoOfChilds.Text), Convert.ToInt32(txtNoOfAdultsTwin.Text),
                  Convert.ToInt32(txtNoOfAdultsTriple.Text),
                  Convert.ToInt32(txtNoOfChildBed.Text), Convert.ToInt32(txtNoOfSingles.Text),
                  lblTourName.Text, decimal.Round(amt, 0), tax, Math.Round(taxamt, 0, MidpointRounding.AwayFromZero),
                  Math.Round(tot, 0, MidpointRounding.AwayFromZero), tempstr, BusserialNo, Convert.ToString(TourSerial), pkpID,
                  afare, cfare, a2fare, a3fare, cbfare, safare, ccfee, decimal.Round(ccamt, 0),
                  Convert.ToInt32(txtNoofdormitory.Text), dfare, rmarks, strValue, NoAWFood, NoCWFood, AdWFoodfare, CWFoodfare, Math.Round(lAdvance, 0, MidpointRounding.AwayFromZero),
                  lIsPartialPaymentByAgent,
                  pServiceChargesTotal, pServiceChargesTax, pServiceChargesTaxValue, Convert.ToDecimal(hdAServiceChargeFare.Value), Convert.ToDecimal(hdCServiceChargeFare.Value));

                Response.Redirect("AgentbookedTour.aspx?orderid=" + orderid);
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
        }
        private void updatebook(int tourno, int totpax, System.DateTime jdate, string orderid, int Rowid, string BusserialNo)
        {
            decimal afare = 0;
            decimal cfare = 0;
            decimal a2fare = 0;
            decimal a3fare = 0;
            decimal cbfare = 0;
            decimal safare = 0;
            decimal dfare = 0;
            decimal AdWFoodfare = 0;
            decimal CWFoodfare = 0;
            System.DateTime doj;
            doj = jdate;
            System.DateTime dob = Convert.ToDateTime(System.DateTime.Now.ToShortDateString());
            string env = "";

            if (RadAC.Checked == true)
            {
                env = "Y";
            }

            if (RadNAC.Checked == true)
            {
                env = "N";
            }

            int adults;
            int child;
            int adultstwin;
            int adultstriple;
            int childbed;
            int singleadult;
            int dormitory, NoAWFood, NoCWFood;
            adults = Convert.ToInt32(txtNoOfAdults.Text.ToString());
            child = Convert.ToInt32(txtNoOfChilds.Text.ToString());
            adultstwin = Convert.ToInt32(txtNoOfAdultsTwin.Text.ToString());
            adultstriple = Convert.ToInt32(txtNoOfAdultsTriple.Text.ToString());
            childbed = Convert.ToInt32(txtNoOfChildBed.Text.ToString());
            singleadult = Convert.ToInt32(txtNoOfSingles.Text.ToString());
            dormitory = Convert.ToInt32(txtNoofdormitory.Text.ToString());
            NoAWFood = Convert.ToInt32(txtNoAWFNoOfAdults.Text.ToString());
            NoCWFood = Convert.ToInt32(txtNoCWFNoOfChilds.Text.ToString());

            decimal amt;
            amt = CalCulateAmount(adults, child, adultstwin, adultstriple, childbed, singleadult, dormitory, NoAWFood, NoCWFood);
            decimal tax, ccfee;
            #region Optimize Code
            /*string chktour = "select isaccomodation from tourmaster where tourno=" + tourno + "";
            string tourchk = DataLib.GetStringData(DataLib.Connection.ConnectionString, chktour);
            string tt = "";
            if (tourchk.ToUpper() == "Y")
            {
                tt = "select TaxValue from ServiceTaxMaster where TaxType='ATP'";
            }
            else
            {
                tt = "select TaxValue from ServiceTaxMaster where TaxType='NATP'";
            }


            tax = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, tt));


            ccfee =  Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString,"select TaxValue from ServiceTaxMaster where TaxType='CC'"));
            */
            #endregion
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                tax = Convert.ToDecimal(pClsLinq.fnGetServiceTaxIsAcc(Convert.ToInt32(Request.QueryString["Tourid"])));

                ccfee = Convert.ToDecimal(DataLib.GetserviceTax("CC"));
                decimal taxamt, ccamt;
                taxamt = (amt * (tax / 100));

                decimal tot, lAdvance;
                tot = (amt) + (taxamt);

                ccamt = (tot * (ccfee / 100));
                tot = tot + (ccamt);
                decimal pServiceChargesTotal = 0;

                int pATotPax = adults + adultstwin + adultstriple + singleadult + dormitory + NoAWFood;
                int pCTotPax = child + childbed + NoCWFood;
                decimal pServiceChargesTax = Convert.ToDecimal(DataLib.GetserviceTax("SCTax"));
                decimal pServiceChargesTaxValue = 0;
                if (hdAServiceChargeFare.Value != "" || hdAServiceChargeFare.Value != "0.00" || hdAServiceChargeFare.Value != "0")
                {
                    pServiceChargesTaxValue = (((Convert.ToDecimal(hdAServiceChargeFare.Value) * pATotPax) + (Convert.ToDecimal(hdCServiceChargeFare.Value) * pCTotPax)) * pServiceChargesTax) / 100;
                    pServiceChargesTotal = (Convert.ToDecimal(hdAServiceChargeFare.Value) * pATotPax) + (Convert.ToDecimal(hdCServiceChargeFare.Value) * pCTotPax) + pServiceChargesTaxValue;
                }
                tot = Math.Round(tot, 0, MidpointRounding.AwayFromZero) + Math.Round(pServiceChargesTotal, 0, MidpointRounding.AwayFromZero);

                lAdvance = tot;
                bool lIsPartialPaymentByAgent = false;
                if (Convert.ToInt32(hdNoofDays.Value) > 14)
                {
                    for (int lCtr = 0; lCtr < rbtnPaymentOption.Items.Count; lCtr++)
                    {
                        if (rbtnPaymentOption.Items[lCtr].Selected)
                            if (rbtnPaymentOption.Items[lCtr].Value == "HALF")
                            {
                                lAdvance = Math.Round((tot * Convert.ToDecimal(.5)), 0, MidpointRounding.AwayFromZero);
                                lIsPartialPaymentByAgent = true;
                            }
                    }
                }
                #region Optimize Code
                /* string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
            //hr = "-" + hr;

            //string tourse = "Select RowId from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and dateadd(hour," + hr + ",journeydate)>=getdate() and  TourNo=" + tourno;

            TourSerial = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, "Select RowId from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and dateadd(hour," + hr + ",journeydate)>=getdate() and  TourNo=" + tourno));


            int pkpID;
            pkpID =  Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString,"select pickupmasterrowid from pickupaddress where tourno='" + tourno + "'"));
         */
                #endregion
                TourSerial = TSerial(jdate, tourno);
                //int pkpID = Convert.ToInt32(pClsLinq.fnGetPickUpMAsterRowID(tourno, "Agent", 0));
                int pkpID = Convert.ToInt32(pClsLinq.fnGetPickUpMAsterRowID(tourno, "Branch", Convert.ToInt32(ddlPickUp.SelectedValue)));
                if (RadAC.Checked == true)
                {
                    afare = decimal.Round(Convert.ToDecimal(tAACFAre));
                    cfare = decimal.Round(Convert.ToDecimal(tCACFAre));
                    a2fare = decimal.Round(Convert.ToDecimal(tA2ACFare));
                    a3fare = decimal.Round(Convert.ToDecimal(tA3ACFare));
                    cbfare = decimal.Round(Convert.ToDecimal(tCBACFare));
                    safare = decimal.Round(Convert.ToDecimal(tSACFare));
                    dfare = decimal.Round(Convert.ToDecimal(tDACFare));
                    AdWFoodfare = decimal.Round(Convert.ToDecimal(tAWFACFAre));
                    CWFoodfare = decimal.Round(Convert.ToDecimal(tCWFACFAre));
                }

                if (RadNAC.Checked == true)
                {
                    afare = decimal.Round(Convert.ToDecimal(tAnonacfare));
                    cfare = decimal.Round(Convert.ToDecimal(tCnonacfare));
                    a2fare = decimal.Round(Convert.ToDecimal(tA2NACFare));
                    a3fare = decimal.Round(Convert.ToDecimal(tA3NACFare));
                    cbfare = decimal.Round(Convert.ToDecimal(tCBNACFare));
                    safare = decimal.Round(Convert.ToDecimal(tSNACFare));
                    dfare = decimal.Round(Convert.ToDecimal(tDNACFare));
                    AdWFoodfare = decimal.Round(Convert.ToDecimal(tAWFnonacfare));
                    CWFoodfare = decimal.Round(Convert.ToDecimal(tCWFnonacfare));
                }

                string str1;
                tempstr = tempstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                string rmarks = "";
                if (txtremarks.Text.Trim() != "")
                {
                    rmarks = DataLib.funClear(txtremarks.Text);
                }
                #region Optimize Code
                /*str1 = "update onlinetoursbooking set doj='" + jdate + "',BusEnvType='" + env + "',NoOfAdults=" + adults + ",Noofchild=" + child + ",NoofAdultsTwin=" + adultstwin + ",NoofAdultsTriple=" + adultstriple + ",ChildWithoutbed=" + childbed + ",SingleAdult=" + singleadult + ",dormitory=" + dormitory + ",Amount=" + decimal.Round(amt) + ",Tax=" + tax + ",CalcTaxValue=" + decimal.Round(taxamt) + ",TotalAmount=" + decimal.Round(tot) + ",seatno='" + tempstr + "',BusSerialNo='" + BusserialNo + "',TourSerial=" + TourSerial + ",PickupPointID=" + pkpID + ",Adultfare=" + afare + ",Childfare=" + cfare + ",adultstwinfare=" + a2fare + ",adultstriplefare=" + a3fare + ",childwithoutbedfare=" + cbfare + ",singleadultfare=" + safare + ",dormitoryfare=" + dfare + ",CreditCardFee=" + ccfee + ",CalcCreditCardFee=" + decimal.Round(ccamt) + ",Remarks='" + rmarks + "' where rowid='" + Rowid + "'";
            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);*/
                #endregion


                int val = ClsCommon.Update_OnlineToursBookingAgent(jdate, Convert.ToChar(env),
                   Convert.ToInt32(txtNoOfAdults.Text),
                   Convert.ToInt32(txtNoOfChilds.Text), Convert.ToInt32(txtNoOfAdultsTwin.Text),
                   Convert.ToInt32(txtNoOfAdultsTriple.Text), Convert.ToInt32(txtNoOfChildBed.Text),
                   Convert.ToInt32(txtNoOfSingles.Text), decimal.Round(amt), tax, Math.Round(taxamt, 0, MidpointRounding.AwayFromZero),
                   decimal.Round(tot), tempstr, BusserialNo, TourSerial, pkpID, afare, cfare, a2fare,
                   a3fare, cbfare, safare, ccfee, decimal.Round(ccamt),
                   Rowid, Convert.ToInt32(txtNoofdormitory.Text), dfare, rmarks, NoAWFood, NoCWFood, AdWFoodfare, CWFoodfare, Math.Round(lAdvance, 0, MidpointRounding.AwayFromZero),
                   lIsPartialPaymentByAgent, pServiceChargesTotal, pServiceChargesTax, pServiceChargesTaxValue, Convert.ToDecimal(hdAServiceChargeFare.Value), Convert.ToDecimal(hdCServiceChargeFare.Value));

                Server.Transfer("AgentbookedTour.aspx?orderid=" + orderid);
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
        }
        private int TSerial(DateTime jdate, int tourno)
        {
            string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
            ClsAdo clsObj = null;
            DataTable ldtRecSet = null;
            try
            {
                clsObj = new ClsAdo();
                ldtRecSet = clsObj.fnFixed_TourSerial(jdate, hr, tourno);
                if (ldtRecSet != null && ldtRecSet.Rows.Count > 0)
                {
                    return Convert.ToInt32(ldtRecSet.Rows[0][0]);
                }
                else
                {
                    return -1;
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (ldtRecSet != null)
                {
                    ldtRecSet.Dispose();
                    ldtRecSet = null;
                }
            }
        }
        private string ChekAvailability1(int sreq, int tourno, System.DateTime jdate)
        {
            hlmsgerr.Text = "";
            lbMsgErr.Text = "";
            string str1;

            #region Optimize Code
            //string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();

            //hr = "-" + hr;

            /*str1 = "Select * from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and dateadd(hour," + hr + ",journeydate)>=getdate() and TourNo=" + tourno + "";
            TourSerial=Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString,str1));*/
            #endregion
            TourSerial = TSerial(jdate, tourno);


            if (RadAC.Checked == true)
            {
                BusEnvType = "Y";
            }
            if (RadNAC.Checked == true)
            {
                BusEnvType = "N";
            }

            DataTable busallot = null;
            string avail = "";
            int tot = 0;
            #region Optimize Code
            /* if (Convert.ToString(ViewState["chkbusno"]) != "a")
            {
                busallot = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "Select * from busallot where TourSerial=" + TourSerial + " and AC='" + BusEnvType + "' and (active='Y' or active='y') and busno in(" + ViewState["chkbusno"] + ") order by busno");
            }
            else
            {
                busallot = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "Select * from busallot where TourSerial=" + TourSerial + " and AC='" + BusEnvType + "' and (active='Y' or active='y') order by busno");
            }
           // busallot = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "Select * from busallot where TourSerial=" + TourSerial + " and AC='" + BusEnvType + "' and (active='Y' or active='y') order by busno");
            * */
            #endregion
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                if (Convert.ToString(ViewState["chkbusno"]) != "a")
                    busallot = pClsLinq.fnGetBusAllot_Detail(Convert.ToString(TourSerial), BusEnvType, Convert.ToString(ViewState["chkbusno"]));
                else
                    busallot = pClsLinq.fnGetBusAllot_Detail(Convert.ToString(TourSerial), BusEnvType, "a");

                if (busallot.Rows.Count > 1)
                {
                    ViewState["MultiPle"] = "Y";
                }
                else
                {
                    ViewState["MultiPle"] = "N";
                }
                for (int i = 0; i < Convert.ToInt32(busallot.Rows.Count); i++)
                {
                    int seat = 0;
                    busserial[i] = Convert.ToInt32(busallot.Rows[i]["RowId"]);                //totalbuses = totalbuses + 1
                    BusSeaterType = Convert.ToInt32(busallot.Rows[i]["BusType"].ToString().Substring(0, 2));
                    seat = GetAvailBusSeat(busserial[i]);
                    //AvailSeat = AvailSeat + seat;
                    AvailSeat = seat;
                    if (sreq <= AvailSeat)
                    {
                        avail = Convert.ToString(AvailSeat) + "-" + busserial[i] + "-" + Convert.ToString(busallot.Rows[i]["busno"]);
                        break;
                    }
                }
                ViewState["tot"] = Convert.ToString(tot);
                if (avail == "")
                {
                    avail = Convert.ToString(AvailSeat);
                }
                if ((sreq > AvailSeat))
                {
                    if (BusEnvType == "Y")
                    {
                        lbMsgErr.Text = "Currently there are Only " + AvailSeat + " seats available in AC bus on your selected date. <br/> You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\" > Send Request</a> Click here";
                    }
                    else
                    {
                        lbMsgErr.Text = "Currently there are Only " + AvailSeat + " seats available in Non-AC bus on your selected date. <br/> You Want to <a href=\"AgentSeatRequestForm.aspx?TourName=" + lblTourName.Text + "\"> Send Request</a> Click here";
                    }
                    if (!(Request.QueryString["RowId"] == null) | Request.QueryString["OrderId"] == null)
                    {
                        hlmsgerr.Text = "Click here to choose another tour.";
                        hlmsgerr.NavigateUrl = "Agentbookedtour.aspx?orderid=" + orderid;
                    }
                    return avail;
                }
                return avail;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (busallot != null)
                {
                    busallot.Dispose();
                    busallot = null;
                }
            }
        }
        private string ChekAvailabilitymultiple(int sreq, int tourno, System.DateTime jdate)
        {
            hlmsgerr.Text = "";
            lbMsgErr.Text = "";
            #region Optimize Code
            /* string str1;
            string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
           // hr = "-" + hr;
            str1 = "Select * from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and dateadd(hour," + hr + ",journeydate)>=getdate() and TourNo=" + tourno + "";
            TourSerial = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str1));*/
            #endregion
            TourSerial = TSerial(jdate, tourno);
            if (RadAC.Checked == true)
            {
                BusEnvType = "Y";
            }
            if (RadNAC.Checked == true)
            {
                BusEnvType = "N";
            }

            DataTable busallot = null; ;
            string avail = "";
            int tot = 0;
            #region Optimize Code
            /*busallot = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "Select * from busallot where TourSerial=" + TourSerial + " 
             * and AC='" + BusEnvType + "' and (active='Y' or active='y') order by busno");*/
            #endregion
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                busallot = pClsLinq.fnGetBusAllot_Detail(Convert.ToString(TourSerial), BusEnvType, "a");

                if (busallot.Rows.Count > 1)
                {
                    ViewState["MultiPle"] = "Y";
                }
                else
                {
                    ViewState["MultiPle"] = "N";
                }

                string seril = "";
                AvailSeat = 0;
                string seatertype = "";
                string busno = "";
                for (int i = 0; i < Convert.ToInt32(busallot.Rows.Count); i++)
                {
                    int seat = 0;
                    busserial[i] = Convert.ToInt32(busallot.Rows[i]["RowId"]);                //totalbuses = totalbuses + 1
                    BusSeaterType = Convert.ToInt32(busallot.Rows[i]["BusType"].ToString().Substring(0, 2));
                    if (busno == "")
                    {
                        busno = Convert.ToString(busallot.Rows[i]["BusNo"]);
                    }
                    else
                    {
                        busno = busno + "*" + Convert.ToString(busallot.Rows[i]["BusNo"]);
                    }
                    if (seatertype == "")
                    {
                        seatertype = Convert.ToString(BusSeaterType);
                    }
                    else
                    {
                        seatertype = seatertype + "@" + Convert.ToString(BusSeaterType);
                    }
                    if (seril == "")
                    {
                        seril = Convert.ToString(busserial[i]);
                    }
                    else
                    {
                        seril = seril + "$" + Convert.ToString(busserial[i]);
                    }
                    seat = GetAvailBusSeat(busserial[i]);
                    AvailSeat = AvailSeat + seat;
                    tot = tot + seat;
                    if (sreq <= AvailSeat)
                    {
                        avail = Convert.ToString(AvailSeat) + "-" + seril + "-" + seatertype + "-" + busno;
                        break;
                    }
                    else
                    {
                        avail = Convert.ToString(AvailSeat) + "-" + seril + "-" + seatertype + "-" + busno;
                    }
                }
                ViewState["tot"] = Convert.ToString(tot);
                if (avail == "")
                {
                    avail = Convert.ToString(AvailSeat);
                }

                return avail;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (busallot != null)
                {
                    busallot.Dispose();
                    busallot = null;
                }
            }
        }

        private void refresh()
        {
            btncheckavail.Enabled = false;
            tAACFAre = lblAACfare.Text.ToString();
            tAnonacfare = lblANACfare.Text.ToString();
            tCACFAre = lblCACfare.Text.ToString();
            tCnonacfare = lblCNACfare.Text.ToString();
            tA2ACFare = lblA2ACfare.Text.ToString();
            tA2NACFare = lblA2NACfare.Text.ToString();
            tA3ACFare = lblA3ACfare.Text.ToString();
            tA3NACFare = lblA3NACfare.Text.ToString();
            tCBACFare = lblCBACfare.Text.ToString();
            tCBNACFare = lblCBNACfare.Text.ToString();
            tSACFare = lblSACfare.Text.ToString();
            tSNACFare = lblSNACfare.Text.ToString();
            tDACFare = lbldACfare.Text.ToString();
            tDNACFare = lbldNACfare.Text.ToString();
            tAWFACFAre = lblAWFfare.Text.ToString();
            tAWFnonacfare = lblAWFNACfare.Text.ToString();
            tCWFACFAre = lblCWFfare.Text.ToString();
            tCWFnonacfare = lblCWFNACfare.Text.ToString();
            btncheckavail.Enabled = false;
            if ((Request.QueryString["orderid"] != "") && (Request.QueryString["orderid"] != null))
            {
                OrderIDH.Value = Request.QueryString["orderid"];
                PreviousAdults = Convert.ToInt32(Request.QueryString["A"]);
                PreviousChilds = Convert.ToInt32(Request.QueryString["C"]);
                prevadu.Value = Convert.ToString(Request.QueryString["A"]);
                prevchi.Value = Convert.ToString(Request.QueryString["C"]);
                order.Value = Convert.ToString(Request.QueryString["orderid"]);
                hlback.NavigateUrl = "AgentbookedTour.aspx?orderid=" + OrderIDH.Value;
            }
            else
            {
                OrderIDH.Value = DataLib.pnr();
            }
            orderid = OrderIDH.Value;
            Tourno = Convert.ToInt32(Request.QueryString["Tourid"]);
        }
        private string validateDiscount(string jdate)
        {

            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                DateTime? pJDate = Convert.ToDateTime(jdate);
                string validateDiscount = pClsLinq.fnValidateDiscountAgent(pJDate, Convert.ToInt32(Request.QueryString["Tourid"]));
                return validateDiscount;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
            #region Optimize Code

            /*string tID = Request.QueryString["Tourid"].ToString().Replace("'", "''");
             * SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@tourid", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(tID);
            string sql = @"select * from tourmaster(nolock) 
                            where 
                            (
                                '" + jdate + @"' between
                                 journeyfrom  and 
                                 journeyTo
                            )
                            and tourNo=@tourid and IsJourney='Y' and isDiscountActive='Y' and IsAgent='Y'";

            //if (DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, sql, param).Rows.Count > 0)
            //{
            //    return "1";
            //}
            //else
            //{
            //    return "1";
            //}

            SqlParameter[] param2 = new SqlParameter[1];
            param2[0] = new SqlParameter("@tourid", SqlDbType.Int);
            param2[0].Value = Convert.ToInt32(tID);

            string sql2 = @"select * from tourmaster(nolock) 
                            where 
                            (
                                cast(convert(varchar(10),getdate(),101)as smalldatetime) between
                                 bookingfrom  and 
                                 bookingTo
                            )
                              and tourNo=@tourid and IsBooking='Y' and isDiscountActive='Y' and IsAgent='Y'";

            SqlParameter[] param3 = new SqlParameter[1];
            param3[0] = new SqlParameter("@tourid", SqlDbType.Int);

            param3[0].Value = Convert.ToInt32(tID);

            string sql3 = @"select * from tourmaster(nolock) 
                            where  tourNo=@tourid and IsFlat='Y' and isDiscountActive='Y' and IsAgent='Y'";

            if (DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, sql, param).Rows.Count > 0)
            {
                return "1";
            }
            else if (DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, sql2, param2).Rows.Count > 0)
            {
                return "2";
            }
            else if (DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, sql3, param3).Rows.Count > 0)
            {
                return "3";
            }
            else
            {
                return "4";
            }*/
            #endregion
        }
        private string ChkBusType(string jdate)
        {
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                string ChkBusType = "";
                DateTime? pJDate = Convert.ToDateTime(jdate);
                ChkBusType = pClsLinq.fnChkBusTypeAgent(pJDate, Convert.ToInt32(Request.QueryString["Tourid"]));
                return ChkBusType;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
            #region Optimize Code
            /*string tID = Request.QueryString["Tourid"].ToString().Replace("'", "''");
            SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@tourid", SqlDbType.Int);
            param[0].Value = Convert.ToInt32(tID);
            string sql = @"select AC from busallot ba inner join tours t on t.rowid=ba.tourserial 
                         and t.tourno=" + tID + " and convert(varchar(10),t.journeydate,101)='" + jdate + "'";


            DataTable dtBus = DataLib.GetDataTableWithparam(DataLib.Connection.ConnectionString, sql, param);

            if (dtBus.Rows.Count > 1)
            {
                return "0";
            }
            else if (dtBus.Rows.Count == 1)
            {
                string strType = dtBus.Rows[0]["AC"].ToString();
                return strType;
            }
            else
            {
                return "1";
            }*/

            #endregion

        }
        //string pnr()
        //{
        //    string str = "select top 1 orderid from onlinetoursbooking order by rowid desc";
        //    string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, str);
        //    rr = rr.Substring(0, 3);
        //    string ss = "";
        //    int tt=100;
        //    try
        //    {
        //       tt = Convert.ToInt32(rr);
        //    }
        //    catch (Exception ex)
        //    {
        //        ss = "Y";
        //    }
        //    finally
        //    {
        //        if (ss == "Y")
        //            tt = 100;
        //        else if (tt == 999)
        //            tt = 100;
        //        else
        //            tt = tt;

        //        tt = tt + 1;
        //    }
        //    //Random R = new Random();R.Next(100, 999).ToString()
        //    return Convert.ToString(tt) + System.DateTime.Now.Day.ToString("00") + System.DateTime.Now.Month.ToString("00") + System.DateTime.Now.Year + System.DateTime.Now.Minute.ToString("00") + System.DateTime.Now.Second.ToString("00") + System.DateTime.Now.Millisecond.ToString("000");
        //}
        private string getvacantseats(int tourno, string jdate)
        {
            string vacantseats = "";
            int ac = 0, nac = 0;
            string[] jd = jdate.Split('/');
            string jdd = jd[1] + "/" + jd[0] + "/" + jd[2];
            #region Optimize Code
            /*SqlParameter[] param = new SqlParameter[2];
            param[0] = new SqlParameter("@tourNo", tourno);
            param[1] = new SqlParameter("@tDate", jdd);
            string str = "sp_NoOfSeats";
            DataSet dtvacantseats = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, str, param);*/
            #endregion
            ClsAdo pClsLinq = null;
            DataSet dtvacantseats = null;
            try
            {
                pClsLinq = new ClsAdo();
                dtvacantseats = pClsLinq.fnGetNoOfSeats(tourno, jdd);
                if (dtvacantseats != null && dtvacantseats.Tables[0].Rows.Count > 0)
                {

                    for (int vacant = 0; vacant <= dtvacantseats.Tables[0].Rows.Count - 1; vacant++)
                    {
                        if (Convert.ToString(dtvacantseats.Tables[0].Rows[vacant]["AC"]) == "Y")
                        {
                            ac = ac + Convert.ToInt32(dtvacantseats.Tables[0].Rows[vacant]["vacant_seats"]);
                        }
                        else
                        {
                            nac = nac + Convert.ToInt32(dtvacantseats.Tables[0].Rows[vacant]["vacant_seats"]);
                        }
                    }
                }

                return ac + "^" + nac;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (dtvacantseats != null)
                {
                    dtvacantseats.Dispose();
                    dtvacantseats = null;
                }
            }
        }

        private void GetAlertAccordingToBusNo(int BusNo, DateTime Jdate, int Tourno)
        {
            if (BusNo == 2 && Jdate == Convert.ToDateTime("06/26/2017") && Tourno == 123)
            {
                ClsCommon.ShowAlert("The tour will be starting from Night 9PM instead of 3PM from Hotel Southern. For more details please contact 1800110606");
            }
        }

        private bool IsHelicopterAvailable(int TourId, string HelDat)
        {
            bool ret = true;
            if (TourId == 131) // for  checking helicoper available for amar-vh 
            {
                DataSet dsMbk = null;
                DataTable dtMaster = null;

                try
                {
                    string helNotavsp = "HelNotAvail_sp";
                    SqlParameter[] param = new SqlParameter[2];

                    param[0] = new SqlParameter("@TourId", TourId);
                    param[1] = new SqlParameter("@HelDate", HelDat);

                    // if there is any entry of this date in master_HelBlockDate then booking will not allowed
                    dsMbk = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, helNotavsp, param);
                    dtMaster = dsMbk.Tables[0];
                    if (dtMaster.Rows.Count > 0) // if helicopter no available then dtmaster count will be > 0
                    {
                        ret = false;
                    }

                    return ret;
                }
                catch (Exception ex)
                {
                    return false;
                }
                finally
                {
                    if (dsMbk != null)
                    {
                        dsMbk = null;
                    }
                    if (dtMaster != null)
                    {
                        dtMaster = null;
                    }
                }
            }
            return ret;
        }
        #endregion
    }
}