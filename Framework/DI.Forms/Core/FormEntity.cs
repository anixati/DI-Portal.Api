using DI.Domain.Core;

namespace DI.Forms.Core
{


    public enum FormCmdAcl
    {
        None=0,
        Update = 1 << 0, // 1
        Delete = 1 << 1, // 2
        Lock = 1 << 2, // 4
        UnLock = 1 << 3, // 8
        Enable = 1 << 4, // 16
        Disable = 1 << 5, // 32
        Dialog = 1 << 6, // 64
        All = ~(~0 << 7),
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

        public long Id { get; }

        public bool Locked { get; }

        public bool Disabled { get; }

        public bool Deleted { get; }
        public int CmdAcl { get; set; }
    }
}