using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Business
{
	public static class ConnectionStringHelper
	{
		public static string MO { get { return ConfigurationManager.ConnectionStrings["ConnectionStringMO"].ConnectionString; } }
	}
}
