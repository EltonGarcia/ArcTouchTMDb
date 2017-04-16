using System;
using System.Linq;
using MvvmCross.Plugins.Messenger;

namespace ArcTouchTMDb.Core
{
	public class MovieDetailsViewModel : BaseViewModel
	{
		private readonly ITMDbService _tmdbService;
		private readonly ISettingsService _settingsService;

		private MovieDetails _movie;

		public MovieDetails Movie
		{
			get { return _movie; }
			set
			{
				_movie = value;
				RaiseAllPropertiesChanged();
			}
		}

		public string Poster
		{
			get
			{
				return $"https://image.tmdb.org/t/p/w600/{Movie.backdrop_path}";
			}
		}

		public string ProductionCompanies
		{
			get
			{
				if (Movie.production_companies != null && Movie.production_companies.Any())
				{
					return string.Join(", ", Movie.production_companies.Select(o => o.name));
				}
				return string.Empty;
			}
		}

		public MovieDetailsViewModel(IMvxMessenger messenger, ITMDbService tmdbService, ISettingsService settingsService) : base(messenger, settingsService)
		{
			_tmdbService = tmdbService;
			_settingsService = settingsService;
		}

		public async void Init(int movieId)
		{
			Movie = await _tmdbService.GetMovieDetails(new Services.API.Request.MovieDetailsRequest(Settings, movieId));
		}
	}
}
