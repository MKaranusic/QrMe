namespace Virgin.GenericRepository.Models.Interfaces
{
    public interface IEntity<TKey> : IPrimaryKeyEntity<TKey>, ISoftDeleteEntity, ITrackableEntity
    {
    }
}