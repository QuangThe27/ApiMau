Các thư viện cần cài đặt trong dự án
- Microsoft.AspNetCore.Authentication.JwtBearer (8.0.3)
- Microsoft.EntityFrameworkCore
- Microsoft.EntityFrameworkCore.SqlServer
- Swashbuckle.AspNetCore
- Swashbuckle.AspNetCore.SwaggerGen
- Swashbuckle.AspNetCore.Swagger
- Microsoft.EntityFrameworkCore.SqlServer

Dự án sử dụng SQL Sever

1. Ở commit đầu là cách làm xác thực và phân quyền bằng JWT (JSON Web Token).
  - Sau khi đăng nhập thì mã token về và xác nhận xem tài khoản đó có đủ quyền để sử dụng các /api tiếp theo không 
2. Vòng đời request sử dụng
  - RQ -> pipeline (chứa Middleware) -> file(filter, action) -> controller -> buss -> controller -> pipeline -> CL
