FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.Catalog/E-Commerce.Catalog.csproj", "E-Commerce.Catalog/"]
RUN dotnet restore "E-Commerce.Catalog/E-Commerce.Catalog.csproj"
COPY . .
WORKDIR "/src/E-Commerce.Catalog"
RUN dotnet build "E-Commerce.Catalog.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.Catalog.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.Catalog.dll"]
