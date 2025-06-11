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
    public partial class AgentCarFixedTours : System.Web.UI.Page
    {
        public string sfixed = "", strCityId = "", stourname = "", sdesc = "", sduday = "", sdunght = "",
         sduration = "", slocation = "", samount = "", stourno = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] != null)
            {
                if (Request.QueryString["cityid"] != null)
                {
                    strCityId = Request.QueryString["cityid"].ToString();
                }
                sfixed = "FixedTour";

                if (!IsPostBack)
                {
                    if (Request.QueryString["cityid"] != null)
                    {
                        strCityId = Request.QueryString["cityid"].ToString();
                        Session["CityId"] = strCityId.ToString();
                    }
                    CarFixedTourBind();
                }
            }
            else
            {
                Response.Redirect("agentlogin.aspx");
            }
        }

        /// <summary>
        /// /// This method binds the fixed tour details to the grid view based on the selected city ID.
        /// </summary>
        public void CarFixedTourBind()
        {

            #region Optimize Code
            /*SqlCommand cmd = new SqlCommand("Car_FixedTourDetails_sp");

            cmd.Connection = DataLib.GetConnection();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@CityId", SqlDbType.VarChar, 100, ParameterDirection.Input, true, 10, 0, "", DataRowVersion.Proposed, strCityId));
            cmd.ExecuteNonQuery();
            SqlDataAdapter dar = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dar.Fill(dt);*/
            #endregion
            ClsAdo clsObj = new ClsAdo();
            DataTable dt = clsObj.fnCar_FixedTourDetails(Convert.ToInt32(strCityId));
            try
            {
                dg_list.DataSource = dt;
                dg_list.DataBind();

                if (dt != null && dt.Rows.Count > 0)
                {
                    stourname = dt.Rows[0]["TourName"].ToString();
                    sdesc = dt.Rows[0]["TourDescription"].ToString();
                    sduday = dt.Rows[0]["DurationDay"].ToString();
                    sdunght = dt.Rows[0]["DurationNight"].ToString();
                    sduration = sduday + '/' + sdunght;
                    slocation = dt.Rows[0]["Locations"].ToString();
                    samount = dt.Rows[0]["Amount"].ToString();
                    stourno = dt.Rows[0]["TourNo"].ToString();

                }
                else
                {
                    lblMsg.InnerText = "No Fixed Tour available";
                }
            }
            finally
            {
                if (dt != null)
                {
                    dt.Dispose();
                    dt = null;
                }
                if (clsObj != null)
                {
                    clsObj = null;
                }
            }
        }
    }
}