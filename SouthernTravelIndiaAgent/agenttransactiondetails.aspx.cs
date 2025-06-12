using SouthernTravelIndiaAgent.BAL;
using SouthernTravelIndiaAgent.Common;
using SouthernTravelIndiaAgent.DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class agenttransactiondetails : System.Web.UI.Page
    {
        int sno = 1;
        double total = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Attributes.Add("onclick", "javascript:return validation();");
            txtFromDate.Attributes.Add("onkeypress", "javascript:return keyboardlock();");
            txtToDate.Attributes.Add("onkeypress", "javascript:return keyboardlock();");
            if (Convert.ToString(Session["AgentId"]) != "")
            {
                if (!IsPostBack)
                {
                    BindDropDown();
                    btnExport.Visible = false;
                }
            }
            else
                Response.Redirect("agentlogin.aspx");
        }
        string strSQL, strWhere;
        protected void BindData()
        {
            DataTable dt;
            string DateFrom, DateFrom1 = "", DateTo, DateTo1 = "";
            DateFrom = Request[txtFromDate.UniqueID];
            if (DateFrom != "")
                DateFrom = ClsCommon.mmddyy2ddmmyy(DateFrom);
            DateTo = Request[txtToDate.UniqueID];
            if (DateTo != "")
                DateTo = ClsCommon.mmddyy2ddmmyy(DateTo);
            int transid = 0;
            if (ddlTrans.SelectedIndex != 0)
                transid = Convert.ToInt32(ddlTrans.SelectedValue);
            //dt = ClsAgentTransaction.Agent_Transaction_Report('Y', Convert.ToInt32(Session["AgentId"]), transid, DateFrom, DateTo, "");
            ClsAdo clsObj = new ClsAdo();
            dt = clsObj.fnGetAgent_Transaction_Report('Y', Convert.ToInt32(Session["AgentId"]), transid, DateFrom, DateTo, "");
            dgrReports.DataSource = dt;
            Globals.CheckData(ref dgrReports, dt, ref lblMsg);
            if (dt.Rows.Count == 0)
            {
                trGrossTot.Visible = false;
                trPageTot.Visible = false;
                btnExport.Visible = false;
            }
            else
            {
                trGrossTot.Visible = true;
                trPageTot.Visible = true;
                btnExport.Visible = true;
            }
        }
        protected void BindDropDown()
        {
            DataTable dt1 = ClsAgentTransaction.Agent_TransactionTypes('N');
            try
            {
                ddlTrans.DataSource = dt1;
                ddlTrans.DataBind();
                ddlTrans.Items.Insert(0, new ListItem("All", "0"));
            }
            finally
            {
                if (dt1 != null)
                {
                    dt1.Dispose();
                    dt1 = null;
                }
            }
        }
        protected void dgrReports_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                DataRowView rv = (DataRowView)e.Item.DataItem;
                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    e.Item.Cells[2].Text = sno.ToString();
                    sno = sno + 1;
                    string k = Convert.ToString(rv.Row["TicketAmount"]);
                    if (k != "" && k != null)
                    {
                        total += Convert.ToDouble(k);
                        ViewState["k"] = total;
                    }
                }
                lblTot.Text = "Page Total :";
                if (total.ToString().IndexOf(".") != -1)
                    lbltotal.Text = Convert.ToString(total.ToString());
                else
                    lbltotal.Text = Convert.ToString(total.ToString()) + ".00";

                if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
                {
                    e.Item.Cells[12].Text = "";
                    string s = e.Item.Cells[5].Text.ToString().Trim();
                    if (e.Item.Cells[4].Text.ToUpper().Substring(0, 5) == "AAFCH")
                    {
                        string val = rv.Row.ItemArray[5].ToString();
                        if (val != "")
                            e.Item.Cells[5].Text = "Chq No:" + val;
                    }
                    if (e.Item.Cells[4].Text.ToUpper().Substring(0, 5) == "ADFCH")
                    {
                        string val = rv.Row.ItemArray[5].ToString();
                        if (val == "")
                        {
                            e.Item.Cells[5].Text = "Cash Withdrawl";
                            e.Item.Cells[12].Text = rv.Row.ItemArray[15].ToString();
                        }
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 4) == "AAFC")
                    {
                        string val = rv.Row.ItemArray[5].ToString();
                        if (val == "")
                            e.Item.Cells[5].Text = "Cash Deposit";
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 3) == "EBK")
                    {
                        string k = rv.Row.ItemArray[5].ToString();
                        if (k == "")
                            e.Item.Cells[5].Text = "Ticket Booking";
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 3) == "CAB")
                    {
                        string Cab = rv.Row.ItemArray[5].ToString();
                        if (Cab == "")
                            e.Item.Cells[5].Text = "Cab Booking";
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 4) == "CANC")
                    {
                        string Cab = rv.Row.ItemArray[5].ToString();
                        if (Cab == "")
                            e.Item.Cells[5].Text = "Cancelation";
                    }
                    else if ((e.Item.Cells[4].Text.ToUpper().Substring(0, 3) == "CAN") && (e.Item.Cells[4].Text.ToUpper().Substring(0, 4) != "CANC"))
                    {
                        string Cab = rv.Row.ItemArray[5].ToString();
                        if (Cab == "")
                            e.Item.Cells[5].Text = " Ticket Cancelation";
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 3) == "SPL")
                    {
                        string spl = rv.Row.ItemArray[5].ToString();
                        if (spl == "")
                            e.Item.Cells[5].Text = "Special Tour Booking";
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 3) == "HAC")
                    {
                        string spl = rv.Row.ItemArray[5].ToString();
                        if (spl == "")
                            e.Item.Cells[5].Text = "HAC Ticket Booking";
                    }
                    else if (e.Item.Cells[4].Text.ToUpper().Substring(0, 3) == "HAC")
                    {
                        string spl = rv.Row.ItemArray[5].ToString();
                        if (spl == "")
                            e.Item.Cells[5].Text = "HAC Ticket Booking";
                    }

                    if (e.Item.Cells[15].Text != "" && e.Item.Cells[15].Text != "&nbsp;")
                    {
                        e.Item.Cells[5].Text = e.Item.Cells[15].Text;
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        double[] prevTotal;
        protected void dgrReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgrReports.CurrentPageIndex = e.NewPageIndex;
            if (dgrReports.CurrentPageIndex == 0)
                ViewState["k"] = 0;
            prevTotal = (double[])ViewState["kArr"];
            BindData();
            prevTotal[dgrReports.CurrentPageIndex] = (int)total;
            double grosstotal = 0.0d;
            if (dgrReports.CurrentPageIndex >= 0)
            {
                for (int i = 0; i <= dgrReports.CurrentPageIndex; i++)
                {
                    grosstotal += (prevTotal[i]);
                }
                lblCary.Text = "Gross Total :";
                if (Convert.ToString(grosstotal).IndexOf(".") != -1)
                    lblCaryFwd.Text = Convert.ToString(grosstotal);
                else
                    lblCaryFwd.Text = Convert.ToString(grosstotal) + ".00";
            }
        }
        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
            double[] arr = new double[dgrReports.PageCount];
            if (dgrReports.PageCount > 0)
            {
                arr[0] = total;
                ViewState["kArr"] = arr;
                trGrossTot.Visible = false;
            }
            if (dgrReports.PageCount > 55)
                trGrossTot.Visible = false;
        }
        private void ExportGridView(DataGrid Dg)
        {
            string attachment = "attachment; filename=ST_AgentTransaction.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            Dg.RenderControl(htw);
            Response.Write(sw.ToString());
            Response.End();
        }
        private void ClearControls(Control control)
        {
            for (int i = control.Controls.Count - 1; i >= 0; i--)
            {
                ClearControls(control.Controls[i]);
            }
            if (!(control is TableCell))
            {
                if (control.GetType().GetProperty("SelectedItem") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    try
                    {
                        literal.Text = (string)control.GetType().GetProperty("SelectedItem").GetValue(control, null);
                    }
                    catch
                    {
                    }
                    control.Parent.Controls.Remove(control);
                }
                else
                    if (control.GetType().GetProperty("Text") != null)
                {
                    LiteralControl literal = new LiteralControl();
                    control.Parent.Controls.Add(literal);
                    literal.Text = (string)control.GetType().GetProperty("Text").GetValue(control, null);
                    control.Parent.Controls.Remove(control);
                }
            }
            return;
        }
        private void PrepareGridViewForExport(Control gv)
        {
            LinkButton lb = new LinkButton();
            Literal l = new Literal();
            string name = String.Empty;
            for (int i = 0; i < gv.Controls.Count; i++)
            {
                if (gv.Controls[i].GetType() == typeof(LinkButton))
                {
                    l.Text = (gv.Controls[i] as LinkButton).Text;
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(EditCommandColumn))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                else if (gv.Controls[i].GetType() == typeof(CheckBox))
                {
                    gv.Controls.Remove(gv.Controls[i]);
                    gv.Controls.AddAt(i, l);
                }
                if (gv.Controls[i].HasControls())
                    PrepareGridViewForExport(gv.Controls[i]);
            }
        }
        protected void btnExport_Click(object sender, EventArgs e)
        {
            BindData();
            DataGrid dgExport = dgrReports;
            dgExport.AllowPaging = false;
            dgExport.DataSource = dgExport.DataSource;
            dgExport.DataBind();
            ExportGridView(dgExport);
        }
    }
}