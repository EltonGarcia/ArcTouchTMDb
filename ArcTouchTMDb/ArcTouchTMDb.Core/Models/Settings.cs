using System;
namespace ArcTouchTMDb.Core
{
	public class Settings
	{
		public string ApiKey { get; set; }
		public Language Language { get; set; }
		public Region Region  { get; set; }
		public Settings()
		{
		}
	}
}
