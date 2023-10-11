#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["HackerNews.Api/HackerNews.Api.csproj", "HackerNews.Api/"]
COPY ["HackerNews.Application/HackerNews.Application.csproj", "HackerNews.Application/"]
COPY ["HackerNews.Domain/HackerNews.Domain.csproj", "HackerNews.Domain/"]
COPY ["HackerNews.ServiceClients/HackerNews.Infrastructure.csproj", "HackerNews.ServiceClients/"]
RUN dotnet restore "HackerNews.Api/HackerNews.Api.csproj"
COPY . .
WORKDIR "/src/HackerNews.Api"
RUN dotnet build "HackerNews.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HackerNews.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HackerNews.Api.dll"]