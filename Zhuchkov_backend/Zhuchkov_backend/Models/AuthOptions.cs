using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Zhuchkov_backend
{
    internal class AuthOptions
    {
        public static string Issuer => "AA";
        public static string Audience => "APIclients";
        public static int LifetimeInYears => 1;
        public static SecurityKey SigningKey => new SymmetricSecurityKey(Encoding.ASCII.GetBytes("superSecretKeyMustBeLoooooong32bitsMore"));

        internal static object GenerateToken(string idTelegram, bool is_admin = false, bool is_super_admin = false)
        {
            var now = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, "user"),
                new Claim("IdTelegram", idTelegram)
            };
            if (is_admin)
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "admin"));
            if (is_super_admin)
                claims.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, "super_admin"));

            ClaimsIdentity identity = new ClaimsIdentity(
                claims,
                "Token",
                ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType
            );

            var jwt = new JwtSecurityToken(
                issuer: Issuer,
                audience: Audience,
                notBefore: now,
                expires: now.AddYears(LifetimeInYears),
                claims: identity.Claims,
                signingCredentials: new SigningCredentials(SigningKey, SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return new { token = encodedJwt };
        }

    }
}