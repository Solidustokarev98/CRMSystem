using CRMSystem.Db;
using CRMSystem.Models;
using CRMSystem.Repository.Interfaces;
using Microsoft.Extensions.Logging;
using System;


namespace CRMSystem.Repository.Implementations
{
    public class ClientRepository : IClientRepository
    {
        private readonly AppDbContext _context;

        public ClientRepository(AppDbContext context)
        {
            _context = context;

            Clients = _context.Clients;
        }


        public IEnumerable<Client> Clients { get; }

        public void Save(Client client)
        {
            if (client.Id == 0)
            {
                _context.Clients.Add(client);
            }
            else
            {
                var dbEntry = _context.Clients.FirstOrDefault(c => c.Id == client.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = client.Name;
                    dbEntry.Email = client.Email;
                    dbEntry.Address = client.Address;
                    dbEntry.City = client.City;
                    dbEntry.Country = client.Country;
                    dbEntry.Phone = client.Phone;
                    dbEntry.HomePage = client.HomePage;
                    dbEntry.Extension = client.Extension;
                }
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var client = _context.Clients.FirstOrDefault(c => c.Id == id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                _context.SaveChanges();
            }
        }

        public Client GetClientById(int id)
        {
            return _context.Clients.FirstOrDefault(c => c.Id == id);
        }

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }

        public IEnumerable<Event> GetEventsByClientId(int clientId)
        {
            return _context.Events.Where(e => e.ClientId == clientId).ToList();
        }
    }
}