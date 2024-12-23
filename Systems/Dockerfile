# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:7.0-jammy AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Install dependencies
RUN apt update
RUN apt install -y libc6 libicu-dev libfontconfig1

# (Optional step) Install the ttf-mscorefonts-installer package 
# to use Microsoft TrueType core fonts in the application
RUN echo ttf-mscorefonts-installer msttcorefonts/accepted-mscorefonts-eula select true | debconf-set-selections
RUN apt install -y ttf-mscorefonts-installer 

FROM mcr.microsoft.com/dotnet/sdk:7.0-jammy AS build
WORKDIR /src

# Copy project files and build the application
COPY . ./

# Specify your DevExpress NuGet Feed URL as the package source
RUN dotnet nuget add source https://nuget.devexpress.com/kdoXuUfFVKbegBZVQOb8hUPjjYUHz6nKy9jarpjMFWIuEUHNcx/api

# Publish a release
RUN dotnet publish "Systems/Systems.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
COPY ["Systems/Fonts/", "/app/Fonts/"]

# Define the entry point for the application
ENTRYPOINT ["dotnet", "Systems.dll"]