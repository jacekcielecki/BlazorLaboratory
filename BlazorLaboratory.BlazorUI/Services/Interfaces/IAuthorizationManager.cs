namespace BlazorLaboratory.BlazorUI.Services.Interfaces;

public interface IAuthorizationManager
{
    Task<string> Login(string email, string password);
}
