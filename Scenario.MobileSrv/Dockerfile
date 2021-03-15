FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Scenario.MobileSrv/Scenario.MobileSrv.csproj", "Scenario.MobileSrv/"]
RUN dotnet restore "Scenario.MobileSrv/Scenario.MobileSrv.csproj"
COPY . .
WORKDIR "/src/Scenario.MobileSrv"
RUN dotnet build "Scenario.MobileSrv.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scenario.MobileSrv.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scenario.MobileSrv.dll"]
