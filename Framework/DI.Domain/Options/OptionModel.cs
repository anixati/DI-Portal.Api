using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Options
{
    public class OptionModel : NamedViewModel
    {
        public OptionModel(string code)
        {
            Code = code;
        }

        public string Code { get; }

    }
}