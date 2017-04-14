using System;
namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class DiscoverRequest : BaseRequest
	{
		public int page { get; set; }
		public string sort_by { get; set; }

		public DiscoverRequest(Settings settings) : base(settings)
		{
		}
	}
}
