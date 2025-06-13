//using SouthernTravelIndiaAgent.BAL;
//using SouthernTravelIndiaAgent.DAL;
//using System;
//using System.Collections.Generic;
//using System.Data;
//using System.Data.SqlClient;
//using System.Linq;
//using System.Web;
//using System.Web.UI;
//using System.Web.UI.WebControls;

//namespace SouthernTravelIndiaAgent.UserControls
//{
//    public delegate void btnSearch_Click(object sender, ImageClickEventArgs e);
//    public partial class UcGST_ucManageCustomer : System.Web.UI.UserControl
//    {
//        #region "Member Variable(s)"
//        GroupLeader pvGroupLeader = null;
//        bool pvIsPassportRequired = false, pvIsClick = false, pvIsDelegateRequired = false, pvIsPanRequired = false, pvIsZeroAdvance = false;
//        bool pvIsOccupationShow = false;
//        private bool pvIsEndUser, pvIsApproval = false;
//        #endregion
//        #region "Properties"
//        public bool fldIsPassportRequired
//        {
//            get { return pvIsPassportRequired; }
//            set
//            {
//                trPassport.Visible = value;
//                pvIsPassportRequired = value;
//            }
//        }

//        public bool fldIsPanRequired
//        {
//            get { return pvIsPanRequired; }
//            set
//            {
//                trPanDetails.Visible = value;
//                //divform60.Visible = value;
//                pvIsPanRequired = value;
//            }
//        }
//        public bool fldIsZeroAdvance
//        {
//            get { return pvIsZeroAdvance; }
//            set
//            {
//                trZeroAdvance.Visible = value;
//                trZeroAdvance1.Visible = value;
//                trZeroAdvance2.Visible = value;
//                trZeroAdvance3.Visible = value;
//                pvIsZeroAdvance = value;
//            }
//        }

//        // public GST_GroupLeader 

//        public GroupLeader fldGroupLeader
//        {
//            get
//            {
//                if (Validated())
//                {
//                    if (pvGroupLeader == null)
//                        pvGroupLeader = new GroupLeader();
//                    pvGroupLeader.fldTitle = ddlTitle.SelectedValue;
//                    pvGroupLeader.fldName = txtName.Value;
//                    pvGroupLeader.fldAddress = txtAddress.Text;
//                    pvGroupLeader.fldSTDCode = txtPhoneCountryCode.Value;
//                    pvGroupLeader.fldPhoneNo = txtPhone.Value;
//                    pvGroupLeader.fldEmailID = txtMail.Value;
//                    pvGroupLeader.fldMobileNo = txtMobile.Value;
//                    pvGroupLeader.fldEmergencyNo = txtAlternateMobileNo.Value;
//                    pvGroupLeader.fldCanSendPromotions = chkPromotions.Checked;
//                    pvGroupLeader.fldNationality = Convert.ToInt32(ddlNationality.SelectedValue);
//                    pvGroupLeader.fldNationalityName = ddlNationality.SelectedItem.Text;

//                    pvGroupLeader.fldCountryID = Convert.ToInt32(ddlCountry.SelectedValue);
//                    pvGroupLeader.fldCountryName = ddlCountry.SelectedItem.Text;

//                    if (ddlCountry.SelectedValue == "59")
//                    {
//                        pvGroupLeader.fldState = ddlState.SelectedValue;
//                    }
//                    else
//                    {
//                        pvGroupLeader.fldState = TxtForeignState.Text;
//                    }

//                    if (ddlCountry.SelectedValue == "59")
//                    {
//                        pvGroupLeader.fldCity = txtCity.Text.Trim();
//                    }
//                    else
//                    {
//                        pvGroupLeader.fldCity = txtForeignCity.Text.Trim();
//                    }

//                    pvGroupLeader.fldPanNo = txtPanNo.Text;
//                    pvGroupLeader.fldPanImage = "";
//                    pvGroupLeader.fldOccupationId = Convert.ToInt32(ddlOccupation.SelectedValue);
//                    pvGroupLeader.fldType = rbtnType.SelectedValue;
//                    pvGroupLeader.fldCompanyName = txtCompanyName.Text;
//                    pvGroupLeader.fldAllowCreditBooking = Convert.ToInt32(rbtnCreditBooking.SelectedValue == "" ? "0" : rbtnCreditBooking.SelectedValue);
//                    pvGroupLeader.fldCreditPeriod = txtCreditPeriod.Text == "" ? 0 : Convert.ToInt32(txtCreditPeriod.Text);
//                    pvGroupLeader.fldIsActive = chkIsActive.Checked;
//                    pvGroupLeader.fldReferredBy = Convert.ToInt32(ddlReferred.SelectedValue);
//                    pvGroupLeader.fldRemarks = txtRemarks.Text;
//                    pvGroupLeader.fldAdvanceAmountPer = txtAdvanceAmount.Text == "" ? 0 : Convert.ToDecimal(txtAdvanceAmount.Text);
//                    if (flImage.HasFile)
//                    {
//                        pvGroupLeader.fldPanImage = flImage.FileName;
//                    }

//                    //**************Aadhar No Add********************
//                    pvGroupLeader.fldAadharNo = txtAadharNo.Text;
//                    pvGroupLeader.fldAadharImage = "";
//                    if (fupAadhar.HasFile)
//                    {
//                        pvGroupLeader.fldAadharImage = fupAadhar.FileName;
//                    }
//                    //**************************************************

//                    //pvGroupLeader.fldStateID = Convert.ToInt32(ddlState.SelectedValue);

