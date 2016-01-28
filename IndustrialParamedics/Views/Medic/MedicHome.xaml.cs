using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class MedicHome : ContentPage
	{
		public MedicHome ()
		{
			InitializeComponent ();
			welcomeText.Text += ": " + App.currentUser.userName;
		}

		async void OnActivitySubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ActivitySubmission());
		}

		async void OnVehicleRequestSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new VehicleRequest());
		}
		void OnTimeEntrySubmit (object sender, EventArgs e)
		{
			DisplayAlert ("Time Entry", "Coming Soon in V2 Time Entry","OK");
		}
		async void OnInventoryOrderSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new InventoryOrder());
		}
		async void OnlogoutButton (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
			App.Parse.LogOutAsync ();
		}

	}
}

