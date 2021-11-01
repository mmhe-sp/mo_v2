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
	}
}
