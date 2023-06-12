using Lennujaam_MVC.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Lennujaam_MVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Lend> Lennud { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}