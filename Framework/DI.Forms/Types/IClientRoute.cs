namespace DI.Forms.Types
{
    public interface IClientRoute
    {
        string Key { get; }
        string Path();
    }
}