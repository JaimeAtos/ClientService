using Atos.Core.Abstractions;

namespace Domain.Common
{
    public abstract class EntityBaseAuditable<TKey, TUserKey> : EntityBase<TKey, TUserKey>, IEntityBaseAuditable<TKey, TUserKey>
    {
        public TUserKey UserModifierId { get; set; }
        public DateTime? DateLastModify { get; set; }
    }
}
