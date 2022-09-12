FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["src/Todos.Api/Todos.Api.csproj", "src/Todos.Api/"]
COPY ["src/Todos.Infrastructure/Todos.Infrastructure.csproj", "src/Todos.Infrastructure/"]
COPY ["src/Todos.Application/Todos.Application.csproj", "src/Todos.Application/"]
COPY ["src/Todos.Domain/Todos.Domain.csproj", "src/Todos.Domain/"]
RUN dotnet restore "src/Todos.Api/Todos.Api.csproj"
COPY . .
WORKDIR "/src/src/Todos.Api"
RUN dotnet build "Todos.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Todos.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
EXPOSE 80
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Todos.Api.dll"]
