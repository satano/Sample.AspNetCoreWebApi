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

        [HttpGet()]
        public IEnumerable<Person> GetAll() => _peopleRepository.GetAll();

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

        [HttpPost]
        [ModelStateValidationFilter]
        public IActionResult Create([FromBody] PersonViewModel person)
        {
            var model = person.Adapt<Person>();
            _peopleRepository.Add(model);

            return Created(nameof(GetById), new { id = model.Id });
        }

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

        [HttpDelete("{id}")]
        [Authorize(Policy="Admin")]
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