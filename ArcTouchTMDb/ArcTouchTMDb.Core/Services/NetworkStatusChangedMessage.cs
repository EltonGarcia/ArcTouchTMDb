using System;
using MvvmCross.Plugins.Messenger;

namespace ArcTouchTMDb.Core
{
	public class NetworkStatusChangedMessage : MvxMessage
	{

		public INetworkService Status { get; private set; }


		public NetworkStatusChangedMessage(INetworkService networkService) : base(networkService)
		{
			this.Status = networkService;
		}
	}
}
