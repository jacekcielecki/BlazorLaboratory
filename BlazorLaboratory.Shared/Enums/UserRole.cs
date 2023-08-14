namespace BlazorLaboratory.Shared.Enums;

using System.ComponentModel;

public enum UserRole
{
    [Description("Viewer")]
    Viewer = 0,
    [Description("Cards Creator")]
    CardsCreator = 1,
    [Description("Administrator")]
    Administrator = 2
}
