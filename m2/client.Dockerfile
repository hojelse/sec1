FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src/
COPY ./client/client.csproj /src/client/
COPY ./common/common.csproj /src/common/
WORKDIR /src/client/
RUN dotnet restore
COPY ./client/ /src/client/
COPY ./common/ /src/common/
WORKDIR /src/client/
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
COPY ./client/*.js .
COPY ./client/*.pem .
EXPOSE 8001
ENTRYPOINT ["dotnet", "client.dll"]
COPY --from=build /app/ .