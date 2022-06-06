using System;
using System.Collections.Generic;
using System.Linq;
using Di.Qry.Core;
using DI.Queries;

namespace Di.Qry.Requests
{
    public class QryFilter : IQryFilter
    {
        public List<QryFilter> Rules { get; set; }

        public string Condition { get; set; }

        public string Field { get; set; }

        public string Operator { get; set; }
        public object Value { get; set; }

        IEnumerable<IQryFilter> IQryFilter.Rules => Rules;

        public bool IsOr => string.Compare(Condition, "or", StringComparison.InvariantCultureIgnoreCase) == 0;

        public bool HasChildRules => Rules != null && Rules.Any();

        public bool IsRuleSet => string.IsNullOrEmpty(Field) && HasChildRules;

        public bool HasOperator => !string.IsNullOrEmpty(Operator);
        public bool IsInRule => HasOperator && string.Compare(Operator, "in", StringComparison.OrdinalIgnoreCase) == 0;

        public bool IsEmptyFilter()
        {
            return HasChildRules && Rules.Any(rl => rl.Value != null);
        }
    }
}