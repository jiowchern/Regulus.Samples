FROM ubuntu:14.04 as chat1-build
WORKDIR /app

RUN apt-get update && apt-get install -y git 

RUN git clone --recurse-submodules https://github.com/jiowchern/Regulus.Samples.git


FROM mcr.microsoft.com/dotnet/core/sdk:2.1 AS chat1-run

WORKDIR /app

COPY --from=chat1-build ./app .

RUN dotnet publish  ./Regulus.Samples/Chat1/Regulus.Samples.Chat1.Server -o /app -c Release


ENTRYPOINT ["dotnet", "Regulus.Samples.Chat1.Server.dll", "31716"]