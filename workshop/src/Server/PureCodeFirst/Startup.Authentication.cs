using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Chat.Server
{
    public partial class Startup
    {
        internal static byte[] SharedSecret = Encoding.ASCII.GetBytes(
            "abcdefghijklmnopqrstuvwxyz1234567890");

        private void ConfigureAuthenticationServices(IServiceCollection services)
        {
            // configure jwt authentication
            services
                .AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(SharedSecret),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                    x.Events = new JwtBearerEvents
                    {
                        OnMessageReceived = context =>
                        {
                            if (context.HttpContext.Request.Query.ContainsKey("token"))
                            {
                                context.Token = context.HttpContext.Request.Query["token"];
                            }                            
                            return Task.CompletedTask;
                        }
                    };
                });
        }
    }
}
