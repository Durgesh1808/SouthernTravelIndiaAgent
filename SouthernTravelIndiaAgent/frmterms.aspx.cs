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
    public partial class frmterms : System.Web.UI.Page
    {
        ClsAdo pClsObj = new ClsAdo();
        protected void Page_Load(object sender, EventArgs e)
        {
            #region Optimize Code
            /* DataTable dtterm = DataLib.GetDataTable(DataLib.Connection.ConnectionString, "SELECT TermsCondition FROM CompanyDetailsForRpt");*/
            #endregion
            DataTable dtterm = pClsObj.fnGetCompanyTermsCondition();
            if (dtterm.Rows.Count > 0)
                terms.InnerHtml = dtterm.Rows[0]["TermsCondition"].ToString();
            else
                terms.InnerHtml = "";
        }
    }
}