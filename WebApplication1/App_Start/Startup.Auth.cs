using System;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Owin;
using WebApplication1.Models;
using System.Security.Claims;
using System.Threading.Tasks;
using Owin.Security.Providers.Twitch;

namespace WebApplication1
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit http://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                    // Enables the application to validate the security stamp when the user logs in.
                    // This is a security feature which is used when you change a password or add an external login to your account.  
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                        validateInterval: TimeSpan.FromMinutes(30),
                        regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                }
            });            
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));

            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "",
            //   appSecret: "");

            var googleOptions = new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = System.Configuration.ConfigurationManager.AppSettings["googleId"],
                ClientSecret = System.Configuration.ConfigurationManager.AppSettings["googleSecret"],
                Provider = new GoogleOAuth2AuthenticationProvider()
                {
                    OnAuthenticated = (context) =>
                    {
                        var pictureUrl = context.User["image"].Value<string>("url");
                        context.Identity.AddClaim(new Claim("googlePicture", pictureUrl));
                        context.Identity.AddClaim(new Claim("googleAccess", context.AccessToken));
                        return Task.FromResult(0);
                    }
                }
            };

            googleOptions.Scope.Add("openid");
            googleOptions.Scope.Add("profile");
            googleOptions.Scope.Add("email");

            googleOptions.Scope.Add("https://www.googleapis.com/auth/youtube.readonly");
            googleOptions.Scope.Add("https://www.googleapis.com/auth/youtube");
            googleOptions.Scope.Add("https://www.googleapis.com/auth/youtube.force-ssl");

            app.UseGoogleAuthentication(googleOptions);

            var options = new TwitchAuthenticationOptions
            {
                ClientId = System.Configuration.ConfigurationManager.AppSettings["twitchId"],
                ClientSecret = System.Configuration.ConfigurationManager.AppSettings["twitchSecret"],
                Provider = new TwitchAuthenticationProvider
                {
                    OnAuthenticated = (context) =>
                    {
                        context.Identity.AddClaim(new Claim("twitchAccess", context.AccessToken));
                        return Task.FromResult(0);
                    }
                }
            };
            app.UseTwitchAuthentication(options);

        }
    }
}