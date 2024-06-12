using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;

namespace SEGES.FrontEnd.Pages.LearnMoreSection
{
    public partial class LearnMorePage
    {
        [Parameter] public Guid sessionId { get; set; }
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] private BlazoredModalInstance BlazoredModal { get; set; } = default!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;

        public List<LearnMoreComments>? Comments { get; set; }
        public LearnMoreComments? Comment = new();

        protected override async Task OnInitializedAsync()
        {
            await LoadComments();
        }

        private async Task LoadComments()
        {
            try
            {
                var responseHttp = await Repository.GetAsync<List<LearnMoreComments>>($"/api/LearnMoreComments/full");

                if (responseHttp.Error)
                {
                    var message = await responseHttp.GetErrorMessageAsync();
                    await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                    return;
                }

                Comments = responseHttp.Response;

                if (Comments == null)
                {
                    await SweetAlertService.FireAsync("Error", "Comentarios no encontrados.", SweetAlertIcon.Error);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task SaveComment()
        {
            if (Comment == null)
            {
                await SweetAlertService.FireAsync("Error", "Project data is null", SweetAlertIcon.Error);
                return;
            }
            Comment.CreatedBy = sessionId;
            var responseHttp = await Repository.PostAsync("/api/LearnMoreComments", Comment);
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }

            GoTo($"/learnMore/{sessionId}");

            var toast = SweetAlertService.Mixin(new SweetAlertOptions
            {
                Toast = true,
                Position = SweetAlertPosition.BottomEnd,
                ShowConfirmButton = true,
                Timer = 3000
            });
            await toast.FireAsync(icon: SweetAlertIcon.Success, message: "Comentario creado con éxito");
            await LoadComments();
            Comment = new LearnMoreComments();
            await Task.Delay(2000);
            StateHasChanged();
        }

        private void GoTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}