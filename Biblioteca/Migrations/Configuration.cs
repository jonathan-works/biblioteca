namespace Biblioteca.Migrations
{
    using Infra;
    using System.Data.Entity.Migrations;
    using Models;
    using System.Xml.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Biblioteca.Infra.BibliotecaDB>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BibliotecaDB context)
        {
            context.Categorias.AddOrUpdate(c => c.nome, new Categoria()
            {
                nome = "Computa��o, Inform�tica e M�dias Digitais"
            },
            new Categoria()
            {
                nome = "Fic��o Cientifica"
            });
            //Apos criar digitar update-database

            context.Autores.AddOrUpdate(a => a.nome, new Autor()
            {
                nome = "J.K. Rowling"
            });
        }
    }
}
