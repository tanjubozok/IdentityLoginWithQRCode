using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace IdentityLoginWithQRCode.Models;

public class DatabaseContext : IdentityDbContext<AppUser, AppRole, int>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }
}
