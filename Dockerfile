FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["UserManagement.MVC/UserManagement.MVC.csproj", "UserManagement.MVC/"]
COPY ["UserManagement.Application/UserManagement.Application.csproj", "UserManagement.Application/"]
COPY ["UserManagement.Domain/UserManagement.Domain.csproj", "UserManagement.Domain/"]
COPY ["UserManagement.Persistence/UserManagement.Persistence.csproj", "UserManagement.Persistence/"]
RUN dotnet restore "UserManagement.MVC/UserManagement.MVC.csproj"
RUN dotnet restore "UserManagement.Application/UserManagement.Application.csproj"
RUN dotnet restore "UserManagement.Domain/UserManagement.Domain.csproj"
RUN dotnet restore "UserManagement.Persistence/UserManagement.Persistence.csproj"
COPY . .
WORKDIR "/src/UserManagement.MVC"
RUN dotnet build "UserManagement.MVC.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "UserManagement.MVC.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "UserManagement.MVC.dll"]
