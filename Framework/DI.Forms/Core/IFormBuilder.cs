namespace DI.Forms.Core
{
    public interface IFormBuilder
    {
        string FormName { get; }
        IFormState Create();
    }
}