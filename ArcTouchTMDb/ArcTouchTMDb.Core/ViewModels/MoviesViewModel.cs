using System.Collections.ObjectModel;
using System.Threading.Tasks;
using MvvmCross.Plugins.Messenger;
using ArcTouchTMDb.Core.Services.API.Request;

namespace ArcTouchTMDb.Core
{
	public class MoviesViewModel : BaseViewModel
	{
		private ITMDbService _tmdbService;
		private ISettingsService _settingsService;

		private Settings _settings;
		private ObservableCollection<Movie> _movies = new ObservableCollection<Movie>();
		private int _page = 1;
		private int? _totalPages = null;

		public ObservableCollection<Movie> Movies
		{
			get { return _movies; }
			set
			{
				_movies = value;
				RaisePropertyChanged(() => Movies);
			}
		}

		public MoviesViewModel(IMvxMessenger messenger, ITMDbService tmdbService, ISettingsService settingsService) : base(messenger)
		{
			_tmdbService = tmdbService;
			_settingsService = settingsService;
			_settings = _settingsService.GetSettings();
		}

		public override async void Start()
		{
			base.Start();
			await ReloadDataAsync();
			//await LoadMovies();
		}

		public async Task LoadMovies()
		{
			await RequestAndRefresh(new DiscoverRequest(_settings));
		}

		protected override async Task InitializeAsync()
		{
			await RequestAndRefresh(new DiscoverRequest(_settings));
		}

		private async Task NextPage()
		{
			if (_totalPages.HasValue && _totalPages.Value > _page)
			{
				var request = new DiscoverRequest(_settings);
				request.page = _page;
				await RequestAndRefresh(request);
			}
		}

		private async Task RequestAndRefresh(DiscoverRequest request)
		{
			var response = await _tmdbService.Discover(request);
			if (response != null)
			{
				_totalPages = response.total_pages;

				foreach (var movie in response.results)
					_movies.Add(movie);
				Movies = _movies;
			}
		}
	}
}
