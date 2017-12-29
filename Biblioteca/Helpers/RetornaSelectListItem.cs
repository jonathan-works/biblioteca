using Biblioteca.Infra;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Biblioteca.Helpers
{
    public static class RetornaSelectListItem
    {
        private static BibliotecaDB db = new BibliotecaDB();
        public static List<SelectListItem> Autores(int id = 0)
        {
            
            var lAutores = db.Autores.ToList();
            List<SelectListItem> item = lAutores.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nome,
                    Value = a.id.ToString(),
                    Selected = (a.id == id)
                };
            });
            return item;
        
        }
        public static List<SelectListItem> Categorias(int id = 0)
        {
            var categorias = db.Categorias.ToList();
            List<SelectListItem> item = categorias.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nome,
                    Value = a.id.ToString(),
                    Selected = (a.id == id)
                };
            });
            return item;
        }
        public static List<SelectListItem> Clientes(int id = 0)
        {
            var clientes = db.Clientes.ToList();
            List<SelectListItem> item = clientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nome,
                    Value = a.id.ToString(),
                    Selected = (a.id == id)
                };
            });
            return item;
        }       
        public static List<SelectListItem> LivrosNaoEmprestados(int id = 0)
        {
            var clientes = db.Livros.Where(l => l.quantidade > 0 ).ToList();
            List<SelectListItem> item = clientes.ConvertAll(a =>
            {
                return new SelectListItem()
                {
                    Text = a.nome,
                    Value = a.id.ToString(),
                    Selected = (a.id == id)
                };
            });
            return item;
        }       
    }
}