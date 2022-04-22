using System.ComponentModel;

namespace DI.Security.Core
{


    public enum ApplicationRoles
    {
        [Description("System Administrator")]
        SysAdmin,
        [Description("Business Administrator")]
        Admin,
        [Description("Contributor")]
        Contributor,
        [Description("Readonly User")]
        Viewer,
    }

}
