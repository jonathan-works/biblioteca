using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Biblioteca.Models
{
    public class Categoria
    {
        public virtual int id { get; set; }
        [DisplayName("Nome da categoria")]
        public virtual string nome { get; set; }
    }
}