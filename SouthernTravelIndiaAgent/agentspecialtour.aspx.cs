using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class agentspecialtour : System.Web.UI.Page
    {
        protected decimal totalAmount = 0;
        protected decimal stax1;
        protected string strTourId = "";
        ClsAdo clsObj = new ClsAdo();
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("AgentspecialseasonTour.aspx?tourid=" + Request.QueryString["tourid"].ToString());
            if (Session["AgentId"] == null)
            {
                //Response.Redirect("sessionexpire.aspx");
                Response.Redirect("agentlogin.aspx");
            }
            if (!IsPostBack)
            {
                string st1 = "select TaxValue from ServiceTaxMaster where TaxType='TP'";
                stax1 = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, st1));
                txtServiceTax.Value = Convert.ToString(decimal.Round(stax1, 2));

                this.txtPax.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                this.txtSinglePax.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                this.Submit.Attributes.Add("onclick", "javascript:return Validationcheck();");
                this.txtMobile.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                this.txtphone.Attributes.Add("onkeydown", "javascript:return chkNumeric();");
                this.txtDate.Attributes.Add("onkeypress", "javascript:return keyboardlock();");
                this.txtFName.Attributes.Add("onkeydown", "return chkCharacter();");
                this.txtComment.Attributes.Add("onkeydown", "return postlimit();");

                int tourid = Convert.ToInt32(Request.QueryString["tourid"].ToString());
                strTourId = Convert.ToString(tourid);
                tid.Value = Convert.ToString(tourid);
                string strTour = "select tourname from Spl_TourMaster(nolock) where tourid=" + tourid + " ";
                String strTourName = DataLib.GetStringData(DataLib.Connection.ConnectionString, strTour);
                txttourName.Text = strTourName;
                //txttourName.ReadOnly = true;
                strTour = "select City from Spl_TourMaster(nolock) where tourid=" + tourid + " ";
                String strCity = DataLib.GetStringData(DataLib.Connection.ConnectionString, strTour);
                lblPkPoint.Text = strCity;
                ViewState["tourno"] = tourid;
                string str = "select a.fare,a.paxid,a.categoryid,b.vehicleid,b.paxtype,b.maxallowed,b.minallowed,a.SARfare from spl_faremaster a, spl_vehiclepax b where a.tourid=" + tourid + "  and a.paxid=b.paxid";
                DataTable dtfare = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);
                if (dtfare.Rows.Count > 0)
                {
                    Submit.Enabled = true;
                    for (int i = 0; i < dtfare.Rows.Count; i++)
                    {
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 1)
                        {
                            car2min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            car2max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtCar12pxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtCar12pxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 2)
                        {
                            car34min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            car34max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtCar34pxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtCar34pxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 3)
                        {
                            innova45min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            innova45max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtInn45PxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtInn45PxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 4)
                        {
                            innova67min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            innova67max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtInn67PxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtInn67PxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 5)
                        {
                            qualis46min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            qualis46max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtQua46PxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtQua46PxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 6)
                        {
                            qualis78min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            qualis78max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtQua78PxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtQua78PxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                        if (Convert.ToInt32(dtfare.Rows[i]["paxid"]) == 7)
                        {
                            tempo89min.Value = Convert.ToString(dtfare.Rows[i]["minallowed"]);
                            tempo89max.Value = Convert.ToString(dtfare.Rows[i]["maxallowed"]);
                            if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "1")
                            {
                                txtTem89PxHiddenSt.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARstandard.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                            else if (Convert.ToString(dtfare.Rows[i]["categoryid"]) == "2")
                            {
                                txtTem89PxHiddenDl.Value = Convert.ToString(dtfare.Rows[i]["fare"]);
                                SARdeluxe.Value = Convert.ToString(dtfare.Rows[i]["SARfare"]);
                            }
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Warning", "<script>alert('Sorry!.. Currently there is no fares for this tour');</script>");
                    Submit.Enabled = false;
                }
            }

            //if (Request.QueryString["paxnum"] != null)
            //{
            // string msg=pandit(Request.QueryString["paxnum"].ToString());         
            //}
            string dep = "select DeptTime from spl_TourMaster where tourid=" + Convert.ToInt32(ViewState["tourno"]) + "";
            depttime.Value = DataLib.GetStringData(DataLib.Connection.ConnectionString, dep);

        }
        public void txtPax_TextChanged(object sender, EventArgs e)
        {
        }


        /// <summary>
        /// /// Handles the click event of the Submit button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Submit_Click(object sender, EventArgs e)
        {
            totalAmount = decimal.Round(Convert.ToDecimal(txtTotalFare.Value));
            int totalPax = Convert.ToInt32(txtTotalPax.Value);
            string strMobile = DataLib.funClear(txtMobile.Text);
            string strPhone = DataLib.funClear(txtphone.Text);
            txtTotalExtrafare.Text = (Request[this.txtTotalExtrafare.UniqueID]);
            string carPax = "", carPax1 = "", InnPax = "", InnPax1 = "", QuaPax = "", QuaPax1 = "", TempPax = "";
            string vType = "", vtype1 = "", vType2 = "", vType3 = "";

            if (txtCar12px.Value != "" && txtCar12px.Value != null && Convert.ToInt32(txtCar12px.Value) != 0)
            {
                carPax = "1";
                vType = "1";
            }
            if (txtCar34px.Value != "" && txtCar34px.Value != null && Convert.ToInt32(txtCar34px.Value) != 0)
            {
                carPax1 = "2";
                vType = "1";
            }

            if (txtInn45Px.Value != "" && txtInn45Px.Value != null && Convert.ToInt32(txtInn45Px.Value) != 0)
            {
                InnPax = "3";
                vtype1 = "2";
            }
            if (txtInn67Px.Value != "" && txtInn67Px.Value != null && Convert.ToInt32(txtInn67Px.Value) != 0)
            {
                InnPax1 = "4";
                vtype1 = "2";
            }
            if (txtQua46Px.Value != "" && txtQua46Px.Value != null && Convert.ToInt32(txtQua46Px.Value) != 0)
            {
                QuaPax = "5";
                vType2 = "3";
            }
            if (txtQua78Px.Value != "" && txtQua78Px.Value != null && Convert.ToInt32(txtQua78Px.Value) != 0)
            {
                QuaPax1 = "6";
                vType2 = "3";
            }

            if (txtTem89Px.Value != "" && txtTem89Px.Value != null && Convert.ToInt32(txtTem89Px.Value) != 0)
            {
                TempPax = "7";
                vType3 = "4";
            }
            int category = 0;

            if (rdoDeluxe.Checked == true)
            {
                category = 2;
            }
            else
            {
                category = 1;
            }
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

            string strPaxdetails = carPax + "," + carPax1 + "," + InnPax + "," + InnPax1 + "," + QuaPax + "," + QuaPax1 + "," + TempPax;
            strPaxdetails = strPaxdetails.Replace(",,,,,", ",");
            strPaxdetails = strPaxdetails.Replace(",,,,", ",");
            strPaxdetails = strPaxdetails.Replace(",,,", ",");
            strPaxdetails = strPaxdetails.Replace(",,", ",");
            strPaxdetails = strPaxdetails.TrimStart(',');
            strPaxdetails = strPaxdetails.TrimEnd(',');
            string strVihcle = vType + "," + vtype1 + "," + vType2 + "," + vType3;
            strVihcle = strVihcle.Replace(",,,", ",");
            strVihcle = strVihcle.Replace(",,", ",");
            strVihcle = strVihcle.TrimStart(',');
            strVihcle = strVihcle.TrimEnd(',');

            string strSinglePax = "";
            decimal extraFair = 0;

            if (Convert.ToString(txtSinglePax.Text) != null && Convert.ToString(txtSinglePax.Text).ToString() != "" && Convert.ToString(txtSinglePax.Text).ToString() != "0")
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
            string ins = "Select InstentBooking from spl_tourmaster where tourid=" + tourid + "";
            string instent = DataLib.GetStringData(DataLib.Connection.ConnectionString, ins);
            double noofhours;
            if (instent == "N")
            {
                TimeSpan hr;
                DateTime jdate = Convert.ToDateTime(mmddyy2ddmmyy(txtDate.Value));
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
                string strJourneyDate = txtDate.Value.Split('/').GetValue(1) + "/" + txtDate.Value.Split('/').GetValue(0) + "/" + txtDate.Value.Split('/').GetValue(2);
                string qry, qry1;
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
                }

                string comtds = "select TaxValue from ServiceTaxMaster where TaxType='TDS'";
                decimal tds = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, comtds));
                string st = "select TaxValue from ServiceTaxMaster where TaxType='TP'";
                decimal stax = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, st));


                int agentid = Convert.ToInt32(Session["AgentId"]);
                string agentlevel = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select agentlevel from agent_agentdetails where agentid=" + agentid);
                decimal comission = Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select agentcomission" + agentlevel.Replace("''", "").ToString() + " from Spl_TourMaster where tourid=" + tourid));
                Session["Comission"] = Convert.ToString(comission);

                //decimal servicetax = (totalAmount * ((stax) / 100));
                decimal servicetax = decimal.Round(Convert.ToDecimal(txtTotalServiceTax.Text));
                if (Convert.ToString(Session["Balance"]) != "")
                {
                    if (Convert.ToDecimal(Convert.ToString(Session["Balance"])) >= (totalAmount + servicetax))
                    {
                        decimal AvailableBalance = Convert.ToDecimal(Convert.ToString(Session["Balance"]));
                        //string refn = "SPL" + System.DateTime.Now.Day + System.DateTime.Now.Month + System.DateTime.Now.Year + System.DateTime.Now.Minute + System.DateTime.Now.Second;
                        string refn = DataLib.SPLticketCode();
                        string strPNR = DataLib.SPLpnr();

                        agentid = Convert.ToInt32(Session["AgentId"]);

                        decimal AgentCommission = decimal.Round(((totalAmount + Convert.ToDecimal(extraFair)) * ((Convert.ToDecimal(Session["Comission"])) / 100)));
                        decimal caltds = 0;
                        caltds = decimal.Round(Convert.ToDecimal(AgentCommission * (Convert.ToDecimal(tds) / 100)));
                        decimal AvailableBalance1 = decimal.Round(AvailableBalance + AgentCommission - (totalAmount + Convert.ToDecimal(extraFair) + caltds + decimal.Round(servicetax)));

                        string strBranch = "select localbranch from agent_agentdetails where agentid='" + agentid + "'";
                        string strLocalBranch = DataLib.GetStringData(DataLib.Connection.ConnectionString, strBranch);


                        if (chkDrop.Checked == true)
                        {
                            string strInsert = @"INSERT INTO spl_tourEnquiry (Email, FirstName,Address,JourneyDate, TourName,
                                    TotalPax, CarType, FareCategoryType, PassengerPerVehicle,Status,tourid,fare,Servicetax,ticketno,
                                    SingleSharing,PickUpVeh,PickVehNo,PickTime,DropVeh,DropVehNo,DropTime,Mobile,Phone,Comments,
                                    station,agentid,PkStation,BranchCode,pnrno) VALUES ('" + txtMail.Text.Replace("'", "''") + "','" + strfname + "','" + DataLib.funClear(txtAddress.Text) + "','" + strJourneyDate + "','" + txttourName.Text + "'," + totalPax + ",'" + strVihcle + "','" + category + "','" + strPaxdetails + "','S'," + tourid + ",'" + decimal.Round((totalAmount + extraFair)) + "','" + decimal.Round(servicetax) + "','" + refn + "','" + strSinglePax + "','" + pickupveh + "','" + strFlightNo + "','" + strPktime + "','" + pickupveh + "','" + strFlightNo + "','" + strPktime + "','" + strMobile + "','" + strPhone + "','" + strComments + "','" + strDFlight + "','" + agentid + "','" + strPKstation + "','" + Session["LocalBranch"].ToString() + "','" + strPNR + "');select @@identity;";
                            string strQueryId = DataLib.GetStringData(DataLib.Connection.ConnectionString, strInsert);

                            string sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,Commission,UserName,BranchCode,TransState,Status,PaymentMode,ServiceTax,TDS,debit,credit)values(" + agentid + ",'" + refn + "',11," + decimal.Round((AgentCommission - caltds)) + "," + decimal.Round(AvailableBalance1) + "," + decimal.Round((totalAmount + servicetax + extraFair)) + "," + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + "," + decimal.Round((AgentCommission - caltds)) + ",'" + Session["UserId"].ToString() + "','" + strLocalBranch + "','S','S','AgentCash','" + decimal.Round(servicetax) + "','" + decimal.Round(caltds) + "'," + decimal.Round((AgentCommission - caltds)) + "," + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + ")";
                            DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, sqladd, false);

                            Response.Redirect("AgentSpltourTicket.aspx?Id=" + strQueryId + "");
                        }
                        else
                        {

                            string strInsert = @"INSERT INTO spl_tourEnquiry (Email, FirstName,Address,JourneyDate, TourName,
                                    TotalPax, CarType, FareCategoryType, PassengerPerVehicle,Status,tourid,fare,Servicetax,
                                    ticketno,SingleSharing,PickUpVeh,PickVehNo,PickTime,DropVeh,DropVehNo,DropTime,Mobile,Phone,
                                    Comments,station,AgentId,pkstation,BranchCode,pnrno) VALUES ('" + txtMail.Text.Replace("'", "''") + "','" + strfname + "','" + DataLib.funClear(txtAddress.Text) + "','" + strJourneyDate + "','" + txttourName.Text + "'," + totalPax + ",'" + strVihcle + "','" + category + "','" + strPaxdetails + "','S'," + tourid + ",'" + decimal.Round((totalAmount + extraFair)) + "','" + decimal.Round(servicetax) + "','" + refn + "','" + strSinglePax + "','" + pickupveh + "','" + strFlightNo + "','" + strPktime + "','" + dropveh + "','" + strDFlightNo + "','" + strDPktime + "','" + strMobile + "','" + strPhone + "','" + strComments + "','" + strDFlight + "','" + agentid + "','" + strPKstation + "','" + Session["LocalBranch"].ToString() + "','" + strPNR + "');select @@identity;";
                            string strQueryId = DataLib.GetStringData(DataLib.Connection.ConnectionString, strInsert);

                            string sqladd = "Insert into OnlineTransactionTable(AgentId,RefNo,TransType,AgentCredit,AvailableBalance,AgentDebit,TicketAmount,Commission,UserName,BranchCode,TransState,Status,PaymentMode,ServiceTax,TDS,debit,credit)values(" + agentid + ",'" + refn + "',11," + decimal.Round((AgentCommission - caltds)) + "," + decimal.Round(AvailableBalance1) + "," + decimal.Round((totalAmount + servicetax + extraFair)) + "," + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + "," + decimal.Round((AgentCommission - caltds)) + ",'" + Session["UserId"].ToString() + "','" + strLocalBranch + "','S','S','AgentCash','" + decimal.Round(servicetax) + "','" + decimal.Round(caltds) + "'," + decimal.Round((AgentCommission - caltds)) + "," + decimal.Round(totalAmount + Convert.ToDecimal(extraFair)) + ")";
                            DataLib.ExecuteSQL1(DataLib.Connection.ConnectionString, sqladd, false);

                            Response.Redirect("AgentSpltourTicket.aspx?Id=" + strQueryId + "");
                        }

                    }
                    else
                    {
                        RegisterStartupScript("Error", "<Script>alert('Insuficient funds');</Script>");
                    }

                }
                else
                {
                    RegisterStartupScript("Error1", "<Script>alert('Balance Should be > 0');</Script>");
                }
            }

            else
            {
                RegisterStartupScript("Error2", "<Script>alert('The date should be greater by 2 days/24hrs from the current date');</Script>");
            }

        }


        /// <summary>
        /// // Converts date from mm/dd/yyyy format to dd/mm/yyyy format.
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
    }
}