namespace BlazorLaboratory.DataAccess.Models;

public class UserModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public int ContactDetailsId { get; set; }
    public ContactDetailsModel? ContactDetails { get; set; }
}