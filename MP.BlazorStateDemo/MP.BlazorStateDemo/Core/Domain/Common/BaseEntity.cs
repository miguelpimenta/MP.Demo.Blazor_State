using System.ComponentModel.DataAnnotations;

namespace MP.BlazorStateDemo.Core.Domain.Common;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
}