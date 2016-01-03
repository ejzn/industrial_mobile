using System;

namespace IndustrialParamedics
{
	public class User
	{
		public string userName { get; set; }
		public string userId { get; set; }
		public string displayName { get; set; }
		public enum Role { admin, medic, customer};
		public Role role { get; set; }

		public User (string userName, string userId, Role role) {
			this.userName = userName;
			this.userId = userId;
			this.role = role;
		}

	}
}

