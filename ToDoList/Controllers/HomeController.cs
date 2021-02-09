using ToDoList.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToDoList.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationContext db;
        public HomeController(ApplicationContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await db.Affairs.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Affair affair)
        {
            db.Affairs.Add(affair);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        public async Task<IActionResult> Edit(int? id)
        {
            if (id != null)
            {
                Affair affair = await db.Affairs.FirstOrDefaultAsync(p => p.Id == id);
                if (affair != null)
                    return View(affair);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Affair affair)
        {
            db.Affairs.Update(affair);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(int? id)
        {
            if (id != null)
            {
                Affair affair = await db.Affairs.FirstOrDefaultAsync(p => p.Id == id);
                if (affair != null)
                    return View(affair);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                Affair affair = await db.Affairs.FirstOrDefaultAsync(p => p.Id == id);
                if (affair != null)
                {
                    db.Affairs.Remove(affair);
                    await db.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }



    }
}
