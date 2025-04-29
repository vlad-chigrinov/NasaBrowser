# См. статью по ссылке https://aka.ms/customizecontainer, чтобы узнать как настроить контейнер отладки и как Visual Studio использует этот Dockerfile для создания образов для ускорения отладки.

# Этот этап используется при запуске из VS в быстром режиме (по умолчанию для конфигурации отладки)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Этот этап используется для сборки проекта службы
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Development
WORKDIR /src
COPY ["NasaBrowser.Api/NasaBrowser.Api.csproj", "NasaBrowser.Api/"]
COPY ["NasaBrowser.Application/NasaBrowser.Application.csproj", "NasaBrowser.Application/"]
COPY ["NasaBrowser.Domain/NasaBrowser.Domain.csproj", "NasaBrowser.Domain/"]
COPY ["NasaBrowser.Infrastructure/NasaBrowser.Infrastructure.csproj", "NasaBrowser.Infrastructure/"]
RUN dotnet restore "./NasaBrowser.Api/NasaBrowser.Api.csproj"
COPY . .
WORKDIR "/src/NasaBrowser.Api"
RUN dotnet build "./NasaBrowser.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Этот этап используется для публикации проекта службы, который будет скопирован на последний этап
FROM build AS publish
ARG BUILD_CONFIGURATION=Development
RUN dotnet publish "./NasaBrowser.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Этот этап используется в рабочей среде или при запуске из VS в обычном режиме (по умолчанию, когда конфигурация отладки не используется)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "NasaBrowser.Api.dll"]