using Kros.KORM.Metadata;
using Kros.KORM.Metadata.Attribute;

namespace Sample.AspNetCoreWebApi.Models
{
    public class Person
    {
        [Key(AutoIncrementMethodType.Custom)]
        public int Id { get; set; }

        public int OwnerId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

    }
}