#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["HrOffice/HrOffice.csproj", "HrOffice/"]
RUN dotnet restore "HrOffice/HrOffice.csproj"
COPY . .
WORKDIR "/src/HrOffice"
RUN dotnet build "HrOffice.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HrOffice.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HrOffice.dll"]
