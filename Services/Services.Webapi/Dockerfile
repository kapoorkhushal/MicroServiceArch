#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Services/Services.Webapi/Services.Webapi.csproj", "Services/Services.Webapi/"]
COPY ["Services/Services.Application/Services.Application.csproj", "Services/Services.Application/"]
COPY ["Services/Services.Domain/Services.Domain.csproj", "Services/Services.Domain/"]
COPY ["Services/Services.Infrastructure/Services.Infrastructure.csproj", "Services/Services.Infrastructure/"]
RUN dotnet restore "Services/Services.Webapi/Services.Webapi.csproj"
COPY . .
WORKDIR "/src/Services/Services.Webapi"
RUN dotnet build "Services.Webapi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Services.Webapi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Services.Webapi.dll"]