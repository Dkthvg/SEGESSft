using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Radzen;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;
using SEGES.Shared.Enums;

namespace SEGES.FrontEnd.Pages.UserAdmin
{
    public partial class UsersAdmin
    {
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter]  private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private DialogService DialogService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter] public bool WithColumnPicker { get; set; } = true;

        private List<UserApp>? users;


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

        async Task UserEditAsync(string id)
        {
            
            await DialogService.OpenAsync<UserEdit>
                (
                    $"useredit/{id}",
                    new Dictionary<string, object>() { { "UserEdit", id } },
                    new DialogOptions() { Width = "700px", Height = "520px" }
                );
            
        }

        async void Goto(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}
