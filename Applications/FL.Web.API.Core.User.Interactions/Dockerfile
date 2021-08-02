#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["FL.Web.API.Core.User.Interactions/FL.Web.API.Core.User.Interactions.csproj", "FL.Web.API.Core.User.Interactions/"]
RUN dotnet restore "FL.Web.API.Core.User.Interactions/FL.Web.API.Core.User.Interactions.csproj"
COPY . .
WORKDIR "/src/FL.Web.API.Core.User.Interactions"
RUN dotnet build "FL.Web.API.Core.User.Interactions.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FL.Web.API.Core.User.Interactions.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FL.Web.API.Core.User.Interactions.dll"]