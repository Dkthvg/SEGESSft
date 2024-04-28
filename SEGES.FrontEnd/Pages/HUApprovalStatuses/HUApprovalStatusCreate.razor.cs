using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.FrontEnd.Shared;
using SEGES.Shared.Entities;

namespace SEGES.FrontEnd.Pages.HUApprovalStatuses
{
    public partial class HUApprovalStatusCreate
    {
        private HUApprovalStatus approvalStatus = new();
        private FormWithName<HUApprovalStatus>? approvalStatusForm;

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;

        private async Task CreateAsync()
        {
            var responseHttp = await Repository.PostAsync("/api/HUApprovalStatus", approvalStatus);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message);
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
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Registro creado con éxito");
        }




        private void Return()
        {
            approvalStatusForm!.FormPostedSuccessfully = true;
            NavigationManager.NavigateTo("/HUApprovalStatus");
        }
    }
}
