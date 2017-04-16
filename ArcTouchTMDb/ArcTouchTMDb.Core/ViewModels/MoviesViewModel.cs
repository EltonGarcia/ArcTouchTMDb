using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using ArcTouchTMDb.Core.Services.API.Request;
using Sequence.Plugins.InfiniteScroll;

namespace ArcTouchTMDb.Core
{
	public class MoviesViewModel : BaseViewModel
	{
		private readonly ITMDbService _tmdbService;
		private readonly ISettingsService _settingsService;
		private readonly IIncrementalCollectionFactory _incrementalCollectionFactory;

		private Settings _settings;
		private ObservableCollection<Movie> _movies;
		private int _page = 0;
		private int? _totalPages = null;

		public ObservableCollection<Movie> Movies
		{
			get 
			{
				if (_movies == null)
				{
					_movies = _incrementalCollectionFactory.GetCollection(async (count, pageSize) => 
					{
						ObservableCollection<Movie> newMovies = new ObservableCollection<Movie>();

						var request = new DiscoverRequest(_settings);
						request.page = ++_page;

						var response = await _tmdbService.Discover(request);
						if (response != null)
						{
							_totalPages = response.total_pages;

							foreach (var movie in response.results)
								newMovies.Add(movie);
						}

						return newMovies;
					}, 20);
				}
				return _movies; 
			}
		}

		public MoviesViewModel(IMvxMessenger messenger, ITMDbService tmdbService, ISettingsService settingsService, IIncrementalCollectionFactory incrementalCollectionFactory) : base(messenger)
		{
			_tmdbService = tmdbService;
			_settingsService = settingsService;
			_settings = _settingsService.GetSettings();
			_incrementalCollectionFactory = incrementalCollectionFactory;
		}
	}
}
