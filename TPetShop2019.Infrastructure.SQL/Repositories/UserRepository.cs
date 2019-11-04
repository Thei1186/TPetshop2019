using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TPetshop2019.Core.DomainServices;
using TPetshop2019.Core.Entity;

namespace TPetShop2019.Infrastructure.SQL.Repositories
{
    public class UserRepository: IUserRepository
    {
        private PetShopContext _context;

        public UserRepository(PetShopContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll()
        {
            return _context.Users;
        }

        public User Get(long id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public void Add(User entity)
        {
            _context.Users.Attach(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Edit(User entity)
        {
            _context.Attach(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remove(long id)
        {
            var entityRemoved = _context.Remove(new User { Id = id }).Entity;
            _context.SaveChanges();
        }
    }
}