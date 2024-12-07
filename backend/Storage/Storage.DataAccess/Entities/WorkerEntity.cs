﻿using Storage.Core.Models;
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
        public string Department { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public DateTime RegistrationDate { get; set; } = new DateTime();
        public List<ToolEntity> Tools { get; } = new List<ToolEntity>();
        public List<RentalEntity> Rentals { get; } = new List<RentalEntity>();
        public List<OperationHistoryEntity> OperationHistories { get; } = new List<OperationHistoryEntity>();

    }
}
