using System.Collections.Generic;

namespace Sample.AspNetCoreWebApi.Models
{
    /// <summary>
    /// Interface, which escribe repository for obtaining people.
    /// </summary>
    public interface IPeopleRepository
    {
        /// <summary>
        /// Get all peple for user.
        /// </summary>
        /// <param name="ownerId">Owner id.</param>
        /// <returns>
        /// All owner people.
        /// </returns>
        IEnumerable<Person> GetAll(int ownerId);

        /// <summary>
        /// Get person by id.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <returns>
        /// Personfalse with specific id.
        /// </returns>
        Person Get(int id);

        /// <summary>
        /// Add person.
        /// </summary>
        /// <param name="person">Adding person.</param>
        void Add(Person person);

        /// <summary>
        /// Edit person.
        /// </summary>
        /// <param name="person">Editing person.</param>
        void Edit(Person person);

        /// <summary>
        /// Delet user by id.
        /// </summary>
        /// <param name="id">Deleted user id.</param>
        void Delete(int id);

        /// <summary>
        /// Exist perrson with id?
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <returns><see langword="true"/> if person with specific id exist; otherwise <see langword="false"/></returns>
        bool Exist(int id);
    }
}