using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class HomePage : ContentPage
	{
		public HomePage ()
		{
			InitializeComponent ();

			App.Parse = DependencyService.Get<IParseSDK> ();

			if (App.Parse != null && App.Parse.checkLogin ()) {
				// TODO: Check if they are a medic or not, use sometype of configurations :)
				App.currentUser = App.Parse.getCurrentUser();
				Navigation.PushModalAsync(new NavigationPage(new MedicHome ()));

			}
		}

		private void LoginCompleted(bool result)
		{
			if (result) {
				App.currentUser = App.Parse.getCurrentUser ();
				Navigation.PushModalAsync (new NavigationPage (new MedicHome ()));
			} else {
				DisplayAlert ("Login Error", "We couldn't log you in, check your connectivity or credentials. Please contact mobile@ips.com for help", "OK");
			}
		}

		void OnLogin (object sender, EventArgs e)
		{
			if (App.Parse != null) {
				App.Parse.LogInAsync (usernameText.Text, passwordText.Text, this.LoginCompleted);
			} else {
				DisplayAlert ("Connectivity Error", "The system could not initialize, please try again", "OK");	
			}
		}
	}
}