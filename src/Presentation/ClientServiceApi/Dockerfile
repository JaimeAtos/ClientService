FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS auth

ARG gituser
ARG gitpass

RUN dotnet nuget add source --username $gituser --password $gitpass --store-password-in-clear-text --name github "https://nuget.pkg.github.com/Talent-management/index.json"

FROM auth AS build
WORKDIR /clientservice

COPY ["src", "src/"]

RUN dotnet restore "src/Presentation/ClientServiceApi/ClientServiceApi.csproj"

WORKDIR /clientservice/src/Presentation/ClientServiceApi

RUN dotnet build "ClientServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientServiceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

ARG dbpass
ARG dbuser
ARG dbhost
ARG dbport
ARG dbname

ARG mqpass
ARG mquser
ARG mqhost
ARG mqport

ENV DBPASSWORD=$dbpass
ENV DBUSER=$dbuser
ENV DBHOST=$dbhost
ENV DBPORT=$dbport
ENV DBNAME=$dbname

ENV MQPASSWORD=$mqpass
ENV MQUSER=$mquser
ENV MQHOST=$mqhost
ENV MQPORT=$mqport

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientServiceApi.dll"]