using Biblioteca.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Autor
    {
        public virtual int id { get; set; }
        [DisplayName("Nome do Autor")]
        public virtual string nome { get; set; }

        public void ExcluirAutor(int id)
        {
            using (BibliotecaDB db = new BibliotecaDB())
            {
               if(AutorPossuiCadastroLivro(id))
                {
                    Autor autor = db.Autores.Find(id);
                    db.Autores.Attach(autor);
                    db.Autores.Remove(autor);
                    db.SaveChanges();
                }
              
            }
        }
        private bool AutorPossuiCadastroLivro(int id)
        {
            bool existeAutorCadastroLivro = false;
            using (BibliotecaDB db = new BibliotecaDB())
            {
                int quantidade = db.Livros.Where(l => l.autorId.Equals(id)).Count();

                existeAutorCadastroLivro = quantidade.Equals(0) ? true : false;
            }
            return existeAutorCadastroLivro;
        }
    }
}