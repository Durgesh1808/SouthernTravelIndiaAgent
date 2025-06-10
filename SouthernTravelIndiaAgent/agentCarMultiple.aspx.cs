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
    public partial class agentCarMultiple : System.Web.UI.Page
    {

        #region "Member Variable(s)"
        private string sfixed
        {
            get
            {
                if (ViewState["sfixed"] != null && ViewState["sfixed"].ToString().Trim() != "")
                {
                    return (string)ViewState["sfixed"];
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["sfixed"] = value; }
        }
        protected string stourno
        {
            get
            {
                if (ViewState["stourno"] != null && ViewState["stourno"].ToString().Trim() != "")
                {
                    return (string)ViewState["stourno"];
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["stourno"] = value; }
        }
        protected string strsubtranfer
        {
            get
            {
                if (ViewState["strsubtranfer"] != null && ViewState["strsubtranfer"].ToString().Trim() != "")
                {
                    return (string)ViewState["strsubtranfer"];
                }
                else
                {
                    return "0";
                }
            }
            set { ViewState["strsubtranfer"] = value; }
        }
        protected string strTransferId
        {
            get
            {
                if (ViewState["strTransferId"] != null && ViewState["strTransferId"].ToString().Trim() != "")
                {
                    return (string)ViewState["strTransferId"];
                }
                else
                {
                    return "0";
                }
            }
            set { ViewState["strTransferId"] = value; }
        }
        protected string strCityId
        {
            get
            {
                if (ViewState["strCityId"] != null && ViewState["strCityId"].ToString().Trim() != "")
                {
                    return (string)ViewState["strCityId"];
                }
                else
                {
                    return string.Empty;
                }
            }
            set { ViewState["strCityId"] = value; }
        }
        //protected string strDuration = "", strCity = "";
        //protected Boolean bAC = false, bMultiCity = false;
        //protected string transferType = "half";
        //protected double days = 0.5;
        ////protected string strCityId = "";

        //protected float duration = 0.0f;

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
                if (Request.QueryString["strs"] != null)
                {
                    sfixed = Request.QueryString["strs"].ToString();
                }

                if (Request.QueryString["TourNo"] != null)
                {
                    stourno = Request.QueryString["TourNo"].ToString();
                    BindGrid(stourno);
                }
                else
                {
                    if (Request.QueryString["cityid"] != null)
                    {
                        strCityId = Request.QueryString["cityid"].ToString();
                    }
                    Session["CityId"] = strCityId.ToString();
                    if (Request.QueryString["tid"] != null)
                    {
                        strTransferId = Request.QueryString["tid"].ToString();
                    }
                    if (Request.QueryString["subtransfer"] != null)
                    {
                        strsubtranfer = Request.QueryString["subtransfer"].ToString();
                    }
                    BindGrid(strTransferId, strCityId, strsubtranfer);
                }
            }
        }

        protected void pageChange(object sender, GridViewPageEventArgs e)
        {
            dgCarResults.PageIndex = e.NewPageIndex;
            if (stourno != string.Empty && stourno != "")
            {
                BindGrid(stourno);
            }
            else
            {
                BindGrid(strTransferId, strCityId, strsubtranfer);
            }
        }

        protected void btnBook_Click(object sender, EventArgs e)
        {
            Button btnBook = (Button)sender;
            Response.Redirect("agentCarSelect.aspx?subtrans=" + strsubtranfer + "&fid=" + btnBook.CommandArgument + "&sfixed=" + sfixed);
        }


        #endregion
        #region "Method(s)"

        void BindGrid(string transferId, string cityId, string strsubtranfer)
        {
            #region Optimize Code
            /*SqlParameter[] param = new SqlParameter[3];
            param[0] = new SqlParameter("@cityid", cityId);
            param[1] = new SqlParameter("@transferid", transferId);
            param[2] = new SqlParameter("@subtransferid", strsubtranfer);

            string sqlstr1 = "sp_GetCar_Local";
            DataSet dtcheck1 = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, sqlstr1, param);*/
            #endregion
            ClsAdo clsObj = new ClsAdo();
            DataTable ldtRecSet = clsObj.fnGetCar_Local(Convert.ToInt32(cityId), Convert.ToInt32(transferId), Convert.ToInt32(strsubtranfer), 0);
            try
            {
                dgCarResults.DataSource = ldtRecSet;
                dgCarResults.DataBind();
                if ((Session["AgentId"] == null) && (Session["BranchId"] == null))
                {
                    dgCarResults.HeaderRow.BackColor = System.Drawing.Color.Gold;
                    dgCarResults.HeaderRow.ForeColor = System.Drawing.Color.Black;
                }
                if (ldtRecSet == null || ldtRecSet.Rows.Count == 0)
                {
                    lblMsg.InnerText = "No car available";
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
        }
        public void BindGrid(string tourno)
        {
            #region Optimize Code
            /*SqlParameter[] param = new SqlParameter[1];
            param[0] = new SqlParameter("@TourID", stourno);

            string sqlstr = "sp_GetCar_fixed";
            DataSet dtcheck = DataLib.GetStoredProcData(DataLib.Connection.ConnectionString, sqlstr, param);*/
            #endregion
            ClsAdo clsObj = new ClsAdo();
            DataTable ldtRecSet = clsObj.fnGetCar_fixed(Convert.ToInt32(stourno));
            try
            {
                dgCarResults.DataSource = ldtRecSet;
                dgCarResults.DataBind();
                if ((Session["AgentId"] == null) && (Session["BranchId"] == null))
                {
                    dgCarResults.HeaderRow.BackColor = System.Drawing.Color.Gold;
                    dgCarResults.HeaderRow.ForeColor = System.Drawing.Color.Black;
                }

                if (ldtRecSet == null || ldtRecSet.Rows.Count == 0)
                {
                    lblMsg.InnerText = "No car available";
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
        }

        #endregion


    }
}