using System;
using MvvmCross.Core.ViewModels;
using MvvmCross.Platform;
using MvvmCross.Platform.IoC;
using Sequence.Plugins.InfiniteScroll;
using Sequence.Plugins.InfiniteScroll.Shared;

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

			Mvx.RegisterType<IIncrementalCollectionFactory, IncrementalCollectionFactory>();

			RegisterAppStart(new AppStart());
		}
	}
}
