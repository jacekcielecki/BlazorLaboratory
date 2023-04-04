using System.ComponentModel;

namespace BlazorLaboratory.BlazorUI.Enum;

public enum UserRole
{
    [Description("Viewer")]
    Viewer = 0,
    [Description("Cards Creator")]
    CardsCreator = 1,
    [Description("Administrator")]
    Administrator = 2
}
