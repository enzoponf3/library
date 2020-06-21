using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PonfeLibrary.Models
{
    public partial class Author
    {
        public Author()
        {
            BookAuth = new HashSet<BookAuth>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Required]
        [StringLength(3)]
        public string Nationality { get; set; }

        [InverseProperty("Auth")]
        public virtual ICollection<BookAuth> BookAuth { get; set; }
    }
}
