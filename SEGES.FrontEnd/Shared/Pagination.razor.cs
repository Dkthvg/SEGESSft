namespace SEGES.FrontEnd.Shared
{
    public partial class Pagination
    {
        private List<PageModel> links = null!;

        [Parameter] public int CurrentPage { get; set; } = 1;
        [Parameter] public int TotalPages { get; set; } = 1;
        [Parameter] public int Radio { get; set; } = 10;
        [Parameter] public EventCallback<int> SelectedPage { get; set; }

        protected override void OnParametersSet()
        {
            links = new List<PageModel>();

            links.Add(new PageModel
            {
                Text = "Anterior",
                Page = CurrentPage - 1,
                Enable = CurrentPage != 1
            });

            for (int i = 1; i < TotalPages; i++)
            {
                links.Add(new PageModel
                {
                    Text = $"{i}",
                    Page = i,
                    Active = i == CurrentPage
                });
            }

            links.Add(new PageModel
            {
                Text = "Siguiente",
                Page = CurrentPage != TotalPages ? CurrentPage + 1 : CurrentPage,
                Enable = CurrentPage != TotalPages
            });
        }

        private async Task InternalSelectedpage(PageModel pageModel)
        {
            if (pageModel.Page == CurrentPage || pageModel.Page == 0)
            {
                return;
            }
            await SelectedPage.InvokeAsync(pageModel.Page);
        }

        private class PageModel
        {
            public string Text { get; set; } = null!;
            public int Page { get; set; }
            public bool Enable { get; set; }
            public bool Active { get; set; }
        }
    }
}