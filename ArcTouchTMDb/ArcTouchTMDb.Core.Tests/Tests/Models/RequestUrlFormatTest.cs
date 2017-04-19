using System;
using System.Collections.Generic;
using ArcTouchTMDb.Core.Services.API.Request;
using NUnit.Framework;

namespace ArcTouchTMDb.Core.Tests
{
	[TestFixture()]
	public class RequestUrlFormatTest
	{
		[Test()]
		public void Test_FormatRequestCorrectly()
		{
			var settings = new Settings()
			{
				ApiKey = "123",
				BaseUrl = "http://domain.test"
			};

			var url = new TestRequest(settings).CreateRequestUrl();
			Assert.AreEqual("http://domain.test/action?api_key=123&paramA=A", url);
		}

		private class TestRequest : BaseRequest
		{
			protected override string action => "/action";

			public TestRequest(Settings settings) : base(settings)
			{
			}

			protected override Dictionary<string, string> parameters
			{
				get
				{
					return new Dictionary<string, string> { 
						{"paramA", "A"}
					};
				}
			}
		}
	}
}
