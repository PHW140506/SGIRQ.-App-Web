using System.Security.Claims;
namespace Sgirq.Api.Extensions;
public static class ClaimsExtensions
{
    public static int PersonalId(this ClaimsPrincipal user)
        => int.TryParse(user.FindFirstValue(ClaimTypes.NameIdentifier), out var id) ? id : 0;
}
