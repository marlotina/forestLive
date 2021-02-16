#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FL.WebAPI.Core.Birds/FL.WebAPI.Core.Birds.csproj", "FL.WebAPI.Core.Birds/"]
RUN dotnet restore "FL.WebAPI.Core.Birds/FL.WebAPI.Core.Birds.csproj"
COPY . .
WORKDIR "/src/FL.WebAPI.Core.Birds"
RUN dotnet build "FL.WebAPI.Core.Birds.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FL.WebAPI.Core.Birds.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FL.WebAPI.Core.Birds.dll"]