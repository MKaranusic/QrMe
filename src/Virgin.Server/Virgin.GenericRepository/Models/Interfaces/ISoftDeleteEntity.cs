namespace Virgin.GenericRepository.Models.Interfaces
{
    public interface ISoftDeleteEntity
    {
        public bool IsDeleted { get; set; }
    }
}