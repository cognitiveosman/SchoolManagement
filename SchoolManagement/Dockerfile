FROM microsoft/dotnet:2.2-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /src
COPY ["SchoolManagement/SchoolManagement.csproj", "SchoolManagement/"]
RUN dotnet restore "SchoolManagement/SchoolManagement.csproj"
COPY . .
WORKDIR "/src/SchoolManagement"
RUN dotnet build "SchoolManagement.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "SchoolManagement.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SchoolManagement.dll"]