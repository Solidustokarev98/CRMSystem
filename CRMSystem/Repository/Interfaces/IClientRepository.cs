using CRMSystem.Models;
using Microsoft.Extensions.Logging;

namespace CRMSystem.Repository.Interfaces
{
    public interface IClientRepository
    {
        IEnumerable<Client> Clients { get; }
        void Save(Client client);
        void Delete(int id);
        Client GetClientById(int id);
        void AddEvent(Event newEvent);
        IEnumerable<Event> GetEventsByClientId(int clientId);
    }
}