FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["Presentation/UserService.API/UserService.API.csproj", "Presentation/UserService.API/"]
COPY ["Core/UserService.Application/UserService.Application.csproj", "Core/UserService.Application/"]
COPY ["Core/UserService.Domain/UserService.Domain.csproj", "Core/UserService.Domain/"]
COPY ["Infrastructure/UserService.Infrastructure/UserService.Infrastructure.csproj", "Infrastructure/UserService.Infrastructure/"]
COPY ["Infrastructure/UserService.Persistence/UserService.Persistence.csproj", "Infrastructure/UserService.Persistence/"]

RUN dotnet restore "Presentation/UserService.API/UserService.API.csproj"
COPY . .
WORKDIR "/src/Presentation/UserService.API"
RUN dotnet publish "UserService.API.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "UserService.API.dll"]
