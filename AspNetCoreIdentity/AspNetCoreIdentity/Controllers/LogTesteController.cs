using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using KissLog;

namespace AspNetCoreIdentity.Controllers
{
    public class LogTesteController : Controller
    {
        private readonly ILogger<LogTesteController> _logger; //asp.net core
        private readonly KissLog.ILogger _loggerKissLog; //kisslog

        public LogTesteController(ILogger<LogTesteController> logger, KissLog.ILogger loggerKissLog)
        {
            _logger = logger;
            _loggerKissLog = loggerKissLog;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexLog()
        {
            _logger.LogError(message: "Ocorreu um erro aqui!");
            return View();
        }

        public IActionResult IndexKissLog()
        {
            _loggerKissLog.Debug("Ocorreu um erro aqui!");
            return View();
        }
        public IActionResult LoginKissLog()
        {
            _loggerKissLog.Info("Usuário autenticado com sucesso!");
            return View();
        }

    }
}