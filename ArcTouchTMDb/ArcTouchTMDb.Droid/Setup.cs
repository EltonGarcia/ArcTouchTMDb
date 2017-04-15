using System;
using Android.Content;
using ArcTouchTMDb.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;

namespace ArcTouchTMDb.Droid
{
	public class Setup : MvxAppCompatSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{

		}

		protected override IMvxApplication CreateApp() => new App();
	}
}
