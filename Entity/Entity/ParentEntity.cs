using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UsersModule.Entity;

public abstract class ParentEntity
{
    [Key]
    [Column("Id")]
    public Guid Id { get; set; }

    [DefaultValue(true)]
    [Column("Active")]
    public bool Active { get; set; } = true;
}

public abstract class ParentEntity<T>
{
    [Key]
    [Column("Id")]
    public required T Id { get; set; }

    [DefaultValue(true)]
    [Column("Active")]
    public bool Active { get; set; } = true;
}

public abstract class BaseEntity<T> : ParentEntity<T>
{
    [MaxLength(250)]
    [Column("CreatedBy")]
    public string CreatedBy { get; set; }

    [MaxLength(250)]
    [Column("ModifiedBy")]
    public string ModifiedBy { get; set; }

    [Column("CreatedOn")]
    public DateTime CreatedOn { get; set; }
    [Column("ModifiedOn")]
    public DateTime? ModifiedOn { get; set; }
}
