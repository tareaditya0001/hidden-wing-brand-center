FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY HiddenWing.BrandCenter.sln ./
COPY global.json ./
COPY src/backend/HiddenWing.BrandCenter.Api/HiddenWing.BrandCenter.Api.csproj src/backend/HiddenWing.BrandCenter.Api/
COPY src/backend/HiddenWing.BrandCenter.Application/HiddenWing.BrandCenter.Application.csproj src/backend/HiddenWing.BrandCenter.Application/
COPY src/backend/HiddenWing.BrandCenter.Domain/HiddenWing.BrandCenter.Domain.csproj src/backend/HiddenWing.BrandCenter.Domain/
COPY src/backend/HiddenWing.BrandCenter.Infrastructure/HiddenWing.BrandCenter.Infrastructure.csproj src/backend/HiddenWing.BrandCenter.Infrastructure/
RUN dotnet restore src/backend/HiddenWing.BrandCenter.Api/HiddenWing.BrandCenter.Api.csproj
COPY src/backend src/backend
RUN dotnet publish src/backend/HiddenWing.BrandCenter.Api/HiddenWing.BrandCenter.Api.csproj -c Release -o /app/publish --no-restore

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENV ASPNETCORE_URLS=http://+:8080
EXPOSE 8080
ENTRYPOINT ["dotnet", "HiddenWing.BrandCenter.Api.dll"]
