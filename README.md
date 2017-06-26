# HaloLive.Library

Library containing assemblies for the HaloLive project. If you're looking for information or documentation consult the [Documentation Repo](https://github.com/HaloLive/Documentation) that contains design diagrams, endpoint and request/response documentation and information about much more.

## Setup

To use this project you'll first need a couple of things:

* Visual Studio 2017

## Builds

Myget: [![halolive MyGet Build Status](https://www.myget.org/BuildSource/Badge/halolive?identifier=0aa218dd-d0bf-4f44-8ee2-a2ef886e5f6f)](https://www.myget.org/)

## Tests

|    | Linux Debug | Windows .NET Debug |
|:---|----------------:|------------------:|
|**master**| [![Build Status](https://travis-ci.org/HaloLive/HaloLive.Library.svg?branch=master)](https://travis-ci.org/HaloLive/HaloLive.Library)* | [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library) |
|**dev**| [![Build Status](https://travis-ci.org/HaloLive/HaloLive.Library.svg?branch=dev)](https://travis-ci.org/HaloLive/HaloLive.Library) | [![Build status](https://ci.appveyor.com/api/projects/status/rinvn2tdxn0yinf4/branch/dev?svg=true)](https://ci.appveyor.com/project/HelloKitty/halolive-library/branch/dev) |

* Failing because TravisCI doesn't support net462 at the moment.
