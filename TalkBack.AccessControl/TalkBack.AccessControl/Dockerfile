#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["TalkBack.AccessControl/TalkBack.AccessControl.csproj", "TalkBack.AccessControl/"]
RUN dotnet restore "TalkBack.AccessControl/TalkBack.AccessControl.csproj"
COPY . .
WORKDIR "/src/TalkBack.AccessControl"
RUN dotnet build "TalkBack.AccessControl.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TalkBack.AccessControl.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TalkBack.AccessControl.dll"]