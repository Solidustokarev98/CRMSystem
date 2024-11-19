using CRMSystem.Repository.Interfaces;
using CRMSystem.Services.Interfaces;
using CRMSystem.Models;
using CRMSystem.ViewModels;




namespace CRMSystem.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _repository;
        private readonly int _pageSize = 4;

        public ClientService(IClientRepository repository)
        {
            _repository = repository;
        }

        public Client GetClient(int id)
        {
            return _repository.GetClientById(id);
        }

        public ClientListViewModel GetClientByPage(int page)
        {
            // Получаем общее количество клиентов
            var totalItems = _repository.Clients.Count();

            // Получаем клиентов для текущей страницы
            var clients = _repository.Clients
                .OrderBy(e => e.Id)
                .Skip((page - 1) * _pageSize)
                .Take(_pageSize)
                .ToList();

            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = _pageSize,
                TotalItems = totalItems
            };

            var result = new ClientListViewModel
            {
                Clients = clients,
                PagingInfo = pagingInfo
            };

            return result;
        }

        public void SaveClient(ClientViewModel client)
        {
            var clientEntity = new Client
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
            _repository.Save(clientEntity);
        }

        public void DeleteClient(int id)
        {
            _repository.Delete(id);
        }

        public void AddEvent(EventViewModel eventViewModel)
        {
            var newEvent = new Event
            {
                ClientId = eventViewModel.ClientId,
                Type = eventViewModel.Type,
                Result = eventViewModel.Result,
                Description = eventViewModel.Description,
                FollowUpOption = eventViewModel.FollowUpOption
            };
            _repository.AddEvent(newEvent);
        }

        public IEnumerable<Event> GetEventsByClientId(int clientId)
        {
            return _repository.GetEventsByClientId(clientId);
        }
    }

}