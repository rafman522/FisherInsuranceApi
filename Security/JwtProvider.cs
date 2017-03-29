using FisherInsuranceApi.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace FisherInsuranceApi.Security
{
    public class JwtProvider
    {
        private readonly RequestDelegate _next;
        private TimeSpan TokenExpiration;
        private SigningCredentials SigningCredentials;
        private FisherContext db;
        private UserManager<ApplicationUser> UserManager;
        private SignInManager<ApplicationUser> SignInManager;
        private static readonly string PrivateKey = "private_key_1234567890";
        public static readonly SymmetricSecurityKey SecurityKey = 
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(PrivateKey));
        public static readonly string Issuer = "FisherInsurance";
        public static string TokenEndPoint = "/api/connect/token";

        public JwtProvider(RequestDelegate next, FisherContext db, UserManager<ApplicationUser>
                userManager, SignInManager<ApplicationUser> signInManager)
        {
            _next = next;

            this.db = db;
            UserManager = userManager;
            SignInManager = signInManager;

            //Configure JWT Token Settings
            TokenExpiration = TimeSpan.FromMinutes(10);
            SigningCredentials = new SigningCredentials(SecurityKey, SecurityAlgorithms.HmacSha256);

        }
        
        public Task Invoke(HttpContext httpContext)
        {
            if (!httpContext.Request.Path.Equals(TokenEndPoint, StringComparison.Ordinal))
            {
                return _next(httpContext);
            }
            if (!httpContext.Request.Method.Equals("POST") && httpContext.Request.HasFormContentType)
            {
                return CreateToken(httpContext);
            }
            else 
            {
                httpContext.Response.StatusCode = 400;
                return httpContext.Response.WriteAsync("Bad Request.");
            }
        }

    
    
        private async Task CreateToken(HttpContext httpContext){
            try
            {
                string username = httpContext.Request.Form["username"];
                string password = httpContext.Request.Form["password"];

                var user = await UserManager.FindByNameAsync(username);

                if (user == null && username.Contains("@"))
                {
                    user = await UserManager.FindByEmailAsync(username);
                }

                var success = user != null && await UserManager.CheckPasswordAsync(user, password);

                if (success)
                {
                    DateTime now = DateTime.UtcNow;

                    var claims = new[]
                    {
                        new Claim(JwtRegisteredClaimNames.Iss, Issuer),
                        new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, new DateTimeOffset(now).ToUnixTimeSeconds().ToString(),
                        ClaimValueTypes.Integer64)

                    };
                    var token = new JwtSecurityToken(
                        claims: claims,
                        notBefore: now,
                        expires: now.Add(TokenExpiration),
                        signingCredentials: SigningCredentials);

                    var encodedToken = new JwtSecurityTokenHandler().WriteToken(token);

                    var jwt = new
                    {
                        access_token = encodedToken,
                        expiration = (int)TokenExpiration.TotalSeconds
                    };

                    httpContext.Response.ContentType = "application/json";

                    await
                    httpContext.Response.WriteAsync(JsonConvert.SerializeObject(jwt));

                    return;
                }
            }
            catch (Exception)
            {
            
            throw;
            }
            httpContext.Response.StatusCode = 400;
            await httpContext.Response.WriteAsync("Invalid username or password.");
        }
    }
    public static class JwtProviderExtensions
    {
        public static IApplicationBuilder UseJwtProvider(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<JwtProvider>();
        }
    }
}