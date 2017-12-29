using Biblioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Biblioteca.Helpers
{
    public static class Calcula
    {
        private static decimal MultaAtrasoDevolucao(DateTime dataDeveriaSerDevolvido, DateTime dataDevolucao)
        {
            var numeroDias = (dataDevolucao -  dataDeveriaSerDevolvido).TotalDays;
            int valorPorDiaAtraso = 2;
            return Convert.ToDecimal(numeroDias * valorPorDiaAtraso);
        }

        public static decimal ValorEmprestimoLivro(Emprestimo emprestimo)
        {
            decimal valorEmprestimo = 7m;

            if(DateTime.Compare(emprestimo.dataDevolucao, emprestimo.dataPrevistaDevolucao) > 0)
            {
                valorEmprestimo += MultaAtrasoDevolucao(emprestimo.dataPrevistaDevolucao, emprestimo.dataDevolucao);
            }
            return valorEmprestimo;
        }
    }
}