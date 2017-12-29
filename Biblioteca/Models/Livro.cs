using Biblioteca.Infra;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Livro
    {
       

        public virtual int id { get; set; }

        [Required]
        [DisplayName("Nome do Livro")]
        [MaxLength(100)]
        public virtual string nome { get; set; }
        [Required]
        [DisplayName("Total de Páginas")]
        public virtual int totalPaginas { get; set; }
        [Required]
        public virtual int categoriaId { get; set; }
        [Required]
        [DisplayName("Quantidade deste Livro")]
        public virtual int quantidade { get; set; }
        [Required]
        [DisplayName("Descrição do Livro")]
        [MaxLength(300)]
        public virtual string descricao { get; set; }
        [Required]
        public virtual int autorId { get; set; }

        public virtual Categoria categoria { get; set; }
        public virtual Autor autor { get; set; }


         public bool ExcluirLivro (int id )
        {
            using (BibliotecaDB db = new BibliotecaDB())
            {
                var livro = db.Livros.Find(id);


                if (livro != null)
                {
                    if (!Emprestimo.LivroEstaEmprestado(id))
                    {
                        db.Livros.Attach(livro);
                        db.Livros.Remove(livro);
                        db.SaveChanges();
                        return true;
                    }

                }
            }
            return false;           
        }

        public static void atualizaQuantidadeLivroEmprestimo(int id)
        {
            using (BibliotecaDB db = new BibliotecaDB())
            {
                Livro livro = db.Livros.Where(l => l.id.Equals(id)).FirstOrDefault();
                if (livro != null)
                {
                    livro.quantidade -= 1;
                    db.Entry(livro).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
        public static void atualizaQuantidadeLivroDevolucao(int id)
        {
            using (BibliotecaDB db = new BibliotecaDB())
            {
                Livro livro = db.Livros.Where(l => l.id.Equals(id)).FirstOrDefault();
                if (livro != null)
                {
                    livro.quantidade += 1;
                    db.Entry(livro).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
        }
    }
}