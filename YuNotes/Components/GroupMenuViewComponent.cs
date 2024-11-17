using Microsoft.AspNetCore.Mvc;
using YuNotes.ViewModels;

namespace YuNotes.Components
{
    public class GroupMenuViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CatalogViewModel model)
        {
            return View("GroupMenu", model);
        }
    }
}
