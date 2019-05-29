#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-nanoserver-1809 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-nanoserver-1809 AS build
WORKDIR /src
COPY ["Chaka.Api.Workflow/Chaka.Api.Workflow.csproj", "Chaka.Api.Workflow/"]
RUN dotnet restore "Chaka.Api.Workflow/Chaka.Api.Workflow.csproj"
COPY . .
WORKDIR "/src/Chaka.Api.Workflow"
RUN dotnet build "Chaka.Api.Workflow.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Chaka.Api.Workflow.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "Chaka.Api.Workflow.dll"]