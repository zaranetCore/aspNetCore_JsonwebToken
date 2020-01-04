#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
#应用基础镜像
FROM mcr.microsoft.com/dotnet/core/aspnet:3.0-buster-slim AS base
#工作目录
WORKDIR /app
#将整个publish文件 放到该文件下生产
COPY . /app
#指定对外端口
EXPOSE 5003
#启动命令
ENTRYPOINT ["dotnet", "Jwt_Policy_Demo.dll"]