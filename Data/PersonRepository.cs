using System.Text.Json;
using RedisAPI.Models;
using StackExchange.Redis;

namespace RedisAPI.Data
{
    public class PersonRepository : IPersonRepository
    {
        private readonly IConnectionMultiplexer _redis;

        public PersonRepository(IConnectionMultiplexer redis)
        {
            _redis = redis;
        }

        public void CreatePerson(PersonName personName)
        {
            if (personName == null)
            {
                throw new ArgumentOutOfRangeException(nameof(personName));
            }

            var db = _redis.GetDatabase();

            var serialPerson = JsonSerializer.Serialize(personName);

            //db.StringSet(personName.Id, serialPerson);
            //db.SetAdd("NameSet", serialPerson);

            db.HashSet("hashName", new HashEntry[]{
                new HashEntry(personName.Id, serialPerson)
            });
        }

        public IEnumerable<PersonName?>? GetAllPerson()
        {
            var db = _redis.GetDatabase();
            
            //var completeSet = db.SetMembers("NameSet");

            var completeHash = db.HashGetAll("hashName");

            if (completeHash.Length > 0)
            {
                var obj = Array.ConvertAll(completeHash, val => JsonSerializer.Deserialize<PersonName>(val.Value)).ToList();

                return obj;
            }

            return null;
        }

        public PersonName? GetPersonById(string id)
        {
            var db = _redis.GetDatabase();

            //var person = db.StringGet(id);

            var person = db.HashGet("hashName", id);

            if (!string.IsNullOrEmpty(id))
            {
                return JsonSerializer.Deserialize<PersonName>(person);
            }

            return null;
        }
    }
}