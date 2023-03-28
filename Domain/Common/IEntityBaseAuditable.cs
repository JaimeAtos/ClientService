namespace Domain.Common
{
    public interface IEntityBaseAuditable<TUserKey>
    {
        public DateTime? ModifiedDate { get; set; }
        public TUserKey ModifiedBy { get; set; }
    }
}
