using BlazorLaboratory.BlazorUI.Services.Interfaces;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace BlazorLaboratory.BlazorUI.Services.Classes;

public class AuthorizationManager : IAuthorizationManager
{
    private IConfiguration _config { get; set; }

    public AuthorizationManager(IConfiguration config)
    {
        _config = config;
    }

    public async Task<string> Login(string email, string password)
    {
        var config = new FirebaseAuthConfig
        {
            ApiKey = _config["Firebase:ApiKey"],
            AuthDomain = _config["Firebase:AuthDomain"],
            Providers = new FirebaseAuthProvider[]
            {
                new EmailProvider()
            }
        };

        FirebaseAuthClient firebaseClient = new FirebaseAuthClient(config);
        UserCredential? userCredential = await firebaseClient.SignInWithEmailAndPasswordAsync(email, password);

        return = userCredential.User.Credential.IdToken;
    }
}
