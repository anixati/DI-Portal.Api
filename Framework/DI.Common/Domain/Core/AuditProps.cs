using System.Collections.Generic;

namespace DI.Domain.Core
{
    public class AuditProps : List<AuditProp>
    {
        public List<PropModel> ToList()
        {
            var rval = new List<PropModel>();
            foreach (var itm in this)
            {
                rval.Add(new PropModel
                {
                    Key = itm.Key,
                    OldValue = itm.OldValue == null ? "" : itm.OldValue.ToString(),
                    NewValue = itm.NewValue == null ? "" : itm.NewValue.ToString(),
                });
            }
            return rval;
        }
    }



    public class PropModel
    {
        public string Key { get; set; }
        public string OldValue { get; set; }
        public string NewValue { get; set; }
    }
}