using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using AspNetCoreIdentity.Extensions;

namespace AspNetCoreIdentity.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Secret()
        {
            return View();
        }

        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {
            return View();
        }

        [Authorize(Policy = "PodeGravar")]
        public IActionResult SecretClaimGravar()
        {
            return View("Secret");
        }

        [AllowAnonymous]
        public IActionResult SimulaErro()
        {
            throw new Exception("Ocorreu um erro.");
            return View();
        }

        [ClaimsAuthorize("Produtos", "Ler2")]
        public IActionResult AcessoNegado()
        {
            return View();
        }

        [Route("erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            //return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            var modelErro = new ErrorViewModel();

            if (id == 500)
            {
                modelErro.Titulo = "Ocorreu um erro!";
                modelErro.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate o nosso suporte.";
                modelErro.CodError = id;
            }else if (id == 404)
            {
                modelErro.Titulo = "Ooops, página não encontrada!";
                modelErro.Mensagem = "A página que você está tentando acessar não existe<br>Em caso de dúvidas, entre em contato com o nosso suporte!";
                modelErro.CodError = id;
            }
            else if (id == 403)
            {
                modelErro.Titulo = "Acesso negado!";
                modelErro.Mensagem = "Você não tem permissão para fazer isto.";
                modelErro.CodError = id;
            }
            else
            {
                return StatusCode(500);
            }

            return View("Error", modelErro);

        }

        //Apesar da classe se chamar ClaimsAuthorizeAttribute, não precisa colocar a terminação Attribute, o AspNet já faz a conversão.
        [ClaimsAuthorize("Produtos", "Ler")]
        public IActionResult ClaimsCustom()
        {
            return View("Secret");
        }

    }
}
