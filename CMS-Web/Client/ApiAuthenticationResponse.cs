using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS_Web.Client
{
	/// <summary>
	/// The implementation of the authentication response from API
	/// </summary>
	public class ApiAuthenticationResponse
	{
		/// <summary>
		/// The access token.
		/// </summary>
		public string access_token { get; set; }

		/// <summary>
		/// The type of the token.
		/// </summary>
		public string token_type { get; set; }

		/// <summary>
		/// The token expiry in seconds.
		/// </summary>
		public int expires_in { get; set; }

		/// <summary>
		/// Logged in username.
		/// </summary>
		public string userName { get; set; }

		/// <summary>
		/// issued on date time
		/// </summary>
		public string issued { get; set; }

		/// <summary>
		/// expires on date time
		/// </summary>
		public string expires { get; set; }
	}
}
