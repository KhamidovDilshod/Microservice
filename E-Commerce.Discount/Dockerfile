FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["E-Commerce.Discount/E-Commerce.Discount.csproj", "E-Commerce.Discount/"]
RUN dotnet restore "E-Commerce.Discount/E-Commerce.Discount.csproj"
COPY . .
WORKDIR "/src/E-Commerce.Discount"
RUN dotnet build "E-Commerce.Discount.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "E-Commerce.Discount.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "E-Commerce.Discount.dll"]
