using Android.App;
using Android.Widget;
using Android.OS;
using ArcTouchTMDb.Core;
using System;
using Android.Views;

namespace ArcTouchTMDb.Droid
{
	[Activity(Label = "ArcTouchTMDb.Droid", Icon = "@mipmap/icon", Theme = "@style/TMDb.Theme", 
	          WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
	public class MainActivity : BaseActivity<MoviesViewModel>
	{
		protected override int ActivityLayoutId => Resource.Layout.Main;
	}
}

