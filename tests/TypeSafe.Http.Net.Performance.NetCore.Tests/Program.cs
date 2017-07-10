using System;
using System.Collections.Generic;
using System.Linq;
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
			ITestInterface apiInterface = builder.Build();

			Console.WriteLine("About to call intercepted method.");

			Task result = apiInterface.TestMethod("test", "1", "2");

			Console.WriteLine("API method temporarily yielded.");

			await result;

			Console.WriteLine("Finished call intercepted method.");
		}

		[Header("Hello-Base-X", "Hi")]
		public interface ITestInterface
		{
			[Get("/api/{endpoint}/{endpoint}")]
			[Header("Hello-Base-Y", "Yo", "Another")]
			[Header("Hello-Base-Y", "Test")]
			[Header("Hello-Base-Y", "Test2", "Test3", "Test4")]
			Task TestMethod([AliasAs("endpoint")] string end, [QueryStringParameter] string testParameter1, [QueryStringParameter, AliasAs("Rewrittenname")] string testParameter2);
		}
	}
}
