#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src

COPY ["ClientService", "ClientService/"]
COPY ["Atos.Core", "Atos.Core/"]

RUN dotnet restore "ClientService/src/ClientServiceApi/ClientServiceApi.csproj"

COPY ClientService/ .
COPY Atos.Core/ .

WORKDIR "/src/ClientService/src/ClientServiceApi"

RUN dotnet build "ClientServiceApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ClientServiceApi.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final

ARG dbpass
ARG dbuser
ARG dbhost
ARG dbport
ARG dbname

ENV DBPASSWORD=$dbpass
ENV DBUSER=$dbuser
ENV DBHOST=$dbhost
ENV DBPORT=$dbport
ENV DBNAME=$dbname

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ClientServiceApi.dll"]