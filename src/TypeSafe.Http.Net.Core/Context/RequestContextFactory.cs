using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using Castle.Core.Internal;

namespace TypeSafe.Http.Net
{
	public sealed class RequestContextFactory : IRequestContextFactory
	{
		//Use \{\*?.*?\} to look at the action path in the attributes.
		//Use \?[^&]*|&[^&]* for query string detection
		private IHeaderServiceCallInterpreter HeaderInterpreterService { get; }

		public RequestContextFactory(IHeaderServiceCallInterpreter headerInterpreterService)
		{
			if (headerInterpreterService == null) throw new ArgumentNullException(nameof(headerInterpreterService));

			HeaderInterpreterService = headerInterpreterService;
		}

		/// <inheritdoc />
		public IHttpClientRequestContext CreateContext(IServiceCallContext callContext, IServiceCallParametersContext parameterContext)
		{
			if (callContext == null) throw new ArgumentNullException(nameof(callContext));
			if (parameterContext == null) throw new ArgumentNullException(nameof(parameterContext));

			HttpBaseMethodAttribute httpMethod = callContext.ServiceMethod
				.GetCustomAttribute<HttpBaseMethodAttribute>();

			if (httpMethod == null)
				throw new InvalidOperationException($"Method: {callContext.ServiceMethod.Name} on Type: {callContext.ServiceType.FullName} cannot produce valid HTTP method.");

			//We can build a get request much easier; it doesn't have a body.
			if (httpMethod.Method == HttpMethod.Get)
				return BuildGetRequest(callContext, parameterContext.HasParameters ? BuildFormattedActionPath(httpMethod.Path, callContext, parameterContext) : httpMethod.Path);
			else
				return BuildNonGetRequest(httpMethod.Method, callContext, parameterContext, 
					parameterContext.HasParameters ? BuildFormattedActionPath(httpMethod.Path, callContext, parameterContext) : httpMethod.Path);
		}

		private IHttpClientRequestContext BuildNonGetRequest(HttpMethod httpMethod, IServiceCallContext callContext, IServiceCallParametersContext parameterContext, string baseActionPath)
		{
			//We don't currently support dynamic header values so
			IEnumerable<IRequestHeader> headers = HeaderInterpreterService.ProduceFromContext(callContext, new NoParametersContext());

			if (parameterContext.HasParameters)
			{
				//If it has parameters it STILL may not have a body. They may be used for querystring or action path.
				ParameterInfo first = callContext.ServiceMethod.GetParameters().First();

				//It should have the body content attribute, which is required explictly, that indicates how it should be serialized.
				if(first.GetCustomAttribute<BodyContentAttribute>() != null)
					return new HttpRequestContext(httpMethod, baseActionPath, headers, new DefaultBodyContext(parameterContext.Parameters.First(), first.GetCustomAttribute<BodyContentAttribute>().GetType()),
						BuildErrorCodeSupressionContext(callContext));
			}

			//If it has no parameters then we should return a context with no body.
			return new HttpRequestContext(httpMethod, baseActionPath, headers, new NoBodyContext(), BuildErrorCodeSupressionContext(callContext));
		}

		public IHttpClientRequestContext BuildGetRequest(IServiceCallContext callContext, string baseActionPath)
		{
			IEnumerable<IRequestHeader> headers = HeaderInterpreterService.ProduceFromContext(callContext, new NoParametersContext());

			return new GetHttpRequestContext(baseActionPath, headers, BuildErrorCodeSupressionContext(callContext));
		}

		public ISupressedErrorCodeContext BuildErrorCodeSupressionContext(IServiceCallContext callContext)
		{
			SupressResponseErrorCodesAttribute codesAttribute = callContext.ServiceMethod.GetAttribute<SupressResponseErrorCodesAttribute>();

			if (codesAttribute == null)
				return new NoErrorCodesSupressedContext();

			return new DefaultErrorCodeSupressedContext(codesAttribute.SupressedCodes);
		}

		private string BuildFormattedActionPath(string baseActionPath, IServiceCallContext callContext, IServiceCallParametersContext parameters)
		{
			//We must check to see if there are any match groups
			//that fit the format {something} in the path
			//if there is we need to look through the parameters of the method to find
			//the replacement.

			//TODO: We could probably cache the results to the index of the parameter and attributes gathered for future calls
			MatchCollection matches = Regex.Matches(baseActionPath, @"\{\*?.*?\}");

			//If nothing fits this regex then there is nothing to replace in the action path.
			if (matches.IsNullOrEmpty())
				return baseActionPath;

			//TODO: Make this not O(n^2)
			ParameterInfo[] parameterInfos = callContext.ServiceMethod.GetParameters();

			//Keep this null until we need it the first time.
			AliasAsAttribute[] asAttributes = null;

			foreach (string matchString in matches.Cast<Match>().Select(m => m.Value).Distinct())
			{
				for (int i = 0; i < parameterInfos.Length; i++)
				{
					//We have to be careful about strings matching a substring of the full thing
					//like id matching both {id} and {groupId}
					if (matchString.Contains(parameterInfos[i].Name))
					{
						//Check if it matches the parameter name
						if (matchString == $"{{{parameterInfos[i].Name}}}")
							baseActionPath = baseActionPath.Replace(matchString, parameters.Parameters[i].ToString());
					}
				}

				if (asAttributes == null)
					asAttributes = parameterInfos.Select(x => x.GetCustomAttribute<AliasAsAttribute>()).ToArray();

				//If it doesn't match the name of a parameter we need to reflect on each parameter to see if it's aliased to what we're looking for
				for (int i = 0; i < asAttributes.Length; i++)
				{
					if (asAttributes[i] == null)
						continue;

					//We have to be careful about strings matching a substring of the full thing
					//like id matching both {id} and {groupId}
					if (matchString.Contains(asAttributes[i].Name))
					{
						//Check if it matches the parameter name
						if (matchString == $"{{{asAttributes[i].Name}}}")
							baseActionPath = baseActionPath.Replace(matchString, parameters.Parameters[i].ToString());
					}
				}
			}

			//Now we have to build the querystring
			MatchCollection queryStringMatches = Regex.Matches(baseActionPath, @"\?[^&]*|&[^&]*");

			bool useQuestionMark = queryStringMatches.IsNullOrEmpty();

			//If there are matches it means there is a query string already being build so we need to append to it.
			for(int i = 0; i < parameterInfos.Length; i++)
			{
				//Check each parameter if it's a querystring parameter.
				if (parameterInfos[i].GetCustomAttribute<QueryStringParameterAttribute>() != null)
				{
					//Check for alias'd parameters
					AliasAsAttribute asAttribute = parameterInfos[i].GetCustomAttribute<AliasAsAttribute>();

					string parameterName = asAttribute != null ? asAttribute.Name : parameterInfos[i].Name;

					if (useQuestionMark)
					{
						baseActionPath = $"{baseActionPath}?{parameterName}={parameters.Parameters[i]}";
						useQuestionMark = false;
					}
					else
					{
						baseActionPath = $"{baseActionPath}&{parameterName}={parameters.Parameters[i]}";
					}
				}
			}

			return baseActionPath;
		}
	}
}