//                    pvGroupLeader.fldPinCode = txtPinCode.Text.Trim();
//                    if (fldIsPassportRequired)
//                    {
//                        pvGroupLeader.fldPassportNo = txtPassportNo.Value;
//                        string[] lValidity = txtPassportValidity.Text.Trim().Split('/');
//                        pvGroupLeader.fldPassValidUpto = new DateTime(Convert.ToInt16(lValidity[2]),
//                            Convert.ToInt16(lValidity[1]), Convert.ToInt16(lValidity[0]));
//                        //pvGroupLeader.fldPassValidUpto = new DateTime(1900, 1, 1);
//                    }
//                    else
//                    {
//                        pvGroupLeader.fldPassportNo = "";
//                        pvGroupLeader.fldPassValidUpto = new DateTime(1900, 1, 1);
//                    }
//                    if (hdnRowId.Value != "")
//                        pvGroupLeader.fldCustomerID = Convert.ToInt32(hdnRowId.Value);

//                    pvGroupLeader.IsGSTIN = rdbIsGSTApplicableYes.Checked == true ? true : false;
//                    pvGroupLeader.GSTIN = txtCustomerGSTIN.Text.Trim() == "" ? "" : txtCustomerGSTIN.Text.Trim();
//                    pvGroupLeader.GSTHolderName = txtGstHolderName.Text.Trim() == "" ? "" : txtGstHolderName.Text.Trim();

//                }
//                else
//                {
//                    pvGroupLeader = null;
//                }
//                return pvGroupLeader;
//            }
//        }

//        public bool fldIsEndUser
//        {
//            get
//            {
//                return pvIsEndUser;
//            }
//            set
//            {
//                pvIsEndUser = value;
//                if (value)
//                {
//                    trPassport.BgColor = "#C7EDFF";
//                }
//                else
//                {
//                    trPassport.BgColor = "white";
//                }
//            }
//        }

//        public bool fldIsClick
//        {
//            get { return pvIsClick; }
//            set { pvIsClick = value; }
//        }

//        public bool fldIsDelegateRequired
//        {
//            get { return pvIsDelegateRequired; }
//            set { pvIsDelegateRequired = value; }
//        }

//        public bool fldIsRequierdOccupation
//        {
//            get { return pvIsOccupationShow; }
//            set { pvIsOccupationShow = value; }
//        }

//        public bool fldIsApproval
//        {
//            get { return pvIsApproval; }
//            set { pvIsApproval = value; }
//        }

//        public bool fldIsEmailClick
//        {
//            get;
//            set;
//        }

//        public string fldIsEmailOrMobileNo
//        {
//            get;
//            set;
//        }

//        #endregion
//        #region Public Event
//        public event btnSearch_Click btnSearchClick;
//        #endregion
//        #region Vitual Methods
//        protected virtual void OnbtnDelQtnMrClk(object sender, ImageClickEventArgs e)
//        {
//            // Call btnPost_Click event delegate instance
//            btnSearchClick(sender, e);
//        }

//        #endregion
//        #region "Event(s)"
//        protected void Page_Load(object sender, EventArgs e)
//        {
//            pvGroupLeader = new GroupLeader();
//            btnSearch.Attributes.Add("onclick", "javascript:return chk1();");
//            txtPassportValidity.Style.Add("readonly", "readonly");
//            if (!IsPostBack)
//            {
//                fnBindcountry(-1);
//                fnNationality(-1);
//                BindOccupation();
//                BindState();
//                if (fldIsZeroAdvance)
//                {

//                }
//                ApprovalHierarchyUserList();
//            }
//            if (fldIsClick)
//            {
//                Button1_Click(sender, e);
//            }

//            if (fldIsRequierdOccupation)
//            {
//                trOccupation.Visible = true;
//            }

//            //if (!string.IsNullOrEmpty(emailid.Text.Trim()) && !string.IsNullOrEmpty(Convert.ToString(Session["fldPaxNo"])))
//            //{
//            //    btnSearch_Click(null, new ImageClickEventArgs(0, 0));

//            //}

//        }
//        /// <summary>
//        /// Fake Button for click page side event data fill.
//        /// </summary>
//        /// <param name="sender"></param>
//        /// <param name="e"></param>
//        public void Button1_Click(object sender, EventArgs e)
//        {
//            string emailidt = emailid.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
//            SearchData(emailidt);
//        }
//        protected void CheckSubmit_Click(object sender, EventArgs e)
//        {
//            DataTable dtOnlineCust = null;
//            string emailidt = emailid.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
//            ClsAdo pclsObj = null;
//            try
//            {
//                pclsObj = new ClsAdo();
//                if (emailid.Text.Trim() != "")
//                {
//                    dtOnlineCust = pclsObj.fnGetCustomerDetail(emailidt);
//                    if (dtOnlineCust != null && dtOnlineCust.Rows.Count > 0)
//                    {
//                        existingcustomer(Convert.ToInt32(dtOnlineCust.Rows[0]["RowId"]));
//                    }
//                    else
//                    {
//                        chkPromotions.Checked = true;
//                        txtName.Attributes.Remove("readonly");
//                        txtMobile.Attributes.Remove("readonly");
//                        txtMail.Attributes.Remove("readonly");
//                        txtName.Value = "";
//                        txtAddress.Text = "";
//                        ddlState.SelectedValue = "";
//                        TxtForeignState.Text = "";
//                        if (type.Value == "email")
//                        {
//                            txtMobile.Value = "";
//                            txtMail.Value = emailid.Text;
//                        }
//                        else if (type.Value == "Mobile")
//                        {
//                            txtMobile.Value = emailid.Text;
//                            txtMail.Value = "";
//                        }
//                        txtPhone.Value = "";
//                        txtPhoneCountryCode.Value = "";
//                        txtAlternateMobileNo.Value = "";
//                    }
//                }
//            }
//            catch (Exception Ex)
//            {
//            }
//            finally
//            {
//                if (pclsObj != null)
//                {
//                    pclsObj = null;
//                }
//                if (dtOnlineCust != null)
//                {
//                    dtOnlineCust.Dispose();
//                    dtOnlineCust = null;
//                }
//            }
//        }
//        public void btnSearch_Click(object sender, ImageClickEventArgs e)
//        {
//            string emailidt = emailid.Text.Trim().Replace("'", "").Replace("<", "").Replace(">", "").Replace("alert", "");
//            SearchData(emailidt);
//            //Button1_Click(sender, e);
//            //Call Virtual Method
//            //BindState();
//            if (fldIsDelegateRequired)
//                OnbtnDelQtnMrClk(sender, e);

