using Microsoft.AspNetCore.Components.Authorization;
using SEGES.Shared.Entities;
using System.Security.Claims;

namespace SEGES.FrontEnd.AuthenticationProviders
{
    public class AuthenticationProviderTest : AuthenticationStateProvider
    {
        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var anonimous = new ClaimsIdentity();
            var user = new ClaimsIdentity(authenticationType: "test");
            var admin = new ClaimsIdentity(new List<Claim>
            {
                new Claim("FirstName", "admin"),
                new Claim("LastName", "prueba"),
                new Claim(ClaimTypes.Name, "admin@yopmail.com"),
                new Claim(ClaimTypes.Role, "Admin")
            },
            authenticationType: "test");

            return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(user)));

            /*  prueba para ver si autoriza 
             *  await Task.Delay(3000);
               var anonimous = new ClaimsIdentity();
               return await Task.FromResult(new AuthenticationState(new ClaimsPrincipal(anonimous)));*/
        }
    }
}
