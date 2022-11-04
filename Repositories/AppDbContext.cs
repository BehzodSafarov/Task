using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Task1.Enums;
using Task1.Models;

namespace Task1.Repositories;

public class AppDbContext : IdentityDbContext
{
     public DbSet<Product>? Products{get; set;}
     public DbSet<Audit>? AuditLogs { get; set; }

     public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){}


    public virtual async Task<int> SaveChangesAsync(string userId)
    {
        OnBeforeSaveChanges(userId);
        var result = await base.SaveChangesAsync();
        return result;
    }
    private void OnBeforeSaveChanges(string userId)
    {
        ChangeTracker.DetectChanges();
        var auditEntries = new List<AuditEntry>();
        foreach (var entry in ChangeTracker.Entries())
        {
            if (entry.Entity is Audit || entry.State == EntityState.Detached || entry.State == EntityState.Unchanged)
                continue;
            var auditEntry = new AuditEntry(entry);
            auditEntry.TableName = entry.Entity.GetType().Name;
            auditEntry.UserId = userId;
            auditEntries.Add(auditEntry);
            foreach (var property in entry.Properties)
            {
                string propertyName = property.Metadata.Name;
                if (property.Metadata.IsPrimaryKey())
                {
                    auditEntry.KeyValues[propertyName] = property.CurrentValue!;
                    continue;
                }
                switch (entry.State)
                {
                    case EntityState.Added:
                        auditEntry.AuditType = EAuditType.Create;
                        auditEntry.NewValues[propertyName] = property.CurrentValue!;
                        break;
                    case EntityState.Deleted:
                        auditEntry.AuditType = EAuditType.Delete;
                        auditEntry.OldValues[propertyName] = property.OriginalValue!;
                        break;
                    case EntityState.Modified:
                        if (property.IsModified)
                        {
                            auditEntry.ChangedColumns.Add(propertyName);
                            auditEntry.AuditType = EAuditType.Update;
                            auditEntry.OldValues[propertyName] = property.OriginalValue!;
                            auditEntry.NewValues[propertyName] = property.CurrentValue!;
                        }
                        break;
                }
            }
        }
        foreach (var auditEntry in auditEntries)
        {
            AuditLogs!.Add(auditEntry.ToAudit());
        }
    }
}
  
