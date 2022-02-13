using System.ComponentModel.DataAnnotations;

namespace Core.Models
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        //public abstract void ChangeProperties(BaseEntity entity);
    }
}