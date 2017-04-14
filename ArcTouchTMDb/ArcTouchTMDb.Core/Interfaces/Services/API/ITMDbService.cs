using System;
using System.Threading.Tasks;
using ArcTouchTMDb.Core.Services.API.Request;
using ArcTouchTMDb.Core.Services.API.Response;

namespace ArcTouchTMDb.Core
{
	public interface ITMDbService
	{
		 Task<DiscoverResponse> Discover(DiscoverRequest request);
		 Task<SearchMoviesResponse> Search(SearchMoviesRequest request);
		 Task<MovieDetailsResponse> GetMovieDetails(MovieDetailsRequest request);
	}
}
