using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.DTO;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SouthernTravelIndiaAgent
{
    public partial class CityWisetours : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["BranchCode"] != null)
                {
                    string hr = "";
                    if (Session["AgentId"] != null)
                        hr = ConfigurationManager.AppSettings["AgentFixedTourHours"].ToString();
                    else
                        hr = ConfigurationManager.AppSettings["BranchFixedTourHours"].ToString();

                    DataTable dtTours = null;
                    ClsAdo pvclsObj = null;
                    try
                    {
                        pvclsObj = new ClsAdo();
                        dtTours = pvclsObj.fnBranchWiseTour(Convert.ToInt32(hr), DataLib.funClear(Request.QueryString["BranchCode"]));
                        if (dtTours != null && dtTours.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtTours.Rows.Count; i++)
                            {
                                Response.Write(dtTours.Rows[i]["tourname"].ToString() + "#" + dtTours.Rows[i]["Tourno"].ToString() + "<br>");
                            }
                        }
                    }
                    finally
                    {
                        if (pvclsObj != null)
                            pvclsObj = null;
                        if (dtTours != null)
                        {
                            dtTours.Dispose();
                            dtTours = null;
                        }
                    }
                }
                if (Request.QueryString["LedgerID"] != null)
                {
                    List<GetSubLedgerHead_SPResult> lResult = null;
                    ClsAdo pvclsObj = null;
                    try
                    {
                        pvclsObj = new ClsAdo();
                        lResult = pvclsObj.fnGetSubLedgerHead(Convert.ToInt32(Request.QueryString["LedgerID"]));
                        if (lResult != null && lResult.Count > 0)
                        {
                            for (int lCtr = 0; lCtr < lResult.Count; lCtr++)
                            {
                                Response.Write(lResult[lCtr].SelectionName.ToString() + "#" + lResult[lCtr].SelectionName.ToString() + "-" + lResult[lCtr].RowID.ToString() + "#" + lResult[lCtr].SelectionType.ToString() + "<br>");
                            }
                        }
                    }
                    finally
                    {
                        if (pvclsObj != null)
                            pvclsObj = null;
                        if (lResult != null)
                        {
                            lResult = null;
                        }
                    }
                }
            }
        }
    }
}