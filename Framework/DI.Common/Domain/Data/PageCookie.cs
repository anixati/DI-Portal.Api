namespace DI.Domain.Data
{
    public class PageCookie
    {
        public PageCookie(int index, int size)
        {
            Index = index;
            Size = size;
        }

        public int Index { get; }
        public int Size { get; }

        public int GetSkip()
        {
            return (Index - 1) * Size;
        }

        public int GetTake()
        {
            return Size;
        }

        public static PageCookie Default()
        {
            return new PageCookie(1, 100);
        }
    }
}