using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;
using Task1.Enums;
using Task1.Models;

namespace Task1.Repositories;

public class AuditEntry
{
    public AuditEntry(EntityEntry entry)
    {
        Entry = entry;
    }
    public EntityEntry Entry { get; }
    public string UserId { get; set; }
    public string TableName { get; set; }
    public Dictionary<string, object> KeyValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> OldValues { get; } = new Dictionary<string, object>();
    public Dictionary<string, object> NewValues { get; } = new Dictionary<string, object>();
    public EAuditType AuditType { get; set; }
    public List<string> ChangedColumns { get; } = new List<string>();
    public Audit ToAudit()
    {
        var audit = new Audit();
        audit.UserId = UserId;
        audit.Type = AuditType.ToString();
        audit.TableName = TableName;
        audit.DateTime = DateTime.Now;
        audit.PrimaryKey = JsonConvert.SerializeObject(KeyValues);
        audit.OldValues = OldValues.Count == 0 ? null : JsonConvert.SerializeObject(OldValues);
        audit.NewValues = NewValues.Count == 0 ? null : JsonConvert.SerializeObject(NewValues);
        audit.AffectedColumns = ChangedColumns.Count == 0 ? null : JsonConvert.SerializeObject(ChangedColumns);
        return audit;
    }
}