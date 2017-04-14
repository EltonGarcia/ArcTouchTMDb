using System.Collections.Generic;

namespace ArcTouchTMDb.Core.Services.API.Response
{
	public class DiscoverResponse : BaseResponse
	{
		public int page { get; set; }
		public List<Movie> results { get; set; }
		public int total_results { get; set; }
		public int total_pages { get; set; }
	}
}
