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
    public partial class getTransferTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["city"] != null)
            {
                string lUserType = "";
                if (Request.QueryString["usertype"] != null)
                {
                    lUserType = DataLib.funClear(Request.QueryString["usertype"]);
                }
                ClsAdo clsObj = null;
                DataTable dtTransfers = null;
                try
                {
                    clsObj = new ClsAdo();
                    dtTransfers = clsObj.fnCar_TransferTypes_CityWise(Convert.ToInt32(DataLib.funClear(Request.QueryString["city"])), lUserType);
                    if (dtTransfers != null && dtTransfers.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtTransfers.Rows.Count; i++)
                        {
                            Response.Write(dtTransfers.Rows[i]["transfername"].ToString() + "#" + dtTransfers.Rows[i]["transferid"].ToString() + "<br>");
                        }
                    }
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (dtTransfers != null)
                    {
                        dtTransfers.Dispose();
                        dtTransfers = null;
                    }
                }
            }
        }
    }
}