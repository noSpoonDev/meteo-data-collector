FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base

WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["MeteoDataCollector.App/MeteoDataCollector.App.csproj", "MeteoDataCollector.App/"]
COPY ["MeteoDataCollector.Core/MeteoDataCollector.Core.csproj", "MeteoDataCollector.Core/"]
COPY ["MeteoDataCollector.Infrastructure/MeteoDataCollector.Infrastructure.csproj", "MeteoDataCollector.Infrastructure/"]
RUN dotnet restore "MeteoDataCollector.App/MeteoDataCollector.App.csproj"

COPY . .
RUN dotnet build "MeteoDataCollector.App/MeteoDataCollector.App.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MeteoDataCollector.App/MeteoDataCollector.App.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT ["dotnet", "MeteoDataCollector.App.dll"]