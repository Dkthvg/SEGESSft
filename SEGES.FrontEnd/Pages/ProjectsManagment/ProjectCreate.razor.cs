using Blazored.Modal.Services;
using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;


namespace SEGES.FrontEnd.Pages.ProjectsManagment
{
    public partial class ProjectCreate
    {
        private Project project = new();

        private DateTime _minDate = DateTime.Now.AddDays(-1);
        [Parameter] public int StatusId { get; set; }
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private IRepository Repository { get; set; } = null!;
        private List<ProjectStatus>? statuses;

        protected override async Task OnInitializedAsync()
        {
            await LoadProjectStatus();
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

        private async Task SaveDataSync()
        {
            
            

            if (project == null)
            {
                await SweetAlertService.FireAsync("Error", "Project data is null", SweetAlertIcon.Error);
                return;
            }

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
