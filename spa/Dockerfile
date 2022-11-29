#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
RUN apt-get update
RUN apt-get install -y curl
RUN apt-get install -y libpng-dev libjpeg-dev curl libxi6 build-essential libgl1-mesa-glx
RUN curl -sL https://deb.nodesource.com/setup_lts.x | bash -
RUN apt-get install -y nodejs
WORKDIR /src
COPY ["spa.csproj", "."]
COPY ["../backend/infrastructure/backend.infrastructure.database/backend.infrastructure.database.csproj", "../backend/infrastructure/backend.infrastructure.database/"]
COPY ["../backend/infrastructure/backend.infrastructure.database.entities/backend.infrastructure.database.entities.csproj", "../backend/infrastructure/backend.infrastructure.database.entities/"]
COPY ["../backend/application/backend.application/backend.application.csproj", "../backend/application/backend.application/"]
COPY ["../backend/backend.utilities/backend.utilities.csproj", "../backend/backend.utilities/"]
COPY ["../backend/partners/backend.partners.interfaces/backend.partners.interfaces.csproj", "../backend/partners/backend.partners.interfaces/"]
COPY ["../backend/backend.api.contracts/backend.api.contracts.csproj", "../backend/backend.api.contracts/"]
COPY ["../backend/application/backend.application.interfaces/backend.application.interfaces.csproj", "../backend/application/backend.application.interfaces/"]
COPY ["../backend/partners/backend.partners/backend.partners.csproj", "../backend/partners/backend.partners/"]
COPY ["../backend/domain/backend.domain.calculation/backend.domain.calculation.csproj", "../backend/domain/backend.domain.calculation/"]
COPY ["../backend/domain/backend.domain/backend.domain.csproj", "../backend/domain/backend.domain/"]
COPY ["../backend/domain/backend.domain.validation/backend.domain.validation.csproj", "../backend/domain/backend.domain.validation/"]
COPY ["../backend/backend.mappers/backend.mappers.csproj", "../backend/backend.mappers/"]
RUN dotnet restore "./spa.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "spa.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "spa.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "spa.dll"]