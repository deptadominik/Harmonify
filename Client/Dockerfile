FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Client/Harmonify.Client.csproj", "Client/"]
COPY ["Shared/Harmonify.Shared.csproj", "Shared/"]
RUN dotnet restore "Client/Harmonify.Client.csproj"
COPY . .
WORKDIR "/src/Client"
RUN dotnet build "Harmonify.Client.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Harmonify.Client.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Harmonify.Client.dll"]
