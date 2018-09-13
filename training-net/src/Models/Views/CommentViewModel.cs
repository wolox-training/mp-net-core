using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace training_net.Models.Views
{
    public class CommentViewModel
    {
        public int Id { get; set;}
        public string UserName { get; set;}
        public string Text { get; set;}
    }
}
