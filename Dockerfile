# https://hub.docker.com/_/microsoft-dotnet
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /source

# copy csproj and restore as distinct layers
COPY *.sln .
COPY LocationLibrary/LocationLibrary.csproj ./LocationLibrary/
COPY WSLocationWebAPI/WSLocationWebAPI.csproj ./WSLocationWebAPI/
COPY LocationTests/LocationTests.csproj ./LocationTests/
RUN dotnet restore

# copy everything else and build app
COPY LocationLibrary/. ./LocationLibrary/
COPY WSLocationWebAPI/. ./WSLocationWebAPI/
COPY LocationTests/. ./LocationTests/
WORKDIR /source/WSLocationWebAPI
# Error restore
#   Supprimer les dossiers bin et obj -> voir .dockerignore
RUN dotnet publish -c release -o /app --no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "aspnetapp.dll"]