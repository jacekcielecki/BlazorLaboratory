namespace BlazorLaboratory.BlazorUI.Dto;

public class UserModel
{
    public Guid Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int ContactDetailsId { get; set; }
    public ContactDetailsModel? ContactDetails { get; set; }
}
