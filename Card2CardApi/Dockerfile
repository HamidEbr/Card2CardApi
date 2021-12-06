#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["Card2CardApi/Card2CardApi.csproj", "Card2CardApi/"]
RUN dotnet restore "Card2CardApi/Card2CardApi.csproj"
COPY . .
WORKDIR "/src/Card2CardApi"
RUN dotnet build "Card2CardApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Card2CardApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Card2CardApi.dll"]