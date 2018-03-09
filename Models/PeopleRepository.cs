using System.Collections.Generic;
using System.Linq;
using Kros.KORM;
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

        public void Add(Person person)
        {
            var people = _database.Query<Person>().AsDbSet();


            people.Add(person);
            people.CommitChanges();
        }

        public void Edit(Person person)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Edit(person);
            people.CommitChanges();
        }

        public IEnumerable<Person> GetAll(int ownerId) =>
            _database.Query<Person>().Where(p=> p.OwnerId == ownerId);

        public Person Get(int id) =>
            _database.Query<Person>().FirstOrDefault(p => p.Id == id);

        public void Delete(int id)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Delete(new Person() { Id = id });
            people.CommitChanges();
        }

        public bool Exist(int id) =>
            _database.Query<Person>().Any(p => p.Id == id);
    }
}