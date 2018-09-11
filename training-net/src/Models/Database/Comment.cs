using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace training_net.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set;}
        public User User { get; set;}
        public Movie Movie { get; set;}
    }
}
