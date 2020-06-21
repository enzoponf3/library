using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PonfeLibrary.Models
{
    [Table("Book_Gen")]
    public partial class BookGen
    {
        [Key]
        public int BookId { get; set; }
        [Key]
        public int GenId { get; set; }

        [ForeignKey(nameof(BookId))]
        [InverseProperty("BookGen")]
        public virtual Book Book { get; set; }
        [ForeignKey(nameof(GenId))]
        [InverseProperty(nameof(Genre.BookGen))]
        public virtual Genre Gen { get; set; }
    }
}
