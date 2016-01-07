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

		async void OnSafetyTrackingSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new ActivitySubmission());
		}

		async void OnEquipmentRequestSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SafetyRequest());
		}
		void OnBillingFormSubmit (object sender, EventArgs e)
		{
			//TODO: Insert billing submission here
		}
		async void OnSuppliesRequestSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FieldOrder());
		}
		async void OnlogoutButton (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
			App.Parse.LogOutAsync ();
		}

	}
}

