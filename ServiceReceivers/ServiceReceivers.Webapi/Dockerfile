#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["ServiceReceivers/ServiceReceivers.Webapi/ServiceReceivers.Webapi.csproj", "ServiceReceivers/ServiceReceivers.Webapi/"]
COPY ["ServiceReceivers/ServiceReceivers.Infrastructure/ServiceReceivers.Infrastructure.csproj", "ServiceReceivers/ServiceReceivers.Infrastructure/"]
COPY ["ServiceReceivers/ServiceReceivers.Application/ServiceReceivers.Application.csproj", "ServiceReceivers/ServiceReceivers.Application/"]
COPY ["ServiceReceivers/ServiceReceivers.Domain/ServiceReceivers.Domain.csproj", "ServiceReceivers/ServiceReceivers.Domain/"]
RUN dotnet restore "ServiceReceivers/ServiceReceivers.Webapi/ServiceReceivers.Webapi.csproj"
COPY . .
WORKDIR "/src/ServiceReceivers/ServiceReceivers.Webapi"
RUN dotnet build "ServiceReceivers.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ServiceReceivers.Webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ServiceReceivers.Webapi.dll"]