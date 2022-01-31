#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/runtime:3.1 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Chat1/Regulus.Samples.Chat1.Server/Regulus.Samples.Chat1.Server.csproj", "Chat1/Regulus.Samples.Chat1.Server/"]
COPY ["Chat1/Regulus.Samples.Chat1.Common/Regulus.Samples.Chat1.Common.csproj", "Chat1/Regulus.Samples.Chat1.Common/"]
COPY ["Chat1/Regulus.Samples.Chat1/Regulus.Samples.Chat1.csproj", "Chat1/Regulus.Samples.Chat1/"]
RUN dotnet restore "Chat1/Regulus.Samples.Chat1.Server/Regulus.Samples.Chat1.Server.csproj"
COPY . .
WORKDIR "/src/Chat1/Regulus.Samples.Chat1.Server"
RUN dotnet build "Regulus.Samples.Chat1.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Regulus.Samples.Chat1.Server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 53771
ENTRYPOINT ["dotnet", "Regulus.Samples.Chat1.Server.dll" ,"--port", "53771" ,"--mode" ,"websocket"]