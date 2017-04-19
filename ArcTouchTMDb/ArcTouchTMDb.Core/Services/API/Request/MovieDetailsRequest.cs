﻿namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class MovieDetailsRequest : BaseRequest
	{
		public int movie_id
		{
			get;
			set;
		}

		protected override string action => string.Format("/movie/{0}", movie_id);

		public MovieDetailsRequest(Settings settings, int movieId) : base(settings)
		{
			movie_id = movieId;
		}
	}
}
