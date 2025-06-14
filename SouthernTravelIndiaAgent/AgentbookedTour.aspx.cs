using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class AgentbookedTour : System.Web.UI.Page
    {
        #region "Member Variable(s)"
        protected decimal GrandTotal, totalAmt;
        protected int tourid, TotalPaxAdults = 0, TotalPaxChilds = 0, adulttwin = 0, adults = 0, ADWFood = 0, CWFood = 0;
        protected int childs = 0, adulttriple = 0, singleadult = 0, childwithoutbed = 0, dormitory = 0;
        protected ArrayList datearr1 = new ArrayList();
        protected ArrayList datearr = new ArrayList();
        protected ArrayList datearr11 = new ArrayList();
        protected ArrayList datearr12 = new ArrayList();
        public DataView TempDataView;
        protected int AvailSeat, BusSeaterType, sqlreadexist, rowid, i;
        protected int[] busserial = new int[30];
        protected int[] nseats = new int[100];
        protected StringBuilder stbuild = new StringBuilder();
        protected string orderid, paxnum, OName, title, strtourid = "";
        Boolean flag;

        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Expires = 0;
            Session["mode"] = "";
            txtName.Attributes.Add("onkeydown", "javascript:return CheckOnlyCharacter();");
            if (Session["AgentId"] == null)
            {
                Response.Redirect("agentlogin.aspx");
            }
            else
            {
                CheckSubmit.Attributes.Add("onclick", "return chk1()");
            }
            btngo.Attributes.Add("onclick", "return checkonsubmit()");
            if (!Page.IsPostBack)
            {
                chkPromotions.Checked = true;
                ClsAdo pClsLinq = null;
                DataTable dtTour = null;
                try
                {
                    pClsLinq = new ClsAdo();

                    //  BindCountryName();
                    //add on 30 may

                    //ddlState.DataSource = pClsLinq.fnGetState();
                    //ddlState.DataTextField = "State";
                    //ddlState.DataValueField = "State";
                    //ddlState.DataBind();
                    //ddlState.Items.Insert(0, new ListItem("--------Select--------", ""));
                    if (!(Request.QueryString["orderid"] == null))
                    {
                        Response.Write("<input type=hidden id=orderid name=orderid value=" + Request.QueryString["orderid"] + ">");
                        lblbookingdate.Text = System.DateTime.Now.ToLongDateString();
                        string str1;
                        #region Optimize Code
                        /*string hr;
                    hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
                    //hr = "-" + hr;
                    str1 = "Select distinct(TourName) as TourName , TourMaster.TourNo from TourMaster,tours 
                     * where Activated <> 'N' and tourmaster.tourno=tours.tourno 
                     * and dateadd(hour," + hr + ",journeydate)>=getdate() order by TourName";
                    DataTable dtTour = new DataTable();
                    dtTour = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);*/
                        #endregion
                        int? pHours = Convert.ToInt32(ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString());
                        dtTour = pClsLinq.fnGetTour(pHours);
                        if (dtTour != null)
                        {
                            ddlTour.Items.Clear();
                            ddlTour.DataSource = dtTour;
                            ddlTour.DataValueField = "TourNo";
                            ddlTour.DataTextField = "TourName";
                            ddlTour.DataBind();
                            ddlTour.Items.Insert(0, new ListItem("<--Select Tour-->", "0"));
                        }
                        FillDgtour();
                    }
                }
                finally
                {
                    if (pClsLinq != null)
                    {
                        pClsLinq = null;
                    }
                    if (dtTour != null)
                    {
                        dtTour.Dispose();
                        dtTour = null;
                    }
                }
                //BindCountryNameNew(-1);
                BindNationality(-1);
                BindCountry(-1);

                //add on 30 may
                //  BindStateName();
                BindStateNameNew();
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

                TieButton(emailid, CheckSubmit);
            }
            else
            {
                ViewState["strPass"] = tmpEnValue.Value;
            }
            if (Session["Childs"] != null)
            {
                Child1.Value = Session["Childs"].ToString();
            }
            else
            {
                Child1.Value = "0";
            }
            this.Submit1.Attributes.Add("onclick", "return doValidate2();");
            CheckSubmit.Attributes.Add("onclick", "return chk1()");
        }

        protected void dgtourdt_DeleteCommand(object source, DataGridCommandEventArgs e)
        {
            ClsAdo pClsLinq = null;
            try
            {
                #region Optimize Code
                /*string sqlQry;
                sqlQry = "DELETE FROM OnlineToursBooking WHERE rowid = '" + dgtourdt.DataKeys[e.Item.ItemIndex] + "'";
                DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sqlQry, false);*/
                #endregion

                pClsLinq = new ClsAdo();
                string pRowId = dgtourdt.DataKeys[e.Item.ItemIndex].ToString();
                int Val = pClsLinq.fnDeleteOnlineToursBooking(pRowId);
                FillDgtour();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
        }
        protected void dgtourdt_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if (((e.Item.ItemType == ListItemType.AlternatingItem) || (e.Item.ItemType == ListItemType.Item)))
            {
                if ((e.Item.Cells[6].Text.ToString() == "Y") || (e.Item.Cells[6].Text.ToString() == "AC"))
                {
                    e.Item.Cells[6].Text = "AC";
                }
                else
                {
                    e.Item.Cells[6].Text = "Non-AC";
                }
            }
        }
        protected void dgtourdt_ItemCreated(object sender, DataGridItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Item)
            {
                LinkButton lnk;
                lnk = (LinkButton)e.Item.Cells[13].Controls[0];
                lnk.Attributes.Add("OnClick", "return confirm('Are you sure to delete this Tour?');");
            }
        }
        protected void CheckSubmit_Click(object sender, EventArgs e)
        {
            ClsAdo pClsLinq = null;
            DataTable dtOnlineCust = null;
            DataTable dtPaxCount = null;
            try
            {
                pClsLinq = new ClsAdo();

                string str1;
                string emailidt = DataLib.funClear(emailid.Value).Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
                if (emailid.Value.Trim() != "")
                {
                    #region Optimize Code
                    /*str1 = "select * from onlinecustomer where email ='" + emailidt + "' or mobile ='" + emailidt + "' ";
                dtOnlineCust = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);*/
                    #endregion
                    dtOnlineCust = pClsLinq.fnGetCustomerDetail(emailidt);
                    orderid = DataLib.funClear(Request.QueryString["orderid"]).Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
                    if (dtOnlineCust != null && dtOnlineCust.Rows.Count > 0)
                    {
                        ViewState["rowid"] = Convert.ToInt32(dtOnlineCust.Rows[0]["RowId"]);
                        // existingcustomer(Convert.ToInt32(dtOnlineCust.Rows[0]["RowId"]), DataLib.funClear(Request.QueryString["orderid"]));
                        existingcustomernew(Convert.ToInt32(dtOnlineCust.Rows[0]["RowId"]), DataLib.funClear(Request.QueryString["orderid"]));

                        GrandTotal = Convert.ToDecimal(ViewState["GrandTotal"]);
                        //**********************************************************//
                        //---------For Agent Customer Details Purpose Only---------//
                        //**********************************************************//
                        #region Optimize Code
                        /*DataTable dtPaxCount = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select top 1 isnull(NoofAdults,0) as NoofAdults, isnull(NoofChild,0) as NoofChild,isnull(NoofAdultsTwin,0) as NoofAdultsTwin,isnull(NoofAdultsTriple,0) as NoofAdultsTriple,isnull(ChildWithoutbed,0) as ChildWithoutbed,isnull(SingleAdult,0) as SingleAdult,isnull(dormitory,0) as dormitory from OnlineToursBooking where orderid='" + orderid + "'");*/
                        #endregion
                        dtPaxCount = pClsLinq.fnGetPaxCount(orderid);
                        if (dtPaxCount != null && dtPaxCount.Rows.Count > 0)
                        {
                            int ad, ac, a2, a3, cb, sa, add, AWFood, CWFood;
                            ad = Convert.ToInt16(dtPaxCount.Rows[0]["NoofAdults"].ToString());
                            ac = Convert.ToInt16(dtPaxCount.Rows[0]["NoofChild"].ToString());
                            a2 = Convert.ToInt16(dtPaxCount.Rows[0]["NoofAdultsTwin"].ToString());
                            a3 = Convert.ToInt16(dtPaxCount.Rows[0]["NoofAdultsTriple"].ToString());
                            cb = Convert.ToInt16(dtPaxCount.Rows[0]["ChildWithoutbed"].ToString());
                            sa = Convert.ToInt16(dtPaxCount.Rows[0]["SingleAdult"].ToString());
                            add = Convert.ToInt16(dtPaxCount.Rows[0]["dormitory"].ToString());
                            AWFood = Convert.ToInt16(dtPaxCount.Rows[0]["noAdultWithFood"].ToString());
                            CWFood = Convert.ToInt16(dtPaxCount.Rows[0]["noChildWithFood"].ToString());
                            paxnum = Convert.ToString(ad + ac + a2 + a3 + cb + sa + add + AWFood + CWFood);
                            ViewState["paxnum"] = paxnum;
                            //paxnumH.Value = paxnum;
                        }
                        if (Convert.ToInt16(paxnum) > 1)
                        {
                            for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                            {

                                stbuild.Append("<TR ><TD class=style3  valign=middle  height=15>*Title</TD>");
                                stbuild.Append("<td class=style3 align=left  valign=middle  height=15><select type=select onchange=\"changeSex('" + i + "');\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                                stbuild.Append("<TD class=style3 align=right valign=middle  height=15>*Name </TD>");
                                stbuild.Append("<TD valign=middle align=left  class=style3><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px; onkeydown=\"javascript:return CheckOnlyCharacter();\"  maxlength=40 /></TD>");
                                stbuild.Append("<TD class=style3  valign=middle align=center height=15>*Age</TD>");
                                stbuild.Append("<TD valign=middle height=15 class=style3><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2 onkeydown=\"return chkNumeric();\" /></TD>");
                                stbuild.Append("<TD class=style3  valign=middle align=center height=15>Sex</TD>");
                                stbuild.Append("<TD class=style3  valign=middle align=center height=15>");
                                stbuild.Append("<input id=RadM" + i + " type=radio runat=server  Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                                stbuild.Append("<TD class=style3  valign=middle align=center height=15>&nbsp;");
                                stbuild.Append("<input type=radio id=RadF" + i + " runat=server  Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                                stbuild.Append("<tr><td  class=style3 colspan=11 align=left bgcolor=#cccccc></td></tr>");
                            }
                        }
                        else
                        {
                            for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                            {
                                stbuild.Append("<TR ><TD class=style3 style=WIDTH: 150px valign=middle width=150 height=15>*Title</TD>");
                                stbuild.Append("<td class=style3 align=left style=WIDTH: 50px valign=middle width=50 height=15><select type=select onchange=\"changeSex(" + i + ");\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                                stbuild.Append("<TD class=style3 align=right valign=middle width=150 height=15>*Name </TD>");
                                stbuild.Append("<TD valign=middle align=left width=154 class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px;width:158px; size=40 onkeydown=\"javascript:return CheckOnlyCharacter();\" maxlength=40 /></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>*Age</TD>");
                                stbuild.Append("<TD style=WIDTH: 80px valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2 onkeydown=\"return chkNumeric();\" style=width:48px; /></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>Sex</TD>");
                                stbuild.Append("<TD class=hlinks style=WIDTH: 50px valign=middle align=center height=15>");
                                stbuild.Append("<input id=RadM" + i + " type=radio runat=server Width=46px Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 50px valign=middle align=center height=15>&nbsp;");
                                stbuild.Append("<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                            }
                        }
                    }
                    else
                    {
                        chkPromotions.Checked = true;
                        txtName.Attributes.Remove("readonly");
                        txtMobile.Attributes.Remove("readonly");
                        txtMail.Attributes.Remove("readonly");

                        txtName.Value = "";
                        txtAddress.Value = "";
                        ddlState.SelectedValue = "0";
                        TxtForeignState.Text = "";      // add on 30 may
                        if (type.Value == "email")
                        {
                            txtMobile.Value = "";
                            txtMail.Value = emailid.Value;
                        }
                        else if (type.Value == "Mobile")
                        {
                            txtMobile.Value = emailid.Value;
                            txtMail.Value = "";
                        }
                        txtPhone.Value = "";
                        txtPhoneCountryCode.Value = "";
                        orderid = Request.QueryString["orderid"].Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
                        GrandTotal = Convert.ToDecimal(ViewState["GrandTotal"]);
                        //**********************************************************//
                        //---------For Branch Customer Details Purpose Only---------//
                        //**********************************************************//
                        #region Optimize Code
                        /*DataTable dtPaxCount = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select top 1 isnull(NoofAdults,0) as NoofAdults, isnull(NoofChild,0) as NoofChild,isnull(NoofAdultsTwin,0) as NoofAdultsTwin,isnull(NoofAdultsTriple,0) as NoofAdultsTriple,isnull(ChildWithoutbed,0) as ChildWithoutbed,isnull(SingleAdult,0) as SingleAdult,isnull(dormitory,0) as dormitory from OnlineToursBooking where orderid='" + orderid + "'");*/
                        #endregion
                        dtPaxCount = new DataTable();
                        dtPaxCount = pClsLinq.fnGetPaxCount(orderid);
                        if (dtPaxCount != null && dtPaxCount.Rows.Count > 0)
                        {
                            int ad, ac, a2, a3, cb, sa, add, AWFood, CWFood;
                            ad = Convert.ToInt16(dtPaxCount.Rows[0]["NoofAdults"].ToString());
                            ac = Convert.ToInt16(dtPaxCount.Rows[0]["NoofChild"].ToString());
                            a2 = Convert.ToInt16(dtPaxCount.Rows[0]["NoofAdultsTwin"].ToString());
                            a3 = Convert.ToInt16(dtPaxCount.Rows[0]["NoofAdultsTriple"].ToString());
                            cb = Convert.ToInt16(dtPaxCount.Rows[0]["ChildWithoutbed"].ToString());
                            sa = Convert.ToInt16(dtPaxCount.Rows[0]["SingleAdult"].ToString());
                            add = Convert.ToInt16(dtPaxCount.Rows[0]["dormitory"].ToString());
                            AWFood = Convert.ToInt16(dtPaxCount.Rows[0]["noAdultWithFood"].ToString());
                            CWFood = Convert.ToInt16(dtPaxCount.Rows[0]["noChildWithFood"].ToString());
                            paxnum = Convert.ToString(ad + ac + a2 + a3 + cb + sa + add + AWFood + CWFood);
                            ViewState["paxnum"] = paxnum;
                        }
                        if (Convert.ToInt16(paxnum) > 1)
                        {
                            for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                            {
                                stbuild.Append("<TR ><TD class=style3 style=WIDTH: 150px valign=middle width=150 height=15>*Title</TD>");
                                stbuild.Append("<td class=style3 align=left style=WIDTH: 50px valign=middle width=50 height=15><select type=select onchange=\"changeSex('" + i + "');\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                                stbuild.Append("<TD class=style3 align=right valign=middle width=150 height=15>*Name </TD>");
                                stbuild.Append("<TD valign=middle align=left width=154 class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px;width:158px; size=40 onkeydown=\"javascript:return CheckOnlyCharacter();\" maxlength=40 /></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>*Age</TD>");
                                stbuild.Append("<TD style=WIDTH: 80px valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2 onkeydown=\"return chkNumeric();\" style=width:48px; /></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>Sex</TD>");
                                stbuild.Append("<TD class=hlinks style=WIDTH: 50px valign=middle align=center height=15>");
                                stbuild.Append("<input id=RadM" + i + " type=radio runat=server Width=46px Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 50px valign=middle align=center height=15>&nbsp;");
                                stbuild.Append("<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                                stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#FFFFFF ></td></tr>");
                            }
                        }
                        else
                        {
                            for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                            {

                                stbuild.Append("<TR ><TD class=style3 style=WIDTH: 150px valign=middle width=150 height=15>*Title</TD>");
                                stbuild.Append("<td class=style3 align=left style=WIDTH: 50px valign=middle width=50 height=15><select type=select onchange=\"changeSex(" + i + ");\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                                stbuild.Append("<TD class=style3 align=right valign=middle width=150 height=15>*Name </TD>");
                                stbuild.Append("<TD valign=middle align=left width=154 class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px;width:158px; size=40 onkeydown=\"javascript:return CheckOnlyCharacter();\" maxlength=40 /></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>*Age</TD>");
                                stbuild.Append("<TD style=WIDTH: 80px valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2 onkeydown=\"return chkNumeric();\" style=width:48px; /></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>Sex</TD>");
                                stbuild.Append("<TD class=hlinks style=WIDTH: 50px valign=middle align=center height=15>");
                                stbuild.Append("<input id=RadM" + i + " type=radio runat=server Width=46px Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                                stbuild.Append("<TD class=style3 style=WIDTH: 50px valign=middle align=center height=15>&nbsp;");
                                stbuild.Append("<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");

                            }
                        }
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "<script>alert('Please Enter the Email Id / Mobile No');</script>");
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (dtPaxCount != null)
                {
                    dtPaxCount.Dispose();
                    dtPaxCount = null;
                }
                if (dtOnlineCust != null)
                {
                    dtOnlineCust.Dispose();
                    dtOnlineCust = null;
                }
            }
        }
        protected void dgtourdt_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btngo_Click1(object sender, EventArgs e)
        {
            if (!(ddlTour.SelectedIndex == 0))
            {
                Session["blockStatus"] = null;
                Session["blockStr"] = null;
                Server.Transfer("AgentTourBooking.aspx?orderid=" + Request["orderid"] + "&TourId=" + ddlTour.SelectedValue + "&A=" + ViewState["TotalPaxAdults"] + "&C=" + ViewState["TotalPaxChilds"] + "");
            }
            else
            {
                Response.Redirect("AgentbookedTour.aspx?orderid=" + Request["orderid"] + "&A=" + ViewState["TotalPaxAdults"] + "&C=" + ViewState["TotalPaxChilds"] + "");
            }
        }
        protected void ddlTour_SelectedIndexChanged(object sender, EventArgs e)
        {
        }
        protected void btnOK_ServerClick(object sender, EventArgs e)
        {
            //Boolean tt = insertCustomerDetails();
            Boolean tt = insertCustomerDetailsnew();

            ClsAdo pClsLinq = null;
            DataTable dt1 = null;
            try
            {
                pClsLinq = new ClsAdo();
                if (tt == true)
                {
                    Session["blockStatus_Agent"] = null;
                    Session["blockStr_Agent"] = null;
                    if ((Convert.ToString(Session["AgentId"]) != ""))
                    {
                        #region Optimize Code
                        /*DataTable dt1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select Sum(Amount) as Amount,sum(CalcTaxvalue) as CalcTaxvalue, 
                     * sum(TotalAmount) as TotalAmount , sum(CalcCreditCardFee) as CalcCreditCardFee from OnlineToursBooking where 
                     * OnlineToursBooking.orderid='" + Convert.ToString(Request.QueryString["orderid"]) + "'");
                    decimal total = Convert.ToDecimal(dt1.Rows[0]["Amount"].ToString()) + Convert.ToDecimal(dt1.Rows[0]["CalcTaxvalue"].ToString());
                    totalAmt = total;
                    if (Convert.ToString(Session["AgentId"]) != "")
                    {
                        string qry, qry1;
                        qry = "select isnull(max(RowId),0) as RowId from OnlineTransactionTable(nolock) where agentid=" + Session["AgentId"];
                        string strRowNo = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry);
                        if (strRowNo == "" || strRowNo == null)
                        {
                        }
                        else
                        {
                            qry1 = "select isnull(availablebalance,'0') as availablebalance from OnlineTransactionTable(nolock) where agentid=" + Session["AgentId"] + " and rowid=" + strRowNo;
                            string balance = DataLib.GetStringData(DataLib.Connection.ConnectionString, qry1);
                            if (balance == "")
                                Session["Balance"] = "0";
                            else
                                Session["Balance"] = balance;
                        }
                     * 
                    }*/
                        #endregion

                        decimal? pBalance = 0;
                        string lBalance = "0";
                        decimal pAdvance = 0;

                        dt1 = pClsLinq.fnGetAgentPayDetail(Convert.ToString(Request.QueryString["orderid"]),
                            Convert.ToInt32(Session["AgentId"]), ref lBalance);
                        pBalance = Convert.ToDecimal(lBalance);
                        decimal total = Convert.ToDecimal(dt1.Rows[0]["Amount"].ToString()) + Convert.ToDecimal(dt1.Rows[0]["CalcTaxvalue"].ToString());
                        totalAmt = total;
                        pAdvance = Convert.ToDecimal(dt1.Rows[0]["AdvancePaid"].ToString());
                        if (pBalance == 0)
                            Session["Balance"] = "0";
                        else
                            Session["Balance"] = pBalance;

                        //if (total <= Convert.ToDecimal(Convert.ToString(Session["Balance"])))
                        if (pAdvance <= Convert.ToDecimal(Convert.ToString(Session["Balance"])))
                        {
                            SaveAadharPhysicalImg(fupAadhar.FileName);

                            Response.Redirect("AgentProcess.aspx?PaymentBy=AgentCash");
                        }
                        else
                        {
                            RegisterStartupScript("Error", "<Script>alert('Insufficient funds');</Script>");
                        }
                    }
                }
                else
                {
                    bindPax();

                    //if (Convert.ToInt16(ViewState["paxnum"]) > 1)
                    //{
                    //    for (i = 1; i <= Convert.ToInt16(ViewState["paxnum"]); i++)
                    //    {
                    //        stbuild.Append("<TR ><TD class=hlinks  valign=middle  height=15>*Title</TD>");
                    //        stbuild.Append("<td class=hlinks align=left  valign=middle  height=15><select type=select onchange=\"changeSex('" + i + "');\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                    //        stbuild.Append("<TD class=hlinks align=left valign=middle  height=15>*Name</TD>");
                    //        stbuild.Append("<TD valign=middle align=left  class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px; onkeydown=\"javascript:return CheckOnlyCharacter();\"  maxlength=40 /></TD>");
                    //        stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>*Age</TD>");
                    //        stbuild.Append("<TD valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2    /></TD>");
                    //        stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>Sex</TD>");
                    //        stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>");
                    //        stbuild.Append("<input id=RadM" + i + " type=radio runat=server  Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                    //        stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>&nbsp;");
                    //        stbuild.Append("<input type=radio id=RadF" + i + " runat=server  Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                    //        stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#cccccc></td></tr>");
                    //    }
                    //}
                    //else
                    //{
                    //    for (i = 1; i <= Convert.ToInt16(ViewState["paxnum"]); i++)
                    //    {
                    //        stbuild.Append("<TR ><TD class=style3 style=WIDTH: 150px valign=middle width=150 height=15>*Title</TD>");
                    //        stbuild.Append("<td class=style3 align=left style=WIDTH: 50px valign=middle width=50 height=15><select type=select onchange=\"changeSex(" + i + ");\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                    //        stbuild.Append("<TD class=style3 align=left valign=middle width=250 height=15>*Name</TD>");
                    //        stbuild.Append("<TD valign=middle align=left width=154 class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px;width:158px; size=40 onkeydown=\"javascript:return CheckOnlyCharacter();\" maxlength=40 /></TD>");
                    //        stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>*Age</TD>");
                    //        stbuild.Append("<TD style=WIDTH: 80px valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2   style=width:48px; /></TD>");
                    //        stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>Sex</TD>");
                    //        stbuild.Append("<TD class=hlinks style=WIDTH: 50px valign=middle align=center height=15>");
                    //        stbuild.Append("<input id=RadM" + i + " type=radio runat=server Width=46px Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                    //        stbuild.Append("<TD class=style3 style=WIDTH: 50px valign=middle align=center height=15>&nbsp;");
                    //        stbuild.Append("<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                    //    }
                    //}
                }
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (dt1 != null)
                {
                    dt1.Dispose();
                    dt1 = null;
                }
            }
        }

        protected void bindPax()
        {
            if (Convert.ToInt16(ViewState["paxnum"]) > 1)
            {
                for (i = 1; i <= Convert.ToInt16(ViewState["paxnum"]); i++)
                {
                    stbuild.Append("<TR ><TD class=hlinks  valign=middle  height=15>*Title</TD>");
                    stbuild.Append("<td class=hlinks align=left  valign=middle  height=15><select type=select onchange=\"changeSex('" + i + "');\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                    stbuild.Append("<TD class=hlinks align=left valign=middle  height=15>*Name</TD>");
                    stbuild.Append("<TD valign=middle align=left  class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px; onkeydown=\"javascript:return CheckOnlyCharacter();\"  maxlength=40 /></TD>");
                    stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>*Age</TD>");
                    stbuild.Append("<TD valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2    /></TD>");
                    stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>Sex</TD>");
                    stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>");
                    stbuild.Append("<input id=RadM" + i + " type=radio runat=server  Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                    stbuild.Append("<TD class=hlinks  valign=middle align=center height=15>&nbsp;");
                    stbuild.Append("<input type=radio id=RadF" + i + " runat=server  Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                    stbuild.Append("<tr><td  class=hlinks colspan=11 align=left bgcolor=#cccccc></td></tr>");
                }
            }
            else
            {
                for (i = 1; i <= Convert.ToInt16(ViewState["paxnum"]); i++)
                {
                    stbuild.Append("<TR ><TD class=style3 style=WIDTH: 150px valign=middle width=150 height=15>*Title</TD>");
                    stbuild.Append("<td class=style3 align=left style=WIDTH: 50px valign=middle width=50 height=15><select type=select onchange=\"changeSex(" + i + ");\" name=contact_title" + i + " id=Title" + i + " title=Alphanum><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                    stbuild.Append("<TD class=style3 align=left valign=middle width=250 height=15>*Name</TD>");
                    stbuild.Append("<TD valign=middle align=left width=154 class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px;width:158px; size=40 onkeydown=\"javascript:return CheckOnlyCharacter();\" maxlength=40 /></TD>");
                    stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>*Age</TD>");
                    stbuild.Append("<TD style=WIDTH: 80px valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2   style=width:48px; /></TD>");
                    stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>Sex</TD>");
                    stbuild.Append("<TD class=hlinks style=WIDTH: 50px valign=middle align=center height=15>");
                    stbuild.Append("<input id=RadM" + i + " type=radio runat=server Width=46px Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                    stbuild.Append("<TD class=style3 style=WIDTH: 50px valign=middle align=center height=15>&nbsp;");
                    stbuild.Append("<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                }
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindStateName();
            BindStateNameNew();
            bindPax();
        }
        protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
        {
            //BindCityName();
            //bindPax();
        }
        protected void OnCheckChanged_rdbIsGSTApplicableYes(object sender, EventArgs e)
        {
            divGSTDetails.Style["display"] = "";
        }
        protected void OnCheckChanged_rdbIsGSTApplicableNo(object sender, EventArgs e)
        {
            divGSTDetails.Style["display"] = "none";
        }

        #endregion
        #region "Method(s)"
        private void FillDgtour()
        {
            #region Optimize Code
            /*string str = "SELECT OnlineToursBooking.rowid as rowid,OnlineToursBooking.CalcTaxvalue, OnlineToursBooking.orderid, 
             * TourMaster.TourName, convert(varchar(10),OnlineToursBooking.doj,103) as doj, 
             * OnlineToursBooking.dob,convert(varchar(10),tours.ReturnDate,103)as ReturnDate,PickUpMaster.departtime,
             * case OnlineToursBooking.BusEnvType when 'Y' then 'AC' when 'N' then 'Non-AC' end as BusEnvType ,
             * OnlineToursBooking.tourid,OnlineToursBooking.noofadults,OnlineToursBooking.noofchild,OnlineToursBooking.NoofAdultsTwin,
             * OnlineToursBooking.NoofAdultsTriple,OnlineToursBooking.ChildWithoutbed,OnlineToursBooking.SingleAdult,OnlineToursBooking.dormitory,
             * OnlineToursBooking.amount,OnlineToursBooking.tax,(OnlineToursBooking.totalamount-OnlineToursBooking.CalcCreditCardFee) as totalamount,
             * OnlineToursBooking.CreditCardFee FROM OnlineToursBooking INNER JOIN TourMaster ON OnlineToursBooking.tourid = TourMaster.TourNo 
             * INNER JOIN PickUpMaster ON OnlineToursBooking.tourid = PickUpMaster.TourNo and pickupmaster.rowid=OnlineToursBooking.pickuppointid INNER JOIN PickupAddress ON PickUpMaster.RowId = PickupAddress.PickupMasterRowId inner join Tours on tours.Tourno=OnlineToursBooking.tourid and tours.rowid= OnlineToursBooking.tourserial and datediff(d,tours.Journeydate,OnlineToursBooking.doj)=0 AND
             * OnlineToursBooking.orderid = '" + DataLib.funClear(Request.QueryString["orderid"]) + "' ORDER BY OnlineToursBooking.rowid ";
            DataTable dtTourDisp = new DataTable();
            dtTourDisp = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
            #endregion
            ClsAdo pClsLinq = null;
            DataTable dtTourDisp = null;
            try
            {
                pClsLinq = new ClsAdo();
                dtTourDisp = pClsLinq.fnGetToursBookingInfoAgent(Convert.ToString(Request.QueryString["orderid"]));

                if (dtTourDisp != null && dtTourDisp.Rows.Count > 0)
                {
                    adults = Convert.ToInt32(dtTourDisp.Rows[0]["noofadults"].ToString());
                    childs = Convert.ToInt32(dtTourDisp.Rows[0]["noofchild"].ToString());
                    adulttwin = Convert.ToInt32(dtTourDisp.Rows[0]["NoofAdultsTwin"].ToString());
                    adulttriple = Convert.ToInt32(dtTourDisp.Rows[0]["NoofAdultsTriple"].ToString());
                    childwithoutbed = Convert.ToInt32(dtTourDisp.Rows[0]["ChildWithoutbed"].ToString());
                    singleadult = Convert.ToInt32(dtTourDisp.Rows[0]["SingleAdult"].ToString());
                    dormitory = Convert.ToInt32(dtTourDisp.Rows[0]["dormitory"].ToString());
                    ADWFood = Convert.ToInt32(dtTourDisp.Rows[0]["noAdultWithFood"].ToString());
                    CWFood = Convert.ToInt32(dtTourDisp.Rows[0]["noChildWithFood"].ToString());
                    if ((strtourid) == "")
                    {
                        fillddlJdate(Convert.ToInt32(dtTourDisp.Rows[0]["tourid"].ToString()));
                    }
                    else
                    {
                        fillddlJdate(Convert.ToInt32(strtourid));
                    }
                    tourid = Convert.ToInt32(dtTourDisp.Rows[0]["tourid"].ToString());
                    dgtourdt.DataSource = dtTourDisp;
                    dgtourdt.DataKeyField = "rowid";
                    dgtourdt.DataBind();
                    #region Optimize Code
                    /*GrandTotal = decimal.Round(Convert.ToDecimal(DataLib.GetStringData(DataLib.Connection.ConnectionString, 
                 * "select (sum(totalamount)-sum(calccreditcardfee)) as totalamount from OnlineToursBooking where 
                 * orderid='" + DataLib.funClear(Request.QueryString["orderid"]) + "'")));*/
                    #endregion
                    GrandTotal = Convert.ToDecimal(pClsLinq.fnGetTourBookTotalAmt(Convert.ToString(Request.QueryString["orderid"])));
                    ViewState["GrandTotal"] = GrandTotal;
                    TotalPaxAdults = (adults + adulttwin + adulttriple + singleadult + dormitory + ADWFood);
                    ViewState["TotalPaxAdults"] = TotalPaxAdults;
                    TotalPaxChilds = (childs + childwithoutbed + CWFood);
                    ViewState["TotalPaxChilds"] = TotalPaxChilds;
                    Session["Adults"] = TotalPaxAdults;
                    Session["Childs"] = TotalPaxChilds;
                    this.lblNoofAdults.Text = Convert.ToString(TotalPaxAdults);
                    this.lblNoofchild.Text = Convert.ToString(TotalPaxChilds);
                }
                else
                {
                    dgtourdt.DataSource = null;
                    dgtourdt.DataBind();
                    Response.Redirect("agenthomepage.aspx");
                }
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (dtTourDisp != null)
                {
                    dtTourDisp.Dispose();
                    dtTourDisp = null;
                }
            }

        }
        public void DoEdit(object sender, DataGridCommandEventArgs e)
        {
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                dgtourdt.EditItemIndex = e.Item.ItemIndex;
                emailid.Value = "";
                #region Optimize Code
                /*strtourid = DataLib.GetStringData(DataLib.Connection.ConnectionString, "SELECT OnlineToursBooking.tourid FROM OnlineToursBooking where rowid='" + dgtourdt.DataKeys[e.Item.ItemIndex] + "'");        */
                #endregion
                int? pRowID = Convert.ToInt32(dgtourdt.DataKeys[e.Item.ItemIndex]);
                strtourid = Convert.ToString(pClsLinq.fnGet_RowWiseTourID(pRowID));
                if ((strtourid.Length > 0) && (strtourid != ""))
                {
                    fillddlJdate(Convert.ToInt32(strtourid));
                }
                FillDgtour();
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
        }
        public void DoCancel(object sender, DataGridCommandEventArgs e)
        {
            dgtourdt.EditItemIndex = -1;
            emailid.Value = "";
            FillDgtour();
        }
        private void fillddlJdate(int TourNo)
        {
            ClsAdo pClsLinq = null;
            DataTable dt = null;
            try
            {
                pClsLinq = new ClsAdo();
                string hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
                #region Optimize Code
                /*dt = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "select distinct(JourneyDate) as doj from Tours where dateadd(hour," + hr + ",journeydate)>=getdate() and TourNo=" + TourNo + " order by journeydate DESC");*/
                #endregion
                dt = pClsLinq.fnGetJourneyDate(Convert.ToInt32(hr), TourNo);

                TempDataView = new DataView();
                TempDataView = dt.DefaultView;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
            }
        }
        public void DoUpdate(object sender, DataGridCommandEventArgs e)
        {
            ClsAdo pClsLinq = null;
            DataTable unblock = null;
            try
            {
                pClsLinq = new ClsAdo();
                emailid.Value = "";
                DropDownList doj;
                string journdate;
                int totalpax = 0, available = 0, tourserial = 0;
                string bus = "";
                bus = e.Item.Cells[6].Text;
                if (bus == "AC")
                {
                    bus = "Y";
                }
                else if (bus == "Non-AC")
                {
                    bus = "N";
                }
                DateTime dt;
                #region Optimize COde
                /*
            strtourid = DataLib.GetStringData(DataLib.Connection.ConnectionString, "SELECT OnlineToursBooking.tourid FROM OnlineToursBooking where rowid='" + dgtourdt.DataKeys[e.Item.ItemIndex] + "'");
             * */
                #endregion
                string pRowID = dgtourdt.DataKeys[e.Item.ItemIndex].ToString();
                strtourid = Convert.ToString(pClsLinq.fnGet_RowWiseTourID(Convert.ToInt32(pRowID)));
                totalpax = (Convert.ToInt32(ViewState["TotalPaxAdults"]) + Convert.ToInt32(ViewState["TotalPaxChilds"]));
                doj = (DropDownList)(e.Item.FindControl("cmbdoj"));
                journdate = doj.Text.ToString();
                dt = Convert.ToDateTime(doj.SelectedValue.ToString());
                available = ChekAvailability1(totalpax, Convert.ToInt32(strtourid), Convert.ToDateTime(journdate), bus);
                #region Optimize Code
                /*tourserial = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, "
             * Select * from Tours where datediff(\"d\",'" + journdate + "',JourneyDate) = 0 and TourNo=" + Convert.ToInt32(strtourid)));*/
                #endregion
                tourserial = pClsLinq.fnGet_TourWiseTourSr(Convert.ToInt32(strtourid), Convert.ToDateTime(journdate));
                if (totalpax <= available)
                {
                    #region Optimize COde

                    /*DataTable unblock;
                unblock = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "Select seatNo,busserialno from onlinetoursbooking where rowid='" + dgtourdt.DataKeys[e.Item.ItemIndex] + "'");*/
                    #endregion
                    int? iRowID = Convert.ToInt32(dgtourdt.DataKeys[e.Item.ItemIndex]);
                    unblock = pClsLinq.fnGetSeatDetail(iRowID);
                    if (unblock != null && unblock.Rows.Count > 0)
                    {
                        string preseat = Convert.ToString(unblock.Rows[0]["seatNo"]);
                        string serialno = Convert.ToString(unblock.Rows[0]["busserialno"]);
                        string[] seatnumbers = preseat.Split(',');
                        string[] ser = serialno.Split(',');
                        string numbers = "";
                        string tempstr = "";
                        if (ser.Length > 1)
                        {
                            for (int k = 0; k < ser.Length; k++)
                            {
                                tempstr = "";
                                for (int i = 0; i < seatnumbers.Length; i++)
                                {
                                    if (seatnumbers[i].Length > 3)
                                    {
                                        if (seatnumbers[i].Substring(1, 1) == Convert.ToString(k + 1))
                                        {
                                            numbers = "s" + seatnumbers[i].Substring(2, 2);
                                            tempstr = tempstr + "," + numbers;
                                        }
                                    }
                                    else
                                    {
                                        if (seatnumbers[i].Substring(1, 1) == Convert.ToString(k + 1))
                                        {
                                            numbers = "s" + seatnumbers[i].Substring(2, 1);
                                            tempstr = tempstr + "," + numbers;
                                        }
                                    }
                                }

                                tempstr = tempstr.Replace(",,", ",").TrimStart(',').TrimEnd(',');
                                doUnBlock(tempstr, Convert.ToInt32(ser[k]));
                            }
                        }
                        else
                        {
                            doUnBlock(preseat, Convert.ToInt32(ser[0]));
                        }
                    }
                    string seat = returnseat(totalpax, Convert.ToString(BusSeaterType));
                    int busserialno = Convert.ToInt32(ViewState["busserial"]);
                    doBlock(seat, busserialno);
                    int? pRowid1 = Convert.ToInt32(dgtourdt.DataKeys[e.Item.ItemIndex]);
                    #region Optimize COde
                    /*string sql = "update onlinetoursbooking set doj = '" + journdate + "',seatno='" + seat + "',BusSerialNo=" + busserialno + ",
                 * tourserial=" + tourserial + " where rowid='" + dgtourdt.DataKeys[e.Item.ItemIndex] + "'";

                DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, sql, false);*/
                    #endregion
                    int val = pClsLinq.fnUpdaterTourDeatil(pRowid1, Convert.ToDateTime(journdate), Convert.ToString(tourserial), Convert.ToString(busserialno), seat);

                    RegisterStartupScript("startupUpdate", "<script>alert('Updated Successfully !')</script>");

                    dgtourdt.EditItemIndex = -1;
                    FillDgtour();
                }
                else
                {
                    RegisterStartupScript("startupUpdate", "<script>alert('Seats Are Not Available!')</script>");
                    dgtourdt.EditItemIndex = -1;
                    FillDgtour();
                }
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (unblock != null)
                {
                    unblock.Dispose();
                    unblock = null;
                }
            }
        }
        private void doBlock(string tstr, int tserial)
        {
            string BlockedString, BlockedString1, BlockedString2, BlockedString3;
            BlockedString1 = Convert.ToString(DateTime.Now.Date);
            BlockedString2 = Convert.ToString(DateTime.Now.ToShortTimeString());
            BlockedString3 = Request.QueryString["orderid"];
            BlockedString = "Blocked By EBK0001" + "\n" + BlockedString1 + "\n" + BlockedString2 + "\n" + BlockedString3;
            Boolean tt = ClsCommon.Block_Seats(tstr, tserial, BlockedString);
            #region Optimize Code
            /*tstr = tstr.ToString().PadLeft(tstr.Length - 1);
            string[] arr;
            int i;
            arr = tstr.ToString().Split(',');
            string BlockedString, BlockedString1, BlockedString2, BlockedString3;
            BlockedString1 = Convert.ToString(DateTime.Now.Date);
            BlockedString2 = Convert.ToString(DateTime.Now.ToShortTimeString());
            BlockedString3 = Request.QueryString["orderid"];
            BlockedString = "Blocked By EBK0001" + "\n" + BlockedString1 + "\n" + BlockedString2 + "\n" + BlockedString3;
            for (i = 0; i <= arr.Length - 1; i++)
            {           
                if ((arr[i]) != "")
                {
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString,  "update seatarrangement set " + arr[i] + " ='" + BlockedString + "' where TourSerial=" + tserial + "",false);             
                }           
            }*/
            #endregion
        }
        private void doUnBlock(string tstr, int tserial)
        {
            ClsCommon.UnBlock_Seats(tstr, tserial);

            #region Optimize Code
            /*tstr = tstr.ToString().PadLeft(tstr.Length - 1);
            string[] arr;
            int i;
            arr = tstr.ToString().Split(',');
            for (i = 0; i <= arr.Length - 1; i++)
            {          
                if ((arr[i]) != "")
                {
                   DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, "update seatarrangement set " + arr[i] + " =null where TourSerial=" + tserial + "", false);                
                }          
            }*/
            #endregion
        }
        private int ChekAvailability1(int sreq, int tourno, System.DateTime jdate, string bustype)
        {
            string str1;
            int TourSerial = 0;
            string BusEnvType = "";
            ClsAdo pClsLinq = null;
            DataTable busallot = null;
            try
            {
                pClsLinq = new ClsAdo();
                TourSerial = pClsLinq.fnGet_TourWiseTourSr(tourno, jdate);
                #region Optimize Code
                /*str1 = "Select * from Tours where datediff(\"d\",'" + jdate + "',JourneyDate) = 0 and TourNo=" + tourno + "";
            TourSerial = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str1));*/



                /*DataTable busallot;
                busallot = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "Select * from busallot where TourSerial=" + TourSerial + " and AC='" + bustype + "' and (active='Y' or active='y')");*/
                #endregion

                busallot = pClsLinq.fnGetBusAllot_Detail(Convert.ToString(TourSerial), bustype, "a");
                if (busallot != null)
                {
                    for (int i = 0; i < Convert.ToInt32(busallot.Rows.Count); i++)
                    {
                        int seat = 0;
                        busserial[i] = Convert.ToInt32(busallot.Rows[i]["RowId"]);                //totalbuses = totalbuses + 1
                        ViewState["busserial"] = Convert.ToString(busallot.Rows[i]["RowId"]);
                        BusSeaterType = Convert.ToInt32(busallot.Rows[i]["BusType"].ToString().Substring(0, 2));
                        ViewState["BusSeaterType"] = BusSeaterType;
                        seat = GetAvailBusSeat(busserial[i]);
                        if (sreq <= seat)
                        {
                            AvailSeat = AvailSeat + seat;
                            break;
                        }
                        else
                        {
                            AvailSeat = 0;
                        }
                    }
                }
                return AvailSeat;
                return 0;
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
        private int GetAvailBusSeat(int CurrentBusSerial)
        {
            ClsAdo pClsLinq = null;
            DataTable vacent = null;
            try
            {
                pClsLinq = new ClsAdo();
                int Seat = 0, i;
                string str;
                /*DataTable vacent = new DataTable();
                str = "Select * from SeatArrangement where TourSerial=" + CurrentBusSerial + "";
                vacent = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str);*/
                vacent = pClsLinq.fnGetSeatArrangement(Convert.ToDecimal(CurrentBusSerial));
                if (vacent != null)
                {
                    for (i = 1; i <= BusSeaterType; i++)
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
        private string returnseat(int seatreq, string seattype)
        {
            int tmp;
            int tmp1;
            int seat = Convert.ToInt32(seattype);
            string tmpstr;
            tmp1 = seatreq;
            tmpstr = "";
            for (tmp = 1; tmp <= seat; tmp++)
            {
                if (nseats[tmp] == 1)
                {
                    tmpstr = tmpstr + "s" + tmp + ",";
                    tmp1 = tmp1 - 1;
                    if (tmp1 == 0)
                    {
                        break;
                    }
                }
                else
                {
                }
            }
            return tmpstr;
        }
        public void existingcustomer(int rowid, string orderid)
        {
            orderid = Request.QueryString["orderid"].Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            #region Optimize Code
            /*sqlreadexist = Convert.ToInt16(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select count(*) from onlinecustbyorder where orderid='" + orderid + "'"));
            string emailID;
            emailID = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select email from OnlineCustomer where rowid=" + rowid);
            string str1 = "SELECT   * FROM  OnlineCustomer WHERE  email = '" + emailID + "'";
            DataTable dtX = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);*/
            #endregion
            //ClsAdo pClsLinq = null;
            GST_Data objGST = null;
            DataTable dtX = null;
            try
            {
                //pClsLinq = new ClsAdo();
                objGST = new GST_Data();
                dtX = new DataTable();
                dtX = objGST.GST_fnExistCustomerDetail(orderid, rowid);

                if (dtX != null && dtX.Rows.Count > 0)
                {
                    txtName.Attributes.Add("readonly", "readonly");
                    txtMobile.Attributes.Add("readonly", "readonly");
                    txtMail.Attributes.Add("readonly", "readonly");

                    txtName.Value = dtX.Rows[0]["FirstName"].ToString();
                    txtAddress.Value = dtX.Rows[0]["Addr1"].ToString();
                    BindCountryName();
                    ddlCountry.SelectedValue = "1";
                    //BindStateName();
                    string s;
                    s = GetStateID(dtX.Rows[0]["state"].ToString());
                    if (s.Length > 0)
                    {
                        try
                        {
                            ddlState.SelectedValue = char.ToUpper(Convert.ToChar(s.Substring(0, 1))).ToString() + s.Substring(1, s.Length - 1);
                        }
                        catch { }
                    }
                    //BindCityName();
                    try
                    {
                        txtCity.Text = dtX.Rows[0]["City"].ToString();
                        //ddlCity.SelectedValue = dtX.Rows[0]["City"].ToString();
                    }
                    catch { }
                    //string s;
                    //s = dtX.Rows[0]["state"].ToString();
                    //if (s.Length > 0)
                    //{
                    //    try
                    //    {
                    //        ddlState.SelectedValue = char.ToUpper(Convert.ToChar(s.Substring(0, 1))).ToString() + s.Substring(1, s.Length - 1);
                    //    }
                    //    catch { }
                    //}
                    txtMobile.Value = dtX.Rows[0]["Mobile"].ToString();
                    txtMail.Value = dtX.Rows[0]["email"].ToString();
                    txtAlternateMobileno.Value = dtX.Rows[0]["AlternativeNo"].ToString();
                    Session["EmailId"] = txtMail.Value;
                    bool isGSTIN = DBNull.Value != dtX.Rows[0]["ISGSITIN"] ? Convert.ToBoolean(dtX.Rows[0]["ISGSITIN"]) : false;
                    string customerGSTIN = DBNull.Value != dtX.Rows[0]["CustomerGSTIN"] ? Convert.ToString(dtX.Rows[0]["CustomerGSTIN"]) : "";
                    string GSTINHolderName = DBNull.Value != dtX.Rows[0]["GstHolderName"] ? Convert.ToString(dtX.Rows[0]["GstHolderName"]) : "";
                    txtPincode.Value = DBNull.Value != dtX.Rows[0]["zipcode"] ? Convert.ToString(dtX.Rows[0]["zipcode"]) : "";

                    if (isGSTIN)
                    {
                        rdbIsGSTApplicableYes.Checked = true;
                        rdbIsGSTApplicableNo.Checked = false;
                        divGSTDetails.Style["display"] = "";
                        txtCustomerGSTIN.Text = customerGSTIN;
                        txtGstHolderName.Text = GSTINHolderName;
                    }
                    else
                    {
                        rdbIsGSTApplicableYes.Checked = false;
                        rdbIsGSTApplicableNo.Checked = true;
                        divGSTDetails.Style["display"] = "none";
                        txtCustomerGSTIN.Text = "";
                        txtGstHolderName.Text = "";
                    }
                    if (dtX.Rows[0]["PhoneNo"].ToString().IndexOf("-") != -1)
                    {
                        string[] tempPhone = dtX.Rows[0]["PhoneNo"].ToString().Split('-');
                        txtPhoneCountryCode.Value = tempPhone[0];
                        txtPhone.Value = tempPhone[1];
                    }
                    else
                    {
                        txtPhone.Value = dtX.Rows[0]["PhoneNo"].ToString();
                    }
                    chkPromotions.Checked = Convert.ToBoolean(dtX.Rows[0]["CanSendPromotions"] != DBNull.Value ?
                       Convert.ToInt16(dtX.Rows[0]["CanSendPromotions"]) : 1);
                }
                else
                {
                    txtName.Attributes.Remove("readonly");
                    txtMobile.Attributes.Remove("readonly");
                    txtMail.Attributes.Remove("readonly");

                    txtName.Value = "";
                    txtAddress.Value = "";
                    ddlState.SelectedValue = "";
                    TxtForeignState.Text = "";
                    if (type.Value == "email")
                    {
                        txtMobile.Value = "";
                        txtMail.Value = emailid.Value;
                    }
                    else if (type.Value == "Mobile")
                    {
                        txtMobile.Value = emailid.Value;
                        txtMail.Value = "";
                    }
                    txtPhone.Value = "";
                    txtPhoneCountryCode.Value = "";
                    chkPromotions.Checked = true;
                }
            }
            finally
            {
                if (objGST != null)
                {
                    objGST = null;
                }
                if (dtX != null)
                {
                    dtX.Dispose();
                    dtX = null;
                }
            }
        }

        public void existingcustomernew(int rowid, string orderid)
        {
            orderid = Request.QueryString["orderid"].Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            #region Optimize Code
            /*sqlreadexist = Convert.ToInt16(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select count(*) from onlinecustbyorder where orderid='" + orderid + "'"));
            string emailID;
            emailID = DataLib.GetStringData(DataLib.Connection.ConnectionString, "select email from OnlineCustomer where rowid=" + rowid);
            string str1 = "SELECT   * FROM  OnlineCustomer WHERE  email = '" + emailID + "'";
            DataTable dtX = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);*/
            #endregion
            //ClsAdo pClsLinq = null;
            GST_Data objGST = null;
            DataTable dtX = null;
            try
            {
                //pClsLinq = new ClsAdo();
                objGST = new GST_Data();
                dtX = new DataTable();
                dtX = objGST.GST_fnExistCustomerDetail(orderid, rowid);

                if (dtX != null && dtX.Rows.Count > 0)
                {
                    txtName.Attributes.Add("readonly", "readonly");
                    txtMobile.Attributes.Add("readonly", "readonly");
                    txtMail.Attributes.Add("readonly", "readonly");

                    txtName.Value = dtX.Rows[0]["FirstName"].ToString();
                    txtAddress.Value = dtX.Rows[0]["Addr1"].ToString();

                    txtAadharNo.Value = dtX.Rows[0]["Aadharno"].ToString();


                    //changes on 11/06/2018

                    ddlCountry.Items.Clear();
                    ddlNationality.Items.Clear();
                    //                BindCountryNameNew(-1);
                    ddlState.Items.Clear();
                    BindStateNameNew();
                    //ddlCountry.SelectedValue = "1";
                    //ddlCountry.SelectedValue = dtX.Rows[0]["Country"].ToString();
                    //ddlNationality.SelectedValue = dtX.Rows[0]["Nationality"].ToString();
                    //BindStateName();
                    string s, stname;
                    s = GetStateID(dtX.Rows[0]["state"].ToString());
                    stname = dtX.Rows[0]["state"].ToString();
                    if (s.Length > 0)
                    {
                        try
                        {
                            BindNationality(-1);
                            if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["Nationality"])))
                            {
                                try
                                {

                                    if (Convert.ToString(dtX.Rows[0]["Nationality"]).Length <= 2)
                                    {
                                        ddlNationality.SelectedValue = Convert.ToString(dtX.Rows[0]["Nationality"]);
                                    }
                                    else
                                    {
                                        ddlNationality.Items.FindByText(Convert.ToString(dtX.Rows[0]["Nationality"])).Selected = true;
                                    }
                                }
                                catch { }
                            }
                            else
                            {
                                ddlNationality.SelectedValue = "0";
                            }

                            BindCountry(-1);
                            if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["Country"])))
                            {
                                try
                                {
                                    if (Convert.ToString(dtX.Rows[0]["Country"]).Length <= 2)
                                    {
                                        ddlCountry.Items.FindByValue(Convert.ToString(dtX.Rows[0]["Country"])).Selected = true;
                                        //ddlCountry.SelectedValue = Convert.ToString(dtX.Rows[0]["Country"]);
                                    }
                                    else
                                    {
                                        ddlCountry.Items.FindByText(Convert.ToString(dtX.Rows[0]["Country"])).Selected = true;
                                    }
                                }
                                catch { }
                            }
                            else
                            {
                                ddlCountry.SelectedValue = "0";
                            }



                            if (Convert.ToString(dtX.Rows[0]["Country"]) == "59" || Convert.ToString(dtX.Rows[0]["Country"]) == "India")
                            {

                                //changes on 11/06/2018

                                string s1 = char.ToUpper(Convert.ToChar(s.Substring(0, 1))).ToString() + s.Substring(1, s.Length - 1);

                                if (s == "0")
                                    ddlState.Items.FindByText(stname).Selected = true;
                                else
                                    ddlState.SelectedValue = s1;
                                //ddlState.Items.FindByValue(s1).Selected = true;
                                ddlState.Style.Clear();
                                ddlState.Style.Add("display", "block");
                                TxtForeignState.Style.Clear();
                                TxtForeignState.Style.Add("display", "none");


                                txtCity.Text = dtX.Rows[0]["City"].ToString();
                                txtCity.Style.Add("display", "block");
                                txtForeignCity.Style.Clear();
                                txtForeignCity.Style.Add("display", "none");
                            }
                            else
                            {

                                TxtForeignState.Text = dtX.Rows[0]["state"].ToString();
                                TxtForeignState.Style.Clear();
                                TxtForeignState.Style.Add("display", "block");
                                ddlState.Style.Clear();
                                ddlState.Style.Add("display", "none");

                                //changes on 11/06/2018

                                txtForeignCity.Style.Add("display", "block");
                                txtForeignCity.Text = dtX.Rows[0]["City"].ToString();
                                txtCity.Style.Clear();
                                txtCity.Style.Add("display", "none");
                            }
                        }
                        catch { }
                    }
                    //BindCityName();
                    try
                    {

                        //ddlCity.SelectedValue = dtX.Rows[0]["City"].ToString();
                    }
                    catch { }
                    //string s;
                    //s = dtX.Rows[0]["state"].ToString();
                    //if (s.Length > 0)
                    //{
                    //    try
                    //    {
                    //        ddlState.SelectedValue = char.ToUpper(Convert.ToChar(s.Substring(0, 1))).ToString() + s.Substring(1, s.Length - 1);
                    //    }
                    //    catch { }
                    //}
                    txtMobile.Value = dtX.Rows[0]["Mobile"].ToString();
                    txtMail.Value = dtX.Rows[0]["email"].ToString();
                    txtAlternateMobileno.Value = dtX.Rows[0]["AlternativeNo"].ToString();
                    Session["EmailId"] = txtMail.Value;
                    bool isGSTIN = DBNull.Value != dtX.Rows[0]["ISGSITIN"] ? Convert.ToBoolean(dtX.Rows[0]["ISGSITIN"]) : false;
                    string customerGSTIN = DBNull.Value != dtX.Rows[0]["CustomerGSTIN"] ? Convert.ToString(dtX.Rows[0]["CustomerGSTIN"]) : "";
                    string GSTINHolderName = DBNull.Value != dtX.Rows[0]["GstHolderName"] ? Convert.ToString(dtX.Rows[0]["GstHolderName"]) : "";
                    txtPincode.Value = DBNull.Value != dtX.Rows[0]["zipcode"] ? Convert.ToString(dtX.Rows[0]["zipcode"]) : "";

                    if (isGSTIN)
                    {
                        rdbIsGSTApplicableYes.Checked = true;
                        rdbIsGSTApplicableNo.Checked = false;
                        divGSTDetails.Style["display"] = "";
                        txtCustomerGSTIN.Text = customerGSTIN;
                        txtGstHolderName.Text = GSTINHolderName;
                    }
                    else
                    {
                        rdbIsGSTApplicableYes.Checked = false;
                        rdbIsGSTApplicableNo.Checked = true;
                        divGSTDetails.Style["display"] = "none";
                        txtCustomerGSTIN.Text = "";
                        txtGstHolderName.Text = "";
                    }
                    if (dtX.Rows[0]["PhoneNo"].ToString().IndexOf("-") != -1)
                    {
                        string[] tempPhone = dtX.Rows[0]["PhoneNo"].ToString().Split('-');
                        txtPhoneCountryCode.Value = tempPhone[0];
                        txtPhone.Value = tempPhone[1];
                    }
                    else
                    {
                        txtPhone.Value = dtX.Rows[0]["PhoneNo"].ToString();
                    }
                    chkPromotions.Checked = Convert.ToBoolean(dtX.Rows[0]["CanSendPromotions"] != DBNull.Value ?
                       Convert.ToInt16(dtX.Rows[0]["CanSendPromotions"]) : 1);
                }
                else
                {
                    txtName.Attributes.Remove("readonly");
                    txtMobile.Attributes.Remove("readonly");
                    txtMail.Attributes.Remove("readonly");

                    txtName.Value = "";
                    txtAddress.Value = "";
                    ddlState.SelectedValue = "";
                    TxtForeignState.Text = "";
                    if (type.Value == "email")
                    {
                        txtMobile.Value = "";
                        txtMail.Value = emailid.Value;
                    }
                    else if (type.Value == "Mobile")
                    {
                        txtMobile.Value = emailid.Value;
                        txtMail.Value = "";
                    }
                    txtPhone.Value = "";
                    txtPhoneCountryCode.Value = "";
                    chkPromotions.Checked = true;
                }
            }
            finally
            {
                if (objGST != null)
                {
                    objGST = null;
                }
                if (dtX != null)
                {
                    dtX.Dispose();
                    dtX = null;
                }
            }
        }

        Boolean insertCustomerDetails()
        {
            int rowidtemp;
            int OAge;
            string CustName = txtName.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Address = txtAddress.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Nationality = ddlNationality.SelectedValue;
            string state = ddlState.SelectedItem.Text.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string phone = txtPhoneCountryCode.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "") + "-" + txtPhone.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Mobile = txtMobile.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string emailID = txtMail.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");

            string Country = ddlCountry.SelectedItem.Text.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");

            string City = txtCity.Text.Trim();//ddlCity.SelectedItem.Text.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");

            string ZipCode = txtPincode.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            bool IsGSTIN = false;
            string CustomerGSTIN = "";
            string GSTHolderName = "";

            //**************Aadhar No Add********************
            string AadharNo = txtAadharNo.Value;
            string AadharImage = "";
            if (fupAadhar.HasFile)
            {
                AadharImage = fupAadhar.FileName;
            }
            //**********************************************

            if (rdbIsGSTApplicableYes.Checked == true)
            {
                IsGSTIN = true;
            }
            else
            {
                IsGSTIN = false;
            }
            if (IsGSTIN == true)
            {
                CustomerGSTIN = txtCustomerGSTIN.Text.Trim();
                GSTHolderName = txtGstHolderName.Text.Trim();
            }
            else
            {
                CustomerGSTIN = "";
                GSTHolderName = "";
            }

            int[] busserial = new int[31];
            string str71;
            string pwd1;
            if (Convert.ToString(emailID) == "")
            {
                emailID = Request.QueryString["orderid"] + "@temp.com";
            }
            orderid = Request.QueryString["orderid"];
            //pwd1 = Request.QueryString["orderid"].Substring(0, 6);// Strings.Left(CustName, 2) + Strings.Left(orderid, 4);
            pwd1 = Convert.ToString(ViewState["strPass"]);
            ClsAdo pClsLinq = null;
            GST_Data objGst = null;
            try
            {
                objGst = new GST_Data();
                pClsLinq = new ClsAdo();
                if (!(Convert.ToString(ViewState["rowid"]) == ""))
                {
                    GST_OnlineCustomer clsTblObj = new GST_OnlineCustomer();
                    clsTblObj.RowId = Convert.ToInt32(ViewState["rowid"]);
                    clsTblObj.FirstName = CustName;
                    clsTblObj.Addr1 = Address;
                    clsTblObj.Nationality = Nationality;
                    clsTblObj.state = state;
                    clsTblObj.PhoneNo = phone;
                    clsTblObj.Mobile = Mobile;
                    clsTblObj.AlternativeNo = txtAlternateMobileno.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", ""); ;
                    clsTblObj.email = emailID;
                    clsTblObj.CanSendPromotions = chkPromotions.Checked;
                    clsTblObj.Country = Country;
                    clsTblObj.City = City;
                    clsTblObj.zipcode = ZipCode;
                    clsTblObj.ISGSITIN = IsGSTIN;
                    clsTblObj.GstHolderName = GSTHolderName;
                    clsTblObj.CustomerGSTIN = CustomerGSTIN;

                    int val = objGst.GST_fnInsertUpdateCustomerDetail(clsTblObj, Convert.ToString(Request.QueryString["orderid"]), AadharNo, AadharImage);

                    if (val == -1)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Email already exists');</script>");
                        return false;
                    }
                    else if (val > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Customer Record Successfully Updated.');</script>");
                    }
                }
                else
                {
                    GST_OnlineCustomer clsTblObj = new GST_OnlineCustomer();
                    clsTblObj.RowId = -1;
                    clsTblObj.FirstName = CustName;
                    clsTblObj.Addr1 = Address;
                    clsTblObj.state = state;
                    clsTblObj.PhoneNo = phone;
                    clsTblObj.Mobile = Mobile;
                    clsTblObj.AlternativeNo = txtAlternateMobileno.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", ""); ;
                    clsTblObj.email = emailID;
                    clsTblObj.password = pwd1;
                    clsTblObj.Nationality = Nationality;
                    clsTblObj.Country = Country;
                    clsTblObj.City = City;
                    clsTblObj.zipcode = ZipCode;
                    clsTblObj.ISGSITIN = IsGSTIN;
                    clsTblObj.GstHolderName = GSTHolderName;
                    clsTblObj.CustomerGSTIN = CustomerGSTIN;
                    int val = objGst.GST_fnInsertUpdateCustomerDetail(clsTblObj, Convert.ToString(Request.QueryString["orderid"]), AadharNo, AadharImage);
                    if (val == -1)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Email already exists');</script>");
                        return false;
                    }
                    else if (val > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Customer Record Successfully Added.');</script>");

                        string filepath = Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\forgotpass.html");
                        System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                        string strToSend = sr.ReadToEnd();
                        #region Optimize Code
                        /*string strrr = "select top 1 rowid from onlinecustomer order by 1 desc";
                    string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, strrr);*/
                        #endregion
                        int crowid = pClsLinq.fnGetCustRowID(); ;
                        strToSend = strToSend.Replace("#membername#", CustName);
                        strToSend = strToSend.Replace("#MemberUserName#", emailID);
                        strToSend = strToSend.Replace("#MemberPassword#", ViewState["Spass"].ToString());
                        string strMobile = Mobile;
                        try
                        {
                            //MailMessage sendMail = new MailMessage();
                            //sendMail.To = emailID;
                            //sendMail.From = "info@southerntravels.in"; //ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                            //sendMail.Body = strToSend;
                            //sendMail.BodyFormat = MailFormat.Html;
                            //sendMail.Subject = "Your Southern Travels Account Details.";
                            //SmtpMail.Send(sendMail);

                            //MailMessage sendMail = new MailMessage();
                            //sendMail.To = emailID;
                            //sendMail.From = "info@southerntravels.in"; //ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                            //sendMail.Body = strToSend;
                            //sendMail.BodyFormat = MailFormat.Html;
                            //sendMail.Subject = "Your Southern Travels Account Details.";
                            //SmtpMail.Send(sendMail);

                            ClsCommon.sendmail(emailID, "", "", "info@southerntravels.in", "Your Southern Travels Account Details.", strToSend, "");

                            if (strMobile != "")
                            {
                                StringBuilder strMsg = new StringBuilder();
                                string strSms = "Your Login Password is:" + ViewState["Spass"].ToString() + " Happy Holidaying...";
                                string strPhNo = strMobile.Trim().TrimStart('0');
                                strSms.Replace("\n", "");
                                string rtnmessage = DataLib.sendsms(Convert.ToInt32(crowid), strPhNo, strSms, "OnLineUser", "EBK0001");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<!-- " + ex.ToString() + " -->");
                        }
                        finally
                        {
                        }
                    }
                }
                int chkPan = SaveOnlinePassenger(emailID);
                Session["orderid"] = Request.QueryString["orderid"];
                alertSeatFull();

                #region Optimize Code
                /*if (!(ViewState["rowid"] == null))
                {
                    str71 = "Select email,mobile from onlinecustomer where email='" + emailID + "'and rowid not in('" + ViewState["rowid"] + "')";
                    DataTable dtcheck = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str71);
                    if (dtcheck.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Email already exists');</script>");
                        return false;
                    }
                    else
                    {
                        str71 = "update onlinecustomer set FirstName='" + CustName + "',Addr1='" + Address + "',State='" + state + "',PhoneNo='" + phone + "',Mobile='" + Mobile + "',Email='" + emailID + "' where rowid='" + ViewState["rowid"] + "'";
                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str71, false);
                    }
                }
                else
                {
                    string str1 = "SELECT * FROM OnlineCustomer  where email ='" + emailID + "'";
                    DataTable dtX = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);
                    string email = "";
                    if (dtX.Rows.Count > 0)
                    {
                        email = dtX.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        email = emailID;
                    }
                    if (dtX.Rows.Count > 0)
                    {
                        str71 = "Select email,mobile from onlinecustomer where email='" + email + "'";
                        DataTable dtcheck1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str71);
                        if (dtcheck1.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "error2", "<script>alert('Email already exists');</script>");
                            return false;
                        }
                        else
                        {
                            str71 = "update onlinecustomer set FirstName='" + CustName + "',Addr1='" + Address + "',State='" + state + "',PhoneNo='" + phone + "',Mobile='" + Mobile + "',Email='" + emailID + "' where email=  '" + email + "'";
                            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str71, false);
                        }
                    }
                    else
                    {
                        str71 = "Insert into onlinecustomer(FirstName,Addr1,State,PhoneNo,Mobile,Email,password) values('" + CustName + "','" + Address + "','" + state + "','" + phone + "','" + Mobile + "','" + emailID + "','" + pwd1 + "')";
                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str71, false);


                        string filepath = Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\forgotpass.html");
                        System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                        string strToSend = sr.ReadToEnd();
                        string strrr = "select top 1 rowid from onlinecustomer order by 1 desc";
                        string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, strrr);
                        int crowid = Convert.ToInt32(rr);
                        strToSend = strToSend.Replace("#membername#", CustName);
                        strToSend = strToSend.Replace("#MemberUserName#", emailID);
                        strToSend = strToSend.Replace("#MemberPassword#", ViewState["Spass"].ToString());
                        string strMobile = Mobile;
                        try
                        {
                            MailMessage sendMail = new MailMessage();
                            sendMail.To = emailID;
                            sendMail.From = "info@southerntravels.in"; //ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                            sendMail.Body = strToSend;
                            sendMail.BodyFormat = MailFormat.Html;
                            sendMail.Subject = "Your Southern Travels Account Details.";                       
                            SmtpMail.Send(sendMail);

                            if (strMobile != "")
                            {
                                StringBuilder strMsg = new StringBuilder();
                                string strSms = "Your Login Password is:" + ViewState["Spass"].ToString() + " Happy Holidaying...";
                                string strPhNo = strMobile.Trim().TrimStart('0');
                                strSms.Replace("\n", "");
                                string rtnmessage = DataLib.sendsms(Convert.ToInt32(crowid), strPhNo, strSms, "OnLineUser", "EBK0001");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<!-- " + ex.ToString() + " -->");
                        }
                        finally
                        {
                        }
                    }

                }
             * fnInsertOnlinePassengerAgent
                string str81 = "select rowid from onlinecustomer where email='" + emailID + "'";
                rowidtemp = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str81));
                OName = Request["TxtOName" + i.ToString()];           
                paxnum = (ViewState["paxnum"].ToString());
                for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                {
                    OName = Request["TxtOName" + i.ToString()];
                    title = Request["contact_title" + i.ToString()];
                    if (OName != "")
                    {
                        OName = title + " " + OName;
                    }
                    OAge = Convert.ToInt16(Request["txtAge" + i.ToString()]);
                    if (OAge == 0)
                    {
                        flag = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                    else
                        flag = true;
                    string str21;
                    str21 = "insert into onlinepassenger (ocustrowid,orderid,name,age,sex) " + " values (" + rowidtemp.ToString() + ",'" + orderid + "'," + "'" + OName + "'," + Request["txtAge" + i.ToString()] + ",'" + Request["Radio" + i.ToString()] + "')";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str21, false);
                }
                if (!(ViewState["rowid"] == null))
                {
                    rowidtemp = Convert.ToInt32(ViewState["rowid"]);
                    String str1 = "insert into onlinecustbyorder (orderid,ocustrowid) " + " values ('" + orderid + "'," + rowidtemp + ")";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);
                }
                else
                {
                    int newrowidtemp;
                    newrowidtemp = rowidtemp;               
                    String str2 = "insert into onlinecustbyorder (orderid,ocustrowid) " + " values ('" + orderid + "'," + newrowidtemp + ")";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str2, false);
                }
            }
            Session["orderid"] = Request.QueryString["orderid"];
            alertSeatFull();*/
                #endregion
                return true;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (objGst != null)
                {
                    objGst = null;
                }
            }
        }

        Boolean insertCustomerDetailsnew()
        {
            int rowidtemp;
            int OAge;
            string CustName = txtName.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Address = txtAddress.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Nationality = ddlNationality.SelectedValue;
            string state = string.Empty;
            string City = string.Empty;
            string phone = txtPhoneCountryCode.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "") + "-" + txtPhone.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Mobile = txtMobile.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string emailID = txtMail.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");

            // string Country = ddlCountry.SelectedItem.Text.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            string Country = ddlCountry.SelectedValue;

            //**************Aadhar No Add********************
            string AadharNo = txtAadharNo.Value;
            string AadharImage = "";
            if (fupAadhar.HasFile)
            {
                AadharImage = fupAadhar.FileName;
            }
            //**********************************************

            if (ddlCountry.SelectedValue == "59")
            {
                state = ddlState.SelectedItem.Text.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            }
            else
            {
                state = TxtForeignState.Text;
            }


            if (ddlCountry.SelectedValue == "59")
            {
                City = txtCity.Text.Trim();
            }
            else
            {
                City = txtForeignCity.Text.Trim();
            }

            // string City = txtCity.Text.Trim();//ddlCity.SelectedItem.Text.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");

            string ZipCode = txtPincode.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", "");
            bool IsGSTIN = false;
            string CustomerGSTIN = "";
            string GSTHolderName = "";
            if (rdbIsGSTApplicableYes.Checked == true)
            {
                IsGSTIN = true;
            }
            else
            {
                IsGSTIN = false;
            }
            if (IsGSTIN == true)
            {
                CustomerGSTIN = txtCustomerGSTIN.Text.Trim();
                GSTHolderName = txtGstHolderName.Text.Trim();
            }
            else
            {
                CustomerGSTIN = "";
                GSTHolderName = "";
            }

            int[] busserial = new int[31];
            string str71;
            string pwd1;
            if (Convert.ToString(emailID) == "")
            {
                emailID = Request.QueryString["orderid"] + "@temp.com";
            }
            orderid = Request.QueryString["orderid"];
            //pwd1 = Request.QueryString["orderid"].Substring(0, 6);// Strings.Left(CustName, 2) + Strings.Left(orderid, 4);
            pwd1 = Convert.ToString(ViewState["strPass"]);
            ClsAdo pClsLinq = null;
            GST_Data objGst = null;
            try
            {
                objGst = new GST_Data();

                pClsLinq = new ClsAdo();
                if (!(Convert.ToString(ViewState["rowid"]) == ""))
                {
                    GST_OnlineCustomer clsTblObj = new GST_OnlineCustomer();
                    clsTblObj.RowId = Convert.ToInt32(ViewState["rowid"]);
                    clsTblObj.FirstName = CustName;
                    clsTblObj.Addr1 = Address;
                    clsTblObj.Nationality = Nationality;
                    clsTblObj.state = state;
                    clsTblObj.PhoneNo = phone;
                    clsTblObj.Mobile = Mobile;
                    clsTblObj.AlternativeNo = txtAlternateMobileno.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", ""); ;
                    clsTblObj.email = emailID;
                    clsTblObj.CanSendPromotions = chkPromotions.Checked;
                    clsTblObj.Country = Country;
                    clsTblObj.City = City;
                    clsTblObj.zipcode = ZipCode;
                    clsTblObj.ISGSITIN = IsGSTIN;
                    clsTblObj.GstHolderName = GSTHolderName;
                    clsTblObj.CustomerGSTIN = CustomerGSTIN;

                    int val = objGst.GST_fnInsertUpdateCustomerDetail(clsTblObj, Convert.ToString(Request.QueryString["orderid"]), AadharNo, AadharImage);

                    if (val == -1)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Email already exists');</script>");
                        return false;
                    }
                    else if (val > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Customer Record Successfully Updated.');</script>");
                    }
                }
                else
                {
                    GST_OnlineCustomer clsTblObj = new GST_OnlineCustomer();
                    clsTblObj.RowId = -1;
                    clsTblObj.FirstName = CustName;
                    clsTblObj.Addr1 = Address;
                    clsTblObj.state = state;
                    clsTblObj.PhoneNo = phone;
                    clsTblObj.Mobile = Mobile;
                    clsTblObj.AlternativeNo = txtAlternateMobileno.Value.Trim().Replace("'", "''").Replace("<", "").Replace(">", "").Replace("alert", ""); ;
                    clsTblObj.email = emailID;
                    clsTblObj.password = pwd1;
                    clsTblObj.Nationality = Nationality;
                    clsTblObj.Country = Country;
                    clsTblObj.City = City;
                    clsTblObj.zipcode = ZipCode;
                    clsTblObj.ISGSITIN = IsGSTIN;
                    clsTblObj.GstHolderName = GSTHolderName;
                    clsTblObj.CustomerGSTIN = CustomerGSTIN;
                    int val = objGst.GST_fnInsertUpdateCustomerDetail(clsTblObj, Convert.ToString(Request.QueryString["orderid"]), AadharNo, AadharImage);
                    if (val == -1)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Email already exists');</script>");
                        return false;
                    }
                    else if (val > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Customer Record Successfully Added.');</script>");

                        string filepath = Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\forgotpass.html");
                        System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                        string strToSend = sr.ReadToEnd();
                        #region Optimize Code
                        /*string strrr = "select top 1 rowid from onlinecustomer order by 1 desc";
                    string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, strrr);*/
                        #endregion
                        int crowid = pClsLinq.fnGetCustRowID(); ;
                        strToSend = strToSend.Replace("#membername#", CustName);
                        strToSend = strToSend.Replace("#MemberUserName#", emailID);
                        strToSend = strToSend.Replace("#MemberPassword#", ViewState["Spass"].ToString());
                        string strMobile = Mobile;
                        try
                        {
                            //MailMessage sendMail = new MailMessage();
                            //sendMail.To = emailID;
                            //sendMail.From = "info@southerntravels.in"; //ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                            //sendMail.Body = strToSend;
                            //sendMail.BodyFormat = MailFormat.Html;
                            //sendMail.Subject = "Your Southern Travels Account Details.";
                            //SmtpMail.Send(sendMail);

                            //MailMessage sendMail = new MailMessage();
                            //sendMail.To = emailID;
                            //sendMail.From = "info@southerntravels.in"; //ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                            //sendMail.Body = strToSend;
                            //sendMail.BodyFormat = MailFormat.Html;
                            //sendMail.Subject = "Your Southern Travels Account Details.";
                            //SmtpMail.Send(sendMail);

                            ClsCommon.sendmail(emailID, "", "", "info@southerntravels.in", "Your Southern Travels Account Details.", strToSend, "");

                            if (strMobile != "")
                            {
                                StringBuilder strMsg = new StringBuilder();
                                string strSms = "Your Login Password is:" + ViewState["Spass"].ToString() + " Happy Holidaying...";
                                string strPhNo = strMobile.Trim().TrimStart('0');
                                strSms.Replace("\n", "");
                                string rtnmessage = DataLib.sendsms(Convert.ToInt32(crowid), strPhNo, strSms, "OnLineUser", "EBK0001");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<!-- " + ex.ToString() + " -->");
                        }
                        finally
                        {
                        }
                    }
                }
                int chkPan = SaveOnlinePassenger(emailID);
                Session["orderid"] = Request.QueryString["orderid"];
                alertSeatFull();

                #region Optimize Code

                /*if (!(ViewState["rowid"] == null))
                {
                    str71 = "Select email,mobile from onlinecustomer where email='" + emailID + "'and rowid not in('" + ViewState["rowid"] + "')";
                    DataTable dtcheck = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str71);
                    if (dtcheck.Rows.Count > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "error1", "<script>alert('Email already exists');</script>");
                        return false;
                    }
                    else
                    {
                        str71 = "update onlinecustomer set FirstName='" + CustName + "',Addr1='" + Address + "',State='" + state + "',PhoneNo='" + phone + "',Mobile='" + Mobile + "',Email='" + emailID + "' where rowid='" + ViewState["rowid"] + "'";
                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str71, false);
                    }
                }
                else
                {
                    string str1 = "SELECT * FROM OnlineCustomer  where email ='" + emailID + "'";
                    DataTable dtX = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str1);
                    string email = "";
                    if (dtX.Rows.Count > 0)
                    {
                        email = dtX.Rows[0]["Email"].ToString();
                    }
                    else
                    {
                        email = emailID;
                    }
                    if (dtX.Rows.Count > 0)
                    {
                        str71 = "Select email,mobile from onlinecustomer where email='" + email + "'";
                        DataTable dtcheck1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, str71);
                        if (dtcheck1.Rows.Count > 0)
                        {
                            ClientScript.RegisterStartupScript(GetType(), "error2", "<script>alert('Email already exists');</script>");
                            return false;
                        }
                        else
                        {
                            str71 = "update onlinecustomer set FirstName='" + CustName + "',Addr1='" + Address + "',State='" + state + "',PhoneNo='" + phone + "',Mobile='" + Mobile + "',Email='" + emailID + "' where email=  '" + email + "'";
                            DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str71, false);
                        }
                    }
                    else
                    {
                        str71 = "Insert into onlinecustomer(FirstName,Addr1,State,PhoneNo,Mobile,Email,password) values('" + CustName + "','" + Address + "','" + state + "','" + phone + "','" + Mobile + "','" + emailID + "','" + pwd1 + "')";
                        DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str71, false);


                        string filepath = Server.MapPath("../" + ConfigurationManager.AppSettings["mailerTemplatesPath"].ToString() + "\\forgotpass.html");
                        System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                        string strToSend = sr.ReadToEnd();
                        string strrr = "select top 1 rowid from onlinecustomer order by 1 desc";
                        string rr = DataLib.GetStringData(DataLib.Connection.ConnectionString, strrr);
                        int crowid = Convert.ToInt32(rr);
                        strToSend = strToSend.Replace("#membername#", CustName);
                        strToSend = strToSend.Replace("#MemberUserName#", emailID);
                        strToSend = strToSend.Replace("#MemberPassword#", ViewState["Spass"].ToString());
                        string strMobile = Mobile;
                        try
                        {
                            MailMessage sendMail = new MailMessage();
                            sendMail.To = emailID;
                            sendMail.From = "info@southerntravels.in"; //ConfigurationSettings.AppSettings["mailAddress"].ToString();//"krishna@sirez.com";
                            sendMail.Body = strToSend;
                            sendMail.BodyFormat = MailFormat.Html;
                            sendMail.Subject = "Your Southern Travels Account Details.";                       
                            SmtpMail.Send(sendMail);

                            if (strMobile != "")
                            {
                                StringBuilder strMsg = new StringBuilder();
                                string strSms = "Your Login Password is:" + ViewState["Spass"].ToString() + " Happy Holidaying...";
                                string strPhNo = strMobile.Trim().TrimStart('0');
                                strSms.Replace("\n", "");
                                string rtnmessage = DataLib.sendsms(Convert.ToInt32(crowid), strPhNo, strSms, "OnLineUser", "EBK0001");
                            }
                        }
                        catch (Exception ex)
                        {
                            Response.Write("<!-- " + ex.ToString() + " -->");
                        }
                        finally
                        {
                        }
                    }

                }
             * fnInsertOnlinePassengerAgent
                string str81 = "select rowid from onlinecustomer where email='" + emailID + "'";
                rowidtemp = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str81));
                OName = Request["TxtOName" + i.ToString()];           
                paxnum = (ViewState["paxnum"].ToString());
                for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                {
                    OName = Request["TxtOName" + i.ToString()];
                    title = Request["contact_title" + i.ToString()];
                    if (OName != "")
                    {
                        OName = title + " " + OName;
                    }
                    OAge = Convert.ToInt16(Request["txtAge" + i.ToString()]);
                    if (OAge == 0)
                    {
                        flag = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                    else
                        flag = true;
                    string str21;
                    str21 = "insert into onlinepassenger (ocustrowid,orderid,name,age,sex) " + " values (" + rowidtemp.ToString() + ",'" + orderid + "'," + "'" + OName + "'," + Request["txtAge" + i.ToString()] + ",'" + Request["Radio" + i.ToString()] + "')";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str21, false);
                }
                if (!(ViewState["rowid"] == null))
                {
                    rowidtemp = Convert.ToInt32(ViewState["rowid"]);
                    String str1 = "insert into onlinecustbyorder (orderid,ocustrowid) " + " values ('" + orderid + "'," + rowidtemp + ")";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str1, false);
                }
                else
                {
                    int newrowidtemp;
                    newrowidtemp = rowidtemp;               
                    String str2 = "insert into onlinecustbyorder (orderid,ocustrowid) " + " values ('" + orderid + "'," + newrowidtemp + ")";
                    DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str2, false);
                }
            }
            Session["orderid"] = Request.QueryString["orderid"];
            alertSeatFull();*/
                #endregion
                return true;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
                if (objGst != null)
                {
                    objGst = null;
                }
            }
        }

        public int SaveOnlinePassenger(string emailID)
        {
            ClsAdo pClsLinq = null;
            try
            {
                pClsLinq = new ClsAdo();
                int Val = 0;
                #region Optimize Code
                /* string str81 = "select rowid from onlinecustomer where email='" + emailid + "'";
            int rowidtemp = Convert.ToInt32(DataLib.GetStringData(DataLib.Connection.ConnectionString, str81));*/
                #endregion
                OName = Request["TxtOName" + i.ToString()];
                paxnum = (ViewState["paxnum"].ToString());
                bool Sex = false;
                string pNameOfPax = "";
                for (i = 1; i <= Convert.ToInt16(paxnum); i++)
                {
                    OName = Request["TxtOName" + i.ToString()];
                    title = Request["contact_title" + i.ToString()];
                    if (OName != "")
                    {
                        OName = title + " " + OName;
                    }
                    int OAge = Convert.ToInt16(Request["txtAge" + i.ToString()]);
                    if (OAge == 0)
                    {
                        flag = false;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                    else
                        flag = true;
                    #region Optimize Code
                    /* string str21;
                str21 = "insert into onlinepassenger (ocustrowid,orderid,name,age,sex) " + " values (" + rowidtemp.ToString() + ",'" + orderid + "'
                * ," + "'" + OName + "'," + Request["txtAge" + i.ToString()] + ",'" + Request["Radio" + i.ToString()] + "')";
                DataLib.ExecuteSQL(DataLib.Connection.ConnectionString, str21, false);*/
                    #endregion
                    pNameOfPax += OName + "-" + Convert.ToInt16(Request["txtAge" + i.ToString()]) + "-" + Convert.ToChar(Request["Radio" + i.ToString()]) + "#";
                    /*Val = pClsLinq.fnInsertOnlinePassengerAgent(emailID, orderid, OName, Convert.ToInt16(Request["txtAge" + i.ToString()]),
                       Convert.ToChar(Request["Radio" + i.ToString()]));*/
                }
                try
                {
                    pClsLinq = new ClsAdo();
                    pNameOfPax = pNameOfPax.TrimEnd('#');
                    Val = pClsLinq.fnInsertOnlinePassengerAgent(emailID, orderid, pNameOfPax, Convert.ToInt16(0), Convert.ToChar('0'));
                }
                finally
                {
                    if (pClsLinq != null)
                    {
                        pClsLinq = null;
                    }
                }
                return Val;
            }
            finally
            {
                if (pClsLinq != null)
                {
                    pClsLinq = null;
                }
            }
        }
        public static bool IsInteger(string theValue)
        {
            try
            {
                Convert.ToInt32(theValue);
                return true;
            }
            catch
            {
                return false;
            }
        } //IsInteger
        void alertSeatFull()
        {

            #region Optimize Code
            /*string ss =Convert.ToString(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select top 1 busserialno from onlinetoursbooking where orderid='" + Session["orderid"].ToString() + "'"));*/
            #endregion
            string ss = "";

            string tourcode = "";
            ClsAdo pclsObj = null;
            DataTable dt1 = null;
            try
            {
                pclsObj = new ClsAdo();
                dt1 = pclsObj.fnGetAlertSeatFull(Convert.ToString(Session["orderid"]));
                //string[] serial = ss.TrimStart(',').Split(',');
                //string[] srtTourcode = tourcode.TrimStart(',').Split(',');
                string lTourCode = "";
                int lTourCtr = 0;
                string lTourID = "";
                for (int lCtr = 0; lCtr < dt1.Rows.Count; lCtr++)
                {
                    int Seat = 0;
                    if (dt1.Rows[lCtr]["busserialno"].ToString().Length > 0)
                    {
                        #region Optimize Code
                        /*string strQry = "select a.tourname,a.busserialno,a.doj,s.*,b.bustype,a.tourid from onlinetoursbooking a,SeatArrangement s,busallot b where orderid='" + Session["orderid"].ToString() + "' and s.tourserial=a.busserialno and a.busserialno=b.rowid";
                DataTable dt1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strQry);


                string strtourid = "select Tour_Short_key from tourmaster where tourno=" + Convert.ToString(dt1.Rows[0]["tourid"]) + "";
                string tourcode = Convert.ToString(DataLib.GetStringData(DataLib.Connection.ConnectionString, strtourid));*/
                        #endregion

                        int BusSeaterType = Convert.ToInt32(dt1.Rows[lCtr]["bustype"].ToString().Substring(0, 2));
                        for (i = 1; i <= BusSeaterType; i++)
                        {
                            if (dt1.Rows[lCtr]["s" + i] == DBNull.Value)
                            {
                                Seat = Seat + 1;
                            }
                        }
                        int remainingseats = BusSeaterType - Seat;
                        decimal fullseats = (remainingseats * 100) / BusSeaterType;

                        if (fullseats >= 65)
                        {
                            string filepath = Server.MapPath("..\\" + ConfigurationManager.AppSettings["travelMail"].ToString() + "\\details.html");
                            System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
                            string strToSend = sr.ReadToEnd();

                            //lTourCode = srtTourcode[lTourCtr].ToString();

                            //if (lTourID == "")
                            //{
                            //    lTourID = dt1.Rows[lCtr]["tourid"].ToString();
                            //}
                            //else
                            //{
                            //    lTourID = dt1.Rows[lCtr - 1]["tourid"].ToString();
                            //}
                            //if (dt1.Rows[lCtr]["tourid"].ToString() != lTourID)
                            //{
                            //    lTourCtr++;
                            //    lTourCode = srtTourcode[lTourCtr].ToString();
                            //}
                            /*if (lTourID != "" && lCtr>0)
                            {
                                lTourID = dt1.Rows[lCtr-1]["tourid"].ToString();
                            }
                            else
                            {
                                lTourCode = srtTourcode[lTourCtr].ToString();

                            }*/
                            strToSend = strToSend.Replace("#firstTourname#", dt1.Rows[lCtr]["Tour_Short_key"].ToString() +
                                ": " + dt1.Rows[lCtr]["tourname"].ToString() + " ( Bus No - " + dt1.Rows[lCtr]["BusNo"].ToString() + " )");
                            strToSend = strToSend.Replace("#Jdate#", dt1.Rows[lCtr]["doj"].ToString().Substring(0, 10));
                            strToSend = strToSend.Replace("#vacsea#", Convert.ToString(remainingseats));
                            try
                            {
                                #region oldCode
                                //MailMessage sendMail = new MailMessage();
                                //sendMail.To = ConfigurationSettings.AppSettings["seatfullalert"].ToString();
                                //sendMail.From = "support@southerntravels.in";
                                //sendMail.Body = strToSend;
                                //sendMail.BodyFormat = MailFormat.Html;
                                //sendMail.Subject = "Bus Seat Full Alert.";
                                //SmtpMail.Send(sendMail);
                                #endregion
                                ClsCommon.sendmail(ConfigurationSettings.AppSettings["seatfullalert"].ToString(), "", "", "support@southerntravels.in", "Bus Seat Full Alert.", strToSend, "");
                            }
                            catch (Exception ex)
                            {
                                Response.Write("<!-- " + ex.ToString() + " -->");
                            }
                            finally
                            {

                            }
                        }

                    }
                }
            }
            finally
            {
                if (pclsObj != null)
                {
                    pclsObj = null;
                }
                if (dt1 != null)
                {
                    dt1.Dispose();
                    dt1 = null;
                }
            }
        }
        //void alertSeatFull()
        //{
        //    int Seat = 0;
        //    #region Optimize Code
        //    /*string ss =Convert.ToString(DataLib.GetStringData(DataLib.Connection.ConnectionString, "select top 1 busserialno from onlinetoursbooking where orderid='" + Session["orderid"].ToString() + "'"));*/
        //    #endregion
        //    string ss = "";
        //    string[] serial = ss.Split(',');
        //    string tourcode = "";
        //    ClsAdo pClsLinq = null;
        //    DataTable dt1 = null;
        //    try
        //    {
        //        pClsLinq = new ClsAdo();
        //        dt1 = new DataTable();
        //        dt1 = pClsLinq.fnGetAlertSeatFull(Convert.ToString(Session["orderid"]), ref ss, ref tourcode);

        //        if (serial.Length == 1)
        //        {
        //            #region Optimize Code
        //            /* string strQry = "select a.tourname,a.busserialno,a.doj,s.*,b.bustype,a.tourid from onlinetoursbooking a,SeatArrangement s,busallot b where orderid='" + Session["orderid"].ToString() + "' and s.tourserial=a.busserialno and a.busserialno=b.rowid";
        //        DataTable dt1 = DataLib.GetDataTable(DataLib.Connection.ConnectionString, strQry);
        //        int BusSeaterType = Convert.ToInt32(dt1.Rows[0]["bustype"].ToString().Substring(0, 2));

        //        string strtourid = "select Tour_Short_key from tourmaster where tourno=" + Convert.ToString(dt1.Rows[0]["tourid"]) + "";
        //        string tourcode = Convert.ToString(DataLib.GetStringData(DataLib.Connection.ConnectionString, strtourid));*/
        //            #endregion
        //            //BusSeaterType = Convert.ToInt32(ViewState["BusSeaterType"]);
        //            int BusSeaterType = Convert.ToInt32(dt1.Rows[0]["bustype"].ToString().Substring(0, 2));
        //            for (i = 1; i <= BusSeaterType; i++)
        //            {
        //                if (dt1.Rows[0]["s" + i] == DBNull.Value)
        //                {
        //                    Seat = Seat + 1;
        //                }
        //            }
        //            try
        //            {
        //                int remainingseats = BusSeaterType - Seat;

        //                decimal fullseats = (remainingseats * 100) / BusSeaterType;

        //                if (fullseats >= 65)
        //                {

        //                    string filepath = Server.MapPath("..\\" + ConfigurationManager.AppSettings["travelMail"].ToString() + "\\details.html");
        //                    System.IO.StreamReader sr = new System.IO.StreamReader(filepath);
        //                    string strToSend = sr.ReadToEnd();
        //                    strToSend = strToSend.Replace("#firstTourname#", tourcode + ": " + dt1.Rows[0]["tourname"].ToString());
        //                    strToSend = strToSend.Replace("#Jdate#", dt1.Rows[0]["doj"].ToString().Substring(0, 10));
        //                    strToSend = strToSend.Replace("#vacsea#", Convert.ToString(remainingseats));

        //                    try
        //                    {
        //                        MailMessage sendMail = new MailMessage();
        //                        sendMail.To = ConfigurationSettings.AppSettings["seatfullalert"].ToString();
        //                        sendMail.From = "support@southerntravels.in";
        //                        sendMail.Body = strToSend;
        //                        sendMail.BodyFormat = MailFormat.Html;
        //                        sendMail.Subject = "Bus Seat Full Alert.";
        //                        SmtpMail.Send(sendMail);
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Response.Write("<!-- " + ex.ToString() + " -->");
        //                    }
        //                    finally
        //                    {
        //                    }
        //                }
        //            }
        //            catch { }
        //        }
        //    }
        //    finally
        //    {
        //        if (pClsLinq != null)
        //        {
        //            pClsLinq = null;
        //        }
        //        if (dt1 != null)
        //        {
        //            dt1.Dispose();
        //            dt1 = null;
        //        }
        //    }
        //}
        public static void TieButton(Control TextBoxToTie, Control ButtonToTie)
        {
            string formName;
            try
            {
                int i = 0;
                Control c = ButtonToTie.Parent;
                while (!(c is System.Web.UI.HtmlControls.HtmlForm) & !(c is System.Web.UI.Page) && i < 500)
                {
                    c = c.Parent;
                    i++;
                }
                if (c is System.Web.UI.HtmlControls.HtmlForm)
                    formName = c.ClientID;
                else
                    formName = "forms[0]";
            }
            catch
            {
                //If we catch an exception, we should use the first form on the page ("forms[0]").
                formName = "forms[0]";
            }
            TieButton(TextBoxToTie, ButtonToTie, formName);
        }
        public static void TieButton(Control TextBoxToTie, Control ButtonToTie, string formName)
        {
            // This is our javascript - we fire the client-side click event of the button if the enter key is pressed.
            string jsString = "if ((event.which && event.which == 13) || (event.keyCode && event.keyCode == 13)) {document." + formName + ".all['" + ButtonToTie.ClientID + "'].click();return false;} else return true; ";
            // We attach this to the onkeydown attribute - we have to cater for HtmlControl or WebControl.
            if (TextBoxToTie is System.Web.UI.HtmlControls.HtmlControl)
                ((System.Web.UI.HtmlControls.HtmlControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
            else if (TextBoxToTie is System.Web.UI.WebControls.WebControl)
                ((System.Web.UI.WebControls.WebControl)TextBoxToTie).Attributes.Add("onkeydown", jsString);
            else
            {
                // We throw an exception if TextBoxToTie is not of type HtmlControl or WebControl.
                throw new ArgumentException("Control TextBoxToTie should be derived from either System.Web.UI.HtmlControls.HtmlControl or System.Web.UI.WebControls.WebControl", "TextBoxToTie");
            }
        }
        private string GetStateID(string lStateName)
        {
            DataListResponse<GetCountryWiseStateName_SPResult> lGetStateName = null;
            clsContractModule clsHCobj = new clsContractModule();
            string pStateID = "0";
            try
            {

                lGetStateName = new DataListResponse<GetCountryWiseStateName_SPResult>();
                int lCountryID = -2;
                if (ClsCommon.ConvertStringint(ddlCountry.SelectedValue) > 0)
                    lCountryID = ClsCommon.ConvertStringint(ddlCountry.SelectedValue);
                clsHCobj.fldConnString = DataLib.getConnectionString();
                lGetStateName = clsHCobj.fnGetCountryWiseStateName(lCountryID);
                if (lGetStateName.Status.Status)
                {
                    var result = lGetStateName.ResultList.Where(q => (q.StateName)
                       .ToLower()
                       .Contains(lStateName.ToLower()))
                       .ToList();
                    if (result != null && result.Count > 0)
                    {
                        pStateID = result[0].StateID.ToString();
                    }
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
            return pStateID;
        }
        private void BindCountryName()
        {
            DataListResponse<GetCountryName_SPResult> lGetCountryName = null;
            clsContractModule clsHCobj = new clsContractModule();
            ddlNationality.Items.Clear();
            ddlCountry.Items.Clear();
            try
            {
                lGetCountryName = new DataListResponse<GetCountryName_SPResult>();
                clsHCobj.fldConnString = DataLib.getConnectionString()  ;
                lGetCountryName = clsHCobj.fnGetCountryName(0);
                if (lGetCountryName.Status.Status)
                {
                    ddlNationality.DataSource = lGetCountryName.ResultList;
                    ddlNationality.DataTextField = "CountryName";
                    ddlNationality.DataValueField = "CountryID";
                    ddlNationality.DataBind();
                    ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));
                    ddlNationality.SelectedValue = "1";

                    ddlCountry.DataSource = lGetCountryName.ResultList;
                    ddlCountry.DataTextField = "CountryName";
                    ddlCountry.DataValueField = "CountryID";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("Country", "0"));
                    ddlCountry.SelectedValue = "1";
                }
            }
            finally
            {
                if (clsHCobj != null)
                {
                    clsHCobj = null;
                }
                if (lGetCountryName != null)
                {
                    lGetCountryName = null;
                }
            }
        }
        private void BindNationality(int lregionID)
        {
            DataTable ldtcountry = null;
            ClsAdo clsObj = new ClsAdo();
            try
            {
                ddlNationality.Items.Clear();
                ldtcountry = clsObj.fnGetCountry(lregionID);
                if (ldtcountry != null && ldtcountry.Rows.Count > 0)
                {
                    ReArrangeCountry(ref ldtcountry);
                    ddlNationality.DataSource = ldtcountry;
                    ddlNationality.DataTextField = "country_name";
                    ddlNationality.DataValueField = "country_Id";
                    ddlNationality.DataBind();
                    ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
                    ddlNationality.Items.Clear();
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

        private void BindCountry(int lregionID)
        {
            DataTable ldtcountry = null;
            ClsAdo clsObj = new ClsAdo();
            try
            {

                ddlCountry.Items.Clear();
                ldtcountry = clsObj.fnGetCountry(lregionID);
                if (ldtcountry != null && ldtcountry.Rows.Count > 0)
                {
                    ReArrangeCountry(ref ldtcountry);
                    ddlCountry.DataSource = ldtcountry;
                    ddlCountry.DataTextField = "country_name";
                    ddlCountry.DataValueField = "country_Id";
                    ddlCountry.DataBind();
                    ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));
                }
                else
                {
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

        private void BindStateName()
        {
            DataListResponse<GetCountryWiseStateName_SPResult> lGetStateName = null;
            clsContractModule clsHCobj = new clsContractModule();
            ddlState.Items.Clear();
            try
            {

                lGetStateName = new DataListResponse<GetCountryWiseStateName_SPResult>();
                int lCountryID = -2;
                if (ClsCommon.ConvertStringint(ddlCountry.SelectedValue) > 0)
                    lCountryID = ClsCommon.ConvertStringint(ddlCountry.SelectedValue);
                clsHCobj.fldConnString = DataLib.getConnectionString() ;
                lGetStateName = clsHCobj.fnGetCountryWiseStateName(lCountryID);
                if (lGetStateName.Status.Status)
                {
                    ddlState.DataSource = lGetStateName.ResultList;
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateID";
                    ddlState.DataBind();
                    ddlState.Items.Insert(0, new ListItem("State", "0"));
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

        public void BindStateNameNew()
        {
            DataSet dscars = null;
            try
            {
                //if (Convert.ToInt32(ddlNationality.SelectedValue) > 0)
                //{
                string GetServiceChargeDetails = "GST_GetStateByCountryId_SP";
                SqlParameter[] param = new SqlParameter[1];
                param[0] = new SqlParameter("@i_CountryID", "1"); //to get the all service list
                dscars = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, GetServiceChargeDetails, param);

                if (dscars.Tables[0] != null && dscars.Tables[0].Rows.Count > 0)
                {
                    ddlState.DataTextField = "StateName";
                    ddlState.DataValueField = "StateID";

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
                //}
            }
            catch (Exception ex)
            {

            }
            finally
            {
                if (dscars != null)
                {
                    dscars = null;
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
}