using Blazored.Modal;
using CurrieTechnologies.Razor.SweetAlert2;
using Microsoft.AspNetCore.Components;
using SEGES.FrontEnd.Repositories;
using SEGES.Shared.Entities;
using SEGES.Shared.Enums;
using System.Net;

namespace SEGES.FrontEnd.Pages.ProjectsManagment
{
    public partial class ProjectsList
    {

        [Inject] private IRepository Repository { get; set; } = null!;
        [Inject] private SweetAlertService SweetAlertService { get; set; } = null!;
        [Inject] private NavigationManager NavigationManager { get; set; } = null!;
        [CascadingParameter] BlazoredModalInstance BlazoredModal { get; set; } = default!;

        private List<Project>? projects = new();
        private List<UserApp>? users;
        private List<ProjectInfo>? projectsInfo;
        private ProjectStatus? projectStatus;

        private string? statusName;


        protected override async Task OnInitializedAsync()
        {
            await LoadAsync();
            await LoadUsers();

            var combinedList = await CombineData();
            projectsInfo = combinedList;

        }

        

        private async Task LoadUsers()
        {
            var responseHttp = await Repository.GetAsync<List<UserApp>>("/api/users/full");
            if (responseHttp.Error)
            {
                var message = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", message, SweetAlertIcon.Error);
                return;
            }
            users = responseHttp.Response;

            foreach (var item in users)
            {
                await Console.Out.WriteLineAsync(item.FullName);
            }
        }

        private async Task LoadAsync()
        {
            var responseHttp = await Repository.GetAsync<List<Project>>($"/api/project/full");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    NavigationManager.NavigateTo("/");
                    return;
                }
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
                return;
            }
            projects = responseHttp.Response;

        }

        private async Task<ProjectStatus?> LoadProjectStatusAsync(int id)
        {
            var responseHttp = await Repository.GetAsync<ProjectStatus>($"api/ProjectStatuses/{id}");
            if (responseHttp.Error)
            {
                if (responseHttp.HttpResponseMessage.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                var messageError = await responseHttp.GetErrorMessageAsync();
                await SweetAlertService.FireAsync("Error", messageError, SweetAlertIcon.Error);
            }
            return responseHttp.Response;
        }

        async Task<List<ProjectInfo>> CombineData()
        {
           var combinedData = new List<ProjectInfo>();
            foreach (var project in projects)
            {
                var stackeHolderName = "";
                var projectManagerName = "";
                var reqEngName = "";
                ProjectStatus? projectStatus = await LoadProjectStatusAsync(project.ProjectStatus_ID.Value);


                if (users !=null)
                {
                     stackeHolderName = users.FirstOrDefault(user => user.Id == project.StakeHolder_ID)?.FullName;
                }
         
                if (users != null)
                {
                    projectManagerName = users.FirstOrDefault(user => user.Id == project.ProjectManager_ID)?.FullName;
                }
                
                if(users != null)
                {
                    reqEngName = users.FirstOrDefault(user => user.Id == project.RequirementsEngineer_ID)?.FullName;
                }

                combinedData.Add(new ProjectInfo
                {
                    projectId = project.ProjectId,
                    ProjectName = project.ProjectName,
                    ProjectDescription = project.ProjectDescription,
                    CreationDate = project.CreationDate,
                    ProjectStartDate = project.ProjectStartDate,
                    ProjectEndDate = project.ProjectEndDate,
                    StakeHolderName = stackeHolderName,
                    ProjectManagerName = projectManagerName,
                    ReqEngName = reqEngName,
                    ProjectStatusName = projectStatus?.Name ?? "Unknown"
                }); 
            }
            return combinedData;

        }


        public class ProjectInfo
        {
            public int?  projectId {  get; set; }
            public string? ProjectName { get; set; }
            public string? ProjectDescription { get; set; }
            public DateTime CreationDate { get; set; }
            public DateTime? ProjectStartDate { get; set; }
            public DateTime? ProjectEndDate { get; set; }
            public string? StakeHolderName { get; set; }
            public string? ProjectManagerName { get; set; }
            public string? ReqEngName { get; set; }
            public string? ProjectStatusName { get; set; } 
        }

            



    }
}
