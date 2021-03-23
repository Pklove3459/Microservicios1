FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["GatewayTienda.csproj", "./"]
RUN dotnet restore "GatewayTienda.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "GatewayTienda.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "GatewayTienda.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "GatewayTienda.dll"]
