using System;
using Parse;
using UIKit;
using Xamarin.Forms;
using IndustrialParamedics.iOS;
using System.Collections.Generic;

[assembly: Dependency(typeof(ParseSDK))]

namespace IndustrialParamedics.iOS
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
		public void LogOutAsync () 
		{
			ParseUser.LogOut();
		}
		public User getCurrentUser()
		{
			if (ParseUser.CurrentUser != null) {
				string role = ParseUser.CurrentUser.Get<string>("userRole");
				User.Role roleEnum;

				if (role != null && role.Length > 0) {
					roleEnum = (User.Role)Enum.Parse (typeof(User.Role), role);
				} else {
					roleEnum = User.Role.customer;
				}

				return new User (ParseUser.CurrentUser.Username, ParseUser.CurrentUser.ObjectId, roleEnum);
			}

			return null;
		}

		public async void sendEmail(string destEmail, int formId)
		{
			await ParseCloud.CallFunctionAsync<string>("sendEmail",new Dictionary<string, object>());
		}

		public async void query (string objectName, Action<IDictionary<string,string>> callback) 
		{
			var query = ParseObject.GetQuery (objectName);
			IEnumerable<Object> results = await query.FindAsync ();
			IDictionary<string,string> clients = new Dictionary<string, string>();

			foreach (ParseObject client in results) {
				if (client.ContainsKey("Name")) {
					clients.Add (new KeyValuePair<string, string> (client.Get<string>("Name"), client.ObjectId));
				}
			}
			callback(clients);
		}

		public async void saveForm (Form form)
		{
			var activity = new ParseObject(Form.ParseRelation[form.formType]);
			foreach (KeyValuePair<string, string> entry in form.elements) {
				int number;
				DateTime dateVal;

				activity [entry.Key] = entry.Value;

				if (entry.Key == "custJobId" || entry.Key == "ipsJobId" || entry.Key == "medicId" || entry.Key == "clientId")
					continue;

				if (Int32.TryParse (entry.Value, out number))
					activity [entry.Key] = number;

				if (DateTime.TryParse (entry.Value, out dateVal))
					activity [entry.Key] = dateVal;
			
			}
			await activity.SaveAsync();
		}
	}
}

