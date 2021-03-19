FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["MSInventario.csproj", "./"]
RUN dotnet restore "MSInventario.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "MSInventario.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "MSInventario.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "MSInventario.dll"]
