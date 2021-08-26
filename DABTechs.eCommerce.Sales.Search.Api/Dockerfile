#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

#Depending on the operating system of the host machines(s) that will build or run the containers, the image specified in the FROM statement may need to be changed.
#For more information, please see https://aka.ms/containercompat

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-nanoserver-1903 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-nanoserver-1903 AS build
WORKDIR /src
COPY ["DABTechs.eCommerce.Sales.Search.Api/DABTechs.eCommerce.Sales.Search.Api.csproj", "DABTechs.eCommerce.Sales.Search.Api/"]
COPY ["DABTechs.eCommerce.Sales.Providers.Azure/DABTechs.eCommerce.Sales.Providers.Azure.csproj", "DABTechs.eCommerce.Sales.Providers.Azure/"]
COPY ["DABTechs.eCommerce.Sales.Business/DABTechs.eCommerce.Sales.Business.csproj", "DABTechs.eCommerce.Sales.Business/"]
COPY ["DABTechs.eCommerce.Sales.Interfaces/DABTechs.eCommerce.Sales.Interfaces.csproj", "DABTechs.eCommerce.Sales.Interfaces/"]
COPY ["DABTechs.eCommerce.Sales.Common/DABTechs.eCommerce.Sales.Common.csproj", "DABTechs.eCommerce.Sales.Common/"]
COPY ["DABTechs.eCommerce.Sales.Domain/DABTechs.eCommerce.Sales.Domain.csproj", "DABTechs.eCommerce.Sales.Domain/"]
COPY ["DABTechs.eCommerce.Sales.Providers.BloomReach/DABTechs.eCommerce.Sales.Providers.BloomReach.csproj", "DABTechs.eCommerce.Sales.Providers.BloomReach/"]
RUN dotnet restore "DABTechs.eCommerce.Sales.Search.Api/DABTechs.eCommerce.Sales.Search.Api.csproj"
COPY . .
WORKDIR "/src/DABTechs.eCommerce.Sales.Search.Api"
RUN dotnet build "DABTechs.eCommerce.Sales.Search.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "DABTechs.eCommerce.Sales.Search.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DABTechs.eCommerce.Sales.Search.Api.dll"]