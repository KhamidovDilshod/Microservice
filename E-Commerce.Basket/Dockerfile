FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.Basket/E-Commerce.Basket.csproj", "E-Commerce.Basket/"]
RUN dotnet restore "E-Commerce.Basket/E-Commerce.Basket.csproj"
COPY . .
WORKDIR "/src/E-Commerce.Basket"
RUN dotnet build "E-Commerce.Basket.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.Basket.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.Basket.dll"]
