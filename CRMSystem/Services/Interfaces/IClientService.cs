using CRMSystem.Models;
using CRMSystem.ViewModels;

namespace CRMSystem.Services.Interfaces
{
    public interface IClientService
    {
        Client GetClient(int id);
        ClientListViewModel GetClientByPage(int page);
        void SaveClient(ClientViewModel client);
        void DeleteClient(int id);
        void AddEvent(EventViewModel eventViewModel);
        IEnumerable<Event> GetEventsByClientId(int clientId);
    }
}