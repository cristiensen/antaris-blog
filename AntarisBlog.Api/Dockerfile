# ==== BUILD STAGE ====
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build

WORKDIR /src

COPY AntarisBlog.Api.csproj .

RUN dotnet restore

COPY . .

RUN dotnet publish -c Release -o /app/publish

# ===== RUNTIME STAGE =====
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime

WORKDIR /app


COPY --from=build /app/publish ./

ENV ASPNETCORE_URLS=http://+:5057
EXPOSE 5057

ENTRYPOINT ["dotnet", "AntarisBlog.Api.dll"]