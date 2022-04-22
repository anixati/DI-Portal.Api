using DI.Security;

namespace Boards.Infrastructure.Design
{
    public class MockUser : IIdentity
    {
        public MockUser(string userId, string name)
        {
            UserId = userId;
            Name = name;
        }

        public string UserId { get; }


        public string Name { get; }

        public string GetKey()
        {
            return $"{UserId}_{Name}";
        }
    }
}