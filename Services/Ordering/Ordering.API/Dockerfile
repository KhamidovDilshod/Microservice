﻿FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Ordering/Ordering/Ordering.csproj", "Ordering/"]
RUN dotnet restore "Services/Ordering/Ordering/Ordering.csproj"
COPY . .
WORKDIR "/src/Ordering"
RUN dotnet build "Ordering.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ordering.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ordering.dll"]
