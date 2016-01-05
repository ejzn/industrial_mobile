using System;
using System.Collections.Generic;
using Syncfusion.SfChart.XForms;

using Xamarin.Forms;

namespace IndustrialParamedics
{

	public class ChartPoint
	{
		public string Date { get; set; }
		public string Medic { get; set; }
		public string Activity { get; set; }
		public int Count { get; set; }
	}


	public partial class SiteReport : ContentPage
	{
		private ICollection<ChartPoint> points;
		private string siteId;

		public SiteReport (string siteId)
		{
			InitializeComponent ();
			this.siteId = siteId;
			siteChart.Title= new ChartTitle(){Text=siteId};
			points = new List<ChartPoint> ();
			setupDataPoints ();
		}

		private void setupDataPoints ()
		{
			App.Parse.querySiteData (siteId, delegate (IList<ChartPoint> results) {
				siteChart.Series.Add (new ColumnSeries () {
					ItemsSource = this.points
				});
			});
		}
	}
}

