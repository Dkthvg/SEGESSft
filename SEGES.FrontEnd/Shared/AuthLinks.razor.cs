using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Blazored.Modal.Services;
using SEGES.FrontEnd.Pages.Auth;


namespace SEGES.FrontEnd.Shared
{
    public partial class AuthLinks
    {
        private string? photoUser;
        private string? userName;

        [CascadingParameter]
        private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [CascadingParameter] IModalService Modal {  get; set; } = default!;


        protected override async Task OnParametersSetAsync()
        {
            var authenticationState = await AuthenticationStateTask;
            var user = authenticationState.User;

            if (user.Identity != null && user.Identity.IsAuthenticated)
            {
                userName = user.Identity.Name;

                var claims = user.Claims.ToList();
                var photoClaim = claims.FirstOrDefault(x => x.Type == "Photo");
                if (photoClaim is not null)
                {
                    photoUser = photoClaim.Value;
                }
            }
        }
        private void ShowModal()
        {
            var options = new Blazored.Modal.ModalOptions
            {
                Size=Blazored.Modal.ModalSize.Medium,
                HideCloseButton = false
            };

            Modal.Show<Login>("",options);
            
        }
    }
}
