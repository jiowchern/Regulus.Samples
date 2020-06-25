dotnet publish -o ./publish/server .\Regulus.Samples.Helloworld.Server\Regulus.Samples.Helloworld.Server.csproj
cd ./publish/server
dotnet ./Regulus.Samples.Helloworld.Server.dll 20909