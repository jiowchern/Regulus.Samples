
rd ./Assets/Project/Plugins /q /s
dotnet publish ./../Regulus.Samples.Chat1.Common -f netstandard2.0 -o ./Assets/Project/Plugins
dotnet publish ./../../Regulus/Regulus.Remote.Client -f netstandard2.0 -o ./Assets/Project/Plugins
dotnet publish ./../../Regulus/Regulus.Remote.Unity -f netstandard2.0 -o ./Assets/Project/Plugins

dotnet publish ./../Regulus.Samples.Chat1.Protocol.Outputer -o ./bin/Protocol.Outputer
md ./Assets/Project/Plugins/Script

dotnet ./bin/Protocol.Outputer/Regulus.Samples.Chat1.Protocol.Outputer.dll Regulus.Samples.Chat1 ./Assets/Project/Plugins/Regulus.Samples.Chat1.Common.dll ./Assets/Project/Plugins/Script

pause



