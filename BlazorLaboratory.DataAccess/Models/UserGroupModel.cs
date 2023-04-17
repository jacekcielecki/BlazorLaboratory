namespace BlazorLaboratory.DataAccess.Models;

public class UserGroupModel
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateCreated { get; set; }
    public Guid CreatorId { get; set; }
    public UserModel Creator { get; set; }
    public Guid ModeratorId { get; set; }
    public UserModel Moderator { get; set; }
    public List<UserModel> Users { get; set; }
}
