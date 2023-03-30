using System;
using System.ComponentModel;

namespace DI.Security.Core
{
    [Flags]
    public enum ApplicationRoles
    {
        [Description("None")] None = 0,
        [Description("System Administrator")] SysAdmin = 1,

        [Description("Business Administrator")]
        Admin = 2,
        [Description("Contributor")] Contributor = 4,
        [Description("Readonly User")] Viewer = 8
    }
}