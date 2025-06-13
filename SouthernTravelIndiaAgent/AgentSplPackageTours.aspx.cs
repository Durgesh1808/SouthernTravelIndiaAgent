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
    public partial class AgentSplPackageTours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AgentId"] == null)
                Response.Redirect("AgentLogin.aspx");
            else
            {
                ////---NORTH INDIA TOUR (ZoneID=1)----
                ClsAdo clsObj = null;
                DataTable dtTourN = null, dtTourS = null, dtTourW = null, dtTourE = null;
                try
                {
                    clsObj = new ClsAdo();
                    dtTourN = clsObj.fnSpecialTours_Zonewise(1);
                    if (dtTourN != null && dtTourN.Rows.Count > 0)
                    {
                        dlToursN.DataSource = dtTourN;
                        dlToursN.DataBind();
                        trNtour.Visible = true;
                    }
                    else
                        trNtour.Visible = false;
                    ////---SOUTH INDIA TOUR (ZoneID=2)----           
                    dtTourS = clsObj.fnSpecialTours_Zonewise(2);

                    if (dtTourS != null && dtTourS.Rows.Count > 0)
                    {
                        dlToursS.DataSource = dtTourS;
                        dlToursS.DataBind();
                        trStour.Visible = true;
                    }
                    else
                        trStour.Visible = false;
                    ////---WESTERN INDIA TOUR (ZoneID=3)----        
                    dtTourW = clsObj.fnSpecialTours_Zonewise(3);

                    if (dtTourW != null && dtTourW.Rows.Count > 0)
                    {
                        dlToursW.DataSource = dtTourW;
                        dlToursW.DataBind();
                        trWtour.Visible = true;
                    }
                    else
                        trWtour.Visible = false;
                    ////---EASTERN INDIA TOUR (ZoneID=4)----            
                    dtTourE = clsObj.fnSpecialTours_Zonewise(4);

                    if (dtTourE != null && dtTourE.Rows.Count > 0)
                    {
                        dlToursE.DataSource = dtTourE;
                        dlToursE.DataBind();
                        trEtour.Visible = true;
                    }
                    else
                        trEtour.Visible = false;
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
                    if (dtTourS != null)
                    {
                        dtTourS.Dispose();
                        dtTourS = null;
                    }
                    if (dtTourW != null)
                    {
                        dtTourW.Dispose();
                        dtTourW = null;
                    }
                    if (dtTourE != null)
                    {
                        dtTourE.Dispose();
                        dtTourE = null;
                    }
                }
            }
        }
    }
}