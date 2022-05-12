using System;
using System.Collections.Generic;
using System.Linq;
using DI.Domain.App;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DI.Domain.Core
{
    public class AuditEvent
    {
        public Dictionary<string, AuditProp> AuditProps = new();
        public List<PropertyEntry> TempProps = new();
        public string TableName { get; private set; }
        public string ActionName { get; private set; }

        public static AuditEvent Create(EntityEntry entry)
        {
            var tracker = new AuditEvent
            {
                TableName = entry.Metadata.GetTableName(),
                ActionName = entry.State.ToString()
            };
            foreach (var property in entry.Properties)
            {
                if (property.IsTemporary)
                {
                    tracker.TempProps.Add(property);
                    continue;
                }

                tracker.AddProp(property.Metadata.Name, x =>
                {
                    switch (entry.State)
                    {
                        case EntityState.Added:
                            x.NewValue = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            x.OldValue = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                        {
                            if (property.IsModified)
                            {
                                x.OldValue = property.OriginalValue;
                                x.NewValue = property.CurrentValue;
                            }

                            break;
                        }
                    }
                });
            }

            return tracker;
        }

        public void AddProp(string propName, Action<AuditProp> action)
        {
            var prop = new AuditProp(propName);
            action(prop);
            AuditProps[prop.Key] = prop;
        }

        internal AuditHistory ToAuditHistory()
        {
            var history = new AuditHistory
            {
                AuditDate = DateTime.UtcNow,
                TableName = TableName,
                Action = ActionName,
                Data = new AuditProps()
            };
            history.Data.AddRange(AuditProps.Values.ToList());
            return history;
        }
    }
}