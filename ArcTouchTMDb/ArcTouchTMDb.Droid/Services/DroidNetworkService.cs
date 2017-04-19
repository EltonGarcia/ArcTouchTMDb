using System;
using System.Reflection;
using System.Threading.Tasks;
using Android.App;
using Android.Content;
using Android.Net;
using ArcTouchTMDb.Core;
using Java.Net;
using MvvmCross.Platform;
using MvvmCross.Platform.Droid;

namespace ArcTouchTMDb.Droid
{
	public class DroidNetworkService : AbstractNetworkService
	{
		public DroidNetworkService()
		{
			NetworkConnectionBroadcastReceiver.OnChange = x => this.SetFromInfo(x, true);


			Mvx.CallbackWhenRegistered<IMvxAndroidGlobals>(x =>
			{
				var manager = (ConnectivityManager)x.ApplicationContext.GetSystemService(Context.ConnectivityService);
				this.SetFromInfo(manager.ActiveNetworkInfo, false);
			});
		}


		private void SetFromInfo(NetworkInfo network, bool fireEvent)
		{
			this.SetStatus(
				network != null && network.IsConnected,
				(network != null && network.Type == ConnectivityType.Wifi),
				(network != null && network.Type == ConnectivityType.Mobile),
				fireEvent
			);
		}


		public override Task<bool> IsHostReachable(string host)
		{
			return Task.Run(() =>
			{
				try
				{
					return InetAddress
						.GetByName(host)
						.IsReachable(5000);
				}
				catch
				{
					return false;
				}
			});
		}
	}
}
