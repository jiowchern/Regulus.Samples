### 聊天室
```powershell
編譯
dotnet build .\Regulus.Samples.Chat1.Server\Regulus.Samples.Chat1.Server.csproj -o bin\server
dotnet build .\Regulus.Samples.Chat1.Client\Regulus.Samples.Chat1.Client.csproj -o bin\client
```
```powershell
執行
dotnet .\bin\client\Regulus.Samples.Chat1.Server.dll 31716
dotnet .\bin\server\Regulus.Samples.Chat1.Client.dll ChatName 127.0.0.1 31716
```