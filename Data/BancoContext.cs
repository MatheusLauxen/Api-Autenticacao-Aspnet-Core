using AuthCore.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthCore.Data
{
    public class BancoContext : DbContext
    {
        public BancoContext(DbContextOptions<BancoContext> options) : base(options)
        {
        }
        public DbSet<SegurancaUsuarioModel> SegurancaUsuario { get; set; }
    }
}
