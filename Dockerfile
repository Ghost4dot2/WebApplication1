FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /source

#copy csproj and resotre as distinct layers
COPY *.sln .
COPY WebApplication1/*.csproj ./WebApplication1/
RUN dotnet restore

# copy everything else and build app
COPY WebApplication1/. ./WebApplication1/
WORKDIR /source/WebApplication1
RUN dotnet publish -c release -o /app -- no-restore

# final stage/image
FROM mcr.microsoft.com/dotnet/aspnet:5.0
WORKDIR /app
COPY --from=build /app ./
ENTRYPOINT ["dotnet", "WebApplication1.dll"]


# https://hub.docker.com/_/microsoft-dotnet
#FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
#WORKDIR /app
#EXPOSE 80

#FROM microsoft/dotnet:5.0-sdk AS build
#WORKDIR /src
#COPY ["WebApplication1.csproj", "./"]
#RUN dotnet restore "./WebApplication1.csproj"
#COPY . .
#WORKDIR "/src/."
#RUN dotnet build "WebApplication1.csproj" -c Release -o /app

#FROM build AS publish
#RUN dotnet publish "WebApplication1.csproj" -c Release -o /app

#FROM base AS final
#WORKDIR /app
#COPY --from=publish /app .
#ENTRYPOINT ["dotnet", "WebApplication1.dll"]

