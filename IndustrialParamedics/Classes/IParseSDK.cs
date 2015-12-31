using System;

namespace IndustrialParamedics
{
	public interface IParseSDK
	{
		// Login / Log out interfaces
		bool checkLogin();
		void LogInAsync (string username, string password, Action<bool> callback);
		void LogOutAsync (string username);
		User getCurrentUser();
		void sendEmail(string destEmail, int formId);

	}
}

