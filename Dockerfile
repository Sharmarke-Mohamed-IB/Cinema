# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /app

# Copy everything and publish
COPY . . 
RUN dotnet publish -c Release -o out

# Run Stage
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime
WORKDIR /app
COPY --from=build /app/out .

# Tell container how to start your app
ENTRYPOINT ["dotnet", "CinemaProject.dll"]

