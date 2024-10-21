using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Tokens;

namespace ESys.API.Middleware;

public class JwtMiddleware  
{  
    private readonly RequestDelegate _next;  
    private readonly IConfiguration _configuration;   

    public JwtMiddleware(RequestDelegate next, IConfiguration configuration )  
    {  
        _next = next;  
        _configuration = configuration;   
    }  

    public async Task Invoke(HttpContext context)  
    {  
        var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();  

        if (token != null)  
            attachAccountToContext(context, token);  

        await _next(context);  
    }  

    private void attachAccountToContext(HttpContext context, string token)  
    {  
        try  
        {  
            var tokenHandler = new JwtSecurityTokenHandler();  
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);  
            tokenHandler.ValidateToken(token, new TokenValidationParameters  
            {  
                ValidateIssuerSigningKey = true,  
                IssuerSigningKey = new SymmetricSecurityKey(key),  
                ValidateIssuer = false,  
                ValidateAudience = false,  
                // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)  
                ClockSkew = TimeSpan.Zero  
            }, out SecurityToken validatedToken);  
              
            var jwtToken = (JwtSecurityToken)validatedToken;  
              
            //get the user name and role from the JWT token.  
            var username = jwtToken.Claims.First(x => x.Type == "username").Value;  
            var role = jwtToken.Claims.First(x => x.Type == "role").Value;  


            var userClaims = new List<Claim>()  
            {  
                new Claim("UserName", username),   
                new Claim("Role", role)  
             };  

            var userIdentity = new ClaimsIdentity(userClaims, "User Identity");  

            var userPrincipal = new ClaimsPrincipal(new[] { userIdentity });  
               
            // attach account to context on successful jwt validation   
            //var user = new MVCWebApplication.Data.User();  
            //user.UserName = "aa@hotmail.com";  
            //context.Items["User"] = user;  

            context.SignInAsync(userPrincipal);  
        }  
        catch (Exception ex)  
        {  
            // do nothing if jwt validation fails  
            // account is not attached to context so request won't have access to secure routes  
            throw new Exception(ex.Message);  
        }  
    }  
}