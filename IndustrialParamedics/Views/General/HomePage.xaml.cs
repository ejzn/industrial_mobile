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
				redirectToLogin ();
			}
		}

		private void redirectToLogin () 
		{
			if (App.currentUser == null || !App.currentUser.enabled) {
				DisplayAlert ("Login Denied", "Your user exists, but is not enabled yet. Please contact admin@ipsems.com.","OK");
				return;
			}

			if (App.currentUser.role == User.Role.medic) {
				Navigation.PushModalAsync (new NavigationPage (new MedicHome ()));
			}

			if (App.currentUser.role == User.Role.customer) {
				Navigation.PushModalAsync (new NavigationPage (new CustomerHome ()));
			}

			if (App.currentUser.role == User.Role.admin) {
				Navigation.PushModalAsync (new NavigationPage (new AdminHome ()));
			}
		}
		private void LoginCompleted(bool result)
		{
			if (result) {
				App.currentUser = App.Parse.getCurrentUser ();
				redirectToLogin ();
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

		void OnSignUp (object sender, EventArgs e)
		{
			Navigation.PushModalAsync (new NavigationPage (new SignUp ()));
		}

		void OnPasswordReset (object sender, EventArgs e)
		{
			Navigation.PushModalAsync (new NavigationPage (new PasswordReset ()));
		}


	}
}