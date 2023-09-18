FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
COPY *.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish BtcDemo.Client.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_ENVIRONMENT=DEVELOPMENT
ENV ASPNETCORE_URLS="http://*:5247"
ENTRYPOINT [ "dotnet","BtcDemo.Client.dll" ]