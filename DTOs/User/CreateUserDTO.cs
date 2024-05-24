using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.User
{
	public class CreateUserDTO
	{
		public string Firstname { get; set; }
		public string Lastname { get; set; }
		public string Pseudo { get; set; }
		public string EmailLogin { get; set; }
		public string Password { get; set; }
		public string ConfirmPassword { get; set; }
	}
}
