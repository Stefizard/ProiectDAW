using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProiectDAW.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace ProiectDAW.Utility
{
    public class JWTMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly AppSettings _appSettings;

        public JWTMiddleware(IOptions<AppSettings> appSettings, RequestDelegate next)
        {
            _appSettings = appSettings.Value;
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext, IUserRepository userRepository, IJWTUtils jwtUtils)
        {
            var token = httpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            var userId = jwtUtils.ValidateToken(token);

            if (userId != Guid.Empty)
            {
                httpContext.Items["User"] = userRepository.Get(userId);
            }

            await _next(httpContext);
        }
    }
}
