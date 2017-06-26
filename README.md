# TypeSafe.HTTP.NET

TypeSafe.HTTP.NET is an automatic type-safe REST web client. Bringing type safety to the uncertainty of web.

TypeSafe.HTTP.NET is a heavily inspired by Square's [Retrofit library](http://square.github.io/retrofit/) and Paul Betts' [Refit library](https://github.com/paulcbetts/refit). It turns your REST or ASP.NET Web APIs into type-safe async RPCs (remote procedural calls):

TODO add simple interface example.

## Setup

To use this project you'll first need a couple of things:

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
