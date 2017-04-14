using System;
namespace ArcTouchTMDb.Core.Services.API.Request
{
	public class BaseRequest : IBaseRequest
	{
		public string api_key { get; set; }
		public string language { get; set; }
		public string region { get; set; }

		public BaseRequest(Settings settings)
		{
			api_key = settings.ApiKey;
			if (settings.Language != null)
				language = settings.Language.Code;
			if (settings.Region != null)
				region = settings.Region.Code;
		}
	}
}
