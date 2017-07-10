using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
			RestServiceBuilder<ITestInterface> builder = new RestServiceBuilder<ITestInterface>();
			builder.Register(new HttpClientRestServiceProxy(@"http://localhost.fiddler:5000"));
			builder.Register<UrlEncodedBodyAttribute, UrlEncodedBodySerializerStrategy>(new UrlEncodedBodySerializerStrategy());
			ITestInterface apiInterface = builder.Build();

			Console.WriteLine("About to call intercepted method.");

			Task result = apiInterface.TestMethod(new TestModel(12456, "Two"), "test");

			Console.WriteLine("API method temporarily yielded.");

			await result;

			Console.WriteLine("Finished call intercepted method.");
		}

		[Header("Hello-Base-X", "Hi")]
		public interface ITestInterface
		{
			[Post("/api/{endpoint}")]
			[Header("Hello-Base-Y", "Yo", "Another")]
			[Header("Hello-Base-Y", "Test")]
			[Header("Hello-Base-Y", "Test2", "Test3", "Test4")]
			Task TestMethod([UrlEncodedBody] TestModel model, [AliasAs("endpoint")] string end);
		}

		public class TestModel
		{
			public TestModel(int testModelString1, string testModelString2)
			{
				TestModelString1 = testModelString1;
				TestModelString2 = testModelString2;
			}

			[AliasAs("AliasedParameter")]
			public int TestModelString1 { get; }

			public string TestModelString2 { get; }
		}
	}
}
