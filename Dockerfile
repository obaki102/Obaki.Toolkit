# Build stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy and restore the reference project
COPY ./src/Application/Obaki.Toolkit.Application.csproj reference/
RUN dotnet restore reference/Obaki.Toolkit.Application.csproj

# Copy and build the main project
COPY . .
RUN dotnet build ./src/Api/Obaki.Toolkit.Api.csproj -c Release --no-restore

# Publish the application
RUN dotnet publish ./src/Api/Obaki.Toolkit.Api.csproj -c Release --no-build --no-restore -o /app/publish

# Use a lightweight runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory inside the container
WORKDIR /app

# Copy the published output from the build image
COPY --from=build /app/publish .

# Expose the port on which the application will listen
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "Obaki.Toolkit.Api.dll"]

