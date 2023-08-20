using BlazorLaboratory.BlazorUI.Services.Interfaces;
using Firebase.Auth;
using Firebase.Auth.Providers;

namespace BlazorLaboratory.BlazorUI.Services.Classes;

public class AuthorizationManager : IAuthorizationManager
{
    private IConfiguration _config;
    private readonly FirebaseToken _token;

    public AuthorizationManager(IConfiguration config, FirebaseToken token)
    {
        _config = config;
        _token = token;
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

        _token.Value = userCredential.User.Credential.IdToken;
        return userCredential.User.Credential.IdToken;
    }
}
