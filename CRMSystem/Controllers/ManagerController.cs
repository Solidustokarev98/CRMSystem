using CRMSystem.Services.Interfaces;
using CRMSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CRMSystem.Controllers
{
    public class ManagerController : Controller
    {
        private readonly IClientService _clientService;

        public ManagerController(IClientService clientService)
        {
            _clientService = clientService;
        }

        public ViewResult Index(int page = 1)
        {
            var result = _clientService.GetClientByPage(page);
            return View(result);
        }

        public ViewResult Edit(int id)
        {
            var client = _clientService.GetClient(id);
            var viewModel = new ClientViewModel
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                Address = client.Address,
                City = client.City,
                Country = client.Country,
                Phone = client.Phone,
                HomePage = client.HomePage,
                Extension = client.Extension
            };
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(ClientViewModel client)
        {
            if (ModelState.IsValid)
            {
                _clientService.SaveClient(client);
                return RedirectToAction("Index");
            }
            return View(client);
        }

        public IActionResult Delete(int id)
        {
            _clientService.DeleteClient(id);
            return RedirectToAction("Index");
        }

        public IActionResult AddEvent(int clientId)
        {
            var eventViewModel = new EventViewModel
            {
                ClientId = clientId
            };
            return View(eventViewModel);
        }

        [HttpPost]
        public IActionResult AddEvent(EventViewModel eventViewModel)
        {
            if (ModelState.IsValid)
            {
                _clientService.AddEvent(eventViewModel);
                return RedirectToAction("Index");
            }
            return View(eventViewModel);
        }

        public IActionResult Events(int clientId)
        {
            var events = _clientService.GetEventsByClientId(clientId);
            return View(events);
        }
    }
}