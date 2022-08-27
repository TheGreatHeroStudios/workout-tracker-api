#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["workout-tracker-api/workout-tracker-api.csproj", "workout-tracker-api/"]
RUN dotnet restore "workout-tracker-api/workout-tracker-api.csproj"
COPY . .
WORKDIR "/src/workout-tracker-api"
RUN dotnet build "workout-tracker-api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "workout-tracker-api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "workout-tracker-api.dll"]