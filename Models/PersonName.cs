using System.ComponentModel.DataAnnotations;

namespace RedisAPI.Models
{
    public class PersonName
    {
        [Required]
        public string Id { get; set; } = $"name:{Guid.NewGuid().ToString()}";
        [Required]
        public string Name { get; set; } = String.Empty;
    }
}