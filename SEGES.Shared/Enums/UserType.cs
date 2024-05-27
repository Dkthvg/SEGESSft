using System.ComponentModel;

namespace SEGES.Shared.Enums
{
    public enum UserType
    {
        [Description("Administrador")]
        Admin,

        [Description("Usuario")]
        User,

        [Description("Ingeniero de Requisitos")]
        ReqEngineer,

        [Description("Gerente de Proyecto")]
        ProjectManager,

        [Description("Interesado")]
        StakeHolder


    }
}
