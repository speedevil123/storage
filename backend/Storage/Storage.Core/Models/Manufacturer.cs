using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Manufacturer
    {
        public Manufacturer(Guid id, string name, string phoneNumber, string email, string country, string postIndex)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Country = country;
            PostIndex = postIndex;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string Country { get; }
        public string PostIndex { get; }

        //Navigation
        public List<Tool> Tools { get; } = new List<Tool>();
    }
}
