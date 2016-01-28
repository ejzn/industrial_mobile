using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class CustomerHome : ContentPage
	{
		public CustomerHome ()
		{
			InitializeComponent ();
			welcomeText.Text += ": " + App.currentUser.userName;
		}

		async void OnSuppliesRequestSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new VehicleRequest());
		}
		async void OnsiteReports (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ReportsForm());
		}
		async void OnlogoutButton (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
			App.Parse.LogOutAsync ();
		}

	}
}

