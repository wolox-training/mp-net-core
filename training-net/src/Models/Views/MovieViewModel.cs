using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
<<<<<<< 86bdffc292f7ac9113e36d107bcc7b5ceb67c8ef
using Microsoft.AspNetCore.Mvc.Rendering;
=======
using System.ComponentModel.DataAnnotations.Schema;
>>>>>>> Validation added in models

namespace training_net.Models.Views
{
  public class MovieViewModel 
    {
        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3), Required]
        public string Title { get; set; }

        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime ReleaseDate { get; set; }
        
        [RegularExpression(@"^[A-Z]+[a-zA-Z""'\s-]*$"), Required, StringLength(30)]
        public string Genre { get; set; }

        //[Range(1, 100), DataType(DataType.Currency)]
        //[Column(TypeName = "decimal(18, 2)")]
        public float Price { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5), Required]
        public string Rating { get; set; }
    }
}
