using System;
using Xamarin.Forms;


namespace IndustrialParamedics
{
	public class App : Application
	{
		public static string userID;
		public static string userDisplayName;
		public static IParseSDK Parse;
		public static User currentUser;

		public App ()
		{
			MainPage = new IndustrialParamedics.HomePage ();
		}

		protected override void OnStart ()
		{
			App.Parse = DependencyService.Get<IParseSDK> ();
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}