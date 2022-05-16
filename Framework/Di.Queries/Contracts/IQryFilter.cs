using System.Collections.Generic;

namespace Di.Qry.Contracts
{
    public interface IQryFilter
    {
        string Condition { get; }
        string Field { get; }
        string Operator { get; }
        object Value { get; }
        IEnumerable<IQryFilter> Rules { get; }
        bool IsOr { get; }
        bool IsRuleset { get; }
        bool HasChildRules { get; }
    }
}