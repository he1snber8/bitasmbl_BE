FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Project-Backend-2024/Project-Backend-2024.csproj", "Project-Backend-2024/"]
COPY ["Project-Backend-2024.Repositories/Project-Backend-2024.Repositories.csproj", "Project-Backend-2024.Repositories/"]
COPY ["Project-Backend-2024.DTO/Project-Backend-2024.DTO.csproj", "Project-Backend-2024.DTO/"]
COPY ["Project-Backend-2024.Facade/Project-Backend-2024.Facade.csproj", "Project-Backend-2024.Facade/"]
COPY ["Project-Backend-2024.Services/Project-Backend-2024.Services.csproj", "Project-Backend-2024.Services/"]
RUN dotnet restore "Project-Backend-2024/Project-Backend-2024.csproj"
COPY . .
WORKDIR "/src/Project-Backend-2024"
RUN dotnet build "Project-Backend-2024.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Project-Backend-2024.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Project-Backend-2024.dll"]
