using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace MovieAppSQL.Models
{
    public class Movie
    { 
        [Key]
        public int MovieId { get; set; }

        [Required]
        public string MovieName { get; set; }

        [Required]
        [Range(1900, 2020)]
        public string ReleaseYear { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [Range(1, 10)]
        public int Rating { get; set; }
    }
}
