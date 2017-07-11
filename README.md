# TypeSafe.Http.Net

TypeSafe.Http.Net is an automatic type-safe REST web client. Bringing type safety to the uncertainty of web.

TypeSafe.Http.Net is a heavily inspired by Square's [Retrofit library](http://square.github.io/retrofit/) and Paul Betts' [Refit library](https://github.com/paulcbetts/refit). It turns your REST or ASP.NET Web APIs into type-safe async RPCs (remote procedural calls):

## How to Use

TypeSafe.Http.Net is designed for ease-of-use. Using the modern concept of reflection, metadata or annotations you can prepare a .NET interface type to become a client to a REST/HTTP/Web service.

Firstly to prepare an interface for use in TypeSafe.Http.Net you'll want to add the NuGet Package [TypeSafe.Http.Net.Metadata](https://www.nuget.org/packages/TypeSafe.Http.Net.Metadata/) that contains the attributes/annotations for the project. This project currently has a requirement of Netstandard1.1 but I am working on reducing this to Netstandard1.0.

### Http Methods

Once included you will be able to reference all of the standard attributes in the TypeSafe.Http.Net project. Of particular importance you'll want to see the [HttpMethod Attributes](https://github.com/HelloKitty/TypeSafe.Http.Net/tree/master/src/TypeSafe.Http.Net.Metadata/Attributes/Methods). For out first example we will be using a GET request.


```
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test()
}
```

**Result of calling Test:**

```
GET {url}/api/test
```

### Headers

If we wanted to add static headers to the all the methods on this service interface we can use another attribute called [Header](https://github.com/HelloKitty/TypeSafe.Http.Net/blob/master/src/TypeSafe.Http.Net.Metadata/Attributes/HeaderAttribute.cs).

```
[Header("User-Agent", "TestClient 1.0")]
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  Task Test()
}
```

**Result of calling Test:**

```
GET {url}/api/test
User-Agent: TestClient 1.0
```

If you want to add a header to only a specifc method and not all methods on a service then you can annotate that specific method instead.

```
[Header("User-Agent", "TestClient 1.0")]
public interface IHttpServiceInterface
{
  [Get("/api/test")]
  [Header("Custom-Header", "Test1", "Test2")]
  Task Test()
}
```

**Result of calling Test:**

```
GET {url}/api/test
User-Agent: TestClient 1.0
Custom-Header: Test1, Test2
```

It is also ok to have multiple of the same header annotation. The values will be combined however the order in which these header values will appear is not defined. Make no expectation of that.

```
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

**Result of calling Test:**

```
GET {url}/api/test
User-Agent: TestClient 1.0
Custom-Header: Test1, Test2, Test3, Test4, Test5, Test6
```

## Setup

To compile or open TypeSafe.Http.Net project you'll first need a couple of things:

* Visual Studio 2017

## Builds

TODO: Our myget and also the nuget link

Myget: [![halolive MyGet Build Status](https://www.myget.org/BuildSource/Badge/halolive?identifier=0aa218dd-d0bf-4f44-8ee2-a2ef886e5f6f)](https://www.myget.org/)

## Tests

TODO actual tests

|    | Linux Debug | Windows .NET Debug |
|:---|----------------:|------------------:|
|**master**| [![Build Status](https://travis-ci.org/HaloLive/HaloLive.Library.svg?branch=master)](https://travis-ci.org/HaloLive/HaloLive.Library)* | [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library) |
|**dev**| [![Build Status](https://travis-ci.org/HaloLive/HaloLive.Library.svg?branch=dev)](https://travis-ci.org/HaloLive/HaloLive.Library) | [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4/branch/dev?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library/branch/dev) |

* Failing because TravisCI doesn't support net462 at the moment.
