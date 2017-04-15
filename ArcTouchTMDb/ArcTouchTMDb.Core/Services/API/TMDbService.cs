using System;
using System.Net.Http;
using System.Threading.Tasks;
using ArcTouchTMDb.Core.Services.API.Request;
using ArcTouchTMDb.Core.Services.API.Response;

namespace ArcTouchTMDb.Core
{
	public class TMDbService : RestAPIService, ITMDbService
	{
		public TMDbService()
		{
		}

		public Task<DiscoverResponse> Discover(DiscoverRequest request)
		{
			return Request<DiscoverResponse>(request);
		}

		public Task<MovieDetailsResponse> GetMovieDetails(MovieDetailsRequest request)
		{
			return Request<MovieDetailsResponse>(request);
		}

		public Task<SearchMoviesResponse> Search(SearchMoviesRequest request)
		{
			return Request<SearchMoviesResponse>(request);
		}
	}
}
