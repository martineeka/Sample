FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["Chaka.Api.Security/Chaka.Api.Security.csproj", "Chaka.Api.Security/"]
RUN dotnet restore "Chaka.Api.Security/Chaka.Api.Security.csproj"
COPY . .
WORKDIR "/src/Chaka.Api.Security"
RUN dotnet build "Chaka.Api.Security.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Chaka.Api.Security.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Chaka.Api.Security.dll"]