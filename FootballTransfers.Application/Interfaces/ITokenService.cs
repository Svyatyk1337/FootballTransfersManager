using System.Collections.Generic;

namespace FootballTransfers.Application.Interfaces
{

    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
        string GenerateRefreshToken();
    }
}