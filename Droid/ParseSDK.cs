using System;
using Parse;
using Xamarin.Forms;
using IndustrialParamedics.Droid;
using System.Collections.Generic;
using System.IO;

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
					
				string customerId = null;
				if(ParseUser.CurrentUser.ContainsKey("customerId")) {
					customerId = ParseUser.CurrentUser.Get<string>("customerId");
				}

				return new User (ParseUser.CurrentUser.Username, ParseUser.CurrentUser.ObjectId, roleEnum, customerId);
			}

			return null;
		}

		public async void sendEmail(string destEmail, string serverFileId)
		{
			var dict = new Dictionary<string, object> { { "email" , destEmail }, { "serverFileUrl", serverFileId } };
			await ParseCloud.CallFunctionAsync<string>("sendEquipmentRequest", dict);
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
		public async void query (Action<IList<string>> callback)
		{
			if (App.currentUser.customerId != null) {
				var query = ParseObject.GetQuery ("Activity")
					.WhereEqualTo ("clientId", App.currentUser.customerId);
				IEnumerable<Object> results = await query.FindAsync ();
				IList<string> jobs = new List<string> ();

				foreach (ParseObject activity in results) {
					if (activity.ContainsKey ("custJobId") && !jobs.Contains(activity ["custJobId"].ToString())) {
						jobs.Add (activity ["custJobId"].ToString());
					}
				}
				callback (jobs);
			}
		}

		public async void querySiteData (string custJobId, Action<IList<ChartPoint>> callback)
		{
			if (App.currentUser.customerId != null) {
				var query = ParseObject.GetQuery ("Activity")
					.WhereEqualTo ("custJobId", custJobId);
				IEnumerable<Object> results = await query.FindAsync ();
				IList<ChartPoint> points = new List<ChartPoint> ();

				foreach (ParseObject activity in results) {
					if (activity.ContainsKey ("activityDate") && activity.ContainsKey ("medicId")) {
						var point = new ChartPoint ();
						point.Activity = "Company Orientation";
						point.Count = 5;
						point.Date = activity.Get<string> ("activityDate");
						point.Medic = activity.Get<string> ("medicId");
					}

				}

				callback (points);
			}
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

		public async void saveFile (string fileName, Stream stream, Action<string> callback)
		{
			if (stream.CanRead) {
				var file = new ParseFile (fileName, stream);
				await file.SaveAsync ();
				callback (file.Url.AbsoluteUri.ToString ());
			}
		}
	}
}

