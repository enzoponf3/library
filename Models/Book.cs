using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PonfeLibrary.Models
{
    public partial class Book
    {
        public Book()
        {
            BookAuth = new HashSet<BookAuth>();
            BookGen = new HashSet<BookGen>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Title { get; set; }
        [StringLength(100)]
        public string Subtitle { get; set; }
        [Required]
        [Column("ISBN")]
        [StringLength(13)]
        public string Isbn { get; set; }
        public int PubId { get; set; }

        [ForeignKey(nameof(PubId))]
        [InverseProperty(nameof(Publisher.Book))]
        public virtual Publisher Pub { get; set; }
        [InverseProperty("Book")]
        public virtual ICollection<BookAuth> BookAuth { get; set; }
        [InverseProperty("Book")]
        public virtual ICollection<BookGen> BookGen { get; set; }
    }
}
