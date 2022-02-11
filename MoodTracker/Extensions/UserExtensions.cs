
using System.Security.Claims;

namespace MoodTracker.Extensions
{
    public static class UserExtensions
    {
        public static string GetId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public static bool IsLoggedIn(this ClaimsPrincipal user)
        {
            return user.Identity is {IsAuthenticated: true};
        }
    }
}
