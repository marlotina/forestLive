using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace FL.Pereza.Helpers.Standard.JwtToken
{
    public static class JwtTokenHelper
    {
        public const string CLAIM_UNIQUE_NAME = "unique_name";

        public const string CLAIM_ROLE = "role";
        public const string TOKEN_HEADER = "Authorization";
        public static string GetClaim(string token)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenCode = token.Split(' ')[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(tokenCode) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == CLAIM_UNIQUE_NAME).Value;
            return stringClaimValue;
        }

        public static string GetClaimByValue(string token, string claimValue)
        {
            if (string.IsNullOrWhiteSpace(token))
                return null;

            var tokenCode = token.Split(' ')[1];
            var tokenHandler = new JwtSecurityTokenHandler();
            var securityToken = tokenHandler.ReadToken(tokenCode) as JwtSecurityToken;

            var stringClaimValue = securityToken.Claims.First(claim => claim.Type == claimValue).Value;
            return stringClaimValue;
        }
    }
}
