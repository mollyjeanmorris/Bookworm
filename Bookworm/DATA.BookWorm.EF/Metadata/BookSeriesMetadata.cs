using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.BookWorm.EF
{
    class BookSeriesMetadata
    {
        [Required(ErrorMessage ="Author required")]
        [Display(Name = "Author")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "Title required")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string Title { get; set; }
    }

    [MetadataType(typeof(BookSeriesMetadata))]
    public partial class BookSeries { }
}
