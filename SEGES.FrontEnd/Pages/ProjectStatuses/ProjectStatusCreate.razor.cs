using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;
using SEGES.FrontEnd.Shared;
using Blazored.Modal;
using Blazored.Modal.Services;


namespace SEGES.FrontEnd.Pages.ProjectStatuses
{
    public partial class ProjectStatusCreate
    {
        private ProjectStatus status = new();
        private FormWithName<ProjectStatus>? projectStatusForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/projectstatuses", status);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
                return;
            }
            await BlazoredModal.CloseAsync(ModalResult.Ok());
            Return();

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito");
        }

        private void Return()
        {
            projectStatusForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/ProjectStatuses");
        }
    }
}