#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FL.Web.Api.Core.Votes/FL.Web.Api.Core.Votes.csproj", "FL.Web.Api.Core.Votes/"]
RUN dotnet restore "FL.Web.Api.Core.Votes/FL.Web.Api.Core.Votes.csproj"
COPY . .
WORKDIR "/src/FL.Web.Api.Core.Votes"
RUN dotnet build "FL.Web.Api.Core.Votes.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FL.Web.Api.Core.Votes.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FL.Web.Api.Core.Votes.dll"]