using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace C_Sharp_Basic;

public class JWTToken
{

    public static void Run()
    {
        string UserId = "3FECC299-AE7E-4557-8506-38493B28AA52";
        GenerateToken(UserId);
    }

    public static string GenerateToken(string userId)
    {
        var secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";
        byte[] key = Convert.FromBase64String(secret);
        SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);
        SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[] {
                  new Claim(ClaimTypes.Name, userId), new Claim(ClaimTypes.Role, "0")}),
            Expires = DateTime.Now.AddDays(5),
            SigningCredentials = new SigningCredentials(securityKey,
            SecurityAlgorithms.HmacSha256Signature)
        };

        JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
        JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);
        var result =  handler.WriteToken(token);
        Console.WriteLine(result);
        return result;
    }
}
