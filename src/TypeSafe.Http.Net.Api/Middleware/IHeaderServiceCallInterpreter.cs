using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace TypeSafe.Http.Net
{
	/// <summary>
	/// Service that interprets service calls to produce <see cref="IRequestHeader"/> collections.
	/// </summary>
	public interface IHeaderServiceCallInterpreter : IRequestPipelineService<IEnumerable<IRequestHeader>>
	{

	}
}
