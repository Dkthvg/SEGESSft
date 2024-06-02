using Microsoft.AspNetCore.Components;

namespace SEGES.FrontEnd.Pages.RequirementsManagment
{
    public partial class CreateRequirement
    {

        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [Parameter] public int projectID { get; set; }

        private void GoTo(string path)
        {
            NavigationManager.NavigateTo(path);
        }
    }
}
