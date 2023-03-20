FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
#EXPOSE 80
#EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["src/Backend.TechChallenge.Api/Backend.TechChallenge.Api.csproj", "Backend.TechChallenge.Api/"]
COPY ["src/Backend.TechChallenge.Core/Backend.TechChallenge.Core.csproj", "Backend.TechChallenge.Core/"]
COPY ["src/Backend.TechChallenge.Infra/Backend.TechChallenge.Infra.csproj", "Backend.TechChallenge.Infra/"]
RUN dotnet restore "Backend.TechChallenge.Api/Backend.TechChallenge.Api.csproj"

RUN echo "Build"
# Build
RUN echo "Build"
COPY . .
WORKDIR "/src/Backend.TechChallenge.Api"
RUN dir -s 
RUN dotnet build "Backend.TechChallenge.Api.csproj" -c Release -o /app/build
 
RUN echo "Publish the app"
# Publish the app
RUN echo "Publish the app"
FROM build AS publish
RUN dotnet publish "Backend.TechChallenge.Api.csproj" -c Release -o /app/publish --self-contained false --no-restore

RUN echo "Build runtime image"
# Build runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Backend.TechChallenge.Api.dll"]