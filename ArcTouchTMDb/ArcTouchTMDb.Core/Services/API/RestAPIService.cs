using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ArcTouchTMDb.Core.Services.API.Request;
using ArcTouchTMDb.Core.Services.API.Response;
using Newtonsoft.Json;

namespace ArcTouchTMDb.Core
{
	public abstract class RestAPIService
	{
		private HttpClient client;

		public RestAPIService()
		{
			client = new HttpClient();
		}

		public async Task<T> Request<T>(string url, IBaseRequest request) where T : IBaseResponse
		{
			try
			{
				var response = await client.GetAsync(url);

				if (response.IsSuccessStatusCode)
				{
					var jsonResponse = await response.Content.ReadAsStringAsync();
					return JsonConvert.DeserializeObject<T>(jsonResponse);
				}
			}
			catch (Exception ex)
			{
				Debug.WriteLine(@"ERROR {0}", ex.Message);
			}
			return default(T);
		}
	}
}
