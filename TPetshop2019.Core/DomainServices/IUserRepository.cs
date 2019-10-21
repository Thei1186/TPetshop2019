using System.Collections.Generic;
using TPetshop2019.Core.Entity;

namespace TPetshop2019.Core.DomainServices
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User Get(long id);
        void Add(User entity);
        void Edit(User entity);
        void Remove(long id);
    }
}