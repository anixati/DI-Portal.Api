using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DI.Domain.App;
using DI.Domain.Config;
using DI.Domain.Core;
using DI.Domain.Features;
using DI.Domain.Options;
using DI.Domain.Users;
using DI.Exceptions;
using DI.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Domain.Services
{
    public abstract class DomainDbBase<T> : DbContext where T : DbContext
    {
        private const string Collation = "app_collation";
        private readonly IIdentityProvider _identityProvider;

        protected DomainDbBase()
        {
        }

        protected DomainDbBase(DbContextOptions<T> options, IIdentityProvider userIdentityProvider) : base(options)
        {
            _identityProvider = userIdentityProvider;
        }

        protected virtual string DefaultSchema => "Dbo";
        public DbSet<AuditHistory> AuditHistory { get; set; }
        public DbSet<AppConfigEntity> Settings { get; set; }
        public DbSet<AutoNumberEntity> AutoNumbers { get; set; }
        public DbSet<OptionKey> OptionKeys { get; set; }
        public DbSet<AppUser> Users { get; set; }
        public DbSet<AppRole> Roles { get; set; }
        public DbSet<AppResource> Resources { get; set; }


        public DbSet<Activity> Activities { get; set; }
        public DbSet<DeleteRecord> DeleteRecords { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.HasCollation(Collation, "en-u-ks-primary", "icu", false);
            // builder.UseDefaultColumnCollation(Collation);
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(DomainDbBase<>).Assembly);
            builder.ApplyConfigurationsFromAssembly(typeof(T).Assembly);
            builder.HasDefaultSchema(DefaultSchema);
            ConfigureModels(builder);
        }

        protected virtual void ConfigureModels(ModelBuilder builder)
        {

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
        {
            ChangeTracker.DetectChanges();
            var entries = ChangeTracker.Entries();
            SetAuditFields(entries);
            ValidateEntries(entries);
            var auditEvents = GetAuditEvents(entries);
            var result = await base.SaveChangesAsync(cancellationToken);
            if (result > 0)
                await OnPostSave(auditEvents);
            return result;
        }

        private void SetAuditFields(IEnumerable<EntityEntry> entries)
        {
            foreach (var entry in entries)
                SetAuditField(entry);
        }

        private async Task OnPostSave(IReadOnlyCollection<AuditEvent> auditEvents)
        {
            if (auditEvents == null || auditEvents.Count == 0)
                return;
            foreach (var auditEvent in auditEvents)
            {
                foreach (var prop in auditEvent.TempProps)
                    auditEvent.AddProp(prop.Metadata.Name,
                        x => x.NewValue = prop.CurrentValue);
                await AuditHistory.AddAsync(auditEvent.ToAuditHistory());
            }

            await base.SaveChangesAsync();
        }

        private void ValidateEntries(IEnumerable<EntityEntry> entries)
        {
            var errors = new List<ValidationResult>(); // all errors are here
            foreach (var e in entries.Where(x => x.State == EntityState.Added ||
                                                 x.State == EntityState.Modified))
            {
                var vc = new ValidationContext(e.Entity, null, null);
                Validator.TryValidateObject(
                    e.Entity, vc, errors, true);
            }

            if (errors.Any())
                throw new DataValidationException(errors);
        }

        private List<AuditEvent> GetAuditEvents(IEnumerable<EntityEntry> entries)
        {
            var retVal = new List<AuditEvent>();
            foreach (var entry in entries)
            {
                if (entry.Entity is AuditHistory || entry.State == EntityState.Detached ||
                    entry.State == EntityState.Unchanged)
                    continue;
                retVal.Add(AuditEvent.Create(entry));
            }

            return retVal;
        }

        private void SetAuditField(EntityEntry entry)
        {
            if (_identityProvider == null) return;
            if (entry.State != EntityState.Added && entry.State != EntityState.Modified) return;
            if (!(entry.Entity is IAuditEntity auditEntity)) return;

            var userKey = GetInstanceUserKey();
            if (entry.State == EntityState.Added)
            {
                auditEntity.CreatedOn = DateTime.UtcNow;
                auditEntity.CreatedBy = userKey;
            }

            auditEntity.ModifiedOn = DateTime.UtcNow;
            auditEntity.ModifiedBy = userKey;
        }

        protected string GetInstanceUserKey()
        {
            var user = _identityProvider.GetIdentity();
            return user?.GetKey();
        }

        public static T Create(IServiceProvider serviceProvider)
        {
            var options = serviceProvider.GetRequiredService<DbContextOptions<T>>();
            var provider = serviceProvider.GetRequiredService<IIdentityProvider>();
            return Activator.CreateInstance(typeof(T), options, provider) as T;
        }
    }
}