using ICICore.Dados.EntityFramework;
using Microsoft.EntityFrameworkCore;
namespace ICICore.Dados.Repositorios
{
    public class Usuario : BaseRepositorio<Entidades.Usuario, int>
    {
        public Usuario()
        {
            this.Contexto = new Contextos.ContextoPrincipal();
        }

        public int IncluirRetornandoId(Entidades.Usuario entidade)
        {
            try
            {
                if (entidade != null)
                {
                    this.Contexto.Add<Entidades.Usuario>(entidade);
                    this.Contexto.SaveChanges();

                }
                return entidade.Id;
            }
            catch (System.Exception ex)
            {

                throw ex;
            }
           

        }
        public void Salvar(Usuario usuario)
        {
            //this.Contexto.Database.ExecuteSqlCommand("USP_Salvar ");

            //Contexto.Database.ExecuteSqlCommandAsync("USP_Salvar");


       
        }





    }
}