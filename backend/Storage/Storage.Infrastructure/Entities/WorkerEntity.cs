using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Entities
{
    public class WorkerEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Position { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = new DateTime();

        //Navigation + ForeignKey
        public Guid DepartmentId { get; set; }
        public Department? Department { get; set; }

        public List<Rental> Rentals { get; set; } = new List<Rental>();

    }
}
