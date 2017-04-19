using System;
namespace ArcTouchTMDb.Core
{
	public class SettingsService : ISettingsService
	{
		public Settings GetSettings()
		{
			return new Settings()
			{
				BaseUrl = "https://api.themoviedb.org/3",
				ApiKey = "1f54bd990f1cdfb230adb312546d765d",
				Language = new Language(),
				Region = new Region()
			};
		}
	}
}