//            fldIsEmailClick = true;

//        }
//        #endregion
//        #region "Method(s)"
//        public void SearchData(string email)
//        {
//            pnlPerDetail.Visible = true;
//            hdValues.Value = "1";
//            DataTable dtOnlineCust = null;
//            string emailidt = email;
//            emailid.Text = email;
//            // clsLinqtoDB pclsObj = null;
//            GST_Data objGST = new GST_Data();
//            try
//            {
//                //  pclsObj = new clsLinqtoDB();
//                if (emailid.Text.Trim() != "")
//                {
//                    // dtOnlineCust = pclsObj.fnGetCustomerDetail(emailidt);
//                    dtOnlineCust = objGST.GST_fnGetCustomerDetail(emailidt, 0);

//                    if (dtOnlineCust != null && dtOnlineCust.Rows.Count > 0)
//                    {
//                        existingcustomer(Convert.ToInt32(dtOnlineCust.Rows[0]["RowId"]));

//                    }
//                    else
//                    {
//                        chkPromotions.Checked = true;
//                        txtName.Attributes.Remove("readonly");
//                        txtMobile.Attributes.Remove("readonly");
//                        txtMail.Attributes.Remove("readonly");
//                        txtName.Value = "";
//                        txtAddress.Text = "";
//                        ddlState.SelectedValue = "0";
//                        TxtForeignState.Text = "";
//                        if (type.Value == "email")
//                        {
//                            txtMobile.Value = "";
//                            txtMail.Value = emailid.Text;
//                        }
//                        else if (type.Value == "Mobile")
//                        {
//                            txtMobile.Value = emailid.Text;
//                            txtMail.Value = "";
//                        }
//                        txtPhone.Value = "";
//                        txtPhoneCountryCode.Value = "";
//                        txtAlternateMobileNo.Value = "";

//                        rbtnType.SelectedIndex = -1;
//                        txtCompanyName.Text = "";
//                        rbtnCreditBooking.SelectedIndex = -1;
//                        txtCreditPeriod.Text = "";
//                        chkIsActive.Checked = true;
//                        txtAdvanceAmount.Text = "";
//                    }
//                }


//            }
//            catch (Exception Ex)
//            {
//            }
//            finally
//            {
//                if (objGST != null)
//                {
//                    objGST = null;
//                }
//                if (dtOnlineCust != null)
//                {
//                    dtOnlineCust.Dispose();
//                    dtOnlineCust = null;
//                }
//            }
//        }
//        public void existingcustomer(int rowid)
//        {
//            ClsAdo pclsObj = null;
//            DataTable dtX = null;
//            try
//            {
//                pclsObj = new ClsAdo();
//                //  dtX = pclsObj.fnExistCustomerDetail("", rowid);
//                dtX = fnExistCustomerDetail("", rowid);
//                if (dtX != null && dtX.Rows.Count > 0)
//                {
//                    hdnRowId.Value = Convert.ToString(rowid);
//                    bool pFlag = AllowCreditBookingCHK(Convert.ToInt32(rowid));
//                    //txtName.Attributes.Add("readonly", "readonly");
//                    //txtMobile.Attributes.Add("readonly", "readonly");
//                    //txtMail.Attributes.Add("readonly", "readonly");
//                    txtName.Value = dtX.Rows[0]["FirstName"].ToString();
//                    txtAddress.Text = dtX.Rows[0]["Addr1"].ToString();

//                    if (Convert.ToString(dtX.Rows[0]["Country"]) == "59" || Convert.ToString(dtX.Rows[0]["Country"]) == "India")
//                    {
//                        txtCity.Text = dtX.Rows[0]["City"].ToString();
//                        txtCity.Style.Clear();
//                        txtCity.Style.Add("display", "block");
//                        txtForeignCity.Style.Clear();
//                        txtForeignCity.Style.Add("display", "none");
//                    }
//                    else
//                    {
//                        txtForeignCity.Text = dtX.Rows[0]["City"].ToString();
//                        txtForeignCity.Style.Clear();
//                        txtForeignCity.Style.Add("display", "block");
//                        txtCity.Style.Clear();
//                        txtCity.Style.Add("display", "none");
//                    }
//                    txtPinCode.Text = dtX.Rows[0]["ZipCode"].ToString();
//                    txtPanNo.Text = Convert.ToString(dtX.Rows[0]["PanNo"]);

//                    txtAadharNo.Text = Convert.ToString(dtX.Rows[0]["Aadharno"]);

//                    if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["Title"])))
//                    {
//                        ddlTitle.SelectedValue = Convert.ToString(dtX.Rows[0]["Title"]);
//                    }
//                    else
//                    {
//                        ddlTitle.SelectedValue = "0";
//                    }

//                    if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["Nationality"])))
//                    {
//                        try
//                        {
//                            fnNationality(-1);
//                            if (Convert.ToString(dtX.Rows[0]["Nationality"]).Length <= 2)
//                            {
//                                ddlNationality.SelectedValue = Convert.ToString(dtX.Rows[0]["Nationality"]);
//                            }
//                            else
//                            {
//                                ddlNationality.Items.FindByText(Convert.ToString(dtX.Rows[0]["Nationality"])).Selected = true;
//                            }
//                        }
//                        catch { }
//                    }
//                    else
//                    {
//                        ddlNationality.SelectedValue = "0";
//                    }

