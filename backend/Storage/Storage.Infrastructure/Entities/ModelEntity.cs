﻿using Storage.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Infrastructure.Entities
{
    public class ModelEntity
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        
        //Navigation + ForeignKey
        public Guid CategoryId { get; set; }
        public CategoryEntity? Category { get; set; }

        public List<ToolEntity> Tools { get; } = new List<ToolEntity>();
    }
}
