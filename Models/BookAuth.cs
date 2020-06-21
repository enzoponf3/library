using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PonfeLibrary.Models
{
    [Table("Book_Auth")]
    public partial class BookAuth
    {
        [Key]
        public int BookId { get; set; }
        [Key]
        public int AuthId { get; set; }

        [ForeignKey(nameof(AuthId))]
        [InverseProperty(nameof(Author.BookAuth))]
        public virtual Author Auth { get; set; }
        [ForeignKey(nameof(BookId))]
        [InverseProperty("BookAuth")]
        public virtual Book Book { get; set; }
    }
}
