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
    public class EmprestimosController : Controller
    {
        private BibliotecaDB db = new BibliotecaDB();

     public ActionResult Index()
     {
         return View();
      }

        public ActionResult Details()
        {
            List<Emprestimo> lEmprestimoNaoDevolvido = db.Emprestimos.Where(_ => _.FoiDevolvido.Equals(false)).ToList();

            return View(lEmprestimoNaoDevolvido);
        }
        
        public ActionResult Create()
        {
            @ViewBag.Livros = RetornaSelectListItem.LivrosNaoEmprestados();
            @ViewBag.Clientes = RetornaSelectListItem.Clientes();
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "id,dataAluguel,dataPrevistaDevolucao,clienteId, livroId")] Emprestimo emprestimo)
        {
            emprestimo = Valida.DataInicialDataFinal(emprestimo);
            if (ModelState.IsValid)
            {
                emprestimo.CadastrarEmprestimo(emprestimo);
               
                return RedirectToAction("Index");
            }
            @ViewBag.Livros = RetornaSelectListItem.LivrosNaoEmprestados();
            @ViewBag.Clientes = RetornaSelectListItem.Clientes();
            return View(emprestimo);
        }

        // GET: Emprestimos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Emprestimo emprestimo = db.Emprestimos.Find(id);
            if (emprestimo == null)
            {
                return HttpNotFound();
            }
            return View(emprestimo);
        }

        // POST: Emprestimos/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "id,dataDevolucao,dataAluguel,dataPrevistaDevolucao,clienteId, livroId")] Emprestimo emprestimo)
        {
            if (ModelState.IsValid)
            {
             
                emprestimo.CadastrarDevolucao(emprestimo);
               
                return View("Extrato", emprestimo);
            }
            return View(emprestimo);            
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
