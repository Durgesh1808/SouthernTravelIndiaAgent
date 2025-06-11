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
    public partial class getAllSplTours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Request.QueryString["zone"] != null) && (Request.QueryString["mode"] != null))
                GEtAllTours(DataLib.funClear(Request.QueryString["zone"]), DataLib.funClear(Request.QueryString["mode"]));
        }

        /// <summary>
        /// /// This method retrieves all special tours available in a specific zone and displays them in a table format.
        /// </summary>
        /// <param name="zoneId"></param>
        /// <param name="mode"></param>
        private void GEtAllTours(string zoneId, string mode)
        {
            string url = "";
            ClsAdo clsObj = null;
            DataTable dtTourN = null;
            try
            {
                clsObj = new ClsAdo();
                dtTourN = clsObj.fnSpecialTours_Zonewise(Convert.ToInt32(DataLib.funClear(zoneId)));
                if (dtTourN != null && dtTourN.Rows.Count > 0)
                {
                    Response.Write(@"<table cellspacing=""5"">");
                    Response.Write(@"<tr><td>Available Tours</td></tr>");
                    for (short i = 0; i < dtTourN.Rows.Count; i++)
                    {
                        url = @"<tr><td><a target=""_top"" href=""" + mode + @"specialtour.aspx?tourid=" + dtTourN.Rows[i]["Tourid"].ToString() + @""" >" + "(" + dtTourN.Rows[i]["Tourcode"].ToString() + ") : " + dtTourN.Rows[i]["Tourname"].ToString() + "</a> (" + dtTourN.Rows[i]["Duration"].ToString() + ")</td></tr>";
                        Response.Write(url);
                    }
                    Response.Write("</table>");
                }
                else
                {
                    Response.Write(@"<table cellspacing=""5"">");
                    Response.Write(@"<tr><td>No open tours</td></tr>");
                    Response.Write("</table>");
                }
            }
            finally
            {
                if (clsObj != null)
                {
                    clsObj = null;
                }
                if (dtTourN != null)
                {
                    dtTourN.Dispose();
                    dtTourN = null;

                }
            }
        }
    }
}