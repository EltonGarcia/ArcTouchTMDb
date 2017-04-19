﻿using System;
using System.ComponentModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using MvvmCross.Platform;

namespace ArcTouchTMDb.Core
{
	public abstract class AbstractNetworkService : INetworkService, INotifyPropertyChanged
	{

		protected void SetStatus(bool connected, bool wifi, bool mobile, bool fireEvent)
		{
			this.IsConnected = connected;
			this.IsWifi = wifi;
			this.IsMobile = mobile;

			if (fireEvent)
			{
				Mvx
					.Resolve<IMvxMessenger>()
					.Publish(new NetworkStatusChangedMessage(this));
			}
		}

		#region INetworkService Members

		public abstract Task<bool> IsHostReachable(string host);


		private bool connected;
		public bool IsConnected
		{
			get { return this.connected; }
			private set
			{
				if (this.connected == value)
					return;

				this.connected = value;
				this.OnPropertyChanged("IsConnected");
			}
		}


		private bool wifi;
		public bool IsWifi
		{
			get { return this.wifi; }
			private set
			{
				if (this.wifi == value)
					return;

				this.wifi = value;
				this.OnPropertyChanged("IsWifi");
			}
		}


		private bool mobile;
		public bool IsMobile
		{
			get { return this.mobile; }
			private set
			{
				if (this.mobile == value)
					return;

				this.mobile = value;
				this.OnPropertyChanged("IsMobile");
			}
		}


		public MvxSubscriptionToken Subscribe(Action<NetworkStatusChangedMessage> action)
		{
			return Mvx
				.Resolve<IMvxMessenger>()
				.Subscribe<NetworkStatusChangedMessage>(action);
		}

		#endregion

		#region INotifyPropertyChanged Members

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string propertyName)
		{
			if (this.PropertyChanged != null)
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}

		#endregion
	}
}
