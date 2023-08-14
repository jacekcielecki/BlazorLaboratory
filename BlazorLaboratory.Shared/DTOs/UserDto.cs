namespace BlazorLaboratory.Shared.DTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int ContactDetailsId { get; set; }
    public ContactDetailsDto? ContactDetails { get; set; }
}
