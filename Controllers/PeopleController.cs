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
    }
}