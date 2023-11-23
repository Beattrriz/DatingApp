using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    public class BuggyController : BaseApiController
    { 
        //classe para retornar erros http
        private readonly DataContext _context;
        public BuggyController(DataContext context)
        {
            _context = context;
        }

        [Authorize]
        [HttpGet("auth")] //usuario nao autorizado
        public ActionResult<string> GetSecret()
        {
            return "texto secreto";
        }

        [HttpGet("not-found")] //usuario nao encontrado
        public ActionResult<AppUser> GetNotFound()
        {
            var thing = _context.Usuarios.Find(-1);

            if (thing == null) return NotFound();

            return thing;
        }

        [HttpGet("server-error")] //erro no servidor
        public ActionResult<string> GetServerError()
        {
                var thing = _context.Usuarios.Find(-1);

                var thingToReturn = thing.ToString();

                return thingToReturn;
        }

        [HttpGet("bad-request")] //solicitação ruim
        public ActionResult<string> GetBadRequest()
        {
            return BadRequest("Essa não foi uma boa requisição");
        }

    }
}