using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace IndustrialParamedics
{
	public partial class ReportsForm : ContentPage
	{

		public ICollection<ChartPoint> SiteData { get; set; }

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
					jobId.Items.Add(client);
				}
				if(jobId.Items.Count > 0) {
					jobId.SelectedIndex = 0;
				}
			});
		}

		async void OnSubmit (object sender, EventArgs e)
		{
			await Navigation.PushAsync(new SiteReport(10));
		}


	}
}

