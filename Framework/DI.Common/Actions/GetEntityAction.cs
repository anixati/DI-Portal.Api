using System.ComponentModel.DataAnnotations;

namespace DI.Actions
{
    public class GetEntityAction : ActionBase
    {
        public enum ActionType : short
        {
            Default = 0,
            Nested
        }

        [Required] public long Id { get; set; }

        public ActionType State { get; set; }
    }
}