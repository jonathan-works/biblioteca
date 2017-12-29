using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace Biblioteca.Infra
{
    public class BibliotecaDB: DbContext
    {
        public BibliotecaDB(): base("DefaultConnection")
        {

        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //o entity framework por padrão cria suas tabelas com plural
            //como ele nao funciona muito bem para o portugues, é bom desabilitar 
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Emprestimo> Emprestimos { get; set; }
    }
}