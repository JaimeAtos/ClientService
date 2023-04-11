using Atos.Core.Abstractions;

namespace Domain.Common
{
    public abstract class EntityBase<TKey, TUserKey> : IEntityBase<TKey, TUserKey>
    {
        public TKey Id { get; set; }
        public bool State { get; set; }
        public TUserKey UserCreatorId { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
