FROM mcr.microsoft.com/dotnet/core/aspnet:3.0

COPY build/for-docker/ app/
WORKDIR app/

EXPOSE 80/tcp

ENTRYPOINT ["dotnet", "NorthwindStore.dll"]
