FROM mcr.microsoft.com/dotnet/sdk:7.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["/src/Application/Obaki.Toolkit.Application.csproj", "Application/"]
COPY ["/src/Api/Obaki.Toolkit.Api.csproj", "Api/"]
RUN dotnet restore Obaki.Toolkit.Api.csproj

COPY . .
WORKDIR "/src/Api"
RUN dotnet build "Obaki.Toolkit.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Obaki.Toolkit.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Obaki.Toolkit.Api.dll"]