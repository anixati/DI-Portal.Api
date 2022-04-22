namespace DI.Core
{
    public enum ResponseCode : short
    {
        Default = 0,
        Created = 1,
        Updated = 2,
        Deleted = 3,
        Disabled = 4,
        Enabled = 5,
        Locked = 6,
        Unlocked = 7,
        StateChanged = 8,
        Superseded = 9,
        NotFound = 10,
        BadRequest = 11,
        ServerError = 12,
        TimedOut = 13,
        Duplicate = 14,
        UnAuthorized = 15
    }
}