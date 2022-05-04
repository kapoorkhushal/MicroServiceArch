#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ServiceProviders/ServiceProviders.Webapi/ServiceProviders.Webapi.csproj", "ServiceProviders/ServiceProviders.Webapi/"]
COPY ["ServiceProviders/ServiceProviders.Application/ServiceProviders.Application.csproj", "ServiceProviders/ServiceProviders.Application/"]
COPY ["ServiceProviders/ServiceProviders.Domain/ServiceProviders.Domain.csproj", "ServiceProviders/ServiceProviders.Domain/"]
COPY ["ServiceReceivers/ServiceReceivers.Domain/ServiceReceivers.Domain.csproj", "ServiceReceivers/ServiceReceivers.Domain/"]
COPY ["ServiceProviders/ServiceProviders.Infrastructure/ServiceProviders.Infrastructure.csproj", "ServiceProviders/ServiceProviders.Infrastructure/"]
RUN dotnet restore "ServiceProviders/ServiceProviders.Webapi/ServiceProviders.Webapi.csproj"
COPY . .
WORKDIR "/src/ServiceProviders/ServiceProviders.Webapi"
RUN dotnet build "ServiceProviders.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceProviders.Webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceProviders.Webapi.dll"]