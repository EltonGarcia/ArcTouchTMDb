using System;
using Android.Content;
using ArcTouchTMDb.Core;
using MvvmCross.Core.ViewModels;
using MvvmCross.Droid.Support.V7.AppCompat;
using MvvmCross.Platform;

namespace ArcTouchTMDb.Droid
{
	public class Setup : MvxAppCompatSetup
	{
		public Setup(Context applicationContext) : base(applicationContext)
		{
		}

		protected override IMvxApplication CreateApp() => new App();

		protected override void InitializeIoC()
		{
			base.InitializeIoC();
			Mvx.RegisterType<INetworkService, DroidNetworkService>();
		}
	}
}
