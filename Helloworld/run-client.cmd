dotnet publish -o ./publish/client .\Regulus.Samples.Helloworld.Client\Regulus.Samples.Helloworld.Client.csproj
cd ./publish/client
dotnet ./Regulus.Samples.Helloworld.Client.dll 127.0.0.1 20909
