using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Example.Models;
using Microsoft.AspNetCore.Mvc;

namespace Example.Server.Controllers
{
	[Route("api/[controller]")]
	public class TestController : Controller
	{
		private Dictionary<string, int> NamePowerMap { get; } = new Dictionary<string, int> { {"Glader", 9001}, {"Lyle", 62} };

		[HttpPost]
		public async Task<IActionResult> GetPowerLevel([FromBody] RequestModel model)
		{
			if (!ModelState.IsValid)
				return BadRequest();

			if (!NamePowerMap.ContainsKey(model.Name))
				return BadRequest();

			return Json(new ResponseModel(Guid.NewGuid().ToString(), NamePowerMap[model.Name]));
		}
	}
}
