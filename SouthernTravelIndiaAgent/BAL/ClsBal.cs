using SouthernTravelIndiaAgent.DAL;
using SouthernTravelIndiaAgent.SProcedure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace SouthernTravelIndiaAgent.BAL
{
    public class ClsBal
    {

        public DataTable ValidateAgent(string pUserID)
        {
            DataTable dtValidateAgent = new DataTable();
            string connString = DataLib.getConnectionString();

            try
            {
                using (SqlConnection conn = new SqlConnection(connString))
                using (SqlCommand cmd = new SqlCommand(StoredProcedures.ValidateAgent_sp, conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Input parameter
                    cmd.Parameters.Add(new SqlParameter("@I_UserID", SqlDbType.VarChar, 20)
                    {
                        Value = pUserID
                    });

                    // Output parameter
                    SqlParameter returnValueParam = new SqlParameter("@O_ReturnValue", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(returnValueParam);

                    // Execute
                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dtValidateAgent);
                    }

                    // You can optionally capture and use the output parameter value:
                    int status = returnValueParam.Value != DBNull.Value ? (int)returnValueParam.Value : 0;

                    if (dtValidateAgent.Rows.Count > 0)
                        return dtValidateAgent.Copy();
                    else
                        return null;
                }
            }
            catch (Exception ex)
            {
                // Optionally log ex
                return null;
            }
            finally
            {
                if (dtValidateAgent != null)
                {
                    dtValidateAgent.Dispose();
                    dtValidateAgent = null;
                }
            }
        }
    }
}