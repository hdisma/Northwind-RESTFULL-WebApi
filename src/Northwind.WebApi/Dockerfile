# pull asp.net core v3.1 runtime 
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
ARG CONNECTIONSTRING

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY . .

RUN dotnet restore #src/Northwind.WebApi/*.csproj -s https://api.nuget.org/v3/index.json
COPY . .
WORKDIR "/src/src/Northwind.WebApi"
RUN dotnet build "Northwind.WebApi.csproj" -c Release -o /app/build
#
FROM build AS publish
RUN dotnet publish "Northwind.WebApi.csproj" -c Release -o /app/publish
#
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Northwind.WebApi.dll"]
