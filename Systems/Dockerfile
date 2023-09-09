FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

ENV TZ="Asia/Ulaanbaatar"

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Systems/Systems.csproj", "Systems/"]
RUN dotnet restore "Systems/Systems.csproj"
COPY . .
WORKDIR "/src/Systems"
RUN dotnet build "Systems.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Systems.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY ["Systems/Fonts/", "/app/Fonts/"]
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Systems.dll"]