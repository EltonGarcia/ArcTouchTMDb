using System;
using System.Collections.Generic;

namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class DiscoverRequest : BaseRequest
	{
		public int page { get; set; }
		public string sort_by { get; set; }

		protected override string action => "/discover/movie";

		protected override Dictionary<string, string> parameters
		{
			get
			{
				return new Dictionary<string, string> 
				{
					{"page", page.ToString()},
					{"sort_by", sort_by}
				};
			}
		}

		public DiscoverRequest(Settings settings) : base(settings)
		{
		}
	}
}
