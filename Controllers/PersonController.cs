using Microsoft.AspNetCore.Mvc;
using RedisAPI.Data;
using RedisAPI.Models;

namespace RedisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonRepository _repository;

        public PersonController(IPersonRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name="GetPersonById")]
        public ActionResult<PersonName> GetPersonById(string id)
        {
            var person = _repository.GetPersonById(id);

            if (person != null)
            {
                return Ok(person);
            }
            return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<PersonName>> GetAllNames()
        {
            return Ok(_repository.GetAllPerson());
        }

        [HttpPost]
        public ActionResult<PersonName> CreatePerson(PersonName personName)
        {
            _repository.CreatePerson(personName);

            return CreatedAtRoute(nameof(GetPersonById), new {Id=personName.Id}, personName);
        }
    }
}