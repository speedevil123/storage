using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Worker
    {


        public Guid Id { get;}
        public string Name { get;} = string.Empty;
        public string Position { get; } = string.Empty;
        public string Email { get; } = string.Empty;
        public string Phone { get; } = string.Empty;
        public DateTime RegistrationDate { get; } = new DateTime();
        //Navigation + ForeignKey
        public Guid DepartmentId { get; }
        public Department Department { get; }

        public List<Rental> Rentals { get; } = new List<Rental>();


    }
}
