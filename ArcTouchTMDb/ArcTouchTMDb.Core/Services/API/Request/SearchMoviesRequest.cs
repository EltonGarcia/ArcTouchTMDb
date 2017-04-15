using System;
using System.Collections.Generic;

namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class SearchMoviesRequest : BaseRequest
	{
		public string query { get; set; }
		public int page { get; set; }

		protected override string action => "/search/movie";

		protected override Dictionary<string, string> parameters
		{
			get
			{
				return new Dictionary<string, string>
				{
					{"page", page.ToString()},
					{"queryby", query}
				};
			}
		}

		public SearchMoviesRequest(Settings settings) : base(settings)
		{
		}

	}
}
