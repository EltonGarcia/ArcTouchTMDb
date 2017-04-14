using System;
namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class MovieDetailsRequest : BaseRequest
	{
		public int movie_id
		{
			get;
			set;
		}
		public MovieDetailsRequest(Settings settings) : base(settings)
		{
		}
	}
}
