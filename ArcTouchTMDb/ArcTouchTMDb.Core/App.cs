using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform.IoC;

namespace ArcTouchTMDb.Core
{
	public class App : MvxApplication
	{
		public override void Initialize()
		{
			base.Initialize();

			CreatableTypes()
			 .EndingWith("Service")
			 .AsInterfaces()
			 .RegisterAsLazySingleton();

			RegisterAppStart(new AppStart());
		}
	}
}
