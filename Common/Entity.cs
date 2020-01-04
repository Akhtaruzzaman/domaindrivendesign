using System;
using System.Collections.Generic;
using System.Text;

namespace Common
{
    public abstract class Entity
    {
        public Guid Id { get; set; }
        public Guid CreatedBy { get; set; }
        public string CreatedFrom { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid? UpdatedBy { get; set; }
        public string UpdatedFrom { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
