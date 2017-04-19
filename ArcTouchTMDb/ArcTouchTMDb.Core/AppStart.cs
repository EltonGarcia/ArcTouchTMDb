using System;
using MvvmCross.Core.ViewModels;

namespace ArcTouchTMDb.Core
{
	public class AppStart : MvxNavigatingObject, IMvxAppStart
	{
		public void Start(object hint = null)
		{
			ShowViewModel<MoviesViewModel>();
		}
	}
}
