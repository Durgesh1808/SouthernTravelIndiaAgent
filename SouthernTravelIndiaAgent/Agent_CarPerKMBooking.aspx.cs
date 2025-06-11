using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
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
    public partial class Agent_CarPerKMBooking : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        string cityid, cabid;
        int noofpax, fare, nighthalt, driverreward, noofkm, noofdays;
        ClsAdo clsObj = null;
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
                cityid = Request.QueryString["cityid"];
                #region Optimize Code
                /* string getcars = "car_perkm_display";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@cityid", Convert.ToInt32(cityid));

                DataSet dscars = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, getcars, param);*/
                #endregion
                clsObj = null;
                DataTable ldtRecSet = null;
                try
                {
                    clsObj = new ClsAdo();
                    ldtRecSet = clsObj.fncar_perkm_display(Convert.ToInt32(cityid), 0);

                    if (ldtRecSet != null && ldtRecSet.Rows.Count > 0)
                    {
                        ddlvehicle.DataSource = ldtRecSet;
                        ddlvehicle.DataTextField = "carname";
                        ddlvehicle.DataValueField = "carid";
                        ddlvehicle.DataBind();
                        ddlvehicle.Items.Insert(0, new ListItem("Select Car", "0"));
                        ddlnoofpax.Items.Insert(0, new ListItem("Select Pax", "0"));
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

                staxvalue.Value = DataLib.GetserviceTax("CB");
            }
            this.txtplacescovered.Attributes.Add("onkeydown", "return postlimit();");
            Submit.Attributes.Add("OnClick", "return validatesubmit();");

        }

        /// <summary>
        /// // This method is called when the vehicle dropdown is changed. It retrieves the maximum seating capacity of the selected vehicle and updates the number of passengers dropdown accordingly.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ddlvehicle_SelectedIndexChanged(object sender, EventArgs e)
        {



            string maxseat = ddlvehicle.SelectedItem.Text;
            cityid = Request.QueryString["cityid"];
            if (Convert.ToInt32(ddlvehicle.SelectedValue) > 0)
            {
                string[] max = maxseat.Split('(');
                maxseat = max[1];
                maxseat = maxseat.Replace(')', ' ');
                DataTable dtfaredetails = null;
                try
                {
                    dropdownmax(Convert.ToInt32(maxseat));
                    #region Optimize Code
                    /*string str = "select * from tbl_carperkmfaremaster where cityid=" + Convert.ToInt32(cityid) + " and 
                     * carid=" + Convert.ToInt32(ddlvehicle.SelectedValue) + "";
                    DataTable dtfaredetails = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
                    #endregion
                    clsObj = new ClsAdo();
                    dtfaredetails = clsObj.fnGetCarPerKMFareMaster(Convert.ToInt32(cityid), Convert.ToInt32(ddlvehicle.SelectedValue), 0);
                    if (dtfaredetails != null && dtfaredetails.Rows.Count > 0)
                    {
                        txtperkm.Text = Convert.ToString(dtfaredetails.Rows[0]["perkmacfare"]);
                        txtdriver.Text = Convert.ToString(dtfaredetails.Rows[0]["driverreward"]);
                        txtnight.Text = Convert.ToString(dtfaredetails.Rows[0]["nighthalt"]);

                        ackmfare.Value = Convert.ToString(dtfaredetails.Rows[0]["perkmacfare"]);
                        nonackmfare.Value = Convert.ToString(dtfaredetails.Rows[0]["perkmnonacfare"]);
                        minkmallowed.Value = Convert.ToString(dtfaredetails.Rows[0]["minkmallowed"]);

                    }
                    else
                    {
                        txtperkm.Text = "0";
                        txtdriver.Text = "0";
                        txtnight.Text = "0";
                        ackmfare.Value = "0";
                        nonackmfare.Value = "0";
                        minkmallowed.Value = "0";
                        ClientScript.RegisterStartupScript(typeof(string), "sorrynofare", "<script>alert('There is no fare for this Vehicle');</script>");
                    }


                }
                catch (Exception ex)
                {
                    ClientScript.RegisterStartupScript(typeof(string), "sorry", "<script>alert('There is an error in the selection');</script>");
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (dtfaredetails != null)
                    {
                        dtfaredetails.Dispose();
                        dtfaredetails = null;
                    }
                }
            }
            else
            {
                ddlnoofpax.Items.Clear();
                ddlnoofpax.Items.Insert(0, new ListItem("Select Pax", "0"));
            }
        }


        /// <summary>
        /// // This method is called when the reset button is clicked. It clears all the input fields in the form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void reset_Click(object sender, EventArgs e)
        {
            txtTourName.Value = "";
            ddlvehicle.SelectedIndex = -1;
            ddlnoofpax.Items.Clear();
            ddlnoofpax.Items.Insert(0, new ListItem("Select Pax", "0"));
            rdoac.Checked = false;
            rdononac.Checked = false;
            txtstartdate.Value = "";
            ddlstarthr.SelectedIndex = 7;
            ddlstartmin.SelectedIndex = 0;
            ddlstartPM.SelectedIndex = 0;
            ddlendhr.SelectedIndex = 7;
            ddlendmin.SelectedIndex = 0;
            ddlendPM.SelectedIndex = 0;
            txtenddate.Value = "";
            txtreportingadd.Value = "";
            txtdropadd.Value = "";
            txtplacescovered.Value = "";
            txtperkm.Text = "";
            txtnight.Text = "";
            txtdriver.Text = "";
            txtadvance.Text = "";
        }

        /// <summary>
        /// // This method is called when the submit button is clicked. It collects the data from the form, validates it, and submits it to the database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Submit_Click(object sender, EventArgs e)
        {
            cabid = DataLib.CABpnr();
            cityid = Request.QueryString["cityid"];
            noofpax = Convert.ToInt32(ddlnoofpax.SelectedValue);
            fare = Convert.ToInt32(txtperkm.Text);
            noofkm = Convert.ToInt32(txtapproxkm.Text);
            noofdays = Convert.ToInt32(txtapproxnoofdays.Text);

            string startdate, enddate, starttime, endtime;
            string stax = Convert.ToString(staxvalue.Value);
            string[] sd = txtstartdate.Value.Split('/');

            startdate = sd[1] + "/" + sd[0] + "/" + sd[2];
            starttime = ddlstarthr.SelectedValue + ":" + ddlstartmin.SelectedValue + ":00 " + ddlstartPM.SelectedValue;

            startdate = startdate + " " + starttime;
            if ((txtenddate.Disabled == true) || (txtenddate.Value == ""))
            {
                enddate = DateTime.Today.ToShortDateString();
                endtime = "00:00:00 AM";
            }
            else
            {
                string[] ed = txtenddate.Value.Split('/');
                enddate = ed[1] + "/" + ed[0] + "/" + ed[2];
                endtime = ddlendhr.SelectedValue + ":" + ddlendmin.SelectedValue + ":00 " + ddlendPM.SelectedValue;
            }
            enddate = enddate + " " + endtime;
            char ac = 'N';
            if (rdoac.Checked == true)
                ac = 'Y';
            // else
            //  ac = "N";
            string pickup, drop, tourname, user, branch, places;
            int advance;

            pickup = DataLib.funClear(txtreportingadd.Value);
            drop = DataLib.funClear(txtdropadd.Value);
            tourname = DataLib.funClear(txtTourName.Value);
            places = DataLib.funClear(txtplacescovered.Value);
            nighthalt = Convert.ToInt32(txtnight.Text);
            driverreward = Convert.ToInt32(txtdriver.Text);
            advance = Convert.ToInt32(txtadvance.Text);
            user = Convert.ToString(Session["BranchUserId"]);
            branch = Convert.ToString(Session["BranchCode"]);
            string email = txtEmail.Value;

            //for calculation same as javascript

            int minkms, night, driver, ndays, kmfromdb;
            if (Convert.ToInt32(txtapproxkm.Text) < Convert.ToInt32(minkmallowed.Value))
            {
                minkms = Convert.ToInt32(minkmallowed.Value);
                night = Convert.ToInt32(txtnight.Text);
                driver = Convert.ToInt32(txtdriver.Text);
                ndays = 1;
                if (Convert.ToInt32(txtapproxnoofdays.Text) > 1)
                {
                    ndays = Convert.ToInt32(txtapproxnoofdays.Text);
                    minkms = minkms * ndays;
                    night = night * ndays;
                    driver = driver * ndays;
                }
            }
            else
            {
                minkms = Convert.ToInt32(txtapproxkm.Text);
                night = Convert.ToInt32(txtnight.Text);
                driver = Convert.ToInt32(txtdriver.Text);
                kmfromdb = Convert.ToInt32(minkmallowed.Value);
                ndays = 1;
                if (Convert.ToInt32(txtapproxnoofdays.Text) > 1)
                {
                    ndays = Convert.ToInt32(txtapproxnoofdays.Text);
                    if (Convert.ToInt32(ndays) * Convert.ToInt32(minkmallowed.Value) >= Convert.ToInt32(minkms))
                    {
                        minkms = Convert.ToInt32(minkmallowed.Value);
                        minkms = minkms * ndays;
                        night = night * ndays;
                        driver = driver * ndays;
                    }
                    else
                    {
                        minkms = minkms;
                        night = night * ndays;
                        driver = driver * ndays;
                    }
                }

            }

            decimal total = Convert.ToDecimal(minkms) * Convert.ToDecimal(Request[txtperkm.UniqueID]);
            total = total + Convert.ToDecimal(night) + Convert.ToDecimal(driver);
            decimal st = Convert.ToDecimal(staxvalue.Value);

            decimal sctax = (total * st) / 100;
            //total = total + sctax;
            total = Math.Round(total, 0);

            if (Convert.ToInt32(txtadvance.Text) >= (Convert.ToInt32(total / 4)))
            {
                //upto Here


                #region Optimize Code
                /*string cmd = "insert_tbl_CarTailermade_perkm_log";
                SqlParameter[] param = new SqlParameter[22];
                param[0] = new SqlParameter("@CabId", cabid);
                param[1] = new SqlParameter("@CityId", cityid);
                param[2] = new SqlParameter("@Tourname", tourname);
                param[3] = new SqlParameter("@VehicleId", ddlvehicle.SelectedValue);
                param[4] = new SqlParameter("@Noofpax", noofpax);
                param[5] = new SqlParameter("@Ac", ac);
                param[6] = new SqlParameter("@StartDate", startdate);
                param[7] = new SqlParameter("@EndDate", enddate);
                param[8] = new SqlParameter("@PickupAddress", pickup);
                param[9] = new SqlParameter("@DropAddress", drop);
                param[10] = new SqlParameter("@PlacesCovered", places);
                param[11] = new SqlParameter("@Islumpsum", "N");

                param[12] = new SqlParameter("@Amount", total);
                param[13] = new SqlParameter("@STax", sctax);

                param[14] = new SqlParameter("@PerKmFare", fare);
                param[15] = new SqlParameter("@ApproxKm", noofkm);
                param[16] = new SqlParameter("@ApproxDays", noofdays);
                param[17] = new SqlParameter("@NightHalt", nighthalt);
                param[18] = new SqlParameter("@DriverReward", driverreward);
                param[19] = new SqlParameter("@Advance", advance);
                param[20] = new SqlParameter("@UserName", user);
                param[21] = new SqlParameter("@BranchCode", branch);

                int val = DataLib.InsStoredProcData(cmd, param);*/
                #endregion
                tbl_CarTailermade_log TblObj = new tbl_CarTailermade_log();
                TblObj.CabId = cabid;
                TblObj.CityId = Convert.ToInt32(cityid);
                TblObj.Tourname = tourname;
                TblObj.VehicleId = Convert.ToInt32(ddlvehicle.SelectedValue);
                TblObj.Noofpax = noofpax;
                TblObj.Ac = ac;
                TblObj.StartDate = Convert.ToDateTime(startdate);
                TblObj.EndDate = Convert.ToDateTime(enddate);
                TblObj.PickupAddress = pickup;
                TblObj.DropAddress = drop;
                TblObj.PlacesCovered = places;
                TblObj.Islumpsum = 'N';
                TblObj.Amount = Convert.ToInt32(total);
                TblObj.STax = Convert.ToInt32(sctax);
                TblObj.PerKmFare = fare;
                TblObj.ApproxKm = noofkm;
                TblObj.ApproxDays = noofdays;
                TblObj.NightHalt = nighthalt;
                TblObj.DriverReward = driverreward;
                TblObj.Advance = advance;
                TblObj.UserName = user;
                TblObj.BranchCode = branch;
                int val = -1;
                try
                {
                    clsObj = new ClsAdo();
                    val = clsObj.fnInsertCarTailermade_perkm_log(TblObj);
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (TblObj != null)
                    {
                        TblObj = null;
                    }
                }
                if (val == 0)
                {
                    Response.Redirect("Agentcustomerdetails.aspx?orderid=" + Convert.ToString(cabid) + "&email=" + email + "&pax=" + noofpax + "&strans=perkm");
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Alert", "<script>alert('There is an error in the Input fields, Please check');</script>");
                    return;
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "lessadvance", "<script>alert(Please Enter Minimum Advance amount of " + Convert.ToInt32(total / 4) + ");</script>");
                return;
            }
        }
        #endregion
        #region "Method(s)"
        void dropdownmax(int x)
        {
            ArrayList arr = new ArrayList();
            for (int i = 1; i <= x; i++)
            {
                arr.Add(i);
            }
            ddlnoofpax.DataSource = arr;
            ddlnoofpax.DataBind();
            ddlnoofpax.Items.Insert(0, new ListItem("Select Pax", "0"));
        }
        #endregion
    }
}