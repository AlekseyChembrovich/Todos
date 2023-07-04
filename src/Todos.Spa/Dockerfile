#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Todos.Spa/Todos.Spa.csproj", "src/Todos.Spa/"]
RUN dotnet restore "src/Todos.Spa/Todos.Spa.csproj"
COPY . .
WORKDIR "/src/src/Todos.Spa"
RUN dotnet build "Todos.Spa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todos.Spa.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todos.Spa.dll"]