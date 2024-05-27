using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using Microsoft.AspNet.Identity;


namespace SEGES.FrontEnd.Pages.ProjectsManagment
{
    public partial class ProjectCreate
    {
        private Project project = new();

        private DateTime _minDate = DateTime.Now.AddDays(-1);
        [Parameter] public int StatusId { get; set; }
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private AuthenticationStateProvider? GetAuthenticationStateAsync { get; set; }

        [Inject] private IRepository Repository { get; set; } = null!;
        
        private List<ProjectStatus>? statuses;
        private List<UserApp>? users;

        private string? CurrentUserEmail { get; set; }
        private string CurrentUserName { get; set; }
        private string CurrentUserId{ get; set; }

        protected override async Task OnInitializedAsync()
        {
            await LoadProjectStatus();
            await LoadUsersAsync();
            var authstate = await AuthenticationStateTask;
            var user = authstate.User;
            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine($"{user.Identity.Name} is authenticated.");
            }
            CurrentUserEmail=user.Identity.Name;
            var currentUserData = await Repository.GetAsync<UserApp>($"/api/Users/email?email={CurrentUserEmail}");
            if (currentUserData.Error)
            {
                var message = await currentUserData.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            CurrentUserName = currentUserData.Response.FirstName + " " +currentUserData.Response.LastName;
            CurrentUserId = currentUserData.Response.Id;
            await Console.Out.WriteLineAsync(CurrentUserId);

        }

        private async Task LoadProjectStatus()
        {
            var responseHttp = await Repository.GetAsync<List<ProjectStatus>>("/api/projectstatuses/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            statuses = responseHttp.Response;
        }

        private async Task LoadUsersAsync()
        {
            var responseHttp = await Repository.GetAsync<List<UserApp>>("/api/Users/combo");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            users = responseHttp.Response;
        }

        private async Task SaveDataSync()
        {
            if (project == null)
            {
                await SweetAlertService.FireAsync("Error", "Project data is null", SweetAlertIcon.Error);
                return;
            }
            project.ProjectManager_ID = CurrentUserId;
            var responseHttp = await Repository.PostAsync("/api/project", project);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            Return();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Proyecto creado con éxito");
        }

        private void Return()
        {
            NavigationManager.NavigateTo("/");
        }
    }
}