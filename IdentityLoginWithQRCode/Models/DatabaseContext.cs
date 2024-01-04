namespace IdentityLoginWithQRCode.Models;

public class DatabaseContext(DbContextOptions<DatabaseContext> options)
    : IdentityDbContext<AppUser, AppRole, int>(options)
{
}
