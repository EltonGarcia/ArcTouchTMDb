using System;
using System.Collections.Generic;
using System.Linq;

namespace ArcTouchTMDb.Core.Services.API.Request
{
	public abstract class BaseRequest : IBaseRequest
	{
		private readonly string parameterFormat = "{0}={1}";
		private readonly string uriFormat = "{0}{1}?{2}";

		public string base_url { get; set; }
		public string api_key { get; set; }
		public string language { get; set; }
		public string region { get; set; }

		public BaseRequest(Settings settings)
		{
			base_url = settings.BaseUrl;
			api_key = settings.ApiKey;
			if (settings.Language != null)
				language = settings.Language.Code;
			if (settings.Region != null)
				region = settings.Region.Code;
		}

		protected abstract string action { get; }
		protected virtual Dictionary<string, string> parameters
		{
			get
			{
				return new Dictionary<string, string>();
			}
		}

		protected virtual Dictionary<string, string> baseParameters
		{
			get
			{
				return new Dictionary<string, string>() {
					{ "api_key", api_key },
					{ "language", language },
					{ "region", region }
				};
			}
		}

		public string CreateRequestUrl()
		{
			return string.Format(uriFormat, base_url, action, FormatParameters());
		}

		private string FormatParameters()
		{
			var @params = baseParameters.Union(parameters).Where(o => !string.IsNullOrEmpty(o.Value));
			return string.Join("&", @params.Select(FormatParameter));
		}

		private string FormatParameter(KeyValuePair<string, string> parameter)
		{
			return string.Format(parameterFormat, parameter.Key, parameter.Value);
		}
	}
}
