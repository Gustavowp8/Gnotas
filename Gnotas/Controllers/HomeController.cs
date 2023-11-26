using Gnotas.Data;
using Gnotas.Models;
using Microsoft.AspNet.Identity;
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

        public IActionResult Index()
        {
            string usuario = User.Identity.GetUserId();
            var notas = db.Notas.Where(n => n.ChaveUsuario == usuario).OrderBy(n => n.Descricao).ToList();
            return View(notas);
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
                nota.ChaveUsuario = User.Identity.GetUserId();
                db.Notas.Add(nota);
                db.SaveChanges();
                TempData["mensagem"] = MensagemModel.Serializar("A nota foi adicionada");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["mensagem"] = MensagemModel.Serializar("Erro ao excluir a nota", TipoMensagem.Erro);
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
                TempData["mensagem"] = MensagemModel.Serializar("A nota foi alterada");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                TempData["mensagem"] = MensagemModel.Serializar("Erro ao tentar alterar a nota", TipoMensagem.Erro);
                return RedirectToAction(nameof(Index));
            }
        }

        //apagar nota
        public ActionResult Apagar(int id)
        {
            db.Notas.Remove(db.Notas.Where(a => a.IdNota == id).FirstOrDefault());
            db.SaveChanges();
            TempData["mensagem"] = MensagemModel.Serializar("A nota foi apagada", TipoMensagem.Erro);
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}
