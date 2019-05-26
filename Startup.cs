using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;

[assembly: OwinStartup(typeof(MyProject1.Startup))]

//for configuring oauthauthorizationserver
namespace MyProject1
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {

            // enable cors origin requests
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);

            var myProvider = new MyAuthAuthorizationServerProvider();
            OAuthAuthorizationServerOptions options =new OAuthAuthorizationServerOptions
            {
                AllowInsecureHttp=true,
                TokenEndpointPath=new PathString("/token"),
                AccessTokenExpireTimeSpan=TimeSpan.FromDays(1),
                Provider=myProvider

            };
            app.UseOAuthAuthorizationServer(options);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());

            //register web api config
            HttpConfiguration config = new HttpConfiguration();
            WebApiConfig.Register(config);
        }
    }
}
