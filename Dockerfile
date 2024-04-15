# BUILD TEMP IMAGE 
# (image dùng để release từ source code)
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build-env
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Sao chép file csproj và restore dependencies
COPY *.sln .
COPY ./dotnetcoreapi.cake.shop.domain/*.csproj dotnetcoreapi.cake.shop.domain/
COPY ./dotnetcoreapi.cake.shop.infrastructure/*.csproj dotnetcoreapi.cake.shop.infrastructure/
COPY ./dotnetcoreapi.cake.shop.application/*.csproj ./dotnetcoreapi.cake.shop.application/
COPY ./dotnetcoreapi-cake-shop/*.csproj ./dotnetcoreapi-cake-shop/

RUN dotnet restore

# Sao chép toàn bộ code còn lại và publish ứng dụng
# publish ở chế độ Release vào thư mục /app/out trong image
COPY . .
RUN dotnet publish -c Release -o out

# BUILD FINAL IMAGE 
# (image thực tế sẽ được build chỉ lấy kết quả release từ image trước để chạy)
# Sử dụng runtime để chạy nhẹ hơn
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build-env /app/out .

# Chạy ứng dụng
ENTRYPOINT [ "dotnet", "dotnetcoreapi.cake.shop.dll" ]

# Build image: docker build -t xuanphucdev/dotnet-lafuong .
# Run container: docker run -p 8080:80 --rm --name dotnet-lafuong xuanphucdev/dotnet-lafuong
# Run container custom port: docker run -e ASPNETCORE_URLS=http://+:8080 -p 8080:80 --rm --name dotnet-lafuong xuanphucdev/dotnet-lafuong

# Run Maria DB: docker run -p 3306:3306 --detach --name some-mariadb --env MARIADB_ROOT_PASSWORD=12345678@Abc  mariadb:latest