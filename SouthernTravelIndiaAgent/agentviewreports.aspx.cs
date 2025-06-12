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
    public partial class agentviewreports : System.Web.UI.Page
    {
        protected double myTotal = 0;
        ClsAdo clsObj = null;
        protected void Page_Load(object sender, EventArgs e)
        {
            btnSubmit.Attributes.Add("onclick", "javascript:return validation();");
            txtFromDate.Attributes.Add("onkeypress", "javascript:return keyboardlock();");
            txtToDate.Attributes.Add("onkeypress", "javascript:return keyboardlock();");
            if (Convert.ToString(Session["AgentId"]) != "")
            {
                if (!IsPostBack)
                {
                    DataTable dt = ClsAgentTransaction.Agent_TransactionTypes('Y');
                    try
                    {
                        ddlType.DataSource = dt;
                        ddlType.DataBind();
                        ddlType.Items.Insert(0, "Select");
                        btnExport.Visible = false;
                    }
                    finally
                    {
                        if (dt != null)
                        {
                            dt.Dispose();
                            dt = null;
                        }
                    }
                }
            }
            else
                Response.Redirect("AgentLogin.aspx");
        }
        protected void btnSubmit_Click(object sender, ImageClickEventArgs e)
        {
            BindData();
            double[] arr = new double[dgrReports.PageCount];
            if (dgrReports.PageCount > 0)
            {
                arr[0] = myTotal;
                ViewState["kArr"] = arr;
                trGrossTot.Visible = false;
            }
            if (dgrReports.PageCount > 50)
                trGrossTot.Visible = false;
        }
        double[] prevTotal;
        protected void dgrReports_PageIndexChanged(object source, DataGridPageChangedEventArgs e)
        {
            dgrReports.CurrentPageIndex = e.NewPageIndex;
            if (dgrReports.CurrentPageIndex == 0)
                ViewState["k"] = 0;
            prevTotal = (double[])ViewState["kArr"];
            BindData();
            prevTotal[dgrReports.CurrentPageIndex] = (int)myTotal;
            double grosstotal = 0.0d;
            if (dgrReports.CurrentPageIndex >= 0)
            {
                for (int i = 0; i <= dgrReports.CurrentPageIndex; i++)
                {
                    grosstotal += (prevTotal[i]);

                }
                lblCary.Text = "Gross Commission(Including Tax) :";
                if (Convert.ToString(grosstotal).IndexOf(".") != -1)
                    lblCaryFwd.Text = Convert.ToString(grosstotal);
                else
                    lblCaryFwd.Text = Convert.ToString(grosstotal) + ".00";
            }
        }
        protected void BindData()
        {
            #region Commented
            /*
            string DateFrom1, DateTo1;
            DateFrom1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(txtFromDate.Text.Trim())).ToShortDateString();
            DateTo1 = Convert.ToDateTime(ClsCommon.mmddyy2ddmmyy(txtToDate.Text.Trim())).ToShortDateString();
            SqlParameter[] lParam = new SqlParameter[4];
            lParam[0] = new SqlParameter("@I_AgentId", Convert.ToInt32(Session["AgentId"]));
            lParam[1] = new SqlParameter("@I_TransTypeID", Convert.ToInt32(ddlType.SelectedValue));
            lParam[2] = new SqlParameter("@I_FromDate", Convert.ToDateTime(DateFrom1));
            lParam[3] = new SqlParameter("@I_ToDate", Convert.ToDateTime(DateTo1));
            DataSet ds = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, "agent_viewreport", lParam);*/
            #endregion

            int? pAgentID = Convert.ToInt32(Session["AgentId"]);
            int? pTransTypeID = Convert.ToInt32(ddlType.SelectedValue);
            DateTime? pFromDate = null;
            DateTime? pToDate = null;
            if (txtFromDate.Text != "")
            {
                string[] pJDate = txtFromDate.Text.Split('/');
                pFromDate = new DateTime(Convert.ToInt32(pJDate[2]), Convert.ToInt32(pJDate[1]), Convert.ToInt32(pJDate[0]));
            }
            if (txtToDate.Text != "")
            {
                string[] pTDate = txtToDate.Text.Split('/');
                pToDate = new DateTime(Convert.ToInt32(pTDate[2]), Convert.ToInt32(pTDate[1]), Convert.ToInt32(pTDate[0]));
            }
            clsObj = new ClsAdo();
            DataTable ldtRecSet = clsObj.fnGetAgent_ViewReport(pAgentID, pTransTypeID, pFromDate, pToDate);
            DataSet ds = new DataSet();
            try
            {
                if (ldtRecSet != null)
                {
                    ds.Tables.Add(ldtRecSet);
                }
                dgrReports.DataSource = ds.Tables[0];
                Globals.CheckData(ref dgrReports, ds.Tables[0], ref lblMsg);
                if (ds.Tables[0].Rows.Count == 0)
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
                if (ds != null)
                {
                    ds.Dispose();
                    ds = null;
                }
            }
        }
        protected void dgrReports_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem))
            {
                DataRowView rv = (DataRowView)e.Item.DataItem; ;
                string k = Convert.ToString(rv.Row["Commission"]);
                if (k != null && k != "")
                {
                    myTotal += Convert.ToDouble(k);
                    ViewState["k"] = myTotal;
                }
            }
            lblTot.Text = "Page Total :";
            if (myTotal.ToString().IndexOf(".") != -1)
                lbltotal.Text = Convert.ToString(myTotal.ToString());
            else
                lbltotal.Text = Convert.ToString(myTotal.ToString()) + ".00";
        }
        private void ExportGridView()
        {
            string attachment = "attachment; filename=ST_AgentComission.xls";
            Response.ClearContent();
            Response.AddHeader("content-disposition", attachment);
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter sw = new StringWriter();
            HtmlTextWriter htw = new HtmlTextWriter(sw);
            this.ClearControls(dgrReports);
            dgrReports.RenderControl(htw);
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
            PrepareGridViewForExport(dgrReports);
            ExportGridView();
        }
    }
}