FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
WORKDIR /app
COPY ./BtcDemo.Core/*.csproj ./BtcDemo.Core/
COPY ./BtcDemo.Data/*.csproj ./BtcDemo.Data/
COPY ./BtcDemo.Service/*.csproj ./BtcDemo.Service/
COPY ./BtcDemo.API/*.csproj ./BtcDemo.API/
COPY ./BtcDemo.Client/*.csproj ./BtcDemo.Client/
COPY *.sln .
RUN dotnet restore
COPY . .
RUN dotnet publish ./BtcDemo.API/BtcDemo.API.csproj -o /publish/
FROM mcr.microsoft.com/dotnet/aspnet:7.0
WORKDIR /app
COPY --from=build /publish .
ENV ASPNETCORE_ENVIRONMENT=DEVELOPMENT
ENV ASPNETCORE_URLS="http://*:5151"
ENTRYPOINT [ "dotnet","BtcDemo.API.dll" ]



