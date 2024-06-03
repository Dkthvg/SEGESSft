using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;

namespace SEGES.FrontEnd.Pages.GoalManagment
{
    public partial class GoalCreate
    {
        private Goal Goal = new();
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter] public int projectId { get; set; }
       
        public Project project = new Project();

        protected override async Task OnInitializedAsync()
        {
            var httpResponse = await Repository.GetAsync<Project>($"/api/project/{projectId}");
            project = httpResponse.Response;

        }
        private async Task CreateNewGoal()
        {
            if (project == null)
            {
                await SweetAlertService.FireAsync("Error", "Project data is null", SweetAlertIcon.Error);
                return;
            }
            Goal.Project_ID = projectId;
            //Goal.CreationDate = DateTime.Now;
            //var responseHttpProject = await Repository.GetAsync<Project>($"/api/Project/{projectId}");
            //Goal.Project = responseHttpProject.Response;
            var responseHttp = await Repository.PostAsync("/api/Goals", Goal);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            GoTo($"/projectDetail/{projectId}");

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Objetivo creado con éxito");
        }
        private void GoTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}
