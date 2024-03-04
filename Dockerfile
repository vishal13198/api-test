# Use the .NET SDK image to build the application
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Update apt-get and install Node.js
RUN apt-get update && \
    apt-get -y install curl && \
    curl -sL https://deb.nodesource.com/setup_16.x | bash - && \
    apt-get -y install nodejs

# Copy the application files into the container
COPY . .

# Restore dependencies
RUN dotnet restore

# Build the application
RUN dotnet build "ShuttleInfraAPI.csproj" -c Release -o /app/build

# Publish the application
RUN dotnet publish "ShuttleInfraAPI.csproj" -c Release -o /app/publish


# Use the .NET runtime image as the base image
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
COPY --from=build /app/publish .

# Expose port 5000 for the application
EXPOSE 5000

# Set the entry point for the application
ENTRYPOINT ["dotnet", "ShuttleInfraAPI.dll"]
