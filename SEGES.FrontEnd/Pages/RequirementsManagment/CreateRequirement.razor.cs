using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

using SEGES.FrontEnd.Repositories;
using SEGES.Shared.DTOs;
using SEGES.Shared.Entities;
using System.Text;
using System.Text.Json;

namespace SEGES.FrontEnd.Pages.RequirementsManagment
{
    public partial class CreateRequirement
    {

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private AuthenticationStateProvider? GetAuthenticationStateAsync { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter] public int projectID { get; set; }
        public Project project = new Project();

        private Requirement requirement = new();
        
        public RequirementDTO requirementDTO = new();

        protected override async Task OnInitializedAsync()
        {
            var httpResponse = await Repository.GetAsync<Project>($"/api/project/{projectID}");
            project = httpResponse.Response;

        }
        private async Task CreateNewRequirement()
        {
            if (requirement == null)
            {
                await SweetAlertService.FireAsync("Error", "Project data is null", SweetAlertIcon.Error);
                return;
            }
            requirement.Project_ID = projectID;
            
            
            var responseHttp = await Repository.PostAsync("/api/Requirements", requirement);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            GoTo($"/projectDetail/{projectID}");

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Requerimiento creado con éxito");
        }
        private void GoTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}
