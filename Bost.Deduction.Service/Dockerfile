#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["Bost.Deduction.Service/Bost.Deduction.Service.csproj", "Bost.Deduction.Service/"]
RUN dotnet restore "Bost.Deduction.Service/Bost.Deduction.Service.csproj"
COPY . .
WORKDIR "/src/Bost.Deduction.Service"
RUN dotnet build "Bost.Deduction.Service.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bost.Deduction.Service.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bost.Deduction.Service.dll"]