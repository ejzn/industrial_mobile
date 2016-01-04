using System;
using System.Collections.Generic;
using Xamarin.Forms;


namespace IndustrialParamedics
{
	public partial class ReportsForm : ContentPage
	{

		public ReportsForm ()
		{
			InitializeComponent ();
			InitializeFieldValues ();
		}


		void InitializeFieldValues ()
		{
			App.Parse.query(delegate (IList<string> results) {

				foreach (var client in results) 
				{
					siteId.Items.Add(client);
				}
				if(siteId.Items.Count > 0) {
					siteId.SelectedIndex = 0;
				}
			});
		}

		void OnSubmit (object sender, EventArgs e)
		{
			DisplayAlert ("test", App.currentUser.customerId, "ok");
			// Pop the report that is generated
		}


	}
}

