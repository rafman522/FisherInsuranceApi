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

        public JwtProvider(RequestDelegate next)
        {
            _next = next;
        }
        
        public Task Invoke(HttpContext httpContext)
        {
            return _next(httpContext);
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