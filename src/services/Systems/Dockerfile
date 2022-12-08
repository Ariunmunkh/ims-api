FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

ENV TZ="Asia/Ulaanbaatar"

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["src/services/Systems/Systems.csproj", "src/services/Systems/"]
RUN dotnet restore "src/services/Systems/Systems.csproj"
COPY . .
WORKDIR "/src/src/services/Systems"
RUN dotnet build "Systems.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Systems.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Systems.dll"]
