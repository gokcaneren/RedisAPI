using RedisAPI.Models;

namespace RedisAPI.Data
{
    public interface IPersonRepository
    {
        void CreatePerson(PersonName personName);
        PersonName? GetPersonById(string id);
        IEnumerable<PersonName> GetAllPerson();
    }
}