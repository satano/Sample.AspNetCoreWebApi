using System.Collections.Generic;

namespace Sample.AspNetCoreWebApi.Models
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> GetAll(int ownerId);

        Person Get(int id);

        void Add(Person person);

        void Edit(Person person);

        void Delete(int id);

        bool Exist(int id);
    }
}