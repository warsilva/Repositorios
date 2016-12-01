using ICICore.Mvc.Web;
using ICICore.Mvc.Web.Atributos;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ICICore.Web.ViewModels
{
    [Titulo("Usuário", "Usuarios")]
    public class Usuario
    {
        [Key]
        [ScaffoldColumn(false)]
        [Pesquisa]
        public int Id { get; set; }
        [Display(GroupName = "Dados Gerais", Prompt = "Ex: Maria")]
        [MaxLength(100)]
        [MinLength(3)]
        [Pesquisa(true, true)]
        public string Nome { get; set; }
        [Display(Name = "Data de Admissão", GroupName = "Dados Gerais")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Ajuda("Data Ex: 30/11/2016")]
        public DateTime DataAdmissao { get; set; }
        [Display(GroupName = "Sistema")]
        public int Status { get; set; }
        [Required(ErrorMessage = "Perfil Obrigatorio")]
        [DataType(DataTypes.ListRadio)]
        [ForeignKey("PerfilId")]
        [Display(Name ="Perfil",GroupName ="Sistema")]
        public int PerfilId { get; set; }
        public Perfil Perfil { get; set; }
    }
}
