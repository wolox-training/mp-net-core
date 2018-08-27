using System;
using System.ComponentModel.DataAnnotations;

namespace training_net.Models.Views
{
  public class MovieViewModel 
    {
        [Display(Name = "Title")]
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Release Date")]

        public DateTime ReleaseDate { get; set; }
        [Display(Name = "Genre")]
        public string Genre { get; set; }
         [Display(Name = "Price")]
        public float Price { get; set; }
    }
}
