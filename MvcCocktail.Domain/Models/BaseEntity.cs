using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace MvcCocktail.Domain.Models
{
    public interface IBaseEntity
    {
        int Id { get; set; }
        DateTime Created { get; set; }
        DateTime? Modified { get; set; }
        bool Deleted { get; set; }
    }

    /// <summary>
    /// Base entity for all domain objects.
    /// </summary>
    public abstract class BaseEntity : IBaseEntity
    {
        /// <summary>
        /// Identity.
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// When the entity was created.
        /// </summary>
        public DateTime Created { get; set; }

        /// <summary>
        /// When the entity was last modified.
        /// </summary>
        public DateTime? Modified { get; set; }

        /// <summary>
        /// Has the entity been deleted.
        /// </summary>
        [IgnoreDataMember]
        public bool Deleted { get; set; }

        public BaseEntity()
        {
           Created = DateTime.Now;
        }
    }
}
