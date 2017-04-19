using System;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
namespace ArcTouchTMDb.Core
{
	public interface INetworkService
	{
		bool IsConnected { get; }
		bool IsWifi { get; }
		bool IsMobile { get; }
		Task<bool> IsHostReachable(string host);
		MvxSubscriptionToken Subscribe(Action<NetworkStatusChangedMessage> action);
	}
}
