FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
LABEL description="Web API application"

USER $APP_UID
WORKDIR /app
EXPOSE 5186
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["WebApi/WebApi.csproj", "WebApi/"]
COPY ["DataBaseAccess/DataBaseAccess.csproj", "DataBaseAccess/"]
COPY ["CollectionLibrary/CollectionLibrary.csproj", "CollectionLibrary/"]
COPY ["Interfaces/Interfaces.csproj", "Interfaces/"]
COPY ["Commons/Commons.csproj", "Commons/"]
COPY ["Application/Application.csproj", "Application/"]
RUN dotnet restore "WebApi/WebApi.csproj"
COPY . .
WORKDIR "/src/WebApi"
RUN dotnet build "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
CMD ["dotnet", "TreasureCollector.dll"]
