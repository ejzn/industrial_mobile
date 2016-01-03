using System;
using System.Collections.Generic;

namespace IndustrialParamedics
{
	public interface IParseSDK
	{
		// Login / Log out interfaces
		bool checkLogin();
		void LogInAsync (string username, string password, Action<bool> callback);
		void LogOutAsync ();
		User getCurrentUser();
		void sendEmail(string destEmail, int formId);

		void query (string objectName, Action<IDictionary<string,string>> callback) ;

		void saveForm (Form form);

	}
}

