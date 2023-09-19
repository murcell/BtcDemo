#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BtcDemo.API/BtcDemo.API.csproj", "BtcDemo.API/"]
COPY ["BtcDemo.Service/BtcDemo.Service.csproj", "BtcDemo.Service/"]
COPY ["BtcDemo.Data/BtcDemo.Data.csproj", "BtcDemo.Data/"]
COPY ["BtcDemo.Core/BtcDemo.Core.csproj", "BtcDemo.Core/"]
RUN dotnet restore "BtcDemo.API/BtcDemo.API.csproj"
COPY . .
WORKDIR "/src/BtcDemo.API"
RUN dotnet build "BtcDemo.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BtcDemo.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BtcDemo.API.dll"]