using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml.Linq;

namespace SouthernTravelIndiaAgent.UserControls
{
    public enum PanelSectiontable
    {
        SEC_EndUser = 1,
        SEC_Branch = 2,
        SEC_Agent = 3,
        SEC_Kerala = 4
    }
    public class TourCategoryHierarchytable
    {
        #region "Member Variable(s)"
        private int pvCategoryID, pvParentID;
        private string pvCategoryName, pvParentCategory;
        private bool pvIsSelected;
        #endregion
        #region "Property(s)"
        public int fldCategoryID
        {
            get { return pvCategoryID; }
            set { pvCategoryID = value; }
        }

        public string fldCategoryName
        {
            get { return pvCategoryName; }
            set { pvCategoryName = value; }
        }

        public int fldParentID
        {
            get { return pvParentID; }
            set { pvParentID = value; }
        }

        public string fldParentCategory
        {
            get { return pvParentCategory; }
            set { pvParentCategory = value; }
        }

        public bool fldIsSelected
        {
            get { return pvIsSelected; }
            set { pvIsSelected = value; }
        }
        #endregion"
        #region "Constructor"
        public TourCategoryHierarchytable()
        {

        }
        public TourCategoryHierarchytable(int pCategoryID, string pCategoryName, int pParentID, string pParentCategory, bool pIsSelected)
        {
            pvCategoryID = pCategoryID;
            pvCategoryName = pCategoryName;
            pvParentID = pParentID;
            pvParentCategory = pParentCategory;
            pvIsSelected = pIsSelected;
        }
        #endregion
    }
    public partial class UcSpecialFarePaneltable : System.Web.UI.UserControl
    {
        #region "Member Variable(s)"
        string pvTourType = "", pvBookNow = "", pvTDFareTypeHead = "", pvTDSeasonHead = "",
            pvTDFareValue = "", pvTDSeasonValue = "", pvFinalHTML = "", pvTourName = "", pvNotes = "", pvWidth = "", pvAlign = "", pvEBroucherEndUser = "", pvEBroucher = "", pvJourneyDate = "";
        int pbTourID;
        PanelSectiontable pvPanelSection;
        bool pvIsLTC = false, pvIsAccomodation = false, pvISQuery = false;
        bool pvShowBook = true;
        bool pvShowNotes = true;
        bool pvNotShowEBroucher = false;


        #endregion
        #region "Property(s)"
        public string fldJourneyDate
        {
            get { return pvJourneyDate; }
            set { pvJourneyDate = value; }
        }
        public string fldWidth
        {
            get { return pvWidth; }
            set { pvWidth = value; }
        }
        public string fldAlign
        {
            get { return pvAlign; }
            set { pvAlign = value; }
        }
        public bool NotShowEBroucher
        {
            get { return pvNotShowEBroucher; }
            set { pvNotShowEBroucher = value; }
        }
        public string fldTourType
        {
            get { return pvTourType; }
            set { pvTourType = value; }
        }

        public int fldTourID
        {
            get { return pbTourID; }
            set { pbTourID = value; }
        }

        public string fldTourName
        {
            get { return pvTourName; }
            set { pvTourName = value; }
        }
        public string fldNotes
        {
            get { return pvNotes; }
            set { pvNotes = value; }
        }