//                    if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["Country"])))
//                    {
//                        try
//                        {
//                            fnBindcountry(-1);
//                            if (Convert.ToString(dtX.Rows[0]["Country"]).Length <= 2)
//                            {
//                                ddlCountry.Items.FindByValue(Convert.ToString(dtX.Rows[0]["Country"])).Selected = true;
//                                //ddlCountry.SelectedValue = Convert.ToString(dtX.Rows[0]["Country"]);
//                            }
//                            else
//                            {
//                                ddlCountry.Items.FindByText(Convert.ToString(dtX.Rows[0]["Country"])).Selected = true;
//                            }
//                        }
//                        catch { }
//                    }
//                    else
//                    {
//                        ddlCountry.SelectedValue = "0";
//                    }

//                    if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["PassportNo"])))
//                    {
//                        txtPassportNo.Value = Convert.ToString(dtX.Rows[0]["PassportNo"]);
//                    }
//                    else
//                    {
//                        txtPassportNo.Value = "";
//                    }
//                    if (!string.IsNullOrEmpty(Convert.ToString(dtX.Rows[0]["PassportValidUpto"])))
//                    {
//                        DateTime lValid = Convert.ToDateTime(dtX.Rows[0]["PassportValidUpto"]);
//                        txtPassportValidity.Text = lValid.Day.ToString() + "/" + lValid.Month.ToString() + "/" +
//                            lValid.Year.ToString();
//                    }
//                    else
//                    {
//                        txtPassportValidity.Text = "";
//                    }

//                    string s;
//                    s = dtX.Rows[0]["state"].ToString();
//                    if (s.Length > 0)
//                    {
//                        try
//                        {
//                            if (Convert.ToString(dtX.Rows[0]["Country"]) == "59" || Convert.ToString(dtX.Rows[0]["Country"]) == "India")
//                            {
//                                ddlState.SelectedValue = char.ToUpper(Convert.ToChar(s.Substring(0, 1))).ToString() + s.Substring(1, s.Length - 1);

//                                try
//                                {
//                                    int Stateid = Convert.ToInt32(s);
//                                    ddlState.SelectedValue = Convert.ToString(Stateid);
//                                }
//                                catch
//                                {
//                                    ddlState.Items.FindByText(s).Selected = true;
//                                }
//                                //ddlState.Style.Clear();
//                                //ddlState.Style.Add("display", "block");
//                                TxtForeignState.Style.Clear();
//                                TxtForeignState.Style.Add("display", "none");

//                            }
//                            else if (ddlCountry.SelectedValue != "0")
//                            {
//                                TxtForeignState.Text = s;
//                                TxtForeignState.Style.Clear();
//                                TxtForeignState.Style.Add("display", "block");
//                                ddlState.Style.Clear();
//                                ddlState.Style.Add("display", "none");
//                            }
//                            else
//                            {
//                                TxtForeignState.Text = "";
//                                TxtForeignState.Style.Clear();
//                                TxtForeignState.Style.Add("display", "block");
//                                ddlState.Style.Clear();
//                                ddlState.Style.Add("display", "none");
//                            }
//                        }
//                        catch { }
//                    }
//                    txtMobile.Value = dtX.Rows[0]["Mobile"].ToString();
//                    txtAlternateMobileNo.Value = dtX.Rows[0]["AlternativeNo"].ToString();
//                    txtMail.Value = dtX.Rows[0]["email"].ToString();
//                    Session["EmailId"] = txtMail.Value;
//                    //txtPhone.Value = dtX.Rows[0]["PhoneNo"].ToString();
//                    if (dtX.Rows[0]["PhoneNo"].ToString().IndexOf("-") != -1)
//                    {
//                        string[] tempPhone = dtX.Rows[0]["PhoneNo"].ToString().Split('-');
//                        txtPhoneCountryCode.Value = tempPhone[0];
//                        txtPhone.Value = tempPhone[1];
//                    }
//                    else
//                    {
//                        txtPhone.Value = dtX.Rows[0]["PhoneNo"].ToString();
//                    }
//                    chkPromotions.Checked = Convert.ToBoolean(dtX.Rows[0]["CanSendPromotions"] != DBNull.Value ?
//                       Convert.ToInt16(dtX.Rows[0]["CanSendPromotions"]) : 1);
//                    if (fldIsPassportRequired)
//                    {
//                        txtMail.Attributes.Remove("readonly");
//                    }

//                    try
//                    {
//                        if (Convert.ToString(dtX.Rows[0]["RefOccupationId"]) == "-1")
//                        {
//                            txtOccupation.Value = Convert.ToString(dtX.Rows[0]["OccupationName"]);
//                            ddlOccupation.SelectedValue = Convert.ToString(dtX.Rows[0]["RefOccupationId"]);
//                        }
//                        else
//                        {
//                            ddlOccupation.SelectedValue = Convert.ToString(dtX.Rows[0]["RefOccupationId"]);
//                        }
//                    }
//                    catch (Exception ex)
//                    {

