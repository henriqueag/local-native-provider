FROM mcr.microsoft.com/dotnet/sdk:7.0 as build

# Argumentos
ARG APP_VERSION

# Preparação
WORKDIR /source
COPY . .

# Execução do build
RUN dotnet restore "local-native-provider.sln"
RUN dotnet build "local-native-provider.sln" --no-restore -c Release /p:Version=${APP_VERSION}

# Publicação
FROM build as publish
RUN dotnet publish "src/LocalNativeProvider/LocalNativeProvider.csproj" --no-build -c Release -o /app/publish

# Final
FROM mcr.microsoft.com/dotnet/runtime:7.0 as final

ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080

WORKDIR /app
COPY --from=publish /app/publish .

ENTRYPOINT [ "dotnet", "LocalNativeProvider.csproj" ]