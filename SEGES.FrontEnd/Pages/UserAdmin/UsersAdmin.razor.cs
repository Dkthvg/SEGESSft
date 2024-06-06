using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;
using SEGES.Shared.Enums;
using System.Net;

namespace SEGES.FrontEnd.Pages.UserAdmin
{
    public partial class UsersAdmin
    {
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private DialogService DialogService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter] public bool WithColumnPicker { get; set; } = true;
        private int currentPage = 1;
        private int totalPages;

        public List<UserApp>? users;

        protected override async Task OnInitializedAsync()
        {
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            var responseHttp = await Repository.GetAsync<List<UserApp>>("/api/users/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            users = responseHttp.Response;

            foreach (var item in users)
            {
                await Console.Out.WriteLineAsync(item.FullName);
            }
        }

        private async Task UserEditAsync(string id)
        {
            await DialogService.OpenAsync<UserEdit>
                (
                    $"useredit/{id}",
                    new Dictionary<string, object>() { { "UserEdit", id } },
                    new DialogOptions() { Width = "700px", Height = "520px" }
                );
        }

        public async Task DeleteAsync(UserApp userId)
        {
            var result = await SweetAlertService.FireAsync(new SweetAlertOptions
            {
                Title = "Confirmación",
                Text = $"¿Estas seguro de borrar el usuario: {userId.FullName}?",
                Icon = SweetAlertIcon.Question,
                ShowCancelButton = true,
            });
            var confirm = string.IsNullOrEmpty(result.Value);
            if (confirm)
            {
                return;
            }

            var responseHttp = await Repository.DeleteAsync<UserApp>($"/api/users/{userId.Email}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/userapp");
                }
                else
                {
                    var mensajeError = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", mensajeError, SweetAlertIcon.Error);
                }
                return;
            }

            await LoadUsers();
            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Usuario borrado con éxito.");
        }

        private async void Goto(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}