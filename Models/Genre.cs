using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PonfeLibrary.Models
{
    public partial class Genre
    {
        public Genre()
        {
            BookGen = new HashSet<BookGen>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }

        [InverseProperty("Gen")]
        public virtual ICollection<BookGen> BookGen { get; set; }
    }
}
