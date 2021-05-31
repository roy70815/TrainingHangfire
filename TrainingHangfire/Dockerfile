#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["TrainingHangfire/TrainingHangfire.csproj", "TrainingHangfire/"]
COPY ["TrainingHangfire.Service/TrainingHangfire.Service.csproj", "TrainingHangfire.Service/"]
COPY ["TrainingHangfire.Repository/TrainingHangfire.Repository.csproj", "TrainingHangfire.Repository/"]
COPY ["TrainingHangfire.Common/TrainingHangfire.Common.csproj", "TrainingHangfire.Common/"]
RUN dotnet restore "TrainingHangfire/TrainingHangfire.csproj"
COPY . .
WORKDIR "/src/TrainingHangfire"
RUN dotnet build "TrainingHangfire.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "TrainingHangfire.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TrainingHangfire.dll"]