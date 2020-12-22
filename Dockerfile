#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:5.0
FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
ENV ASPNETCORE_URLS=http://+:4000
EXPOSE 4000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["BlogNetCore/BlogNetCore.csproj", "BlogNetCore/"]
RUN dotnet restore "BlogNetCore/BlogNetCore.csproj"
COPY . .
WORKDIR "/src/BlogNetCore"
RUN dotnet build "BlogNetCore.csproj" -c Debug -o /app/build

FROM build AS publish
RUN dotnet publish "BlogNetCore.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BlogNetCore.dll"]