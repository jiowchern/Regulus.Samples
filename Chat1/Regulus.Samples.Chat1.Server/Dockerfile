FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS chat1-build

WORKDIR /app

RUN git clone --recurse-submodules https://github.com/jiowchern/Regulus.Samples.git

RUN dotnet publish  ./Regulus.Samples/Chat1/Regulus.Samples.Chat1.Server -o /bin -c Release


FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS chat1-run
WORKDIR /app

COPY --from=chat1-build /bin .

### RUN dotnet Regulus.Samples.Chat1.Server.dll 31716


ENTRYPOINT ["dotnet", "Regulus.Samples.Chat1.Server.dll", "31716"]