//                    }
//                    try
//                    {
//                        rbtnType.SelectedIndex = -1;
//                        rbtnType.SelectedValue = "P";
//                        if (Convert.ToString(dtX.Rows[0]["CustomerType"]) != "")
//                        {
//                            rbtnType.SelectedValue = Convert.ToString(dtX.Rows[0]["CustomerType"]);
//                        }
//                        rbtnCreditBooking.SelectedIndex = -1;
//                        if (Convert.ToString(dtX.Rows[0]["AllowCreditBooking"]) != "")
//                        {
//                            if (Convert.ToString(dtX.Rows[0]["AllowCreditBooking"]) == "false")
//                            {
//                                rbtnCreditBooking.SelectedValue = "0";
//                            }
//                            else if (Convert.ToString(dtX.Rows[0]["AllowCreditBooking"]) == "True")
//                            {
//                                rbtnCreditBooking.SelectedValue = "1";
//                            }
//                        }
//                        txtCreditPeriod.Text = Convert.ToString(dtX.Rows[0]["CreditPeriod"]);
//                        txtCompanyName.Text = Convert.ToString(dtX.Rows[0]["company"]);
//                        txtAdvanceAmount.Text = Convert.ToString(dtX.Rows[0]["AdvanceAmountPer"]); ;
//                        if (Convert.ToString(dtX.Rows[0]["Status"]).ToLower() == "a")
//                        {
//                            chkIsActive.Checked = true;
//                        }
//                        if (Convert.ToString(dtX.Rows[0]["CustomerType"]) == "P")
//                        {
//                            //rbtnType.SelectedIndex = -1;
//                            if (fldIsApproval || !pFlag)
//                            {
//                                txtAdvanceAmount.Text = "";
//                                txtCompanyName.Text = "";
//                                txtCreditPeriod.Text = "";
//                                rbtnCreditBooking.SelectedIndex = -1;
//                                ddlReferred.SelectedIndex = -1;
//                                txtRemarks.Text = "";
//                            }
//                        }
//                    }
//                    catch (Exception ex)
//                    {

//                    }

//                    #region "Bind GST"
//                    bool isGstIn = dtX.Rows[0]["ISGSITIN"] != DBNull.Value ? Convert.ToBoolean(dtX.Rows[0]["ISGSITIN"]) : false;
//                    if (isGstIn)
//                    {
//                        rdbIsGSTApplicableYes.Checked = true;
//                        rdbIsGSTApplicableNo.Checked = false;
//                        trGSTDetails.Style["display"] = "";
//                        txtCustomerGSTIN.Text = dtX.Rows[0]["CustomerGSTIN"] != DBNull.Value ? Convert.ToString(dtX.Rows[0]["CustomerGSTIN"]) : "";
//                        txtGstHolderName.Text = dtX.Rows[0]["GstHolderName"] != DBNull.Value ? Convert.ToString(dtX.Rows[0]["GstHolderName"]) : "";
//                    }
//                    else
//                    {
//                        rdbIsGSTApplicableYes.Checked = false;
//                        rdbIsGSTApplicableNo.Checked = true;
//                        trGSTDetails.Style["display"] = "none";
//                        txtCustomerGSTIN.Text = "";
//                        txtGstHolderName.Text = "";
//                    }
//                    #endregion
//                    //rbtnType.SelectedIndex = -1;
//                    // txtCompanyName.Text = "";
//                    //rbtnCreditBooking.SelectedIndex = -1;
//                    // txtCreditPeriod.Text = "";
//                    //chkIsActive.Checked = true;

//                    //txtPanNo.Text = Convert.ToString(dtX.Rows[0]["PanNo"]);
//                }
//                else
//                {
//                    txtName.Attributes.Remove("readonly");
//                    txtMobile.Attributes.Remove("readonly");
//                    txtMail.Attributes.Remove("readonly");

//                    txtName.Value = "";
//                    txtAddress.Text = "";
//                    ddlState.SelectedValue = "";
//                    TxtForeignState.Text = "";
//                    if (type.Value == "email")
//                    {
//                        txtMobile.Value = "";
//                        txtMail.Value = emailid.Text;
//                    }
//                    else if (type.Value == "Mobile")
//                    {
//                        txtMobile.Value = emailid.Text;
//                        txtMail.Value = "";
//                    }
//                    txtPhone.Value = "";
//                    txtPhoneCountryCode.Value = "";
//                    chkPromotions.Checked = true;
//                    txtAlternateMobileNo.Value = "";

//                    rbtnType.SelectedIndex = -1;
//                    txtCompanyName.Text = "";
//                    rbtnCreditBooking.SelectedIndex = -1;
//                    txtCreditPeriod.Text = "";
//                    chkIsActive.Checked = true;
//                    txtAdvanceAmount.Text = "";
//                }
//            }
//            finally
//            {
//                if (pclsObj != null)
//                {
//                    pclsObj = null;
//                }
//                if (dtX != null)
//                {
//                    dtX.Dispose();
//                    dtX = null;
//                }
//            }
//        }
//        private bool Validated()
//        {
//            if (ddlTitle.SelectedValue.Trim().ToLower() == "title")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please select name title.');", true);
//                ddlTitle.Focus();
//                return false;
//            }
//            if (txtName.Value.Trim() == "")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter Full Name.');", true);
//                txtName.Focus();
//                return false;
//            }
//            if (ddlNationality.SelectedValue == "0")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please select Nationality.');", true);
//                ddlNationality.Focus();
//                return false;
//            }

//            if (ddlCountry.SelectedValue == "0")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please select Country.');", true);
//                ddlNationality.Focus();
//                return false;
//            }

//            if (ddlState.SelectedValue == "0" && ddlCountry.SelectedValue == "59")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please select State.');", true);
//                ddlState.Focus();
//                return false;
//            }

//            if (TxtForeignState.Text == "" && ddlCountry.SelectedValue != "59")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please Enter State.');", true);
//                TxtForeignState.Focus();
//                return false;
//            }

//            if (txtAddress.Text.Trim() == "")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter Address.');", true);
//                txtAddress.Focus();
//                return false;
//            }

//            if (txtCity.Text.Trim() == "" && ddlCountry.SelectedValue == "59")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter City.');", true);
//                txtCity.Focus();
//                return false;
//            }
//            if (txtForeignCity.Text.Trim() == "" && ddlCountry.SelectedValue != "59")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter City.');", true);
//                txtForeignCity.Focus();
//                return false;
//            }
//            if (txtPinCode.Text.Trim() == "")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter Pincode.');", true);
//                txtPinCode.Focus();
//                return false;
//            }

