using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class PasswordReset : ContentPage
	{
		public PasswordReset ()
		{
			InitializeComponent ();
		}

		async void OnReset (object sender, EventArgs e)
		{
			App.Parse.passwordReset (email.Text);
			await Navigation.PopModalAsync ();
		}

		async void OnCancel (object sender, EventArgs e)
		{
			await Navigation.PopModalAsync ();
		}


	}
}

