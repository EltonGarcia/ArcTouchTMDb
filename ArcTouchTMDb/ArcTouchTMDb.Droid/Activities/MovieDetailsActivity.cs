
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ArcTouchTMDb.Core;

namespace ArcTouchTMDb.Droid
{
	[Activity(Label = "ArcTouchTMDb.Droid", Icon = "@mipmap/icon", Theme = "@style/TMDb.Theme",
			  WindowSoftInputMode = SoftInput.AdjustPan | SoftInput.StateHidden)]
	public class MovieDetailsActivity : BaseActivity<MovieDetailsViewModel>
	{
		protected override int ActivityLayoutId => Resource.Layout.MovieDetails;
	}
}
