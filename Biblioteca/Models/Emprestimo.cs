using Biblioteca.Helpers;
using Biblioteca.Infra;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;

namespace Biblioteca.Models
{
    public class Emprestimo
    {
       

        public virtual int id { get; set; }
        public virtual Livro livro { get; set; }
        public virtual Cliente cliente { get; set; }
        [Required]
        public virtual int livroId { get; set; }
        [Required]
        public virtual int clienteId { get; set; }

        [DisplayName("Data do Aluguel")]       
        public virtual DateTime dataAluguel { get; set; }

        [DisplayName("Data prevista para devolução")]        
        public virtual DateTime dataPrevistaDevolucao { get; set; }


        [DisplayName("Data da devolução")]
        public virtual DateTime dataDevolucao { get; set; }

        [DisplayName("Livro devolvido")]
        [Required]
        [DefaultValue(false)]
        public virtual bool FoiDevolvido { get; set; }

        [DisplayName("Valor Pago pelo Emprestimo")]
        [Required]
        [DefaultValue(0)]
        public virtual decimal valorPago { get; set; }


        public static bool LivroEstaEmprestado(int livroId) {
            int quantidadeLivroEmprestado;
            using (BibliotecaDB db = new BibliotecaDB())
            {
                quantidadeLivroEmprestado = db.Emprestimos.Where(_ => _.livroId.Equals(livroId)).Count();
            }
            return quantidadeLivroEmprestado.Equals(0) ? false : true;
        }

        public  void CadastrarEmprestimo(Emprestimo emprestimo)
        {
            emprestimo.dataDevolucao = emprestimo.dataPrevistaDevolucao;
            Livro.atualizaQuantidadeLivroEmprestimo(emprestimo.livroId);
            using (BibliotecaDB db = new BibliotecaDB())
            {
                db.Emprestimos.Add(emprestimo);
                db.SaveChanges();
            }
              
        }

        public void CadastrarDevolucao(Emprestimo emprestimo)
        {
            emprestimo.valorPago = Calcula.ValorEmprestimoLivro(emprestimo);
            Livro.atualizaQuantidadeLivroDevolucao(emprestimo.livroId);
            emprestimo.FoiDevolvido = true;
            using (BibliotecaDB db = new BibliotecaDB())
            {
                db.Entry(emprestimo).State = EntityState.Modified;
                db.SaveChanges();
            }
        }


    }
}