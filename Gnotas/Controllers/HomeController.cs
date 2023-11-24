using Gnotas.Data;
using Gnotas.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using System.Diagnostics;

namespace Gnotas.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly Context db;

        public HomeController(Context db)
        {
            this.db = db;
        }
        
        public async Task<IActionResult> Index()
        {
            return View(await db.Notas.OrderBy(x => x.Descricao).AsNoTracking().ToListAsync());
        }

        public ActionResult Adicionar()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Adicionar(NotaModel nota)
        {
            try
            {
                db.Notas.Add(nota);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        /* Ver nota */

        public async Task<IActionResult> VerNota(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(nameof(Index));
            }

            var nota = await db.Notas.FindAsync(id);
            if (nota == null) 
            {
                return RedirectToAction(nameof(Index));
            }
            return View(nota);
        }

        //Editar nota
        public IActionResult Editar(int id) 
        {
            return View(db.Notas.Where(a => a.IdNota == id).FirstOrDefault());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Editar(int id, NotaModel nota)
        {
            try
            {
                var notaModel = db.Notas.Find(id);
                if (notaModel == null)
                {
                    //Caso a nota não seja encontrada
                }
                //Atualizar 
                notaModel.Titulo = nota.Titulo;
                notaModel.Descricao = nota.Descricao;

                //Salva
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        //apagar nota
        public ActionResult Apagar(int id)
        {
            db.Notas.Remove(db.Notas.Where(a => a.IdNota == id).FirstOrDefault());
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
