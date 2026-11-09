using System.Security.Claims;

public static class ClaimsPrincipalExtensions
{
    // Auth0 user id ("sub" claim, mapped to NameIdentifier)
    public static string GetUserId(this ClaimsPrincipal user) =>
        user.FindFirstValue(ClaimTypes.NameIdentifier)
        ?? throw new InvalidOperationException("User is not authenticated");
}
