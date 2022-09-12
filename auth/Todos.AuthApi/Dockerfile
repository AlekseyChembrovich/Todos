FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["auth/Todos.AuthApi/Todos.AuthApi.csproj", "auth/Todos.AuthApi/"]
RUN dotnet restore "auth/Todos.AuthApi/Todos.AuthApi.csproj"
COPY . .
WORKDIR "/src/auth/Todos.AuthApi"
RUN dotnet build "Todos.AuthApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todos.AuthApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todos.AuthApi.dll"]
