#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/LibraryManagement.Api/LibraryManagement.Api.csproj", "src/LibraryManagement.Api/"]
COPY ["src/LibraryManagement.Application/LibraryManagement.Application.csproj", "src/LibraryManagement.Application/"]
COPY ["src/LibraryManagement.Domain/LibraryManagement.Domain.csproj", "src/LibraryManagement.Domain/"]
COPY ["src/LibraryManagement.Contract/LibraryManagement.Contract.csproj", "src/LibraryManagement.Contract/"]
COPY ["src/LibraryManagement.Infastructure/LibraryManagement.Infastructure.csproj", "src/LibraryManagement.Infastructure/"]
RUN dotnet restore "./src/LibraryManagement.Api/LibraryManagement.Api.csproj"
COPY . .
WORKDIR "/src/src/LibraryManagement.Api"
RUN dotnet build "./LibraryManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./LibraryManagement.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LibraryManagement.Api.dll"]