using System;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace IndustrialParamedics
{


	public partial class SignUp : ContentPage
	{
		public SignUp ()
		{
			InitializeComponent ();

		}
			

		async void OnSignUp (object sender, EventArgs e)
		{

			if (email.Text != confirmEmail.Text) {

				await DisplayAlert ("Email Error", "Emails must match", "OK");
				return;
			}

			if (password.Text != passwordConfirm.Text) {

				await DisplayAlert ("Password Error", "Passwords must match", "OK");
				return;
			}
				
			App.Parse.signUp (username.Text, password.Text, email.Text, "Medic", delegate (bool results) {
				if(results) {
					Navigation.PopModalAsync ();
					DisplayAlert ("Sign Up Success", "Please contact your administrator to confirm your account", "OK");
				} else {
					DisplayAlert ("Sign Up Error", "Check your username/email if it is already in use", "OK");
				}
			});
		}
	}
}