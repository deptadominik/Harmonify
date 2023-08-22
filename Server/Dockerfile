FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["Server/Harmonify.Server.csproj", "Server/"]
COPY ["Client/Harmonify.Client.csproj", "Client/"]
COPY ["Shared/Harmonify.Shared.csproj", "Shared/"]
RUN dotnet restore "Server/Harmonify.Server.csproj"
COPY . .
WORKDIR "/src/Server"
RUN dotnet build "Harmonify.Server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Harmonify.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Harmonify.Server.dll"]
