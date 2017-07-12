using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Example.Models
{
	[JsonObject]
	public class ResponseModel
	{
		[JsonProperty("id")]
		public string Identifier { get; }

		[JsonProperty("power")]
		public int Power { get; }

		public ResponseModel(string identifier, int power)
		{
			if (string.IsNullOrWhiteSpace(identifier)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(identifier));

			Identifier = identifier;
			Power = power;
		}
	}
}
