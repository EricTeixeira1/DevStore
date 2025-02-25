#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DevStore.Web/DevStore.Web.csproj", "DevStore.Web/"]
COPY ["DevStore.SharedKernel/DevStore.SharedKernel.csproj", "DevStore.SharedKernel/"]
COPY ["DevStore.Domain/DevStore.Domain.csproj", "DevStore.Domain/"]
COPY ["DevStore.Application/DevStore.Application.csproj", "DevStore.Application/"]
COPY ["DevStore.Infra/DevStore.Infra.csproj", "DevStore.Infra/"]
RUN dotnet restore "./DevStore.Web/DevStore.Web.csproj"
COPY . .
WORKDIR "./DevStore.Web"
RUN dotnet build "./DevStore.Web.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DevStore.Web.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DevStore.Web.dll"]