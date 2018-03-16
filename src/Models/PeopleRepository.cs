using System.Collections.Generic;
using System.Linq;
using Kros.KORM;
using Kros.Utils;

namespace Sample.AspNetCoreWebApi.Models
{
    /// <summary>
    /// Repository for obtaining people.
    /// </summary>
    public class PeopleRepository : IPeopleRepository
    {
        private readonly IDatabase _database;

        /// <summary>
        /// Ctor.
        /// </summary>
        /// <param name="database">Korm database accesor.</param>
        public PeopleRepository(IDatabase database)
        {
            _database = Check.NotNull(database, nameof(database));
        }

        /// <inheritdoc/>
        public void Add(Person person)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Add(person);
            people.CommitChanges();
        }

        /// <inheritdoc/>
        public void Edit(Person person)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Edit(person);
            people.CommitChanges();
        }

        /// <inheritdoc/>
        public IEnumerable<Person> GetAll(int ownerId) =>
            _database.Query<Person>().Where(p => p.OwnerId == ownerId);

        /// <inheritdoc/>
        public Person Get(int id) =>
            _database.Query<Person>().FirstOrDefault(p => p.Id == id);

        /// <inheritdoc/>
        public void Delete(int id)
        {
            var people = _database.Query<Person>().AsDbSet();

            people.Delete(new Person() { Id = id });
            people.CommitChanges();
        }

        /// <inheritdoc/>
        public bool Exist(int id) =>
            _database.Query<Person>().Any(p => p.Id == id);
    }
}