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

		async void OnSuppliesRequestSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new FieldOrder());

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

	}
}

