using Microsoft.EntityFrameworkCore;

namespace FSC.Api.Seguridad.Modelos
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseMySql("Server=localhost;Database=dashboard;User=root;Password=;Port=3306",
                    new MySqlServerVersion(new Version(5, 7, 33)));
            }
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<ModelRole> ModelRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ModelRole>(entity =>
            {
                entity.HasKey(mr => new
                {
                    mr.role_id,
                    mr.model_id
                });

                entity.HasOne<Role>(mr => mr.Role)
                      .WithMany(r => r.ModelRoles)
                      .HasForeignKey(mr => mr.role_id)
                      .HasPrincipalKey(r => r.id);
             
                entity.HasOne<User>(mr => mr.User)
                      .WithMany(u => u.ModelRoles)
                      .HasForeignKey(mr => mr.model_id)
                      .HasPrincipalKey(u => u.id);
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(rp => new
                {
                    rp.role_id,
                    rp.permission_id
                });

                entity.HasOne(rp => rp.Role)
                      .WithMany(r => r.RolePermissions)
                      .HasForeignKey(rp => rp.role_id)
                      .HasPrincipalKey(r => r.id);

                entity.HasOne(rp => rp.Permission)
                      .WithMany(p => p.RolePermissions)
                      .HasForeignKey(rp => rp.permission_id)
                      .HasPrincipalKey(p => p.id);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(r => new
                {
                    r.id
                });

                entity.HasMany(r => r.RolePermissions)
                      .WithOne(rp => rp.Role)
                      .HasForeignKey(r => r.role_id)
                      .HasPrincipalKey(rp => rp.id);

                entity.HasMany(r => r.ModelRoles)
                      .WithOne(mr => mr.Role)
                      .HasForeignKey(r => r.role_id)
                      .HasPrincipalKey(mr => mr.id);
            });                                                                                     

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => new
                {
                    u.id
                });

                entity.HasMany(u => u.ModelRoles)
                      .WithOne(mr => mr.User)
                      .HasForeignKey(u => u.model_id)
                      .HasPrincipalKey(mr => mr.id);
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasKey(p => new
                {
                    p.id
                });

                entity.HasMany(r => r.RolePermissions)
                      .WithOne(rp => rp.Permission)
                      .HasForeignKey(r => r.permission_id)
                      .HasPrincipalKey(rp => rp.id);
            });
        }
    }
}
