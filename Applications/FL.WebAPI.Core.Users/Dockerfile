FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["FL.WebAPI.Core.Users/FL.WebAPI.Core.Users.csproj", "FL.WebAPI.Core.Users/"]
RUN dotnet restore "FL.WebAPI.Core.Users/FL.WebAPI.Core.Users.csproj"
COPY . .
WORKDIR "/src/FL.WebAPI.Core.Users"
RUN dotnet build "FL.WebAPI.Core.Users.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "FL.WebAPI.Core.Users.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "FL.WebAPI.Core.Users.dll"]