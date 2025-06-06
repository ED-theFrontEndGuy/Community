# base
# downloads linux -> dotnet sdk etc..
FROM mcr.microsoft.com/dotnet/sdk:latest AS build

# create catalogue /app in vm and change active catalogue there
WORKDIR /app

# copy solution current host from catalogue to container's active catalogue
# from -> where
COPY *.sln .

# copy all projects from solution
COPY App.BLL/*.csproj ./App.BLL/
COPY App.BLL.DTO/*.csproj ./App.BLL.DTO/
COPY App.BLL.Interfaces/*.csproj ./App.BLL.Interfaces/
COPY App.DAL.DTO/*.csproj ./App.DAL.DTO/
COPY App.DAL.EF/*.csproj ./App.DAL.EF/
COPY App.DAL.Interfaces/*.csproj ./App.DAL.Interfaces/
COPY App.Domain/*.csproj ./App.Domain/
COPY App.DTO/*.csproj ./App.DTO/
COPY App.Resources/*.csproj ./App.Resources/
#TODO
#COPY App.Tests/*.csproj ./App.Tests/

COPY WebApp/*.csproj ./WebApp/

COPY Base.BLL/*.csproj ./Base.BLL/
COPY Base.BLL.Interfaces/*.csproj ./Base.BLL.Interfaces/
COPY Base.DAL.EF/*.csproj ./Base.DAL.EF/
COPY Base.DAL.Interfaces/*.csproj ./Base.DAL.Interfaces/
COPY Base.Domain/*.csproj ./Base.Domain/
COPY Base.Helpers/*.csproj ./Base.Helpers/
COPY Base.Interfaces/*.csproj ./Base.Interfaces/
COPY Base.Resources/*.csproj ./Base.Resources/

# recover nuget packages in the container
RUN dotnet restore

# copy whole source code and build app
COPY App.BLL/. ./App.BLL/
COPY App.BLL.DTO/. ./App.BLL.DTO/
COPY App.BLL.Interfaces/. ./App.BLL.Interfaces/
COPY App.DAL.DTO/. ./App.DAL.DTO/
COPY App.DAL.EF/. ./App.DAL.EF/
COPY App.DAL.Interfaces/. ./App.DAL.Interfaces/
COPY App.Domain/. ./App.Domain/
COPY App.DTO/. ./App.DTO/
COPY App.Resources/. ./App.Resources/
COPY WebApp/. ./WebApp/
#TODO
#COPY App.Tests/. ./App.Tests/

COPY Base.BLL/. ./Base.BLL/
COPY Base.BLL.Interfaces/. ./Base.BLL.Interfaces/
COPY Base.DAL.EF/. ./Base.DAL.EF/
COPY Base.DAL.Interfaces/. ./Base.DAL.Interfaces/
COPY Base.Domain/. ./Base.Domain/
COPY Base.Helpers/. ./Base.Helpers/
COPY Base.Interfaces/. ./Base.Interfaces/
COPY Base.Resources/. ./Base.Resources/

RUN dotnet publish -c Release -o out

# TODO - run tests, stop if unsuccessful

# create new image
# arm cpu image
#FROM mcr.microsoft.com/dotnet/aspnet:latest AS runtime
# intel cpu image - 9.0-bookworm-slim-amd64
FROM mcr.microsoft.com/dotnet/aspnet:9.0-bookworm-slim-amd64 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 8080
COPY --from=build /app/out ./
ENV ConnectionStrings:DefaultConnection="Host=host.docker.internal;Port=5432;Database=community;Username=postgres;Password=postgres"
ENTRYPOINT ["dotnet", "WebApp.dll"]