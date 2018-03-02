using System.Collections.Generic;

namespace Sample.AspNetCoreWebApi.Models
{
    public interface IPeopleRepository
    {
        IEnumerable<Person> GetPeople();

        Person GetPerson(int id);

        void AddPerson(Person person);

        void EditPerson(Person person);
    }
}