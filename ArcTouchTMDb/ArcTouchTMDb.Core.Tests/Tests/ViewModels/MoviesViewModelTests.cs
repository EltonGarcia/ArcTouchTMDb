using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ArcTouchTMDb.Core.Services.API.Response;
using Moq;
using MvvmCross.Plugins.Messenger;
using NUnit.Framework;

namespace ArcTouchTMDb.Core.Tests
{
	[TestFixture()]
	public class MoviesViewModelTests
	{
		[Test()]
		public async Task Test_LoadMoviesCorrectly()
		{
			//arrange
			//CityDataService mockCityDataService = ServiceMocks.GetMockCityDataService(3);
			var mockMessenger = new Mock<IMvxMessenger>();
			var mockTMDbService = new Mock<ITMDbService>();
			var mockSettingsService = new Mock<ISettingsService>();

			var mockSettings = new Mock<Settings>();
			mockSettings.SetupAllProperties();
			mockSettingsService.Setup(o => o.GetSettings()).Returns(mockSettings.Object);

			var movies = new List<Movie>();
			for (int i = 0; i < 3; i++)
				movies.Add(new Mock<Movie>().SetupAllProperties().Object);

			var mockResponse = new Mock<DiscoverResponse>();
			mockResponse.SetupAllProperties();
			//mockResponse.SetupGet(o => o.results).Returns(movies);
			mockTMDbService.Setup(o => o.Discover(null)).ReturnsAsync(mockResponse.Object);

			//act
			var moviesViewModel = new MoviesViewModel(mockMessenger.Object, mockTMDbService.Object, mockSettingsService.Object);
			await moviesViewModel.LoadMovies();

			//assert
			Assert.AreEqual(moviesViewModel.Movies.Count, 3);
		}
	}
}
