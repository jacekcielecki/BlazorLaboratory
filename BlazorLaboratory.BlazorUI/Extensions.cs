namespace BlazorLaboratory.BlazorUI;

public static class Extensions
{
    public static void AddDefaultSecurityHeaders(this HttpClient client)
    { 
        client.DefaultRequestHeaders.Add("Content-Security-Policy", 
            new List<string> { "default-src https: object-src 'none'", "default-src 'self'; script-src *.trusted.com"});
        client.DefaultRequestHeaders.Add("X-XSS-Protection", "1; mode=block");
        client.DefaultRequestHeaders.Add("X-Frame-Options", 
            new List<string> { "DENY", "SAMEORIGIN" });
        client.DefaultRequestHeaders.Add("X-Content-Type-Options", "nosniff");
        client.DefaultRequestHeaders.Add("Strict-Transport-Security", "Strict-Transport-Security: max-age=31536000; includeSubDomains; preload");
    }
}
