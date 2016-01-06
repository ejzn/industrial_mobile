using System;
using System.Collections.Generic;
using Syncfusion.SfChart.XForms;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace IndustrialParamedics
{

	public class ChartPoint
	{
		public DateTime Date { get; set; }
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
			siteChart.HeightRequest = Device.OnPlatform(550, 500, 550);
		}


		private ObservableCollection<ChartDataPoint> getData()
		{
			ObservableCollection<ChartDataPoint> datas = new ObservableCollection<ChartDataPoint>();
			datas.Add(new ChartDataPoint(1, 59));
			datas.Add(new ChartDataPoint(2, 59));
			datas.Add(new ChartDataPoint(3, 59));
			datas.Add(new ChartDataPoint(4, 59));
			datas.Add(new ChartDataPoint(5, 59));
			datas.Add(new ChartDataPoint(6, 59));
			datas.Add(new ChartDataPoint(7, 60));
			datas.Add(new ChartDataPoint(8, 59));
			datas.Add(new ChartDataPoint(9, 56));
			datas.Add(new ChartDataPoint(10, 56));
			datas.Add(new ChartDataPoint(11, 56));
			datas.Add(new ChartDataPoint(12, 56));
			datas.Add(new ChartDataPoint(13, 56));
			datas.Add(new ChartDataPoint(14, 59));
			datas.Add(new ChartDataPoint(15, 59));
			datas.Add(new ChartDataPoint(16, 59));
			datas.Add(new ChartDataPoint(17, 19));
			datas.Add(new ChartDataPoint(18, 19));
			datas.Add(new ChartDataPoint(19, 49));
			datas.Add(new ChartDataPoint(20, 59));
			datas.Add(new ChartDataPoint(21, 69));
			datas.Add(new ChartDataPoint(22, 89));
			datas.Add(new ChartDataPoint(23, 59));
			datas.Add(new ChartDataPoint(24, 59));
			datas.Add(new ChartDataPoint(25, 18));
			datas.Add(new ChartDataPoint(26, 8));
			datas.Add(new ChartDataPoint(27, 8));
			datas.Add(new ChartDataPoint(28, 8));
			datas.Add(new ChartDataPoint(29, 8));
			datas.Add(new ChartDataPoint(30, 39));
			datas.Add(new ChartDataPoint(31, 39));
			datas.Add(new ChartDataPoint(32, 39));
			datas.Add(new ChartDataPoint(33, 39));
			datas.Add(new ChartDataPoint(34, 55));
			datas.Add(new ChartDataPoint(35, 55));
			datas.Add(new ChartDataPoint(36, 55));
			datas.Add(new ChartDataPoint(37, 55));
			datas.Add(new ChartDataPoint(38, 59));
			datas.Add(new ChartDataPoint(39, 62));
			datas.Add(new ChartDataPoint(40, 62));
			return datas;
		}


		private void setupDataPoints ()
		{
			App.Parse.querySiteData (siteId, delegate (IList<ChartPoint> results) {
				siteChart.Series.Add (new ColumnSeries () {
					ItemsSource = getData()
				});
			});
		}
	}
}

