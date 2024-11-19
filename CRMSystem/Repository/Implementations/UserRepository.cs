using CRMSystem.Repository.Interfaces;
using CRMSystem.Db;
using CRMSystem.Models;

using Microsoft.Extensions.Logging;
using System;


namespace CRMSystem.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;

            Users = _context.Users;
        }


        public IEnumerable<User> Users { get; }

        public void Save(User user)
        {
            if (user.Id == 0)
            {
                _context.Users.Add(user);
            }
            else
            {
                var dbEntry = _context.Users.FirstOrDefault(c => c.Id == user.Id);
                if (dbEntry != null)
                {
                    dbEntry.Name = user.Name;
                    dbEntry.IsAdmin = user.IsAdmin;
                }
            }
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _context.Users.FirstOrDefault(c => c.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                _context.SaveChanges();
            }
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(c => c.Id == id);
        }

        public void AddEvent(Event newEvent)
        {
            _context.Events.Add(newEvent);
            _context.SaveChanges();
        }
    }
}