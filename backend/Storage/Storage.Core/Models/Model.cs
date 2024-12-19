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

        public Guid Id { get; }
        public string Name { get; }

        //Navigation + ForeignKey
        public Guid CategoryId { get; }
        public Category? Category { get; }

        public List<Tool> Tools { get; } = new List<Tool>();
    }
}
