%NUGET% restore TypeSafe.HTTP.NET.sln -NoCache -NonInteractive -ConfigFile NuGet.config
msbuild TypeSafe.HTTP.NET.sln /p:Configuration=Release