#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Presentation/ETicaretAPI.Api/ETicaretAPI.Api.csproj", "Presentation/ETicaretAPI.Api/"]
COPY ["Core/ETicaretAPI.Application/ETicaretAPI.Application.csproj", "Core/ETicaretAPI.Application/"]
COPY ["Core/ETicaretAPI.Domain/ETicaretAPI.Domain.csproj", "Core/ETicaretAPI.Domain/"]
COPY ["Infrastructure/ETicaretAPI.Persistence/ETicaretAPI.Persistence.csproj", "Infrastructure/ETicaretAPI.Persistence/"]
RUN dotnet restore "./Presentation/ETicaretAPI.Api/ETicaretAPI.Api.csproj"
COPY . .
WORKDIR "/src/Presentation/ETicaretAPI.Api"
RUN dotnet build "./ETicaretAPI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./ETicaretAPI.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ETicaretAPI.Api.dll"]