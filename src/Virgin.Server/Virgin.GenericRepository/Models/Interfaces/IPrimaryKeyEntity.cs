using System.ComponentModel.DataAnnotations;

namespace Virgin.GenericRepository.Models.Interfaces
{
    public interface IPrimaryKeyEntity<T>
    {
        [Key]
        public T Id { get; set; }
    }
}