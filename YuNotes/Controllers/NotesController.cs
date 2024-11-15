﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using YuNotes.Contracts;
using YuNotes.Data;
using YuNotes.Models;
using YuNotes.Repositories;
using YuNotes.ViewModels;

namespace YuNotes.Controllers
{
    [Authorize]
    public class NotesController : Controller
    {
        INotesRepository repo;

        public NotesController(INotesRepository context)
        {
            repo = context;
        }


        [HttpGet]
        [Route("notes")]
        public async Task<IActionResult> Catalog(CatalogRequest request)
        {
            int pageSize = 4;
            var userContext = HttpContext.User;
            var userEmail = userContext.FindFirstValue(ClaimTypes.Email)!;
            User user = await repo.GetUser(userEmail);

            IEnumerable<Note> notesQuery = await repo.GetAllNotes(user, request.GroupId, request.SearchTitle);
            IEnumerable<NoteGroup> groups = await repo.GetAllGroups();

            notesQuery = request.SortOrder switch
            {
                SortState.TitleDesc => notesQuery.OrderByDescending(n => n.Title),
                SortState.EditAsc => notesQuery.OrderBy(s => s.EditDate),
                SortState.EditDesc => notesQuery.OrderByDescending(s => s.EditDate),
                SortState.CreateAsc => notesQuery.OrderBy(s => s.CreateDate),
                SortState.CreateDesc => notesQuery.OrderByDescending(s => s.CreateDate),
                _ => notesQuery.OrderBy(s => s.Title),
            };

            var count = notesQuery.Count();
            var notes = notesQuery.Skip((request.Page - 1) * pageSize).Take(pageSize);

            CatalogViewModel viewModel = new() { Notes = notes, 
                GroupModel = new() { NoteGroups = groups, GroupId = request.GroupId },
                SortViewModel = new SortViewModel(request.SortOrder),
                PageViewModel = new PageViewModel(count, request.Page, pageSize),
                SearchTitle = request.SearchTitle };

            return View(viewModel);
        }

        [HttpGet]
        [Route("note/{id:guid}")]
        public async Task<IActionResult> GetNote(Guid id)
        {
            var userContext = HttpContext.User;
            var userEmail = userContext.FindFirstValue(ClaimTypes.Email)!;
            User user = await repo.GetUser(userEmail);

            Note note = await repo.GetNote(id, user);
            IEnumerable<NoteGroup> groups = await repo.GetAllGroups();

            SelectList noteGroups = new SelectList(groups, "Id", "Name");

            return View(new NoteViewModel() { Note = note, NoteGroups = noteGroups });
        }

        [HttpPost]
        [Route("note/delete")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var userContext = HttpContext.User;
            var userEmail = userContext.FindFirstValue(ClaimTypes.Email)!;
            User user = await repo.GetUser(userEmail);

            await repo.DeleteNote(id, user);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [Route("note/edit")]
        public async Task<IActionResult> EditNote(NoteViewModel model)
        {
            var userContext = HttpContext.User;
            var userEmail = userContext.FindFirstValue(ClaimTypes.Email)!;
            User user = await repo.GetUser(userEmail);

            Note note = model.Note;
            await repo.EditNote(note, user);

            return RedirectToAction("Catalog");
        }

        [HttpPost]
        [Route("note/addgroup")]
        public async Task<IActionResult> AddGroup(NoteGroup addedGroup)
        {
            if(addedGroup.Name is null)
                RedirectToAction("Catalog");

            await repo.AddGroup(addedGroup);
            return RedirectToAction("Catalog");
        }

        [HttpGet]
        [Route("note/deletegroup")]
        public async Task<IActionResult> DeleteGroup(Guid id)
        {
            await repo.DeleteGroup(id);
            return RedirectToAction("Catalog");
        }
    }
}
