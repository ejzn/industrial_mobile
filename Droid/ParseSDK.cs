using System;
using Parse;
using Xamarin.Forms;
using IndustrialParamedics.Droid;
using System.Collections.Generic;

[assembly: Dependency(typeof(ParseSDK))]

namespace IndustrialParamedics.Droid
{
	public class ParseSDK : IParseSDK
	{
		public bool checkLogin ()
		{
			if (ParseUser.CurrentUser != null)
			{
				return true;
			}
			return false;
		}
		public async void LogInAsync (string username, string password, Action<bool> callback) 
		{
			try
			{
				await ParseUser.LogInAsync(username, password);
				callback(true);
			}
			catch
			{
				// Need to do something here....
				callback(false);
			}
		}
		public void LogOutAsync (string username) 
		{
			ParseUser.LogOut();
		}
		public User getCurrentUser()
		{
			if (ParseUser.CurrentUser != null) {
				return new User (ParseUser.CurrentUser.Username, ParseUser.CurrentUser.ObjectId);
			}

			return null;
		}

		public async void sendEmail(string destEmail, int formId)
		{
			await ParseCloud.CallFunctionAsync<string>("sendEmail", new Dictionary<string, object>());
		}
	}
}

