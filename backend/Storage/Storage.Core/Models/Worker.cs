using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Worker
    {
        public Worker(Guid id, string name, string position, string email, string phoneNumber, 
            DateTime registrationDate, Guid departmentId, Department department)
        {
            Id = id;
            Name = name;
            Position = position;
            Email = email;
            PhoneNumber = phoneNumber;
            RegistrationDate = registrationDate;
            DepartmentId = departmentId;
            Department = department;
        }

        public Guid Id { get;}
        public string Name { get;} = string.Empty;
        public string Position { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string PhoneNumber { get; } = string.Empty;
        public DateTime RegistrationDate { get; } = new DateTime();

        //Navigation + ForeignKey
        public Guid DepartmentId { get; }
        public Department? Department { get; }

        public List<Rental> Rentals { get; } = new List<Rental>();


    }
}
