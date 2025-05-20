# Use the official .NET 7 SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy the project file and restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy the rest of the application code
COPY . ./

# Build and publish the release to the out directory
RUN dotnet publish ./CinemaWebApp/CinemaWebApp.csproj -c Release -o /app/out


# Use the ASP.NET Core Runtime image for running the app
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out ./

# Expose port 80 and run the app
EXPOSE 80
ENTRYPOINT ["dotnet", "Cinema.dll"]
