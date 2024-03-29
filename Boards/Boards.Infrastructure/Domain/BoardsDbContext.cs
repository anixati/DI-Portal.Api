﻿using System.Linq;
using Boards.Domain.Boards;
using Boards.Domain.Roles;
using DI.Domain.Services;
using DI.Reports;
using DI.Security;
using Microsoft.EntityFrameworkCore;

namespace Boards.Infrastructure.Domain
{
    public class BoardsDbContext : DomainDbBase<BoardsDbContext>
    {
        public BoardsDbContext()
        {
        }

        public BoardsDbContext(DbContextOptions<BoardsDbContext> options, IIdentityProvider provider)
            : base(options, provider)
        {
        }

        public DbSet<Portfolio> Portfolios { get; set; }
        public DbSet<Board> Boards { get; set; }
        public DbSet<BoardRole> BoardRoles { get; set; }
        public DbSet<BoardAppointment> BoardAppointments { get; set; }
        public virtual DbSet<DashboardItem> DashboardItems { get; set; }

        protected override void ConfigureModels(ModelBuilder builder)
        {
            foreach (var relationship in builder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
                //enable in runtime
                //relationship.DeleteBehavior = DeleteBehavior.NoAction;

                //enable only on design
                relationship.DeleteBehavior = DeleteBehavior.Cascade;
            builder.Entity<DashboardItem>(x => x.HasNoKey());
        }
    }
}