using System;
using System.ComponentModel.DataAnnotations;
using Virgin.GenericRepository.Models.Interfaces;

namespace Virgin.GenericRepository.Models
{
    public abstract class EntityBase<T> : IEntity<T>
    {
        [Key]
        public abstract T Id { get; set; }

        public bool IsDeleted { get; set; }
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
    }
}