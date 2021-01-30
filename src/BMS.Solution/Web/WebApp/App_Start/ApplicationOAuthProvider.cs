using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.OAuth;
using NLog;
#nullable disable
namespace WebApp
{
  public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
  {
    private readonly string _publicClientId;

    public ApplicationOAuthProvider(string publicClientId)
    {
      if (publicClientId == null)
      {
        throw new ArgumentNullException("publicClientId");
      }

      this._publicClientId = publicClientId;
    }

    public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
    {
      context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
      var userManager = context.OwinContext.GetUserManager<ApplicationUserManager>();
      var user = await userManager.FindAsync(context.UserName, context.Password);
      if (user == null)
      {
        context.SetError("invalid_grant", "The user name or password is incorrect.");
        context.Rejected();
        return;
      }
      var oAuthIdentity = await user.GenerateUserIdentityAsync(userManager,
          OAuthDefaults.AuthenticationType);
      var cookiesIdentity = await user.GenerateUserIdentityAsync(userManager,
         CookieAuthenticationDefaults.AuthenticationType);
      context.Validated(oAuthIdentity);
      var properties = CreateProperties(user.UserName, user.Email, user.TenantId, user.GivenName, user.Avatars);
      var ticket = new AuthenticationTicket(oAuthIdentity, properties);
      context.Validated(ticket);
      context.Request.Context.Authentication.SignIn(cookiesIdentity);
      var logger = LogManager.GetCurrentClassLogger();
      logger.Info($"{context.UserName}:移动端登录成功");
    }

    public override Task TokenEndpoint(OAuthTokenEndpointContext context)
    {
      foreach (var property in context.Properties.Dictionary)
      {
        context.AdditionalResponseParameters.Add(property.Key, property.Value);
      }
      return Task.FromResult<object>(null);
    }

    public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
    {
      // Resource owner password credentials does not provide a client ID.
      // Enter the ClientID and ClientSecret separated by a colon (:) in “Encode to Base64 format” textbox, and then click on the “Encode” button as shown in the below diagram which will generate the Base64 encoded value.
      // Select the Header tab and provide the Authorization value as shown below.
      // Authorization: BASIC QzFBMDNCMTAtN0Q1OS00MDdBLUE5M0UtQjcxQUIxN0FEOEMyOjE3N0UzMjk1LTA2NTYtNDMxNy1CQzkxLUREMjcxQTE5QUNGRg==
      //if (context.TryGetBasicCredentials(out var clientId, out var clientSecret))
      //{
      //  // validate the client Id and secret against database or from configuration file.  
      //  context.Validated();
      //}
      //else
      //{
      //  context.SetError("invalid_client", "Client credentials could not be retrieved from the Authorization header");
      //  context.Rejected();
      //}
      if (context.ClientId == null)
      {
        context.Validated();
      }
      return Task.FromResult<object>(null);
    }

    public override Task ValidateClientRedirectUri(OAuthValidateClientRedirectUriContext context)
    {
      if (context.ClientId == this._publicClientId)
      {
        var expectedRootUri = new Uri(context.Request.Uri, "/");
        if (expectedRootUri.AbsoluteUri == context.RedirectUri)
        {
          context.Validated();
        }
      }

      return Task.FromResult<object>(null);
    }

    public static AuthenticationProperties CreateProperties(string userName, string email, int tenantId, string givenname, string avatars)
    {
      IDictionary<string, string> data = new Dictionary<string, string>
            {
                { "username", userName },
                { "email", email },
                { "givenname", givenname },
                { "avatars", avatars },
                { "tenantid", tenantId.ToString() }
            };
      return new AuthenticationProperties(data);
    }
  }
}