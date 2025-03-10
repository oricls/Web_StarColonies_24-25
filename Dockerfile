FROM registry.helmo.be/microsoft/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM registry.helmo.be/microsoft/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["StarColonies.Web/StarColonies.Web.csproj", "StarColonies.Web/"]
RUN dotnet restore "StarColonies.Web/StarColonies.Web.csproj"
COPY . .
WORKDIR "/src/StarColonies.Web"
RUN dotnet build "StarColonies.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "StarColonies.Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
EXPOSE 80
ENV ASPNETCORE_HTTP_PORTS=80

WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "StarColonies.Web.dll"]
