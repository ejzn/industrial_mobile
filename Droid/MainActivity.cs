using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Parse;

namespace IndustrialParamedics.Droid
{
	[Activity (Label = "IndustrialParamedics.Droid", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
	public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsApplicationActivity
	{
		protected override void OnCreate (Bundle bundle)
		{
			base.OnCreate (bundle);

			global::Xamarin.Forms.Forms.Init (this, bundle);

			ParseClient.Initialize ("Z6950gFV1RIPa66H4TKH23Fc5XDhPB2tGqC0Q27U", "x1BQxvzB8RubGSyqPnWoAX9PWkWLGzD9MJcJ3ACz");
			LoadApplication (new App ());
		}
	}
}

