using Microsoft.AspNetCore.Mvc;
using YuNotes.ViewModels;

namespace YuNotes.Components
{
    public class SortMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CatalogViewModel model)
        {
            return View("SortMenu", model);
        }
    }
}
