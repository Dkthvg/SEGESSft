using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Radzen;
using SEGES.FrontEnd.Repositories;
using SEGES.FrontEnd.Services;
using SEGES.Shared.DTOs;

namespace SEGES.FrontEnd.Pages.Auth
{
    public partial class Login
    {
        private LoginDTO loginDTO = new();
        private bool wasClose;

        

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private ILoginService LoginService { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CloseModalAsync()
        {
            wasClose = true;
            await BlazoredModal.CloseAsync(ModalResult.Ok());
        }
        private async Task LoginAsync(LoginArgs args)
        {
            await Console.Out.WriteLineAsync("se llamó el loginasync");
            
            loginDTO.Email = args.Username;
            loginDTO.Password = args.Password;
            await Console.Out.WriteLineAsync("user " + loginDTO.Email);
            if (wasClose)
            {
                NavigationManager.NavigateTo("/");
                return;
            }
            var responseHttp = await Repository.PostAsync<LoginDTO, TokenDTO>("/api/accounts/Login", loginDTO);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            await LoginService.LoginAsync(responseHttp.Response!.Token);
            NavigationManager.NavigateTo("/");
        }

        private void RecoverPassword()
        {
            NavigationManager.NavigateTo("/RecoverPassword");
        }

        private void Register()
        {
            NavigationManager.NavigateTo("/register");
        }
    }
}