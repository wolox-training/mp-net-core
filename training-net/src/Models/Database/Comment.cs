using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace training_net.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentString { get; set;}
        public string User { get; set;}

        [ForeignKey("MovieForeignKey")]
        public Movie Movie { get; set;}
    }
}