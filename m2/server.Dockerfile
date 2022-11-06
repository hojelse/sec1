FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/
COPY ./server/server.csproj /src/server/
COPY ./common/common.csproj /src/common/
WORKDIR /src/server/
RUN dotnet restore
COPY ./server/ /src/server/
COPY ./common/ /src/common/
WORKDIR /src/server/
RUN dotnet publish -o /app/

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
RUN apt-get -y update \
  && apt-get install -y curl \
  && curl -sL https://deb.nodesource.com/setup_17.x | bash - \ 
  && apt-get install -y nodejs \
  && apt-get clean \
  && echo 'node verions:' $(node -v) \
  && echo 'npm version:' $(npm -v) \
  && echo 'dotnet version:' $(dotnet --version)
WORKDIR /app/
COPY ./server/*.js .
COPY ./server/*.pem .
EXPOSE 8000
ENTRYPOINT ["dotnet", "server.dll"]
COPY --from=build /app/ .