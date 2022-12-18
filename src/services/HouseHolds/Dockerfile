FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

ENV TZ="Asia/Ulaanbaatar"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/services/HouseHolds/HouseHolds.csproj", "src/services/HouseHolds/"]
RUN dotnet restore "src/services/HouseHolds/HouseHolds.csproj"
COPY . .
WORKDIR "/src/src/services/HouseHolds"
RUN dotnet build "HouseHolds.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "HouseHolds.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "HouseHolds.dll"]
