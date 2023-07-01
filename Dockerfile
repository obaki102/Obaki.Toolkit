# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /root

# Copy and restore the reference project
COPY ./src/Application/Obaki.Toolkit.Application.csproj reference/
RUN dotnet restore reference/Obaki.Toolkit.Application.csproj

# Copy and build the main project
COPY ./src/Api/ ./
RUN dotnet build ./src/Api/Obaki.Toolkit.Api.csproj -c Release -o /app/build

# Publish stage
FROM build AS publish
RUN dotnet publish ./src/Api/Obaki.Toolkit.Api.csproj -c Release -o /app/publish

# Final stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
EXPOSE 80
ENTRYPOINT ["dotnet", "Obaki.Toolkit.Api.dll"]

