using FirebaseAdminAuthentication.DependencyInjection.Models;
using System.Security.Claims;

namespace BlazorLaboratory.GraphQL.Extensions;

public static class ClaimsPrincipleExtension
{
    public static string? GetUserId(this ClaimsPrincipal claims) => claims.FindFirstValue(FirebaseUserClaimType.ID);
    public static string? GetUserEmail(this ClaimsPrincipal claims) => claims.FindFirstValue(FirebaseUserClaimType.EMAIL);
    public static string? GetUserUsername(this ClaimsPrincipal claims) => claims.FindFirstValue(FirebaseUserClaimType.USERNAME);
    public static string? GetUserEmailVerified(this ClaimsPrincipal claims) => claims.FindFirstValue(FirebaseUserClaimType.EMAIL_VERIFIED);
}
