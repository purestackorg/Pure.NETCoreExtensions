FROM microsoft/aspnetcore:2.0 AS base
WORKDIR /app
EXPOSE 80

FROM microsoft/aspnetcore-build:2.0 AS build
WORKDIR /src
COPY *.sln ./
COPY Pure.NetCoreExtensions.Test/Pure.NetCoreExtensions.Test.csproj Pure.NetCoreExtensions.Test/
COPY Pure.NetCoreExtensions/Pure.NetCoreExtensions.csproj Pure.NetCoreExtensions/
RUN dotnet restore
COPY . .
WORKDIR /src/Pure.NetCoreExtensions.Test
RUN dotnet build -c Release -o /app

FROM build AS publish
RUN dotnet publish -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Pure.NetCoreExtensions.Test.dll"]