//            //if (txtPhoneCountryCode.Value.Trim() == "")
//            //{
//            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter country code.');", true);
//            //    txtPhoneCountryCode.Focus();
//            //    return false;
//            //}
//            if (txtMobile.Value.Trim() == "")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please provide mobile number.');", true);
//                txtMobile.Focus();
//                return false;
//            }
//            if (txtAlternateMobileNo.Value.Trim() == "")
//            {
//                ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please provide emergency contact number.');", true);
//                txtAlternateMobileNo.Focus();
//                return false;
//            }
//            if (fldIsPassportRequired)
//            {
//                if (txtPassportNo.Value.Trim() == "")
//                {
//                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please provide passport number.');", true);
//                    txtPassportNo.Focus();
//                    return false;
//                }
//                if (txtPassportValidity.Text.Trim() == "")
//                {
//                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please provider passport validity.');", true);
//                    txtPassportValidity.Focus();
//                    return false;
//                }
//            }

//            if (rdbIsGSTApplicableYes.Checked == true)
//            {
//                if (txtCustomerGSTIN.Text.Trim() == "")
//                {
//                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter customer GSTIN.');", true);
//                    txtCustomerGSTIN.Focus();
//                    return false;
//                }
//                else
//                {
//                    ///^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9]{1}Z[0-9]{1}$/
//                    System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex("^[0-9]{2}[A-Z]{5}[0-9]{4}[A-Z]{1}[1-9]{1}Z[0-9A-Z]{1}$");
//                    bool isval = r.IsMatch(txtCustomerGSTIN.Text.Trim());
//                    if (!isval)
//                    {
//                        ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter valid GSTIN.');", true);
//                        txtCustomerGSTIN.Focus();
//                        return false;
//                    }
//                }
//                if (txtGstHolderName.Text.Trim() == "")
//                {
//                    ScriptManager.RegisterStartupScript(this, this.GetType(), "GropuLeaderValidate", "alert('Please enter GST holder name.');", true);
//                    txtGstHolderName.Focus();
//                    return false;
//                }

//            }
//            return true;
//        }
//        public void fnBindcountry(int lregionID)
//        {
//            DataTable ldtcountry = null;
//            ClsAdo clsObj = new ClsAdo();
//            try
//            {
//                ddlCountry.Items.Clear();
//                ldtcountry = clsObj.fnGetCountry(lregionID);
//                if (ldtcountry != null && ldtcountry.Rows.Count > 0)
//                {
//                    ReArrangeCountry(ref ldtcountry);
//                    ddlCountry.DataSource = ldtcountry;
//                    ddlCountry.DataTextField = "country_name";
//                    ddlCountry.DataValueField = "country_Id";
//                    ddlCountry.DataBind();
//                    ddlCountry.Items.Insert(0, new ListItem("--Select--", "0"));

//                }
//                else
//                {
//                    ddlCountry.Items.Clear();
//                }
//            }
//            catch { }
//            finally
//            {
//                if (ldtcountry != null)
//                {
//                    ldtcountry.Dispose();
//                    ldtcountry = null;
//                }
//            }
//        }


//        public void fnNationality(int lregionID)
//        {
//            DataTable ldtcountry = null;
//            ClsAdo clsObj = new ClsAdo();
//            try
//            {
//                ddlNationality.Items.Clear();
//                ldtcountry = clsObj.fnGetCountry(lregionID);
//                if (ldtcountry != null && ldtcountry.Rows.Count > 0)
//                {
//                    ReArrangeCountry(ref ldtcountry);
//                    ddlNationality.DataSource = ldtcountry;
//                    ddlNationality.DataTextField = "country_name";
//                    ddlNationality.DataValueField = "country_Id";
//                    ddlNationality.DataBind();
//                    ddlNationality.Items.Insert(0, new ListItem("--Select--", "0"));
//                }
//                else
//                {
//                    ddlNationality.Items.Clear();
//                }
//            }
//            catch { }
//            finally
//            {
//                if (ldtcountry != null)
//                {
//                    ldtcountry.Dispose();
//                    ldtcountry = null;
//                }
//            }
//        }


//        private void ReArrangeCountry(ref DataTable ldtcountry)
//        {
//            if (ldtcountry != null && ldtcountry.Rows.Count > 0)
//            {
//                DataRow[] foundRows;
//                string filter = "country_name = 'India'";
//                foundRows = ldtcountry.Select(filter);
//                int lIndex = 0;
//                foreach (DataRow dr in foundRows)
//                {
//                    DataRow ldr = ldtcountry.NewRow();
//                    ldr.ItemArray = dr.ItemArray;
//                    lIndex = dr.Table.Rows.IndexOf(dr);
//                    //ldtcountry.AcceptChanges();
//                    ldtcountry.Rows.InsertAt(ldr, 0);
//                    ldtcountry.Rows.RemoveAt(lIndex + 1);
//                    ldtcountry.AcceptChanges();
//                    break;
//                }
//            }
//        }

//        public void BindOccupation()
//        {
//            DataSet dscars = null;
//            try
//            {
//                string GetServiceChargeDetails = "GetOccupationList_sp";
//                SqlParameter[] param = new SqlParameter[1];
//                param[0] = new SqlParameter("@IsActive", 'Y'); //to get the all service list
//                dscars = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, GetServiceChargeDetails, param);

//                ddlOccupation.DataTextField = "Occupation";
//                ddlOccupation.DataValueField = "OccupationId";

//                ddlOccupation.DataSource = dscars.Tables[0];
//                ddlOccupation.DataBind();

//                ddlOccupation.Items.Insert(0, new ListItem("Select", "0"));
//                ddlOccupation.Items.Add(new ListItem("other", "-1"));

//            }
//            catch (Exception ex)
//            {

