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
			await Navigation.PushAsync(new FieldOrder());

		}

		async void OnEquipmentRequestSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new EquipmentRequest());
		}
		void OnBillingFormSubmit (object sender, EventArgs e)
		{

		}
		void OnSuppliesRequestSubmit (object sender, EventArgs e)
		{

		}
		async void OnlogoutButton (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
			App.Parse.LogOutAsync ();
		}

	}
}

