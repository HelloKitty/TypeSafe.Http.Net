using System;
using System.Threading.Tasks;
using Example.Models;
using TypeSafe.Http.Net;

namespace Example.Client
{
	public class Program
	{
		static void Main(string[] args)
		{
			Task.Run(AsyncMain).Wait();

			Console.Read();
		}

		private static async Task AsyncMain()
		{
			IExampleHttpService service = RestServiceBuilder<IExampleHttpService>.Create()
				.RegisterDefaultSerializers()
				.RegisterDotNetHttpClient(@"http://localhost:5000")
				.RegisterJsonNetSerializer()
				.Build();

			Console.WriteLine("Service built.");

			ResponseModel model = await service.GetPowerLevel(new RequestModel("Glader"));

			Console.WriteLine($"Glader's power level: {model.Power} Id: {model.Identifier}");
		}

		public interface IExampleHttpService
		{
			[Post("/api/test")]
			Task<ResponseModel> GetPowerLevel([JsonBody] RequestModel model);
		}
	}
}