        public bool CanBook
        {
            get { return pvShowBook; }
            set { pvShowBook = value; }
        }
        private bool ISQuery
        {
            get { return pvISQuery; }
            set { pvISQuery = value; }
        }
        public bool fldShowNotes
        {
            get { return pvShowNotes; }
            set { pvShowNotes = value; }
        }
        public string fldBookNow
        {
            get
            {
                //            pvBookNow = @"<tr style=""BACKGROUND-COLOR: white;"">
                //                            <td colspan=""#ImgColSpan"" align=""right"">
                //
                //                                <a href=http://www.southerntravelsindia.com/download.aspx?file=spltour" + fldTourID + @".pdf >Download e-Broucher
                //                                 <img src=http://www.southerntravelsindia.com/Assets/images/pdf.gif alt=""image"" border=""0""/></a>&nbsp;
                //                                  <input id=""Button1"" style=""FONT-SIZE: 11px; COLOR: #333333; FONT-FAMILY: Arial; 
                //                                   BACKGROUND-COLOR: #eeeeee;BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: #000000 1px solid; WIDTH: 76px;
                //                                   BORDER-BOTTOM: #000000 1px solid; HEIGHT: 24px; BACKGROUND-COLOR: #ffd21e;"" 
                //                                   onclick=""Javascript:window.location.href='BookSpecialTour.aspx?TourID=" + fldTourID.ToString() + @"'""
                //                                   type=""button"" value=""Book Now"" name=""Button1""/>;
                //
                //                            </td>
                //                        </tr>";


                pvBookNow = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">

                                <a href=http://www.southerntravelsindia.com/download.aspx?file=spltour" + fldTourID + @".pdf >Download e-Broucher
                                 <img src=http://www.southerntravelsindia.com/Assets/images/pdf.gif alt=""image"" border=""0""/></a>&nbsp;
                                  <input id=""Button1"" style=""FONT-SIZE: 11px; COLOR: #333333; FONT-FAMILY: Arial; 
                                   BACKGROUND-COLOR: #eeeeee;BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: #000000 1px solid; WIDTH: 76px;
                                   BORDER-BOTTOM: #000000 1px solid; HEIGHT: 24px; BACKGROUND-COLOR: #ffd21e;"" 
                                   onclick=""Javascript:window.location.href='BookingOfSpecialTour_" + fldTourID.ToString() + @"'""
                                   type=""button"" value=""Book Now"" name=""Button1""/>

                            </td>
                        </tr>";

                if (!ISQuery)
                {
                    if (fldJourneyDate != null && fldJourneyDate != "")
                    {
                        pvEBroucherEndUser = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">
                        <a href=http://www.southerntravelsindia.com/download.aspx?file=spltour" + fldTourID + @".pdf >Download e-Broucher
                                 <img src=http://www.southerntravelsindia.com/Assets/images/pdf.gif alt=""image"" border=""0""/></a>&nbsp;
                    <img id=""Button" + fldTourID.ToString() + @""" alt="""" 
                        style=""cursor: pointer"" src=""Assets/images/btn_bookNow.gif""
                        onclick=""Javascript:window.location.href='BookSpecialTour.aspx?TourID=" + fldTourID.ToString() + "&jdate=" + fldJourneyDate + @"'""
                       

                            </td>
                        </tr>";
                    }
                    else
                    {
                        pvEBroucherEndUser = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">
                        <a href=http://www.southerntravelsindia.com/download.aspx?file=spltour" + fldTourID + @".pdf >Download e-Broucher
                                 <img src=http://www.southerntravelsindia.com/Assets/images/pdf.gif alt=""image"" border=""0""/></a>&nbsp;
                    <img id=""Button" + fldTourID.ToString() + @""" alt="""" 
                        style=""cursor: pointer"" src=""Assets/images/btn_bookNow.gif""
                        onclick=""Javascript:window.location.href='BookSpecialTour.aspx?TourID=" + fldTourID.ToString() + @"'""
                       

                            </td>
                        </tr>";
                    }
                }
                else
                {
                    pvEBroucherEndUser = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">
                      
                    <img id=""Button" + fldTourID.ToString() + @""" alt="""" 
                        style=""cursor: pointer"" src=""Assets/images/query.gif""
                        onclick=""Javascript:window.location.href='Fixedenquiryform.aspx?ID=" + fldTourID.ToString() + "&TourType=2" + "" + @"'""
                       

                            </td>
                        </tr>";
                }
                return (fldPanelSection == PanelSectiontable.SEC_EndUser ? pvEBroucherEndUser : pvBookNow);
                //return pvBookNow;


            }
        }
        public string fldEBroucher
        {
            get
            {
                pvEBroucher = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">
                                  <input id=""Button1"" style=""FONT-SIZE: 11px; COLOR: #333333; FONT-FAMILY: Arial; 
                                   BACKGROUND-COLOR: #eeeeee;BORDER-RIGHT: #000000 1px solid; BORDER-TOP: #000000 1px solid; FONT-WEIGHT: bold; BORDER-LEFT: #000000 1px solid; WIDTH: 76px;
                                   BORDER-BOTTOM: #000000 1px solid; HEIGHT: 24px; BACKGROUND-COLOR: #ffd21e;"" 
                                   onclick=""Javascript:window.location.href='BookingOfSpecialTour_" + fldTourID.ToString() + @"'""
                                   type=""button"" value=""Book Now"" name=""Button1""/>

                            </td>
                        </tr>";
                if (fldJourneyDate != null && fldJourneyDate != "")
                {
                    pvEBroucherEndUser = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">
                    <img id=""Button" + fldTourID.ToString() + @""" alt="""" 
                        style=""cursor: pointer"" src=""Assets/images/btn_bookNow.gif""
                        onclick=""Javascript:window.location.href='BookSpecialTour.aspx?TourID=" + fldTourID.ToString() + "&jdate=" + fldJourneyDate + @"'""
                       

                            </td>
                        </tr>";
                }
                else
                {
                    pvEBroucherEndUser = @"<tr style="""">
                            <td colspan=""#ImgColSpan"" align=""right"">
                    <img id=""Button" + fldTourID.ToString() + @""" alt="""" 
                        style=""cursor: pointer"" src=""Assets/images/btn_bookNow.gif""
                        onclick=""Javascript:window.location.href='BookSpecialTour.aspx?TourID=" + fldTourID.ToString() + @"'""
                       

                            </td>
                        </tr>";
                }
                return (fldPanelSection == PanelSectiontable.SEC_EndUser ? pvEBroucherEndUser : pvEBroucher);

            }
        }
        public string fldTDFareTypeHead
        {
            get
            {
                pvTDFareTypeHead = @"<td bgcolor=""" +
                    (fldPanelSection == PanelSectiontable.SEC_Branch ? "#FareBGColor" : (fldPanelSection == PanelSectiontable.SEC_Agent ? "#FareBGColor" : (fldPanelSection == PanelSectiontable.SEC_Kerala ? "#F3F3F3" : "#f5f5f5"))) +
                    @"""  rowspan=""#rowspan"" class=""#THClass"" style="" line-height:16px; width:50px; text-align:center"">
                #FareType </td>";
                return pvTDFareTypeHead;
            }
        }

        public string fldTDFareValue
        {
            get
            {
                pvTDFareValue = @"<td bgcolor=""#FareBGColor"" id=""TdAcValues1"" 
                style=""color: #FareForeColor; padding: 4px;""
                align=""right"">Rs. #FareValue /- </td>";
                return pvTDFareValue;
            }
        }

        public string fldTDSeasonHead
        {
            get
            {

                pvTDSeasonHead = @"<th align=center bgcolor=""" +
                    (fldPanelSection == PanelSectiontable.SEC_Branch ? "#FareBGColor" : (fldPanelSection == PanelSectiontable.SEC_Agent ? "#FareBGColor" : (fldPanelSection == PanelSectiontable.SEC_Kerala ? "#397900" : "#FareBGColor"))) +
                    @""" style=""color: " +
                    (fldPanelSection == PanelSectiontable.SEC_Branch ? "#FareForeColor" : (fldPanelSection == PanelSectiontable.SEC_Agent ? "#FareForeColor" : (fldPanelSection == PanelSectiontable.SEC_Kerala ? "#ffffff" : "#000000"))) +
                    @" line-height:16px; text-align:center;"" class=""#THClass"" valign=""#valign"" rowspan=""#RowSpan"" colspan=""#ColSpan"">
                #SeasonHead </th>";
                return pvTDSeasonHead;
            }
        }

        public string fldTDSeasonValue
        {
            get
            {
                pvTDSeasonValue = @"<td bgcolor=""" +
                     (fldPanelSection == PanelSectiontable.SEC_Branch ? "#FareBGColor" : (fldPanelSection == PanelSectiontable.SEC_Agent ? "#FareBGColor" : (fldPanelSection == PanelSectiontable.SEC_Kerala ? "#ffffff" : "#FareBGColor"))) +
                    @"""style=""color: #FareForeColor; padding: 4px; line-height: 16px;"" 
                width=""100px"" align=""center"">#SeasonValue </td>";
                return pvTDSeasonValue;
            }
        }

        public string fldFinalHTML
        {
            get
            {
                return pvFinalHTML;
            }
            set
            {
                pvFinalHTML = value;
            }
        }

        public PanelSectiontable fldPanelSection
        {
            get { return pvPanelSection; }
            set { pvPanelSection = value; }
        }

        public bool fldIsLTC
        {
            get
            {
                return pvIsLTC;
            }
            set
            {
                pvIsLTC = value;
            }
        }

        public bool fldIsAccomodation
        {
            get
            {
                return pvIsAccomodation;
            }
            set
            {
                pvIsAccomodation = value;
            }
        }
        #endregion
        #region "Event(s)"
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //GetFarePanel();
                GetNewFarePanel();
                if (fldTourID == 278 || fldTourID == 252) { divnote2.Visible = true; divnote1.Visible = false; }
                else { divnote2.Visible = false; divnote1.Visible = true; }
                tdPanel.Style.Add("max-width", fldWidth);
                tdPanel.Style.Add("float", fldAlign);
            }
        }
        #endregion
        #region "Method(s)"
        private void GetFarePanel()
        {
            ClsAdo lContext = new ClsAdo();
            DataTable ldtFarePanel = null;
            List<Get_SPL_Fare_Panel_spResult> lOrderedFarePanel = null;
            List<Get_SPL_Fare_Panel_spResult> lFarePanel = null;
            try
            {
                int? lStatus = 0;
                string strSpTourName = "";
                string strSpNotes = "";
                bool? lIsAccomodation = false;
                bool? lIsQuery = false;
                lFarePanel =
                   lContext.Get_SpecialTour_FarePanel("fixed", fldTourID, fldIsLTC, out lIsAccomodation,
                   out lStatus, out strSpTourName, out strSpNotes, out lIsQuery)/*.OrderBy(x => x.CategoryID).ThenBy(y => y.SeasonID)*/.ToList();
                fldTourName = strSpTourName;
                fldNotes = strSpNotes;
                ISQuery = Convert.ToBoolean(lIsQuery);
                if (strSpNotes.Trim().Length > 0)
                    lblNote.Text = "<b>Note :</b> " + strSpNotes;

                lOrderedFarePanel =
                     lFarePanel.OrderBy(VehID => VehID.VehicleID).
                         ThenBy(PaxID => PaxID.PaxID).
                         ThenBy(FrmDate => FrmDate.FromDate).
                         ThenBy(SesID => SesID.SeasonID).
                         ThenBy(CatID => CatID.CategoryID).ToList();
                ldtFarePanel = new DataTable();
                ArrayList lSeasonIDs = new ArrayList();
                if (lFarePanel != null && lFarePanel.Count > 0)
                {
                    ldtFarePanel.Columns.Add("VehicleID", typeof(int));
                    ldtFarePanel.Columns.Add("VehicleName", typeof(string));
                    ldtFarePanel.Columns.Add("PaxID", typeof(int));
                    ldtFarePanel.Columns.Add("PaxType", typeof(string));
                    ldtFarePanel.Columns.Add("CategoryID", typeof(int));
                    ldtFarePanel.Columns.Add("CategoryName", typeof(string));
                    ldtFarePanel.Columns.Add("SeasonID", typeof(int));
                    //ldtFarePanel.Columns.Add("Fare", typeof(int));
                    // ***** TO GET SEASON COLUMNS *****
                    #region "TO GET SEASON COLUMNS"
                    int lCategoryID = Convert.ToInt32(lFarePanel[0].CategoryID);
                    int lSeasonID = Convert.ToInt32(lFarePanel[0].SeasonID);
                    string lSeasonHeading = string.Empty, lDistinctSeasonIDs = lCategoryID.ToString();
                    for (int Ctr = 0; Ctr < lFarePanel.Count; Ctr++)
                    {
                        if (lCategoryID == lFarePanel[Ctr].CategoryID)
                        {
                            if (lFarePanel[Ctr].Season.Trim() != string.Empty)
                            {
                                CheckSeasonHead(Convert.ToInt32(lFarePanel[Ctr].SeasonID), lFarePanel[Ctr].VehicleID,
                                    lFarePanel[Ctr].PaxID, lFarePanel[Ctr].Season, ref lSeasonIDs);
                            }
                        }
                        else
                        {
                            break;
                        }
                    }
                    lDistinctSeasonIDs = string.Empty;
                    for (int Ctr = 0; Ctr < lSeasonIDs.Count; Ctr++)
                    {
                        ldtFarePanel.Columns.Add(((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonHead, typeof(string));
                        if (lDistinctSeasonIDs.Trim() == string.Empty)
                        {
                            lDistinctSeasonIDs = ((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonID.ToString();
                        }
                        else
                        {
                            lDistinctSeasonIDs += "," + ((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonID.ToString();
                        }
                    }
                    #endregion
                    string[] larrSeasonIDs = lDistinctSeasonIDs.Split(',');
                    Array.Sort<string>(larrSeasonIDs);
                    for (int Ctr = 0; Ctr < lOrderedFarePanel.Count; Ctr++)
                    {
                        string lCurrentColumn = string.Empty;
                        if (ldtFarePanel.Rows.Count > 0)
                        {
                            bool lAlreadyExist = false;
                            // Some old Pax detail is available.
                            for (int PaxCtr = 0; PaxCtr < ldtFarePanel.Rows.Count; PaxCtr++)
                            {
                                if ((Convert.ToInt32(lOrderedFarePanel[Ctr].CategoryID) ==
                                    Convert.ToInt32(ldtFarePanel.Rows[PaxCtr]["CategoryID"])) &&
                                    (Convert.ToInt32(lOrderedFarePanel[Ctr].SeasonID) ==
                                    Convert.ToInt32(ldtFarePanel.Rows[PaxCtr]["SeasonID"])) &&
                                    (Convert.ToInt32(lOrderedFarePanel[Ctr].VehicleID) ==
                                    Convert.ToInt32(ldtFarePanel.Rows[PaxCtr]["VehicleID"])) &&
                                    (Convert.ToInt32(lOrderedFarePanel[Ctr].PaxID) ==
                                    Convert.ToInt32(ldtFarePanel.Rows[PaxCtr]["PaxID"])))
                                {
                                    lAlreadyExist = true;
                                    break;
                                }
                            }
                            if (!lAlreadyExist)
                            {
                                DataRow ldrFarePanel = ldtFarePanel.NewRow();
                                ldrFarePanel["VehicleID"] = lOrderedFarePanel[Ctr].VehicleID;
                                ldrFarePanel["VehicleName"] = lOrderedFarePanel[Ctr].VehicleName.ToString();
                                ldrFarePanel["PaxID"] = lOrderedFarePanel[Ctr].PaxID;
                                ldrFarePanel["PaxType"] = lOrderedFarePanel[Ctr].PaxType.ToString();
                                ldrFarePanel["CategoryID"] = Convert.ToInt32(lOrderedFarePanel[Ctr].CategoryID);
                                ldrFarePanel["CategoryName"] = lOrderedFarePanel[Ctr].CategoryName;
                                ldrFarePanel["SeasonID"] = Convert.ToInt32(lOrderedFarePanel[Ctr].SeasonID);
                                lCurrentColumn = GetColumnName(lSeasonIDs, Convert.ToInt32(lOrderedFarePanel[Ctr].SeasonID));
                                ldrFarePanel[lCurrentColumn] = Convert.ToInt32(lOrderedFarePanel[Ctr].Fare);
                                ldtFarePanel.Rows.Add(ldrFarePanel);
                            }
                        }
                        else
                        {
                            // No Pax detail available.
                            DataRow ldrFarePanel = ldtFarePanel.NewRow();
                            ldrFarePanel["VehicleID"] = lOrderedFarePanel[Ctr].VehicleID;
                            ldrFarePanel["VehicleName"] = lOrderedFarePanel[Ctr].VehicleName.ToString();
                            ldrFarePanel["PaxID"] = lOrderedFarePanel[Ctr].PaxID;
                            ldrFarePanel["PaxType"] = lOrderedFarePanel[Ctr].PaxType.ToString();
                            ldrFarePanel["CategoryID"] = Convert.ToInt32(lOrderedFarePanel[Ctr].CategoryID);
                            ldrFarePanel["CategoryName"] = lOrderedFarePanel[Ctr].CategoryName;
                            ldrFarePanel["SeasonID"] = Convert.ToInt32(lOrderedFarePanel[Ctr].SeasonID);
                            lCurrentColumn = GetColumnName(lSeasonIDs, Convert.ToInt32(lOrderedFarePanel[Ctr].SeasonID));
                            ldrFarePanel[lCurrentColumn] = Convert.ToInt32(lOrderedFarePanel[Ctr].Fare);
                            ldtFarePanel.Rows.Add(ldrFarePanel);
                        }
                    }
                    //if (fldPanelSection == PanelSectiontable.SEC_EndUser)
                    //{
                    //    ShowFarePanel_EndUser(ldtFarePanel, lSeasonIDs);
                    //}
                    //else
                    //{
                    ShowFarePanel(ldtFarePanel, lSeasonIDs);
                    //}
                    /*grvFirstFarePanel.DataSource = ldtFarePanel;
                    grvFirstFarePanel.DataBind();
                    grvFarePanel.DataSource = lOrderedFarePanel;
                    grvFarePanel.DataBind();*/
                }
            }
            catch (Exception Ex)
            {
            }
            finally
            {
                if (lContext != null)
                {
                    
                    lContext = null;
                }
                if (ldtFarePanel != null)
                {
                    ldtFarePanel.Dispose();
                    ldtFarePanel = null;
                }
                if (lOrderedFarePanel != null)
                {
                    lOrderedFarePanel = null;
                }
                if (lFarePanel != null)
                {
                    lFarePanel = null;
                }
            }
        }

        private string GetColumnName(ArrayList lSeasonIDs, int pSeasonID)
        {
            string lColumnName = string.Empty;
            for (int Ctr = 0; Ctr < lSeasonIDs.Count; Ctr++)
            {
                if (pSeasonID == ((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonID)
                {
                    lColumnName = ((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonHead;
                    break;
                }
            }
            return lColumnName;
        }

        private void CheckSeasonHead(long? pSeasonID, int pVehicleID, int pPaxID, string pSeasonHeading, ref ArrayList parrSeasonIDs)
        {
            if (parrSeasonIDs.Count == 0)
            {
                pbSplTourSeasons lTourSeason = new pbSplTourSeasons();
                lTourSeason.pbSeasonID = Convert.ToInt32(pSeasonID);
                lTourSeason.pbVehicleID = pVehicleID;
                lTourSeason.pbPaxID = pPaxID;
                lTourSeason.pbSeasonHead = pSeasonHeading;
                parrSeasonIDs.Add(lTourSeason);
            }
            else
            {
                bool lExist = false;
                for (int Ctr = 0; Ctr < parrSeasonIDs.Count; Ctr++)
                {
                    pbSplTourSeasons lTourSeason = (pbSplTourSeasons)parrSeasonIDs[Ctr];
                    if (lTourSeason.pbSeasonID == Convert.ToInt32(pSeasonID))
                    {
                        if ((lTourSeason.pbVehicleID == pVehicleID) && (lTourSeason.pbPaxID == pPaxID))
                        {
                            lTourSeason.pbSeasonHead += ", " + pSeasonHeading;
                            parrSeasonIDs[Ctr] = lTourSeason;
                        }
                        lExist = true;
                        break;
                    }
                }
                if (!lExist)
                {
                    pbSplTourSeasons lTourSeason = new pbSplTourSeasons();
                    lTourSeason.pbSeasonID = Convert.ToInt32(pSeasonID);
                    lTourSeason.pbVehicleID = pVehicleID;
                    lTourSeason.pbPaxID = pPaxID;
                    lTourSeason.pbSeasonHead = pSeasonHeading;
                    parrSeasonIDs.Add(lTourSeason);
                }
            }
        }

        private void ShowFarePanel(DataTable pdtFarePanel, ArrayList pSeasonIDs)
        {
            StringBuilder lFarePanel = new StringBuilder();
            lFarePanel.Append("<table bgcolor=#cccccc cellpadding=0 cellspacing=1 width=\"100%\" >");
            int lStandard = 0, lDelux = 0;
            for (int Ctr = 0; Ctr < pdtFarePanel.Rows.Count; Ctr++)
            {
                if (Convert.ToString(pdtFarePanel.Rows[Ctr]["CategoryName"]) != "Deluxe")
                {
                    lStandard = 1;
                }
                if (Convert.ToString(pdtFarePanel.Rows[Ctr]["CategoryName"]) != "Standard")
                {
                    lDelux = 1;
                }
            }
            // ***** FOT GETTING HEADER *****
            lFarePanel.Append("<tr>");
            lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", "Type of Vehicles").Replace("#RowSpan", "2").Replace("#ColSpan", "2").Replace("#valign", "middle"));
            for (int HeadCtr = 7; HeadCtr < pdtFarePanel.Columns.Count; HeadCtr++)
            {
                lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", pdtFarePanel.Columns[HeadCtr].ColumnName).
                    Replace("#ColSpan", (lStandard + lDelux).ToString()).Replace("#valign", "middle"));
            }
            lFarePanel.Append("</tr>");
            //*****FOR GETTING SUB-HEADER
            lFarePanel.Append("<tr>");
            for (int HeadCtr = 7; HeadCtr < pdtFarePanel.Columns.Count; HeadCtr++)
            {
                if (lStandard > 0)
                {
                    lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", "Standard (Non-AC)").
                        Replace("#ColSpan", "0"));
                }
                if (lDelux > 0)
                {
                    lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", "Deluxe (AC)").
                       Replace("#ColSpan", "0"));
                }
            }
            lFarePanel.Append("</tr>");

            int lCurrPaxID = 0, lPrevPaxID = 0, lPrevVehicleID = 0, lCurrVehicleID = 0, lCurrRowSpan = 1;
            for (int Ctr = 0; Ctr < pdtFarePanel.Rows.Count; Ctr++)
            {
                lCurrPaxID = Convert.ToInt32(pdtFarePanel.Rows[Ctr]["PaxID"]);
                lCurrVehicleID = Convert.ToInt32(pdtFarePanel.Rows[Ctr]["VehicleID"]);
                if (lPrevPaxID == 0)
                {
                    lPrevPaxID = lCurrPaxID;
                    lPrevVehicleID = lCurrVehicleID;
                    lFarePanel.Append("<tr>");
                    lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["VehicleName"])).
                        Replace("#rowspan", "#rowspan" + lPrevVehicleID.ToString()));
                    lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["PaxType"])));
                }

                if (lPrevPaxID == lCurrPaxID)
                {
                    lFarePanel.Append(fldTDSeasonValue.Replace("#SeasonValue",
                        pdtFarePanel.Rows[Ctr][GetColumnName(pSeasonIDs, Convert.ToInt32(pdtFarePanel.Rows[Ctr]["SeasonID"]))].ToString() + ".00"));
                }
                else
                {
                    lFarePanel.Append("</tr>");
                    lFarePanel.Append("<tr>");
                    if (lPrevVehicleID != lCurrVehicleID)
                    {
                        lFarePanel.Replace("#rowspan" + lPrevVehicleID.ToString(), lCurrRowSpan.ToString());
                        lCurrRowSpan = 1;
                        lPrevVehicleID = lCurrVehicleID;
                    }
                    else
                    {
                        lCurrRowSpan++;
                    }
                    if (lCurrRowSpan == 1)
                    {
                        lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["VehicleName"])).
                            Replace("#rowspan", "#rowspan" + lPrevVehicleID.ToString()));
                    }
                    lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["PaxType"])));
                    lFarePanel.Append(fldTDSeasonValue.Replace("#SeasonValue",
                        pdtFarePanel.Rows[Ctr][GetColumnName(pSeasonIDs, Convert.ToInt32(pdtFarePanel.Rows[Ctr]["SeasonID"]))].ToString() + ".00"));
                    lPrevPaxID = lCurrPaxID;
                }
            }
            if (lCurrRowSpan > 1)
            {
                lFarePanel.Replace("#rowspan" + lPrevVehicleID.ToString(), lCurrRowSpan.ToString());
            }
            lFarePanel.Append("</tr>");

            if (NotShowEBroucher)
            {
                lFarePanel.Append(fldEBroucher.Replace("#ImgColSpan", ((pSeasonIDs.Count * 2) + 1).ToString()));
                //tdPanel.Width = "100%";
                /*if (lblNote.Text == "")
                {
                    tdPanel.Style.Add("Width", "100%");
                    tdPanel.Align = "right";
                }*/
            }
            if (CanBook)
            {
                if (!NotShowEBroucher)
                {
                    lFarePanel.Append(fldBookNow.Replace("#ImgColSpan", ((pSeasonIDs.Count * 2) + 2).ToString()));
                }
            }
            lFarePanel.Append("</table>");
            litSpecialTourFarePanel.Text = lFarePanel.ToString();

            HiddenField hdnTourFare = this.Page.FindControl("hdnTourFare") as HiddenField;
            hdnTourFare.Value = hdnTourFare.Value + "<h3 class='title'>Tour <span>Price</span></h3><div class='tablewrap'>" + lFarePanel.ToString() + "</div>";

        }
        #endregion
        #region "New Implementation"
        private void GetNewFarePanel_XML()
        {
            Get_SPL_Fare_Panel_spResult lContext = null;
            List<Get_SPL_Fare_Panel_spResult> lFarePanel = null;
            List<Get_SPL_Fare_Panel_spResult> lOrderedFarePanel = null;
            Get_SPL_Fare_Panel_spResult lCurrentFare = null;
            DataTable ldtFarePanel = null;
            XDocument XDocTourItinerary = XDocument.Load(Server.MapPath("Common/SpecialTourFarePanel.xml"));
            try
            {
                int? lStatus = 0;
                string strSpTourName = "";
                string strSpNotes = "";
                bool? lIsAccomodation = false;
                bool? lIsQuery = true;
                /*lContext = new Get_SPL_Fare_Panel_spResult(DataLib.getConnectionString());
                lFarePanel =
                    lContext.Get_SpecialTour_FarePanel("fixed", fldTourID, fldIsLTC, ref lIsAccomodation,
                    ref lStatus, ref strSpTourName, ref strSpNotes, ref lIsQuery).ToList();*/
                var varTourItinerary = from FarePanelList in XDocTourItinerary.Descendants("SPLFarePanel")
                                       where Convert.ToInt32(FarePanelList.Element("TourID").Value) == fldTourID
                                       select new
                                       {
                                           PanelID = FarePanelList.Element("PanelID").Value.Trim(),
                                           FareID = FarePanelList.Element("FareID").Value.Trim(),
                                           CategoryName = FarePanelList.Element("CategoryName").Value.Trim(),
                                           Fare = FarePanelList.Element("Fare").Value.Trim(),
                                           FromDate = FarePanelList.Element("FromDate").Value.Trim(),
                                           ToDate = FarePanelList.Element("ToDate").Value.Trim(),
                                           s_month = FarePanelList.Element("s_month").Value.Trim(),
                                           CategoryID = FarePanelList.Element("CategoryID").Value.Trim(),
                                           Season = FarePanelList.Element("Season").Value.Trim(),
                                           SeasonID = FarePanelList.Element("SeasonID").Value.Trim(),
                                           VehicleID = FarePanelList.Element("VehicleID").Value.Trim(),
                                           VehicleName = FarePanelList.Element("VehicleName").Value.Trim(),
                                           PaxID = FarePanelList.Element("PaxID").Value.Trim(),
                                           PaxType = FarePanelList.Element("PaxType").Value.Trim(),
                                           ParentID = FarePanelList.Element("ParentID").Value.Trim(),
                                           ParentCategory = FarePanelList.Element("ParentCategory").Value.Trim(),
                                           TourName = FarePanelList.Element("TourName").Value.Trim(),
                                           Notes = FarePanelList.Element("Notes").Value.Trim(),
                                           IsQuery = FarePanelList.Element("IsQuery").Value.Trim()
                                       };
                int FareCtr = 0;
                lFarePanel = new List<Get_SPL_Fare_Panel_spResult>();
                foreach (var CFare in varTourItinerary.ToList())
                {
                    lCurrentFare = new Get_SPL_Fare_Panel_spResult();
                    lCurrentFare.PanelID = Convert.ToInt32(CFare.PanelID);
                    lCurrentFare.FareID = Convert.ToInt32(CFare.FareID);
                    lCurrentFare.CategoryName = CFare.CategoryName;
                    lCurrentFare.Fare = Convert.ToDecimal(CFare.Fare);
                    lCurrentFare.FromDate = Convert.ToDateTime(CFare.FromDate);
                    lCurrentFare.ToDate = Convert.ToDateTime(CFare.ToDate);
                    lCurrentFare.s_month = CFare.s_month;
                    lCurrentFare.CategoryID = Convert.ToInt32(CFare.CategoryID);
                    lCurrentFare.Season = CFare.Season;
                    lCurrentFare.SeasonID = Convert.ToInt64(CFare.SeasonID);
                    lCurrentFare.VehicleID = Convert.ToInt32(CFare.VehicleID);
                    lCurrentFare.VehicleName = CFare.VehicleName;
                    lCurrentFare.PaxID = Convert.ToInt32(CFare.PaxID);
                    lCurrentFare.PaxType = CFare.PaxType;
                    lCurrentFare.ParentID = Convert.ToInt32(CFare.ParentID);
                    lCurrentFare.ParentCategory = CFare.ParentCategory;
                    if (FareCtr == 0)
                    {
                        strSpTourName = CFare.TourName;
                        strSpNotes = CFare.Notes;
                        lIsQuery = Convert.ToBoolean(CFare.IsQuery);
                        FareCtr++;
                    }
                    lFarePanel.Add(lCurrentFare);
                }


                fldTourName = strSpTourName;
                fldNotes = strSpNotes;
                ISQuery = Convert.ToBoolean(lIsQuery);
                if (strSpNotes.Trim().Length > 0)
                    lblNote.Text = "<b>Note :</b> " + strSpNotes;

                //lOrderedFarePanel =
                //    lFarePanel.OrderBy(FrmDate => FrmDate.FromDate).
                //        ThenBy(VehID => VehID.VehicleID).
                //        ThenBy(PaxID => PaxID.PaxID).
                //        ThenBy(SesID => SesID.SeasonID).
                //        ThenBy(CatID => CatID.CategoryID).ToList();


                lOrderedFarePanel =
                lFarePanel.OrderBy(FrmDate => FrmDate.FromDate).
                    ThenBy(VehID => VehID.VehicleID).
                    ThenBy(PaxID => PaxID.PaxID).
                    ThenBy(CatID => CatID.CategoryID).ToList();

                ldtFarePanel = new DataTable();
                ArrayList lSeasonIDs = new ArrayList();
                if ((lFarePanel != null && lFarePanel.Count > 0) && (!IsStudentPackage()))
                {
                    ldtFarePanel.Columns.Add("VehicleID", typeof(int));
                    ldtFarePanel.Columns.Add("VehicleName", typeof(string));
                    ldtFarePanel.Columns.Add("PaxID", typeof(int));
                    ldtFarePanel.Columns.Add("PaxType", typeof(string));
                    ldtFarePanel.Columns.Add("CategoryID", typeof(int));
                    ldtFarePanel.Columns.Add("CategoryName", typeof(string));
                    ldtFarePanel.Columns.Add("SeasonID", typeof(int));
                    ldtFarePanel.Columns.Add("ParentID", typeof(int));
                    ldtFarePanel.Columns.Add("ParentCategory", typeof(string));
                    //ldtFarePanel.Columns.Add("Fare", typeof(int));
                    // ***** TO GET SEASON COLUMNS *****
                    #region "TO GET SEASON COLUMNS"
                    int lCategoryID = Convert.ToInt32(lFarePanel[0].CategoryID);
                    int lSeasonID = Convert.ToInt32(lFarePanel[0].SeasonID);
                    string lSeasonHeading = string.Empty, lDistinctSeasonIDs = lCategoryID.ToString();
                    for (int Ctr = 0; Ctr < lFarePanel.Count; Ctr++)
                    {
                        if (lFarePanel[Ctr].Season.Trim() != string.Empty)
                        {
                            CheckNewSeasonHead(Convert.ToInt32(lFarePanel[Ctr].SeasonID), lFarePanel[Ctr].ParentID,
                                Convert.ToInt32(lFarePanel[Ctr].CategoryID), lFarePanel[Ctr].Season, lFarePanel[Ctr].ParentCategory,
                                lFarePanel[Ctr].CategoryName, (Convert.ToDecimal(lFarePanel[Ctr].Fare) > 0 ? true : false), ref lSeasonIDs);
                        }
                    }
                    lOrderedFarePanel = new List<Get_SPL_Fare_Panel_spResult>();
                    lOrderedFarePanel = GetDistinctFare(lFarePanel);
                    if (fldPanelSection == PanelSectiontable.SEC_EndUser)
                    {
                        ShowFarePanel_EndUser(lOrderedFarePanel, lSeasonIDs);
                    }
                    else
                    {
                        ShowFarePanel(lOrderedFarePanel, lSeasonIDs);
                    }

                    return;
                    #endregion
                }
                else
                {
                    if (IsStudentPackage())
                    {
                        string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        /*url = url.Substring(0, url.IndexOf(HttpContext.Current.Request.ApplicationPath) + HttpContext.Current.Request.ApplicationPath.Length) +
                            "/StatFarePanel.aspx?TourID=" + fldTourID.ToString() + "&TourType=2";*/
                        //url = "http://test.southerntravelsindia.com/WSND/StatFarePanel.aspx?TourID=" + fldTourID.ToString() + "&TourType=2";

                        string BasePath = System.Configuration.ConfigurationManager.AppSettings["SouthernBasePath"];
                        url = BasePath + "StatFarePanel.aspx?TourID=" + fldTourID.ToString() + "&TourType=2";

                        System.Net.HttpWebRequest requestToSender = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                        System.Net.HttpWebResponse responseFromSender = (System.Net.HttpWebResponse)requestToSender.GetResponse();
                        string TourFare = string.Empty;

                        //Remember to use System.IO namespace
                        using (System.IO.StreamReader responseReader = new System.IO.StreamReader(responseFromSender.GetResponseStream()))
                        {
                            TourFare = responseReader.ReadToEnd();
                        }
                        litSplTourFarePanelBook.Text = TourFare;
                    }
                    else
                    {
                        if (ISQuery)
                        {
                            litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\" width=\"100%\">" + fldBookNow.Replace("#ImgColSpan", "") + "</table>";
                        }
                    }
                    lblNote.Text = "";
                }
            }
            catch (Exception Ex)
            {
            }
            finally
            {
                if (lContext != null)
                {
                    lContext = null;
                }
                if (ldtFarePanel != null)
                {
                    ldtFarePanel.Dispose();
                    ldtFarePanel = null;
                }
                if (lOrderedFarePanel != null)
                {
                    lOrderedFarePanel = null;
                }
                if (lFarePanel != null)
                {
                    lFarePanel = null;
                }
                if (lCurrentFare != null)
                {
                    lCurrentFare = null;
                }
                if (XDocTourItinerary != null)
                {
                    XDocTourItinerary = null;
                }
            }
        }
        public void GetNewFarePanel()
        {
            ClsAdo lContext = new ClsAdo();
            List<Get_SPL_Fare_Panel_spResult> lFarePanel = null;
            List<Get_SPL_Fare_Panel_spResult> lOrderedFarePanel = null;
            DataTable ldtFarePanel = null;
            try
            {
                int? lStatus = 0;
                string strSpTourName = "";
                string strSpNotes = "";
                bool? lIsAccomodation = false;
                bool? lIsQuery = false;
                lFarePanel =
                    lContext.Get_SpecialTour_FarePanel("fixed", fldTourID, fldIsLTC, out lIsAccomodation,
                    out lStatus, out strSpTourName, out strSpNotes, out lIsQuery).OrderBy(x => x.CategoryID).ThenBy(y => y.SeasonID).ToList();
                fldTourName = strSpTourName;
                fldNotes = strSpNotes;
                ISQuery = Convert.ToBoolean(lIsQuery);
                if (strSpNotes.Trim().Length > 0)
                    lblNote.Text = "<b>Note :</b> " + strSpNotes;

                lOrderedFarePanel =
                    lFarePanel.OrderBy(FrmDate => FrmDate.FromDate).
                        ThenBy(VehID => VehID.VehicleID).
                        ThenBy(PaxID => PaxID.PaxID).
                        ThenBy(SesID => SesID.SeasonID).
                        ThenBy(CatID => CatID.CategoryID).ToList();

                // added to sort by season date
                lFarePanel =
                    lFarePanel.OrderBy(FrmDate => FrmDate.FromDate).
                        ThenBy(VehID => VehID.VehicleID).
                        ThenBy(PaxID => PaxID.PaxID).
                        ThenBy(CatID => CatID.CategoryID).ToList();


                ldtFarePanel = new DataTable();
                ArrayList lSeasonIDs = new ArrayList();
                if ((lFarePanel != null && lFarePanel.Count > 0) && (!IsStudentPackage()))
                {
                    ldtFarePanel.Columns.Add("VehicleID", typeof(int));
                    ldtFarePanel.Columns.Add("VehicleName", typeof(string));
                    ldtFarePanel.Columns.Add("PaxID", typeof(int));
                    ldtFarePanel.Columns.Add("PaxType", typeof(string));
                    ldtFarePanel.Columns.Add("CategoryID", typeof(int));
                    ldtFarePanel.Columns.Add("CategoryName", typeof(string));
                    ldtFarePanel.Columns.Add("SeasonID", typeof(int));
                    ldtFarePanel.Columns.Add("ParentID", typeof(int));
                    ldtFarePanel.Columns.Add("ParentCategory", typeof(string));
                    //ldtFarePanel.Columns.Add("Fare", typeof(int));
                    // ***** TO GET SEASON COLUMNS *****
                    #region "TO GET SEASON COLUMNS"
                    int lCategoryID = Convert.ToInt32(lFarePanel[0].CategoryID);
                    int lSeasonID = Convert.ToInt32(lFarePanel[0].SeasonID);
                    string lSeasonHeading = string.Empty, lDistinctSeasonIDs = lCategoryID.ToString();
                    for (int Ctr = 0; Ctr < lFarePanel.Count; Ctr++)
                    {
                        if (lFarePanel[Ctr].Season.Trim() != string.Empty)
                        {
                            CheckNewSeasonHead(Convert.ToInt32(lFarePanel[Ctr].SeasonID), lFarePanel[Ctr].ParentID,
                                Convert.ToInt32(lFarePanel[Ctr].CategoryID), lFarePanel[Ctr].Season, lFarePanel[Ctr].ParentCategory,
                                lFarePanel[Ctr].CategoryName, (Convert.ToDecimal(lFarePanel[Ctr].Fare) > 0 ? true : false), ref lSeasonIDs);
                        }
                    }
                    lOrderedFarePanel = new List<Get_SPL_Fare_Panel_spResult>();
                    lOrderedFarePanel = GetDistinctFare(lFarePanel);
                    if (fldPanelSection == PanelSectiontable.SEC_EndUser)
                    {
                        ShowFarePanel_EndUser(lOrderedFarePanel, lSeasonIDs);
                    }
                    else
                    {
                        ShowFarePanel(lOrderedFarePanel, lSeasonIDs);
                    }

                    return;
                    #endregion
                }
                else
                {
                    if (IsStudentPackage())
                    {
                        string url = HttpContext.Current.Request.Url.AbsoluteUri;
                        /*url = url.Substring(0, url.IndexOf(HttpContext.Current.Request.ApplicationPath) + HttpContext.Current.Request.ApplicationPath.Length) +
                            "/StatFarePanel.aspx?TourID=" + fldTourID.ToString() + "&TourType=2";*/
                        //url = "http://test.southerntravelsindia.com/WSND/StatFarePanel.aspx?TourID=" + fldTourID.ToString() + "&TourType=2";
                        string BasePath = System.Configuration.ConfigurationManager.AppSettings["SouthernBasePath"];
                        url = BasePath + "StatFarePanel.aspx?TourID=" + fldTourID.ToString() + "&TourType=2";

                        System.Net.HttpWebRequest requestToSender = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(url);
                        System.Net.HttpWebResponse responseFromSender = (System.Net.HttpWebResponse)requestToSender.GetResponse();
                        string TourFare = string.Empty;

                        //Remember to use System.IO namespace
                        using (System.IO.StreamReader responseReader = new System.IO.StreamReader(responseFromSender.GetResponseStream()))
                        {
                            TourFare = responseReader.ReadToEnd();
                        }
                        litSplTourFarePanelBook.Text = TourFare;
                    }
                    else
                    {
                        if (ISQuery)
                        {
                            litSplTourFarePanelBook.Text = "<p class=\"btnwrap\" style=\"text-align: right;\">" +
       "<input id=\"btnEnquiry133\"  name=\"btnEnquiry133\" style=\"cursor: pointer; width:150px\" class=\"commonbtn\" onclick=\"Javascript:window.location.href='Fixedenquiryform.aspx?ID=" + fldTourID + "&TourType=2'\" value=\"Submit Query\"/>" +

            "</p>";


                        }
                    }
                    lblNote.Text = "";
                }
            }
            catch (Exception Ex)
            {
            }
            finally
            {
                if (lContext != null)
                {
                    lContext = null;
                }
                if (ldtFarePanel != null)
                {
                    ldtFarePanel.Dispose();
                    ldtFarePanel = null;
                }
                if (lOrderedFarePanel != null)
                {
                    lOrderedFarePanel = null;
                }
                if (lFarePanel != null)
                {
                    lFarePanel = null;
                }
            }
        }

        private bool IsStudentPackage()
        {
            if ((fldTourID == 49/*SSBM*/) || (fldTourID == 67/*SSK*/) || (fldTourID == 46/*SSMC*/) || (fldTourID == 50/*SSG*/) ||
                (fldTourID == 115/*SSMCA*/) || (fldTourID == 123/*ED 05*/) || (fldTourID == 124/*ED 06*/)
                || (fldTourID == 119/*KTM*/) || (fldTourID == 120/*KTP*/) || (fldTourID == 121/*KTMP*/ || (fldTourID == 299/*LAK*/) || (fldTourID == 277/*LAKB*/) || (fldTourID == 208/*LAKB1*/))
                || (fldTourID == 132/*NR12*/) || (fldTourID == 133/*NR13*/) || (fldTourID == 160/*KM*/) || (fldTourID == 158/*YGBK-CH*/) || (fldTourID == 159/*BK-CH*/)
                || (fldTourID == 341/*GPKCL*/) || (fldTourID == 342/*KEP*/) || (fldTourID == 343/*KN*/) || (fldTourID == 344/*KPCN*/) || (fldTourID == 345/*KTMPJ*/))
            {
                return true;
            }
            return false;
        }

        private List<Get_SPL_Fare_Panel_spResult> GetDistinctFare(List<Get_SPL_Fare_Panel_spResult> lFarePanel)
        {
            List<Get_SPL_Fare_Panel_spResult> lReturnValue = new List<Get_SPL_Fare_Panel_spResult>();
            try
            {
                lFarePanel = lFarePanel.OrderBy(VehID => VehID.VehicleID).
                            ThenBy(PaxID => PaxID.PaxID).
                            ThenBy(FrmDate => FrmDate.FromDate).
                            ThenBy(SesID => SesID.SeasonID).
                            ThenBy(ParentID => ParentID.ParentID).
                            ThenBy(CatID => CatID.CategoryID).ToList();
                bool lIsExists = false;
                for (int Ctr = 0; Ctr < lFarePanel.Count; Ctr++)
                {
                    lIsExists = false;
                    for (int CompareCtr = 0; CompareCtr < lReturnValue.Count; CompareCtr++)
                    {
                        if ((lFarePanel[Ctr].VehicleID == lReturnValue[CompareCtr].VehicleID) &&
                            (lFarePanel[Ctr].PaxID == lReturnValue[CompareCtr].PaxID) &&
                            (lFarePanel[Ctr].SeasonID == lReturnValue[CompareCtr].SeasonID) &&
                            (lFarePanel[Ctr].CategoryID == lReturnValue[CompareCtr].CategoryID))
                        {
                            lIsExists = true;
                            break;
                        }
                    }
                    if (!lIsExists)
                    {
                        lReturnValue.Add(lFarePanel[Ctr]);
                    }
                }
                return lReturnValue;
            }
            finally
            {
                if (lReturnValue != null)
                {
                    lReturnValue = null;
                }
            }
        }

        private void ShowFarePanel(List<Get_SPL_Fare_Panel_spResult> lFarePanel, ArrayList lSeasonIDs)
        {

            //nare
            ArrayList SeasonIdList = new ArrayList();
            ArrayList NewSeasonIdList = new ArrayList();
            NewSeasonIdList = lSeasonIDs;
            int SeasonId = 0;
            List<Get_SPL_Fare_Panel_spResult> lFarePanelCopy = lFarePanel;


            for (int i = 0; i < lSeasonIDs.Count; i++)
            {
                pbSpecialTourSeasons pbs = (pbSpecialTourSeasons)lSeasonIDs[i];
                SeasonId = pbs.pbSeasonID;


                lFarePanel = lFarePanel.OrderBy(VehID => VehID.VehicleID).
                            //ThenBy(PaxID => PaxID.PaxID).
                            ThenBy(PaxID => Convert.ToInt32(PaxID.PaxType.Replace("Pax", ""))).
                            ThenBy(FrmDate => FrmDate.FromDate).
                            ThenBy(SesID => SesID.SeasonID).
                            ThenBy(ParentID => ParentID.ParentID).
                            ThenBy(CatID => CatID.CategoryID).ToList();

                lFarePanel = lFarePanel.Where(x => x.Fare > 0 && x.SeasonID == SeasonId).ToList();

                StringBuilder lFarePanelHTML = new StringBuilder(), lFareRow = new StringBuilder(), lFareRowWOVehicleType = new StringBuilder();
                bool lChildCategoryExist = false;
                for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                {
                    if (((pbSpecialTourSeasons)lSeasonIDs[SeasonCtr]).pbChildCategoryExist)
                    {
                        lChildCategoryExist = true;
                        break;
                    }
                }
                lFarePanelHTML.Append("<table  style=\"border:1px solid #BF3411;\"  cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" >");
                // ***** FOT GETTING HEADER *****
                lFarePanelHTML.Append("<tr>");
                lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", "Type of Vehicles").
                    Replace("#RowSpan", (lChildCategoryExist ? "3" : "2")).Replace("#ColSpan", "2").Replace("#valign", "middle").
                                Replace("#FareBGColor", "#d7e9ff").
                                Replace("#FareForeColor", "#DF411A").
                                Replace("#THClass", "orange"));
                string lCurrSeason = string.Empty;
                int lCurrSeasonID = 0, lSeasonCol = 0, ChildCtr = 0, ParentCtr = 0, lCurrentParentID = 0, lCurrentChildID = 0;
                #region "Season Header"
                //  for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[i];
                lSeasonCol = 0;
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    if (lCurrentSeason.pbChildCategoryExist && lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                    {
                        lSeasonCol += lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count;
                    }
                    else
                    {
                        lSeasonCol++;
                    }
                }
                lCurrSeason = lCurrentSeason.pbSeasonHead;
                lCurrSeasonID = lCurrentSeason.pbSeasonID;
                lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", lCurrSeason).
                    Replace("#RowSpan", "1").Replace("#ColSpan", lSeasonCol.ToString()).Replace("#valign", "middle").
                            Replace("#FareBGColor", "#d7e9ff").
                            Replace("#FareForeColor", "#DF411A").
                            Replace("#THClass", "orange"));
                /*lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", pdtFarePanel.Columns[FareHeadCtr].ColumnName).
                    Replace("#ColSpan", (lStandard + lDelux).ToString()).Replace("#valign", "middle"));*/
                // }
                lFarePanelHTML.Append("</tr>");
                #endregion
                #region "Parent Header"
                lFarePanelHTML.Append("<tr>");
                //  for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                //  pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[SeasonCtr];
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    lSeasonCol = 0;
                    lCurrSeason = lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryName;
                    if (lCurrentSeason.pbChildCategoryExist && lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                    {
                        lSeasonCol += lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count;
                    }
                    else
                    {
                        lSeasonCol++;
                    }
                    lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", lCurrSeason).
                        Replace("#RowSpan", (lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist ? "1" : (lChildCategoryExist ? "2" : "1"))).
                        Replace("#ColSpan", lSeasonCol.ToString()).Replace("#valign", "middle").Replace("#FareBGColor", "#d7e9ff").
                            Replace("#FareForeColor", "#DF411A").Replace("#THClass", "orange"));
                }
                // }
                lFarePanelHTML.Append("</tr>");
                #endregion
                //lFareRow.Append("<tr>");
                lFareRow.Append(fldTDFareTypeHead.Replace("#FareType", "#VehicleType").Replace("#rowspan", "#VehicleRowSpan"));
                lFareRow.Append(fldTDFareTypeHead);
                //lFareRowWOVehicleType.Append("<tr>");
                lFareRowWOVehicleType.Append(fldTDFareTypeHead);
                #region "Child Header"
                lFarePanelHTML.Append("<tr>");
                // for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                //  pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[SeasonCtr];

                int lCurrentSeasonID = lCurrentSeason.pbSeasonID;
                lCurrentParentID = 0;
                lCurrentChildID = 0;
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    lCurrentParentID = 0;
                    lCurrentChildID = 0;
                    lCurrSeason = lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryName;
                    lCurrentParentID = lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryID;
                    if (!lChildCategoryExist)
                    {
                        lFareRow.Append(fldTDSeasonValue.Replace("#SeasonValue",
                            "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                        lFareRowWOVehicleType.Append(fldTDSeasonValue.Replace("#SeasonValue",
                            "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                    }
                    else
                    {
                        if (lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                        {
                            for (ChildCtr = 0; ChildCtr < lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count; ChildCtr++)
                            {
                                lCurrentChildID = lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren[ChildCtr].pbChildID;
                                lCurrSeason = lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren[ChildCtr].pbCategoryName;
                                lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", lCurrSeason).
                                    Replace("#RowSpan", "1").Replace("#ColSpan", "1").Replace("#valign", "middle").Replace("#FareBGColor", "#d7e9ff").
                            Replace("#FareForeColor", "#DF411A").Replace("#THClass", "orange"));
                                lFareRow.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                    "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                                lFareRowWOVehicleType.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                    "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                            }
                        }
                        else
                        {
                            lFareRow.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                            lFareRowWOVehicleType.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                        }
                    }
                }
                // }
                lFarePanelHTML.Append("</tr>");
                lFarePanelHTML = lFarePanelHTML.Replace("<tr></tr>", "");
                //lFareRow.Append("</tr>");
                //lFareRowWOVehicleType.Append("</tr>");
                #endregion
                string lCurrVehichle = string.Empty, lCurrFare = string.Empty, lCurrPaxType = string.Empty, lPrevPaxType = string.Empty;
                int lCurrVehichleID = 0, lPrevVehichleID = 0, lCurrPaxID = 0, lPrevPaxID = 0, lVehicleRows = 0;
                lCurrentParentID = 0;
                lCurrentChildID = 0;
                StringBuilder lCurrRow = new StringBuilder();
                bool lIsEvenRow = true;
                string lCurrentBGColor = "#bcdbff", lCurrentForeColor = "black";
                for (int FareCtr = 0; FareCtr < lFarePanel.Count; FareCtr++)
                {
                    lCurrSeasonID = Convert.ToInt32(lFarePanel[FareCtr].SeasonID);
                    lCurrentParentID = lFarePanel[FareCtr].ParentID == 0 ? Convert.ToInt32(lFarePanel[FareCtr].CategoryID) : lFarePanel[FareCtr].ParentID;
                    lCurrentChildID = lFarePanel[FareCtr].ParentID > 0 ? Convert.ToInt32(lFarePanel[FareCtr].CategoryID) : 0;
                    lCurrVehichleID = lFarePanel[FareCtr].VehicleID;
                    lCurrPaxID = lFarePanel[FareCtr].PaxID;
                    lCurrVehichle = lFarePanel[FareCtr].VehicleName;
                    lCurrPaxType = lFarePanel[FareCtr].PaxType;
                    if (lPrevVehichleID == 0)
                    {
                        lPrevVehichleID = lFarePanel[FareCtr].VehicleID;
                        lPrevPaxID = lCurrPaxID;
                        lPrevPaxType = lCurrPaxType;
                        lCurrRow = new StringBuilder();
                        lCurrRow = lCurrRow.Append(lFareRow.ToString().Replace("#VehicleRowSpan", "#VehicleRowSpan" + lCurrVehichleID.ToString()));
                        lFarePanelHTML.Append("<tr>");
                        lVehicleRows += 1;
                    }
                    if (lCurrVehichleID == lPrevVehichleID)
                    {
                        if (lCurrPaxID == lPrevPaxID)
                        {
                            lCurrRow = lCurrRow.Replace("#VehicleType", lCurrVehichle).Replace("#FareType", lCurrPaxType).
                                Replace("#FareBGColor", lCurrentBGColor).
                                Replace("#FareForeColor", lCurrentForeColor).
                                Replace("#THClass", "orange");
                            lCurrRow = lCurrRow.Replace("#" + lCurrSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#",
                                ((((lFarePanel[FareCtr].IsBranch && fldPanelSection == PanelSectiontable.SEC_Branch) ||
                                (lFarePanel[FareCtr].IsAgent && fldPanelSection == PanelSectiontable.SEC_Agent) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_EndUser) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_Kerala))
                                &&
                                (Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare) > 0)) ?
                                "<span class=\"rupee\">`</span><span style=\"text-decoration:line-through;\">" + lFarePanel[FareCtr].Fare.ToString() +
                                "</span></br><span class=\"rupee\">`</span>" + lFarePanel[FareCtr].DiscountedFare.ToString() :
                                "<span class=\"rupee\">`</span> " + lFarePanel[FareCtr].Fare.ToString()));
                        }
                        else
                        {
                            lFarePanelHTML.Append(lCurrRow.ToString());
                            lFarePanelHTML.Append("</tr>");
                            if (!lIsEvenRow)
                            {
                                lCurrentBGColor = "#bcdbff";
                                lIsEvenRow = true;
                            }
                            else
                            {
                                lCurrentBGColor = "#d7e9ff";
                                lIsEvenRow = false;
                            }
                            lFarePanelHTML.Append("<tr>");
                            lPrevVehichleID = lFarePanel[FareCtr].VehicleID;
                            lCurrRow = new StringBuilder();
                            lCurrRow = lCurrRow.Append(lFareRowWOVehicleType.ToString());
                            lPrevPaxID = lCurrPaxID;
                            lCurrRow = lCurrRow.Replace("#VehicleType", lCurrVehichle).Replace("#FareType", lCurrPaxType).
                                Replace("#FareBGColor", lCurrentBGColor).
                                Replace("#FareForeColor", lCurrentForeColor).
                                Replace("#THClass", "orange");
                            lCurrRow = lCurrRow.Replace("#" + lCurrSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#",
                                ((((lFarePanel[FareCtr].IsBranch && fldPanelSection == PanelSectiontable.SEC_Branch) ||
                                (lFarePanel[FareCtr].IsAgent && fldPanelSection == PanelSectiontable.SEC_Agent) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_EndUser) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_Kerala))
                                &&
                                (Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare) > 0)) ?
                                "<span class=\"rupee\">`</span><span style=\"text-decoration:line-through;\">" + lFarePanel[FareCtr].Fare.ToString() +
                                "</span></br><span class=\"rupee\">`</span>" + lFarePanel[FareCtr].DiscountedFare.ToString() :
                                "<span class=\"rupee\">`</span> " + lFarePanel[FareCtr].Fare.ToString()));
                            lVehicleRows += 1;
                        }
                    }
                    else
                    {
                        lFarePanelHTML.Append(lCurrRow.ToString());
                        lFarePanelHTML.Append("</tr>");
                        lFarePanelHTML = lFarePanelHTML.Replace("#VehicleRowSpan" + lPrevVehichleID.ToString(), lVehicleRows.ToString());
                        lVehicleRows = 1;
                        if (!lIsEvenRow)
                        {
                            lCurrentBGColor = "#bcdbff";
                            lIsEvenRow = true;
                        }
                        else
                        {
                            lCurrentBGColor = "#d7e9ff";
                            lIsEvenRow = false;
                        }
                        lFarePanelHTML.Append("<tr>");
                        lPrevVehichleID = lFarePanel[FareCtr].VehicleID;
                        lCurrRow = new StringBuilder();
                        lCurrRow = lCurrRow.Append(lFareRow.ToString().Replace("#VehicleRowSpan", "#VehicleRowSpan" + lCurrVehichleID.ToString())).
                                Replace("#FareBGColor", lCurrentBGColor).
                                Replace("#FareForeColor", lCurrentForeColor);
                        lPrevPaxID = lCurrPaxID;
                        lPrevPaxType = lCurrPaxType;
                        lCurrRow = lCurrRow.Replace("#VehicleType", lCurrVehichle).Replace("#FareType", lCurrPaxType).
                                Replace("#FareBGColor", lCurrentBGColor).Replace("#FareForeColor", lCurrentForeColor).
                                Replace("#THClass", "orange");
                        lCurrRow = lCurrRow.Replace("#" + lCurrSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#",
                            ((((lFarePanel[FareCtr].IsBranch && fldPanelSection == PanelSectiontable.SEC_Branch) ||
                                (lFarePanel[FareCtr].IsAgent && fldPanelSection == PanelSectiontable.SEC_Agent) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_EndUser) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_Kerala))
                                &&
                                (Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare) > 0)) ?
                                "<span class=\"rupee\">`</span><span style=\"text-decoration:line-through;\">" + lFarePanel[FareCtr].Fare.ToString() +
                                "</span></br><span class=\"rupee\">`</span>" + lFarePanel[FareCtr].DiscountedFare.ToString() :
                                "<span class=\"rupee\">`</span> " + lFarePanel[FareCtr].Fare.ToString()));
                    }
                }
                lFarePanelHTML = lFarePanelHTML.Replace("#VehicleRowSpan" + lPrevVehichleID.ToString(), lVehicleRows.ToString());
                lFarePanelHTML.Append(lCurrRow.ToString());
                lFarePanelHTML.Append("</tr>");

                int lTotalColumns = 2;
                #region "Update Non-Fare Cells"
                //for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                //   pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[SeasonCtr];
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    if (lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                    {
                        for (ChildCtr = 0; ChildCtr < lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count; ChildCtr++)
                        {
                            lFarePanelHTML = lFarePanelHTML.Replace("#" + lCurrentSeason.pbSeasonID.ToString() + "-" +
                                lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryID.ToString() + "-" +
                                lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren[ChildCtr].pbChildID.ToString() + "#", "--");
                            lTotalColumns++;
                        }
                    }
                    else
                    {
                        lFarePanelHTML = lFarePanelHTML.Replace("#" + lCurrentSeason.pbSeasonID.ToString() + "-" +
                            lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryID.ToString() + "-0#", "--");
                        lTotalColumns++;

                    }
                }
                //}
                #endregion
                if (!ISQuery)
                {
                    if (CanBook)
                    {
                        //litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\">" + fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                        //lFarePanelHTML.Append(fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()));
                    }
                }
                else
                {
                    //litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\">" + fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                }
                if (!ISQuery)
                {
                    if (NotShowEBroucher)
                    {
                        litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\" width=\"100%\">" + fldEBroucher.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                        //lFarePanelHTML.Append(fldEBroucher.Replace("#ImgColSpan", lTotalColumns.ToString()));
                        //tdPanel.Width = "100%";
                        /*if (lblNote.Text == "")
                        {
                            tdPanel.Style.Add("Width", "100%");
                            tdPanel.Align = "right";
                        }*/
                    }
                }
                else
                {
                    //litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\" width=\"100%\">" + fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                }
                if (!fldShowNotes)
                {
                    //lFarePanelHTML.Append(fldEBroucher.Replace("#ImgColSpan", lTotalColumns.ToString()));
                    lblNote.Text = "";
                    /*tdPanel.Style.Add("Width", "100%");
                    tdPanel.Align = fldAlign;*/
                }
                if (fldPanelSection == PanelSectiontable.SEC_Branch)
                {
                    tdNotePanel.Attributes.Add("class", "col2");
                }
                else
                {
                    tdNotePanel.Attributes.Add("class", "row");
                }
                //nareen
                lFarePanelHTML.Append("</table>");
                // litSpecialTourFarePanel.Text = lFarePanelHTML.ToString();
                litSpecialTourFarePanel.Text = litSpecialTourFarePanel.Text + lFarePanelHTML.ToString();
                lFarePanel = lFarePanelCopy;
            }
            HiddenField hdnTourFare = this.Page.FindControl("hdnTourFare") as HiddenField;
            hdnTourFare.Value = hdnTourFare.Value + "<h3 class='title'>Tour <span>Price</span></h3><div class='tablewrap'>" + litSpecialTourFarePanel.Text + "</div>";
        }

        private void CheckNewSeasonHead(long? pSeasonID, int pParentID, int pChildID, string pSeasonHeading,
            string pParentName, string pChildName, bool pFareExist, ref ArrayList parrSeasonIDs)
        {
            if (parrSeasonIDs.Count == 0)
            {
                pbSpecialTourSeasons lTourSeason = new pbSpecialTourSeasons();
                lTourSeason.pbSeasonID = Convert.ToInt32(pSeasonID);
                lTourSeason.pbSeasonHead = pSeasonHeading;
                lTourSeason.pbSeasonCategory = new List<pbParentCategory>();
                pbParentCategory lParentCategory = new pbParentCategory();
                lParentCategory.pbCategoryID = (pParentID > 0 ? pParentID : pChildID);
                lParentCategory.pbCategoryName = (pParentID > 0 ? pParentName : pChildName);
                lParentCategory.pbFareExist = pFareExist;
                lParentCategory.pbChildren = new List<pbChildCategory>();
                if (pParentID > 0)
                {
                    pbChildCategory lChild = new pbChildCategory();
                    lChild.pbChildID = pChildID;
                    lChild.pbParentID = pParentID;
                    lChild.pbCategoryName = pChildName;
                    lParentCategory.pbFareExist = false;
                    lTourSeason.pbChildCategoryExist = true;
                    lParentCategory.pbChildCategoryExist = true;
                    lParentCategory.pbChildren.Add(lChild);
                }
                lTourSeason.pbSeasonCategory.Add(lParentCategory);
                parrSeasonIDs.Add(lTourSeason);
            }
            else
            {
                bool lExist = false, lParentExist = false, lChildExist = false;
                int lParentCtr = 0;
                for (int Ctr = 0; Ctr < parrSeasonIDs.Count; Ctr++)
                {
                    lParentExist = false; lChildExist = false; lParentCtr = 0;
                    pbSpecialTourSeasons lTourSeason = (pbSpecialTourSeasons)parrSeasonIDs[Ctr];
                    if (lTourSeason.pbSeasonID == Convert.ToInt32(pSeasonID))
                    {
                        for (int ParentCtr = 0; ParentCtr < lTourSeason.pbSeasonCategory.Count; ParentCtr++)
                        {
                            pbParentCategory lParentCategory = lTourSeason.pbSeasonCategory[ParentCtr];
                            if (pParentID == 0)
                            {
                                if (lParentCategory.pbCategoryID == pChildID)
                                {
                                    lParentCategory.pbFareExist = pFareExist;
                                    ((pbSpecialTourSeasons)parrSeasonIDs[Ctr]).pbSeasonCategory[ParentCtr] = lParentCategory;
                                    lParentExist = true;
                                    lChildExist = true;
                                    break;
                                }
                            }
                            else
                            {
                                if (lParentCategory.pbCategoryID == pParentID)
                                {
                                    lParentCtr = ParentCtr;
                                    lParentExist = true;
                                    for (int ChildCtr = 0; ChildCtr < lParentCategory.pbChildren.Count; ChildCtr++)
                                    {
                                        pbChildCategory lChildCategory = lParentCategory.pbChildren[ChildCtr];
                                        if (lChildCategory.pbChildID == pChildID)
                                        {
                                            lChildExist = true;
                                        }
                                    }
                                }

                            }
                        }
                        if (!lParentExist)
                        {
                            if (pParentID == 0)
                            {
                                pbParentCategory lParentCategory = new pbParentCategory();
                                lParentCategory.pbCategoryID = pChildID;
                                lParentCategory.pbCategoryName = pChildName;
                                lParentCategory.pbFareExist = pFareExist;
                                lParentCategory.pbChildren = new List<pbChildCategory>();
                                lTourSeason.pbSeasonCategory.Add(lParentCategory);
                            }
                            else
                            {
                                pbParentCategory lParentCategory = new pbParentCategory();
                                lParentCategory.pbCategoryID = pParentID;
                                lParentCategory.pbCategoryName = pParentName;
                                //lParentCategory.pbFareExist = false;
                                lParentCategory.pbChildren = new List<pbChildCategory>();
                                pbChildCategory lChildCategory = new pbChildCategory();
                                lChildCategory.pbChildID = pChildID;
                                lChildCategory.pbCategoryName = pChildName;
                                lChildCategory.pbParentID = pParentID;
                                lParentCategory.pbChildren.Add(lChildCategory);
                                lParentCategory.pbChildCategoryExist = true;
                                lTourSeason.pbSeasonCategory.Add(lParentCategory);
                                lTourSeason.pbChildCategoryExist = true;
                                parrSeasonIDs[Ctr] = lTourSeason;
                            }
                        }
                        else
                        {
                            if (!lChildExist)
                            {
                                pbChildCategory lChildCategory = new pbChildCategory();
                                pbParentCategory lParentCategory = lTourSeason.pbSeasonCategory[lParentCtr];
                                lChildCategory.pbChildID = pChildID;
                                lChildCategory.pbCategoryName = pChildName;
                                lChildCategory.pbParentID = pParentID;
                                lParentCategory.pbChildren.Add(lChildCategory);
                                lParentCategory.pbChildCategoryExist = true;
                                lTourSeason.pbSeasonCategory[lParentCtr] = lParentCategory;
                                lTourSeason.pbChildCategoryExist = true;
                                parrSeasonIDs[Ctr] = lTourSeason;
                            }
                        }
                        if (!lTourSeason.pbSeasonHead.Trim().Contains(pSeasonHeading.Trim()))
                        {
                            lTourSeason.pbSeasonHead += ", " + pSeasonHeading;
                            parrSeasonIDs[Ctr] = lTourSeason;
                        }
                        lExist = true;
                        break;
                    }
                }
                if (!lExist)
                {
                    pbSpecialTourSeasons lTourSeason = new pbSpecialTourSeasons();
                    lTourSeason.pbSeasonID = Convert.ToInt32(pSeasonID);
                    lTourSeason.pbSeasonHead = pSeasonHeading;
                    lTourSeason.pbSeasonCategory = new List<pbParentCategory>();
                    pbParentCategory lParentCategory = new pbParentCategory();
                    lParentCategory.pbCategoryID = (pParentID > 0 ? pParentID : pChildID);
                    lParentCategory.pbCategoryName = (pParentID > 0 ? pParentName : pChildName);
                    lParentCategory.pbFareExist = pFareExist;
                    lParentCategory.pbChildren = new List<pbChildCategory>();
                    if (pParentID > 0)
                    {
                        pbChildCategory lChild = new pbChildCategory();
                        lChild.pbChildID = pChildID;
                        lChild.pbParentID = pParentID;
                        lChild.pbCategoryName = pChildName;
                        lParentCategory.pbFareExist = false;
                        lTourSeason.pbChildCategoryExist = true;
                        lParentCategory.pbChildCategoryExist = true;
                        lParentCategory.pbChildren.Add(lChild);
                    }
                    lTourSeason.pbSeasonCategory.Add(lParentCategory);
                    parrSeasonIDs.Add(lTourSeason);
                }
            }
        }

        private string GetNewColumnName(ArrayList lSeasonIDs, int pSeasonID)
        {
            string lColumnName = string.Empty;
            for (int Ctr = 0; Ctr < lSeasonIDs.Count; Ctr++)
            {
                if (pSeasonID == ((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonID)
                {
                    lColumnName = ((pbSplTourSeasons)lSeasonIDs[Ctr]).pbSeasonHead;
                    break;
                }
            }
            return lColumnName;
        }

        private void ShowNewFarePanel(DataTable pdtFarePanel, ArrayList pSeasonIDs)
        {
            StringBuilder lFarePanel = new StringBuilder();
            lFarePanel.Append("<table cellpadding=\"0\" cellspacing=\"1\" >");
            int lStandard = 0, lDelux = 0;
            for (int Ctr = 0; Ctr < pdtFarePanel.Rows.Count; Ctr++)
            {
                if (Convert.ToString(pdtFarePanel.Rows[Ctr]["CategoryName"]) != "Deluxe")
                {
                    lStandard = 1;
                }
                if (Convert.ToString(pdtFarePanel.Rows[Ctr]["CategoryName"]) != "Standard")
                {
                    lDelux = 1;
                }
            }
            // ***** FOT GETTING HEADER *****
            lFarePanel.Append("<tr>");
            lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", "Type of Vehicles").Replace("#RowSpan", "2").Replace("#ColSpan", "2").Replace("#valign", "middle"));
            for (int HeadCtr = 7; HeadCtr < pdtFarePanel.Columns.Count; HeadCtr++)
            {
                lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", pdtFarePanel.Columns[HeadCtr].ColumnName).
                    Replace("#ColSpan", (lStandard + lDelux).ToString()).Replace("#valign", "middle"));
            }
            lFarePanel.Append("</tr>");
            //*****FOR GETTING SUB-HEADER
            lFarePanel.Append("<tr>");
            for (int HeadCtr = 7; HeadCtr < pdtFarePanel.Columns.Count; HeadCtr++)
            {
                if (lStandard > 0)
                {
                    lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", "Standard (Non-AC)").
                        Replace("#ColSpan", "0"));
                }
                if (lDelux > 0)
                {
                    lFarePanel.Append(fldTDSeasonHead.Replace("#SeasonHead", "Deluxe (AC)").
                       Replace("#ColSpan", "0"));
                }
            }
            lFarePanel.Append("</tr>");

            int lCurrPaxID = 0, lPrevPaxID = 0, lPrevVehicleID = 0, lCurrVehicleID = 0, lCurrRowSpan = 1;
            for (int Ctr = 0; Ctr < pdtFarePanel.Rows.Count; Ctr++)
            {
                lCurrPaxID = Convert.ToInt32(pdtFarePanel.Rows[Ctr]["PaxID"]);
                lCurrVehicleID = Convert.ToInt32(pdtFarePanel.Rows[Ctr]["VehicleID"]);
                if (lPrevPaxID == 0)
                {
                    lPrevPaxID = lCurrPaxID;
                    lPrevVehicleID = lCurrVehicleID;
                    lFarePanel.Append("<tr>");
                    lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["VehicleName"])).
                        Replace("#rowspan", "#rowspan" + lPrevVehicleID.ToString()));
                    lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["PaxType"])));
                }

                if (lPrevPaxID == lCurrPaxID)
                {
                    lFarePanel.Append(fldTDSeasonValue.Replace("#SeasonValue",
                        pdtFarePanel.Rows[Ctr][GetColumnName(pSeasonIDs, Convert.ToInt32(pdtFarePanel.Rows[Ctr]["SeasonID"]))].ToString() + ".00"));
                }
                else
                {
                    lFarePanel.Append("</tr>");
                    lFarePanel.Append("<tr>");
                    if (lPrevVehicleID != lCurrVehicleID)
                    {
                        lFarePanel.Replace("#rowspan" + lPrevVehicleID.ToString(), lCurrRowSpan.ToString());
                        lCurrRowSpan = 1;
                        lPrevVehicleID = lCurrVehicleID;
                    }
                    else
                    {
                        lCurrRowSpan++;
                    }
                    if (lCurrRowSpan == 1)
                    {
                        lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["VehicleName"])).
                            Replace("#rowspan", "#rowspan" + lPrevVehicleID.ToString()));
                    }
                    lFarePanel.Append(fldTDFareTypeHead.Replace("#FareType", Convert.ToString(pdtFarePanel.Rows[Ctr]["PaxType"])));
                    lFarePanel.Append(fldTDSeasonValue.Replace("#SeasonValue",
                        pdtFarePanel.Rows[Ctr][GetColumnName(pSeasonIDs, Convert.ToInt32(pdtFarePanel.Rows[Ctr]["SeasonID"]))].ToString() + ".00"));
                    lPrevPaxID = lCurrPaxID;
                }
            }
            if (lCurrRowSpan > 1)
            {
                lFarePanel.Replace("#rowspan" + lPrevVehicleID.ToString(), lCurrRowSpan.ToString());
            }
            lFarePanel.Append("</tr>");
            if (!ISQuery)
            {
                if (CanBook)
                {
                    litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\">" + fldBookNow.Replace("#ImgColSpan", ((pSeasonIDs.Count * 2) + 2).ToString()) + "</table>";
                    //lFarePanel.Append(fldBookNow.Replace("#ImgColSpan", ((pSeasonIDs.Count * 2) + 2).ToString()));
                }
            }
            else
            {
                litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\">" + fldBookNow.Replace("#ImgColSpan", ((pSeasonIDs.Count * 2) + 2).ToString()) + "</table>";
            }
            if (NotShowEBroucher)
            {
                lFarePanel.Append(fldEBroucher.Replace("#ImgColSpan", ((pSeasonIDs.Count * 2) + 2).ToString()));
                //tdPanel.Width = "100%";
                /*tdPanel.Style.Add("Width", "100%");
                /tdPanel.Align = "right";*/
            }

            lFarePanel.Append("</table>");
            litSpecialTourFarePanel.Text = lFarePanel.ToString();

            HiddenField hdnTourFare = this.Page.FindControl("hdnTourFare") as HiddenField;
            hdnTourFare.Value = hdnTourFare.Value + "<h3 class='title'>Tour <span>Price</span></h3><div class='tablewrap'>" + lFarePanel.ToString() + "</div>";
        }

        private void ShowFarePanel_EndUser(List<Get_SPL_Fare_Panel_spResult> lFarePanel, ArrayList lSeasonIDs)
        {
            //Narendra
            ArrayList SeasonIdList = new ArrayList();
            ArrayList NewSeasonIdList = new ArrayList();
            NewSeasonIdList = lSeasonIDs;
            int SeasonId = 0;
            List<Get_SPL_Fare_Panel_spResult> lFarePanelCopy = lFarePanel;


            for (int i = 0; i < lSeasonIDs.Count; i++)
            {
                pbSpecialTourSeasons pbs = (pbSpecialTourSeasons)lSeasonIDs[i];
                SeasonId = pbs.pbSeasonID;

                lFarePanel = lFarePanel.Where(x => x.Fare > 0 && x.SeasonID == SeasonId).ToList();

                lFarePanel = lFarePanel.OrderBy(VehID => VehID.VehicleID).
                            //ThenBy(PaxID => PaxID.PaxID).
                            ThenBy(PaxID => Convert.ToInt32(PaxID.PaxType.Replace("Pax", ""))).
                            ThenBy(FrmDate => FrmDate.FromDate).
                            ThenBy(SesID => SesID.SeasonID).
                            ThenBy(ParentID => ParentID.ParentID).
                            ThenBy(CatID => CatID.CategoryID).ToList();





                StringBuilder lFarePanelHTML = new StringBuilder(), lFareRow = new StringBuilder(), lFareRowWOVehicleType = new StringBuilder();
                bool lChildCategoryExist = false;

                for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                {
                    if (((pbSpecialTourSeasons)lSeasonIDs[SeasonCtr]).pbChildCategoryExist)
                    {
                        lChildCategoryExist = true;
                        break;
                    }
                }


                lFarePanelHTML.Append("<table  class=\"table-bordered\"  cellpadding=\"0\" cellspacing=\"1\" width=\"100%\" >");

                // ***** FOT GETTING HEADER *****
                lFarePanelHTML.Append("<tr>");
                lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", "Type of Vehicles").
                    Replace("#RowSpan", (lChildCategoryExist ? "3" : "2")).Replace("#ColSpan", "2").Replace("#valign", "middle").
                                Replace("#FareBGColor", "").
                                Replace("#FareForeColor", "").
                                Replace("#THClass", ""));
                string lCurrSeason = string.Empty;
                int lCurrSeasonID = 0, lSeasonCol = 0, ChildCtr = 0, ParentCtr = 0, lCurrentParentID = 0, lCurrentChildID = 0;
                #region "Season Header"
                //  for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[i];
                lSeasonCol = 0;
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    if (lCurrentSeason.pbChildCategoryExist && lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                    {
                        lSeasonCol += lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count;
                    }
                    else
                    {
                        lSeasonCol++;
                    }
                }
                lCurrSeason = lCurrentSeason.pbSeasonHead;
                lCurrSeasonID = lCurrentSeason.pbSeasonID;
                lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", lCurrSeason).
                    Replace("#RowSpan", "1").Replace("#ColSpan", lSeasonCol.ToString()).Replace("#valign", "middle").
                            Replace("#FareBGColor", "").
                            Replace("#FareForeColor", "").
                            Replace("#THClass", ""));
                /*lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", pdtFarePanel.Columns[FareHeadCtr].ColumnName).
                    Replace("#ColSpan", (lStandard + lDelux).ToString()).Replace("#valign", "middle"));*/
                //  }
                lFarePanelHTML.Append("</tr>");
                #endregion

                #region "Parent Header"
                lFarePanelHTML.Append("<tr>");

                // for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                //  pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[SeasonCtr];
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    lSeasonCol = 0;
                    lCurrSeason = lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryName;
                    if (lCurrentSeason.pbChildCategoryExist && lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                    {
                        lSeasonCol += lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count;
                    }
                    else
                    {
                        lSeasonCol++;
                    }
                    lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", lCurrSeason).
                        Replace("#RowSpan", (lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist ? "1" : (lChildCategoryExist ? "2" : "1"))).
                        Replace("#ColSpan", lSeasonCol.ToString()).Replace("#valign", "middle").Replace("#FareBGColor", "").
                            Replace("#FareForeColor", "").Replace("#THClass", "th2"));
                }
                //}
                lFarePanelHTML.Append("</tr>");
                #endregion
                //lFareRow.Append("<tr>");
                lFareRow.Append(fldTDFareTypeHead.Replace("#FareType", "#VehicleType").Replace("#rowspan", "#VehicleRowSpan"));
                lFareRow.Append(fldTDFareTypeHead);
                //lFareRowWOVehicleType.Append("<tr>");
                lFareRowWOVehicleType.Append(fldTDFareTypeHead);
                #region "Child Header"
                lFarePanelHTML.Append("<tr>");
                // for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                // {
                //   pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[SeasonCtr];
                int lCurrentSeasonID = lCurrentSeason.pbSeasonID;
                lCurrentParentID = 0;
                lCurrentChildID = 0;
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    lCurrentParentID = 0;
                    lCurrentChildID = 0;
                    lCurrSeason = lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryName;
                    lCurrentParentID = lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryID;
                    if (!lChildCategoryExist)
                    {
                        lFareRow.Append(fldTDSeasonValue.Replace("#SeasonValue",
                            "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                        lFareRowWOVehicleType.Append(fldTDSeasonValue.Replace("#SeasonValue",
                            "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                    }
                    else
                    {
                        if (lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                        {
                            for (ChildCtr = 0; ChildCtr < lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count; ChildCtr++)
                            {
                                lCurrentChildID = lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren[ChildCtr].pbChildID;
                                lCurrSeason = lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren[ChildCtr].pbCategoryName;
                                lFarePanelHTML.Append(fldTDSeasonHead.Replace("#SeasonHead", lCurrSeason).
                                    Replace("#RowSpan", "1").Replace("#ColSpan", "1").Replace("#valign", "middle").Replace("#FareBGColor", "").
                            Replace("#FareForeColor", "").Replace("#THClass", "th2"));
                                lFareRow.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                    "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                                lFareRowWOVehicleType.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                    "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                            }
                        }
                        else
                        {
                            lFareRow.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                            lFareRowWOVehicleType.Append(fldTDSeasonValue.Replace("#SeasonValue",
                                "#" + lCurrentSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#"));
                        }
                    }
                }
                //}
                lFarePanelHTML.Append("</tr>");
                lFarePanelHTML = lFarePanelHTML.Replace("<tr></tr>", "");
                //lFareRow.Append("</tr>");
                //lFareRowWOVehicleType.Append("</tr>");
                #endregion
                string lCurrVehichle = string.Empty, lCurrFare = string.Empty, lCurrPaxType = string.Empty, lPrevPaxType = string.Empty;
                int lCurrVehichleID = 0, lPrevVehichleID = 0, lCurrPaxID = 0, lPrevPaxID = 0, lVehicleRows = 0;
                lCurrentParentID = 0;
                lCurrentChildID = 0;
                StringBuilder lCurrRow = new StringBuilder();
                bool lIsEvenRow = true;
                string lCurrentBGColor = "", lCurrentForeColor = "";
                for (int FareCtr = 0; FareCtr < lFarePanel.Count; FareCtr++)
                {
                    lCurrSeasonID = Convert.ToInt32(lFarePanel[FareCtr].SeasonID);
                    lCurrentParentID = lFarePanel[FareCtr].ParentID == 0 ? Convert.ToInt32(lFarePanel[FareCtr].CategoryID) : lFarePanel[FareCtr].ParentID;
                    lCurrentChildID = lFarePanel[FareCtr].ParentID > 0 ? Convert.ToInt32(lFarePanel[FareCtr].CategoryID) : 0;
                    lCurrVehichleID = lFarePanel[FareCtr].VehicleID;
                    lCurrPaxID = lFarePanel[FareCtr].PaxID;
                    lCurrVehichle = lFarePanel[FareCtr].VehicleName;
                    lCurrPaxType = lFarePanel[FareCtr].PaxType;
                    if (lPrevVehichleID == 0)
                    {
                        lPrevVehichleID = lFarePanel[FareCtr].VehicleID;
                        lPrevPaxID = lCurrPaxID;
                        lPrevPaxType = lCurrPaxType;
                        lCurrRow = new StringBuilder();
                        lCurrRow = lCurrRow.Append(lFareRow.ToString().Replace("#VehicleRowSpan", "#VehicleRowSpan" + lCurrVehichleID.ToString()));
                        lFarePanelHTML.Append("<tr>");
                        lVehicleRows += 1;
                    }
                    if (lCurrVehichleID == lPrevVehichleID)
                    {
                        if (lCurrPaxID == lPrevPaxID)
                        {
                            lCurrRow = lCurrRow.Replace("#VehicleType", lCurrVehichle).Replace("#FareType", lCurrPaxType).
                                Replace("#FareBGColor", "").
                                Replace("#FareForeColor", "").
                                Replace("#THClass", "");
                            lCurrRow = lCurrRow.Replace("#" + lCurrSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#",
                                ((((lFarePanel[FareCtr].IsBranch && fldPanelSection == PanelSectiontable.SEC_Branch) ||
                                (lFarePanel[FareCtr].IsAgent && fldPanelSection == PanelSectiontable.SEC_Agent) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_EndUser) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_Kerala))
                                &&
                                (Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare) > 0)) ?
                                "<i class=\"fa fa-rupee\"></i><span style=\"text-decoration:line-through;\">" + lFarePanel[FareCtr].Fare.ToString() +
                                "</span></br><i class=\"fa fa-rupee\"></i>" + Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare).ToString("#,##0") + "/-" :
                                "<i class=\"fa fa-rupee\"></i>" + Convert.ToDecimal(lFarePanel[FareCtr].Fare).ToString("#,##0") + "/-"));
                        }
                        else
                        {
                            lFarePanelHTML.Append(lCurrRow.ToString());
                            lFarePanelHTML.Append("</tr>");
                            if (!lIsEvenRow)
                            {
                                lCurrentBGColor = "";
                                lIsEvenRow = true;
                            }
                            else
                            {
                                lCurrentBGColor = "";
                                lIsEvenRow = false;
                            }
                            lFarePanelHTML.Append("<tr>");
                            lPrevVehichleID = lFarePanel[FareCtr].VehicleID;
                            lCurrRow = new StringBuilder();
                            lCurrRow = lCurrRow.Append(lFareRowWOVehicleType.ToString());
                            lPrevPaxID = lCurrPaxID;
                            lCurrRow = lCurrRow.Replace("#VehicleType", lCurrVehichle).Replace("#FareType", lCurrPaxType).
                                Replace("#FareBGColor", "").
                                Replace("#FareForeColor", "").
                                Replace("#THClass", "");
                            lCurrRow = lCurrRow.Replace("#" + lCurrSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#",
                                ((((lFarePanel[FareCtr].IsBranch && fldPanelSection == PanelSectiontable.SEC_Branch) ||
                                (lFarePanel[FareCtr].IsAgent && fldPanelSection == PanelSectiontable.SEC_Agent) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_EndUser) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_Kerala))
                                &&
                                (Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare) > 0)) ?
                                "<i class=\"fa fa-rupee\"></i><span style=\"text-decoration:line-through;\">" + lFarePanel[FareCtr].Fare.ToString() +
                                "</span></br><i class=\"fa fa-rupee\"></i>" + Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare).ToString("#,##0") + "/-" :
                                "<i class=\"fa fa-rupee\"></i>" + Convert.ToDecimal(lFarePanel[FareCtr].Fare).ToString("#,##0") + "/-"));
                            lVehicleRows += 1;
                        }
                    }
                    else
                    {
                        lFarePanelHTML.Append(lCurrRow.ToString());
                        lFarePanelHTML.Append("</tr>");
                        lFarePanelHTML = lFarePanelHTML.Replace("#VehicleRowSpan" + lPrevVehichleID.ToString(), lVehicleRows.ToString());
                        lVehicleRows = 1;
                        if (!lIsEvenRow)
                        {
                            lCurrentBGColor = "";
                            lIsEvenRow = true;
                        }
                        else
                        {
                            lCurrentBGColor = "";
                            lIsEvenRow = false;
                        }
                        lFarePanelHTML.Append("<tr>");
                        lPrevVehichleID = lFarePanel[FareCtr].VehicleID;
                        lCurrRow = new StringBuilder();
                        lCurrRow = lCurrRow.Append(lFareRow.ToString().Replace("#VehicleRowSpan", "#VehicleRowSpan" + lCurrVehichleID.ToString())).
                                Replace("#FareBGColor", lCurrentBGColor).
                                Replace("#FareForeColor", lCurrentForeColor);
                        lPrevPaxID = lCurrPaxID;
                        lPrevPaxType = lCurrPaxType;
                        lCurrRow = lCurrRow.Replace("#VehicleType", lCurrVehichle).Replace("#FareType", lCurrPaxType).
                                Replace("#FareBGColor", "").Replace("#FareForeColor", "").
                                Replace("#THClass", "");
                        lCurrRow = lCurrRow.Replace("#" + lCurrSeasonID.ToString() + "-" + lCurrentParentID.ToString() + "-" + lCurrentChildID.ToString() + "#",
                            ((((lFarePanel[FareCtr].IsBranch && fldPanelSection == PanelSectiontable.SEC_Branch) ||
                                (lFarePanel[FareCtr].IsAgent && fldPanelSection == PanelSectiontable.SEC_Agent) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_EndUser) ||
                                (lFarePanel[FareCtr].IsOnline && fldPanelSection == PanelSectiontable.SEC_Kerala))
                                &&
                                (Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare) > 0)) ?
                                "<i class=\"fa fa-rupee\"></i><span style=\"text-decoration:line-through;\">" + lFarePanel[FareCtr].Fare.ToString() +
                                "</span></br><i class=\"fa fa-rupee\"></i>" + Convert.ToDecimal(lFarePanel[FareCtr].DiscountedFare).ToString("#,##0") + "/-" :
                                "<i class=\"fa fa-rupee\"></i>" + Convert.ToDecimal(lFarePanel[FareCtr].Fare).ToString("#,##0") + "/-"));
                    }
                }
                lFarePanelHTML = lFarePanelHTML.Replace("#VehicleRowSpan" + lPrevVehichleID.ToString(), lVehicleRows.ToString());
                lFarePanelHTML.Append(lCurrRow.ToString());
                lFarePanelHTML.Append("</tr>");

                int lTotalColumns = 2;
                #region "Update Non-Fare Cells"
                // for (int SeasonCtr = 0; SeasonCtr < lSeasonIDs.Count; SeasonCtr++)
                //{
                //   pbSpecialTourSeasons lCurrentSeason = (pbSpecialTourSeasons)lSeasonIDs[SeasonCtr];
                for (ParentCtr = 0; ParentCtr < lCurrentSeason.pbSeasonCategory.Count; ParentCtr++)
                {
                    if (lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildCategoryExist)
                    {
                        for (ChildCtr = 0; ChildCtr < lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren.Count; ChildCtr++)
                        {
                            lFarePanelHTML = lFarePanelHTML.Replace("#" + lCurrentSeason.pbSeasonID.ToString() + "-" +
                                lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryID.ToString() + "-" +
                                lCurrentSeason.pbSeasonCategory[ParentCtr].pbChildren[ChildCtr].pbChildID.ToString() + "#", "--");
                            lTotalColumns++;
                        }
                    }
                    else
                    {
                        lFarePanelHTML = lFarePanelHTML.Replace("#" + lCurrentSeason.pbSeasonID.ToString() + "-" +
                            lCurrentSeason.pbSeasonCategory[ParentCtr].pbCategoryID.ToString() + "-0#", "--");
                        lTotalColumns++;

                    }
                }
                // }
                #endregion
                if (!ISQuery)
                {
                    if (CanBook)
                    {
                        //litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\">" + fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                        //lFarePanelHTML.Append(fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()));
                    }
                }
                else
                {
                    //litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\">" + fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                }
                if (!ISQuery)
                {
                    if (NotShowEBroucher)
                    {
                        litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\" width=\"100%\">" + fldEBroucher.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                        //lFarePanelHTML.Append(fldEBroucher.Replace("#ImgColSpan", lTotalColumns.ToString()));
                        //tdPanel.Width = "100%";
                        /*if (lblNote.Text == "")
                        {
                            tdPanel.Style.Add("Width", "100%");
                            tdPanel.Align = "right";
                        }*/
                    }
                }
                else
                {
                    //litSplTourFarePanelBook.Text = "<table cellpadding=\"0\" cellspacing=\"0\" border=\"0\" class=\"tb10\" width=\"100%\">" + fldBookNow.Replace("#ImgColSpan", lTotalColumns.ToString()) + "</table>";
                }
                if (!fldShowNotes)
                {
                    //lFarePanelHTML.Append(fldEBroucher.Replace("#ImgColSpan", lTotalColumns.ToString()));
                    lblNote.Text = "";
                    /*tdPanel.Style.Add("Width", "100%");
                    tdPanel.Align = fldAlign;*/
                }
                if (fldPanelSection == PanelSectiontable.SEC_Branch)
                {
                    tdNotePanel.Attributes.Add("class", "col2");
                }
                else
                {
                    tdNotePanel.Attributes.Add("class", "row");
                }
                lFarePanelHTML.Append("</table>");
                //litSpecialTourFarePanel.Text = lFarePanelHTML.ToString();
                litSpecialTourFarePanel.Text = litSpecialTourFarePanel.Text + lFarePanelHTML.ToString();
                lFarePanel = lFarePanelCopy;
            }
            HiddenField hdnTourFare = this.Page.FindControl("hdnTourFare") as HiddenField;
            hdnTourFare.Value = hdnTourFare.Value + "<h3 class='title'>Tour <span>Price</span></h3><div class='tablewrap'>" + litSpecialTourFarePanel.Text + "</div>";
        }

        #endregion
    }
}