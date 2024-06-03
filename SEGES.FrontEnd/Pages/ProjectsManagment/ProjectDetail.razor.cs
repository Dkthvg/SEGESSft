using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;

namespace SEGES.FrontEnd.Pages.ProjectsManagment
{
    public partial class ProjectDetail
    {
        

        [CascadingParameter] private Task<AuthenticationState> AuthenticationStateTask { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        [Parameter] public int projectID { get; set; }

        private List<Requirement>? requirements { get; set; }
        private Project? project;

        protected override async Task OnInitializedAsync()
        {
            await LoadProjectDetail();
            await LoadRequirementsLis();
        }

        private async Task LoadRequirementsLis()
        {
            try
            {
                var responseHttp = await Repository.GetAsync<List<Requirement>>($"/api/Requirements/full");
                
                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
                
                requirements = responseHttp.Response;
                
                if (requirements == null)
                {
                    await SweetAlertService.FireAsync("Error", "Requerimientos no encontrados.", SweetAlertIcon.Error);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task LoadProjectDetail()
        {
            try
            {
                if (projectID <= 0)
                {
                    await SweetAlertService.FireAsync("Error", "ID de proyecto no válido.", SweetAlertIcon.Error);
                    return;
                }
                var responseHttp = await Repository.GetAsync<Project>($"/api/project/{projectID}");
                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }
                project = responseHttp.Response;
                if (project == null)
                {
                    await SweetAlertService.FireAsync("Error", "Proyecto no encontrado.", SweetAlertIcon.Error);
                }
            }
            catch (Exception ex)
            {
                await SweetAlertService.FireAsync("Error", $"Ocurrió un error: {ex.Message}", SweetAlertIcon.Error);
            }
        }

        private void GoTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}
