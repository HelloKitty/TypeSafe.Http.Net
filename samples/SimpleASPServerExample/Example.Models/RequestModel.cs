using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Example.Models
{
	[JsonObject]
	public class RequestModel
	{
		[JsonProperty("name")]
		public string Name { get; }

		public RequestModel(string name)
		{
			if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Value cannot be null or whitespace.", nameof(name));

			Name = name;
		}
	}
}
