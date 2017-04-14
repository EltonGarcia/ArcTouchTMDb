using System;
using System.Net.Http;
using System.Threading.Tasks;
using ArcTouchTMDb.Core.Services.API.Request;
using ArcTouchTMDb.Core.Services.API.Response;

namespace ArcTouchTMDb.Core
{
	public class TMDbService : ITMDbService
	{
		public TMDbService()
		{
		}

		public Task<DiscoverResponse> Discover(DiscoverRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<MovieDetailsResponse> GetMovieDetails(MovieDetailsRequest request)
		{
			throw new NotImplementedException();
		}

		public Task<SearchMoviesResponse> Search(SearchMoviesRequest request)
		{
			throw new NotImplementedException();
		}
	}
}
