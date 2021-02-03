FROM mcr.microsoft.com/dotnet/runtime:5.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["HelloAKS.csproj", "./"]
RUN dotnet restore "HelloAKS.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "HelloAKS.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HelloAKS.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HelloAKS.dll"]
