using System.ComponentModel.DataAnnotations;

namespace DI.Actions
{
    public class SetStatusAction : ActionBase
    {
        public enum ActionType : short
        {
            Default = 0,
            Enable = 2,
            Disable = 3,
            Lock = 4,
            UnLock = 5,
            Delete = 6
        }

        [Required] public long Id { get; set; }

        [Required] public ActionType Action { get; set; }

        public string Reason { get; set; }
    }
}