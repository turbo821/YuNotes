using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using System.Text.RegularExpressions;
using YuNotes.Contracts;
using YuNotes.Models;
using YuNotes.Repositories.Interfaces;
using YuNotes.Services.Interfaces;
using YuNotes.ViewModels;

namespace YuNotes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        INotesService _service;

        public NotesController(INotesService service)
        {
            _service = service;
        }

        [HttpGet]
        [Route("notes")]
        public async Task<IActionResult> Catalog(CatalogRequest request)
        {
            var userEmail = Email;

            var viewModel = await _service.GetPaginationNotes(request, userEmail);

            return View(viewModel);
        }

        [HttpGet]
        [Route("note/{id:guid}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            var userEmail = Email;

            var viewModel = await _service.GetNote(id, userEmail);

            return View(viewModel);
        }

        [HttpPost]
        [Route("note/delete")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var userEmail = Email;

            await _service.DeleteNote(id, userEmail);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [Route("note/edit")]
        public async Task<IActionResult> EditNote(NoteViewModel model)
        {
            var userEmail = Email;
            Note note = model.Note;

            await _service.EditNote(note, userEmail);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [Route("note/addgroup")]
        public async Task<IActionResult> AddGroup(NoteGroup addedGroup)
        {
            if (addedGroup.Name is null || addedGroup.Name == string.Empty || Regex.IsMatch(addedGroup.Name, @"[^\w\s]"))
                return RedirectToAction("Catalog");
            
            var userEmail = Email;
            await _service.AddGroup(addedGroup, userEmail);
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        [Route("note/deletegroup")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            var userEmail = Email;
            await _service.DeleteGroup(id, userEmail);
            return RedirectToAction("Catalog");
        }

        private string Email => HttpContext.User.FindFirstValue(ClaimTypes.Email)!;
    }
}
