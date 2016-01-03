using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class AdminHome : ContentPage
	{
		public AdminHome ()
		{
			InitializeComponent ();
			welcomeText.Text += ": " + App.currentUser.userName;
		}

		void OnSuppliesRequestSubmit (object sender, EventArgs e)
		{

		}
		void OnEquipmentRequestSubmit (object sender, EventArgs e)
		{
		}
		void OnBillingFormSubmit (object sender, EventArgs e)
		{

		}
		void OnSafetyTrackingSubmit (object sender, EventArgs e)
		{

		}
		async void OnlogoutButton (object sender, EventArgs e)
		{
			await this.Navigation.PopModalAsync ();
			App.Parse.LogOutAsync ();
		}

	}
}

