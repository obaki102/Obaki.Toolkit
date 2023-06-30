# Use the .NET SDK image as the base image
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory inside the container
WORKDIR /app

# Copy the .csproj and restore dependencies
COPY ./src/Application/Obaki.Toolkit.Application.csproj Api/
COPY ./src/Api/Obaki.Toolkit.Api.csproj Application/
RUN dotnet restore Api/Obaki.Toolkit.Api.csproj

# Copy the remaining source code
COPY ./src/Api/ Api/
COPY ./src/Application/ Application/

# Build the application
RUN dotnet build Api/Obaki.Toolkit.Api.csproj -c Release --no-restore

# Publish the application
RUN dotnet publish Api/Obaki.Toolkit.Api.csproj -c Release --no-build --no-restore -o /app/publish

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
