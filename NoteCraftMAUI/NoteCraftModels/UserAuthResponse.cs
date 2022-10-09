using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteCraftModels
{
	public class UserAuthResponse
	{
		public UserAuthResponse(UserDto user, string token)
		{
			User = user;
			Token = token;
		}

		public UserDto User { get; set; }
		public string Token { get; set; }
	}
}
