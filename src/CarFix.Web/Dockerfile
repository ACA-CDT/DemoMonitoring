#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/CarFix.Web/CarFix.Web.csproj", "src/CarFix.Web/"]
RUN dotnet restore "src/CarFix.Web/CarFix.Web.csproj"
COPY . .
WORKDIR "/src/src/CarFix.Web"
RUN dotnet build "CarFix.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CarFix.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CarFix.Web.dll"]