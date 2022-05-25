using DI.Forms.Types;

namespace DI.Forms.Core
{
    public interface IFormState
    {
        FormSchema Build();
    }
}