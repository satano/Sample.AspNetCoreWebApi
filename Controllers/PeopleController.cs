using System.Collections.Generic;
using Kros.Utils;
using Microsoft.AspNetCore.Mvc;
using Sample.AspNetCoreWebApi.Models;

namespace Sample.AspNetCoreWebApi.Controllers
{

    public class PeopleController : BaseController
    {
        private readonly IPeopleRepository _peopleRepository;

        public PeopleController(IPeopleRepository peopleRepository)
        {
            _peopleRepository = Check.NotNull(peopleRepository, nameof(peopleRepository));
        }

        [HttpGet()]
        public IEnumerable<Person> GetAll() => _peopleRepository.GetPeople();

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var person = _peopleRepository.GetPerson(id);

            if (person == null)
            {
                return NotFound();
            }

            return Ok(person);
        }

        public IActionResult Create([FromBody] Person person)
        {
            _peopleRepository.AddPerson(person);

            return Ok();
        }
    }
}