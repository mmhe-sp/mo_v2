using MMHE.MO.Models;
using System;
using System.Data;
using System.Data.SqlClient;

namespace MMHE.MO.Business.Repositories
{
    public class JSLRepository
    {
        public DataTable GetAll(string project)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@pProNo", project);
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("SP_GetJSLMaster", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }

        public void Save(JSLDetails jsl)
        {


            using (SqlConnection _con = new SqlConnection(ConnectionStringHelper.MO))
            {
                _con.Open();
                SqlCommand cmd = new SqlCommand("[MO].[SaveJSL]", _con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pJSLID", jsl.JSLID);
                cmd.Parameters.AddWithValue("@pProjectNo", jsl.ProjectNo);
                cmd.Parameters.AddWithValue("@pType", jsl.Type);
                cmd.Parameters.AddWithValue("@pDiscipline", jsl.Discipline);
                cmd.Parameters.AddWithValue("@pOwnerNo", jsl.OwnerNo);
                cmd.Parameters.AddWithValue("@pWBS", jsl.WBS);
                cmd.Parameters.AddWithValue("@pWBSDesc", jsl.WBSDesc);
                cmd.Parameters.AddWithValue("@pWorkTitle", jsl.WorkTitle);
                cmd.Parameters.AddWithValue("@pRemakrs", jsl.Remakrs);
                cmd.Parameters.AddWithValue("@pStatus", jsl.Status);
                cmd.Parameters.AddWithValue("@pAWOVoNo", jsl.AWOVoNo);
                cmd.Parameters.AddWithValue("@pIssuedBy", jsl.IssuedBy);

                if (!jsl.RcvPMT.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtRcvPMT", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtRcvPMT", jsl.RcvPMT.Value);
                }
                if (!jsl.SubmitTo.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtSubmitTo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtSubmitTo", jsl.SubmitTo.Value);
                }
                if (!jsl.RcvClient.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtRcvClient", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtRcvClient", jsl.RcvClient.Value);
                }




                cmd.Parameters.AddWithValue("@pAWOVORemarks", jsl.AWOVORemarks);

                if (!jsl.DWCCompleted.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtDWC_Completed", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtDWC_Completed", jsl.DWCCompleted);
                }

                if (!jsl.WCRPlan.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtWCRPlan", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtWCRPlan", Convert.ToDateTime(jsl.WCRPlan));
                }
                if (!jsl.WCRAct.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtWCRAct", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtWCRAct", jsl.WCRAct);
                }
                if (!jsl.WCRSign.HasValue)
                {
                    cmd.Parameters.AddWithValue("@pDtWCRSign", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtWCRSign", jsl.WCRSign);
                }

                cmd.Parameters.AddWithValue("@pWCRStatus", jsl.WCRStatus);
                cmd.Parameters.AddWithValue("@pWeightage", jsl.Weightage);
                cmd.Parameters.AddWithValue("@pBy", jsl.User);
                cmd.Parameters.AddWithValue("@pSeqNo", jsl.SequenceNo);

                cmd.ExecuteNonQuery();
                _con.Close();
            }
        }

        public void Import(string strTransType, string JSLID, string strProjectNo, string strType, string strDiscipline, string strOwnerNo, string strWBS, string strWBSDesc, string strWorkTitle,
                        string strRemakrs, string strStatus, string strAWOVoNo, string strIssuedBy,
            string dtRcvPMT, string strDtSubmitTo,
            string strDtRcvClient, string strAWOVORemarks,
            string dtDWCCompleted, string DtWCRPlan, string DtWCRAct, string DtWCRSign, string strWCRStatus, string strWeightage, string strCUser, string strSeqNo)
        {
            Guid gJSLID;
            if (JSLID != "")
            {
                gJSLID = new Guid(JSLID);
            }
            else
            {
                gJSLID = new Guid();
            }


            using (SqlConnection _con = new SqlConnection(ConnectionStringHelper.MO))
            {
                _con.Open();
                SqlCommand cmd = new SqlCommand("[dbo].[SP_InsJSLMaster]", _con);
                cmd.CommandType = CommandType.StoredProcedure;

                cmd.Parameters.AddWithValue("@pTransType", strTransType);
                cmd.Parameters.AddWithValue("@pJSLID", gJSLID);
                cmd.Parameters.AddWithValue("@pProjectNo", strProjectNo);
                cmd.Parameters.AddWithValue("@pType", strType);
                cmd.Parameters.AddWithValue("@pDiscipline", strDiscipline);
                cmd.Parameters.AddWithValue("@pOwnerNo", strOwnerNo);
                cmd.Parameters.AddWithValue("@pWBS", strWBS);
                cmd.Parameters.AddWithValue("@pWBSDesc", strWBSDesc);
                cmd.Parameters.AddWithValue("@pWorkTitle", strWorkTitle);
                cmd.Parameters.AddWithValue("@pRemakrs", strRemakrs);
                cmd.Parameters.AddWithValue("@pStatus", strStatus);
                cmd.Parameters.AddWithValue("@pAWOVoNo", strAWOVoNo);
                cmd.Parameters.AddWithValue("@pIssuedBy", strIssuedBy);

                if (string.IsNullOrWhiteSpace(dtRcvPMT) || dtRcvPMT == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtRcvPMT", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtRcvPMT", Convert.ToDateTime(dtRcvPMT));
                }
                if (string.IsNullOrWhiteSpace(strDtSubmitTo) || strDtSubmitTo == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtSubmitTo", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtSubmitTo", Convert.ToDateTime(strDtSubmitTo));
                }
                if (string.IsNullOrWhiteSpace(strDtRcvClient) || strDtRcvClient == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtRcvClient", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtRcvClient", Convert.ToDateTime(strDtRcvClient));
                }




                cmd.Parameters.AddWithValue("@pAWOVORemarks", strAWOVORemarks);

                if (string.IsNullOrWhiteSpace(dtDWCCompleted) || dtDWCCompleted == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtDWC_Completed", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtDWC_Completed", Convert.ToDateTime(dtDWCCompleted));
                }

                if (string.IsNullOrWhiteSpace(DtWCRPlan) || DtWCRPlan == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtWCRPlan", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtWCRPlan", Convert.ToDateTime(DtWCRPlan));
                }
                if (string.IsNullOrWhiteSpace(DtWCRAct) || DtWCRAct == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtWCRAct", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtWCRAct", Convert.ToDateTime(DtWCRAct));
                }
                if (string.IsNullOrWhiteSpace(DtWCRSign) || DtWCRSign == "01/01/1900")
                {
                    cmd.Parameters.AddWithValue("@pDtWCRSign", DBNull.Value);
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pDtWCRSign", Convert.ToDateTime(DtWCRSign));
                }

                cmd.Parameters.AddWithValue("@pWCRStatus", strWCRStatus);
                cmd.Parameters.AddWithValue("@pWeightage", strWeightage);
                cmd.Parameters.AddWithValue("@pBy", strCUser);
                cmd.Parameters.AddWithValue("@pSeqNo", strSeqNo);

                cmd.ExecuteNonQuery();
                _con.Close();
            }
        }

        public DataTable ExportJSL(string project)
        {
            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@ProjectNo", project);
            DataTable table = new DataTable();
            using (SqlConnection connection = new SqlConnection(ConnectionStringHelper.MO))
            {
                using (SqlCommand command = new SqlCommand("MO.ExportJSL", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddRange(parameters);
                    SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(command);
                    sqlDataAdapter.Fill(table);
                }
            }
            return table;
        }
    }
}
