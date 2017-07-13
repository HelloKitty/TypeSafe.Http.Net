# TypeSafe.Http.Net

TypeSafe.Http.Net is an automatic type-safe REST web client. Bringing type safety to the uncertainty of web.

TypeSafe.Http.Net is a heavily inspired by Square's [Retrofit library](http://square.github.io/retrofit/) and Paul Betts' [Refit library](https://github.com/paulcbetts/refit). It turns your REST or ASP.NET Web APIs into type-safe async RPCs (remote procedural calls):

## Features
- [ ] **Performance** (library is not yet profiled/optimized)
- [x] Type Headers
- [x] Method Headers
- [ ] Dynamic Header Values/Formatting
- [x] Action Paths
- [x] Dynamic Action Paths
- [x] JSON Serialization/Deserialization
- [x] URL Encoded Body Serialization/Deserialization
- [ ] URL Encoded Dictionary Serialization/Deserialization
- [ ] Protobuf Serialization/Deserialization
- [ ] Multipart
- [ ] Stream Content
- [x] [.NET HttpClient Implementation](https://msdn.microsoft.com/en-us/library/system.net.http.httpclient(v=vs.118).aspx)
- [ ] [RestSharp Implementation](https://github.com/restsharp/RestSharp)

## How to Use

TypeSafe.Http.Net is designed for ease-of-use. Using the modern concept of reflection, metadata or annotations you can prepare a .NET interface type to become a client to a REST/HTTP/Web service.

```csharp
IServiceInterface service = TypeSafeHttpBuilder<IServiceInterface>().Create()
  .RegisterDefaultSerializers()
	.RegisterDotNetHttpClient(@"http://localhost:5000")
	.RegisterJsonNetSerializer()
	.Build();
```

To prepare an interface for use in TypeSafe.Http.Net you'll want to add the NuGet Package [TypeSafe.Http.Net.Metadata](https://www.nuget.org/packages/TypeSafe.Http.Net.Metadata/) that contains the attributes/annotations for the project. The Metadata project currently has a requirement of Netstandard1.1 but I am working on reducing this to Netstandard1.0.

### Http Methods

Once included you will be able to reference all of the standard attributes in the TypeSafe.Http.Net project. Of particular importance you'll want to see the [HttpMethod Attributes](https://github.com/HelloKitty/TypeSafe.Http.Net/tree/master/src/TypeSafe.Http.Net.Metadata/Attributes/Methods). For out first example we will be using a GET request.


```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test()
}
```

Result of calling Test:

```
GET {baseurl}/api/test
```

The ability to control the endpoint/action dynamically, which is required for a REST service, is simple to take advantage of. All you need is to utilize formatted strings and provide parameters to the Test method. You can insert values into the action path like so.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/{id}/test")]
  Task Test(int id)
}
```

Result of calling Test:

```
GET {baseurl}/api/{id}/test
```

Where {id} is for example 5 if you make a call to the interface like so service.Test(5).

If you want to use a parameter name differing from the string format indicator then you can utilize [AliasAs](https://github.com/HelloKitty/TypeSafe.Http.Net/blob/master/src/TypeSafe.Http.Net.Metadata/Attributes/AliasAsAttribute.cs) Attribute. This attribute allows to essentially rename a parameter. It works with querystrings, action paths and even UrlEncodedBody serialization.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/{id}/test")]
  Task Test([AliasAs("id")] int identifier)
}
```

Result of calling Test:

```
GET {baseurl}/api/{id}/test
```

### Headers

If we wanted to add static headers to the all the methods on this service interface we can use another attribute called [Header](https://github.com/HelloKitty/TypeSafe.Http.Net/blob/master/src/TypeSafe.Http.Net.Metadata/Attributes/HeaderAttribute.cs).

```csharp
[Header("User-Agent", "TestClient 1.0")]
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test()
}
```

Result of calling Test:

```
GET {url}/api/test
User-Agent: TestClient 1.0
```

If you want to add a header to only a specifc method and not all methods on a service then you can annotate that specific method instead.

```csharp
[Header("User-Agent", "TestClient 1.0")]
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  [Header("Custom-Header", "Test1", "Test2")]
  Task Test()
}
```

Result of calling Test:

```
GET {url}/api/test
User-Agent: TestClient 1.0
Custom-Header: Test1, Test2
```

It is also ok to have multiple of the same header annotation. The values will be combined however the order in which these header values will appear is not defined. Make no expectation of that.

```csharp
[Header("User-Agent", "TestClient 1.0")]
[Header("Custom-Header", "Test5", "Test6")]
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  [Header("Custom-Header", "Test1", "Test2")]
  [Header("Custom-Header", "Test3", "Test4")]
  Task Test()
}
```

Result of calling Test:

```
GET {url}/api/test
User-Agent: TestClient 1.0
Custom-Header: Test1, Test2, Test3, Test4, Test5, Test6
```

Dynamic headers are a planned feature but not yet supported.

### Query String Parameters

TypeSafe.Http.Net also supports querystring parameters in multiple ways. The most basic way is a static querystring parameter.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test?param1=1&param2=2")]
  Task Test()
}
```

Result of calling Test:

```
GET {url}/api/test?param1=1&param2=2
```

Another way is to utilize the [QueryStringParameter](https://github.com/HelloKitty/TypeSafe.Http.Net/blob/master/src/TypeSafe.Http.Net.Metadata/Attributes/Serialization/QueryStringParameterAttribute.cs) attribute like the following.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test?param1=1&param2=2")]
  Task Test([QueryStringParameter] string t)
}
```

Result of calling Test:

```
GET {url}/api/test?param1=1&param2=2&t={t}
```

{t} will be the value of the string provided during the method call. It is essentially effortless. You do not need to worry about existing querystring parameters or even if you have none. The querystring will be built for you.

Some more examples.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test([QueryStringParameter] string test)
}
```

Result of calling Test:

```
GET {url}/api/test?test={test}
```

It is also possible to use the [AliasAs](https://github.com/HelloKitty/TypeSafe.Http.Net/blob/master/src/TypeSafe.Http.Net.Metadata/Attributes/AliasAsAttribute.cs) Attribute to control the queryparameter name.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test([QueryStringParameter, AliasAs("param1")] string test)
}
```

Result of calling Test:

```
GET {url}/api/test?param1={test}
```

### Request Body Content

Sometimes you want to send content in the request body. These are usually data models and it is possible to do this using the various body attributes. One major requirement is that the body content must be the first parameter in the method. See the examples below

```csharp
public class TestModel
{
  //Sent
  public string TestParameter { get; }
  
  //Sent
  [AliasAs("Param2")]
  public string TestParameter2 { get; }
  
  //Not usually sent
  private int Test3 { get; }
}
```

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test([UrlEncodedBody]TestModel model)
}
```

Result of calling Test:

```
GET {url}/api/test
Content-Type: application/x-www-form-urlencoded
Body: TestParameter={1}&Param2={2}
```

It is also possible to send just simple string content.

```csharp
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test([StringContent] string data)
}
```

Result of calling Test:

```
GET {url}/api/test
Content-Type: text/plain
Body: {data}
```

There are various other serializers that work in a similar fashion. Inlcude serializers like the [Json.NET Serializer Implementation](https://www.nuget.org/packages/TypeSafe.Http.Net.Serializer.JsonNET) and register it to use with [JsonBody](https://github.com/HelloKitty/TypeSafe.Http.Net/blob/master/src/TypeSafe.Http.Net.Metadata/Attributes/Serialization/Concrete/JsonBodyAttribute.cs) Attribute.

### Response Body Content

Unlike Refit the response content is deserialized based on the returned content-type. You do not need to annoate anything. This is handled internally. If a deserializer is not found that handles the returned content-type it will throw an exception.

For example if application/json is returned in the response body it will look for a register serializer that handles application/json and then try to deserialize it as a JSON object.

This design allows the internal library to handle deserialization for you based on how the server has returned the response. If you want to indicate a specific content type should be sent back use the HTTP recommended Accept headers.

## Setup

To compile or open TypeSafe.Http.Net project you'll first need a couple of things:

* Visual Studio 2017

## Builds

NuGet: [Core Library](https://www.nuget.org/packages/TypeSafe.Http.Net.Core/)

NuGet: [.NET HttpClient Implementation](https://www.nuget.org/packages/TypeSafe.Http.Net.HttpClient/)

NuGet: [Metadata/Attributes Library](https://www.nuget.org/packages/TypeSafe.Http.Net.Metadata/)

NuGet: [JSON.Net (Newtonsoft) Serializer Implementation](https://www.nuget.org/packages/TypeSafe.Http.Net.Serializer.JsonNET/)


Myget: [![hellokitty MyGet Build Status](https://www.myget.org/BuildSource/Badge/hellokitty?identifier=772ec112-a0e1-49b7-9d94-ede9ff28945a)](https://www.myget.org/)

## Tests

TODO actual tests

|    | Linux Debug | Windows .NET Debug |
|:---|----------------:|------------------:|
|**master**| [![Build Status](https://travis-ci.org/HaloLive/HaloLive.Library.svg?branch=master)](https://travis-ci.org/HaloLive/HaloLive.Library)* | [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library) |
|**dev**| [![Build Status](https://travis-ci.org/HaloLive/HaloLive.Library.svg?branch=dev)](https://travis-ci.org/HaloLive/HaloLive.Library) | [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4/branch/dev?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library/branch/dev) |

* Failing because TravisCI doesn't support net462 at the moment.
