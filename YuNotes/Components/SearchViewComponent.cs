using Microsoft.AspNetCore.Mvc;
using YuNotes.Models;
using YuNotes.ViewModels;

namespace YuNotes.Components
{
    public class SearchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CatalogViewModel model)
        {
            return View("Search", model);
        }
    }
}
