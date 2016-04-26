using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using PropertyManager.Api.Domain;

namespace PropertyManager.Api.Infrastructure
{
    public class PropertyManagerDataContext : IdentityDbContext<IdentityUser>
    {
        public PropertyManagerDataContext() : base("PropertyManager")
        {
        }

        // SQL Tables
        public IDbSet<Lease> Leases { get; set; }
        public IDbSet<Property> Properties { get; set; }
        public IDbSet<Tenant> Tenants { get; set; }
        public IDbSet<WorkOrder> WorkOrders { get; set; }


        // Model Relationships
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Property
            modelBuilder.Entity<Property>()
                .HasMany(p => p.Leases)
                .WithRequired(l => l.Property)
                .HasForeignKey(l => l.PropertyId);

            modelBuilder.Entity<Property>()
                .HasMany(p => p.WorkOrders)
                .WithRequired(wo => wo.Property)
                .HasForeignKey(wo => wo.PropertyId);

            // Tenant
            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.Leases)
                .WithRequired(l => l.Tenant)
                .HasForeignKey(l => l.TenantId);

            modelBuilder.Entity<Tenant>()
                .HasMany(t => t.WorkOrders)
                .WithOptional(wo => wo.Tenant)
                .HasForeignKey(wo => wo.TenantId);

            modelBuilder.Entity<PropertyManagerUser>()
                        .HasMany(u => u.Properties)
                        .WithRequired(p => p.User)
                        .HasForeignKey(p => p.UserId);

            modelBuilder.Entity<PropertyManagerUser>()
                        .HasMany(u => u.Tenants)
                        .WithRequired(t => t.User)
                        .HasForeignKey(t => t.UserId)
                        .WillCascadeOnDelete(false);

            // will setup all the relationships for ASP Net Identity
            base.OnModelCreating(modelBuilder);
        }

        public System.Data.Entity.DbSet<PropertyManager.Api.Domain.PropertyManagerUser> IdentityUsers { get; set; }
    }
}