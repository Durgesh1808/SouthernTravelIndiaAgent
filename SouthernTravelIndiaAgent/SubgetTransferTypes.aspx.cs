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
    public partial class SubgetTransferTypes : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["subtrans"] != null)
            {
                ClsAdo clsObj = null;
                DataTable dtSubTransfer = null;
                try
                {
                    clsObj = new ClsAdo();
                    dtSubTransfer = clsObj.fnCar_SubTransfertypes(Convert.ToInt32(Request.QueryString["subtrans"]));
                    if (dtSubTransfer != null && dtSubTransfer.Rows.Count > 0)
                    {
                        if (clsObj != null)
                        {
                            clsObj = null;
                        }
                        for (int i = 0; i < dtSubTransfer.Rows.Count; i++)
                        {
                            Response.Write(dtSubTransfer.Rows[i]["Subtransfername"].ToString() + "#" + dtSubTransfer.Rows[i]["subtransferId"].ToString() + "<br>");
                        }
                    }
                }
                finally
                {
                    if (clsObj != null)
                    {
                        clsObj = null;
                    }
                    if (dtSubTransfer != null)
                    {
                        dtSubTransfer.Dispose();
                        dtSubTransfer = null;
                    }
                }
            }
        }
    }
}