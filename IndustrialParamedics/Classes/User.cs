using System;

namespace IndustrialParamedics
{
	public class User
	{
		public string userName { get; set; }
		public string userId { get; set; }
		public string displayName { get; set; }

		public User (string userName, string userId) {
			this.userName = userName;
			this.userId = userId;
		}

	}
}

