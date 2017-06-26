using System;
using System.Collections.Generic;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Service that interprets service calls to produce <see cref="string"/> action paths.
	/// These are the paths appended to the
	/// </summary>
	public interface IActionPathServiceCallInterpreter : IRequestPipelineService<string>
	{
		
	}
}
