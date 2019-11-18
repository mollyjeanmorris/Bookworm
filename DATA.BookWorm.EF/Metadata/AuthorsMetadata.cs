using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DATA.BookWorm.EF
{
    class AuthorsMetadata
    {
        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "Max 50 characters")]

        public string LastName { get; set; }
    }
    
    [MetadataType(typeof(AuthorsMetadata))]
    public partial class Author {
        [Display(Name = "Full Name")]
        public string FullName { get { return $"{FirstName} {LastName}"; } }

    }
}
