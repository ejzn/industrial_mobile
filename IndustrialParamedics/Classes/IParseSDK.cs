using System;
using System.Collections.Generic;
using System.IO;

namespace IndustrialParamedics
{
	public interface IParseSDK
	{
		// Login / Log out interfaces
		bool checkLogin();
		void LogInAsync (string username, string password, Action<bool> callback);
		void LogOutAsync ();
		User getCurrentUser();

		void signUp (string username, string password, string email, string role, Action<bool> callback);
		void passwordReset (string email);


		void sendSignupNotification(string destEmail, string username);
		void sendEmail(string destEmail, string serverFileId);
		void sendFieldOrder(string destEmail, string serverFileId);

		void query (string objectName, Action<IDictionary<string,string>> callback);

		void query (Action<IList<string>> callback);

		void querySiteData (string custJobId, Action<IList<ChartPoint>> callback);

		void saveForm (Form form);

		void saveFile (string fileName, Stream stream, Action<string> callback);

		String getFieldOrderEmail ();
		String getVehicleRequestEmail ();
	}
}