//            }
//            finally
//            {
//                if (dscars != null)
//                {
//                    dscars = null;
//                }
//            }
//        }

//        public void BindState()
//        {
//            DataSet dscars = null;
//            try
//            {
//                //if (Convert.ToInt32(ddlNationality.SelectedValue) > 0)
//                //{
//                string GetServiceChargeDetails = "GST_GetStateByCountryId_SP";
//                SqlParameter[] param = new SqlParameter[1];
//                param[0] = new SqlParameter("@i_CountryID", "1"); //to get the all service list
//                dscars = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, GetServiceChargeDetails, param);

//                if (dscars.Tables[0] != null && dscars.Tables[0].Rows.Count > 0)
//                {
//                    ddlState.DataTextField = "StateName";
//                    ddlState.DataValueField = "StateName";

//                    ddlState.DataSource = dscars.Tables[0];
//                    ddlState.DataBind();

//                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
//                    //ddlState.Items.Add(new ListItem("other", "-1"));
//                }
//                else
//                {
//                    ddlState.DataSource = null;
//                    ddlState.DataBind();
//                    ddlState.Items.Insert(0, new ListItem("--Select--", "0"));
//                }
//                //}

//            }
//            catch (Exception ex)
//            {

//            }
//            finally
//            {
//                if (dscars != null)
//                {
//                    dscars = null;
//                }
//            }
//        }

//        private DataTable fnExistCustomerDetail(string lOrdeID, int lRowID)
//        {
//            DataTable dt = new DataTable();
//            DataSet dataset = new DataSet();
//            string GetCustomerDetails = "ExistCustomerDetail_sp";
//            SqlParameter[] param = new SqlParameter[2];
//            param[0] = new SqlParameter("@i_OrderID", lOrdeID);
//            param[1] = new SqlParameter("@i_RowID", lRowID);
//            try
//            {
//                dataset = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, GetCustomerDetails, param);
//                dt = dataset.Tables[0];
//                return dt;
//            }
//            catch (Exception ex)
//            {
//                return null;
//            }
//            finally
//            {
//                if (dataset != null)
//                {
//                    dataset = null;
//                }
//                if (dt != null)
//                {
//                    dt = null;
//                }
//            }

//        }

//        private void ApprovalHierarchyUserList()
//        {
//            DataSet ldtResult = new DataSet();

//            try
//            {

//                SqlParameter[] param = new SqlParameter[1];
//                param[0] = new SqlParameter("@I_SectionID", "3");
//                ldtResult = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "GetApprovalHierarchyUserList", param);

//                ddlReferred.DataSource = ldtResult;
//                ddlReferred.DataTextField = "FullName";
//                ddlReferred.DataValueField = "UserId";
//                ddlReferred.DataBind();
//            }
//            finally
//            {
//                if (ldtResult != null)
//                {
//                    ldtResult.Dispose();
//                    ldtResult = null;
//                }
//            }
//            ddlReferred.Items.Insert(0, new ListItem("Select", "0"));

//        }
//        private bool AllowCreditBookingCHK(int pCustomerID)
//        {
//            bool pStatus = false;
//            DataSet dsResult = new DataSet();
//            DataTable dtResult = new DataTable();
//            try
//            {
//                SqlParameter[] lParam = new SqlParameter[1];
//                lParam[0] = new SqlParameter("@i_RowID", pCustomerID);
//                dsResult = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "AllowCreditBookingCHK_SP", lParam);
//                dtResult = dsResult.Tables[0];

//                if (dtResult != null && dtResult.Rows.Count > 0)
//                {
//                    if (dtResult.Rows[0]["CustomerType"].ToString() == "P")
//                    {
//                        if (dtResult.Rows[0]["BookingStatus"].ToString() == "S")
//                        {
//                            pStatus = false;
//                            fldIsApproval = true;
//                        }
//                        else
//                        {
//                            pStatus = true;
//                            fldIsApproval = false;
//                        }
//                    }
//                    else
//                    {
//                        pStatus = true;
//                    }

//                }

//                return pStatus;

//            }
//            catch (Exception ex)
//            {
//                return false;
//            }
//            finally
//            {
//                if (dsResult != null)
//                {
//                    dsResult = null;
//                }
//                if (dtResult != null)
//                {
//                    dtResult = null;
//                }
//            }
//        }

//        //[System.Web.Script.Services.ScriptMethod()]
//        //[WebMethod]
//        //public static string[] GetCity(string prefixText, int count, int? contextKey)
//        //{
//        //    int? lStateID1 = null;
//        //    if (contextKey > 0)
//        //        lStateID1 = contextKey;

//        //    GST_Data obj = new GST_Data();
//        //    List<GST_GetCityListByStateIdAndSearchedCityTextResult> objCity = obj.GST_GetCityListByStateIdAndSearchedCityText(prefixText, lStateID1);
//        //    List<string> txtItems = new List<string>();
//        //    String dbValues;
//        //    foreach (GST_GetCityListByStateIdAndSearchedCityTextResult item in objCity)
//        //    {
//        //        dbValues = AjaxControlToolkit.AutoCompleteExtender.CreateAutoCompleteItem(item.CityName.ToString(), item.CityID.ToString());
//        //        txtItems.Add(dbValues);
//        //    }
//        //    return txtItems.ToArray();
//        //}
//        #endregion
//    }
//    public class GroupLeader
//    {
//        #region "Member Variable(s)"
//        int pvCustomerID = 0, pvNationality, pvCountryID, pvStateID, pvReferredBy;
//        string pvTitle = "", pvName = "", pvAddress = "", pvPhoneNo = "", pvState = "", pvEmailID = "", pvMobileNo = "",
//            pvEmergencyNo = "", pvPassportNo = "", pvCountryName = "", pvNationalityName = "", pvCity = "", pvPinCode = "", pvSTDCode = "", pvPanImage = "", pvPanNo = "",
//            pvType = "", pvCompanyName = "", pvRemarks = "", pvAadharNo = "", pvAadharImage = "";
//        int pvAllowCreditBooking = 0, pvCreditPeriod = 0;
//        bool pvCanSendPromotions = true, pvIsPassportRequired = false, pvIsActive = true;
//        DateTime? pvPassValidUpto = null;
//        int pvOccupationId = 0; string pvOccupationName;
//        decimal pvAdvanceAmountPer = 0;
//        //bool pvIsGst = null; string pvGSTIN = null; string pvGSTHolderName = null;


