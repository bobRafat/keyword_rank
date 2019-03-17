FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 20162
EXPOSE 44381

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY InfoTrack.KeywordRank/InfoTrack.KeywordRank.csproj InfoTrack.KeywordRank/
COPY InfoTrack.KeywordRank/nuget.config InfoTrack.KeywordRank/
RUN dotnet restore InfoTrack.KeywordRank/InfoTrack.KeywordRank.csproj
COPY . .
WORKDIR /src/InfoTrack.KeywordRank
RUN dotnet build InfoTrack.KeywordRank.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish InfoTrack.KeywordRank.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "InfoTrack.KeywordRank.dll"]
