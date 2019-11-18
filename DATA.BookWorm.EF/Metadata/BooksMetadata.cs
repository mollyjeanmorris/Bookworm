using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.BookWorm.EF
{
    class BooksMetadata
    {
        [Required(ErrorMessage = "Author is required")]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [UIHint("MultilineText")]
        [StringLength(500, ErrorMessage = "Max 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Star rating is required")]
        [Display(Name = "Star Rating")]
        [RegularExpression("^[1-5]*$", ErrorMessage ="Please Enter a number 1-5")]
        public string StarRating { get; set; }

        [Display(Name = "Cover Art")]
        [StringLength(500, ErrorMessage = "Max 500 characters")]
        public string CoverArt { get; set; }
    }

    [MetadataType(typeof(BooksMetadata))]
    public partial class Book { }
}
