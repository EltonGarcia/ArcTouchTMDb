
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MvvmCross.Droid.Views;

namespace ArcTouchTMDb.Droid
{
	[Activity(MainLauncher = true,
			Label = "@string/app_name",
			Theme = "@style/Theme.Splash", NoHistory = true,
			ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation,
			ScreenOrientation = ScreenOrientation.Portrait)]
	public class SplashScreenActivity : MvxSplashScreenActivity
	{
		public SplashScreenActivity() : base(Resource.Layout.SplashScreen)
		{

		}
	}
}
