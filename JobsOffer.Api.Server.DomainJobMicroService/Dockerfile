#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["JobsOffer.Api.Server.DomainJobMicroService/JobsOffer.Api.Server.DomainJobMicroService.csproj", "JobsOffer.Api.Server.DomainJobMicroService/"]
COPY ["JobsOffer.Api.Server/JobsOffer.Api.Server.csproj", "JobsOffer.Api.Server/"]
COPY ["JobsOffer.Api.Business/JobsOffer.Api.Business.csproj", "JobsOffer.Api.Business/"]
COPY ["JobsOffer.Api.UnitOfWork/JobsOffer.Api.UnitOfWork.csproj", "JobsOffer.Api.UnitOfWork/"]
COPY ["JobsOffer.Api.Domain/JobsOffer.Api.Domain.csproj", "JobsOffer.Api.Domain/"]
COPY ["JobsOffer.Api.Infrastructure/JobsOffer.Api.Infrastructure.csproj", "JobsOffer.Api.Infrastructure/"]
RUN dotnet restore "./JobsOffer.Api.Server.DomainJobMicroService/JobsOffer.Api.Server.DomainJobMicroService.csproj"
COPY . .
WORKDIR "/src/JobsOffer.Api.Server.DomainJobMicroService"
RUN dotnet build "./JobsOffer.Api.Server.DomainJobMicroService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./JobsOffer.Api.Server.DomainJobMicroService.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "JobsOffer.Api.Server.DomainJobMicroService.dll"]