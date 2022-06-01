using DI.Domain.Core;

namespace DI.Forms.Core
{
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