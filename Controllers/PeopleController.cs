using System.Collections.Generic;
using Kros.Utils;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Sample.AspNetCoreWebApi.Filters;
using Sample.AspNetCoreWebApi.Models;
using Sample.AspNetCoreWebApi.ViewModels;

namespace Sample.AspNetCoreWebApi.Controllers
{
    public class PeopleController : ControllerBase
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = Check.NotNull(peopleRepository, nameof(peopleRepository));
        }

        /// <summary>
        /// Get all people for authorized user.
        /// </summary>
        /// <returns>All user people.</returns>
        [HttpGet()]
        public IEnumerable<Person> GetAll() => _peopleRepository.GetAll();

        /// <summary>
        /// Get person by id.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <returns>Person if exist.</returns>
        /// <response code="404">If the person is not found.</response>
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _peopleRepository.Get(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        /// <summary>
        /// Create new person on server.
        /// </summary>
        /// <param name="person">New person.</param>
        /// <returns>Person id.</returns>
        /// <response code="201">Returns the newly-created item.</response>
        [HttpPost]
        [ModelStateValidationFilter]
        [ProducesResponseType(201)]
        public IActionResult Create([FromBody] PersonViewModel person)
        {
            var model = person.Adapt<Person>();
            _peopleRepository.Add(model);

            return Created(nameof(GetById), new { id = model.Id });
        }

        /// <summary>
        /// Update person.
        /// </summary>
        /// <param name="id">Person id.</param>
        /// <param name="person">Person data for update.</param>
        /// <response code="404">If the person is not found.</response>
        [HttpPut("{id}")]
        [ModelStateValidationFilter]
        public IActionResult Update(int id, [FromBody] PersonViewModel person)
        {
            var model = _peopleRepository.Get(id);

            if (model == null)
            {
                return NotFound();
            }

            person.Adapt(model);
            _peopleRepository.Edit(model);

            return Ok();
        }

        /// <summary>
        /// Delete user by id.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <response code="404">If the person is not found.</response>
        /// <response code="401">If actual user is not admin.</response>
        [HttpDelete("{id}")]
        [Authorize(Policy = "Admin")]
        public IActionResult Delete(int id)
        {
            if (!_peopleRepository.Exist(id))
            {
                return NotFound();
            }

            _peopleRepository.Delete(id);

            return Ok();
        }
    }
}