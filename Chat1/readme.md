### 聊天室
編譯  
```powershell
dotnet publish .\Regulus.Samples.Chat1.Server\Regulus.Samples.Chat1.Server.csproj -o bin\server
dotnet publish .\Regulus.Samples.Chat1.Client\Regulus.Samples.Chat1.Client.csproj -o bin\client
```
執行 Server  
```powershell
cd .\bin\server
dotnet .\Regulus.Samples.Chat1.Server.dll 31716
```

執行 Client  
```powershell
cd .\bin\client
dotnet .\Regulus.Samples.Chat1.Client.dll ChatName 127.0.0.1 31716
```