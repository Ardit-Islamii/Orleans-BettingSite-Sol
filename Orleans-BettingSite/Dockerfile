#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Orleans-BettingSite/Orleans-BettingSite.csproj", "Orleans-BettingSite/"]
RUN dotnet restore "Orleans-BettingSite/Orleans-BettingSite.csproj"
COPY . .
WORKDIR "/src/Orleans-BettingSite"
RUN dotnet build "Orleans-BettingSite.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Orleans-BettingSite.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Orleans-BettingSite.dll"]