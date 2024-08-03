Scaffold command for DB context update

--Use package manager console

Scaffold-DbContext "data source=SACHITH-PC;Initial Catalog=SalesTracking;Persist Security Info=False;User ID=sa;Password=pass#word1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=True;Connection Timeout=30;" Microsoft.EntityFrameworkCore.SqlServer  -context "DatabaseContext" -Force -NoPluralize -NoOnConfiguring