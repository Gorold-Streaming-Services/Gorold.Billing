FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 5232

ENV ASPNETCORE_URLS=http://+:5232

# Creates a non-root user with an explicit UID and adds permission to access the /app folder
# For more info, please refer to https://aka.ms/vscode-docker-dotnet-configure-containers
RUN adduser -u 5678 --disabled-password --gecos "" appuser && chown -R appuser /app
USER appuser

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Gorold.Billing/Gorold.Billing.csproj", "Gorold.Billing/"]
COPY ["Gorold.Billing.Contracts/Gorold.Billing.Contracts.csproj", "Gorold.Billing.Contracts/"]

RUN --mount=type=secret,id=GH_OWNER,dst=/GH_OWNER --mount=type=secret,id=GH_PAT,dst=/GH_PAT \
    dotnet nuget add source --username USERNAME --password `cat /GH_PAT` --store-password-in-clear-text --name github "https://nuget.pkg.github.com/`cat /GH_OWNER`/index.json"

RUN dotnet restore "Gorold.Billing/Gorold.Billing.csproj"
COPY . .
WORKDIR "/src/Gorold.Billing"
RUN dotnet build "Gorold.Billing.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Gorold.Billing.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Gorold.Billing.dll"]
