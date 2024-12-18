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
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime RegistrationDate { get; set; }

        //Navigation + ForeignKey
        public Guid DepartmentId { get; set; }
        public DepartmentEntity? Department { get; set; }

        public List<RentalEntity> Rentals { get; set; } = new List<RentalEntity>();

    }
}
