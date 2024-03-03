using Microsoft.EntityFrameworkCore;
using System;

namespace ShuttleInfraAPI.Models
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Provider> Provider { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Resource> Resource { get; set; }
        public DbSet<SubResource> SubResource { get; set; }
        public DbSet<Users> users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Projects> Projects { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserProject> UserProjects { get; set; }

        public DbSet<OrganizationProject> organizations_projects { get; set; }
        public DbSet<Organizations> Organizations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Users>()
            .HasKey(u => u.Id);
            modelBuilder.Entity<Role>()
            .HasKey(u => u.id);
            modelBuilder.Entity<Projects>()
            .HasKey(u => u.id);
            modelBuilder.Entity<UserProject>()
            .HasKey(u => u.id);
            modelBuilder.Entity<UserRole>()
           .HasKey(u => u.id);
            modelBuilder.Entity<Organizations>()
            .HasKey(u => u.Id);
            modelBuilder.Entity<OrganizationProject>()
            .HasKey(u => u.Id);


            base.OnModelCreating(modelBuilder);
        }


    }
}
