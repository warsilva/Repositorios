using ICICore.Mvc.Web.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ICICore.Web.ViewModels
{
    [Titulo("Perfil", "Perfis")]
    public class Perfil
    {
        [Key]
        [ScaffoldColumn(false)]
        [Pesquisa]
        public int Id { get; set; }
        [Display(GroupName = "Geral")]
        [Pesquisa(true, true)]
        public string Nome { get; set; }
    }
}
