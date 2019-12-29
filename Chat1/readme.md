## Console  [![Github Rocker Status](https://github.com/jiowchern/Regulus.Samples/workflows/Build/badge.svg)](https://github.com/jiowchern/Regulus.Samples/actions?query=workflow%3ABuild)  
**編譯**  
```powershell
dotnet publish .\Regulus.Samples.Chat1.Server\Regulus.Samples.Chat1.Server.csproj -o bin\server
dotnet publish .\Regulus.Samples.Chat1.Client\Regulus.Samples.Chat1.Client.csproj -o bin\client
```
**執行 Server**  
```powershell
cd .\bin\server
dotnet .\Regulus.Samples.Chat1.Server.dll 31716
```

**執行 Client**  
```powershell
cd .\bin\client
dotnet .\Regulus.Samples.Chat1.Client.dll ChatName 127.0.0.1 31716
```
---
## Docker [![Github Rocker Status](https://github.com/jiowchern/Regulus.Samples/workflows/Docker/badge.svg)](https://github.com/jiowchern/Regulus.Samples/actions?query=workflow%3ADocker) 
**Packages**  
* [Server Package](https://github.com/jiowchern/Regulus.Samples/packages/94444)  
* [Client Package](https://github.com/jiowchern/Regulus.Samples/packages/94715)

**Pull Server** 


```powershell
docker pull docker.pkg.github.com/jiowchern/regulus.samples/regulus.samples.chat1.server:latest
```
**Run Server**
```powershell
docker run --rm -it --name chat1.server --entrypoint dotnet regulus.samples.chat1.server ./Regulus.Samples.Chat1.Server.dll 40123
```
**Pull Client**  
```powershell
docker pull docker.pkg.github.com/jiowchern/regulus.samples/regulus.samples.chat1.client:latest
```
**Run Client**
```powershell
docker run --rm -it --name chat1.client --net=container:chat1.server --entrypoint ChaterName regulus.samples.chat1.client ./Regulus.Samples.Chat1.Client.dll ChaterName 127.0.0.1 40123
```
---
