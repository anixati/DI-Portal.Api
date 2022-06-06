using System.Collections.Generic;

namespace DI.Queries
{
    public interface IQryFilter
    {
        string Condition { get; }
        string Field { get; }
        string Operator { get; }
        object Value { get; }
        IEnumerable<IQryFilter> Rules { get; }
        bool IsOr { get; }
        bool IsRuleSet { get; }
        bool HasChildRules { get; }
        bool HasOperator { get; }
        bool IsInRule { get; }
        bool IsEmptyFilter();
    }
}