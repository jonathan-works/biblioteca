using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Biblioteca.Infra;
using Biblioteca.Models;
using Biblioteca.Helpers;

namespace Biblioteca.Controllers
{
    public class LivrosController : Controller
    {
        private BibliotecaDB db = new BibliotecaDB();

       
        public ActionResult Index()
        {
            return View(db.Livros.ToList());
        }

       
        public ActionResult Details()
        {
            List<Livro> livro = db.Livros.ToList();           
            return View(livro);
        }

       
        
       
        public ActionResult Create()
        {
            @ViewBag.Autores = RetornaSelectListItem.Autores();
            @ViewBag.Categorias = RetornaSelectListItem.Categorias();
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,nome,descricao,totalPaginas,quantidade,categoriaId,autorId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Livros.Add(livro);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            @ViewBag.Categorias = RetornaSelectListItem.Categorias();
            return View(livro);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = db.Livros.Find(id);
            if (livro == null)
            {
                return HttpNotFound();
            }
            @ViewBag.Autores = RetornaSelectListItem.Autores(livro.autorId);
            @ViewBag.Categorias = RetornaSelectListItem.Categorias(livro.categoriaId);
            return View(livro);
        }
               
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,nome,totalPaginas,descricao,quantidade,categoriaId,autorId")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                db.Entry(livro).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            @ViewBag.Autores = RetornaSelectListItem.Autores(livro.autorId);
            @ViewBag.Categorias = RetornaSelectListItem.Categorias(livro.categoriaId);
            return View(livro);
        }

        
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Livro livro = new Livro();
            if (livro.ExcluirLivro(id.Value))
            {
                return Content("Livro Excluido com sucesso.");
            }
            return Content("Não foi possivel excluir o livro.");

            
        }
       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
