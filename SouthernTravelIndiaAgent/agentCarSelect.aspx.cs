using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class agentCarSelect : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        protected string dtPickUp, scityid, sscityname1;
        protected string dtPickUp1, strSql = "", strsubtrans = "", sqlQuery;
        protected int strValues1;
        protected decimal totalAmt;
        string transferId = "", fareId = "";
        float duration = 0.0f;
        ClsAdo clsObj = null;
        #endregion

        #region "Event(s)"

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }

            if (!IsPostBack)
            {
                Button1.Attributes.Add("onclick", "return checkonsubmit();");
                Session["mode"] = "car";

                dtPickUp = ddate.Value.ToString();
                if (dtPickUp == "")
                {
                    dtPickUp1 = Convert.ToDateTime(System.DateTime.Now.Date).ToShortDateString();
                    ddate.Value = mmddyy2ddmmyy(dtPickUp1);
                }
                BindPage();
                string scityid = Convert.ToString(Session["CityId"]);
                #region Optimize Code
                /*string strpickup = "SELECT   Pickupcityadd, Dropcityadd,CityName FROM City_tbl WHERE (CityId = '" + scityid + "')";
                DataTable dtpickup = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strpickup);*/
                #endregion
                clsObj = new ClsAdo();
                DataTable dtpickup = clsObj.fnGetFind_City(Convert.ToInt32(scityid));
                try
                {
                    if (dtpickup != null && dtpickup.Rows.Count > 0)
                    {
                        txtPickAddress.Value = dtpickup.Rows[0]["Pickupcityadd"].ToString();
                        txtDropAddress1.Value = dtpickup.Rows[0]["Dropcityadd"].ToString();
                        Session["sscityname1"] = dtpickup.Rows[0]["CityName"].ToString();
                    }
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (dtpickup != null)
                    {
                        dtpickup.Dispose();
                        dtpickup = null;
                    }
                }
            }
        }


        /// <summary>
        /// /// This method binds the page with necessary data such as car details, fare, etc.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Button1_Click(object sender, EventArgs e)
        {
            string[] DateArr = new string[3];
            char[] splitter = { '/' };
            dtPickUp = ddate.Value.ToString();

            DateArr = dtPickUp.Split(splitter);
            dtPickUp1 = DateArr[1] + "/" + DateArr[0] + "/" + DateArr[2];
            dtPickUp1 = Convert.ToDateTime(dtPickUp1).ToShortDateString();
            dtPickUp1 = dtPickUp1 + " " + pick_hrs.Value + ":" + pick_min.Value;
            #region Commented
            /* if (Convert.ToDateTime(dtPickUp1) < DateTime.Now.AddHours(Convert.ToDouble(ConfigurationManager.AppSettings["TimeGap"])))
            {
                if (Convert.ToDouble(ConfigurationManager.AppSettings["TimeGap"]) > 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Lessdate", "<script>alert('Booking can not be made before " + ConfigurationManager.AppSettings["TimeGap"].ToString() + " hours');</script>");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Lessdate", "<script>alert('Pickup Date & Time Should be greaterthan Todays Date & Time');</script>");
                }
                return;
            }*/
            #endregion
            if (DateTime.Now >= Convert.ToDateTime(dtPickUp1))
            {
                ClsCommon.ShowAlert("Pickup Date & Time Should be greater than Todays Date & Time");
                //ClientScript.RegisterStartupScript(GetType(), "Lessdate", "<script>alert('Pickup Date & Time Should be greater than Todays Date & Time');</script>");

            }
            else
            {
                string scityname = Convert.ToString(Session["sscityname1"]);
                decimal scarfare = Convert.ToDecimal(fare.Value);
                decimal scartax = Convert.ToDecimal(txtStaxAmount.Value);
                totalAmt = Convert.ToDecimal(scarfare + scartax);

                if (totalAmt <= getAgentBalance())
                {
                    string stourname = lblPurpose.Text;
                    string sCarname = lblCarModel.Text;
                    string smaxseat = lblCarCapacity.Text;
                    string scarr = sCarname + '(' + smaxseat + ')';
                    string sperkm = lblExtraKms.Text;
                    string shrfare = lblExtraHrs.Text;
                    scityid = txtcityid.Value;
                    string sagentid = Session["AgentId"].ToString();


                    #region Commented
                    //List<getBranchDetailsByCityIdResult> objBranchDetailsByCityId = clsObj.getBranchDetailsByCityId(Convert.ToInt32(scityid));

                    //string sbranchid1 = string.Empty;
                    //string soperatedbranchcodefinal = string.Empty;
                    //string soperatedbranchbarnchfinal = string.Empty;

                    //if (objBranchDetailsByCityId != null && objBranchDetailsByCityId.Count > 0)
                    //{
                    //    sbranchid1 = Convert.ToString(objBranchDetailsByCityId[0].branchId);
                    //    soperatedbranchcodefinal = Convert.ToString(objBranchDetailsByCityId[0].branchcode);
                    //    soperatedbranchbarnchfinal = Convert.ToString(objBranchDetailsByCityId[0].branchname);
                    //}
                    #endregion
                    //************************
                    ViewState["totalAmt"] = Convert.ToString(lblTotalAmount.Text);

                    string strafterfate = txtafterfare.Value;
                    //string orderId = "";

                    //orderId = DataLib.CABpnr();

                    float noOfCars = 1;
                    noOfCars = float.Parse(noOfCarsHidden.Value);

                    float dis = 0.0f, adv = 0.0f, stax = 0.0f;
                    if (txtDiscount.Value != "")
                        dis = float.Parse(txtDiscount.Value);
                    if (txtAdvance.Value != "")
                        adv = float.Parse(txtAdvance.Value);
                    stax = float.Parse(txtStaxAmount.Value);

                    if (Request.QueryString["fid"] != null)
                        fareId = Request.QueryString["fid"].ToString();
                    if (Request.QueryString["subtrans"] != null)
                        transferId = Request.QueryString["subtrans"].ToString();

                    string pickadd = txtPickAddress.Value;
                    string sdrop = txtDropAddress1.Value;

                    int? returnStatus = 0;
                    string CabId = string.Empty;


                    ClsAdo objLinqToDb = new ClsAdo();

                    try
                    {



                        if (Request.QueryString["sfixed"].ToString() == "FixedTour")
                        {
                            objLinqToDb.CabBookingAgent(Convert.ToInt32(scityid), Convert.ToInt32(fareId), Convert.ToInt32(transferId), Convert.ToInt32(numPax.Text), Math.Round(Convert.ToDecimal(txtafterfare.Value), 2), Convert.ToInt32(noOfCars), Convert.ToDateTime(dtPickUp1), pickadd, sdrop, txtEmail.Value, Math.Round((Convert.ToDecimal(txtafterfare.Value) * Convert.ToDecimal(noOfCars)), 2), Convert.ToDecimal(txtStaxAmount.Value), 0, (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)), "Branch", Convert.ToDecimal(dis), (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)), "3", true, stourname, scarr, sperkm, shrfare, Convert.ToInt32(sagentid), Convert.ToString(ViewState["duration"]), scityname, ref returnStatus, ref CabId);
                        }
                        else
                        {
                            objLinqToDb.CabBookingAgent(Convert.ToInt32(scityid), Convert.ToInt32(fareId), Convert.ToInt32(transferId), Convert.ToInt32(numPax.Text), Math.Round(Convert.ToDecimal(txtafterfare.Value), 2), Convert.ToInt32(noOfCars), Convert.ToDateTime(dtPickUp1), pickadd, sdrop, txtEmail.Value, Math.Round((Convert.ToDecimal(txtafterfare.Value) * Convert.ToDecimal(noOfCars)), 2), Convert.ToDecimal(txtStaxAmount.Value), 0, (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)), "Branch", Convert.ToDecimal(dis), (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)), "1", true, stourname, scarr, sperkm, shrfare, Convert.ToInt32(sagentid), Convert.ToString(ViewState["duration"]), scityname, ref returnStatus, ref CabId);
                        }
                        if (returnStatus > 0)
                        {
                            if (!string.IsNullOrEmpty(CabId))
                            {
                                Session["orderid"] = CabId;
                                strsubtrans = Request.QueryString["subtrans"].ToString();
                                Response.Redirect("Agentcustomerdetails.aspx?pax=" + numPax.Text + "&subtrans=" + strsubtrans + "&strans=" + Request.QueryString["sfixed"].ToString() + "&orderid=" + CabId + "&mode=car&email=" + txtEmail.Value);
                            }
                        }
                        else
                        {
                            ClsCommon.ShowAlert("Cab could not booked, please try again later");

                            //ClientScript.RegisterStartupScript(this.GetType(), Guid.NewGuid().ToString(), "alert('Cab could not booked, please try again later');", true);
                        }
                    }
                    catch { }
                    finally
                    {
                        if (objLinqToDb != null)
                        {
                            objLinqToDb = null;
                        }
                    }
                }
                else
                {
                    ClsCommon.ShowAlert("Insufficient funds");

                    //ClientScript.RegisterStartupScript(typeof(string), "Error", "<Script>alert('Insufficient funds');</Script>");
                    return;
                }
            }
        }

        //protected void Button1_Click(object sender, EventArgs e)
        //{


        //    string[] DateArr = new string[3];
        //    char[] splitter = { '/' };
        //    dtPickUp = ddate.Value.ToString();

        //    DateArr = dtPickUp.Split(splitter);
        //    dtPickUp1 = DateArr[1] + "/" + DateArr[0] + "/" + DateArr[2];
        //    dtPickUp1 = Convert.ToDateTime(dtPickUp1).ToShortDateString();
        //    dtPickUp1 = dtPickUp1 + " " + pick_hrs.Value + ":" + pick_min.Value;

        //    if (Convert.ToDateTime(dtPickUp1) < DateTime.Now.AddHours(Convert.ToDouble(ConfigurationManager.AppSettings["TimeGap"])))
        //    {
        //        if (Convert.ToDouble(ConfigurationManager.AppSettings["TimeGap"]) > 0)
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "Lessdate", "<script>alert('Booking can not be made before " + ConfigurationManager.AppSettings["TimeGap"].ToString() + " hours');</script>");
        //        }
        //        else
        //        {
        //            ClientScript.RegisterStartupScript(GetType(), "Lessdate", "<script>alert('Pickup Date & Time Should be greaterthan Todays Date & Time');</script>");
        //        }
        //        return;
        //    }
        //    string scityname = Convert.ToString(Session["sscityname1"]);
        //    decimal scarfare = Convert.ToDecimal(fare.Value);
        //    decimal scartax = Convert.ToDecimal(txtStaxAmount.Value);
        //    totalAmt = Convert.ToDecimal(scarfare + scartax);

        //    if (totalAmt <= getAgentBalance())
        //    {
        //        string stourname = lblPurpose.Text;
        //        string sCarname = lblCarModel.Text;
        //        string smaxseat = lblCarCapacity.Text;
        //        string scarr = sCarname + '(' + smaxseat + ')';
        //        string sperkm = lblExtraKms.Text;
        //        string shrfare = lblExtraHrs.Text;
        //        scityid = txtcityid.Value;
        //        string sagentid = Session["AgentId"].ToString();

        //        #region OptimizeCode

        //        //string sbranchid = "select branchid from City_tbl where cityid='" + scityid + "'";
        //        //string sbranchid1 = DataLib.GetStringData(DataLib.Connection.ConnectionString, sbranchid);

        //        //string soperatedbranchcode = "select branchcode from BranchMaster  WHERE branchid='" + sbranchid1 + "'";
        //        //string soperatedbranchcodefinal = DataLib.GetStringData(DataLib.Connection.ConnectionString, soperatedbranchcode);

        //        //string soperatedbranchname = "select branchname from BranchMaster  WHERE branchid='" + sbranchid1 + "'";
        //        //string soperatedbranchbarnchfinal = DataLib.GetStringData(DataLib.Connection.ConnectionString, soperatedbranchname);

        //        #endregion OptimizeCode

        //        List<getBranchDetailsByCityIdResult> objBranchDetailsByCityId = clsObj.getBranchDetailsByCityId(Convert.ToInt32(scityid));

        //        string sbranchid1 = string.Empty;
        //        string soperatedbranchcodefinal = string.Empty;
        //        string soperatedbranchbarnchfinal = string.Empty;

        //        if (objBranchDetailsByCityId != null && objBranchDetailsByCityId.Count > 0)
        //        {
        //            sbranchid1 = Convert.ToString(objBranchDetailsByCityId[0].branchId);
        //            soperatedbranchcodefinal = Convert.ToString(objBranchDetailsByCityId[0].branchcode);
        //            soperatedbranchbarnchfinal = Convert.ToString(objBranchDetailsByCityId[0].branchname);
        //        }

        //        //************************
        //        ViewState["totalAmt"] = Convert.ToString(lblTotalAmount.Text);

        //        string strafterfate = txtafterfare.Value;
        //        string orderId = "";

        //        orderId = DataLib.CABpnr();

        //        float noOfCars = 1;
        //        noOfCars = float.Parse(noOfCarsHidden.Value);

        //        float dis = 0.0f, adv = 0.0f, stax = 0.0f;
        //        if (txtDiscount.Value != "")
        //            dis = float.Parse(txtDiscount.Value);
        //        if (txtAdvance.Value != "")
        //            adv = float.Parse(txtAdvance.Value);
        //        stax = float.Parse(txtStaxAmount.Value);

        //        if (Request.QueryString["fid"] != null)
        //            fareId = Request.QueryString["fid"].ToString();
        //        if (Request.QueryString["subtrans"] != null)
        //            transferId = Request.QueryString["subtrans"].ToString();

        //        //string str1 = "select * from onlinecustomer where email ='" + txtEmail.Value + "' or mobile ='" + txtEmail.Value + "'";
        //        // DataTable dtX = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);
        //        // if (dtX.Rows.Count > 0)
        //        // {

        //        // }
        //        // string pickadd = txtPickAddress.Value + ',' + txtPickupAddr2.Value;
        //        // string sdrop = txtDropAddress1.Value + ',' + txtDropAddress2.Value;
        //        string pickadd = txtPickAddress.Value;
        //        string sdrop = txtDropAddress1.Value;
        //        if (Request.QueryString["sfixed"].ToString() == "FixedTour")
        //        {
        //            #region Optimize Code
        //            /*SqlParameter[] paramsms = new SqlParameter[28];
        //            paramsms[0] = new SqlParameter("@CabId", orderId);
        //            paramsms[1] = new SqlParameter("@FareId", fareId);
        //            paramsms[2] = new SqlParameter("@TransferId", transferId);
        //            paramsms[3] = new SqlParameter("@NoOfPax", numPax.Text);
        //            paramsms[4] = new SqlParameter("@FarePerCar", Math.Round(Convert.ToDecimal(txtafterfare.Value), 2));
        //            paramsms[5] = new SqlParameter("@NoOfCars", noOfCars.ToString());
        //            paramsms[6] = new SqlParameter("@PickTupTime", dtPickUp1);
        //            paramsms[7] = new SqlParameter("@PickupAddress", pickadd);
        //            paramsms[8] = new SqlParameter("@DropOffAddress", sdrop);
        //            paramsms[9] = new SqlParameter("@EmailId", txtEmail.Value);
        //            paramsms[10] = new SqlParameter("@Fare", Math.Round((Convert.ToDecimal(txtafterfare.Value) * Convert.ToDecimal(noOfCars)), 2));
        //            paramsms[11] = new SqlParameter("@STax", txtStaxAmount.Value);
        //            paramsms[12] = new SqlParameter("@CC", "0.00");
        //            paramsms[13] = new SqlParameter("@NetTotal", (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)));
        //            paramsms[14] = new SqlParameter("@PayMode", "Branch");
        //            paramsms[15] = new SqlParameter("@Discount", dis);
        //            paramsms[16] = new SqlParameter("@Advance", (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)));
        //            paramsms[17] = new SqlParameter("@TourCategoty", "3");
        //            paramsms[18] = new SqlParameter("@IsExist", "1");
        //            paramsms[19] = new SqlParameter("@TourName", stourname);
        //            paramsms[20] = new SqlParameter("@CarName", scarr);
        //            paramsms[21] = new SqlParameter("@OperatedranchName", soperatedbranchbarnchfinal);
        //            paramsms[22] = new SqlParameter("@OperatedBranchcode", soperatedbranchcodefinal);
        //            paramsms[23] = new SqlParameter("@PerExtraKMFare", sperkm);
        //            paramsms[24] = new SqlParameter("@PerExtraHRFare", shrfare);
        //            paramsms[25] = new SqlParameter("@AgentId", sagentid);
        //            paramsms[26] = new SqlParameter("@duration", Convert.ToString(ViewState["duration"]));
        //            paramsms[27] = new SqlParameter("@OriginCity", scityname);

        //            int Val = DataLib.InsStoredProcData("insert_tbl_CarBookings_Log_Fixed_agent", paramsms);*/
        //            #endregion
        //            tbl_CarBookings_Log TblObj = new tbl_CarBookings_Log();
        //            TblObj.CabId = orderId;
        //            TblObj.FareId = Convert.ToInt32(fareId);
        //            TblObj.TransferId = Convert.ToInt32(transferId);
        //            TblObj.NoOfPax = Convert.ToInt32(numPax.Text);
        //            TblObj.FarePerCar = Math.Round(Convert.ToDecimal(txtafterfare.Value), 2);
        //            TblObj.NoOfCars = Convert.ToInt32(noOfCars);
        //            TblObj.PickTupTime = Convert.ToDateTime(dtPickUp1);
        //            TblObj.PickupAddress = pickadd;
        //            TblObj.DropOffAddress = sdrop;
        //            TblObj.EmailId = txtEmail.Value;
        //            TblObj.Fare = Math.Round((Convert.ToDecimal(txtafterfare.Value) * Convert.ToDecimal(noOfCars)), 2);
        //            TblObj.STax = Convert.ToDecimal(txtStaxAmount.Value);
        //            TblObj.CC = 0;
        //            TblObj.NetTotal = (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value));
        //            TblObj.PayMode = "Branch";
        //            TblObj.Discount = Convert.ToDecimal(dis);
        //            TblObj.Advance = (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value));
        //            TblObj.TourCategoty = "3";
        //            TblObj.IsExist = true;
        //            TblObj.TourName = stourname;
        //            TblObj.CarName = scarr;
        //            TblObj.OperatedranchName = soperatedbranchbarnchfinal;
        //            TblObj.OperatedBranchcode = soperatedbranchcodefinal;
        //            TblObj.PerExtraKMFare = sperkm;
        //            TblObj.PerExtraHRFare = shrfare;
        //            TblObj.AgentId = Convert.ToInt32(sagentid);
        //            TblObj.duration = Convert.ToString(ViewState["duration"]);
        //            TblObj.OriginCity = scityname;
        //            int val = clsObj.fnInserttbl_CarBookings_Log_Fixed_agent(TblObj);

        //        }
        //        else
        //        {
        //            #region Optimize Code
        //            /*SqlParameter[] paramsms = new SqlParameter[28];
        //            paramsms[0] = new SqlParameter("@CabId", orderId);
        //            paramsms[1] = new SqlParameter("@FareId", fareId);
        //            paramsms[2] = new SqlParameter("@TransferId", transferId);
        //            paramsms[3] = new SqlParameter("@NoOfPax", numPax.Text);
        //            paramsms[4] = new SqlParameter("@FarePerCar", Math.Round(Convert.ToDecimal(txtafterfare.Value), 2));
        //            paramsms[5] = new SqlParameter("@NoOfCars", noOfCars.ToString());
        //            paramsms[6] = new SqlParameter("@PickTupTime", dtPickUp1);
        //            paramsms[7] = new SqlParameter("@PickupAddress", pickadd);
        //            paramsms[8] = new SqlParameter("@DropOffAddress", sdrop);
        //            paramsms[9] = new SqlParameter("@EmailId", txtEmail.Value);
        //            paramsms[10] = new SqlParameter("@Fare", Math.Round((Convert.ToDecimal(txtafterfare.Value) * Convert.ToDecimal(noOfCars)), 2));
        //            paramsms[11] = new SqlParameter("@STax", txtStaxAmount.Value);
        //            paramsms[12] = new SqlParameter("@CC", "0.00");
        //            paramsms[13] = new SqlParameter("@NetTotal", (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)));
        //            paramsms[14] = new SqlParameter("@PayMode", "Branch");
        //            paramsms[15] = new SqlParameter("@Discount", dis);
        //            paramsms[16] = new SqlParameter("@Advance", (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value)));
        //            paramsms[17] = new SqlParameter("@TourName", stourname);
        //            paramsms[18] = new SqlParameter("@CarName", scarr);
        //            paramsms[19] = new SqlParameter("@OperatedranchName", soperatedbranchbarnchfinal);
        //            paramsms[20] = new SqlParameter("@OperatedBranchcode", soperatedbranchcodefinal);
        //            paramsms[21] = new SqlParameter("@PerExtraKMFare", sperkm);
        //            paramsms[22] = new SqlParameter("@PerExtraHRFare", shrfare);
        //            paramsms[23] = new SqlParameter("@TourCategoty", "1");
        //            paramsms[24] = new SqlParameter("@IsExist", "1");
        //            paramsms[25] = new SqlParameter("@AgentId", sagentid);
        //            paramsms[26] = new SqlParameter("@duration", Convert.ToString(ViewState["duration"]));
        //            paramsms[27] = new SqlParameter("@OriginCity", scityname);


        //            int Val = DataLib.InsStoredProcData("insert_tbl_CarBookings_Log_Local_agent", paramsms);*/
        //            #endregion
        //            tbl_CarBookings_Log TblObj = new tbl_CarBookings_Log();
        //            TblObj.CabId = orderId;
        //            TblObj.FareId = Convert.ToInt32(fareId);
        //            TblObj.TransferId = Convert.ToInt32(transferId);
        //            TblObj.NoOfPax = Convert.ToInt32(numPax.Text);
        //            TblObj.FarePerCar = Math.Round(Convert.ToDecimal(txtafterfare.Value), 2);
        //            TblObj.NoOfCars = Convert.ToInt32(noOfCars);
        //            TblObj.PickTupTime = Convert.ToDateTime(dtPickUp1);
        //            TblObj.PickupAddress = pickadd;
        //            TblObj.DropOffAddress = sdrop;
        //            TblObj.EmailId = txtEmail.Value;
        //            TblObj.Fare = Math.Round((Convert.ToDecimal(txtafterfare.Value) * Convert.ToDecimal(noOfCars)), 2);
        //            TblObj.STax = Convert.ToDecimal(txtStaxAmount.Value);
        //            TblObj.CC = 0;
        //            TblObj.NetTotal = (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value));
        //            TblObj.PayMode = "Branch";
        //            TblObj.Discount = Convert.ToDecimal(dis);
        //            TblObj.Advance = (decimal)(float.Parse(txtafterfare.Value) + float.Parse(txtStaxAmount.Value));
        //            TblObj.TourName = stourname;
        //            TblObj.CarName = scarr;
        //            TblObj.OperatedranchName = soperatedbranchbarnchfinal;
        //            TblObj.OperatedBranchcode = soperatedbranchcodefinal;
        //            TblObj.PerExtraKMFare = sperkm;
        //            TblObj.PerExtraHRFare = shrfare;
        //            TblObj.TourCategoty = "1";
        //            TblObj.IsExist = true;
        //            TblObj.AgentId = Convert.ToInt32(sagentid);
        //            TblObj.duration = Convert.ToString(ViewState["duration"]);
        //            TblObj.OriginCity = scityname;
        //            int val = clsObj.fnInserttbl_CarBookings_Log_Local_agent(TblObj);

        //        }
        //        Session["orderid"] = orderId;
        //        strsubtrans = Request.QueryString["subtrans"].ToString();
        //        Response.Redirect("Agentcustomerdetails.aspx?pax=" + numPax.Text + "&subtrans=" + strsubtrans + "&strans=" + Request.QueryString["sfixed"].ToString() + "&orderid=" + orderId + "&mode=car&email=" + txtEmail.Value);
        //    }
        //    else
        //    {
        //        ClientScript.RegisterStartupScript(typeof(string), "Error", "<Script>alert('Insufficient funds');</Script>");
        //        return;
        //    }
        //}
        #endregion
        #region "Method(s)"
        private void BindPage()
        {
            DataTable ldtRecSet = null;
            clsObj = new ClsAdo();
            DataSet dtFare = null;
            try
            {
                if (Request.QueryString["fid"] != null)
                {
                    fareId = Request.QueryString["fid"].ToString();
                }
                strsubtrans = Request.QueryString["subtrans"].ToString();

                //SqlParameter[] param = new SqlParameter[1];
                //param[0] = new SqlParameter("@FareId", fareId);

                if (Request.QueryString["sfixed"].ToString() == "FixedTour")
                {
                    //sqlQuery = "sp_Car_GetFixedTour";
                    ldtRecSet = new DataTable();
                    ldtRecSet = clsObj.fnCar_GetFixedTour(Convert.ToInt32(fareId));
                }
                else
                {
                    if (Request.QueryString["subtrans"].ToString() == "0")
                    {

                        //sqlQuery = "sp_Car_LocalTransfer";
                        ldtRecSet = new DataTable();
                        ldtRecSet = clsObj.fnCar_LocalTransfer(Convert.ToInt32(fareId));
                    }
                    else
                    {
                        //sqlQuery = "sp_Car_LocalSubTransfer";
                        ldtRecSet = new DataTable();
                        ldtRecSet = clsObj.fnCar_LocalSubTransfer(Convert.ToInt32(fareId));

                    }
                }

                //DataSet dtFare = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, sqlQuery, param);
                dtFare = new DataSet();
                if (ldtRecSet != null)
                {
                    dtFare.Tables.Add(ldtRecSet);
                }
                txtStax.Value = DataLib.GetserviceTax("CB");



                if (dtFare.Tables[0].Rows.Count > 0)
                {
                    ViewState["duration"] = Convert.ToString(dtFare.Tables[0].Rows[0]["duration"].ToString());

                    lblCarModel.Text = dtFare.Tables[0].Rows[0]["CarName"].ToString();
                    lblCarCapacity.Text = dtFare.Tables[0].Rows[0]["MaxSeat"].ToString();
                    lblTotalAmount.Text = dtFare.Tables[0].Rows[0]["fare"].ToString();
                    ViewState["totalAmt"] = dtFare.Tables[0].Rows[0]["fare"].ToString();
                    fare.Value = dtFare.Tables[0].Rows[0]["fare"].ToString();
                    //Label1.Text = dtFare.Rows[0]["fare"].ToString();
                    maxSeat.Value = dtFare.Tables[0].Rows[0]["MaxSeat"].ToString();
                    lblExtraKms.Text = dtFare.Tables[0].Rows[0]["PerKMAcFare"].ToString();
                    lblExtraHrs.Text = dtFare.Tables[0].Rows[0]["ExtraHrsAC"].ToString();
                    lblPurpose.Text = dtFare.Tables[0].Rows[0]["transfertype"].ToString();
                    strValues1 = Convert.ToInt32(dtFare.Tables[0].Rows[0]["MaxSeat"].ToString());
                    dropdownmax(strValues1);
                    //scityid = dtFare.Tables[0].Rows[0]["CityId"].ToString();
                    txtcityid.Value = dtFare.Tables[0].Rows[0]["CityId"].ToString();


                }
                //decimal stValue = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select TaxValue from ServiceTaxMaster where TaxType='CB'"));
                decimal stValue = Convert.ToDecimal(clsObj.fnGetServiceTaxValue("CB"));
                lblSTax.Text = stValue.ToString() + "%";
                txtStax.Value = stValue.ToString();

                if (Session["BranchId"] != null)
                {
                    tr1.Visible = true;
                    tr2.Visible = true;
                    tr3.Visible = true;
                    tr4.Visible = true;
                    netBalorTotal.Text = "Balance";
                }
                else
                {
                    tr1.Style.Add("display", "none");
                    tr2.Style.Add("display", "none");
                    tr3.Style.Add("display", "none");
                    netBalorTotal.Text = "Total";

                }

            }
            catch (Exception ex)
            {
                Response.Write("<!--" + ex.ToString() + " -->");
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
                if (dtFare != null)
                {
                    dtFare.Dispose();
                    dtFare = null;
                }
            }
        }


        /// <summary>
        /// /// This method converts a date from mm/dd/yyyy format to dd/mm/yyyy format.
        /// </summary>
        /// <param name="date1"></param>
        /// <returns></returns>
        public static string mmddyy2ddmmyy(string date1)
        {
            if (date1 != null || date1 != "")
            {
                string[] DateArr3 = new string[3];
                char[] splitter1 = { '/' };
                DateArr3 = date1.Split(splitter1);
                return DateArr3[1] + "/" + DateArr3[0] + "/" + DateArr3[2];
            }
            else
            {
                return "";
            }
        }
        void dropdownmax(int x)
        {
            ArrayList arr = new ArrayList();
            for (int i = 1; i <= x; i++)
            {
                arr.Add(i);
            }

            numPax.DataSource = arr;
            numPax.DataBind();
            numPax.Items.Insert(0, new ListItem("Select", "0"));
        }


        /// <summary>
        /// /// This method retrieves the agent's balance from the database.
        /// </summary>
        /// <returns></returns>
        decimal getAgentBalance()
        {
            decimal availBal = 0.0m;
            if (Session["AgentId"] != null)
            {
                #region Optimize Code
                /*string qry, qry1;
                qry = "select isnull(max(RowId),0) as RowId from OnlineTransactionTable(nolock) where agentid=" + Session["AgentId"];
                string strRowNo = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
                if (strRowNo == "" || strRowNo == null)
                {
                    // lblBalance.Text = "Wallet Empty";
                }
                else
                {

                    qry1 = "select isnull(availablebalance,0) as availablebalance from OnlineTransactionTable(nolock) where agentid=" + Session["AgentId"] + " and rowid=" + strRowNo;
                    string balance = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry1);
                    if (balance == "")
                        Session["Balance"] = "0";
                    else
                        Session["Balance"] = balance;
                    availBal = Convert.ToDecimal(Session["Balance"]);
                }*/
                #endregion
                decimal? pBalance = 0;
                string lBalance = "0";
                clsObj = new ClsAdo();
                DataTable dt1 = clsObj.fnGetAgentPayDetail("", Convert.ToInt32(Session["AgentId"]), ref lBalance);
                try
                {
                    pBalance = Convert.ToDecimal(lBalance);

                    if (pBalance == 0)
                        Session["Balance"] = "0";
                    else
                        Session["Balance"] = pBalance;
                    availBal = Convert.ToDecimal(Session["Balance"]);
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (dt1 != null)
                    {
                        dt1.Dispose();
                        dt1 = null;

                    }
                }
            }
            return availBal;

        }
        #endregion


    }
}