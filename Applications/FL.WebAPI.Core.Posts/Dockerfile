FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FL.WebAPI.Core.Posts/FL.WebAPI.Core.Posts.csproj", "FL.WebAPI.Core.Posts/"]
RUN dotnet restore "FL.WebAPI.Core.Posts/FL.WebAPI.Core.Posts.csproj"
COPY . .
WORKDIR "/src/FL.WebAPI.Core.Posts"
RUN dotnet build "FL.WebAPI.Core.Posts.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FL.WebAPI.Core.Posts.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FL.WebAPI.Core.Posts.dll"]