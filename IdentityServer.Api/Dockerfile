FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["IdentityServer.Api/IdentityServer.Api.csproj", "IdentityServer.Api/"]
RUN dotnet restore "IdentityServer.Api/IdentityServer.Api.csproj"
COPY . .
WORKDIR "/src/IdentityServer.Api"
RUN dotnet build "IdentityServer.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "IdentityServer.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "IdentityServer.Api.dll"]