//        #endregion
//        #region "Properties"
//        public int fldCustomerID
//        {
//            get { return pvCustomerID; }
//            set { pvCustomerID = value; }
//        }
//        public int fldNationality
//        {
//            get { return pvNationality; }
//            set { pvNationality = value; }
//        }
//        public int fldCountryID
//        {
//            get { return pvCountryID; }
//            set { pvCountryID = value; }
//        }

//        public int fldStateID
//        {
//            get { return pvStateID; }
//            set { pvStateID = value; }
//        }

//        public string fldTitle
//        {
//            get { return pvTitle; }
//            set { pvTitle = value; }
//        }

//        public string fldName
//        {
//            get
//            {
//                return pvName;
//            }
//            set { pvName = value; }
//        }

//        public string fldAddress
//        {
//            get
//            {
//                return pvAddress;
//            }
//            set { pvAddress = value; }
//        }
//        public string fldSTDCode
//        {
//            get
//            {
//                return pvSTDCode;
//            }
//            set { pvSTDCode = value; }
//        }

//        public string fldPhoneNo
//        {
//            get
//            {
//                return pvPhoneNo;
//            }
//            set { pvPhoneNo = value; }
//        }

//        public string fldState
//        {
//            get
//            {
//                return pvState;
//            }
//            set { pvState = value; }
//        }

//        public string fldEmailID
//        {
//            get
//            {
//                return pvEmailID;
//            }
//            set { pvEmailID = value; }
//        }

//        public string fldMobileNo
//        {
//            get
//            {
//                return pvMobileNo;
//            }
//            set { pvMobileNo = value; }
//        }

//        public string fldEmergencyNo
//        {
//            get
//            {
//                return pvEmergencyNo;
//            }
//            set { pvEmergencyNo = value; }
//        }

//        public string fldPassportNo
//        {
//            get
//            {
//                return pvPassportNo;
//            }
//            set { pvPassportNo = value; }
//        }

//        public string fldCountryName
//        {
//            get { return pvCountryName; }
//            set { pvCountryName = value; }
//        }

//        public string fldNationalityName
//        {
//            get { return pvNationalityName; }
//            set { pvNationalityName = value; }
//        }

//        public string fldCity
//        {
//            get { return pvCity; }
//            set { pvCity = value; }
//        }

//        public string fldPinCode
//        {
//            get { return pvPinCode; }
//            set { pvPinCode = value; }
//        }

//        public string fldPanNo
//        {
//            get { return pvPanNo; }
//            set { pvPanNo = value; }
//        }
//        public string fldPanImage
//        {
//            get { return pvPanImage; }
//            set { pvPanImage = value; }
//        }
//        public string fldAadharNo
//        {
//            get { return pvAadharNo; }
//            set { pvAadharNo = value; }
//        }
//        public string fldAadharImage
//        {
//            get { return pvAadharImage; }
//            set { pvAadharImage = value; }
//        }


//        public bool fldCanSendPromotions
//        {
//            get
//            {
//                return pvCanSendPromotions;
//            }
//            set { pvCanSendPromotions = value; }
//        }

//        public DateTime? fldPassValidUpto
//        {
//            get { return pvPassValidUpto; }
//            set { pvPassValidUpto = value; }
//        }

//        public int fldOccupationId
//        {
//            get { return pvOccupationId; }
//            set { pvOccupationId = value; }
//        }
//        public string fldOccupation
//        {
//            get { return pvOccupationName; }
//            set { pvOccupationName = value; }
//        }

//        public string fldType
//        {
//            get { return pvType; }
//            set { pvType = value; }
//        }
//        public string fldCompanyName
//        {
//            get { return pvCompanyName; }
//            set { pvCompanyName = value; }
//        }
//        public int fldAllowCreditBooking
//        {
//            get { return pvAllowCreditBooking; }
//            set { pvAllowCreditBooking = value; }
//        }
//        public int fldCreditPeriod
//        {
//            get { return pvCreditPeriod; }
//            set { pvCreditPeriod = value; }
//        }
//        public bool fldIsActive
//        {
//            get
//            {
//                return pvIsActive;
//            }
//            set { pvIsActive = value; }
//        }
//        public decimal fldAdvanceAmountPer
//        {
//            get { return pvAdvanceAmountPer; }
//            set { pvAdvanceAmountPer = value; }
//        }
//        public int fldReferredBy
//        {
//            get { return pvReferredBy; }
//            set { pvReferredBy = value; }
//        }
//        public string fldRemarks
//        {
//            get { return pvRemarks; }
//            set { pvRemarks = value; }
//        }

//        //public bool IsGSTIN
//        //{
//        //    get { return pvIsGst; }
//        //    set { pvIsGst = value; }
//        //}
//        //public string GSTIN
//        //{
//        //    get { return pvGSTIN; }
//        //    set { pvGSTIN = value; }
//        //}
//        //public string GSTHolderName
//        //{
//        //    get { return pvGSTHolderName; }
//        //    set { pvGSTHolderName = value; }
//        //}

//        public bool IsGSTIN { get; set; }
//        public string GSTIN { get; set; }
//        public string GSTHolderName { get; set; }
//        #endregion
//    }
//}