using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace VA.Identity.Infrastructure.Security.Data
{
    internal class SecurityAppDbContext : IdentityDbContext
    {
        public SecurityAppDbContext(DbContextOptions<SecurityAppDbContext> options) : base(options) { }
    }
}