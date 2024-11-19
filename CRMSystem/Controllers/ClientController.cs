using CRMSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ClientController : Controller
    {
        private readonly IClientService _service;
        private readonly int _pageSize = 4;

        public ClientController(IClientService service)
        {
            _service = service;
        }


        public ViewResult Index(int page = 1)
        {
            var result = _service.GetClientByPage(page);

            return View(result);
        }
    }
}