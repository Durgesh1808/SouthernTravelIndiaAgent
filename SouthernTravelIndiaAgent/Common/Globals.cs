using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent.Common
{
    /// <summary>
    /// // This class contains global variables and utility methods used throughout the application.
    /// </summary>
    public class Globals
    {
        public Globals()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        /// <summary>
        /// /// This property holds the agent's address.
        /// </summary>
        static string _AgentAddress;


        /// <summary>
        /// /// This property allows setting and getting the agent's address.
        /// </summary>
        public static string AgentAddress
        {
            get
            {
                return _AgentAddress;
            }
            set
            {
                _AgentAddress = value;
            }
        }
        static string _AgentPhone;


        /// <summary>
        /// /// This property holds the agent's phone number.
        /// </summary>
        public static string AgentPhone
        {
            get
            {
                return _AgentPhone;
            }
            set
            {
                _AgentPhone = value;
            }
        }


        /// <summary>
        /// /// This property holds the agent's email address.
        /// </summary>
        /// <param name="oDropDown"></param>
        /// <param name="iType"></param>
        /// <param name="bShowDefault"></param>

        public static void LoadDropDownDay(ref System.Web.UI.WebControls.DropDownList oDropDown, int iType, bool bShowDefault)
        {
            if (iType != -1)
            {
                for (int i = 1; i <= 31; i++)
                {
                    oDropDown.Items.Add(i.ToString());
                }
            }

            if (bShowDefault)
                oDropDown.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" DD ", "0"));
        }


        /// <summary>
        /// /// This property holds the agent's email address.
        /// </summary>
        public static string fldDateSeparator
        {
            get
            {
                return System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator;
            }
        }


        /// <summary>
        /// /// This method loads the month dropdown with month names and values.
        /// </summary>
        /// <param name="oDropDown"></param>
        /// <param name="iType"></param>
        /// <param name="bShowDefault"></param>
        public static void LoadDropDownMonth(ref System.Web.UI.WebControls.DropDownList oDropDown, int iType, bool bShowDefault)
        {

            if (bShowDefault)
                oDropDown.Items.Insert(0, new System.Web.UI.WebControls.ListItem(" MM ", "0"));
            oDropDown.Items.Insert(1, new System.Web.UI.WebControls.ListItem("Jan", "1"));
            oDropDown.Items.Insert(2, new System.Web.UI.WebControls.ListItem("Feb", "2"));
            oDropDown.Items.Insert(3, new System.Web.UI.WebControls.ListItem("Mar", "3"));
            oDropDown.Items.Insert(4, new System.Web.UI.WebControls.ListItem("Apr", "4"));
            oDropDown.Items.Insert(5, new System.Web.UI.WebControls.ListItem("May", "5"));
            oDropDown.Items.Insert(6, new System.Web.UI.WebControls.ListItem("Jun", "6"));
            oDropDown.Items.Insert(7, new System.Web.UI.WebControls.ListItem("Jul", "7"));
            oDropDown.Items.Insert(8, new System.Web.UI.WebControls.ListItem("Aug", "8"));
            oDropDown.Items.Insert(9, new System.Web.UI.WebControls.ListItem("Sep", "9"));
            oDropDown.Items.Insert(10, new System.Web.UI.WebControls.ListItem("Oct", "10"));
            oDropDown.Items.Insert(11, new System.Web.UI.WebControls.ListItem("Nov", "11"));
            oDropDown.Items.Insert(12, new System.Web.UI.WebControls.ListItem("Dec", "12"));
        }


        /// <summary>
        /// /// This method checks if the DataTable has data and updates the DataGrid and Label accordingly.
        /// </summary>
        /// <param name="oDataGrid"></param>
        /// <param name="dt"></param>
        /// <param name="oLbl"></param>
        public static void CheckData(ref System.Web.UI.WebControls.DataGrid oDataGrid, DataTable dt, ref System.Web.UI.WebControls.Label oLbl)

        {
            oLbl.Text = "";
            if (dt == null || dt.Rows.Count == 0)
            {
                oLbl.Text = "No Data Found !";
                oDataGrid.Visible = false;
            }
            else
            {
                oDataGrid.Visible = true;
                try
                {

                    oDataGrid.DataBind();
                }

                catch
                {
                    try
                    {
                        oDataGrid.CurrentPageIndex = 0;
                        oDataGrid.DataBind();
                    }
                    catch
                    {
                        oLbl.Text = "No data for selection";
                    }
                    oLbl.Text = "";
                }
            }
        }


        /// <summary>
        /// /// This method checks if the DataTable has data and updates the DataGrid and Label accordingly.
        /// </summary>
        /// <param name="oDataGrid"></param>
        /// <param name="dt"></param>
        /// <param name="oLbl"></param>
        public static void CheckData1(ref System.Web.UI.WebControls.DataGrid oDataGrid, DataTable dt, ref System.Web.UI.WebControls.Label oLbl)
        {
            oLbl.Text = "";
            if (Convert.ToInt32(dt.Rows.Count) == 0)
            {
                oLbl.Text = "No Data Found for the Search Criteria you Entered";
                oDataGrid.Visible = false;
            }
            else
            {
                oDataGrid.Visible = true;
                try
                {
                    oDataGrid.DataBind();
                }
                catch
                {
                    try
                    {
                        oDataGrid.CurrentPageIndex = 0;
                        oDataGrid.DataBind();
                    }
                    catch
                    {
                        oLbl.Text = "No data for selection";
                    }
                    oLbl.Text = "";
                }
            }
        }
        /// <summary>
        /// Clear Controls of the Request form
        /// </summary>
        /// <param name="Parent"> Form </param>
        public static void ClearControls(Control Parent)
        {
            foreach (Control c in Parent.Controls)
            {
                // Check and see if it's a textbox
                if ((c.GetType() == typeof(TextBox)))
                {
                    // Since its a textbox clear out the text	
                    ((TextBox)(c)).Text = "";
                }
                // Check and see if it's a CheckBox
                if ((c.GetType() == typeof(CheckBox)))
                {
                    // Since its a CheckBox, Uncheck the CheckBox	
                    ((CheckBox)(c)).Checked = false;
                }
                // Check and see if it's a RadioButton
                if ((c.GetType() == typeof(RadioButton)))
                {
                    // Since its a RadioButton clear out the RadioButton	
                    ((RadioButton)(c)).Checked = false;
                }
                // Check and see if it's a HtmlInputText
                if ((c.GetType() == typeof(HtmlInputText)))
                {
                    // Since its a textbox clear out the text	
                    ((HtmlInputText)(c)).Value = "";
                }
                if ((c.GetType() == typeof(DropDownList)))
                {
                    // Since its a textbox clear out the text	
                    ((DropDownList)(c)).SelectedIndex = -1;
                }
                // Now we need to call itself (recursive) because
                // all items (Panel, GroupBox, etc) is a container
                // so we need to check all containers for any
                // textboxes so we can clear them
                if (c.HasControls())
                {
                    ClearControls(c);
                }
            }
        }

        /// <summary>
        /// // This method fills the labels in the given control with text from the provided DataSet.
        /// </summary>
        /// <param name="pParent"></param>
        /// <param name="pDsLanguage"></param>
        public static void FillLanguageLabels(Control pParent, DataSet pDsLanguage)
        {
            if (pDsLanguage != null && pDsLanguage.Tables.Count > 0 &&
                pDsLanguage.Tables[0] != null && pDsLanguage.Tables[0].Rows.Count > 0)
            {
                Label ctrl;
                string les = "0", hig = "0", ctrlName;
                DataRow[] drleast = pDsLanguage.Tables[0].Select("LabelId = labelId", "LabelId Asc");
                if (drleast != null && drleast.Length > 0)
                    les = drleast[0][0].ToString();
                DataRow[] drhighest = pDsLanguage.Tables[0].Select("LabelId = labelId", "LabelId Desc");
                if (drhighest != null && drhighest.Length > 0)
                    hig = drhighest[0][0].ToString();
                for (int i = Convert.ToInt32(les); i <= Convert.ToInt32(hig); i++)
                {
                    ctrlName = "Label" + (i);
                    ctrl = (Label)pParent.FindControl(ctrlName);
                    DataRow[] dr = pDsLanguage.Tables[0].Select("LabelId = '" + i + "'");
                    if (dr != null && dr.Length > 0)
                    {
                        if (pParent.FindControl(ctrlName) != null && dr[0][1].ToString() != null &&
                            dr[0][1].ToString().Trim() != string.Empty)
                            ctrl.Text = dr[0][1].ToString();
                    }
                }
            }
        }


        /// <summary>
        /// /// This method generates a row of HTML for passenger details based on the number of passengers.
        /// </summary>
        /// <param name="pNoofPax"></param>
        /// <returns></returns>
        public static StringBuilder PassengerRow(int pNoofPax)
        {
            StringBuilder stbuild = new StringBuilder();
            for (int i = 1; i <= Convert.ToInt16(pNoofPax); i++)
            {
                /*stbuild.Append("<TR ><TD class=style3 align=left  valign=middle width=50  height=15>*Title :&nbsp;</TD>");
                stbuild.Append("<td class=style3 align=left  valign=middle width=50 height=15><select type=select onchange=\"chnageSex(" + i + ");\" name=contact_title" + i + " id=Title" + i + " title=''><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></td>");
                stbuild.Append("<TD class=style3 align=right valign=middle width=90 height=15>*Name :&nbsp;</TD>");
                stbuild.Append("<TD valign=middle align=left width=154 class=hlinks><input name=txtOName" + i + "  type=text id=txtOName" + i + "  style=height:20px;width:158px; size=40 onkeypress=\"return CheckOnlyCharacter(event);\" maxlength=40 onCopy=\"return false\" onDrag=\"return false\" onDrop=\"return false\" onPaste=\"return false\" autocomplete=off/></TD>");
                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=right height=15>*Age :</TD>");
                stbuild.Append("<TD style=WIDTH: 80px valign=middle height=15 class=hlinks><input name=txtAge" + i + " type=text id=txtAge" + i + " maxLength=2 size=2 onkeypress=\"return chkNumeric(event);\"  style=width:48px; onCopy=\"return false\" onDrag=\"return false\" onDrop=\"return false\" onPaste=\"return false\" autocomplete=off/></TD>");
                stbuild.Append("<TD class=style3 style=WIDTH: 90px valign=middle align=center height=15>Sex :</TD>");
                stbuild.Append("<TD class=hlinks style=WIDTH: 50px valign=middle align=center height=15>");
                stbuild.Append("<input id=RadM" + i + " type=radio runat=server Width=46px Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label></TD>");
                stbuild.Append("<TD class=style3 style=WIDTH: 50px valign=middle align=center height=15>&nbsp;");
                stbuild.Append("<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F  GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></TD>");
                stbuild.Append("<tr><td  class=hlinks colspan=9 align=left bgcolor=#FFFFFF height=1px ></td></tr>");
                */
                stbuild.Append("<div class='row mrgnbtminput'>");
                stbuild.Append("<div class=col-md-2><label> Title*</label> <select  class='form-control' Style='width: auto;' type=select onchange=\"chnageSex(" + i + ");\" name=contact_title" + i + " id=Title" + i + " title=''><option value=\"\" selected>Select</option><option value=Dr.>Dr.</option><option value=Mr.>Mr.</option><option value=Ms.>Ms.</option><option value=Mrs.>Mrs.</option><option value=Prof.>Prof.</option></select></div>");

                stbuild.Append("<div class=col-md-5> <label>Name*</label> <input placeholder=\"Name\" name=txtOName" + i + "  type=text id=txtOName" + i + "  class=form-control   onkeypress=\"return CheckOnlyCharacter(event);\" maxlength=40 onCopy=\"return false\" onDrag=\"return false\" onDrop=\"return false\" onPaste=\"return false\" autocomplete=off/></div>");

                stbuild.Append("<div class=col-md-2> <label>Age*</label> <input placeholder=\"Age\" name=txtAge" + i + " type=text id=txtAge" + i + "   class='form-control' Style=''  maxLength=2  onkeypress=\"return chkNumeric(event);\"  onCopy=\"return false\" onDrag=\"return false\" onDrop=\"return false\" onPaste=\"return false\" autocomplete=off/></div>");

                stbuild.Append("<div class=col-md-3><label>Sex*</label> </br><input id=RadM" + i + " type=radio runat=server  Name=Radio" + i + " Value=M GroupName=Radio" + i + "   /><label for=RadM" + i + ">Male</label> &nbsp;&nbsp;<input type=radio id=RadF" + i + " runat=server Width=48px Name=Radio" + i + " Value=F    GroupName=Radio" + i + "/><label for=RadF" + i + ">Female</label></div>");

                stbuild.Append("</div>");

            }
            return stbuild;

        }

        /// <summary>
        /// /// This method converts a number to its equivalent string representation in Indian Rupees.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        public static string GetNumber(int Num)
        {
            int NumLength;
            string TotalString = "";
            NumLength = Num.ToString().Length;
            switch (NumLength)
            {
                case 1:
                    //(For Single Digits) 
                    TotalString = ReturnNumbers(Num);
                    break;
                case 2:
                    //(For Double Digits) 
                    //TotalString = ReturnNumbers(Num);
                    TotalString = TenFunction(Num);

                    break;
                case 3:
                    //(For Triple Digits) 
                    TotalString = HundredFunction(Num);
                    break;
                case 4:
                case 5:
                    //For Thousand's 
                    TotalString = ThousandFunction(Num);
                    break;
                case 6:
                case 7:
                    //For Lakh's 
                    TotalString = LakhFunction(Num);
                    break;
                case 8:
                case 9:
                    //For Crore's 
                    TotalString = CroreFunction(Num);
                    break;
            }
            TotalString = TotalString + " rupees Only";
            return TotalString;
        }


        /// <summary>
        /// /// This method returns the string representation of a number based on its value.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private static string ReturnNumbers(int Num)
        {
            switch (Num)
            {
                case 1:
                    return "One";
                case 2:
                    return "Two";
                case 3:
                    return "Three";
                case 4:
                    return "Four";
                case 5:
                    return "Five";
                case 6:
                    return "Six";
                case 7:
                    return "Seven";
                case 8:
                    return "Eight";
                case 9:
                    return "Nine";
                case 10:
                    return "Ten";
                case 11:
                    return "Eleven";
                case 12:
                    return "Twelve";
                case 13:
                    return "Thirteen";
                case 14:
                    return "Fourteen";
                case 15:
                    return "Fifteen";
                case 16:
                    return "Sixteen";
                case 17:
                    return "Seventeen";
                case 18:
                    return "Eighteen";
                case 19:
                    return "Nineteen";
                case 20:
                    return "Twenty";
                case 30:
                    return "Thirty";
                case 40:
                    return "Fourty";
                case 50:
                    return "Fifty";
                case 60:
                    return "Sixty";
                case 70:
                    return "Seventy";
                case 80:
                    return "Eighty";
                case 90:
                    return "Ninty";
                case 100:
                    return "Hundred";
                case 1000:
                    return "Thousand";
                case 100000:
                    return "Lakh";
                case 10000000:
                    return "Crore";
                default:
                    return "";

            }
        }


        /// <summary>
        /// //  This method handles the conversion of numbers greater than 20 into their string representation.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private static string TenFunction(int Num)
        {
            int HundredDigit;
            int TenDigit;
            int TenPlace;
            //'Stores the Ten Place Digit 
            int UnitPlace;
            //'Stores the UnitPlace Digit 
            string TotalString;
            TotalString = "";
            TenDigit = Num;
            if (TenDigit > 20)
            {
                double q;
                TenPlace = Convert.ToInt16(Math.Floor((Convert.ToDouble(TenDigit) / 10)));
                UnitPlace = TenDigit % 10;
                if (TotalString.Trim() == "")
                    TotalString = ReturnNumbers(Convert.ToInt16(Math.Floor(Convert.ToDouble(TenPlace) * 10)));
                else
                    TotalString = TotalString + " And " + ReturnNumbers(Convert.ToInt16(Math.Floor(Convert.ToDouble(TenPlace) * 10)));

                TotalString = TotalString + " " + ReturnNumbers(UnitPlace);
            }
            else
            {
                if (TotalString.Trim() == "")
                    TotalString = ReturnNumbers(TenDigit);
                else
                {
                    if (TenDigit > 0)
                        TotalString = TotalString + " And " + ReturnNumbers(TenDigit);
                }
            }
            return TotalString;
        }


        /// <summary>
        /// /// This method handles the conversion of numbers in the hundreds into their string representation.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private static string HundredFunction(int Num)
        {
            int HundredDigit;
            int TenDigit;
            int TenPlace;
            //'Stores the Ten Place Digit 
            int UnitPlace;
            //'Stores the UnitPlace Digit 
            string TotalString;
            TotalString = "";
            HundredDigit = Convert.ToInt16(Math.Floor(Convert.ToDouble(Num) / 100));
            TenDigit = Num % 100;

            if (HundredDigit > 0)
            {
                TotalString = ReturnNumbers(HundredDigit) + " Hundred ";
            }

            if (TenDigit > 20)
            {
                double q;
                TenPlace = Convert.ToInt16(Math.Floor((Convert.ToDouble(TenDigit) / 10)));
                UnitPlace = TenDigit % 10;
                if (TotalString.Trim() == "")
                {
                    TotalString = ReturnNumbers(Convert.ToInt16(Math.Floor(Convert.ToDouble(TenPlace) * 10)));
                }
                else
                {
                    TotalString = TotalString + " And " + ReturnNumbers(Convert.ToInt16(Math.Floor(Convert.ToDouble(TenPlace) * 10)));
                }

                TotalString = TotalString + " " + ReturnNumbers(UnitPlace);
            }
            else
            {
                if (TotalString.Trim() == "")
                {
                    TotalString = ReturnNumbers(TenDigit);
                }
                else
                {
                    if (TenDigit > 0)
                    {
                        TotalString = TotalString + " And " + ReturnNumbers(TenDigit);
                    }
                }

            }
            return TotalString;
        }

        /// <summary>
        /// /// This method handles the conversion of numbers in the thousands into their string representation.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private static string ThousandFunction(int Num)
        {
            int FourthDigit;
            int HundNumber;
            string TotalString;
            FourthDigit = Convert.ToInt16(Math.Floor(Convert.ToDouble(Num) / 1000));
            HundNumber = Num % 1000;
            if (FourthDigit > 0)
            {
                TotalString = HundredFunction(FourthDigit) + " Thousand " + HundredFunction(HundNumber);
            }
            else
            {
                TotalString = HundredFunction(HundNumber);
            }
            return TotalString;
        }


        /// <summary>
        /// /// This method handles the conversion of numbers in the lakhs into their string representation.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private static string LakhFunction(int Num)
        {
            int FifthDigit;
            int ThousandNumber;
            string TotalString;
            FifthDigit = Convert.ToInt16(Math.Floor(Convert.ToDouble(Num) / 100000));
            ThousandNumber = Num % 100000;
            TotalString = HundredFunction(FifthDigit) + " Lakh " + ThousandFunction(ThousandNumber);
            return TotalString;
        }


        /// <summary>
        /// /// This method handles the conversion of numbers in the crores into their string representation.
        /// </summary>
        /// <param name="Num"></param>
        /// <returns></returns>
        private static string CroreFunction(int Num)
        {
            int SixthDigit;
            int TenThousandNumber;
            string TotalString;
            SixthDigit = Convert.ToInt16(Math.Floor(Convert.ToDouble(Num) / 10000000));
            TenThousandNumber = Num % 10000000;
            TotalString = HundredFunction(SixthDigit) + " Crore " + LakhFunction(TenThousandNumber);
            return TotalString;
        }
    }

}