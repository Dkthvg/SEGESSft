using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;
using System.Net;
using System.Text.Json;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace SEGES.FrontEnd.Pages.ProjectsManagment
{
    public partial class MyProjectsRE
    {
        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;
        


        public  List<Project>? projects;
        public List<Project>? projectsAssigned;
        public List<ProjectCount> dataChart;


        protected override async Task OnInitializedAsync()
        {
            await LoadProjectsAsync();
            dataChart = await GetCountByStatus(projectsAssigned);

        }

        private async Task LoadProjectsAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Project>>("/api/Project/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            projects = responseHttp.Response;
            if (projects != null)
            projectsAssigned = projects.Where(project => project.RequirementsEngineer_ID == "b34efb32-d0f0-4590-8583-eaf820d61953").ToList();
        }

        private async Task<List<ProjectCount>> GetCountByStatus(List<Project> myProjects)
        {
            var groupedByStatus = myProjects.GroupBy(myProject => myProject.ProjectStatus_ID);
            var count = groupedByStatus.Select(group => new ProjectCount
            {
                //NombreEstado = await Repository.GetAsync($"/api/ProjectStatuses/{group.Key}").Result.,
                Estado = group.Key, // El estado del grupo
                Cantidad = group.Count() // La cantidad de proyectos en el grupo
            }).ToList();
            return count;
        }

        private void GoTo(int project_ID)
        {
            NavigationManager.NavigateTo($"/projectDetail/{project_ID}");
        }

    }
    public class ProjectCount
    {
        //public string NombreEstado {get; set; }
        public int? Estado { get; set; }
        public int Cantidad { get; set; }
    }
}
