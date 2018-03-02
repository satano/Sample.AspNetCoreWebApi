using System.Collections.Generic;
using Kros.KORM;
using System.Linq;
using Kros.Utils;

namespace Sample.AspNetCoreWebApi.Models
{
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IDatabase _database;

        public PeopleRepository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        public void AddPerson(Person person)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Add(person);
            people.CommitChanges();
        }

        public void EditPerson(Person person)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Edit(person);
            people.CommitChanges();
        }

        public IEnumerable<Person> GetPeople() =>
            _database.Query<Person>();

        public Person GetPerson(int id) =>
            _database.Query<Person>().FirstOrDefault(p => p.Id == id);

    }
}