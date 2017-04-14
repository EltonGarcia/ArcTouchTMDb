using System;
namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class SearchMoviesRequest : BaseRequest
	{
		public string query { get; set; }
		public int page { get; set; }

		public SearchMoviesRequest(Settings settings) : base(settings)
		{
		}
	}
}
