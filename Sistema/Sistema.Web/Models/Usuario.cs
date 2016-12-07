using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sistema.Web.Models
{
	public class Usuario
	{
		public string ConnectionId { get; set; }
		public int id { get; set; }
		public string name { get; set; }
		public List<Usuario> friendsList { get; set; }
		public string fontName { get; set; }
		public string fontSize { get; set; }
		public string fontColor { get; set; }
		public string sex { get; set; }
		public int age { get; set; }
		public string status { get; set; }
		public string memberType { get; set; }
		public string avator { get; set; }
		public string ContextName { get; set; }

		
	}
}
