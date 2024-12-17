using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Worker
    {
        public Worker() { }
        public Worker(Guid id, string name, string position, 
            string department, string email, string phone, DateTime registrationDate)
        {
            Name = name;
            Position = position;
            Department = department;
            Email = email;
            Phone = phone;
            RegistrationDate = registrationDate;
        }

        public Guid Id { get;}
        public string Name { get;} = string.Empty;
        public string Position { get; } = string.Empty;
        public string Department { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string Phone { get; } = string.Empty;
        public DateTime RegistrationDate { get; } = new DateTime();
        public List<Rental> Rentals { get; } = new List<Rental>();
        public List<OperationHistory> OperationHistories { get; } = new List<OperationHistory>();

    }
}
