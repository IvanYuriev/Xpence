FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 9000

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Scenario.AuthSrv/Scenario.AuthSrv.csproj", "Scenario.AuthSrv/"]
RUN dotnet restore "Scenario.AuthSrv/Scenario.AuthSrv.csproj"
COPY . .
WORKDIR "/src/Scenario.AuthSrv"
RUN dotnet build "Scenario.AuthSrv.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Scenario.AuthSrv.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Scenario.AuthSrv.dll"]
