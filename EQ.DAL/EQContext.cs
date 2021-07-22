using EQ.Constants;
using EQ.DAL.Maps;
using EQ.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using EQ.Helpers.Hash;

namespace EQ.DAL
{
    public class EQContext : DbContext
    {
        public DbSet<Role> Roles { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Service> Services { get; set; }

        public DbSet<Window> Windows { get; set; }

        public DbSet<Ticket> Tickets{ get; set; }


        public EQContext(DbContextOptions contextOptions) : base(contextOptions)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            new RoleMap().Build(modelBuilder.Entity<Role>());
            new UserMap().Build(modelBuilder.Entity<User>());
            new ServiceMap().Build(modelBuilder.Entity<Service>());
            new WindowMap().Build(modelBuilder.Entity<Window>());
            new TicketMap().Build(modelBuilder.Entity<Ticket>());

            this.InitDb(modelBuilder);
        }

        private void InitDb(ModelBuilder modelBuilder)
        {
            var adminRoleId = Guid.NewGuid();

            modelBuilder.Entity<Role>().HasData(
                new Role()
                {
                    Id = adminRoleId,
                    RoleName = RoleConstatns.Admin
                }
            );

            var passHash = HashHelper.GetPasswordHash("1234");

            var admin = new User()
            {
                Id = Guid.NewGuid(),
                Email = "admin@eq.com",
                RoleId = adminRoleId,
                PasswordHash = passHash
            };

            admin.PasswordHash = passHash;

            modelBuilder.Entity<User>().HasData(admin);
        }
    }
}
