using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TypeSafe.Http.Net.Performance.Tests
{
	public static class Program
	{
		public static void Main(string[] args)
		{
			Console.WriteLine("Calling Async Main.");

			Task.Run(AsyncMain).Wait();

			Console.WriteLine("Async main yielded.");

			Console.ReadKey();
		}

		public static async Task AsyncMain()
		{
			ITestInterface apiInterface = RestServiceBuilder<ITestInterface>.Create()
				.RegisterDotNetHttpClient(@"http://localhost.fiddler:5000")
				.RegisterDefaultSerializers()
				.RegisterJsonNetSerializer()
				.Build();

			Console.WriteLine("About to call intercepted method.");

			Task<TestReturnModel> result = apiInterface.TestMethod(new TestModel(12456, "Two"), "test");

			Console.WriteLine("API method temporarily yielded.");

			await result;

			Console.WriteLine($"Finished call intercepted method. Result: {result.Result.ReturnValue}");
		}

		[Header("Hello-Base-X", "Hi")]
		public interface ITestInterface
		{
			[Post("/api/{endpoint}")]
			[Header("Hello-Base-Y", "Yo", "Another")]
			[Header("Hello-Base-Y", "Test")]
			[Header("Hello-Base-Y", "Test2", "Test3", "Test4")]
			Task<TestReturnModel> TestMethod([JsonBody] TestModel model, string endpoint);
		}

		[JsonObject]
		public class TestModel
		{
			public TestModel(int testModelString1, string testModelString2)
			{
				TestModelString1 = testModelString1;
				TestModelString2 = testModelString2;
			}

			[JsonProperty]
			[AliasAs("AliasedParameter")]
			public int TestModelString1 { get; }

			[JsonProperty]
			public string TestModelString2 { get; }
		}

		[JsonObject]
		public class TestReturnModel
		{
			[JsonProperty]
			public string ReturnValue { get; }

			public TestReturnModel(string returnValue)
			{
				ReturnValue = returnValue;
			}
		}
	}
}
