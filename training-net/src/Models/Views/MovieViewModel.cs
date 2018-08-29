using System;
using System.ComponentModel.DataAnnotations;

namespace training_net.Models.Views
{
  public class MovieViewModel 
    {
        public string Title { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }
        public float Price { get; set; }
    }
}
