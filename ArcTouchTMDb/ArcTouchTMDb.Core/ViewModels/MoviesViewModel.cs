using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using MvvmCross.Plugins.Messenger;
using ArcTouchTMDb.Core.Services.API.Request;
using Sequence.Plugins.InfiniteScroll;
using MvvmCross.Core.ViewModels;
using System.Linq;
using ArcTouchTMDb.Core.Services.API.Response;

namespace ArcTouchTMDb.Core
{
	/// <summary>
	/// Movies view model.
	/// </summary>
	public class MoviesViewModel : BaseViewModel
	{
		private readonly ITMDbService _tmdbService;
		private readonly IIncrementalCollectionFactory _incrementalCollectionFactory;
		private INetworkService _networkService;

		private ObservableCollection<Movie> _movies;
		private int _page = 0;
		private int _pageSize = 20;
		private int? _totalPages = null;
		private bool _listLoaded;
		private string _search;

		/// <summary>
		/// Gets a value indicating whether load in progress.
		/// </summary>
		/// <value><c>true</c> if show progress; otherwise, <c>false</c>.</value>
		public bool ShowProgress
		{
			get
			{
				return _movies == null || !_movies.Any();
			}
		}

		/// <summary>
		/// Gets or sets a value indicating the list was loaded.
		/// </summary>
		/// <value><c>true</c> if list loaded; otherwise, <c>false</c>.</value>
		public bool ListLoaded
		{
			get
			{
				return _listLoaded;
			}
			set
			{
				_listLoaded = value;
			}
		}

		/// <summary>
		/// Gets a value indicating empty results.
		/// </summary>
		/// <value><c>true</c> if empty results; otherwise, <c>false</c>.</value>
		public bool EmptyResults
		{
			get {
				return ListLoaded && (_movies == null || !_movies.Any());
			}
		}

		/// <summary>
		/// Gets or sets the search query.
		/// </summary>
		/// <value>The search.</value>
		public string Search
		{
			get { return _search; }
			set
			{
				if (_search != value)
				{
					_search = value;
					RaisePropertyChanged(() => Search);
				}
			}
		}

		/// <summary>
		/// The movies list.
		/// </summary>
		/// <value>The movies.</value>
		public ObservableCollection<Movie> Movies
		{
			get
			{
				var hasNextPage = !_totalPages.HasValue || (_totalPages.HasValue && _totalPages.Value > _page);
				if (_networkService.IsConnected && _movies == null && hasNextPage)
				{
					_movies = _incrementalCollectionFactory.GetCollection(async (count, pageSize) =>
					{
						return await GetMovies();
					}, _pageSize);
				}

				if (!ListLoaded)
				{
					ListLoaded = true;
				}
				return _movies;
			}
		}

		/// <summary>
		/// Gets the next movies list page.
		/// </summary>
		/// <returns>The movies.</returns>
		private async Task<ObservableCollection<Movie>> GetMovies()
		{
			ObservableCollection<Movie> newMovies = new ObservableCollection<Movie>();

			DiscoverResponse response;
			if (string.IsNullOrEmpty(Search))
			{
				var request = new DiscoverRequest(Settings);
				request.page = ++_page;
				response = await _tmdbService.Discover(request);
			}
			else
			{
				var request = new SearchMoviesRequest(Settings, Search);
				request.page = ++_page;
				response = await _tmdbService.Search(request);
			}

			if (response != null)
			{
				_totalPages = response.total_pages;

				foreach (var movie in response.results)
					newMovies.Add(movie);
			}

			return newMovies;
		}

		private MvxCommand<Movie> _movieDetailsCommand;
		public MvxCommand<Movie> MovieDetailsCommand
		{
			get
			{
				return _movieDetailsCommand = _movieDetailsCommand ??
					new MvxCommand<Movie>(movie =>
					{
						ShowViewModel<MovieDetailsViewModel>(new { movieId = movie.id });
					});
			}
		}

		/// <summary>
		/// The search movies command.
		/// </summary>
		/// <value>The search command.</value>
		public MvxCommand SearchCommand
		{
			get
			{
				return new MvxCommand(FindMovies);
			}
		}

		private void FindMovies()
		{
			_page = 0;
			_movies = null;
			RaisePropertyChanged(() => Movies);
		}

		/// <summary>
		/// The clear search command.
		/// </summary>
		/// <value>The clear search command.</value>
		public MvxCommand ClearSearchCommand
		{
			get
			{
				return new MvxCommand(()=> { 
					Search = null;
					FindMovies();
				});
			}
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="T:ArcTouchTMDb.Core.MoviesViewModel"/> class.
		/// </summary>
		/// <param name="messenger">Messenger.</param>
		/// <param name="tmdbService">Tmdb service.</param>
		/// <param name="settingsService">Settings service.</param>
		/// <param name="incrementalCollectionFactory">Incremental collection factory.</param>
		public MoviesViewModel(IMvxMessenger messenger, ITMDbService tmdbService, ISettingsService settingsService,
				       IIncrementalCollectionFactory incrementalCollectionFactory, INetworkService networkService)
			: base(messenger, settingsService)
		{
			_tmdbService = tmdbService;
			_incrementalCollectionFactory = incrementalCollectionFactory;
			_networkService = networkService;
		}

		public async override void Start()
		{
			base.Start();
			ListLoaded = false;
			if (_networkService.IsConnected)
			{
				await CheckListLoad();
			}
			else
			{
				_networkService.Subscribe(async (result) =>
				{
					if (result.Status.IsConnected)
					{
						RaisePropertyChanged(() => Movies);
						await CheckListLoad();
					}
				});
			}
		}

		private async Task CheckListLoad()
		{
			await Task.Delay(1000);
			if (!ShowProgress)
			{
				InvokeOnMainThread(() =>
				{
					RaisePropertyChanged(() => ListLoaded);
					RaisePropertyChanged(() => ShowProgress);
				});
			}
			else
			{
				await CheckListLoad();
			}
		}
	}
}
