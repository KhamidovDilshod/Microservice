namespace Ordering.Domain.Common;
#pragma warning disable
public abstract class EntityBase
{
    public int Id { get; protected set; }
    public string CreatedBy { get; set; }
    public DateTime CreatedByDate { get; set; }
    public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}