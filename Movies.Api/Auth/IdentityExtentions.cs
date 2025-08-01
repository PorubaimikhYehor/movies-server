using System;

namespace Movies.Api.Auth;

public static class IdentityExtentions
{
    public static Guid? GetUserId(this HttpContext context)
    {
        var userId = context.User.Claims.SingleOrDefault(x => x.Type == "userid");
        if (Guid.TryParse(userId?.Value, out var parsedId))
        {
            return parsedId;
        }
        return null;
    }
}
