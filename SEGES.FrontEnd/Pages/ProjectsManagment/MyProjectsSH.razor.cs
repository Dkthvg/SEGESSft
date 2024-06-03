using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;

namespace SEGES.FrontEnd.Pages.ProjectsManagment
{
    public partial class MyProjectsSH
    {
        private string? CurrentUserEmail { get; set; }
        private string? CurrentUserName { get; set; }
        private string? CurrentUserId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;

        public List<Project>? projects;
        public List<Project>? projectsAssignedToSH;
        public List<ProjectCount> dataChart;

        protected override async Task OnInitializedAsync()
        {
            var authstate = await AuthenticationStateTask;
            var user = authstate.User;
            if (user.Identity.IsAuthenticated)
            {
                Console.WriteLine($"{user.Identity.Name} is authenticated.");
            }
            CurrentUserEmail = user.Identity.Name;
            var currentUserData = await Repository.GetAsync<UserApp>($"/api/Users/email?email={CurrentUserEmail}");
            if (currentUserData.Error)
            {
                var message = await currentUserData.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            CurrentUserName = currentUserData.Response.FirstName + " " + currentUserData.Response.LastName;
            CurrentUserId = currentUserData.Response.Id;
            await Console.Out.WriteLineAsync(CurrentUserId);

            await LoadProjectsAsync();
            dataChart = await GetCountByStatus(projectsAssignedToSH);

        }

        private async Task LoadProjectsAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Project>>("/api/Project/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            projects = responseHttp.Response;
            if (projects != null)
                projectsAssignedToSH = projects.Where(project => project.StakeHolder_ID == CurrentUserId).ToList();
        }

        private async Task<List<ProjectCount>> GetCountByStatus(List<Project> myProjects)
        {
            var groupedByStatus = myProjects.GroupBy(myProject => myProject.ProjectStatus_ID);
            var count = groupedByStatus.Select(group => new ProjectCount
            {
                //NombreEstado = await Repository.GetAsync($"/api/ProjectStatuses/{group.Key}").Result.,
                Estado = group.Key, // El estado del grupo
                Cantidad = group.Count() // La cantidad de proyectos en el grupo
            }).ToList();
            return count;
        }
        private void GoTo(int project_ID)
        {
            NavigationManager.NavigateTo($"/projectDetail/{project_ID}");
        }
    }
}
