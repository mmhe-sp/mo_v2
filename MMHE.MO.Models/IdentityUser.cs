using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMHE.MO.Models
{
	public class IdentityUser
	{
		public string Name { get; set; }
		public int Id { get; set; }
		public string ProjectId { get; set; }
		public string Email { get; set; }
		public string WBS { get; set; }
		public string ProjectName { get; set; }
	}
}
