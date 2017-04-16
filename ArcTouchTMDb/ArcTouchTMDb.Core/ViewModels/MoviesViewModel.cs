using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Threading;
using MvvmCross.Plugins.Messenger;
using ArcTouchTMDb.Core.Services.API.Request;
using Sequence.Plugins.InfiniteScroll;
using MvvmCross.Core.ViewModels;
using System.Linq;

namespace ArcTouchTMDb.Core
{
	/// <summary>
	/// Movies view model.
	/// </summary>
	public class MoviesViewModel : BaseViewModel
	{
		private readonly ITMDbService _tmdbService;
		private readonly IIncrementalCollectionFactory _incrementalCollectionFactory;

		private ObservableCollection<Movie> _movies;
		private int _page = 0;
		private int _pageSize = 20;
		private int? _totalPages = null;
		private bool _listLoaded;
		private object _lockObject = new object();

		public bool ShowProgress
		{
			get
			{
				return _movies == null || !_movies.Any();
			}
		}

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
		/// The movies list.
		/// </summary>
		/// <value>The movies.</value>
		public ObservableCollection<Movie> Movies
		{
			get
			{
				var hasNextPage = !_totalPages.HasValue || (_totalPages.HasValue && _totalPages.Value > _page);
				if (_movies == null && hasNextPage)
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

			var request = new DiscoverRequest(Settings);
			request.page = ++_page;

			var response = await _tmdbService.Discover(request);
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
		/// Initializes a new instance of the <see cref="T:ArcTouchTMDb.Core.MoviesViewModel"/> class.
		/// </summary>
		/// <param name="messenger">Messenger.</param>
		/// <param name="tmdbService">Tmdb service.</param>
		/// <param name="settingsService">Settings service.</param>
		/// <param name="incrementalCollectionFactory">Incremental collection factory.</param>
		public MoviesViewModel(IMvxMessenger messenger, ITMDbService tmdbService, ISettingsService settingsService, IIncrementalCollectionFactory incrementalCollectionFactory)
			: base(messenger, settingsService)
		{
			_tmdbService = tmdbService;
			_incrementalCollectionFactory = incrementalCollectionFactory;
		}

		public async override void Start()
		{
			base.Start();
			ListLoaded = false;
			await CheckListLoad();
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
