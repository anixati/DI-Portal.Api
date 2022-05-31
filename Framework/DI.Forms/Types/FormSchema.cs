using DI.Domain.Core;
using System.Collections.Generic;

namespace DI.Forms.Types
{
    public class FormSchema
    {
        public FormSchema(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public List<FormField> Fields { get; set; } = new();
    }

    public class FormEntity
    {
        public string Title { get; }

        public FormEntity(string title, IEntity entity)
        {
            Title = title;
            Id = entity.Id;
            Locked = entity.Locked;
            Disabled = entity.Disabled;
            Deleted = entity.Deleted;

        }

        public long Id { get;  }

        public bool Locked { get;  }

        public bool Disabled { get;  }

        public bool Deleted { get;  }
    }
}