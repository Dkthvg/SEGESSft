using Microsoft.AspNetCore.Components;

namespace SEGES.FrontEnd.Pages
{
    public partial class Index
    {
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public string sessionId { get; set; }

        protected override async Task OnInitializedAsync()
        {
            sessionId = await SessionService.GetOrCreateSessionIdAsync();
            Console.WriteLine($"Session ID: {sessionId}");
        }

        private void GoTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}