using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using Syncfusion.SfChart.XForms;
using Syncfusion.SfChart.XForms.iOS;
using Parse;


namespace IndustrialParamedics.iOS
{
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
	{
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			global::Xamarin.Forms.Forms.Init ();

			new SfChartRenderer();

			ParseClient.Initialize ("Z6950gFV1RIPa66H4TKH23Fc5XDhPB2tGqC0Q27U", "x1BQxvzB8RubGSyqPnWoAX9PWkWLGzD9MJcJ3ACz");

			// Code for starting up the Xamarin Test Cloud Agent
			#if ENABLE_TEST_CLOUD
			Xamarin.Calabash.Start();
			#endif

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}

