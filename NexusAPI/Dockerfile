#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Xphyrus.NexusService/Xphyrus.NexusService.csproj", "Xphyrus.NexusService/"]
RUN dotnet restore "Xphyrus.NexusService/Xphyrus.NexusService.csproj"
COPY . .
WORKDIR "/src/Xphyrus.NexusService"
RUN dotnet build "Xphyrus.NexusService.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Xphyrus.NexusService.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Xphyrus.NexusService.dll","--server.urls", "http://+:80;https://+:443"]