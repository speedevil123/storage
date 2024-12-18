using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Department
    {
        public Department(Guid id, string name, string phoneNumber, string email, string address)
        {
            Id = id;
            Name = name;
            PhoneNumber = phoneNumber;
            Email = email;
            Address = address;
        }

        public Guid Id { get; }
        public string Name { get; }
        public string PhoneNumber { get; }
        public string Email { get; }
        public string Address { get; }

        //Navigation
        public Worker Worker { get; }
    }
}
