#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["Chaka.Api.Organization/Chaka.Api.Organization.csproj", "Chaka.Api.Organization/"]
RUN dotnet restore "Chaka.Api.Organization/Chaka.Api.Organization.csproj"
COPY . .
WORKDIR "/src/Chaka.Api.Organization"
RUN dotnet build "Chaka.Api.Organization.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Chaka.Api.Organization.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Chaka.Api.Organization.dll"]