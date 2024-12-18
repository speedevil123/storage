using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage.Core.Models
{
    public class Model
    {
        public Model(Guid id, string name, Guid categoryId, Category category)
        {
            Id = id;
            Name = name;
            CategoryId = categoryId;
            Category = category;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        //Navigation + ForeignKey
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
