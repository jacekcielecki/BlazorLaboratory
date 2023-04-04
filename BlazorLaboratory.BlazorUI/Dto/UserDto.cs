using BlazorLaboratory.BlazorUI.Enum;

namespace BlazorLaboratory.BlazorUI.Dto;

public class UserDto
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public UserRole Role { get; set; }
}
