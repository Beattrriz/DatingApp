using API.Extensions;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Helpers
{
    public class RegistrarAtividadeDoUsuario : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next(); // acçao API concluida e vai recuperar o contexto do resultado

            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return ; //vai retornar se o usuário não for autenticado não necessario pq o usuario ja vai estar autenticado

            var IdUsuario = resultContext.HttpContext.User.GetIdUsuario();

            var repo = resultContext.HttpContext.RequestServices.GetRequiredService<IUsuarioRepository>();
            var usuario = await repo.GetUserByIdAsync(int.Parse(IdUsuario));
            usuario.UltimaVezAtivo = DateTime.UtcNow;
            await repo.SaveAllAsync();
        }
    }
}