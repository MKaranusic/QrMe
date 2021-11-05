using System;

namespace Virgin.GenericRepository.Models.Interfaces
{
    public interface ITrackableEntity
    {
        public DateTime CreatedUtc { get; set; }
        public DateTime ModifiedUtc { get; set; }
    }
}