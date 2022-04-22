namespace DI.Services.Requests
{
    public abstract class SearchRequestBase
    {
        protected SearchRequestBase() : this(null, null)
        {
        }

        protected SearchRequestBase(int? size, int? index)
        {
            Size = size;
            Index = index;
        }

        public int? Index { get; }
        public int? Size { get; }
    }
}