using Blazored.Modal;
using Blazored.Modal.Services;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.FrontEnd.Shared;
using SEGES.Shared.Entities;

namespace SEGES.FrontEnd.Pages.DocTraceabilityTypes
{
    //   [Authorize(Roles = "Admin")]
    public partial class DocTraceabilityTypeCreate
    {
        private DocTraceabilityType traceabilityType = new();
        private FormWithName<DocTraceabilityType>? docTraceabilityTypeForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/DocTraceabilityType", traceabilityType);
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
            docTraceabilityTypeForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/DocTraceabilityType");
        }
    }
}
